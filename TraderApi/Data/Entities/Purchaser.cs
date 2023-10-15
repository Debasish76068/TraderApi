using System.ComponentModel.DataAnnotations;

namespace TraderApi.Data.Entities
{
    public class Purchaser
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(15)]
        public string Mobile1 { get; set; }

        [StringLength(15)]
        public string? Mobile2 { get; set; }

        [StringLength(15)]
        public string? Mobile3 { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
