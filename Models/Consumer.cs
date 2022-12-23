using System.ComponentModel.DataAnnotations;

namespace lab4.Models
{
    public class Consumer
    {
        [Key]
        public int ID { get; set; }
        public string LastName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public int NumberOfResidents { get; set; }
        public float Square { get; set; }
        public int AddressID { get; set; }
        public string PersonAccount { get; set; } = null!;


        public Address Address { get; set; } = null!;

    }
}
