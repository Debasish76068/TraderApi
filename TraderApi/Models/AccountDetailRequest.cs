namespace TraderApi.Models
{
    public class AccountDetailRequest
    {
        public string Name { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string UsedBy { get; set; }
    }
}
