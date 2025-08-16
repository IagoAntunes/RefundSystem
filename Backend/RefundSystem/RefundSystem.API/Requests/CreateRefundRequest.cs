using System.ComponentModel.DataAnnotations;

namespace RefundSystem.API.Requests
{
    public class CreateRefundRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
