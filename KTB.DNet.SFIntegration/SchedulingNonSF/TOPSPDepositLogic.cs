using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.BusinessFacade.PO;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain.Search;
using System.Net;
using System.Net.Http;
using KTB.DNET.BusinessFacade.SparePart;
using System.Web.Configuration;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class TOPSPDepositLogic
    {
        public static async Task WSResend_TOPSPDeposit()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();
            SparePartBillingFacade func = new SparePartBillingFacade(User);
            List<SparePartBilling> listFail = func.GetTOPSPBillingDepositData();
            List<string> listWLog = new List<string>();

            foreach (SparePartBilling dt in listFail)
            {
                string WSBody = func.GetWsLogTOPSPBillingDeposit(dt.BillingNumber);
                listWLog.Add(WSBody);
            }

            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string WSLink = appConfigFacade.Retrieve("WsLinkDsk").Value;
            string WSPass = appConfigFacade.Retrieve("WSPass").Value;

            foreach (string iWlogFail in listWLog)
            {
                var content = new StringContent(iWlogFail);
                client.DefaultRequestHeaders.Add("x-pass-header", WSPass);
                var response = await client.PostAsync(WSLink, content);
            }

        }

    }
}
