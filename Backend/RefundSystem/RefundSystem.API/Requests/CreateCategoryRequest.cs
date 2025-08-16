using System.ComponentModel.DataAnnotations;

namespace RefundSystem.API.Requests
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
