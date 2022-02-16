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

    public class CustomerController : Controller
    {
        ProjectPasalDBEntities db = new ProjectPasalDBEntities();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var customers = db.tblCustomers.ToList();
            List<CustomerViewModel> list = new List<CustomerViewModel>();
            foreach (var customer in customers)
            {
                list.Add(new CustomerViewModel()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    Balance = customer.Balance
                });
            }
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.Action = "Add Customer";
                return View(new CustomerViewModel());
            }
            else
            {
                ViewBag.Action = "Edit Customer";
                tblCustomer customer = db.tblCustomers.Where(p => p.Id == id).FirstOrDefault();
                CustomerViewModel cvm = new CustomerViewModel();
                cvm.Id = customer.Id;
                cvm.Name = customer.Name;
                cvm.Address = customer.Address;
                cvm.Email = customer.Email;
                cvm.Phone = customer.Phone;
                cvm.Balance = customer.Balance;
                return View(cvm);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(CustomerViewModel customer)
        {
            if (customer.Id == 0)
            {
                tblCustomer cvm = new tblCustomer();
                cvm.Name = customer.Name;
                cvm.Address = customer.Address;
                cvm.Email = customer.Email;
                cvm.Phone = customer.Phone;
                cvm.Balance = customer.Balance;
                db.tblCustomers.Add(cvm);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                tblCustomer cvm = db.tblCustomers.Where(p => p.Id == customer.Id).FirstOrDefault();
                cvm.Name = customer.Name;
                cvm.Address = customer.Address;
                cvm.Email = customer.Email;
                cvm.Phone = customer.Phone;
                cvm.Balance = customer.Balance;
                db.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            tblCustomer sm = db.tblCustomers.Where(x => x.Id == id).FirstOrDefault();
            db.tblCustomers.Remove(sm);
            db.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

        }

        //Customer Transaction
        public ActionResult Transaction()
        {
            return View();
        }
        public JsonResult TransactionData()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var custTrans = db.CustomerTransactions.ToList();
            List<CustomerTransactionViewModel> list = new List<CustomerTransactionViewModel>();
            foreach (var Transaction in custTrans)
            {
                var customer = db.tblCustomers.Where(c => c.Id == Transaction.CustomerId).FirstOrDefault();
                list.Add(new CustomerTransactionViewModel()
                {
                    Id = Transaction.Id,
                    CustomerName=customer.Name,
                    Amount=Transaction.Amount,
                    Balance=Transaction.Balance,
                    Remark=Transaction.Remark,
                    Description=Transaction.Description
                }) ;
            }
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CreateTransaction()
        {
            List<string> remark = new List<string>();
            remark.Add("Amount Received");
            remark.Add("Amount Given/Product Purchased");
            ViewBag.Customer = db.tblCustomers.ToList();
            ViewBag.Remark = remark;
            return View();
        }
        [HttpPost]
        public ActionResult CreateTransaction(CustomerTransaction ct)
        {
            tblCustomer customer = db.tblCustomers.Where(c => c.Id == ct.CustomerId).FirstOrDefault();
            if(ct.Remark== "Amount Received")
            {
                ct.Balance = customer.Balance - ct.Amount;
            }
            else
            {
                ct.Balance = customer.Balance + ct.Amount;
            }
            customer.Balance = ct.Balance;
            db.CustomerTransactions.Add(ct);
            db.SaveChanges();
            return Json(new { success = true, message = "Added Successfully" }, JsonRequestBehavior.AllowGet);

        }
    }
}