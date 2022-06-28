using System.Web;
using System.Web.Mvc;

namespace KTB.DNet.Web.Scheduling
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
