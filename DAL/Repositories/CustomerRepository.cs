using ClassModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CustomerRepository : AbstractRepository, IRepository<Customer>, IEnumerable<Customer>
    {
        internal static EntityModel.Customer ToEntity(Customer customer)
        {
            return new EntityModel.Customer()
            {
                Customer_Id = customer.Customer_Id,
                Customer_First_name = customer.Customer_First_name,
                Customer_Last_Name = customer.Customer_Last_Name,
                Phone_number = customer.Phone_number,
            };
        }

        internal static Customer ToObject(EntityModel.Customer customer)
        {
            return customer == null ? null : new Customer(customer.Customer_Id, customer.Customer_First_name,
                customer.Customer_Last_Name, customer.Phone_number);
        }

        private static void Check(Customer item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }

        private static void Check(EntityModel.Customer item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }

        public void Add(Customer item)
        {
            if (item == null)
                throw new ArgumentException("Customer can not be null");

            Context.CustomerSet.Add(ToEntity(item));
            Context.SaveChanges();
        }

        public void Update(int id, Customer item)
        {
            if (item == null)
                throw new ArgumentException("Customer can not be null");
            //Check(item);
            var element = CustomerById(item.Customer_Id);
            if (element == null)
                throw new ArgumentException("Customer with this ID is not found");
            //Check(element);

            element.Customer_First_name = item.Customer_First_name;
            element.Customer_Last_Name = item.Customer_Last_Name;
            element.Phone_number = item.Phone_number;
            Context.SaveChanges();
        }

        public void Remove(Customer item)
        {
            if (item == null)
                throw new ArgumentException("Customer can not be null");
            //Check(item);
            var element = CustomerById(item.Customer_Id);
            if (element == null)
                throw new ArgumentException("Customer with this ID is not found");
            // Check(element);

            Context.CustomerSet.Remove(element);
            Context.SaveChanges();
        }

        private EntityModel.Customer CustomerById(int id)
        {
            return Context.CustomerSet.FirstOrDefault(x => x.Customer_Id == id);
        }

        public Customer CustomerModelById(int id)
        {
            var customer = CustomerById(id);
            return ToObject(customer);
        }

        internal EntityModel.Customer CustomerByName(string FirstName, string LastName)
        {
            return Context.CustomerSet.FirstOrDefault(x => x.Customer_First_name == FirstName && x.Customer_Last_Name == LastName);
        }

        internal EntityModel.Customer AddIfNotAndGetCustomer(string firstName, string secondName)
        {
            var client = CustomerByName(firstName, secondName);
            if (client == null)
            {
                client = Context.CustomerSet.Add(ToEntity(new Customer(firstName, secondName)));
                Context.SaveChanges();
            }
            return client;
        }

        public IEnumerable<Customer> Items
        {
            get { return Context.CustomerSet.AsEnumerable().Select(item => ToObject(item)); }
        }

        public IEnumerator<Customer> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
public  IEnumerable<Customer> GetAll()
        {
            var customers = Context.CustomerSet.ToList();

            return customers.Select(item => this.GetRecord(item.Customer_Id)).ToList();
        }

               public Customer GetRecord(int id)
        {
            var record = Context.CustomerSet.Include("Order").FirstOrDefault(x => x.Customer_Id == id);
            Customer customer = ToObject(record);

            List<Order> orders = record.Order.Select(item => OrderRepository.ToObject(item)).ToList();
            customer.Orders = orders;

            return customer;
        }
    }
}
