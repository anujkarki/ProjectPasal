using ProjectPasal.Models;
using ProjectPasal.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPasal.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        ProjectPasalDBEntities db = new ProjectPasalDBEntities();

        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var sales = db.tblSales.ToList();
            var customers = db.tblCustomers.ToList();
            List<SalesViewModel> list = new List<SalesViewModel>();
            foreach (var sale in sales)
            {
                tblCustomer cust = db.tblCustomers.Where(c => c.Id == sale.Id).FirstOrDefault();
                tblUser user = db.tblUsers.Where(u => u.Id == sale.UserId).FirstOrDefault();
                list.Add(new SalesViewModel()
                {
                    Id = sale.Id,
                    CustomerName = cust.Name,
                    Amount=sale.Amount,
                    Remark=sale.Remark,
                    UserName=user.Name
                }) ;
            }
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            ViewBag.Customers = db.tblCustomers.ToList();
            ViewBag.Products = db.tblProducts.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateSalesViewModel sale)
        {
            tblSale s = new tblSale();
            SalesItem si = new SalesItem();
            tblProduct product = db.tblProducts.Where(p => p.Id == sale.ProductId).FirstOrDefault();
            tblCustomer customer = db.tblCustomers.Where(c => c.Id == sale.CustomerId).FirstOrDefault();
            s.CustomerId = customer.Id;
            s.Amount = product.Price * sale.Quantity;
            customer.Balance = customer.Balance + s.Amount;
            s.UserId = Convert.ToInt16(Session["UserId"]);
            s.Remark = sale.Remark;
            db.tblSales.Add(s);
            si.ProductId = product.Id;
            si.Quantity = sale.Quantity;
            si.SalesId = s.Id;
            db.SalesItems.Add(si);
            db.SaveChanges();
            return Json(new { success = true, message = "Added Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}