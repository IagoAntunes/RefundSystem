using RefundSystem.Application.Dtos;
using RefundSystem.Domain.Dtos;

namespace RefundSystem.Application.Services.Interfaces
{
    public interface IRefundService
    {
        Task<List<RefundDto>> GetAllRefunds();
        Task<List<RefundDto>> GetRefundsByUser(Guid userId);
        Task<RefundDto> CreateRefund(CreateRefundDto creatRefund, Guid userId);
        Task<RefundDto?> DeleteRefund(Guid userId,Guid refundId);
        Task<RefundDto?> ChangeStatus(Guid refundId, int newStatus);
    }
}
