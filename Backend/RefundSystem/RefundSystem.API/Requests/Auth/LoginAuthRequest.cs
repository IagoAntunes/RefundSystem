using System.ComponentModel.DataAnnotations;

namespace RefundSystem.API.Requests.Auth
{
    public class LoginAuthRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
