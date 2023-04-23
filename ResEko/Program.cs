using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResEko.Models;

namespace ResEko
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connString = builder.Configuration
    .GetConnectionString("DefaultConnection");


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<AccountService>();

            builder.Services.AddDbContext<ApplicationContext>
    (o => o.UseSqlServer(connString));

            // 1. Registera identity-klasserna och vilken DbContext som ska användas
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            // 2. Specificera att cookies ska användas och URL till inloggnings-sidan
            builder.Services.ConfigureApplicationCookie(
                o => o.LoginPath = "/Adminlogin");


            var app = builder.Build();

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
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}