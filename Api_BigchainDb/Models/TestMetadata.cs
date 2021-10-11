using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Api_BigchainDb.Models
{
    [Serializable]
    public class TestMetadata
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }

        [JsonProperty]
        public string msg { get; set; }
    }
}
