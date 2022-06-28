#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ApplicationUserManager class
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Persistence.Stores;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
#endregion

namespace KTB.DNet.Interface.Dapper.Repository
{
    public class ApplicationUserManager : UserManager<APIUser, int>
    {
        public ApplicationUserManager(IUserStore<APIUser, int> store)
            : base(store)
        {
            this.UserValidator = new UserValidator<APIUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
        }

        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
        //    IOwinContext context)
        //{
        //    var manager = new ApplicationUserManager(new APIUserStore(context.Get<DNETInterfaceDBContext>()));
        //    // Configure validation logic for usernames
        //    manager.UserValidator = new UserValidator<APIUser, int>(manager)
        //    {
        //        AllowOnlyAlphanumericUserNames = false,
        //        RequireUniqueEmail = true
        //    };
        //    // Configure validation logic for passwords
        //    manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = true,
        //        RequireDigit = true,
        //        RequireLowercase = true,
        //        RequireUppercase = true,
        //    };

        //    manager.UserLockoutEnabledByDefault = true;
        //    manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //    manager.MaxFailedAccessAttemptsBeforeLockout = 5;

        //    IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
        //    if (dataProtectionProvider != null)
        //    {
        //        manager.UserTokenProvider =
        //            new DataProtectorTokenProvider<APIUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
        //    }
        //    return manager;
        //}

        /// <summary>
        /// Find by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public override Task<APIUser> FindByNameAsync(string userName)
        {
            //return Users.Include(u => u.Roles).Include(u => u.Claims).Include(u => u.Logins).Include(u => u.Clients).FirstOrDefaultAsync(u => u.UserName == userName);

            return Task.FromResult<APIUser> (new APIUser());
        }

        /// <summary>
        /// Find user by name and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override Task<APIUser> FindAsync(string userName, string password)
        {
            return base.FindAsync(userName, password);
        }

        /// <summary>
        /// Find by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public APIUser FindByName(string userName)
        {
            // change to list first before return the object, to handle open data reader problem
            //var users = Users
            //    .Include(u => u.Roles)
            //    .Include(u => u.Claims)
            //    .Include(u => u.Logins)
            //    .Include(u => u.Clients)
            //    .ToList();

            //return users.FirstOrDefault(u => u.UserName == userName);

            return new APIUser();
        }

        /// <summary>
        /// Find by dealer ID
        /// </summary>
        /// <param name="dealerID"></param>
        /// <returns></returns>
        public APIUser FindByDealerID(int dealerID)
        {
            //var users = Users
            //    .Include(u => u.Roles)
            //    .Include(u => u.Claims)
            //    .Include(u => u.Logins)
            //    .Include(u => u.Clients)
            //    .ToList();

            //return users.FirstOrDefault(u => u.DealerId == dealerID);

            return new APIUser();
        }

        public APIUser FindUserById(int userId)
        {
            //return Users
            //    .Include(u => u.Roles)
            //    .Include(u => u.Claims)
            //    .Include(u => u.Logins)
            //    .Include(u => u.Clients)
            //    //.Include(u => u.Clients.GroupBy(c => c.ClientId)
            //    //.Select(group => group.First()))
            //    .Where(user => user.Id == userId)
            //    .OrderByWithDirection(u => u.Id, false) // have to give a default order when skipping .. so use the PK
            //    .FirstOrDefault();

            return new APIUser();
        }

        public List<APIUser> Filter(int? dealerId, string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {

            //bool isDealerIdNull = !(dealerId != null && dealerId.HasValue);
            //var result = Users.Include(u => u.Roles).Include(u => u.Claims).Include(u => u.Logins).Include(u => u.Clients).AsEnumerable()
            //               .Where(
            //                        u =>
            //                        {
            //                            bool keywordExists = !string.IsNullOrEmpty(keyword);
            //                            bool isUserDealerIdNull = !(u.DealerId != null && u.DealerId.HasValue);
            //                            bool isDealerMatch = isDealerIdNull ? false : (isUserDealerIdNull ? false : ((int)u.DealerId) == dealerId.Value);

            //                            return (isDealerIdNull || isDealerMatch) && (!keywordExists ||
            //                                (keywordExists && (u.UserName != null ? u.UserName.ToUpper().Contains(keyword) : false)) ||
            //                                (keywordExists && (u.Email != null ? u.Email.ToUpper().Contains(keyword) : false)));
            //                        }
            //                        )
            //               .OrderByWithDirection(u => orderedProperty.GetValue(u, null), orderDir) // have to give a default order when skipping .. so use the PK
            //               .Skip(skip)
            //               .Take(take)
            //               .ToList();

            //// now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            //filteredResultsCount = Users.Include(u => u.Roles).Include(u => u.Claims).Include(u => u.Logins).Include(u => u.Clients).AsEnumerable()
            //               .Where(
            //                        u =>
            //                        {
            //                            bool keywordExists = !string.IsNullOrEmpty(keyword);
            //                            bool isUserDealerIdNull = !(u.DealerId != null && u.DealerId.HasValue);
            //                            bool isDealerMatch = isDealerIdNull ? false : (isUserDealerIdNull ? false : ((int)u.DealerId) == dealerId.Value);

            //                            return (isDealerIdNull || isDealerMatch) && (!keywordExists ||
            //                                (keywordExists && (u.UserName != null ? u.UserName.ToUpper().Contains(keyword) : false)) ||
            //                                (keywordExists && (u.Email != null ? u.Email.ToUpper().Contains(keyword) : false)));
            //                        }
            //                        ).Count();
            //totalResultsCount = Users.Include(u => u.Roles).Include(u => u.Claims).Include(u => u.Logins).Include(u => u.Clients).Count();

            filteredResultsCount = 0;

            totalResultsCount = 0;

            return new List<APIUser>();

        }

        /// <summary>
        /// Create identity
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public override Task<ClaimsIdentity> CreateIdentityAsync(APIUser user, string authenticationType)
        {
            return base.CreateIdentityAsync(user, authenticationType);
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> UpdateAsync(APIUser user)
        {
            var result = await base.UpdateAsync(user);

            return result;
        }
    }
}
