using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Framework.Helper;
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebAuth.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace KTB.DNet.Interface.WebAuth.Controllers
{
    /// <summary>
    /// Author          : Muhamad Ridwan
    /// Created On      : July, 2018
    /// 
    /// PT Mitrais
    /// 
    /// </summary>
    public class AuthController : ApiController
    {
        private JWTManager jwtManager;
        private IUserRepository<APIUser, int> _userRepo;
        private IClientUserRepository<APIClientUser, int> _clientUserRepo;
        private IUserActivityRepository<UserActivity, long> _userActivityRepo;
        private IApplicationConfigRepository<ApplicationConfig, long> _appConfigRepo;

        public AuthController()
        {
            jwtManager = new JWTManager();

            _userRepo = new UserRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            _clientUserRepo = new ClientUserRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            _userActivityRepo = new UserActivityRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.LogConnection));
            _appConfigRepo = new ApplicationConfigRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.LogConnection));
        }

        [HttpPost]
        [Route("loginswagger")]
        public IHttpActionResult LoginSwagger(SwaggerUser swaggerUser)
        {
            APIUser dbUser = _userRepo.GetAuthenticatedUser(swaggerUser.Username, swaggerUser.Password, swaggerUser.DealerCode);

            if (dbUser == null)
            {
                throw new HttpResponseException(UnauthorizedResponse(MessageResource.ErrorMsgAuthInvalidUsernameOrPassword));
            }

            APIClientUser userToClient = _clientUserRepo.GetByUserIdAndAppName(dbUser.Id, "swagger").FirstOrDefault();

            if (userToClient == null)
            {
                throw new HttpResponseException(UnauthorizedResponse(MessageResource.ErrorMsgAuthUserNotRegisteredOnSpecifiedClient));
            }

            LoginUser user = new LoginUser
            {
                Username = swaggerUser.Username,
                Password = swaggerUser.Password,
                DealerCode = swaggerUser.DealerCode,
                ClientId = userToClient.ClientId.ToString()
            };

            return Token(user);
        }

        [HttpPost]
        [Route("token")]
        public IHttpActionResult Token(LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            DateTime today = DateTime.UtcNow;
            DateTime tokenRequestDate = GetTokenRequestDate();

            APIUser dbUser = _userRepo.GetAuthenticatedUser(user.Username, user.Password, user.DealerCode);

            if (dbUser == null)
            {
                throw new HttpResponseException(UnauthorizedResponse(MessageResource.ErrorMsgAuthInvalidUsernameOrPassword));
            }

            APIClientUser clientUser = dbUser.Clients == null ? null : dbUser.Clients.FirstOrDefault(c => c.ClientId.ToString().ToUpper() == user.ClientId.ToUpper().Trim());
            if (clientUser == null)
            {
                throw new HttpResponseException(UnauthorizedResponse(MessageResource.ErrorMsgAuthUserNotRegisteredOnSpecifiedClient));
            }

            clientUser = _clientUserRepo.Get(clientUser.Id);

            string jwt = string.Empty;
            int tokenLifeTime = AppConfigs.GetInt("TokenLifeTime");
            DateTime tokenExpiryDate = tokenRequestDate.AddDays(tokenLifeTime);

            // validate token life time
            if (!_clientUserRepo.IsTokenExpired(clientUser, today))
            {
                jwt = clientUser.Token;
            }
            else
            {

                List<string> audiences = AppConfigs.GetListFromSection<string>("TokenAudiences");

                List<Claim> claimsData = new List<Claim>();

                claimsData.Add(new Claim(ClaimTypes.Name, dbUser.UserName));
                claimsData.Add(new Claim("DealerCode", user.DealerCode));
                claimsData.Add(new Claim("UserId", dbUser.Id.ToString()));
                claimsData.Add(new Claim("ClientId", clientUser.ClientId.ToString()));
                claimsData.Add(new Claim("DealerId", dbUser.DealerId.ToString()));


                foreach (string aud in audiences)
                {
                    claimsData.Add(new Claim("aud", aud));
                }

                jwt = jwtManager.GenerateToken(claimsData, clientUser.Client.SecretKey, tokenExpiryDate);
                clientUser.Token = jwt;
                clientUser.TokenExpired = tokenExpiryDate;
            }


            // update token and log the activity
            clientUser.LastLogin = today;
            clientUser.LastActivity = today;

            _clientUserRepo.SetUserLogin(dbUser.UserName);
            _clientUserRepo.Update(clientUser);

            // log user login
            _userActivityRepo.SetUserLogin(dbUser.UserName);
            _userActivityRepo.Create(
                new UserActivity()
                {
                    Username = user.Username,
                    Activity = UserActivityType.Login,
                    ActivityDesc = string.Format("{0}-{1} : {2}", user.Username, user.DealerCode, UserActivityType.Login.ToString()),
                    ActivityTime = tokenRequestDate,
                    Endpoint = this.Request.RequestUri.AbsoluteUri,
                    DealerCode = user.DealerCode,
                    AppId = clientUser.Client.AppId
                }
            );

            return Ok(new
                {
                    access_token = jwt,
                    token_type = "Bearer"
                });
        }

        private DateTime GetTokenRequestDate()
        {
            DateTime today = DateTime.Now.Date;
            today = DateTime.SpecifyKind(today, DateTimeKind.Utc);

            int expiryTokenUTC = 7;
            int expiryTokenTime = 3;

            try
            {
                ApplicationConfig configUtc = _appConfigRepo.GetByKey(Constants.ConfigKey.System_ExpiryTokenUTC);
                ApplicationConfig configExpiredTime = _appConfigRepo.GetByKey(Constants.ConfigKey.System_ExpiryTokenTime); ;

                expiryTokenUTC = configUtc == null ? expiryTokenUTC : int.Parse(configUtc.Value);
                expiryTokenTime = configExpiredTime == null ? expiryTokenTime : int.Parse(configExpiredTime.Value);
            }
            catch (Exception ex)
            {
                expiryTokenUTC = 7;
                expiryTokenTime = 3;
            }

            //today = today.AddHours(-expiryTokenUTC); // convert to UTC Time
            today = today.AddHours(expiryTokenTime); // set the expired time
            return today;
        }

        private HttpResponseMessage UnauthorizedResponse(string message)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            var json = JsonConvert.SerializeObject(
                                        new ResponseMessage()
                                        {
                                            Success = false,
                                            Status = ResponseStatus.Warning,
                                            Message = message
                                        });

            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            response.ReasonPhrase = "Unauthorized";
            return response;
        }


    }
}
