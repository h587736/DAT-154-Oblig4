using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library3.Models;
using Library3.DB_data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Library3;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace FrontDeskApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HotelDbContext dx = new();
        private readonly LocalView<Room> Rooms;
        private readonly LocalView<Booking> Bookings;
        private readonly LocalView<Customer> Customers;

        public MainWindow()
        {
            InitializeComponent();

            Rooms = dx.Rooms.Local;
            Bookings = dx.Bookings.Local;
            Customers = dx.Customers.Local;

            dx.Rooms.Load();
            dx.Bookings.Load();
            dx.Customers.Load();

            DateTime checkInDate = DateTime.Now;
            DateTime checkOutDate = DateTime.Now.AddDays(1);

            // Populates the lists with all the relevant entries
            allRooms();
            allBookings();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchBox.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                // If the search field is empty, all the rooms are displayed
                allRooms();
            }
            else if (int.TryParse(searchText, out int roomNumber))
            {

                var filteredList = (from r in Rooms
                                    join b in Bookings on r.Id equals b.RoomId into roomBookings
                                    from rb in roomBookings.DefaultIfEmpty()
                                    join c in Customers on rb?.CustomerId equals c.Id into customerBookings
                                    from cb in customerBookings.DefaultIfEmpty()
                                    where r.RoomNumber.ToString().Contains(searchText)
                                    select new
                                    {
                                        RoomNumber = r.RoomNumber,
                                        RoomType = r.RoomType,
                                        NumBeds = r.NumBeds,
                                        Price = r.Price,
                                        AvailableFrom = r.AvailableFrom,
                                        AvailableTo = r.AvailableTo,
                                        Booked = r.Booked,
                                        CustomerFirstName = cb != null ? cb.FirstName : "",
                                        CustomerLastName = cb != null ? cb.LastName : "",
                                        CheckInDate = rb != null ? rb.CheckInDate : (DateTime?)null
                                    }).ToList();

                roomList.ItemsSource = filteredList;
            }
        }

        private void SearchBooking_Click(object sender, RoutedEventArgs e)
        {
            string BookingSearchText = bookingSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(BookingSearchText))
            {
                // If the search field is empty, all the bookings are displayed
                allBookings();
            }
            else
            {
                var filteredList = (from b in Bookings
                                    join c in Customers on b.CustomerId equals c.Id
                                    where c.LastName == bookingSearch.Text
                                    select new
                                    {
                                        LastName = c.LastName,
                                        FirstName = c.FirstName,
                                        Email = c.Email,
                                        RoomNumber = b.Room.RoomNumber,
                                        CheckInDate = b.CheckInDate,
                                        CheckOutDate = b.CheckOutDate
                                    }).ToList();

                bookingList.ItemsSource = filteredList;
            }
        }

        //Class for adding students to a course
        private void changeRoomButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                var customerLastName = customerLastNameTextBox.Text;
                var oldRoomNumber = int.Parse(oldRoomNumberTextBox.Text);
                var newRoomNumber = int.Parse(newRoomNumberTextBox.Text);

                var oldRoom = context.Rooms.SingleOrDefault(r => r.RoomNumber == oldRoomNumber);
                if (oldRoom == null)
                {
                    // Room not found
                    return;
                }

                var newRoom = context.Rooms.SingleOrDefault(r => r.RoomNumber == newRoomNumber);
                if (newRoom == null)
                {
                    // Room not found
                    return;
                }

                var booking = context.Bookings.Include(b => b.Customer).SingleOrDefault(b => b.RoomId == oldRoom.Id && b.Customer.LastName == customerLastName);
                if (booking == null)
                {
                    // Booking not found
                    return;
                }

                booking.Room = newRoom;
                context.SaveChanges();
            }

        }
            private void allRooms()
        {
            var filteredList = (from r in Rooms
                                join b in Bookings on r.Id equals b.RoomId into roomBookings
                                from rb in roomBookings.DefaultIfEmpty()
                                join c in Customers on rb?.CustomerId equals c.Id into customerBookings
                                from cb in customerBookings.DefaultIfEmpty()
                                select new
                                {
                                    RoomNumber = r.RoomNumber,
                                    RoomType = r.RoomType,
                                    NumBeds = r.NumBeds,
                                    Price = r.Price,
                                    AvailableFrom = r.AvailableFrom,
                                    AvailableTo = r.AvailableTo,
                                    Booked = r.Booked,
                                    CustomerFirstName = cb != null ? cb.FirstName : "",
                                    CustomerLastName = cb != null ? cb.LastName : "",
                                    CheckInDate = rb != null ? rb.CheckInDate : (DateTime?)null

                                }).ToList();
            roomList.ItemsSource = filteredList;
        }

        private void allBookings()
        {
            var filteredList = (from b in Bookings
                                join c in Customers on b.CustomerId equals c.Id
                                select new
                                {
                                    LastName = c.LastName,
                                    FirstName = c.FirstName,
                                    Email = c.Email,
                                    RoomNumber = b.Room.RoomNumber,
                                    CheckInDate = b.CheckInDate,
                                    CheckOutDate = b.CheckOutDate
                                }).ToList();

            bookingList.ItemsSource = filteredList;

        }
    }
}
