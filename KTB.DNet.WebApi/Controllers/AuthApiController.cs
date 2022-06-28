using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;

namespace KTB.DNet.WebApi.Controllers
{
    public class AuthApiController: BaseApiController
    {
        /**
        *   @api {post} /api/authapi/token Token
        *   @apiName Token
        *   @apiGroup Login
        *
        *   @apiHeader {String} X-Authorization-Client ClientSecret value.
        *   @apiHeaderExample {String} Header Example:
        *       {
        *           "X-Authorization-Client": "THIS.IS.YOUR.CLIENT.SECRET"
        *       }
        *       
        *   @apiParam {String} username Username.
        *   @apiParam {String} password Password.
        *
        *   @apiParamExample {post} Url Encoded Example:
        *		"uasername": "string"
        *		"password": "string"
        *
        *   @apiSuccess {Boolean} success Status of the api. <code>True</code> if success; <code>False</code> if fail;
        *   @apiSuccess {Number} total always return <code>1</code>.
        *   @apiSuccess {String} _id Always return <code>-1</code>.
        *   @apiSuccess {String} message The reason if status is fail (<code>success is false</code>).
        *   @apiSuccess {String} lst List of the created record <code>contain of parameter structure with token</code>.
        *
        *   @apiSuccessExample {json} Success-Response:
        *       {
        *           "success": true,
        *           "total": 1,
        *           "_id": -1,
        *           "message": "",
        *           "lst":[{
        *               "username": "string",
        *               "token": "string"
        *           }]
        *       }
        */
        [HttpPost]
        public IDictionary<String, Object> Token(System.Net.Http.Formatting.FormDataCollection param)
        {
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);

            if (param == null) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION," You need to supply Username and Password."), null);
            if (!Request.Headers.Contains("X-Authorization-Client")) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " You need to supply ClientSecret."), null);
            IEnumerable<string> headerValues = Request.Headers.GetValues("X-Authorization-Client");
            var _clieny = headerValues.FirstOrDefault();
            var _app = "Salesforce";
            if (Request.Headers.Contains("app"))
            {
                IEnumerable<string> appHeaderValues = Request.Headers.GetValues("app");
                _app = appHeaderValues.FirstOrDefault();
            }
            if (_app.ToLower().Equals("groupware"))
            {

                if (!_clieny.Equals(appConfigFacade.Retrieve("gwdnet_client").Value)) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your ClientSecret is invalid."), null);

                //SUCCESS . CONTINUE
                string _strUsername = param.Get("username") != null ? param.Get("username") : "_NOTHING_";
                string _strPassword = param.Get("password") != null ? param.Get("password") : "_NOTHING_";

                Dictionary<string, object> user = new Dictionary<string, object>();
                List<object> users = new List<object>();

                if ((_strUsername.ToLower().Equals(appConfigFacade.Retrieve("gwdnet_username").Value)) && (_strPassword.Equals(appConfigFacade.Retrieve("gwdnet_password").Value)))
                {
                    user.Add("username", _strUsername);
                    user.Add("token", appConfigFacade.Retrieve("gwdnet_token").Value);
                    user.Add("app", _app);
                    users.Add(user);

                    return result(true, _strUsername, users.Count, "", users);
                }
                else
                {
                    if (_strUsername.Equals("_NOTHING_"))
                    {
                        return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " You need to supply Username and Password."), null);
                    }
                    else
                    {
                        user.Add("username", _strUsername);
                        users.Add(user);
                        return result(false, _strUsername, users.Count, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Wrong Username and Password."), users);
                    }
                }
            }
            else
            {
                if (!_clieny.Equals(appConfigFacade.Retrieve("wsdnet_client").Value)) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your ClientSecret is invalid."), null);

                //SUCCESS . CONTINUE
                string _strUsername = param.Get("username") != null ? param.Get("username") : "_NOTHING_";
                string _strPassword = param.Get("password") != null ? param.Get("password") : "_NOTHING_";

                Dictionary<string, object> user = new Dictionary<string, object>();
                List<object> users = new List<object>();

                if ((_strUsername.ToLower().Equals(appConfigFacade.Retrieve("wsdnet_username").Value)) && (_strPassword.Equals(appConfigFacade.Retrieve("wsdnet_password").Value)))
                {
                    user.Add("username", _strUsername);
                    user.Add("token", appConfigFacade.Retrieve("wsdnet_token").Value);
                    users.Add(user);

                    return result(true, _strUsername, users.Count, "", users);
                }
                else
                {
                    if (_strUsername.Equals("_NOTHING_"))
                    {
                        return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " You need to supply Username and Password."), null);
                    }
                    else
                    {
                        user.Add("username", _strUsername);
                        users.Add(user);
                        return result(false, _strUsername, users.Count, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Wrong Username and Password."), users);
                    }
                }
            }
        }
    }
}