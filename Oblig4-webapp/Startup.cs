using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Library3.DB_data;
using Microsoft.Extensions.Configuration;
using Library3;


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
            var connectionStrings = new ConnectionStrings();
            _configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

            // Register the DbContext with the connection string
            services.AddDbContext<HotelDbContext>(options =>
        options.UseSqlServer(connectionStrings.HotelDbConnection));

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