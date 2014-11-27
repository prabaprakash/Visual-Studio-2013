using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using ASPMVC.Models;
using HtmlAgilityPack;

namespace ASPMVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var obj = new Praba() {Name = "Prabakarn.A", Id = "11mse1108"};
            ViewData["Detail"] = obj;
            return View();
        }

        public ActionResult OnClick()
        {
            Response.Write("Hello");
            return RedirectToAction("Index","Home");
        }

     
        public async Task<object> OnRequest()
        {
          
            for (int i = 1100; i <1130; i++)
            {
                await Task.Run(() => {
                    var request =(HttpWebRequest)WebRequest.Create("http://27.251.145.3/studenthistory-jan2014/results_submit.asp");
                                         var postData = "regno=11mse" + i;
                                         postData += "&B1=Submit";
                                         var data = Encoding.ASCII.GetBytes(postData);

                                         request.Method = "POST";
                                         request.ContentType = "application/x-www-form-urlencoded";
                                         request.ContentLength = data.Length;

                                         using (var stream = request.GetRequestStream())
                                         {
                                             stream.Write(data, 0, data.Length);
                                         }

                                         var response = (HttpWebResponse)request.GetResponse();

                                         var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                                         // FileStream a=new FileStream("/f.txt",FileMode.Create);
                                         //System.IO.File.WriteAllText(@"C:\Users\PrabaKarthi\Desktop\a.txt", responseString);
                                         if (responseString.Length != 3425)
                                         {//
                                             //add.Append(responseString);
                                             Response.Write(responseString);
                                         }
                });
               // Response.Write(add);
            }
            return null;
        }

      

        public ActionResult About()
        {
            return View();
        }

        public ActionResult HtmlAgility()
        {
            string path = @"C:\Users\PrabaKarthi\Desktop\a\vit.htm";
            var getHtmlWeb = new HtmlWeb();
            FileStream a = new FileStream(path,FileMode.Open,FileAccess.Read);
            
            string g;
            using (var b=new StreamReader(a))
            {
                g = b.ReadToEnd();
            }
            /// http://27.251.145.3/
            /// 
           //Response.Write(g);
           
            //var document = getHtmlWeb.Load("http://27.251.145.3/");
          HtmlDocument doc=new HtmlDocument();
            doc.LoadHtml(g);
            
            var aTags = doc.DocumentNode.SelectNodes("//td");
            int counter = 1;
            if (aTags != null)
            {
                foreach (var aTag in aTags)
                {
                   // if(aTag.Attributes["width"].Value=="49%")
                    // Response.Write( counter + ". " + aTag.InnerHtml + " - " + aTag.Attributes["href"].Value + "\t" + "<br />");
                        foreach (var tag in aTag.Attributes)
                        {
                           // Response.Write(tag.Name);
                            if (tag.Value == "49%")
                            {
                                Response.Write(aTag);
                            }
                        }
                  // Response.Write(aTag.InnerHtml);
                    counter++;
                }
            }
            return null;
        }
        
        [HttpPost]
        public ActionResult AjaxAction(string a,string b)
        {
            Response.Write(a+b);
            return null;
        }
    }
}
