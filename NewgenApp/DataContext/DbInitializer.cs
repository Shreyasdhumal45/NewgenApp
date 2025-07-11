using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewgenApp.Models;

namespace NewgenApp.DataContext
{
    public class DbInitializer
    {

        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {

            using (var context = new NewgenWebDBContext(serviceProvider.GetRequiredService<DbContextOptions<NewgenWebDBContext>>()))
            {
                context.Database.EnsureCreated();

                //check and create test user if not exists
                if (await userManager.FindByNameAsync("testuser@test.com") == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "testuser@test.com",
                        Email = "testuser@test.com",
                        EmailConfirmed = true

                    };

                    await userManager.CreateAsync(user, "Test@1234");
                }
                if (await userManager.FindByNameAsync("admin@test.com") == null)
                {
                    var admin = new ApplicationUser
                    {
                        UserName = "admin@test.com",
                        Email = "admin@test.com",
                        EmailConfirmed = true

                    };
                    await userManager.CreateAsync(admin, "Admin@1234");
                }
            }
        }
    }
}
