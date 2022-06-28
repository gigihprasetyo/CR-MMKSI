using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.ServiceModel.Channels;

using System.IO;
using Newtonsoft.Json;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.BusinessFacade.SAP;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.BusinessFacade.FinishUnit;
using KTB.DNet.BusinessFacade.Salesman;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace KTB.DNet.WebApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/dashboard")]
    public class DashboardController : BaseApiController
    {
        private Database _Db;
        private DBCommandWrapper _DBCommandWrapper;

        [HttpGet]
        [System.Web.Http.Route("dealer")]
        public IDictionary<string, object> Dealer()
        {
            string _id = HttpContext.Current.Request.QueryString["id"];
            if (String.IsNullOrEmpty(_id)) _id = "-123456789";

            bool success = true; string message = string.Empty;
            Db = DatabaseFactory.CreateDatabase();

            List<object> lst = RetrieveSP(String.Concat("SELECT DealerName FROM Dealer WHERE id = ", _id));

            return this.result(success, "-1", lst.Count(), message, lst);
        }

        [HttpGet]
        [System.Web.Http.Route("case")]
        public IDictionary<string, object> Case()
        {
            string _id = HttpContext.Current.Request.QueryString["id"];
            if (String.IsNullOrEmpty(_id)) _id = "-123456789";

            bool success = true; string message = string.Empty;
            Db = DatabaseFactory.CreateDatabase();

            List<object> lst = RetrieveSP(String.Concat("EXEC [dbo].[sp_GetCustomerCaseSummaryPerMonth] ", _id));

            return this.result(success, "-1", lst.Count(), message, lst);
        }

        [HttpGet]
        [System.Web.Http.Route("casesummary")]
        public IDictionary<string, object> CaseSummary()
        {
            string _id = HttpContext.Current.Request.QueryString["id"];
            if (String.IsNullOrEmpty(_id)) _id = "-123456789";

            bool success = true; string message = string.Empty;
            Db = DatabaseFactory.CreateDatabase();

            List<object> lst = RetrieveSP(String.Concat("EXEC [dbo].[sp_GetCustomerCaseSummaryPerStatus ] ", _id));

            return this.result(success, "-1", lst.Count(), message, lst);
        }

        [HttpGet]
        [System.Web.Http.Route("lead")]
        public IDictionary<string, object> Lead()
        {
            string _id = HttpContext.Current.Request.QueryString["id"];
            if (String.IsNullOrEmpty(_id)) _id = "-123456789";

            bool success = true; string message = string.Empty;
            Db = DatabaseFactory.CreateDatabase();

            List<object> lst = RetrieveSP(String.Concat("exec [sp_GetLeadCustomerSummaryPerMonth] ", _id));

            return this.result(success, "-1", lst.Count(), message, lst);
        }

        [HttpGet]
        [System.Web.Http.Route("leadsummary")]
        public IDictionary<string, object> LeadSummary()
        {
            string _id = HttpContext.Current.Request.QueryString["id"];
            if (String.IsNullOrEmpty(_id)) _id = "-123456789";

            bool success = true; string message = string.Empty;
            Db = DatabaseFactory.CreateDatabase();

            List<object> lst = RetrieveSP(String.Concat("exec [sp_GetLeadCustomerPerStatus] ", _id));

            return this.result(success, "-1", lst.Count(), message, lst);
        }

        [HttpGet]
        [System.Web.Http.Route("leadsummaryresponse")]
        public IDictionary<string, object> LeadSummaryResponse()
        {
            string _id = HttpContext.Current.Request.QueryString["id"];
            if (String.IsNullOrEmpty(_id)) _id = "-123456789";

            bool success = true; string message = string.Empty;
            Db = DatabaseFactory.CreateDatabase();

            List<object> lst = RetrieveSP(String.Concat("exec [sp_GetLeadCustomerPerResponse] ", _id));

            return this.result(success, "-1", lst.Count(), message, lst);
        }

        protected List<object> RetrieveSP(String sql)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.IDataReader dtr = null;
            IDictionary<string, object> arlResult = new Dictionary<string, object>();
            List<object> res = new List<object>();
            System.Data.SqlClient.SqlConnection con = null;

            try
            {
                con = (System.Data.SqlClient.SqlConnection)Db.GetConnection();
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;
                cmd.CommandTimeout = 600;

                dtr = cmd.ExecuteReader(System.Data.CommandBehavior.Default);

                while (dtr.Read())
                {
                    arlResult = Enumerable.Range(0, dtr.FieldCount).ToDictionary(dtr.GetName, dtr.GetValue);
                    res.Add(arlResult);
                }
            }
            catch (Exception ex)
            {
                res = new List<object>();
            }
            finally
            {
                if (dtr != null) dtr.Close();
                if (con != null) { con.Close(); con.Dispose(); }
            }

            return res;
        }

        protected Database Db {
            get {
                return _Db;
            }
            set {
                _Db = value;
            }
        }

        protected DBCommandWrapper DBCommandWrapper
        {
            get
            {
                return _DBCommandWrapper;
            }
            set
            {
                _DBCommandWrapper = value;
            }
        }
    }
}
