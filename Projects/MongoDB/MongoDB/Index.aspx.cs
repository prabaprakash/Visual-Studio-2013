using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace MongoDB
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.Connect To Server

            const string ConnectionString = "mongodb://localhost:27017";
            MongoDB.Driver.MongoClient mongoClient = new MongoClient(ConnectionString);

            MongoDB.Driver.MongoServer server = mongoClient.GetServer();

            MongoDB.Driver.MongoDatabase blog = server.GetDatabase("local");

            //2.Create Collection

            //blog.CreateCollectionSettings<Roo>("Recipe");
            // blog.CreateCollection("Recipe");
            //  blog.CreateCollection("ufo");
            // Set Select Collection   ->  log

            //3. GEt All Collection Name

            var collection = blog.GetCollectionNames();
            Response.Write("Database Collections<br/>");
            foreach (var VARIABLE in collection)
            {
                Response.Write("Collection Name .: " + VARIABLE + "<br/>");
            }

            //4. Select Query
            Response.Write("Select Query for Collection Log<br/>");
            var logtable = blog.GetCollection<log>("log");
            var b = logtable.FindAll();
            foreach (var VARIABLE in b)
            {
                Response.Write(VARIABLE.id + "</br>");
            }

            //5. Insert Query
            Response.Write("Insert Query for Collection Log<br/>");
            var lo = new log() { id = "12", name = "sam" };
            var a = logtable.Insert(lo);
            Response.Write("<br/><br/><br/>" + a.Ok);
            foreach (var VARIABLE in b)
            {
                Response.Write("</br>" + VARIABLE.id + VARIABLE.name + "</br>");
            }

            //6. Select By Where Query
            Response.Write("Select By 'Where' Query for Collection Log<br/>");
            Response.Write("Continue</br>");
            var query1 = Query<log>.EQ(f => f.id, "12");
            var c = logtable.Find(query1);
            foreach (var VARIABLE in c)
            {
                Response.Write(VARIABLE.id + "<br/>");
            }

            //7. Update Query
            Response.Write("Update Query for Collection Log<br/>");
            var query2 = Query<log>.EQ(f => f.id, "12");
            var update = Update<log>.Set(h => h.id, "10");
            logtable.Update(query2, update);

            //8. Remove Items
            Response.Write("Remove Query for Collection Log<br/>");
            var query = Query<log>.EQ(h => h.id, "10");
            logtable.Remove(query);

            //9. Linq
            //Response.Write("LINQ Select Query for Collection Log<br/>");
            //var ff = from x in logtable.AsQueryable<log>()
            //         where x.id == "11"
            //         select x;

            //foreach (var r in ff)
            //{
            //   Response.Write(r.id + "</br");
            //}

            //10. Delete

            //var asss = blog.GetCollection<StudentDetails>("StudentDetails");
            //var ss = new StudentDetails()
            //{
            //    Name = "praba",
            //    Batch = "2011-2016",
            //    Cgpa = "8.157",
            //    Program = "ms",
            //    RegNo = "11mse108"
            //};
            //asss.Insert(ss);
            //asss.Drop();
            //logtable.Drop();

            //11. Linq Count
            //String s="";
            Response.Write("Data Binding the Collection Recipe by Linq<br/>");
            List<Roo> ro = new List<Roo>();
            var posts = blog.GetCollection<Roo>("Recipe");
            var cc =
                (from x in LinqExtensionMethods.AsQueryable<Roo>((MongoCollection)posts)
                 where x.prepTime.Equals("PT10M")
                 select x).Take(20);
            foreach (var v in cc)
            {
                ro.Add(v);
                //s += v.cookTime + "----" + v.datePublished + "----" + v.description + "----" + v.image + "----" + v.ingredients + "----" + v.name + "----" + v.prepTime + "----" +
                // v.recipeYield + "----" + v.source + "----" + v.ts.date + "----" + v.url + "----" + v._id.oid + "<br/>";
            }
            Roo d = new Roo()
            {
                cookTime = "32",
                datePublished = "ff",
                description = "f",
                image = "f",
                url = "fff",
                ingredients = "fff",
                name = "ffff",
                prepTime = "20",
                recipeYield = "33",
                source = "33",
                _id = new Id() { oid = "fgg" },
            };
            //posts.Insert(d);

            // Response.Write(s);

            GridView1.DataSource = ro;
            GridView1.DataBind();

            //foreach (Department department in departments.FindAll())
            //{
            //    lst.Add(department);
            //}

            //12. Json To MongoDB
            //12.a Recipe
            //var post = blog.GetCollection<Roo>("Recipe");
            //string path = Server.MapPath("recipeitems-latest.json");
            //String fString = "";
            //using (StreamReader reader = File.OpenText(path))
            //{
            //    /// fString = await reader.ReadToEndAsync();
            //    while ((fString = await reader.ReadLineAsync()) != null)
            //    {
            //        var asss = JsonConvert.DeserializeObject<Roo>(fString);
            //        if (asss.name.Equals("Drop Biscuits and Sausage Gravy"))
            //        {
            //            //  break;

            //        }
            //        post.Insert(asss);
            //        //// Response.Write(asss.description);

            //    }
            //}
            //12.b.UFO
            //var ufo = blog.GetCollection<Ufo>("ufo");
            //string path = Server.MapPath("ufo_awesome.json");
            //String fString = "";
            //String g="";
            //using (StreamReader reader = File.OpenText(path))
            //{
            //    /// fString = await reader.ReadToEndAsync();
            //    while ((fString = await reader.ReadLineAsync()) != null)
            //    {
            //       var a = fString.Replace("&quot;", "'");

            //     //  var jsonText = System.Web.HttpUtility.HtmlDecode(a.Replace(@"/",""));
            //       // var d = HttpUtility.HtmlDecode(jsonText.Replace(@"\", ""));

            //        var asss = JsonConvert.DeserializeObject<Ufo>(a);
            //        g+= asss.sighted_at ;
            //       // break;
            //      // ufo.Insert(asss);
            //        //// Response.Write(asss.description);

            //    }
            //   Response.Write(g);
            //}
            //ufo.Insert(new Ufo()
            //{
            //    description = "dsd",
            //    duration = "ds",
            //    reported_at = "dsad",
            //    shape = "dsad",
            //    sighted_at = "fsf"
            //});
            ///// var bsonDoc = BsonDocument.Parse(fString);

            //13.Data Grid
            // var posts = blog.GetCollection<Roo>("Recipe");
            //// var searchQuery = Query.EQ("someName", "someValue"); // you can place any search condition here
            // //if you want all documents from collection use FindAll
            // var cursor = posts.FindAll();
            // cursor.SetLimit(50); // you can specify limit
            // // set sort orders
            // cursor.SetSortOrder(SortBy.Ascending("name"));

            // var resultList = cursor.ToList();
            // GridView1.DataSource = resultList;
            // GridView1.DataBind();
            MapReduce();
        }

        public void MapReduce()
        {
            const string ConnectionString = "mongodb://localhost:27017";
            MongoDB.Driver.MongoClient mongoClient = new MongoClient(ConnectionString);

            MongoDB.Driver.MongoServer server = mongoClient.GetServer();

            MongoDB.Driver.MongoDatabase blog = server.GetDatabase("local");
            blog.DropCollection("movie");
            blog.CreateCollection("movie");
            var mov = blog.GetCollection<Movie>("movie");
            var movies = new List<Movie>
         {
        new Movie { Title="The Perfect Developer",
                    Category="SciFi", Minutes=118 },
        new Movie { Title="Lost In Frankfurt am Main",
                    Category="Horror", Minutes=122 },
        new Movie { Title="The Infinite Standup",
                    Category="Horror", Minutes=341 }
        };
            mov.InsertBatch(movies);


            string map = @"
    function() {
        var movie = this;
        emit(movie.Category, { count: 1, totalMinutes: movie.Minutes });
    }";
            string reduce = @"        
    function(key, values) {
        var result = {count: 0, totalMinutes: 0 };

        values.forEach(function(value){               
            result.count += value.count;
            result.totalMinutes += value.totalMinutes;
        });

        return result;
    }";

            string finalize = @"
    function(key, value){
      
      value.average = value.totalMinutes / value.count;
      return value;

    }";

            
            var options = new MapReduceOptionsBuilder();
            options.SetFinalize(finalize);
            options.SetOutput(MapReduceOutput.Inline);
            var results = mov.MapReduce(map, reduce, options);

            foreach (var result in results.GetResults())
            {
                Response.Write(result.ToJson());
            }











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
            try
            {
                var option = new MapReduceOptionsBuilder();
                var posts=blog.GetCollection<Roo>("Recipe");
                // var que = Query<Roo>.All(h => h.prepTime, "");
                //options.SetQuery(que);
                option.SetFinalize(finalizes);
                option.SetOutput(MapReduceOutput.Inline);

                var result = posts.MapReduce(maps, reduces,option);
                List<MongoMap> mn=new List<MongoMap>();
                foreach (var r in result.GetResults())
                {
                    //Response.Write(r.ToJson()+ "</br>");
                    mn.Add(JsonConvert.DeserializeObject<MongoMap>(r.ToJson()));

                }
            }
            catch (MongoException ex)
            {
                Response.Write(ex.StackTrace + ex.Message + ex.Source);
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int a = e.NewPageIndex;

            const string ConnectionString = "mongodb://localhost:27017";
            MongoDB.Driver.MongoClient mongoClient = new MongoClient(ConnectionString);

            var server = mongoClient.GetServer();

            var blog = server.GetDatabase("local");
            List<Roo> ro = new List<Roo>();
            var posts = blog.GetCollection<Roo>("Recipe");
            var cc =
                (from x in LinqExtensionMethods.AsQueryable<Roo>((MongoCollection)posts)
                 select x).Skip(a * 10).Take(10);
            foreach (var v in cc)
            {
                ro.Add(v);
                //s += v.cookTime + "----" + v.datePublished + "----" + v.description + "----" + v.image + "----" + v.ingredients + "----" + v.name + "----" + v.prepTime + "----" +
                // v.recipeYield + "----" + v.source + "----" + v.ts.date + "----" + v.url + "----" + v._id.oid + "<br/>";
            }
            //Response.Write(s);

            GridView1.DataSource = ro;
            GridView1.DataBind();
        }

        public void AllTables()
        {
        }

        public void Insert()
        {
        }

        public void Update()
        {
        }

        public void Delete()
        {
        }

        public void Select()
        {
        }
    }
}