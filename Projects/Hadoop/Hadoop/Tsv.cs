//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Hadoop.MapReduce;
//using Microsoft.Hadoop.MapReduce.HdfsExtras.Hdfs;
//using Microsoft.Hadoop.WebClient.WebHCatClient;
//using Microsoft.Hadoop.WebHDFS;

//namespace Hadoop
//{
//    class Json
//    {
//        static void Main(string[] args)
//        {
//            HadoopJobConfiguration myconfig = new HadoopJobConfiguration();
//            myconfig.InputPath = "/demo/ufo/in";
//            myconfig.OutputFolder = "/demo/ufo/out";

//            //connect to cluster

//            Uri myUri = new Uri("http://localhost:50070");
//            Copy(myUri, "/demo/ufo/in");

//            string userName = "hadoop";

//            string passWord = null;

//            IHadoop myCluster = Microsoft.Hadoop.MapReduce.Hadoop.Connect(myUri, userName, null);

//            //execute mapreduce job

//            MapReduceResult jobResult = myCluster.MapReduceJob.Execute<MySimpleMapper, MySimpleReducer>(myconfig);
//            int exitCode = jobResult.Info.ExitCode;



//            string exitStatus = "Failure";

//            if (exitCode == 0) exitStatus = "Success";



//            exitStatus = exitCode + " (" + exitStatus + ")";

//            Console.WriteLine();

//            Console.Write("Exit Code = " + exitStatus);



//        }

//        public static void Copy(Uri myUri, string destFolderName)
//        {
//            WebHDFSClient myClient = new WebHDFSClient(myUri, "hadoop");

//            //drop destination directory (if exists)
//            myClient.DeleteDirectory(destFolderName, true).Wait();

//            ////create destination directory
//            myClient.CreateDirectory(destFolderName).Wait();


//            ////load file to destination directory
//            string srcFileName = "C:\\Users\\PrabaKarthi\\Desktop\\Hadoop Process\\icsdata\\chimps\\ufo_awesome.tsv";
//            myClient.CreateFile(srcFileName, destFolderName + "/" + "ufo_awesome.tsv").Wait();
//            //var a = myClient.GetDirectoryStatus(destFolderName);
//            //var d = a.GetAwaiter();
//            //d.OnCompleted(() =>
//            //{

//            //    var c = d.GetResult().Files.ToList();
//            //    foreach (var VARIABLE in c)
//            //    {
//            //        Console.WriteLine(VARIABLE.PathSuffix);
//            //    }
//            //    Console.Read();
//            //});
//            //var b = myClient.GetFileStatus(destFolderName + "/part-00000").GetAwaiter();
//            //b.OnCompleted(() =>
//            //{
//            //    var s = b.GetResult();
//            //});


//            //Console.Read();

//        }

//    }

//    public class MySimpleMapper : MapperBase
//    {
//        public override void Map(string inputLine, MapperContext context)
//        {
//            //split line on tabs
//            string[] inputValues = inputLine.Split('\t');
//            string dateObserved = inputValues[0].Substring(0, 4);
//            string ufoType = inputValues[2].Trim();

//            //get year observed
//            // string yearObserved = "unknown";
//            if (String.IsNullOrEmpty(dateObserved)) dateObserved = "Unknow";


//            //get ufo type
//            if (String.IsNullOrEmpty(ufoType.Trim())) ufoType = "uknown";

//            //send output
//            context.EmitKeyValue(dateObserved, ufoType);
//        }
//    }

//    public class MySimpleReducer : ReducerCombinerBase
//    {
//        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
//        {
//            ///  count sightings by type
//            var query = from v in values
//                        group v by v into g
//                        select new
//                        {
//                            ufoType = g.Key,
//                            sightings = g.Count()
//                        };

//            // send results to output
//            foreach (var item in query)
//            {
//                context.EmitKeyValue(
//                    key, item.ufoType + "\t" + item.sightings
//                    );
//            }
//        }
//    }


//}
