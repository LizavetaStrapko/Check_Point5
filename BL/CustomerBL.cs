using ClassModel;
using DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class CustomerBL
    {
        private CustomerRepository _customerRepository = new CustomerRepository();
        private OrderRepository _orderRepository = new OrderRepository();

        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public IEnumerable<CustomerOrderView> GetAllSummary()
        {
            List<CustomerOrderView> customerSummary = new List<CustomerOrderView>();
            foreach (var customer in _customerRepository.GetAll())
            {
                double? totalSum = 0;

                foreach (var order in customer.Orders)
                {
                    var orderObj = _orderRepository.GetRecord(order.Order_Id);
                    foreach (var orderItem in orderObj.OrderItems)
                    {
                        totalSum += orderItem.Total_cost;
                    }
                }


                customerSummary.Add(new CustomerOrderView()
                {
                    Customer_First_name = customer.Customer_First_name,
                    Customer_Last_Name = customer.Customer_Last_Name,
                    Phone_Number = customer.Phone_number,
                    OrderCount = customer.Orders.Count().ToString(),
                    TotalSum = totalSum.ToString()
                });
            }
            return customerSummary;
        }

        public void Add(Customer customer)
        {
            _customerRepository.Add(customer);
        }

        public void Delete(Customer customer)
        {
            _customerRepository.Remove(customer);
        }

        public void Update(int id, Customer item)
        {
            _customerRepository.Update(id, item);
        }

        public Customer GetRecord(int id)
        {
            return _customerRepository.GetRecord(id);
        }

        //public void Save()
        //{
        //    _customerRepository.Save();
        //}
    }
}
    