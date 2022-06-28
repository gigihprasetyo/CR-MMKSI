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
    public static class SPKNationalEventLogic
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);

        public static void SendMail()
        {
            try
            {
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);

                string strEmailScheduleTime = string.Empty;
                CriteriaComposite crit4 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit4.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "BabitEventNational.EmailScheduleTimeInHours"));
                AppConfig objAppConfig = appConfigFacade.Retrieve(crit4).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strEmailScheduleTime = objAppConfig.Value;
                }
                //if (strEmailScheduleTime != DateTime.Now.ToString("HH")) { return; }

                string strWSEmailFrom = string.Empty;
                CriteriaComposite crit0 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit0.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "BabitEventNational.EmailFrom"));
                objAppConfig = appConfigFacade.Retrieve(crit0).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strWSEmailFrom = objAppConfig.Value;
                }
                string strSubjectEmail = string.Empty;
                CriteriaComposite crit1 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit1.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "BabitEventNational.SubjectEmail"));
                objAppConfig = appConfigFacade.Retrieve(crit1).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strSubjectEmail = objAppConfig.Value;
                }
                string strEmailCC = string.Empty;
                CriteriaComposite crit2 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit2.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "BabitEventNational.EmailCC"));
                objAppConfig = appConfigFacade.Retrieve(crit2).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strEmailCC = objAppConfig.Value;
                }
                string strEmailBCC = string.Empty;
                CriteriaComposite crit3 = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit3.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "BabitEventNational.EmailBcc"));
                objAppConfig = appConfigFacade.Retrieve(crit3).Cast<AppConfig>().FirstOrDefault();
                if (objAppConfig != null)
                {
                    strEmailBCC = objAppConfig.Value;
                }

                DataTable dt = new DataTable();
                dt = DT_ForSendEmail();

                bool _result = false;
                string strSubjectEmail2 = string.Empty;
                string _dealerCode = string.Empty;
                string _body = string.Empty;
                string _SPKNumber = string.Empty;
                string _wSEmailTo = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strSubjectEmail2 = strSubjectEmail;
                    _result = false;
                    var index = i;
                    DataRow dr1 = dt.Rows[i];
                    DataRow dr2 = null;
                    if (_wSEmailTo == "")
                    {
                        _wSEmailTo = dr1["SendTo"].ToString();
                    }
                    else
                    {
                        _wSEmailTo += ";" + dr1["SendTo"].ToString();
                    }

                    if ((i + 1) == dt.Rows.Count)
                    {
                        _body = dr1["EmailBody"].ToString();
                        if (!string.IsNullOrWhiteSpace(_body) && !string.IsNullOrWhiteSpace(_wSEmailTo))
                        {
                            strSubjectEmail2 = strSubjectEmail2 + " [" + dr1["RegNumberEvent"].ToString() + " - " + dr1["EventName"].ToString() + "]";
                            _result = DoSendEmail(_body, strSubjectEmail2, strWSEmailFrom, _wSEmailTo, strEmailCC, strEmailBCC);
                        }
                    }
                    else
                    {
                        dr2 = dt.Rows[i + 1];
                        if (dr1["DealerCodeSendTo"].ToString() != dr2["DealerCodeSendTo"].ToString() || dr1["RegNumberEvent"].ToString() != dr2["RegNumberEvent"].ToString())
                        {
                            _body = dr1["EmailBody"].ToString();
                            strSubjectEmail2 = strSubjectEmail2 + " [" + dr1["RegNumberEvent"].ToString() + " - " + dr1["EventName"].ToString() + "]";
                            if (!string.IsNullOrWhiteSpace(_body) && !string.IsNullOrWhiteSpace(_wSEmailTo))
                            {
                                _result = DoSendEmail(_body, strSubjectEmail2, strWSEmailFrom, _wSEmailTo, strEmailCC, strEmailBCC);
                            }
                            _wSEmailTo = "";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string cuk = e.Message;
            }
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
                    message.IsBodyHtml = true;
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

        private static DataTable DT_ForSendEmail()
        {
            SPKNationalEventFacade spkNationalEventFac = new SPKNationalEventFacade(User);
            DataTable result = spkNationalEventFac.RetrieveDataTableForSendMail();
            return result;
        }

    }
}
