using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CouponService
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //Service2.WcfServiceClient s=new WcfServiceClient();


            var ser = new ServiceReference1.SoapServiceSoapClient();
            var b = ser.AddClient(new ServiceReference1.ShopLists()
              {
                  AdminId = "1",
                  CouponCode = "a",
                  ExpiryDateTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"),
                  CreatedDateTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"),
                  MobileNo = "244343",
                  Status = 1,
                  UserName = "fdffd",
                  Offer = 4,
                  ValidatedDateTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
              });
            //var b = ser.AddAdmin(new ServiceReference1.ShopAdmins() {EmailId = "a",Password = "a"});
             Response.Write(b);

            //var a = ser.UnUsedList("6");
            //foreach (var VARIABLE in a)
            //{
            //    Response.Write(VARIABLE.ValidatedDateTime);
            //}
        }
    }
}