using Microsoft.AspNetCore.Mvc;
using Oblig4_webapp.Models;
using Oblig4_webapp.MyDbContext;
using System;
using System.Linq;

namespace Oblig4_webapp.Controllers
{
    public class BookingController : Controller
    {
        private readonly HotelDbContext _context;

        public BookingController(HotelDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bookings = _context.Bookings.ToList();
            return View(bookings);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Check if the selected room is available for the specified date range
                var existingBooking = _context.Bookings
                    .Where(b => b.RoomId == booking.RoomId &&
                                ((b.CheckInDate >= booking.CheckInDate && b.CheckInDate <= booking.CheckOutDate) ||
                                 (b.CheckOutDate >= booking.CheckInDate && b.CheckOutDate <= booking.CheckOutDate)))
                    .FirstOrDefault();

                if (existingBooking != null)
                {
                    ModelState.AddModelError(string.Empty, "The selected room is already booked for the selected date range.");
                    return View(booking);
                }

                _context.Add(booking);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        public IActionResult Edit(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check if the selected room is available for the specified date range
                var existingBooking = _context.Bookings
                    .Where(b => b.Id != id && b.RoomId == booking.RoomId &&
                                ((b.CheckInDate >= booking.CheckInDate && b.CheckInDate <= booking.CheckOutDate) || (b.CheckOutDate >= booking.CheckInDate && b.CheckOutDate <= booking.CheckOutDate)))
                    .FirstOrDefault();

                if (existingBooking != null)
                {
                    ModelState.AddModelError(string.Empty, "The selected room is already booked for the selected date range.");
                    return View(booking);
                }

                _context.Update(booking);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _context.Bookings.Find(id);
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
