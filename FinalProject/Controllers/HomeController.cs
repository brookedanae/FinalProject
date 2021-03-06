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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Services.APIModels;

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

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var latest = await _context.UserConcerts
                .Include(x => x.Concert)
                .Where(x => x.User.Id == user.Id)
                .OrderByDescending(x => x.Id)
                .Select(x => x.Concert)
                .FirstOrDefaultAsync();

            if (latest == null)
            {
                return View(new HomeViewModel { Recommendations = new List<SearchResultsViewModel>() });
            }

            var recommendations = await _ticketService.GetEventsByKeywordAsync(latest.Venue);
            if (recommendations != null)
            {
                //RedirectToAction("Index");
                var model = recommendations._embedded?.events.Select(x => new SearchResultsViewModel
                {
                    TicketMasterId = x.id,
                    Name = x.name,
                    Date = x.dates.start.localDate,
                    Time = DateTime.TryParse(x.dates.start.localTime, out var time) ? time.ToString(@"hh\:mm\:ss tt") : null,
                    Venue = x._embedded.venues.FirstOrDefault()?.name,
                    State = x._embedded.venues.FirstOrDefault()?.state.name,
                    City = x._embedded.venues.FirstOrDefault()?.city.name,
                    Url = x.images.FirstOrDefault(x => x.url.Contains("CUSTOM"))?.url ?? string.Empty,
                    SeatMap = x.url
                });



                return View(new HomeViewModel { Recommendations = model });
            }


            
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
            var model = results._embedded?.events.Select(x => new SearchResultsViewModel
            {
                TicketMasterId = x.id,
                Name = x.name,
                Date = x.dates.start.localDate,
                Time = DateTime.TryParse(x.dates.start.localTime, out var time) ? time.ToString(@"hh\:mm\:ss tt") : null,
                Venue = x._embedded.venues.FirstOrDefault()?.name,
                State = x._embedded.venues.FirstOrDefault()?.state.name,
                City = x._embedded.venues.FirstOrDefault()?.city.name,
                Url = x.images.FirstOrDefault(x => x.url.Contains("CUSTOM"))?.url ?? string.Empty,
                SeatMap = x.url
            });

            return View("SearchResults", model);
        }

        public async Task<IActionResult> AddEvent(SearchResultsViewModel search)
        {
            var concert = await _context.Concerts.FirstOrDefaultAsync(x => x.TicketMasterId == search.TicketMasterId);
            var weatherResponse = await _weatherService.GetWeatherAsync(search.City);
            var weather = weatherResponse.list.FirstOrDefault(x => x.dt_txt.Contains(search.Date));
            if (concert == null)
            {
                concert = new Concert
                {
                    TicketMasterId = search.TicketMasterId,
                    Name = search.Name,
                    Date = search.Date,
                    Time = DateTime.TryParse(search.Time, out var time) ? time.ToString(@"hh\:mm\:ss tt") : null,
                    Venue = search.Venue,
                    City = search.City,
                    Temperature = weather?.main?.temp.ToString("0"),
                    Forecast = weather?.weather?.FirstOrDefault()?.description,
                    State = search.State,
                    SeatMap = search.SeatMap,
                    Url = search.Url
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
