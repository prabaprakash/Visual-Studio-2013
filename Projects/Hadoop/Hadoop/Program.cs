//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Hadoop.MapReduce;
//using Microsoft.Hadoop.WebClient.Storage;
//using Microsoft.Hadoop.WebClient.WebHCatClient;
//using Microsoft.Hadoop.WebHDFS;

//namespace Hadoop
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//           // decl();
//            HadoopJobConfiguration myconfig = new HadoopJobConfiguration();
//            myconfig.InputPath = "/demo/in";
//            myconfig.OutputFolder = "/demo/out";
//            //connect to cluster

//            //Uri myUri = new Uri("http://localhost:50070");
//            Uri myUri = new Uri("http://192.168.10.128");

//            string userName = "root";

//            string passWord = "hadoop";

//            IHadoop myCluster = Microsoft.Hadoop.MapReduce.Hadoop.Connect(myUri, userName, passWord);

//            //execute mapreduce job

//            MapReduceResult jobResult =

//                myCluster.MapReduceJob.Execute<MySimpleMapper, MySimpleReducer>(myconfig);
//            int exitCode = jobResult.Info.ExitCode;



//            string exitStatus = "Failure";

//            if (exitCode == 0) exitStatus = "Success";



//            exitStatus = exitCode + " (" + exitStatus + ")";

//            Console.WriteLine();

//            Console.Write("Exit Code = " + exitStatus);
  

//            Console.Read();
//        }
//        public static void decl()
//        {
//          //  string l = "/demo/out";
//            string srcFileName = "C:\\Users\\PrabaKarthi\\Desktop\\Hadoop Process\\4300.txt";
//            string destFolderName = "/demo/in";
//            string destFileName = "4300.txt";

//            //connect to hadoop cluster
//            Uri myUri = new Uri("http://192.168.10.128");
//            string userName = "root";
//            WebHDFSClient myClient = new WebHDFSClient(myUri, userName);

//            //drop destination directory (if exists)
//            myClient.DeleteDirectory(destFolderName, true).Wait();

//            ////create destination directory
//            myClient.CreateDirectory(destFolderName).Wait();


//            ////load file to destination directory
//            myClient.CreateFile(srcFileName, destFolderName + "/" + destFileName).Wait();

//            //list file contents of destination directory
//            //Console.WriteLine();
//            //Console.WriteLine("Contents of " + destFolderName);

//            //myClient.GetDirectoryStatus(l).ContinueWith(
//            //     ds => ds.Result.Files.ToList().ForEach(
//            //     f => Console.WriteLine("\t" + f.PathSuffix)
//            //     ));
//            //var a = myClient.GetDirectoryStatus(l);
//            //var d = a.GetAwaiter();
//            //d.OnCompleted(() =>
//            //{

//            //    var c = d.GetResult().Files.ToList();
//            //    foreach (var VARIABLE in c)
//            //    {
//            //        Console.WriteLine(VARIABLE.PathSuffix);
//            //    }
//            //});

//            //keep command window open until user presses enter
//            Console.ReadLine();
//        }

//        //public static void ma()
//        //{

//        //    int c = 0;
//        //    int gg = 0;
//        //    int hal = 0;
//        //    for (int j = 1; j <= 10000; j++)
//        //    {

//        //        for (int i = 1; i <= j; i++)
//        //        {
//        //            if (j%i == 0)
//        //            {
//        //                c++;
//        //            }

//        //        }
//        //        if (c == 2)
//        //        {
//        //            Console.WriteLine(j);
//        //            gg++;
//        //        }
//        //        else
//        //        {
//        //            hal++;
//        //        }
//        //        c = 0;



//        //    }
//        //    Console.WriteLine(gg    +  "'    " +hal);
//        //    Console.Read();

//        //}

//    }

//    public class MySimpleMapper : MapperBase
//    {
//        public override void Map(string inputLine, MapperContext context)
//        {
//            int value = int.Parse(inputLine);
//            int c = 0;
//            for (int i = 1; i <= value; i++)
//            {
//                if (value % i == 0)
//                {
//                    c++;
//                }
//            }

//            context.EmitKeyValue(c == 2 ? "Primeddd" : "not a Prime", value.ToString());



//        }
//    }

//    public class MySimpleReducer : ReducerCombinerBase
//    {
//        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
//        {
//            int mycount = 0;
//            int mysum = 0;
//            foreach (string value in values)
//            {
//                mysum += int.Parse(value);
//                mycount++;
//                context.EmitKeyValue(key, mycount + "\t" + mysum);

//            }

//        }
//    }


//}
