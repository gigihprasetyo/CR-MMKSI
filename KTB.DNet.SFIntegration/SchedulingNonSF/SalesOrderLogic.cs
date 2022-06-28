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

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class SalesOrderLogic
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);

        public static List<string> CheckSO()
        {           
            List<SqlParameter> Param = new List<SqlParameter>();
            DataTable dt = new DataTable();
            dt = new SalesOrderFacade(User).CheckSalesOrder();
            List<string> POHeaderList = new List<string>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string PONumber = string.Empty;
                    PONumber = row["PONumber"].ToString();
                    POHeaderList.Add(PONumber);
                }
            }
            return POHeaderList;
        }

        public static async Task ResendWS_SalesOrder()
        {
            HttpClient client = new HttpClient();
            List<WsLog> WSList = new List<WsLog>();
            List<string> POHeaderList = CheckSO();
            if (POHeaderList.Count() > 0)
            {
                for (int i = 0; i < POHeaderList.Count(); i++ )
                {
                    CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(WsLog), "RowStatus", MatchType.Exact, 0));
                    crit.opAnd(new Criteria(typeof(WsLog), "Body", MatchType.StartsWith, "K;" + "SOCREATE"), "(", true);
                    crit.opOr(new Criteria(typeof(WsLog), "Body", MatchType.StartsWith, "K;" + "SOCHANGE"), ")", false);
                    crit.opAnd(new Criteria(typeof(WsLog), "CreatedTime", MatchType.GreaterOrEqual, DateTime.Now.AddMonths(-2)));
                    crit.opAnd(new Criteria(typeof(WsLog), "CreatedTime", MatchType.LesserOrEqual, DateTime.Now));
                    crit.opAnd(new Criteria(typeof(WsLog), "Body", MatchType.Partial, POHeaderList[i]));

                    SortCollection st = new SortCollection();
                    st.Add(new Sort(typeof(WsLog), "ID", Sort.SortDirection.DESC));

                    var arr = new WsLogFacade(User).Retrieve(crit, st).Cast<WsLog>().ToList();
                    if (arr.Count() > 0)
                    {
                        AppConfigFacade appConfigFacade = new AppConfigFacade(User);
                        string WSLink = appConfigFacade.Retrieve("WSLink").Value;
                        string WSPass = appConfigFacade.Retrieve("WSPass").Value;

                        var content = new StringContent(arr[0].Body);
                        client.DefaultRequestHeaders.Add("x-pass-header", WSPass);
                        var response = await client.PostAsync(WSLink, content);
                    }
                }
            }

        }
    }
}
