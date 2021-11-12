using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace Event_Management.Models
{
    [CollectionName("Roles")]
    public class ApplicationRole :MongoIdentityRole<Guid>
    {
    }
}
