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
using KTB.DNet.Domain.Search;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Configuration;
using System.IO;
using KTB.DNet.DataMapper.Framework;
using System.Collections;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class PDIPKTEmailBroadcastLogic
    {
        public static void Send()
        {
            const string sp_DealerNotification = "sp_DealerNotification ";
            const string sp_EmailPDIPKTNotif = "sp_EmailPDIPKTNotif ";
            string tbAHeader, tbBHeader, tbCHeader, tbABody, tbBBody, tbCBody;

            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            var _m_PDI = MapperFactory.GetInstance().GetMapper(typeof(PDI).ToString());

            try
            {
                DataTable dtDealerGroup = _m_PDI.RetrieveDataSet(sp_DealerNotification).Tables[0];

                foreach (DataRow dr in dtDealerGroup.Rows)
                {
                    ArrayList parameters = new ArrayList();
                    SqlParameter sqlParam =new SqlParameter{
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = dr["DealerID"],
                        ParameterName = "@DealerID"
                    };

                    parameters.Add(sqlParam);

                    DataSet dataBroadcast = _m_PDI.RetrieveDataSet(sp_EmailPDIPKTNotif, parameters);
                    DataTable dtA = dataBroadcast.Tables[0];
                    DataTable dtB = dataBroadcast.Tables[1];
                    DataTable dtC = dataBroadcast.Tables[2];

                    tbAHeader = string.Empty;
                    tbBHeader = string.Empty;
                    tbCHeader = string.Empty;

                    tbABody = string.Empty;
                    tbBBody = string.Empty;
                    tbCBody = string.Empty;

                    foreach (DataColumn iColumn in dtA.Columns)
                    {
                        tbAHeader += "<td align='center' bgcolor='lightgray'><b>" + iColumn.ColumnName + "</b></td>";
                    }

                    foreach (DataColumn iColumn in dtB.Columns)
                    {
                        tbBHeader += "<td align='center' bgcolor='lightgray'><b>" + iColumn.ColumnName + "</b></td>";
                    }

                    foreach (DataColumn iColumn in dtC.Columns)
                    {
                        tbCHeader += "<td align='center' bgcolor='lightgray'><b>" + iColumn.ColumnName + "</b></td>";
                    }

                    foreach (DataRow iRow in dtA.Rows)
                    {
                        tbABody += "<tr>";
                        foreach (DataColumn iClm in dtA.Columns)
                        {
                            tbABody += "<td align='center'>" + iRow[iClm].ToString() + "</td>";
                        }
                        tbABody += "</tr>";
                    }

                    foreach (DataRow iRow in dtB.Rows)
                    {
                        tbBBody += "<tr>";
                        foreach (DataColumn iClm in dtB.Columns)
                        {
                            tbBBody += "<td align='center'>" + iRow[iClm].ToString() + "</td>";
                        }
                        tbBBody += "</tr>";
                    }

                    foreach (DataRow iRow in dtC.Rows)
                    {
                        tbCBody += "<tr>";
                        foreach (DataColumn iClm in dtC.Columns)
                        {
                            tbCBody += "<td align='center'>" + iRow[iClm].ToString() + "</td>";
                        }
                        tbCBody += "</tr>";
                    }

                    string emailTemplate = string.Empty;
                    string emailTemplateFile = AppDomain.CurrentDomain.BaseDirectory + @"\TemplateFile\EmailPDIPKT.html";
                    using (StreamReader sr = new StreamReader(emailTemplateFile))
                    {
                        emailTemplate = sr.ReadToEnd();
                    }

                    foreach (DataColumn iClmReplace in dtDealerGroup.Columns)
                    {
                        emailTemplate = emailTemplate.Replace
                        (
                            string.Format("[{0}]", iClmReplace.ColumnName),
                            dr[iClmReplace].ToString()
                        );
                    }

                    emailTemplate = emailTemplate.Replace("[TableAHeader]", tbAHeader);
                    emailTemplate = emailTemplate.Replace("[TableBHeader]", tbBHeader);
                    emailTemplate = emailTemplate.Replace("[TableCHeader]", tbCHeader);
                    emailTemplate = emailTemplate.Replace("[TableABody]", tbABody);
                    emailTemplate = emailTemplate.Replace("[TableBBody]", tbBBody);
                    emailTemplate = emailTemplate.Replace("[TableCBody]", tbCBody);

                    string emailFrom = WebConfigurationManager.AppSettings["PDIPKTEmailFrom"];
                    string emailTo = dr["Email"].ToString();
                    string emailCC = dr["CC"].ToString();
                    string emailBCC = dr["BCC"].ToString();
                    string subjectEmail = dr["Subject"].ToString();

                    SendEmail(emailTemplate, subjectEmail, emailFrom, emailTo, emailCC, emailBCC);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SendEmail(string htmlString, string subject, string EmailFrom, string EmailTo, string EmailCC = "", string EmailBCC = "", string DisplayName="")
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                DisplayName = "MMKSI Customer Experience";
                if (DisplayName !="")
                {
                    message.From = new MailAddress(EmailFrom, DisplayName);
                }else
                {
                    message.From = new MailAddress(EmailFrom );
                }

               
               
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
