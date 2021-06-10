using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace FinalProject.Controllers
{
    //Testing out our repo
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly ITicketmasterService _ticketService;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, ITicketmasterService ticketService, ApplicationDbContext context)
        {
            _logger = logger;
            _weatherService = weatherService;
            _ticketService = ticketService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel search)
        {
            var results = await _ticketService.GetEventAsync(search.PostalCode);
            var model = results._embedded?.events.Select(x => new SearchResultsViewModel
            {
                TicketMasterId = x.id,
                Name = x.name,
                DateTime = x.dates.start.dateTime,
                Venue = x._embedded.venues.FirstOrDefault()?.name,
                State = x._embedded.venues.FirstOrDefault()?.state.name,
                City = x._embedded.venues.FirstOrDefault()?.city.name
            });

            return View("SearchResults", model);
        }

        public async Task<IActionResult> AddEvent(SearchResultsViewModel search)
        {
            var model = await _context.Where(x => Concerts.TicketMasterId);
               

            // Search Conerts table (via dbcontext) based on the ticketmasterid
            // If it exist retrieve it, if not create a new one
            // Add a new UserConcerts record via dbcontext with the new concert
            
            return View();
        }
    }
}
