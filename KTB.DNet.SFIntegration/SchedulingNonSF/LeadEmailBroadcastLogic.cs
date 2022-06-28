using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.BusinessFacade.SAP;
using KTB.DNet.Domain.Search;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Configuration;
using System.IO;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public class LeadEmailBroadcastLogic
    {
        public static void Send()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SAPCustomerFacade func = new SAPCustomerFacade(User);
            AppConfigFacade funcConfig = new AppConfigFacade(User);
            string headerColumn = string.Empty;
            string bodyTable = string.Empty;
            try
            {
                DataSet dataBroadcast = func.GetDataBroadcast();
                DataTable dtSubject = dataBroadcast.Tables[0];
                DataTable dtReplace = dataBroadcast.Tables[1];
                DataTable dtCustomer = dataBroadcast.Tables[2];

                if (dtCustomer.Rows.Count == 0)
                {
                    return;
                }

                foreach (DataColumn iColumn in dtCustomer.Columns)
                {
                    headerColumn += "<td bgcolor=\"lightgray\"><b>" + iColumn.ColumnName + "</b></td>";
                }

                foreach (DataRow iRow in dtCustomer.Rows)
                {
                    bodyTable += "<tr>";
                    foreach (DataColumn iClm in dtCustomer.Columns)
                    {
                        bodyTable += "<td>" + iRow[iClm].ToString() + "</td>";
                    }
                    bodyTable += "</tr>";
                }

                string emailTemplate = string.Empty;
                string emailTemplateFile = AppDomain.CurrentDomain.BaseDirectory + @"\TemplateFile\EmailDigitalLead.html";
                using (StreamReader sr = new StreamReader(emailTemplateFile))
                {
                    emailTemplate = sr.ReadToEnd();
                }

                foreach (DataColumn iClmReplace in dtReplace.Columns)
                {
                    emailTemplate = emailTemplate.Replace
                    (
                        string.Format("[{0}]", iClmReplace.ColumnName), 
                        dtReplace.Rows[0][iClmReplace].ToString()
                    );
                }
                emailTemplate = emailTemplate.Replace("[DataHeader]", headerColumn);
                emailTemplate = emailTemplate.Replace("[DataBody]", bodyTable);

                string emailFrom = WebConfigurationManager.AppSettings["EmailFrom"];
                string emailTo = funcConfig.Retrieve("ACDigitalLeadEmailTo").Value;
                string emailCC = funcConfig.Retrieve("ACDigitalLeadEmailcc").Value;
                string emailBCC = funcConfig.Retrieve("ACDigitalLeadEmailbcc").Value;
                string subjectEmail = dtSubject.Rows[0][0].ToString();

                SendEmail(emailTemplate, subjectEmail, emailFrom, emailTo, emailCC, emailBCC);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SendEmail(string htmlString,string subject, string EmailFrom, string EmailTo, string EmailCC = "", string EmailBCC="")
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(EmailFrom);
                string[] Email_To_List = EmailTo.Split(';');
                foreach (string emTo in Email_To_List)
                {
                    message.To.Add(new MailAddress(emTo));
                }

                if (!string.IsNullOrEmpty(EmailCC))
                {
                    string[] Email_CC_List = EmailCC.Split(';');
                    foreach (string emCC in Email_CC_List)
                    {
                        message.CC.Add(new MailAddress(emCC));
                    }
                }

                if (!string.IsNullOrEmpty(EmailBCC))
                {
                    string[] Email_BCC_List = EmailBCC.Split(';');
                    foreach (string emBCC in Email_BCC_List)
                    {
                        message.Bcc.Add(new MailAddress(emBCC));
                    }
                }

                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                string SmtpAddress = WebConfigurationManager.AppSettings["SMTP"];
                SmtpClient SmtpServer = new SmtpClient(SmtpAddress);
                SmtpServer.Port = string.IsNullOrEmpty(WebConfigurationManager.AppSettings["Port"]) ? 
                    25 : Convert.ToInt32(WebConfigurationManager.AppSettings["Port"]);
                SmtpServer.Send(message);
            }
            catch (Exception)
            {
                throw;
            }
        }    
    }
}
