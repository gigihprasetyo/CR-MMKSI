using KTB.DNet.BusinessFacade.General;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Utility;
using KTB.DNet.BusinessValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class PKTToSAPLogic
    {
        public static void Transfer()
        {
            try
            {
                const string sp_PKTGetDataToSAP = "sp_PKTGetDataToSAP";
                var _m_ChassisMasterPKT = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());
                var dt = _m_ChassisMasterPKT.RetrieveDataSet(sp_PKTGetDataToSAP).Tables[0];
                StringBuilder str = new StringBuilder();

                foreach (DataRow row in dt.Rows)
                {
                    str.Append(string.Format("{0};{1}", row["ChassisNumber"], Convert.ToDateTime(row["HandoverDate"]).ToString("ddMMyyyy")));
                    str.Append(Environment.NewLine);
                }

                if (str.Length > 0)
                {
                    string _user = WebConfigurationManager.AppSettings["User"];
                    string _password = WebConfigurationManager.AppSettings["Password"];
                    string _webServer = WebConfigurationManager.AppSettings["WebServer"];
                    string destFile = string.Format(@"{0}\FinishUnit\PKT\PKTData{1}.txt", WebConfigurationManager.AppSettings["SAPFolder"], DateTime.Now.ToString("ddMMyyyyHHmmss"));

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
