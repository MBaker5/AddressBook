using AddressBook.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Data

{
    public class AdressDbContext : DbContext
    {
        public AdressDbContext(DbContextOptions<AdressDbContext> options) : base(options) { }
        public DbSet<Adress> Adresses { get; set; } = null!;
    }


}
