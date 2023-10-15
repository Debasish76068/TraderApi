using System.ComponentModel.DataAnnotations;

namespace TraderApi.Data.Entities
{
    public class Dispatch
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string AgentName { get; set; }

        [StringLength(50)]
        public string Item { get; set; }
        public int BagQuantity { get; set; }
        public decimal Rate { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? BookingDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
