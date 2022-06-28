#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ApplicationRoleManager class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 4/12/2018 15:57
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace KTB.DNet.Interface.Dapper.Repository
{
    public class ApplicationRoleManager : RoleManager<APIRole, int>
    {
        public ApplicationRoleManager(IRoleStore<APIRole, int> roleStore)
            : base(roleStore)
        {

        }

        public async Task<List<APIRole>> GetAll()
        {
            return await Task.FromResult<List<APIRole>>(new List<APIRole>());
        }
    }
}
