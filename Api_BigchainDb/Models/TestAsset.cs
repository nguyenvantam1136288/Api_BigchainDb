using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Api_BigchainDb.Models
{
    //[Serializable]
    public class TestAsset //Payawe
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }

        //[JsonProperty]
        public string msg { get; set; }
       // [JsonProperty]
        public string city { get; set; }
        //[JsonProperty]
        public string temperature { get; set; }
        //[JsonProperty]
        public DateTime datetime { get; set; }
    }
}
