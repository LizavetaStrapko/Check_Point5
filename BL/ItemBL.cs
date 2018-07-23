using ClassModel;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class ItemBL
    {
        private ItemRepository _itemRepository = new ItemRepository();

        public IEnumerable<Item> GetAll()
        {
            return _itemRepository.GetAll().AsQueryable();
        }

        public void Add(Item item)
        {
            _itemRepository.Add(item);
        }

        public void Delete(Item item)
        {
            _itemRepository.Remove(item);
        }

        public void Update(int id, Item item)
        {
            _itemRepository.Update(id, item);
        }

        public Item GetRecord(int id)
        {
            return _itemRepository.GetRecord(id);
        }

        //public void Save()
        //{
        //    _itemRepository.Save();
        //}

        public IEnumerable<Item> Get(int numRec, int numSkip)
        {
            return _itemRepository.Get(numRec, numSkip);
        }
    }
}
