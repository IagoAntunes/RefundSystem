using System.ComponentModel.DataAnnotations;

namespace RefundSystem.API.Requests
{
    public class ChangeRefundStatusRequest
    {
        [Required]
        public int Status { get; set; }
    }
}
