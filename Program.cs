using Microsoft.EntityFrameworkCore;

namespace Thoujour
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ThoughtsDb>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ThoughtsDb") ?? throw new InvalidOperationException("Connection string 'ThoughtsDb' not found.")));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Thoughts/Error");
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); ;

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Thoughts}/{action=Index}/{id?}");

            app.Run();
        }
    }
}