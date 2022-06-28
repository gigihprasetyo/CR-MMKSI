using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Net.Http;
using System.Web.Configuration;

using KTB.DNet.BusinessFacade.PO;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain.Search;
using KTB.DNET.BusinessFacade.SparePart;
using KTB.DNet.DataMapper.Framework;
using KTB.DNET.BusinessFacade;
using KTB.DNet.Utility;
using System.IO;
using KTB.DNet.BusinessFacade;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class MSPRegistrationLogic
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
        public static void GenerateTextFiletoSAP()
        {
            //+++++++++++++++++++++++++++++++++++++
            StringBuilder lines = new StringBuilder();
            DateTime sendDataStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(-1);
            DateTime sendDataEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59).AddDays(-1);
            MSPRegistrationHistoryFacade MSPRFacade = new MSPRegistrationHistoryFacade(User);
            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(MSPRegistrationHistory), "MSPRegistration.RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
            crit.opAnd(new Criteria(typeof(MSPRegistrationHistory), "MSPRegistration.CreatedTime", MatchType.GreaterOrEqual, sendDataStart));
            crit.opAnd(new Criteria(typeof(MSPRegistrationHistory), "MSPRegistration.CreatedTime", MatchType.LesserOrEqual, sendDataEnd));
            SortCollection st = new SortCollection();
            st.Add(new Sort(typeof(MSPRegistrationHistory), "MSPRegistration.MSPCode", Sort.SortDirection.ASC));
            var arrMSPRegistrationHistory = MSPRFacade.Retrieve(crit, st).Cast<MSPRegistrationHistory>().ToList();

            string separator = ";";
            string strErrMsg = "";
            string strErrMsg2 = "";
            StringBuilder line = new StringBuilder();
            foreach (MSPRegistrationHistory obj in arrMSPRegistrationHistory)
            {
                lines = new StringBuilder();
                line.Append("H");
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCode);
                line.Append(separator);
                line.Append(obj.MSPRegistration.Dealer.DealerCode);
                line.Append("\r\n");
                line.Append("D");
                line.Append(separator);
                line.Append(obj.MSPRegistration.ChassisMaster.ChassisNumber);
                line.Append(separator);
                line.Append(obj.RequestType);
                line.Append(separator);
                line.Append(obj.BenefitMasterHeaderID);
                line.Append(separator);
                if (obj.MSPRegistration.MSPCustomer.RefCustomer != null)
                {
                    line.Append(obj.MSPRegistration.MSPCustomer.RefCustomer.Code);
                }
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCustomer.Name1);
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCustomer.Age);
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCustomer.Alamat);
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCustomer.PhoneNo);
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCustomer.KTPNo);
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCustomer.Kelurahan);
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCustomer.Kecamatan);
                line.Append(separator);
                if (obj.MSPRegistration.MSPCustomer.Province != null)
                {
                    line.Append(obj.MSPRegistration.MSPCustomer.Province.ProvinceName);
                }
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCustomer.PreArea);
                line.Append(separator);
                line.Append(obj.MSPRegistration.MSPCustomer.Email);
                line.Append(separator);
                line.Append(obj.MSPMaster.MSPType.Code);
                line.Append(separator);
                line.Append(obj.MSPMaster.Duration);
                line.Append(separator);
                line.Append(obj.MSPMaster.MSPKm);
                line.Append(separator);
                line.Append(obj.MSPMaster.VehicleType.VechileTypeCode);
                line.Append(separator);
                line.Append(obj.MSPRegistration.ChassisMaster.EndCustomer.OpenFakturDate.ToString("yyyyMMdd"));
                line.Append(separator);
                line.Append(obj.SelisihAmount);
                line.Append(separator);
                line.Append(obj.SoldBy);
                line.Append(separator);
                line.Append(obj.RegistrationDate.ToString("yyyyMMdd"));
                line.Append("\r\n");
                lines.Append(line);
            }

            if (((lines.ToString() != string.Empty)))
            {
                strErrMsg = DoSendSAP(lines);
                if ((strErrMsg != ""))
                {
                    if ((strErrMsg2 == ""))
                    {
                        strErrMsg2 = ("-" + strErrMsg);
                    }
                    else
                    {
                        strErrMsg2 = (strErrMsg2 + ("\\n-" + strErrMsg));
                    }
                }
            }
        }

        public static string DoSendSAP(StringBuilder lines)
        {
            string errMess = "";
            string strFileName = "WSM_MSPRegistrationHistory2SAP.txt";
            string FileDataPath = KTB.DNet.Lib.WebConfig.GetValue("MSPDirectory") + @"\" + strFileName;
            string _user = KTB.DNet.Lib.WebConfig.GetValue("User");
            string _password = KTB.DNet.Lib.WebConfig.GetValue("Password");
            string _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer"); // 172.17.104.204
            var imp = new SAPImpersonate(_user, _password, _webServer);
            imp.Start();
            try
            {
                var dirInfo = new DirectoryInfo(Path.GetDirectoryName(FileDataPath));
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                if (File.Exists(FileDataPath))
                {
                    File.Delete(FileDataPath);
                }

                var fs = new FileStream(FileDataPath, FileMode.CreateNew);
                var sw = new StreamWriter(fs);
                sw.WriteLine(lines.ToString());
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                errMess = ex.Message;
            }

            imp.StopImpersonate();
            imp = null;

            return errMess;
        }
    }
}
