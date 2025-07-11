using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewgenApp.Models;


using NewgenApp.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the ApplicationDbContext with dependency injection
builder.Services.AddDbContext<NewgenWebDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NewgenConnectionString"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configure identity options here if needed
    options.Password.RequiredLength = 5; // Example: Set minimum password length to 5 characters
    options.Password.RequireNonAlphanumeric = false; // Example: Disable requirement for non-alphanumeric characters
})
  .AddEntityFrameworkStores<NewgenWebDBContext>()
  .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = context =>
        {
            // Handle redirect to login page when user is not authenticated
            context.Response.Redirect("/Account/Login");
            return Task.CompletedTask;
        },
        OnRedirectToAccessDenied = context =>
        {
            // Handle redirect to access denied page when user does not have permission
            context.Response.Redirect("/Home/UnAuthorized");
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

//seed Roles - Insert Roles into Role table
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "User" }; // Define your roles here
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure authentication middleware is added before authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();