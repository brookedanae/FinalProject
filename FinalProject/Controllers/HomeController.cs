using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, ITicketmasterService ticketService)
        {
            _logger = logger;
            _weatherService = weatherService;
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            var results = await _ticketService.GetEventAsync(model.PostalCode);

            var model = results.Select(x => new SearchResultsViewModel(x._event.name));

            return View("SearchResults", model);
        }
    }
}
