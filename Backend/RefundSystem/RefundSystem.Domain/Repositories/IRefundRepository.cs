using RefundSystem.Domain.Entities;


namespace RefundSystem.Domain.Repositories
{
    public interface IRefundRepository
    {
        Task<List<RefundEntity>> GetRefundsByUser(Guid userId);
        Task<RefundEntity> CreateRefund(RefundEntity refund);
        Task<RefundEntity?> DeleteRefund(Guid userId, Guid refundId);
    }
}
