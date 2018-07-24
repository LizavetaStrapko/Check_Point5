using BL;
using Project5.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.Controllers.TableControllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private CustomerBL _customerBL = new CustomerBL();

        public ActionResult Index()
        {
            ViewBag.Title = "Customers";
            List<Customer> customers = new List<Customer>();

            foreach (var item in _customerBL.GetAll())
            {
                customers.Add(new Customer()
                {
                    Customer_Id = item.Customer_Id,
                    Customer_First_name = item.Customer_First_name,
                    Customer_Last_Name = item.Customer_Last_Name,
                    Phone_number = item.Phone_number
                });
            }
            return View(customers);
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            List<Customer> customers = new List<Customer>();

            foreach (var item in _customerBL.GetAll().Where((s) =>
            {
                if (s.Customer_First_name != null)
                {
                    return s.Customer_First_name.Contains(searchString);
                }
                else return false;
            }))
            {
                customers.Add(new Customer()
                {
                    Customer_Id = item.Customer_Id,
                    Customer_First_name = item.Customer_First_name,
                    Customer_Last_Name = item.Customer_Last_Name,
                    Phone_number = item.Phone_number
                });
            }
            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerBL.Add(new global::ClassModel.Customer()
                {
                    Customer_First_name = customer.Customer_First_name,
                    Customer_Last_Name = customer.Customer_Last_Name,
                    Phone_number = customer.Phone_number
                });
                // _customerBL.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit()
        {
            int customerId = int.Parse(RouteData.Values["id"].ToString());
            var record = _customerBL.GetRecord(customerId);
            Customer customer = new Customer()
            {
                Customer_First_name = record.Customer_First_name,
                Customer_Last_Name = record.Customer_Last_Name,
                Phone_number = record.Phone_number
            };
            return View(customer);
        }

        [HttpPost]
        //public ActionResult Edit(Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int customerId = int.Parse(RouteData.Values["id"].ToString());
        //        _customerBL.Update(new ClassModel.Customer() { Customer_Id = customerId },
        //            new ClassModel.Customer()
        //            {
        //                Customer_First_name = customer.Customer_First_name,
        //                Customer_Last_Name = customer.Customer_Last_Name,
        //                Phone_number = customer.Phone_number
        //            });
        //        _customerBL.Save();
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public ActionResult CustomerSummary()
        {
            List<CustomerOrderView> customerSummary =
                new List<CustomerOrderView>();

            foreach (var item in _customerBL.GetAllSummary())
            {
                customerSummary.Add(new CustomerOrderView()
                {
                    Customer_First_name = item.Customer_First_name,
                    Customer_Last_Name = item.Customer_Last_Name,
                    Phone_Number = item.Phone_Number,
                    OrderCount = item.OrderCount,
                    TotalSum = item.TotalSum
                });
            }
            return View(customerSummary);
        }

        [HttpPost]
        public ActionResult CustomerSummary(string searchString)
        {
            List<CustomerOrderView> customerSummary =
                new List<CustomerOrderView>();

            foreach (var item in _customerBL.GetAllSummary().Where((s) =>
            {
                if (s.Customer_First_name != null)
                {
                    return s.Customer_First_name.Contains(searchString);
                }
                else return false;
            }))
            {
                customerSummary.Add(new CustomerOrderView()
                {
                    Customer_First_name = item.Customer_First_name,
                    Customer_Last_Name = item.Customer_Last_Name,
                    Phone_Number = item.Phone_Number,
                    OrderCount = item.OrderCount,
                    TotalSum = item.TotalSum
                });
            }
            return View(customerSummary);
        }
    }
}