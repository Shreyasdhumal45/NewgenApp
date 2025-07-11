using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using NewgenApp.Models;

namespace NewgenApp.DataContext
{
   //  public class NewgenWebDBContext: DbContext
   public class NewgenWebDBContext : IdentityDbContext<ApplicationUser>
    {
        public NewgenWebDBContext(DbContextOptions<NewgenWebDBContext> options) : base(options)
        {
        }

      public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
