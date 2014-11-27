using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDB
{
    [BsonIgnoreExtraElements]
    class Movie
    {
        [BsonId]
        public ObjectId i { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Category")]
        public string Category { get; set; }
        [BsonElement("Minutes")]
        public int Minutes { get; set; }
    }
}
