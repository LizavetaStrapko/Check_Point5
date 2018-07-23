using ClassModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class OrderItemRepository : AbstractRepository, IEnumerable<Order_Item>
    {
           internal static EntityModel.Order_Item ToEntity(Order_Item order_Item)
        {
            return new EntityModel.Order_Item()
            {
                Item_amount = order_Item.Item_amount,
                Total_cost = order_Item.Total_cost
            };
        }
        internal static Order_Item ToObject(EntityModel.Order_Item order_Item)
        {
            return order_Item == null ? null : new Order_Item(order_Item.Item_amount, order_Item.Total_cost);
        }

        public void Add(Order_Item item)
        {
            if (item == null)
                throw new ArgumentException("Order_Item can not be null");

            Context.Order_ItemSet.Add(ToEntity(item));
            Context.SaveChanges();
        }

       
        public Order_Item Order_ItemById(int id)
        {
            var order_Item = Context.Order_ItemSet.FirstOrDefault(x => x.Order_Id == x.Order.Order_Id && x.Item_Id == x.Item.Item_Id);
            return order_Item == null ? null : ToObject(order_Item);
        }

        public IEnumerable<Order_Item> Items
        {
            get { return Context.Order_ItemSet.AsEnumerable().Select(item => Order_ItemById(item.Order_Id)); }
        }

        public IEnumerator<Order_Item> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Update(EntityModel.Order_Item obj1, EntityModel.Order_Item obj2)
        {
            var l =
                Context.Order_ItemSet
                    .FirstOrDefault(x => x.Order_Id == obj1.Order.Order_Id && x.Item_Id == obj1.Item.Item_Id);

            if (l != null)
            {
                l.Item_amount = obj2.Item_amount;
                l.Total_cost = obj2.Total_cost;
            }
        }

        public void Remove(Order_Item obj)
        {
            var l =
                Context.Order_ItemSet
                    .FirstOrDefault(x => x.Order_Id == obj.Order.Order_Id && x.Item_Id == obj.Item.Item_Id);

            Context.Order_ItemSet.Remove(l);
        }

        public IEnumerable<Order_Item> GetAll()
        {
            return Context.Order_ItemSet.Select(item => this.GetRecord(item.Order_Id)).ToList();
        }

        public Order_Item GetRecord(int id)
        {
            var record = Context.Order_ItemSet.FirstOrDefault(x => x.Order_Id == id);
            Order_Item orderItem = ToObject(record);

            orderItem.Order = OrderRepository.ToObject(record.Order);
            orderItem.Item = ItemRepository.ToObject(record.Item);

            return orderItem;
        }

        
    }
}
   