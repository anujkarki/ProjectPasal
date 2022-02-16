using ProjectPasal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProjectPasal
{
    public class MyRoleProvider:RoleProvider
    {
        ProjectPasalDBEntities db = new ProjectPasalDBEntities();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            List<string> roleList = new List<string>();
            var userRoles = db.tblRoles.ToList();
            foreach(var item in userRoles)
            {
                roleList.Add(item.Name);
            }
            string[] roles=roleList.ToArray();
            return roles;
        }

        public override string[] GetRolesForUser(string Email)
        {
            tblUser user = db.tblUsers.Where(u => u.Email == Email).FirstOrDefault();
            var ur = db.UserRoles.Where(u => u.UserId == user.Id);
            //string[] roles = new string[] {  };
            List<string> rolesList = new List<string>();
            foreach(var item in ur)
            {
                tblRole r = db.tblRoles.Where(rl => rl.Id == item.RoleId).FirstOrDefault();
                rolesList.Add(r.Name);
            }

            string[] roles = rolesList.ToArray();
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string Email, string roleName)
        {
            tblUser user = db.tblUsers.Where(u => u.Email == Email).FirstOrDefault();
            tblRole role = db.tblRoles.Where(r => r.Name == roleName).FirstOrDefault();
            var ur = db.UserRoles.Where(u => u.UserId == user.Id && u.RoleId == role.Id).FirstOrDefault();
            if (ur != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}