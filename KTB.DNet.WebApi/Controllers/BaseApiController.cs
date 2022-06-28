using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;

namespace KTB.DNet.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected string _TOKEN_DEFAULT = "bdH8mCzkvIsMvyLZmUd4Kun4tNnt/+gV";
        protected string _USERNAME_DEFAULT = "salesforce";
        protected string _PASSWORD_DEFAULT = "123456";
        protected string _CLIENT_DEFAULT = "54l35f0rc3";

        protected int _API_STATUS = 0;
        protected string _TOKEN = String.Empty;
        protected string _API_MESSAGE = String.Empty;
        protected string _ACCESS_DENIED_AUTHENTICATION = "Failed to authenticate.";
        protected string _ERROR_NO_PARAMETER = "Failed to get data. You need to specify ID value.";
        protected string _ERROR_NO_FOUND = "Failed to get data. Data you are requested is not available on Database.";

        protected Dictionary<string, string> Param;

        protected string GetToken(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            DateTime expired = DateTime.Now.AddDays(30);
            string Key = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            string token = String.Concat(Key, expired.ToString("yyyyMMddHHmmss"));

            //SAVE TOKEN
            
            return token;
        }

        protected bool IsLogin()
        {
            if (!Request.Headers.Contains("X-Authorization-Token")) return false;

            IEnumerable<string> headerValues = Request.Headers.GetValues("X-Authorization-Token");
            var _token = headerValues.FirstOrDefault();

            //CHECK IF _token VALID
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            var _app = "Salesforce";
            if (Request.Headers.Contains("app"))
            {
                IEnumerable<string> appHeaderValues = Request.Headers.GetValues("app");
                _app = appHeaderValues.FirstOrDefault();
            }
            if (!_app.ToLower().Equals("groupware"))
            {
                if (!_token.Equals(appConfigFacade.Retrieve("wsdnet_token").Value)) return false;
            }
            else
            {
                if (!_token.Equals(appConfigFacade.Retrieve("gwdnet_token").Value)) return false;
            }

            return true;
        }

        #region Result

        protected IDictionary<String, Object> result(bool success, string id, int total, string message, List<Object> list)
        {
            IDictionary<String, Object> dictJSON = new Dictionary<String, Object>();
            dictJSON.Add("success", success);
            dictJSON.Add("total", total);
            dictJSON.Add("_id", id);
            dictJSON.Add("message", message);
            dictJSON.Add("lst", list);
            //if (list != null) dictJSON = dictJSON.Concat(list).ToDictionary(x => x.Key, x => x.Value);

            //HttpResponseHeader.
            return dictJSON;
        }

protected IDictionary<String, Object> resultdictionary(bool success, string id, int total, string message, IDictionary<String, Object> list)
        {
            IDictionary<String, Object> dictJSON = new Dictionary<String, Object>();
            dictJSON.Add("success", success);
            dictJSON.Add("total", total);
            dictJSON.Add("_id", id);
            dictJSON.Add("message", message);
            dictJSON.Add("lst", list);
            //if (list != null) dictJSON = dictJSON.Concat(list).ToDictionary(x => x.Key, x => x.Value);

            //HttpResponseHeader.
            return dictJSON;
        }

        #endregion
    }
}
