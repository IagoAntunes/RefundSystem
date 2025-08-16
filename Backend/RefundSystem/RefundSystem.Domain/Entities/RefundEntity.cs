using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Domain.Entities
{
    public class RefundEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Status { get; set; }
        public string FilePath { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }

        public CategoryEntity Category { get; set; }
    }
}
