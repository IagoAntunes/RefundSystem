namespace RefundSystem.Domain.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public ICollection<RefundEntity> Refunds { get; set; } = new List<RefundEntity>();
    }
}
