using BookingSite.Controllers;
using BookingSite.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FlightBookingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();
app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();
    if (path.StartsWith("/admin"))
    {
        var role = context.Session.GetString("UserRole");
        if (role != "Admin")
        {
            context.Response.Redirect("/login");
            return;
        }
    }

    await next();
});
app.UseRouting();
// Custom route for Admin users and airports
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Admin}/{action=Index}/{id?}");
    

// Route for Users
app.MapControllerRoute(
    name: "booking",
    pattern: "dat-ve",
    defaults: new { controller = "GetTicket", action = "GetTicket" }
);

app.MapControllerRoute(
    name: "adminUsers",
    pattern: "admin/users/{action=Index}/{id?}",
    defaults: new { controller = "User" });

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FlightBookingContext>();
    context.Database.Migrate();
    string adminPassword = AuthController.HashPassword("admin");
    string clientPassword = AuthController.HashPassword("client");

    if (!context.Users.Any(u => u.Email == "admin@gmail.com"))
    {
        context.Users.Add(new User
        {
            Email = "admin@gmail.com",
            FullName = "admin",
            Password = adminPassword,
            Role = "Admin"
        });
        context.Users.Add(new User
        {
            Email = "client@gmail.com",
            FullName = "client",
            Password = clientPassword,
            Role = "Client"
        });
        context.SaveChanges();
    }
}

app.Run();
