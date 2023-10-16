namespace TraderApi.Models
{
    public class DispatchRequest
    {
        public string AgentName { get; set; }
        public string Item { get; set; }
        public int BagQuantity { get; set; }
        public decimal Rate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? BookingDate { get; set; }
        public string UsedBy { get; set; }
    }
}
