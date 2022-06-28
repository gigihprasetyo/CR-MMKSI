using DNET.Monitoring.DAL;
using KTB.DNet.Web.Scheduling.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace KTB.DNet.Web.Scheduling.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        static DBConnection strConnString;

        //public ActionResult Index()
        //{
        //    return View();
        //}
        private static void setConn()
        {
            strConnString = new DBConnection();
            strConnString.DataSource = WebConfigurationManager.AppSettings["DataSource"];
            strConnString.DBName = WebConfigurationManager.AppSettings["DBName"];
            strConnString.UserName = WebConfigurationManager.AppSettings["UserName"];
            strConnString.Password = WebConfigurationManager.AppSettings["Password"];
            strConnString.DBType = EnumDBType.DBType.SqlServer;
        }

        public static void SendSchedulerReporting()
        {
            setConn();

            DataTable dt = new DataTable();

            using (GlobalDatabase db = new GlobalDatabase(strConnString))
            {
                String strQuery = "up_GetJobData";
                #region body
                string EmailBody = @"<!DOCTYPE html>
                                    <html>
                                        <head></head>
                                        <body>
                                            Dear DNet Team,
                                            </br></br>
                                            Berikut status scheduler hangfire pada tanggal {0}
                                            </br></br>                                      
                                            <table class=""table"" cellspacing=""1"" cellpadding=""1"" width=""100%"" border=""1"">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            No
                                                        </th>
                                                        <th>
                                                            Job Type
                                                        </th>
                                                        <th>
                                                            Status
                                                        </th>
                                                        <th>
                                                            Jam (GMT+0)
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {1}
                                                </tbody>
                                            </table>
                                        </body>
                                    </html>";
                #endregion
                dt = db.ExecuteDataTable(strQuery, CommandType.StoredProcedure);

                string Template = @"<tr>
	                                        <td>
		                                        {0}
	                                        </td>
	                                        <td>
		                                        {1}
	                                        </td>
	                                        <td>
		                                        {2}
	                                        </td>
                                            <td>
		                                        {3}
	                                        </td>
                                        </tr>";
                string GeneratedTable = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string JobType = dt.Rows[i]["JobType"].ToString();
                        string StateName = dt.Rows[i]["StateName"].ToString();
                        string SchedulerDate = dt.Rows[i]["CreatedAt"].ToString();
                        int CutIndex = JobType.IndexOf(",");
                        JobType = JobType.Substring(0, CutIndex);
                        GeneratedTable = GeneratedTable + string.Format(Template, i + 1, JobType, StateName, SchedulerDate);
                    }
                    //oEmail.JobType = dt.Rows[0]["JobType"].ToString();
                    //oEmail.StateName = dt.Rows[0]["StateName"].ToString();
                }
                string datenow = DateTime.Now.ToString("dd/MM/yyyy");
                EmailBody = string.Format(EmailBody, datenow, GeneratedTable);
                SendEmail(EmailBody);
            }
        }

        public static void SendEmail(string htmlString)
        {
            try
            {
                string EmailFrom = WebConfigurationManager.AppSettings["EmailFrom"];
                string EmailTo = WebConfigurationManager.AppSettings["EmailTo"];
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(EmailFrom);
                string[] EmailToList = EmailTo.Split(';');
                foreach (string em in EmailToList)
                {
                    message.To.Add(new MailAddress(em));
                }

                //message.To.Add(new MailAddress("selly.isamuddin@bsi.co.id"));
                message.Subject = "Scheduler Report";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                string SmtpAddress = WebConfigurationManager.AppSettings["SMTP"];
                SmtpClient SmtpServer = new SmtpClient(SmtpAddress);
                SmtpServer.Port = 25;
                SmtpServer.Send(message);
            }
            catch (Exception e){
                string cuk = e.Message;
            }
        }    
    }
}