using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class Adress
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string? Email { get; set;}
        [Required]
        public string City { get; set; }
        public string? Street { get; set; }
        public DateTime Created { get; set; }

    }
}
