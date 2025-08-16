using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RefundSystem.Infrastructure.Data
{
    public class RefundSystemAuthDbContext : IdentityDbContext
    {

        public RefundSystemAuthDbContext(DbContextOptions<RefundSystemAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var applicantId = "048ad5d4-0292-4698-b07e-51ff123dbb1e";
            var approverId = "4be49190-99b3-4e20-a528-87e1872579cd";
            var adminId = "20b7f89e-e971-43ac-a027-d7d5d30889ca";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = applicantId,
                    ConcurrencyStamp = applicantId,
                    Name = "Applicant",
                    NormalizedName = "Applicant".ToUpper()
                },
                new IdentityRole()
                {
                    Id = approverId,
                    ConcurrencyStamp = approverId,
                    Name = "Approver",
                    NormalizedName = "Approver".ToUpper()
                },
                new IdentityRole()
                {
                    Id = adminId,
                    ConcurrencyStamp = adminId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
