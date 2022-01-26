using FightFraud.Domain.Entities;
using FightFraud.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FightFraud.Infrastructure.Persistence
{

    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {

            var administrator = new ApplicationUser { UserName = "user@mail.com", Email = "user@mail.com" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Pa$$w0rd!");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.People.Any())
            {
                var fakePeople = new List<Person>() {
                    new Person{ FirstName = "Andrew", LastName = "Craw", DateOfBirth = new System.DateTime(1985,02,20), IdentificationNumber = "931212312"},
                    new Person{ FirstName = "Mark", LastName = "Lumberg", DateOfBirth = new System.DateTime(1985,02,20)},
                    new Person{ FirstName = "John", LastName = "Smith" },
                };

                context.People.AddRange(fakePeople);

                await context.SaveChangesAsync();
            }
        }
    }
}