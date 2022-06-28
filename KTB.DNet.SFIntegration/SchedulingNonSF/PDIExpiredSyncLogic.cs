using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using KTB.DNet.BusinessValidation;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class PDIExpiredSyncLogic
    {
        public static void Transfer()
        {
            try
            {
                const string up_PDIExpiredSync = "up_PDIExpiredSync";
                var _m_PDI = MapperFactory.GetInstance().GetMapper(typeof(PDI).ToString());
                var dt = _m_PDI.RetrieveDataSet(up_PDIExpiredSync).Tables[0];

                StringBuilder str = new StringBuilder();
                string code = string.Empty;
                string dealerCode = string.Empty;
                string chassisNumber = string.Empty;
                string kind = string.Empty;
                string pdiDate = string.Empty;
                string releaseDate = string.Empty;

                if (dt.Rows.Count > 0)
                    code = dt.Rows[0]["Category"].ToString();

                foreach (DataRow row in dt.Rows)
                {
                    dealerCode = row["DealerCode"].ToString();
                    chassisNumber = row["ChassisNumber"].ToString();
                    kind = row["Kind"].ToString();
                    pdiDate = string.IsNullOrEmpty(row["PDIDate"].ToString()) ? "" : string.Format("{0:ddMMyyyy}", Convert.ToDateTime(row["PDIDate"]));
                    releaseDate = string.IsNullOrEmpty(row["ReleaseDate"].ToString()) ? "" : string.Format("{0:ddMMyyyy}", Convert.ToDateTime(row["ReleaseDate"]));
                    str.Append(string.Format("{0},{1},{2},{3},{4}", dealerCode, chassisNumber, kind, pdiDate, releaseDate));
                    str.Append(Environment.NewLine);
                }

                if (str.Length > 0)
                {
                    string _user = WebConfigurationManager.AppSettings["User"];
                    string _password = WebConfigurationManager.AppSettings["Password"];
                    string _webServer = WebConfigurationManager.AppSettings["WebServer"];
                    string destFile = string.Format(@"{0}\Service\PDI\PDIData{1}_{2}.txt", WebConfigurationManager.AppSettings["SAPFolder"],
                                                DateTime.Now.ToString("ddMMyyyyHHmmss"), code);
                    SAPHelper helper = new SAPHelper(_user, _password, _webServer);
                    helper.SentFile(destFile, str);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
