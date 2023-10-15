using Microsoft.EntityFrameworkCore;
using TraderApi.Data;
namespace TraderApi.Models
{
    public class PurchaserRequest
    {
        public string Name { get; set; }
        public string Mobile1 { get; set; }
        public string? Mobile2 { get; set; }
        public string? Mobile3 { get; set; }
        public string UsedBy { get; set; }

    }

    }



