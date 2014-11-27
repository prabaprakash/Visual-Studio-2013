using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MongoDB.Driver;

namespace MongoDB
{
    public  sealed class Single 
    {
        const string ConnectionString = "mongodb://localhost:27017";


        public MongoServer MongoServer;

       

        private static readonly Lazy<Single> lazy = new Lazy<Single>(() => new Single());
        public static Single Instance{get { return lazy.Value; }}

        private Single()
        {
            MongoDB.Driver.MongoClient mongoClient = new MongoClient(ConnectionString);
            MongoServer = mongoClient.GetServer();
        }
    }

}