using System.Collections.Generic;

namespace ClassModel
{
   public class Manager
    {
        public int Manager_Id { get; set; }

        public string Manager_First_name { get; set; }
        
        public string Manager_Last_name { get; set; }
       
        public IEnumerable<Order> Orders { get; set; }

        public Manager(int id, string FirstName, string LastName)
                : this(FirstName, LastName)
        {
            Manager_Id = id;
        }

        public Manager(string firstName, string lastName)
        {
            Manager_First_name = firstName;
            Manager_Last_name = lastName;
        }

        public Manager()
        {
        }
    }
}
