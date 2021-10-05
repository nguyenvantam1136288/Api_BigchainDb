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
    }
}
