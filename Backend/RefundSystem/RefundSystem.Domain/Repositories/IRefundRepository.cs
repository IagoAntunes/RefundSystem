using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Entities;


namespace RefundSystem.Domain.Repositories
{
    public interface IRefundRepository
    {
        Task<List<RefundEntity>> GetAllRefunds();
        Task<List<RefundEntity>> GetRefundsByUser(Guid userId);
        Task<RefundEntity> CreateRefund(RefundEntity refund);
        Task<RefundEntity?> DeleteRefund(Guid userId, Guid refundId);
        Task<RefundEntity?> ChangeStatus(Guid refundId, int newStatus);
    }
}
