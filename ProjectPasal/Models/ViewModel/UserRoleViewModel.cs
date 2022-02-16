using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPasal.Models.ViewModel
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleViewModel> Role { get; set; }
    }
}