using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;

namespace MongoDB
{
    /// <summary>
    /// Summary description for RecipeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RecipeService : System.Web.Services.WebService
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private static MongoDB.Driver.MongoClient mongoClient = new MongoClient(ConnectionString);

        private static MongoDB.Driver.MongoServer server = mongoClient.GetServer();

        private MongoDB.Driver.MongoDatabase blog = server.GetDatabase("local");

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public String Add(String js)
        {
            try
            {
                var posts = blog.GetCollection<Roo>("Recipe");

                var ro = JsonConvert.DeserializeObject<Roo>(js);

                var a = posts.Insert(ro);
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Source;
            }

        }

        [WebMethod]
        public void Delete()
        {

        }

        [WebMethod]
        public String Search(String key)
        {
            var posts = blog.GetCollection<Roo>("Recipe");
            IQueryable<Roo> a = (from x in LinqExtensionMethods.AsQueryable<Roo>((MongoCollection)posts)
                                 where x.name.Contains(key)
                                 select x).Take(25);
            return JsonConvert.SerializeObject(a);
        }

        [WebMethod]
        public String ItemPerTime()
        {
            List<couting> sc = new List<couting>();
            var posts = blog.GetCollection<Roo>("Recipe");
            var a = (LinqExtensionMethods.AsQueryable<Roo>((MongoCollection)posts).Select(x => x.prepTime)).Distinct();
            foreach (var ab in a)
            {
                sc.Add(new couting()
                {
                    preptime = ab,
                    total = (from x in LinqExtensionMethods.AsQueryable<Roo>((MongoCollection)posts)
                             where x.prepTime == ab
                             select x).Count()


                });
            }
            return JsonConvert.SerializeObject(sc);
        }

        [WebMethod]
        public String DataAnalytics()
        {
            string maps = @"
                function() {
                    var Roo = this;
                    var a=Roo.preTime;
                    if(!a)
                    {
                     a='unknown';
                    }  
                    emit(a, { count: 1 });
                }";
            string reduces = @"
                function(key, values) {
                    var result = {count: 0};
            
                    values.forEach(function(value){
                        result.count += value.count;
                    });
                    return result;
                }";
            string finalizes = @"
                function(key, value){
            
                  value.average = value.count / value.count;
                  return value;
            
                }";

            var option = new MapReduceOptionsBuilder();
            var posts = blog.GetCollection<Roo>("Recipe");
            // var que = Query<Roo>.All(h => h.prepTime, "");
            //options.SetQuery(que);
            option.SetFinalize(finalizes);
            option.SetOutput(MapReduceOutput.Inline);

            var result = posts.MapReduce(maps, reduces, option);
            String sc = "[";
            foreach (var r in result.GetResults())
            {
                
                sc += r.ToJson()+",";
            }
            return sc+"]";
        }
    }
}
