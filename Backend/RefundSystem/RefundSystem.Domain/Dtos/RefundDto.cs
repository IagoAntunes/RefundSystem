using RefundSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Domain.Dtos
{
    public class RefundDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Status { get; set; }
        public Guid ImageId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }

        public CategoryDto Category { get; set; }

        public string UserName { get; set; }
    }
}
