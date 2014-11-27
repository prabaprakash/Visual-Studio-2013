using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Json_Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            String fString="";
            String g="";
            using (StreamReader reader = File.OpenText(@"ufo_awesome.json"))
            {
                /// fString = await reader.ReadToEndAsync();
                while ((fString =  reader.ReadLine()) != null)
                {
                    var a = fString.Split(new char[] {'"'});
                    foreach (var VARIABLE in a)
                    {
                        Console.WriteLine(VARIABLE);
                    }
                    break;
                    
                    //var a = fString.Replace("&quot;", "'");
                    //var asss = JsonConvert.DeserializeObject<Ufo>(a);
                    //g+= asss.sighted_at;
                }
               
                Console.WriteLine(g);
                Console.Read();

            }
        }
    }
    public class Ufo
    {
       
        public string sighted_at { get; set; }
     
        public string reported_at { get; set; }
      
        public string location { get; set; }
    
        public string shape { get; set; }
    
        public string duration { get; set; }
      
        public string description { get; set; }
    }
}
