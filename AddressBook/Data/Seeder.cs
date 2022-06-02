using AddressBook.Models;

namespace AddressBook.Data
{
    public class Seeder
    {
        public static async Task Seed(AdressDbContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            
            if(context.Adresses.Any())
            {
                return;
            }

            List<Adress> adresses = new List<Adress>
            {
                new Adress()
                {
                   Name = "Jan Kowalski",
                   PhoneNumber = "555 555 555",
                   Email = "jan.kowalski@gmail.com",
                   City = "Kątowice",
                   Street = "Katowicka 11",
                   Created = DateTime.Now
                },
                new Adress()
                {
                   Name = "John Smith",
                   PhoneNumber = "122 333 444",
                   Email = "John.smith@gmail.com",
                   City = "New York",
                   Street = "Lexington Ave 11",
                   Created = DateTime.Now
                },
                new Adress()
                {
                   Name = "Alice Wolf",
                   PhoneNumber = "434 112 431",
                   Email = "Wolf.alice@gmail.com",
                   City = "New York",
                   Street = "Lexington Ave 15",
                   Created = DateTime.Now
                }
            };

            await context.AddRangeAsync(adresses);
            await context.SaveChangesAsync();   
        }
    }
}
