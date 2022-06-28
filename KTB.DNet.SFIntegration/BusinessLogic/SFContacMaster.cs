using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using KTB.DNet.Salesforce.Class;
using KTB.DNET.BusinessFacade;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;

namespace KTB.DNet.SFIntegration.BusinessLogic
{
    public static class SFContachMaster
    {
        public static void ProcessContact()
        {
            new ContactParser().Process(1);
        }

        static void CreateLog(string vReturn, string msg, string logCategory, string strJson)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            WsLog wslog = new WsLog();
            wslog.Source = "Internal";
            wslog.Status = vReturn.ToString();
            wslog.Message = msg;
            wslog.Body = String.Concat(logCategory, strJson);
            wslog.RowStatus = 0;
            wslog.CreatedBy = "WebService";

            WsLogFacade wslogfacade = new WsLogFacade(User);
            wslogfacade.Insert(wslog);

        }
    }
}
