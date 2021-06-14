using FinalProject.Data;
using FinalProject.Data.DatabaseModels;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    //Testing out our repo
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; //push
        private readonly IWeatherService _weatherService;
        private readonly ITicketmasterService _ticketService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, ITicketmasterService ticketService, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _weatherService = weatherService; 
            _ticketService = ticketService;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MainPage()
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
            //var weatherResults = await _context._weatherService(search.PostalCode);
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
            var concert = await _context.Concerts.FirstOrDefaultAsync(x => x.TicketMasterId == search.TicketMasterId);
            //var weatherResponse = await _weatherService.GetWeatherAsync(search.City);
            //var temp = weatherResponse.list.FirstOrDefault(l => true/*l.dt_txt == some date );*/);
            if (concert == null)
            {
                concert = new Concert
                {
                    TicketMasterId = search.TicketMasterId,
                    Name = search.Name,
                    Date = search.DateTime,
                    Venue = search.Venue,
                    City = search.City,
                    //Temp = weatherResponse
                    //PostalCode = search.po
                    //Weather = search.w
                };
                _context.Concerts.Add(concert);
            }

            //this portion adds the user's ID into the new concert database entry (basically fills in the UserConcerts column)
            var userConcert = new UserConcert
            {
                Concert = concert,
                User = await _userManager.GetUserAsync(User)
            };

            _context.UserConcerts.Add(userConcert);
            await _context.SaveChangesAsync();

            return View(search);
        }
    }
}
