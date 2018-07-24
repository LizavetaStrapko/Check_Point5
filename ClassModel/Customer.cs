using ClassModel;
using System.Collections.Generic;

namespace ClassModel
{
    public class Customer
    {
        private string firstName;
        private string secondName;

        public int Customer_Id { get; set; }

        public string Customer_First_name { get; set; }

        public string Customer_Last_Name { get; set; }

        public string Phone_number { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public Customer(int id, string FirstName, string LastName, string phone_number)
                : this(FirstName, LastName, phone_number)
        {
            Customer_Id = id;
        }

        public Customer(string firstName, string lastName, string phone_number)
        {
            Customer_First_name = firstName;
            Customer_Last_Name = lastName;
            Phone_number = phone_number;
        }

        public Customer(string firstName, string secondName)
        {
            this.firstName = firstName;
            this.secondName = secondName;
        }

        public Customer()
        {
        }
    }
}