using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Models;
using Oblig4_webapp.MyDbContext;
using Microsoft.Extensions.DependencyInjection;


namespace Oblig4_webapp.Pages
{
    public class Pages_HomeModel : PageModel
    {
        private readonly IServiceProvider _serviceProvider;

        public Pages_HomeModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IList<Rooms> Rooms { get; set; }

        public void OnGet()
        {
            using var context = _serviceProvider.GetRequiredService<Oblig4_webapp.MyDbContext.HotelDbContext>();
            Rooms = context.Room.ToList();
        }
    }

}