using Microsoft.EntityFrameworkCore;
using Thoujour.Areas.Identity.Data;
namespace Thoujour
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ThoughtsDb>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ThoughtsDb") ?? throw new InvalidOperationException("Connection string 'ThoughtsDb' not found.")));

            builder.Services.AddDbContext<Persons>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PersonsConnection") ?? throw new InvalidOperationException("Connection string 'ThoughtsDb' not found.")));

            builder.Services.AddDefaultIdentity<Person>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Persons>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Thoughts/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); ;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Thoughts}/{action=Index}/{id?}");

            app.Run();
        }
    }
}