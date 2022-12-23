using System.ComponentModel.DataAnnotations;

namespace lab4.Models
{
    public class Service
    {
        [Key]
        public int ID { get; set; }
        public string ServiceName { get; set; } = null!;
        public string PricePerSquareMeter { get; set; } = null!;
        public string PricePerPerson { get; set; } = null!;

    }
}
