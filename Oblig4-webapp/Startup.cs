using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oblig4_webapp.MyDbContext;
using Microsoft.Extensions.Configuration;
using Library;


namespace Oblig4_webapp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Use the connection string from the appsettings.json file
            var connectionString = _configuration.GetConnectionString("Server=tcp:fredoblig4.database.windows.net,1433;Initial Catalog=dat154DB;Persist Security Info=False;User ID=badfred;Password=password;");

            // Register the DbContext with the connection string
            services.AddDbContext<HotelDbContext>(options =>
        options.UseSqlServer(connectionString));

            services.AddControllersWithViews();
            services.AddScoped<HotelDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}