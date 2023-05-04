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
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == request.Email && c.Password == request.Password);

            if (customer == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Create token here and return it to client

            return Ok("Login successful.");
        }
    }
}
