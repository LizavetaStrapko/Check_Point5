using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project5.Models
{
    public class CustomerOrderView
    {
        public string Customer_First_name { get; set; }

        public string Customer_Last_Name { get; set; }

        public string Phone_Number { get; set; }

        public string OrderCount { get; set; }

        public string TotalSum { get; set; }
    }
}