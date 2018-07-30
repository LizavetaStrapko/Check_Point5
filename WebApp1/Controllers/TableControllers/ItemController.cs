using BL;
using Project5.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.Controllers.TableControllers
{
    public class ItemController : Controller
    {
            private readonly ItemBL _itemBL = new ItemBL();

            public ActionResult Index()
            {
                ViewBag.Title = "Products";
                List<Item> products = new List<Item>();

                foreach (var item in _itemBL.GetAll())
                {
                    products.Add(new Item()
                    {
                        Item_Id = item.Item_Id,
                        Title = item.Title,
                        Cost = item.Cost,
                        Amount = item.Amount
                    });
                }
                return View(products);
            }

            [HttpPost]
            public ActionResult Index(string searchString)
            {
                List<Item> products = new List<Item>();

                foreach (var item in _itemBL.GetAll().Where((s) =>
                {
                    if (s.Title != null)
                    {
                        return s.Title.Contains(searchString);
                    }
                    else return false;
                }))

                {
                    products.Add(new Item()
                    {
                        Item_Id = item.Item_Id,
                        Title = item.Title,
                        Cost = item.Cost,
                        Amount = item.Amount
                    });
                }
                return View(products);
            }

            [Authorize(Roles = "Admin")]
            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [Authorize(Roles = "Admin")]
            public ActionResult Create(Item item)
            {
                if (ModelState.IsValid)
                {
                    _itemBL.Add(new ClassModel.Item()
                    {
                        Title = item.Title,
                        Cost = item.Cost,
                        Amount = item.Amount
                    });
                   // _itemBL.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }

            [Authorize(Roles = "Admin")]
            public ActionResult Edit()
            {
                int itemId = int.Parse(RouteData.Values["id"].ToString());
                var record = _itemBL.GetRecord(itemId);
                Item item = new Item()
                {
                    Title = record.Title,
                    Cost = record.Cost,
                    Amount = record.Amount
                };
                return View(item);
            }

           // [HttpPost]
            //[Authorize(Roles = "Admin")]
            //public ActionResult Edit(Item item)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        int itemId = int.Parse(RouteData.Values["id"].ToString());
            //        _itemBL.Update(new ClassModel.Item() { Item_Id = itemId },
            //            new ClassModel.Item()
            //            {
            //                Title = item.Title,
            //                Cost = item.Cost,
            //                Amount = item.Amount
            //            });
            //        //_itemBL.Save();
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return View();
            //    }
            }
        }
    
