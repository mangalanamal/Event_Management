using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Management.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {

        private readonly EventService _eventService;
        private readonly IWebHostEnvironment _hostEnviroment;
        public EventsController(EventService eventService, IWebHostEnvironment hostEnviroment)
        {
            _eventService = eventService;
            _hostEnviroment = hostEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string sds)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventsCreateViewModel events)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = new EventModel();
                    string wwwRootPath = _hostEnviroment.WebRootPath;
                    string[] days = { };
                    string imgFileName = Path.GetFileNameWithoutExtension(events.ImageFile.FileName);
                    string extension = Path.GetExtension(events.ImageFile.FileName);
                    imgFileName = imgFileName + DateTime.Now.ToString("yyMMssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "\\Image", imgFileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await events.ImageFile.CopyToAsync(fileStream);
                    }

                    obj.ImageName = imgFileName;
                    obj.EventName = events.EventName;
                    obj.Location = events.Location;
                    obj.UserID = HttpContext.Session.GetString("UserId");
                    if (events.Recarsive == true)
                    {
                        List<string> list = new List<string>();
                        if (events.M == true)
                        {
                            list.Add(DayOfWeek.Monday.ToString());
                        }
                        if (events.T == true)
                        {
                            list.Add(DayOfWeek.Tuesday.ToString());
                        }
                        if (events.W == true)
                        {
                            list.Add(DayOfWeek.Wednesday.ToString());
                        }
                        if (events.TH == true)
                        {
                            list.Add(DayOfWeek.Thursday.ToString());
                        }
                        if (events.F == true)
                        {
                            list.Add(DayOfWeek.Friday.ToString());
                        }
                        if (list.Count > 0)
                        {
                            obj.Days = list.ToArray();
                        }
                        else
                        {
                            obj.Days = days;
                        }
                        obj.StartDate = events.StartDate;
                        obj.EndDate = events.EndDate;
                        obj.IsRecarsive = true;
                        obj.Date = null;
                    }
                    else
                    {
                        obj.IsRecarsive = false;
                        obj.Date = events.Date;
                        obj.StartDate = null;
                        obj.EndDate = null;
                    }

                    _eventService.Create(obj);
                    ViewBag.Message = "Event Created Successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Can not be create event..!";
                }


            }

            return View();
        }
    }
}
