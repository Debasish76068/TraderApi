namespace TraderApi.Models
{
    public class OrderRequest
    {
        public string Name { get; set; }
        public string item { get; set; }
        public int BagQuantity { get; set; }
        public decimal Rate { get; set; }
        public string UsedBy { get; set; }

    }
}
