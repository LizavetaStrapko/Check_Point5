using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{

    public enum TableEnum
    {
        None,
        CustomerSet,
        ItemSet,
        ManagerSet,
        Order_ItemSet,
        OrderSet,
        Users
    }
    [Authorize(Roles = "user")]
    public class MainTableController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            var selectList = new List<SelectListItem>();
            var tt = Enum.GetNames(typeof(TableEnum));
            for (int i = 0; i < tt.Length; i++)
            {
                if (tt[i] == TableEnum.Users.ToString())
                    if (!(User.Identity.IsAuthenticated && User.IsInRole("admin")))
                        continue;

                selectList.Add(new SelectListItem()
                {
                    Text = tt[i],
                    Value = tt[i],
                    Selected = i == 0
                });
            }

            return View("Index", selectList);
        }

        public ActionResult GetTable(string tableName)
        {
            TableEnum tt = (TableEnum)Enum.Parse(typeof(TableEnum), tableName);
            switch (tt)
            {
                case TableEnum.None:
                    return new EmptyResult();
                case TableEnum.CustomerSet:
                    return RedirectToAction("Index", "Customer");
                case TableEnum.ManagerSet:
                    return RedirectToAction("Index", "Manager");
                case TableEnum.ItemSet:
                    return RedirectToAction("GetProducts", "Item");
                case TableEnum.OrderSet:
                    return RedirectToAction("GetFileInformations", "FileInformationTable");
                case TableEnum.Order_ItemSet:
                    return RedirectToAction("GetSaleInfos", "SaleInfoTable");
                case TableEnum.Users:
                    return RedirectToAction("GetUsers", "UserTable");
                default:
                    return View("Error");
            }
        }
    }
}