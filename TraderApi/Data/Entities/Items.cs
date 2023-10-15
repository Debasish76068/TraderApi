using System.ComponentModel.DataAnnotations;

namespace TraderApi.Data.Entities
{
    public class Items
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Item { get; set; }
        
        public string Note { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
