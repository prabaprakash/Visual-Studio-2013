using System.ComponentModel;
using AjaxControlToolkit;
using MongoDB.ServiceReference1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MongoDB
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private RecipeServiceSoapClient sc = new RecipeServiceSoapClient();

        protected void TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (text1.Text.Length > 2)
                {
                    var a = sc.Search(text1.Text);
                    GridView1.DataSource = JsonConvert.DeserializeObject<List<Roo>>(a);
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.StackTrace);
            }
        }

        protected void ButtonTest_Click(object sender, EventArgs e)
        {
            Roo d = new Roo()
            {
                cookTime = cooktime.Text,
                datePublished = datepublished.Text,
                description = description.Text,
                image = imageurl.Text,
                url = url.Text,
                ingredients = ingridients.Text,
                name = name.Text,
                prepTime = preptime.Text,
                recipeYield = recipeyield.Text,
                source = source.Text,
                _id = new Id() { oid = name.Text },
            };
            var b = JsonConvert.SerializeObject(d);
            if (b != "")
            {
                var a = sc.Add(b);
                if (a == "1")
                {
                    Response.Write("Added");
                }
                else
                {
                    Response.Write("Name already Added");
                }
            }
        }

        protected void panelonload(object sender, EventArgs e)
        {
            try
            {
                var sc = new RecipeServiceSoapClient();
                List<MongoMap> mn = new List<MongoMap>();
                //Response.Write(sc.Search("mango"));
                mn = JsonConvert.DeserializeObject<List<MongoMap>>(sc.DataAnalytics());
                // String ca = null, data;

                Dictionary<String,Double> dic=new Dictionary<string, Double>();
                foreach (var map in mn)
                {
                    dic.Add(map._id,map.value.count);
                    //Response.Write(map._id+"-"+map.value.count+"<br/>");
                    if (map.value.count > 100)
                    {
                        // ca += map._id.Replace("PT", "") + ",";
                        BarChartSeries a = new BarChartSeries();
                        a.Name = map._id.Replace("PT", "");
                        a.BarColor = GenerateRandomColour().ToString();
                        a.Data = new[] { Convert.ToDecimal(map.value.count) };
                        BarChart1.Series.Add(a);
                      
                    }
                 
                    GridView2.DataSource = dic;
                    GridView2.DataBind();
                }
                // BarChart1.CategoriesAxis = ca.Substring(0, ca.Count() - 1);
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
      

        private Color GenerateRandomColour()
        {
            Random rnd = new Random();

            return Color.FromArgb(
              rnd.Next(0, 255),
              rnd.Next(0, 255),
              rnd.Next(0, 255));
        }
    }
}