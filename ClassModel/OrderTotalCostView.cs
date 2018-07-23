using System;

namespace ClassModel
{
   public class OrderTotalCostView
    {
        public int Order_Id { get; set; }

        public DateTime? Date { get; set; }

        public int? Customer_Id { get; set; }

        public int? Manager_Id { get; set; }

        public int? NumberOfItems { get; set; }

        public double? Total_cost { get; set; }
    }
}
