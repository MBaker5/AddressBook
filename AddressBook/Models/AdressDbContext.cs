using Microsoft.EntityFrameworkCore;

namespace AddressBook.Models

{
    public class AdressDbContext: DbContext
    {
        public AdressDbContext(DbContextOptions<AdressDbContext> options): base(options) { }
        public DbSet<Adress> Adresses { get; set; } = null!;
    }


}
