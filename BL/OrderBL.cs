using ClassModel;
using DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class OrderBL
    {
        private OrderRepository _orderRepository = new OrderRepository();

        public IEnumerable<OrderTotalCostView> GetAll()
        {
            List<OrderTotalCostView> listOrderSummaries = new List<OrderTotalCostView>();

            foreach (var item in _orderRepository.GetAll())
            {
                listOrderSummaries.Add(new OrderTotalCostView()
                {
                    Order_Id = item.Order_Id,
                    Date = item.Date,
                    Manager_Id = item.Manager_Id,
                    Customer_Id = item.Customer_Id,
                    Total_cost = item.OrderItems.Sum(x => x.Total_cost),
                    NumberOfItems = item.OrderItems.Sum(x => x.Item_amount)
                });
            }

            return listOrderSummaries;
        }

        public void Update(int id, Order item)
        {
            _orderRepository.Update(id, item);
        }

        public Order GetRecord(int id)
        {
            return _orderRepository.GetRecord(id);
        }

        //public void Save()
        //{
        //    _orderRepository.Save();
        //}
    }
}
