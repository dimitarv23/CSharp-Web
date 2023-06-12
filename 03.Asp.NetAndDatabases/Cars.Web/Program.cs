namespace Cars.Web
{
    using Cars.Core.Contracts;
    using Cars.Core.Services;
    using Cars.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CarsDbContext>(options =>
                options.UseSqlServer(builder.Configuration
                    .GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<DbSeeder>();

            builder.Services.AddScoped<ICarService, CarService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<CarsDbContext>();

                DbSeeder.SeedData(dbContext);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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