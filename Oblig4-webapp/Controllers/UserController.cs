using Microsoft.AspNetCore.Mvc;
using Library3.Models;
using Library3.DB_data;
using System.Linq;

namespace Oblig4_webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public UserController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            // Check if email is already registered
            if (_context.Customers.Any(c => c.Email == request.Email))
            {
                return Conflict("Email already registered.");
            }

            // Create new customer
            var customer = new Customer
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok("Registration successful.");
        }

    }
}
