using Microsoft.AspNetCore.Identity;
using ModelsB.Authentication_and_Authorization_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextB
{
    public class BTechDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUserB> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new ApplicationUserB()
                {
                    UserName = "Khaled",
                    Email = "admin@gmail.com",
                    PasswordHash = "Kha123456",
                    UserType = "Admin",
                    Address = "Cairo",
                    City = "Giza",
                    Country = "Egypt",
                    PostalCode = "12345",
                    EmailConfirmed = true,

                };

                var result = await userManager.CreateAsync(User);
                if (result.Succeeded)
                {

                    var Role = await roleManager.RoleExistsAsync("Admin");
                    if (!Role)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));

                    }

                    await userManager.AddToRoleAsync(User, "Admin");
                }

            }
        }
    }
}
