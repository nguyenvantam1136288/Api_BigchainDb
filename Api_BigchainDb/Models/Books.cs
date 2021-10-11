using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api_BigchainDb.Models
{
    public class Books
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
        public string Author { get; set; }
    }
}
