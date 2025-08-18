using Microsoft.EntityFrameworkCore;
using RefundSystem.Domain.Entities;
using RefundSystem.Domain.Repositories;
using RefundSystem.Infrastructure.Data;

namespace RefundSystem.Infrastructure.Repositories
{
    public class RefundRepository : IRefundRepository
    {
        private readonly RefundSystemDbContext dbContext;

        public RefundRepository(RefundSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RefundEntity?> ChangeStatus(Guid refundId, int newStatus)
        {
            var refundFinded = await dbContext.Refunds
                .Where(r => r.Id == refundId)
                .FirstOrDefaultAsync();
            if (refundFinded == null)
            {
                return null;
            }
            refundFinded.Status = newStatus;
            dbContext.Refunds.Update(refundFinded);
            await dbContext.SaveChangesAsync();
            return refundFinded;
        }

        public async Task<RefundEntity> CreateRefund(RefundEntity refund)
        {
            var result = await dbContext.Refunds.AddAsync(refund);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<RefundEntity?> DeleteRefund(Guid userId, Guid refundId)
        {
            var refundFinded = await dbContext.Refunds
                .Where(r => r.UserId == userId && r.Id == refundId)
                .FirstOrDefaultAsync();
            
            if(refundFinded == null)
            {
                return null;
            }
            dbContext.Refunds.Remove(refundFinded);
            await dbContext.SaveChangesAsync();

            return refundFinded;
        }

        public async Task<List<RefundEntity>> GetRefundsByUser(Guid userId)
        {
            var result = await dbContext.Refunds
                                        .Where(r => r.UserId == userId)
                                        .Include(r => r.Category)
                                        .ToListAsync();

            return result;
        }
    }
}
