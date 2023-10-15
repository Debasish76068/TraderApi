using System.ComponentModel.DataAnnotations;

namespace TraderApi.Models
{
    public class TransporterRequest
    {
        public string Name { get; set; }
        public string Mobile1 { get; set; }
        public string UsedBy { get; set; }

    }
}
