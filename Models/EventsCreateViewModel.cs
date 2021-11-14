using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Event_Management.Models
{
    public class EventsCreateViewModel
    {      
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public bool Recarsive { get; set; }
        public bool M { get; set; }
        public bool T { get; set; }
        public bool W { get; set; }
        public bool TH { get; set; }
        public bool F { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
