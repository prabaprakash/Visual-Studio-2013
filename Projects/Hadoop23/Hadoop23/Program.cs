using Microsoft.Hadoop.MapReduce;
using Microsoft.Hadoop.WebClient.WebHCatClient;
using Microsoft.Hadoop.WebHDFS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hadoop23
{
    class Program
    {
        static void Main(string[] args)
        {
            HadoopJobConfiguration myconfig = new HadoopJobConfiguration();
            myconfig.InputPath = "/demo/recipe/in";
            myconfig.OutputFolder = "/demo/recipe/out";

            //connect to cluster

            Uri myUri = new Uri("http://127.0.0.1");
            // Copy(myUri, "/demo/recipe/in");

            string userName = "hadoop";

            string passWord = null;

            IHadoop myCluster = Microsoft.Hadoop.MapReduce.Hadoop.Connect(myUri, userName, null);

            //execute mapreduce job

            MapReduceResult jobResult = myCluster.MapReduceJob.Execute<MySimpleMapper, MySimpleReducer>(myconfig);
            int exitCode = jobResult.Info.ExitCode;



            string exitStatus = "Failure";

            if (exitCode == 0) exitStatus = "Success";



            exitStatus = exitCode + " (" + exitStatus + ")";

            Console.WriteLine();

            Console.Write("Exit Code = " + exitStatus);



        }

        //public static void Copy(Uri myUri, string destFolderName)
        //{
        //    WebHDFSClient myClient = new WebHDFSClient(myUri, "hadoop");

        //    //drop destination directory (if exists)
        //    myClient.DeleteDirectory(destFolderName, true).Wait();

        //    ////create destination directory
        //    myClient.CreateDirectory(destFolderName).Wait();


        //    ////load file to destination directory
        //    string srcFileName = "C:\\Users\\PrabaKarthi\\Desktop\\Hadoop Process\\icsdata\\chimps\\recipeitems-latest.json";
        //    myClient.CreateFile(srcFileName, destFolderName + "/" + "recipeitems-latest.json").Wait();
        //    //var a = myClient.GetDirectoryStatus(destFolderName);
        //    //var d = a.GetAwaiter();
        //    //d.OnCompleted(() =>
        //    //{

        //    //    var c = d.GetResult().Files.ToList();
        //    //    foreach (var VARIABLE in c)
        //    //    {
        //    //        Console.WriteLine(VARIABLE.PathSuffix);
        //    //    }
        //    //    Console.Read();
        //    //});
        //    //var b = myClient.GetFileStatus(destFolderName + "/part-00000").GetAwaiter();
        //    //b.OnCompleted(() =>
        //    //{
        //    //   var s= b.GetResult();
        //    //});


        //    //Console.Read();

        //}

    }

    public class MySimpleMapper : MapperBase
    {
        public override void Map(string inputLine, MapperContext context)
        {
            //split line on tabs
            try
            {
                var s = JsonConvert.DeserializeObject<Roo>(inputLine);


                //get year observed
                if (string.IsNullOrEmpty(s.prepTime))
                {
                    s.prepTime = "Un";
                }

                //get ufo type
                if (string.IsNullOrEmpty(s.name))
                {
                    s.name = "nul";
                }

                context.EmitKeyValue(s.prepTime, s.name);


                //send output

            }
            catch (InvalidCastException ex)
            {
                context.EmitKeyValue("error", "roor");
            }

        }
    }

    public class MySimpleReducer : ReducerCombinerBase
    {
        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            //count sightings by type
            try
            {
                //var query = from v in values
                //            group v by v into g
                //            select new
                //            {
                //                preptime = g.Count(),
                //            };

                //send results to output
                foreach (var item in values)
                {
                    context.EmitKeyValue(
                        key, item
                        );
                }
            }
            catch (InvalidCastException e)
            {
                context.EmitKeyValue(
                       "error", "error"
                       );
            }
        }
    }

    public class Id
    {

        public string oid { get; set; }
    }


    public class Ts
    {

        public long date { get; set; }
    }

    public class Roo
    {

        public Id _id { get; set; }

        public string name { get; set; }

        public string ingredients { get; set; }

        public string url { get; set; }

        public string image { get; set; }

        public Ts ts { get; set; }

        public string cookTime { get; set; }

        public string source { get; set; }

        public string recipeYield { get; set; }

        public string datePublished { get; set; }

        public string prepTime { get; set; }

        public string description { get; set; }
    }
}
