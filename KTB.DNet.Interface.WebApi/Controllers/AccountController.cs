#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Account Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports

#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    public class AccountController : BaseController
    {
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public IHttpActionResult Logout()
        //{
        //    bool successLogout = false;

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        try
        //        {
        //            var identity = User.Identity as ClaimsIdentity;
        //            string usernameStr = null;
        //            string dealerCodeStr = null;
        //            if (identity != null)
        //            {
        //                IEnumerable<Claim> claims = identity.Claims;
        //                var userName = claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        //                if (userName != null)
        //                { usernameStr = userName.Value; }
        //            }

        //            if (identity != null)
        //            {
        //                IEnumerable<Claim> claims = identity.Claims;
        //                var dealerCode = claims.FirstOrDefault(x => x.Type == "dealercode");
        //                if (dealerCode != null)
        //                {
        //                    dealerCodeStr = dealerCode.Value;
        //                }
        //            }

        //            if (string.IsNullOrEmpty(usernameStr) || string.IsNullOrEmpty(dealerCodeStr))
        //            {
        //                LogError(new Exception("Username or DealerCode could not be null/empty."));
        //                return this.Unauthorized();
        //            }

        //            AuthRepository _repo = new AuthRepository();

        //            var user = _repo.FindUserByName(User.Identity.Name);
        //            if (user != null && user.IsActive)
        //            {
        //                _repo.Update(user);
        //                successLogout = true;

        //            }

        //            UserActivityRepository _userActivityRepo = new UserActivityRepository();

        //            // log user logout activity
        //            _userActivityRepo.Create(
        //                new UserActivity()
        //                {
        //                    Username = usernameStr,
        //                    Activity = UserActivityType.Logout,
        //                    ActivityDesc = string.Format("{0}-{1} : {2}", usernameStr, dealerCodeStr, UserActivityType.Logout.ToString()),
        //                    ActivityTime = DateTime.Now,
        //                    Endpoint = (this as ApiController).ActionContext.Request.RequestUri.AbsoluteUri,
        //                    AppId = new Guid(AppConfigs.GetString("AppId"))
        //                }
        //            );
        //        }
        //        catch (Exception ex)
        //        {
        //            LogError(ex);
        //        }
        //    }

        //    if (successLogout)
        //    {
        //        return this.Ok(new { message = "Logout successful." });
        //    }
        //    else
        //    {
        //        return this.Unauthorized();
        //    }
        //}
    }
}
