using System.ComponentModel.DataAnnotations;

namespace RefundSystem.API.Requests
{
    public class CreateRefundRequest
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Value { get; set; }
        public int Status { get; set; }
        public IFormFile File { get; set; }
    }
}
