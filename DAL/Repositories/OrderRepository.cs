using ClassModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class OrderRepository : AbstractRepository, IRepository<Order>, IEnumerable<Order>
    {
              internal static EntityModel.Order ToEntity(Order order)
        {
            return new EntityModel.Order()
            {
                Order_Id = order.Order_Id,
                Customer_Id = order.Customer.Customer_Id,
                Manager_Id = order.Manager.Manager_Id,
                Date = order.Date
            };
        }

        internal static Order ToObject(EntityModel.Order order)
        {
            return order == null ? null : new Order(order.Order_Id, order.Date, CustomerRepository.ToObject(order.Customer),
                ManagerRepository.ToObject(order.Manager));
        }

        private static EntityModel.Customer AddIfNotAndGetCustomer(string firstName, string lastName)
        {
            return new CustomerRepository().AddIfNotAndGetCustomer(firstName, lastName);
        }
        private static EntityModel.Manager AddIfNotAndGetManager(string first_name, string last_name)
        {
            return new ManagerRepository().AddIfNotAndGetManager(first_name, last_name);
        }

        public void Add(Order item)
        {
            if (item == null)
                throw new ArgumentException("Order can not be null");

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var customer = AddIfNotAndGetCustomer(item.Customer.Customer_First_name, item.Customer.Customer_Last_Name);
                    var manager = AddIfNotAndGetManager(item.Manager.Manager_First_name, item.Manager.Manager_Last_name);
                    item.Customer.Customer_Id = customer.Customer_Id;
                    item.Manager.Manager_Id = manager.Manager_Id;
                    Context.OrderSet.Add(ToEntity(item));
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
                Context.SaveChanges();
            }
        }

        public void Update(int id, Order item)
        {
            if (item == null)
                throw new ArgumentException("Order can not be null");

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var element = OrderById(id);
                    if (element == null)
                        throw new ArgumentException("Order with this ID is not found");

                    if (item.Customer == null)
                        throw new ArgumentException("Customer can not be null");
                    if (item.Manager == null)
                        throw new ArgumentException("Manager can not be null");

                    var customer = AddIfNotAndGetCustomer(item.Customer.Customer_First_name, item.Customer.Customer_Last_Name);
                    var manager = AddIfNotAndGetManager(item.Manager.Manager_First_name, item.Manager.Manager_Last_name);

                    element.Date = item.Date;
                    element.Customer_Id = customer.Customer_Id;
                    element.Manager_Id = manager.Manager_Id;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public void Remove(Order item)
        {
            if (item == null)
                throw new ArgumentException("Order can not be null");

            var element = OrderById(item.Order_Id);
            if (element == null)
                throw new ArgumentException("Order with this ID is not found");

            Context.OrderSet.Remove(element);
            Context.SaveChanges();
        }

        private EntityModel.Order OrderById(int id)
        {
            return Context.OrderSet.FirstOrDefault(x => x.Order_Id == id);
        }

        public Order OrderObjectById(int id)
        {
            var saleInfo = Context.OrderSet.FirstOrDefault(x => x.Order_Id == id);
            return saleInfo == null ? null : ToObject(saleInfo);
        }
        
        public IEnumerable<Order> Items
        {
            get { return Context.OrderSet.AsEnumerable().Select(item => OrderObjectById(item.Order_Id)); }
        }

        public IEnumerator<Order> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
     
public IEnumerable<Order> GetAll()
{
    return Context.OrderSet.ToList().Select(item => this.GetRecord(item.Order_Id)).ToList();
}

public Order GetRecord(int id)
{
    var record =
        Context.OrderSet.Include("OrderItems")
            .Include("Customer")
            .Include("Manager")
            .FirstOrDefault(x => x.Order_Id == id);
    Order order = ToObject(record);
    var t = Context.OrderSet.Where(x => x.Order_Id == id).Select(x => x.Customer.Order).FirstOrDefault();
    order.Customer = CustomerRepository.ToObject(record.Customer);
    order.Manager = ManagerRepository.ToObject(record.Manager);

    List<Order_Item> listOrderItems =
        record.Order_Item.Select(item => OrderItemRepository.ToObject(item)).ToList();
    order.OrderItems = listOrderItems;

    return order;
}
    }
}
