using ClassModel;
using DAL.Repositories;
using System.Collections.Generic;

namespace BL
{
    public class ManagerBL
    {
        private ManagerRepository _managerRepository = new ManagerRepository();


        public IEnumerable<Manager> GetAll()
        {
            return _managerRepository.GetAll();
        }

        public void Add(Manager manager)
        {
            _managerRepository.Add(manager);
        }

        public void Delete(Manager manager)
        {
            _managerRepository.Remove(manager);
        }

        public void Update(int id, Manager item)
        {
            _managerRepository.Update(id, item);
        }

        public Manager GetRecord(int id)
        {
            return _managerRepository.GetRecord(id);
        }

//        public void Save()
//        {
//            _managerRepository.Save();
//        }
//    }
//}
    }
}
