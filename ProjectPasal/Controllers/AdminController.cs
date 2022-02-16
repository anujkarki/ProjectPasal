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

    //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        ProjectPasalDBEntities db = new ProjectPasalDBEntities();
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddOrEditUser(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.Action = "Add User";
                return View(new UserViewModel());
            }
            else
            {
                ViewBag.Action = "Edit User";
                tblUser user = db.tblUsers.Where(u => u.Id == id).FirstOrDefault();
                UserViewModel uvm = new UserViewModel();
                uvm.Id = user.Id;
                uvm.Name = user.Name;
                uvm.Email = user.Email;
                uvm.Phone = user.Phone;
                return View(uvm);
            }
        }
        [HttpPost]
        public ActionResult AddOrEditUser(UserViewModel uvm)
        {
            if (uvm.Id == 0)
            {
                tblUser user = new tblUser();
                UserRole ur = new UserRole();
                user.Name = uvm.Name;
                user.Email = uvm.Email;
                user.Password = uvm.Phone;
                user.Phone = uvm.Phone;
                db.tblUsers.Add(user);
                ur.RoleId = 2;
                ur.UserId = user.Id;
                db.UserRoles.Add(ur);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                tblUser user = db.tblUsers.Where(u => u.Id == uvm.Id).FirstOrDefault();
                user.Name = uvm.Name;
                user.Email = uvm.Email;
                user.Password = uvm.Phone;
                user.Phone = uvm.Phone;
                db.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            tblUser sm = db.tblUsers.Where(x => x.Id == id).FirstOrDefault();
            db.tblUsers.Remove(sm);
            db.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ManageUser()
        {
            return View();
        }
        public JsonResult GetData()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var users = db.tblUsers.ToList();
            List<UserViewModel> list = new List<UserViewModel>();
            foreach (var user in users)
            {
                var ur = db.UserRoles.Where(u => u.UserId == user.Id);
                List<string> rolesList = new List<string>();
                foreach (var item in ur)
                {
                    tblRole r = db.tblRoles.Where(rl => rl.Id == item.RoleId).FirstOrDefault();
                    rolesList.Add(r.Name);
                }

                list.Add(new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Roles = rolesList
                });
            }
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ManageUserRole(int id)
        {
            tblUser user = db.tblUsers.Where(u => u.Id == id).FirstOrDefault();
            var userRoles = db.UserRoles.Where(u => u.UserId == user.Id).ToList();
            List<RoleViewModel> roles = new List<RoleViewModel>();
            UserRoleViewModel userRoleViewModel = new UserRoleViewModel();
            userRoleViewModel.UserId = id;
            userRoleViewModel.UserName = user.Name;
            foreach (var role in userRoles)
            {
                tblRole role1 = db.tblRoles.Where(r => r.Id == role.Id).FirstOrDefault();
                roles.Add(new RoleViewModel()
                {
                    Id = role1.Id,
                    Name = role1.Name
                });

            }
            return View(userRoleViewModel);
        }
    }
}