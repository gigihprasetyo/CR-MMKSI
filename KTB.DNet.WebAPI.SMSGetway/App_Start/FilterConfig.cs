using System.Web;
using System.Web.Mvc;

namespace KTB.DNet.WebAPI.SMSGetway
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
