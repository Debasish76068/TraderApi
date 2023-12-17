using System.ComponentModel.DataAnnotations;

namespace TraderApi.Data.Entities
{
    public class PurchaserOrder              
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int PurchaserId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public int ItemsId { get; set; }

        [StringLength(50)]
        public string item { get; set; }
        public int BagQuantity { get; set; }
        public decimal Rate { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public bool IsDeleted { get; set; }

        public bool IsDeletedFromOrderWise { get; set; }
    }
}
