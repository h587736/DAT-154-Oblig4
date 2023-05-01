using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public partial class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
