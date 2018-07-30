using BL;
using Project5.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.Controllers.TableControllers
{
    [Authorize(Roles = "admin")]
    public class ManagerController : Controller
    {
        private ManagerBL _managerBL = new ManagerBL();

        public ActionResult Index()
        {
            List<Manager> managers = new List<Manager>();

            foreach (var item in _managerBL.GetAll())
            {
                managers.Add(new Manager()
                {
                    Manager_Id = item.Manager_Id,
                    Manager_First_name = item.Manager_First_name,
                    Manager_Last_name = item.Manager_Last_name
                });
            }
            return View(managers);
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            List<Manager> managers = new List<Manager>();

            foreach (var item in _managerBL.GetAll().Where((s) =>
            {
                if (s.Manager_First_name != null)
                {
                    return s.Manager_First_name.Contains(searchString);
                }
                else return false;
            }))
            {
                managers.Add(new Manager()
                {
                    Manager_Id = item.Manager_Id,
                    Manager_First_name = item.Manager_First_name,
                    Manager_Last_name = item.Manager_Last_name
                });
            }
            return View(managers);
        }

        public ActionResult Edit()
        {
            int managerId = int.Parse(RouteData.Values["id"].ToString());
            var record = _managerBL.GetRecord(managerId);
            Manager manager = new Manager()
            {
                Manager_Id = record.Manager_Id,
                Manager_First_name = record.Manager_First_name,
                Manager_Last_name = record.Manager_Last_name
            };
            return View(manager);
        }

        //[HttpPost]
        //public ActionResult Edit(Manager manager)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int manager_Id = int.Parse(RouteData.Values["id"].ToString());
        //        _managerBL.Update(new ClassModel.Manager() { Manager_Id = manager_Id },
        //            new ClassModel.Manager()
        //            {
        //                Manager_First_name = manager.Manager_First_name,
        //                Manager_Last_name = manager.Manager_Last_name
        //            });
        //        _managerBL.Save();
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Manager manager)
        {
            if (ModelState.IsValid)
            {
                _managerBL.Add(new ClassModel.Manager()
                {
                    Manager_First_name = manager.Manager_First_name,
                    Manager_Last_name = manager.Manager_Last_name
                });
               // _managerBL.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}