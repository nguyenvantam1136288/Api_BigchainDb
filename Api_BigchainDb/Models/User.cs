using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api_BigchainDb.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BookId { get; set; }
        public Location location { get; set; }
    }
    public class ListUser
    {
        public List<User> ListUsers { get; set; }
    }
    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
