using System.ComponentModel.DataAnnotations;
using TraderApi.Data.Entities;

namespace TraderApi.Models
{
    public class SalesBillRequest
    {
        public bool IsAgentOrder { get; set; }
        public DateTime BookingDate { get; set; }
        public string SalesYear { get; set; }
        public int SalesBillNumber { get; set; }
        public int BilitNumber { get; set; }
        public int TransporterId { get; set; }
        public string TransporterName { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int PurchaserId { get; set; }
        public string Purchaser { get; set; }
        public string Status { get; set; }
        public decimal Weight { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal TcsPercentage { get; set; }
        public decimal PackingChargePerBag { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public decimal PackingCharges { get; set; }
        public decimal Total { get; set; }
        public decimal Less { get; set; }
        public decimal GrossTotal { get; set; }
        public string UsedBy { get; set; }
        public List<Models.Particular> Particulars { get; set; }
    }
}
