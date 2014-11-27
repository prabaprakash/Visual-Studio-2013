using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB
{

    [BsonIgnoreExtraElements]
    public class log
    {

        [BsonId]
        public ObjectId i { get; set; }

        [BsonElement("id")]
        public string id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class StudentDetails
    {
        [BsonElement("name")]
        public String Name { get; set; }

        [BsonElement("regno")]
        public String RegNo { get; set; }

        [BsonElement("program")]
        public String Program { get; set; }

        [BsonElement("branch")]
        public String Branch { get; set; }


        [BsonElement("batch")]
        public String Batch { get; set; }

        [BsonElement("cgpa")]

        public String Cgpa { get; set; }

        [BsonElement("gpa")]
        public String Gpa { get; set; }


    }
    [BsonIgnoreExtraElements]
    public class Id
    {
        [BsonElement("oid")]
        public string oid { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Ts
    {
        [BsonElement("date")]
        public long date { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class Roo
    {
        [BsonElement("_id")]
        public Id _id { get; set; }
        [BsonElement("name")]
        public string name { get; set; }
        [BsonElement("ingredients")]
        public string ingredients { get; set; }
        [BsonElement("url")]
        public string url { get; set; }
        [BsonElement("image")]
        public string image { get; set; }
        [BsonElement("ts")]
        public Ts ts { get; set; }
        [BsonElement("cookTime")]
        public string cookTime { get; set; }
        [BsonElement("source")]
        public string source { get; set; }
        [BsonElement("recipeYield")]
        public string recipeYield { get; set; }
        [BsonElement("datePublished")]
        public string datePublished { get; set; }
        [BsonElement("preTime")]
        public string prepTime { get; set; }
        [BsonElement("description")]
        public string description { get; set; }
    }
    public class Models
    {
    }
    [BsonIgnoreExtraElements]
    public class Ufo
    {
        [BsonElement("sighted_at")]
        public string sighted_at { get; set; }
        [BsonElement("reported_at")]
        public string reported_at { get; set; }
        [BsonElement("location")]
        public string location { get; set; }
        [BsonElement("shape")]
        public string shape { get; set; }
        [BsonElement("duration")]
        public string duration { get; set; }
        [BsonElement("description")]
        public string description { get; set; }
    }
    public class couting
    {
        public String preptime { get; set; }
        public int total { get; set; }
    }
    public class Value
    {
        public double count { get; set; }
        public double average { get; set; }
    }
    public class MongoMap
    {
        public String _id { get; set; }
        public Value value { get; set; }
    }
}