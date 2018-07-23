using ClassModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class ItemRepository : AbstractRepository, IRepository<Item>, IEnumerable<Item>
    {
        internal static EntityModel.Item ToEntity(Item item)
        {
            return new EntityModel.Item()
            {
                Item_Id = item.Item_Id,
                Title = item.Title,
                Cost = item.Cost,
                Amount = item.Amount
            };
        }

        internal static Item ToObject(EntityModel.Item item)
        {
            return item == null ? null : new Item(item.Item_Id, item.Title, item.Cost, item.Amount);
        }

        private static void Check(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }

        private static void Check(EntityModel.Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }

        public void Add(Item item)
        {
            if (item == null)
                throw new ArgumentException("Item can not be null");

            Context.ItemSet.Add(ToEntity(item));
            Context.SaveChanges();
        }

        public void Update(int id, Item item)
        {
            if (item == null)
                throw new ArgumentException("Item can not be null");
            //Check(item);
            var element = ItemById(item.Item_Id);
            if (element == null)
                throw new ArgumentException("Item with this ID is not found");
            //Check(element);

            element.Title = item.Title;
            element.Cost = item.Cost;
            element.Amount = item.Amount;
            Context.SaveChanges();
        }

        public void Remove(Item item)
        {
            if (item == null)
                throw new ArgumentException("Item can not be null");
            //Check(item);
            var element = ItemById(item.Item_Id);
            if (element == null)
                throw new ArgumentException("Item with this ID is not found");
            // Check(element);

            Context.ItemSet.Remove(element);
            Context.SaveChanges();
        }

        private EntityModel.Item ItemById(int id)
        {
            return Context.ItemSet.FirstOrDefault(x => x.Item_Id == id);
        }

        public Item ItemModelById(int id)
        {
            var item = ItemById(id);
            return ToObject(item);
        }

        internal EntityModel.Item ItemByName(string Title)
        {
            return Context.ItemSet.FirstOrDefault(x => x.Title == Title);
        }

        internal EntityModel.Item AddIfNotAndGetItem(string title)
        {
            var item = ItemByName(title);
            if (item == null)
            {
                item = Context.ItemSet.Add(ToEntity(new Item(title)));
                Context.SaveChanges();
            }
            return item;
        }

        public IEnumerable<Item> Items
        {
            get { return Context.ItemSet.AsEnumerable().Select(item => ToObject(item)); }
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
        public IEnumerable<Item> GetAll()
        {
            var items = Context.ItemSet.ToList();

            return items.Select(item => this.GetRecord(item.Item_Id)).ToList();
        }

        public Item GetRecord(int id)
        {
            return ToObject(Context.ItemSet.FirstOrDefault(x => x.Item_Id == id));
        }

        public IEnumerable<Item> Get(int numberOfRecords, int numSkip)
        {
            var list =
                Context.ItemSet.OrderBy(x => x.Item_Id).Skip(numSkip).Take(numberOfRecords).Select(x => x).ToList();
            return list.Select(x => ToObject(x));
        }
       

    }
}