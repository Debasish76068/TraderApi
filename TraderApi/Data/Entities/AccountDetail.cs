using System.ComponentModel.DataAnnotations;

namespace TraderApi.Data.Entities
{
    public class AccountDetail
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(30)]
        public string AccountNumber { get; set; }

        [StringLength(30)]
        public string IfscCode { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
