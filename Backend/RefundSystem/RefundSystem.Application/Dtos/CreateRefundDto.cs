using Microsoft.AspNetCore.Http;

namespace RefundSystem.Application.Dtos
{
    public class CreateRefundDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Value { get; set; }
        public IFormFile File { get; set; }
    }
}