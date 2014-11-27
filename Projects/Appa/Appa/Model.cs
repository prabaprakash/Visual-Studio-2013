using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Appa
{
    public class ShopLists
    {
        [MaxLength(15)]
        public string MobileNo { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(5)]
        public int Offer { get; set; }

        [MaxLength(20)]
        public DateTime ExpiryDateTime
        {
            get;
            set;
        }
        [MaxLength(100), PrimaryKey]
        public string CouponId { get; set; }
        [MaxLength(2)]
        public int Condition { get; set; }

        [MaxLength(50)]
        public string AdminName { get; set; }

        [MaxLength(20)]
        public DateTime AddedDateTime { get; set; }

    }

    public class ShopAdmins
    {
        [MaxLength(30), PrimaryKey]
        public string AdminName { get; set; }
        [MaxLength(30)]
        public string Password { get; set; }
    }

  
        public class mShopLists
        {


            public int Id { get; set; }


            public string MobileNo { get; set; }


            public string UserName { get; set; }


            public int Offer { get; set; }


            public string ExpiryDateTime
            {
                get;
                set;
            }

            public string CouponCode { get; set; }

            public int Status { get; set; } //1 - valid


            public string AdminId { get; set; }


            public string ValidatedDateTime { get; set; }


            public string CreatedDateTime { get; set; }

        }


        public class mShopAdmins
        {

            public int Id { get; set; }


            public string EmailId { get; set; }


            public string Password { get; set; }
        
    }


}
