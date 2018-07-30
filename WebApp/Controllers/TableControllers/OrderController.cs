using BL;
using Project5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers.TableControllers
{
   [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private OrderBL _orderBL = new OrderBL();

        public ActionResult Index()
        {
            List<OrderTotalCostView> orders = new List<OrderTotalCostView>();

            foreach (var item in _orderBL.GetAll())
            {
                orders.Add(new OrderTotalCostView()
                {
                    Order_Id = item.Order_Id,
                    Date = item.Date,
                    Customer_Id = item.Customer_Id,
                    Manager_Id = item.Manager_Id,
                    NumberOfItems = item.NumberOfItems,
                    Total_cost = item.Total_cost
                });
            }
            return View(orders);
        }

        [HttpPost]
        public ActionResult Index(string searchString, string searchString2)
        {
            List<OrderTotalCostView> orders = new List<OrderTotalCostView>();
            if (!string.IsNullOrEmpty(searchString))
            {
                foreach (var item in _orderBL.GetAll().Where((s) =>
                {
                    if (s.Date != null)
                    {
                        return s.Date.ToString().Contains(searchString);
                    }
                    else return false;
                }))
                {
                    orders.Add(new OrderTotalCostView()
                    {
                        Order_Id = item.Order_Id,
                        Date = item.Date,
                        Customer_Id = item.Customer_Id,
                        Manager_Id = item.Manager_Id,
                        NumberOfItems = item.NumberOfItems,
                        Total_cost = item.Total_cost
                    });
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(searchString2))
                {
                    foreach (var item in _orderBL.GetAll().Where((s) =>
                    {
                        if (s.Manager_Id != null)
                        {
                            return s.Manager_Id.ToString().Contains(searchString2);
                        }
                        else return false;
                    }))
                    {
                        orders.Add(new OrderTotalCostView()
                        {
                            Order_Id = item.Order_Id,
                            Date = item.Date,
                            Customer_Id = item.Customer_Id,
                            Manager_Id = item.Manager_Id,
                            NumberOfItems = item.NumberOfItems,
                            Total_cost = item.Total_cost
                        });
                    }
                }
            }
            return View(orders);
        }

        public ActionResult Edit()
        {
            int orderId = int.Parse(RouteData.Values["id"].ToString());
            var record = _orderBL.GetRecord(orderId);
            OrderTotalCostView order = new OrderTotalCostView()
            {
                Date = record.Date,
                Customer_Id = record.Customer_Id,
                Manager_Id = record.Manager_Id
            };
            return View(order);
        }

        //[HttpPost]
        //public ActionResult Edit(OrderTotalCostView order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int orderId = int.Parse(RouteData.Values["id"].ToString());
        //        _orderBL.Update(new ClassModel.Order() { OrderId = orderId },
        //            new ClassModel.Order()
        //            {
        //                Date = order.Date,
        //                Customer_Id = order.Customer_Id,
        //                Manager_Id = order.Manager_Id
        //            });
        //        _orderBL.Save();
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public ActionResult Graphs()
        {
            return View();
        }

        public ActionResult GetDateAndCost()
        {
            List<double?[]> p = new List<double?[]>();

            foreach (var item in _orderBL.GetAll().GroupBy(x => x.Date.Value.Month))
            {
                double? totalSum = item.Sum(x => x.Total_cost);
                p.Add(new[] { item.Key, item.First().Date.Value.Year, totalSum });
            }

            return Json(new { result = p }, JsonRequestBehavior.AllowGet);
        }
    }
}