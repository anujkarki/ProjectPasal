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

    public class ProductController : Controller
    {
        ProjectPasalDBEntities db = new ProjectPasalDBEntities();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var products = db.tblProducts.ToList();
            List<ProductViewModel> list = new List<ProductViewModel>();
            foreach (var product in products)
            {
                list.Add(new ProductViewModel()
                {
                    Id=product.Id,
                    Name=product.Name,
                    Category=product.Category,
                    Description=product.Description,
                    Price=product.Price
                });
            }
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.Action = "Add Product";
                return View(new ProductViewModel());
            }
            else
            {
                ViewBag.Action = "Edit Product";
                tblProduct product = db.tblProducts.Where(p => p.Id == id).FirstOrDefault();
                ProductViewModel pvm = new ProductViewModel();
                pvm.Id = product.Id;
                pvm.Name = product.Name;
                pvm.Category = product.Category;
                pvm.Description = product.Description;
                pvm.Price = product.Price;
                return View(pvm);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(ProductViewModel product)
        {
            if (product.Id == 0)
            {
                tblProduct pvm = new tblProduct();

                pvm.Name = product.Name;
                pvm.Category = product.Category;
                pvm.Description = product.Description;
                pvm.Price = product.Price;
                db.tblProducts.Add(pvm);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                tblProduct pvm = db.tblProducts.Where(p => p.Id == product.Id).FirstOrDefault();
                pvm.Name = product.Name;
                pvm.Category = product.Category;
                pvm.Description = product.Description;
                pvm.Price = product.Price;
                db.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            tblProduct sm = db.tblProducts.Where(x => x.Id == id).FirstOrDefault();
            db.tblProducts.Remove(sm);
            db.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

        }

    }

}