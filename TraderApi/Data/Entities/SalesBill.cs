using System.ComponentModel.DataAnnotations;

namespace TraderApi.Data.Entities
{
    public class SalesBill
    {
        public int Id { get; set; }
        public bool IsAgentOrder { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime BookingDate { get; set; }
        public string SalesYear { get; set; }
        public int SalesBillNumber { get; set; }
        public string OrderNo { get; set; }
        public int BilitNumber { get; set; }

        public int? TransporterId { get; set; }
        [StringLength(50)]
        public string? TransporterName { get; set; }
        public int? AgentId { get; set; }

        [StringLength(50)]
        public string? AgentName { get; set; }
        public int? PurchaserId { get; set; }

        [StringLength(50)]
        public string? Purchaser { get; set; }

        [StringLength(10)]
        public string? Status { get; set; }

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
        public decimal IgstPercentage { get; set; }
        public decimal TcsPointPercentage { get; set; }
        public decimal BillAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public string Note { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection< Particulars> Particulars { get; set; }
    }
}
