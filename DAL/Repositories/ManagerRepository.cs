using ClassModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class ManagerRepository : AbstractRepository, IRepository<Manager>, IEnumerable<Manager>
    {
        internal static EntityModel.Manager ToEntity(Manager manager)
        {
            return new EntityModel.Manager()
            {
                Manager_Id = manager.Manager_Id,
                Manager_First_name = manager.Manager_First_name,
                Manager_Last_name=manager.Manager_Last_name
            };
        }

        internal static Manager ToObject(EntityModel.Manager manager)
        {
            return manager == null ? null : new Manager(manager.Manager_Id, manager.Manager_First_name, 
                                                        manager.Manager_Last_name);
        }
        
        public void Add(Manager item)
        {
            if (item == null)
                throw new ArgumentException("Manager can not be null");

            Context.ManagerSet.Add(ToEntity(item));
            Context.SaveChanges();
        }

        public void Update(int id, Manager item)
        {
            if (item == null)
                throw new ArgumentException("Manager can not be null");
            var element = ManagerById(item.Manager_Id);
            if (element == null)
                throw new ArgumentException("Manager with this ID is not found");

            element.Manager_First_name = item.Manager_First_name;
            element.Manager_Last_name = item.Manager_Last_name;
            Context.SaveChanges();
        }

        public void Remove(Manager item)
        {
            if (item == null)
                throw new ArgumentException("Manager can not be null");
            var element = ManagerById(item.Manager_Id);
            if (element == null)
                throw new ArgumentException("Manager with this ID is not found");

            Context.ManagerSet.Remove(element);
            Context.SaveChanges();
        }

        internal EntityModel.Manager ManagerById(int id)
        {
            return Context.ManagerSet.FirstOrDefault(x => x.Manager_Id == id);
        }
        public Manager ManagerObjectById(int id)
        {
            var manager = Context.ManagerSet.FirstOrDefault(x => x.Manager_Id == id);
            return ToObject(manager);
        }

        internal EntityModel.Manager ManagerByName(string firstName, string lastName)
        {
            return Context.ManagerSet.FirstOrDefault(x => x.Manager_First_name == firstName && x.Manager_Last_name==lastName);
        }

        internal EntityModel.Manager AddIfNotAndGetManager(string firstName, string lastName)
        {
            var manager = ManagerByName(firstName, lastName);
            if (manager == null)
            {
                manager = Context.ManagerSet.Add(ToEntity(new Manager(firstName, lastName)));
                Context.SaveChanges();
            }
            return manager;
        }

        public IEnumerable<Manager> Items
        {
            get { return Context.ManagerSet.AsEnumerable().Select(item => ToObject(item)); }
        }

        public IEnumerator<Manager> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
public  IEnumerable<Manager> GetAll()
{
    var managers = Context.ManagerSet.Include("Orders");
    List<Manager> listManagers = new List<Manager>();

    foreach (var item in managers)
    {
        listManagers.Add(GetRecord(item.Manager_Id));
    }
    return listManagers;
}
public Manager GetRecord(int id)
{
    var record = Context.ManagerSet.Include("Orders").FirstOrDefault(x => x.Manager_Id == id);
   Manager manager = ToObject(record);

    List<Order> orders = record.Order.Select(item => OrderRepository.ToObject(item)).ToList();
    manager.Orders = orders;

    return manager;
}
    }
}