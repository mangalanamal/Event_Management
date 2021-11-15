using Event_Management.Models;
using Event_Management.MongodbContext;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Management.Services
{
    public class EventService
    {
        private readonly IMongoCollection<EventModel> _event;
        public EventService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _event = database.GetCollection<EventModel>("Events");
        }

        public List<EventModel> Get() =>
            _event.Find(e => true).ToList();

        public EventModel Get(string id) =>
            _event.Find<EventModel>(e => e.Id == id).FirstOrDefault();

        public EventModel Create(EventModel eventModel)
        {
            _event.InsertOne(eventModel);
            return eventModel;
        }

        public void Update(string id, EventModel eventModel) =>
            _event.ReplaceOne(e => e.Id == id, eventModel);

        public void Remove(EventModel eventModel) =>
            _event.DeleteOne(e => e.Id == eventModel.Id);

        public void Remove(string id) =>
            _event.DeleteOne(e => e.Id == id);

        public List<EventModel> Search(string eventName) =>
           _event.Find<EventModel>(e => e.EventName.Contains(eventName)).ToList();

        public List<EventModel> SearchList(string userId) {
         return _event.Find<EventModel>(e => e.UserID == userId).ToList();
        }

    }
}
