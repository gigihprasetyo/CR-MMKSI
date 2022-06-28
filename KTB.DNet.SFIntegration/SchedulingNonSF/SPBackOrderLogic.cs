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
    public static class SPBackOrderLogic
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);

        public static void SendMail()
        {
            try
            {
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);

                string strEmailScheduleTime = string.Empty;
                CriteriaComposite crit4 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit4.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "SPBackOrder_EmailScheduleTime"));
                AppConfig objAppConfig = appConfigFacade.Retrieve(crit4).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strEmailScheduleTime = objAppConfig.Value;
                }
                if (strEmailScheduleTime != DateTime.Now.ToString("HH")) { return; }

                string strWSEmailFrom = string.Empty;
                CriteriaComposite crit0 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit0.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "SPBackOrder_EmailFrom"));
                objAppConfig = appConfigFacade.Retrieve(crit0).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strWSEmailFrom = objAppConfig.Value;
                }
                string strSubjectEmail = string.Empty;
                CriteriaComposite crit1 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit1.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "SPBackOrder_SubjectEmail"));
                objAppConfig = appConfigFacade.Retrieve(crit1).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strSubjectEmail = objAppConfig.Value;
                }
                string strEmailCC = string.Empty;
                CriteriaComposite crit2 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit2.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "SPBackOrder_EmailCC"));
                objAppConfig = appConfigFacade.Retrieve(crit2).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strEmailCC = objAppConfig.Value;
                }
                string strEmailBCC = string.Empty;
                CriteriaComposite crit3 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit3.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "SPBackOrder_EmailBCC"));
                objAppConfig = appConfigFacade.Retrieve(crit3).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strEmailBCC = objAppConfig.Value;
                }

                DataTable dt = new DataTable();
                dt = DT_ForSendEmail();

                bool _result = false;
                string _dealerCode = string.Empty;
                string _body = string.Empty;
                string _poNumber = string.Empty;
                string _wSEmailTo = string.Empty;
                string _wSEmailCC = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _result = false;
                    var index = i;
                    DataRow dr1 = dt.Rows[i];
                    DataRow dr2 = null;
                    DataRow dr3 = null;
                    if (_wSEmailTo == "")
                    {
                        _wSEmailTo = dr1["SendTo"].ToString();
                    }
                    else
                    {
                        if (i > 0)
                        {
                            dr3 = dt.Rows[i - 1];
                            if (dr1["NameRecipientTo"].ToString() != dr3["NameRecipientTo"].ToString())
                            {
                                _wSEmailTo += ";" + dr1["SendTo"].ToString();                            
                            }
                        }
                        else
                        {
                            _wSEmailTo = dr1["SendTo"].ToString();                            
                        }
                    }

                    dr3 = null;
                    if (_wSEmailCC == "")
                    {
                        _wSEmailCC = strEmailCC + ";" + dr1["SendCC"].ToString();
                    }
                    else
                    {
                        if (i > 0)
                        {
                            dr3 = dt.Rows[i - 1];
                            if (dr1["NameRecipientCC"].ToString() != dr3["NameRecipientCC"].ToString())
                            {
                                _wSEmailCC += ";" + dr1["SendCC"].ToString();
                            }
                        }
                        else
                        {
                            _wSEmailCC += ";" + dr1["SendCC"].ToString();
                        }
                    }

                    if ((i + 1) == dt.Rows.Count)
                    {
                        _body = dr1["EmailBody"].ToString();
                        if (!string.IsNullOrWhiteSpace(_body) && !string.IsNullOrWhiteSpace(_wSEmailTo))
                        {
                            _result = DoSendEmail(_body, strSubjectEmail, strWSEmailFrom, _wSEmailTo, _wSEmailCC, strEmailBCC);
                            if (_result)
                            {
                                _poNumber = dr1["PONumber"].ToString();
                                if (!string.IsNullOrWhiteSpace(_poNumber))
                                {
                                    updateStatusSendEmailLog(_poNumber);
                                }
                            }
                        }
                    }
                    else
                    {
                        dr2 = dt.Rows[i + 1];
                        if (dr1["DealerCode"].ToString() != dr2["DealerCode"].ToString())
                        {
                            _body = dr1["EmailBody"].ToString();
                            if (!string.IsNullOrWhiteSpace(_body) && !string.IsNullOrWhiteSpace(_wSEmailTo))
                            {
                                _result = DoSendEmail(_body, strSubjectEmail, strWSEmailFrom, _wSEmailTo, _wSEmailCC, strEmailBCC);
                                if (_result)
                                {
                                    _poNumber = dr1["PONumber"].ToString();
                                    if (!string.IsNullOrWhiteSpace(_poNumber))
                                    {
                                        updateStatusSendEmailLog(_poNumber);
                                    }
                                }
                            }
                            _wSEmailTo = "";
                            _wSEmailCC = "";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string cuk = e.Message;
            }
        }

        public static void GenerateTextFiletoSAP()
        {
            string strTimeDB = string.Empty;
            string strTimeOfDay = DateTime.Now.ToString("HH");
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            CriteriaComposite crit0 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
            crit0.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "SPBackOrder_WSM_ExtendTime"));
            AppConfig objAppConfig = appConfigFacade.Retrieve(crit0).Cast<AppConfig>().FirstOrDefault();
            if (objAppConfig != null)
            {
                strTimeDB = objAppConfig.Value;
            }
            if (strTimeOfDay != strTimeDB) { return; }

            StringBuilder lines = new StringBuilder();
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            SparePartOutstandingOrderDetailFacade facSPOODtl = new SparePartOutstandingOrderDetailFacade(User);
            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(SparePartOutstandingOrderDetail), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
            crit.opAnd(new Criteria(typeof(SparePartOutstandingOrderDetail), "Status", MatchType.InSet, "(1,2)"));
            crit.opAnd(new Criteria(typeof(SparePartOutstandingOrderDetail), "IsTransfer", MatchType.Exact, 0));
            crit.opAnd(new Criteria(typeof(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.ValidTo", MatchType.Exact, now));
            SortCollection st = new SortCollection();
            st.Add(new Sort(typeof(SparePartOutstandingOrderDetail), "SparePartOutstandingOrder.SparePartPO.PONumber", Sort.SortDirection.ASC));
            var arlSPOODtl = facSPOODtl.Retrieve(crit, st).Cast<SparePartOutstandingOrderDetail>().ToList();

            string separator = ";";
            string strErrMsg = "";
            string strErrMsg2 = "";
            string strPONumber = "";
            string strPartNumber = "";
            foreach (SparePartOutstandingOrderDetail obj in arlSPOODtl)
            {
                System.Text.StringBuilder line = new System.Text.StringBuilder();
                if ((strPONumber != obj.SparePartOutstandingOrder.SparePartPO.PONumber))
                {
                    if (((lines.ToString() != string.Empty) && (strPONumber != string.Empty)))
                    {
                        strErrMsg = DoSendSAP(lines, strPONumber);
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

                    strPONumber = obj.SparePartOutstandingOrder.SparePartPO.PONumber;
                    strPartNumber = "";
                    lines = new StringBuilder();
                    line.Append("H");
                    line.Append(separator);
                    line.Append(obj.SparePartOutstandingOrder.SparePartPO.PONumber);
                    line.Append("\r\n");
                }
                if (strPartNumber != obj.PartNumber){
                    line.Append("D");
                    line.Append(separator);
                    line.Append(obj.PartNumber);
                    line.Append(separator);
                    line.Append(obj.Status == 2 ? "X" : "");
                    line.Append("\r\n");
                    strPartNumber = obj.PartNumber;
                    lines.Append(line);
                }
            }

            if (((lines.ToString() != string.Empty) && (strPONumber != "")))
            {
                strErrMsg = DoSendSAP(lines, strPONumber);
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
                else
                {
                    int _result = 0;
                    foreach (SparePartOutstandingOrderDetail obj in arlSPOODtl)
                    {
                        obj.IsTransfer = 1;
                        _result = facSPOODtl.Update(obj);
                    }
                }
            }
        }

        public static string DoSendSAP(StringBuilder lines, string strPONumber)
        {
            string errMess = "";
            string strFileName = strPONumber + "ExtendBO.txt";
            string FileDataPath = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolderSPBOFile") + @"\" + strFileName;
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

        public static Boolean DoSendEmail(string bodyMail, string SubjectEmail, string EmailFrom, string EmailTo, string EmailCC, string EmailBCC)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(EmailFrom);

                string[] EmailCCToList = EmailCC == "" ? new string[] { } : EmailCC.Split(';');
                foreach (string em in EmailCCToList)
                {
                    message.CC.Add(new MailAddress(em));
                }

                string[] EmailBCCToList = EmailBCC == "" ? new string[] { } : EmailBCC.Split(';');
                foreach (string em in EmailBCCToList)
                {
                    message.Bcc.Add(new MailAddress(em));
                }

                string[] EmailToList = EmailTo == "" ? new string[] { } : EmailTo.Split(';');
                foreach (string em in EmailToList)
                {
                    message.To.Add(new MailAddress(em));
                }

                if (EmailToList.Length > 0)
                {
                    string FileImagePath = KTB.DNet.Lib.WebConfig.GetValue("SPBackOrderFileDirectory") + "QuickGuide.png";
                    message.IsBodyHtml = true;
                    message.AlternateViews.Add(GetEmbeddedImage(FileImagePath,ref bodyMail));
                    message.Subject = SubjectEmail;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = bodyMail;
                    string SmtpAddress = WebConfigurationManager.AppSettings["SMTP"];
                    SmtpClient SmtpServer = new SmtpClient(SmtpAddress);
                    SmtpServer.Port = 25;
                    string userState = bodyMail;
                    SmtpServer.Send(message);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static AlternateView GetEmbeddedImage(String filePath, ref string bodyMail)
        {
            string errMess = "";
            AlternateView alternateView = null;
            string _user = KTB.DNet.Lib.WebConfig.GetValue("User");
            string _password = KTB.DNet.Lib.WebConfig.GetValue("Password");
            string _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer"); // 172.17.104.204
            var imp = new SAPImpersonate(_user, _password, _webServer);
            imp.Start();
            try
            {
                LinkedResource res = new LinkedResource(filePath);
                res.ContentId = Guid.NewGuid().ToString();
                string htmlBody = bodyMail.Replace("<#QuickGuide#>", @"<img src='cid:" + res.ContentId + @"'/>");
                alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(res);
            }
            catch (Exception ex)
            {
                errMess = ex.Message;
            }
            imp.StopImpersonate();
            imp = null;

            return alternateView;
        }

        private static DataTable DT_ForSendEmail()
        {
            SparePartPOFacade SPPOFac = new SparePartPOFacade(User);
            DataTable result = SPPOFac.RetrieveDataTableForSendMail();
            return result;
        }

        private static bool updateStatusSendEmailLog(string _poNumber = "")
        {
            int result = 0;
            try
            {
                SparePartPOFacade SPPOFac = new SparePartPOFacade(User);
                string[] poNums = _poNumber.Split(',');
                foreach (var noPO in poNums)
                {
                    SparePartPO objSparePartPO = SPPOFac.Retrieve(noPO);
                    if (objSparePartPO != null)
                    {
                        var spOutstandingOrderFacade = new SparePartOutstandingOrderFacade(User);
                        CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(SparePartOutstandingOrder), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                        crit.opAnd(new Criteria(typeof(SparePartOutstandingOrder), "SparePartPO.ID", MatchType.Exact, objSparePartPO.ID));
                        SparePartOutstandingOrder objSparePartOutstandingOrder = spOutstandingOrderFacade.Retrieve(crit).Cast<SparePartOutstandingOrder>().FirstOrDefault();
                        if (objSparePartOutstandingOrder != null)
                        {
                            objSparePartOutstandingOrder.LastEmailDate = DateTime.Now;
                            int _result = spOutstandingOrderFacade.Update(objSparePartOutstandingOrder);
                        }
                    }
                }
                result = 1;
            }
            catch (Exception)
            {
                result = 0;
            }
            return result <= 0 ? false: true;
        }
    }
}
