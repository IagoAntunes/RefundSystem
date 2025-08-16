using System.ComponentModel.DataAnnotations;

namespace RefundSystem.API.Requests.Auth
{
    public class RegisterAuthRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string[] Roles { get; set; }
    }
}
