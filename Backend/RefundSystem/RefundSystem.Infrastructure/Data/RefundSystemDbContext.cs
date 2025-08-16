using Microsoft.EntityFrameworkCore;
using RefundSystem.Domain.Entities;

namespace RefundSystem.Infrastructure.Data
{
    public class RefundSystemDbContext : DbContext
    {
        public RefundSystemDbContext(DbContextOptions<RefundSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<RefundEntity> Refunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefundEntity>(entity =>
            {
                entity.Property(r => r.Value)
                      .HasColumnType("decimal(18,2)");
            });
        }
    }

}
