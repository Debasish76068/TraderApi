using System.ComponentModel.DataAnnotations;

namespace TraderApi.Data.Entities
{
    public class Particulars
    {
        public int Id { get; set; }
        public int SaleBillId { get; set; }
        
        public SalesBill SalesBill { get; set; }

        [StringLength(50)]
        public string Item { get; set; }
        public int Bags { get; set; }
        public decimal Rate { get; set; }
    }
}
