using KTB.DNet.Interface.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KTB.DNet.Interface.Persistence;

namespace KTB.DNet.Interface.Repository
{
    public class ApplicationRoleManager : RoleManager<APIRole, int>
    {
        public ApplicationRoleManager(IRoleStore<APIRole, int> roleStore)
            : base(roleStore)
        {

        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<APIRole, int, APIUserRole>(context.Get<DNETInterfaceDBContext>()));
        }

        public async Task<List<APIRole>> GetAll()
        {
            var result = await Roles.ToListAsync();
            return result;
        }

    }
}
