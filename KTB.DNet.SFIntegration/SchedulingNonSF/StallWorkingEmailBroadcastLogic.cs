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
    public static class StallWorkingEmailBroadcastLogic
    {
        public static void Send()
        {
            const string sp_DealerNotificationSW = "sp_DealerNotificationSW";
            const string sp_EmailStallWorkingTimeNotif = "sp_EmailStallWorkingTimeNotif";
            string tbAHeader, tbBHeader, tbABody, tbBBody;

            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            var _m_StallWorkingTime = MapperFactory.GetInstance().GetMapper(typeof(StallWorkingTime).ToString());

            try
            {
                DataTable dtDealerGroup = _m_StallWorkingTime.RetrieveDataSet(sp_DealerNotificationSW).Tables[0];

                foreach (DataRow dr in dtDealerGroup.Rows)
                {
                    ArrayList parameters = new ArrayList();
                    SqlParameter sqlParam = new SqlParameter
                    {
                        DbType = DbType.String,
                        Direction = ParameterDirection.Input,
                        Value = dr["DealerID"],
                        ParameterName = "@DealerID"
                    };

                    parameters.Add(sqlParam);

                    DataSet dataBroadcast = _m_StallWorkingTime.RetrieveDataSet(sp_EmailStallWorkingTimeNotif, parameters);
                    DataTable dtA = dataBroadcast.Tables[0];
                    DataTable dtB = dataBroadcast.Tables[1];

                    tbAHeader = string.Empty;
                    tbBHeader = string.Empty;

                    tbABody = string.Empty;
                    tbBBody = string.Empty;

                    string bgColor = string.Empty;
                    string fontColor = string.Empty;

                    foreach (DataColumn iColumn in dtA.Columns)
                    {
                        tbAHeader += "<td align='center' bgcolor='#C1F1FF' style='background:#C1F1FF;'><b>" + iColumn.ColumnName + "</b></td>";
                    }

                    foreach (DataRow iRow in dtA.Rows)
                    {
                        tbABody += "<tr>";
                        foreach (DataColumn iClm in dtA.Columns)
                        {
                            if (iClm.ColumnName.Equals("Kode Dealer") || iClm.ColumnName.Equals("Dealer Area"))
                                tbABody += "<td align='center'>" + iRow[iClm].ToString() + "</td>";
                            else
                            {
                                bgColor = string.IsNullOrEmpty(iRow[iClm].ToString()) ? "#d3d3d3" : iRow[iClm].ToString();
                                tbABody += "<td align='center' bgcolor='" + bgColor + "' style='background:" + bgColor + ";'></td>";
                            }
                        }
                        tbABody += "</tr>";
                    }

                    foreach (DataColumn iColumn in dtB.Columns)
                    {
                        switch (iColumn.ColumnName)
                        {
                            case "Total Stall Fill Rate Gap":
                                bgColor = "#ff7c0a";
                                fontColor = "style='color:#ffffff;'";
                                break;
                            case "Stall Reguler Fill Rate (%)":
                            case "Stall MQP Fill Rate (%)":
                            case "Stall Washing Fill Rate (%)":
                            case "Total Stall Fill Rate (%)":
                                bgColor = "#154c79";
                                fontColor = "style='color:#ffffff;'";
                                break;
                            default:
                                bgColor = "#C1F1FF";
                                fontColor = string.Empty;
                                break;
                        }

                        tbBHeader += "<td align='center' bgcolor='" + bgColor + "' " + fontColor + "><b>" + iColumn.ColumnName + "</b></td>";
                    }

                    foreach (DataRow iRow in dtB.Rows)
                    {
                        bgColor = string.Empty;
                        fontColor = string.Empty;

                        if (iRow["Total Input Stall"].ToString().Trim().Equals("0"))
                        {
                            bgColor = "#fc0303";
                            fontColor = "style='color:#ffffff;'";
                        }

                        tbBBody += "<tr>";
                        foreach (DataColumn iClm in dtB.Columns)
                        {
                            bgColor = bgColor.Equals("#fc0303") ? bgColor : string.Empty;
                            if (string.IsNullOrEmpty(bgColor) && (iRow[iClm].ToString().Trim().Equals("0") || iRow[iClm].ToString().Trim().Equals("0.00 %")))
                                bgColor = "#FFFF00";
                            tbBBody += "<td align='center' bgcolor='" + bgColor + "' " + fontColor + ">" + iRow[iClm].ToString() + "</td>";
                        }
                        tbBBody += "</tr>";
                    }

                    string emailTemplate = string.Empty;
                    string emailTemplateFile;

                    if (dr["IsDealerDMS"].ToString().Equals("1"))
                        emailTemplateFile = AppDomain.CurrentDomain.BaseDirectory + @"\TemplateFile\EmailStallWorkingTimeForDMS.html";
                    else
                        emailTemplateFile = AppDomain.CurrentDomain.BaseDirectory + @"\TemplateFile\EmailStallWorkingTime.html";

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
                    emailTemplate = emailTemplate.Replace("[TableABody]", tbABody);
                    emailTemplate = emailTemplate.Replace("[TableBBody]", tbBBody);

                    emailTemplate = emailTemplate.Replace("[TotalColumnA]", dtA.Columns.Count.ToString());
                    emailTemplate = emailTemplate.Replace("[TotalColumnB]", dtB.Columns.Count.ToString());

                    string emailFrom = dr["Sender"].ToString();
                    string emailTo = dr["Email"].ToString();
                    string emailCC = dr["CC"].ToString();
                    string emailBCC = dr["BCC"].ToString();
                    string subjectEmail = dr["Subject"].ToString();
                    string displayName = dr["DisplayName"].ToString();

                    SendEmail(emailTemplate, subjectEmail, emailFrom, emailTo, emailCC, emailBCC, displayName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SendEmail(string htmlString, string subject, string EmailFrom,
            string EmailTo, string EmailCC = "", string EmailBCC = "", string DisplayName = "")
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = string.IsNullOrEmpty(DisplayName) ? new MailAddress(EmailFrom) : new MailAddress(EmailFrom, DisplayName);
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
