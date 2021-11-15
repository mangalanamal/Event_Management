using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace Event_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EventService _eventService;
        public HomeController(ILogger<HomeController> logger, EventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }        

        public IActionResult Index()
        {

            List<EventModel> list = new List<EventModel>();
            if (HttpContext.Session.GetString("UserId") != null)
            {
                var userId = HttpContext.Session.GetString("UserId");
                list = _eventService.SearchList(userId);
            }
            return View(list);
        }
                    
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
