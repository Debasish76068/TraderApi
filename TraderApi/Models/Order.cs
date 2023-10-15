namespace TraderApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string AgentName { get; set; }
        public string item { get; set; }
        public int BagQuantity { get; set; }
        public decimal Rate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
