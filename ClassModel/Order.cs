using System;
using System.Collections.Generic;

namespace ClassModel
{
    public class Order
    {
        private Customer customer;
        private Manager manager;

        public int Order_Id { get; set; }

        public DateTime Date { get; set; }
        
        public int Customer_Id { get; set; }

        public int Manager_Id { get; set; }

        public Manager Manager { get; set; }

        public List<Order_Item> OrderItems { get; set; }

        public Customer Customer { get; set; }

        public Order(DateTime date, int customer_id, int manager_id)
        {
            Date = date;
            Customer_Id = customer_id;
            Manager_Id = manager_id;
        }

        public Order(int order_id, DateTime date, int customer_id, int manager_id)
            : this(date, customer_id, manager_id)
        {
            Order_Id = order_id;
        }

        public Order(int order_Id, DateTime date, Customer customer, Manager manager)
        {
            Order_Id = order_Id;
            Date = date;
            this.customer = customer;
            this.manager = manager;
        }
    }
}
