using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace CouponService
{
    /// <summary>
    /// Summary description for SoapService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoapService : System.Web.Services.WebService
    {
        public BookCatalog a = new BookCatalog();

        [WebMethod]
        public string HelloWorld()
        {

            return "HelloWorld";
        }

        [WebMethod]
        public string Login(String shop)
        {
            string res = "failed";
            var shopAdmins = JsonConvert.DeserializeObject<ShopAdmins>(shop);
            var f = from x in a.ShopAdmins
                    where x.EmailId == shopAdmins.EmailId && x.Password == shopAdmins.Password
                    select x;
            if (f.Any())
            {

                if (f.Count() == 1)
                {
                    foreach (var vas in f)
                    {
                        res = vas.Id.ToString();
                    }
                    return res;
                }
                else
                {
                    return res;
                }
            }
            else
            {
                return res;
            }
        }
        [WebMethod]
        public string AddAdmin(string shop)
        {
            try
            {
                var i = 0;
                var shopAdmins = JsonConvert.DeserializeObject<ShopAdmins>(shop);
                var c = from x in a.ShopAdmins where x.EmailId.Equals(shopAdmins.EmailId) select x;
                // a.GetTable<ShopLists>().InsertOnSubmit(shopLists);
                if (c.Any())
                {
                    i = c.Count();
                }
                if (i == 0)
                {
                    a.ShopAdmins.InsertOnSubmit(shopAdmins);
                    a.SubmitChanges();
                    return 1 + "";
                }
                else if (i == 1)
                {
                    return "Username already Exist's";
                }
                else
                {
                    return "Try Again";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string CiD { get; set; }
        [WebMethod]
        public string AddClient(string  shop)
        {
            try
            {
                CiD = Id();
                ShopLists shopLists = JsonConvert.DeserializeObject<ShopLists>(shop);
                // a.GetTable<ShopLists>().InsertOnSubmit(shopLists);
                shopLists.CouponCode = CiD;
                a.ShopLists.InsertOnSubmit(shopLists);
                a.SubmitChanges();
                return CiD;
            }
            catch (Exception)
            {

                return "failed";
            }
        }

        [WebMethod]
        public string Validate(string adminId, string coupon)
        {
            var expiryTime = "";

            int i = 1, status = 2;
            try
            {
                var cur = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                var curs = DateTime.Parse(cur);
                var b =
                    (from x in a.ShopLists
                     where x.AdminId.Equals(adminId)
                     && x.CouponCode.Equals(coupon)
                     select x
                     ).Take(1);

                if (b.Any())
                {
                    foreach (var vars in b)
                    {
                        status = vars.Status;
                        expiryTime = vars.ExpiryDateTime;

                    }
                    if (expiryTime != "")
                    {
                        i = DateTime.Compare(curs, DateTime.Parse(expiryTime));
                    }
                }
                if (b.Count() == 1 && i <= 0 && status == 1)
                {
                    // a.ShopLists.Attach(shop);
                    //using (var cc = new BookCatalog())
                    //{
                    //    ShopLists shop = cc.ShopLists.First(c => c.AdminId == AdminId && c.CouponCode == Coupon);
                    //    shop.Status = 0;
                    //    cc.SubmitChanges();
                    //    cc.Dispose();
                    //    // cc.ShopLists.Attach(shop);
                    //}

                    ShopLists shop = a.ShopLists.First(c => c.AdminId == adminId && c.CouponCode == coupon);
                    shop.Status = 0;
                    a.SubmitChanges();

                    return "Valid Till " + expiryTime;
                }
                else if (b.Count() == 1 && i <= 0 && status == 0)
                {
                    return "InValid.." + "Used Already on  " + expiryTime;
                }
                else if (b.Count() == 1 && i == 1)
                {
                    return "Coupon Expired on " + expiryTime;
                }
                else
                {
                    return "Invalid";
                }
            }
            catch (Exception ex)
            {

                return "Invalidate " + ex.StackTrace;
            }
        }
        [WebMethod]
        public string UsedCoupons(String AdminId)
        {

            var b = from x in a.ShopLists where x.AdminId == AdminId && x.Status == 0 select x;
            if (b.Any())
            {
                return JsonConvert.SerializeObject(b);
            }
            else
            {
                return "empty";
            }
        }
        [WebMethod]
        public string UnUsedCoupons(String AdminId)
        {
            var a = new BookCatalog();
            var b = from x in a.ShopLists where x.AdminId == AdminId && x.Status == 1 select x;
            if (b.Any())
            {
                return JsonConvert.SerializeObject(b);
            }
            else
            {
                return "empty";
            }
        }


        [WebMethod]
        public string AllCoupons(String Password)
        {
            if (Password == "hacking")
            {
                var b = from x in a.ShopLists orderby x.ExpiryDateTime select x;
                if (b.Any())
                {
                    return JsonConvert.SerializeObject(b.ToList());
                }
                else
                {
                    return "empty";
                }
            }
            else
            {
                return "empty";
            }
        }
        [WebMethod]
        public string Test()
        {
            DateTime date1 = new DateTime(2009, 8, 1, 12, 0, 0);
            DateTime date2 = new DateTime(2009, 8, 1, 12, 0, 0);
            int result = DateTime.Compare(date1, date2);
            string relationship;
            {
                if (result < 0)
                    relationship = "is earlier than";
                else if (result == 0)
                    relationship = "is the same time as";
                else
                    relationship = "is later than";
            }

            return string.Format("{0} {1} {2}", date1, relationship, date2).ToString();
        }
        private string Id()
        {


            // get 1st random string 
            string Rand1 = RandomString(4);

            // get 2nd random string 
            string Rand2 = RandomString(4);

            // creat full rand string
            string docNum = Rand1 + "-" + Rand2;
            return docNum;
        }
        private string RandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

    }
    [Database]
    public class BookCatalog : DataContext
    {
        //private static readonly Lazy<BookCatalog> lazy =
        //      new Lazy<BookCatalog>(() => new BookCatalog());

        //public static BookCatalog Instance { get { return lazy.Value; } }
        private static readonly string localString="Data Source=(localdb)\\Projects;Initial Catalog=shoping;";
        private static readonly string connectionString =
            "Data Source=qec0kqdq9v.database.windows.net;Initial Catalog=Coupon;User ID=praba;Password=Coupon1.;Integrated Security=False;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        public BookCatalog()
            : base(connectionString)
        {

        }

        public Table<ShopAdmins> ShopAdmins;
        public Table<ShopLists> ShopLists;


    }
    [Table(Name = "ShopLists"),Serializable]

    public class ShopLists
    {

        [Column(IsPrimaryKey = true, DbType = "INT IDENTITY (1, 1) NOT NULL", IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string MobileNo { get; set; }

        [Column]
        public string UserName { get; set; }

        [Column]
        public int Offer { get; set; }

        [Column]
        public string ExpiryDateTime
        {
            get;
            set;
        }
        [Column]
        public string CouponCode { get; set; }
        [Column]
        public int Status { get; set; } //1 - valid

        [Column]
        public string AdminId { get; set; }

        [Column]
        public string ValidatedDateTime { get; set; }

        [Column]
        public string CreatedDateTime { get; set; }

    }

    [Table(Name = "ShopAdmins"),Serializable]
    public class ShopAdmins
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT IDENTITY (1, 1) NOT NULL")]
        public int Id { get; set; }

        [Column]
        public string EmailId { get; set; }

        [Column]
        public string Password { get; set; }
    }

}
