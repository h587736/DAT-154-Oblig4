using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Library3.Models;
using Library3.DB_data;

namespace Oblig4_webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/room
        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _context.Rooms.ToList();
            return Ok(rooms);
        }

        // GET: api/room/5
        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // POST: api/room
        [HttpPost]
        public IActionResult CreateRoom([FromBody] Room room)
        {
            if (room == null)
            {
                return BadRequest();
            }

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        // PUT: api/room/5
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            room.RoomNumber = updatedRoom.RoomNumber;
            room.NumBeds = updatedRoom.NumBeds;
            room.Price = updatedRoom.Price;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/room/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
