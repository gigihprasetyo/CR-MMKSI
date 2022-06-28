using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Web.Scheduling
{
    public class ApplicationPreload : System.Web.Hosting.IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            HangfireBootstrapper.Instance.Start();
        }
    }
}