using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library3.Models;
using Library3.DB_data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace Oblig4_webapp.Pages
{
    public class Pages_HomeModel : PageModel
    {
        private readonly IServiceProvider _serviceProvider;

        public Pages_HomeModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IList<Room> Rooms { get; set; }

        public void OnGet()
        {
            using var context = _serviceProvider.GetRequiredService<Library3.DB_data.HotelDbContext>();
            Rooms = context.Rooms.ToList();
        }
    }

}
