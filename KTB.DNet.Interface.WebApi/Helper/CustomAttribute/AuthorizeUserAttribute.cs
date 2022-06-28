using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Domain.Models;
using KTB.DNet.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KTB.DNet.Interface.WebApi.Helper.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizeUserAttribute:AuthorizeAttribute
    {

        public string PermissionName { get; set; }               

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = false;

            var isAuthorized = base.AuthorizeCore(httpContext);
            var userRepo = new ApplicationUserRepository(new ApplicationDbContext());
            var roleRepo = new RoleRepository(new ApplicationDbContext());
            var rolePermissionRepo = new RolePermissionRepository(new ApplicationDbContext());
            var permissions = new List<Permission>();

            if (!isAuthorized)
            {
                return false;
            }

            var currentUser = userRepo.FindUserByName(httpContext.User.Identity.Name);
            if (currentUser == null)
            {
                return false;
            }                      

            foreach (var role in currentUser.Roles)
            {
                permissions.AddRange(rolePermissionRepo.GetByRoleId(role.RoleId).Select(x=> x.Permission).ToList());             
            }

            authorized = permissions.FirstOrDefault(x => x.Name == PermissionName) != null ? true : false;

            return authorized;
        }

       
    }
}