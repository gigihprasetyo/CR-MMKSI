using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.BusinessFacade.PO;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain.Search;
using System.Net;
using System.Net.Http;
using KTB.DNET.BusinessFacade.SparePart;
using System.Net.Mail;
using System.Web.Configuration;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class SPBillingLogic
    {
        static string bodyTemplate = @"<FONT face=Arial size=1>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                            <tr>
                                            <td colspan=3 align=center><b>SPBilling Tidak Naik Ke D-NET </b></td>
                                            </tr>
                                            <tr>
                                            <td colspan=3 height=50></td>
                                            </tr>
                                            <tr>
                                            <td colspan=3 height=50>
                                            Dengan hormat,&nbsp;
                                            <br><br>Berikut daftar SPBilling yang gagal Naik ke D-Net:
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan=3 height=10></td>
                                            </tr>

                                            {0}

                                            <tr>
                                            <td colspan=5 height=10></td>
                                            </tr>

                                            </table>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>

                                            <tr>
                                            <td width='100%'>Terimakasih</td>
                                            </tr>

                                            <tr>
                                            <td >&nbsp;</td>
                                            </tr>

                                            <tr>
                                            </tr>
                                            </table>
                                            </FONT>";
       
        public static async Task WSResend_SPBilling()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();
            SparePartBillingFacade func = new SparePartBillingFacade(User);
            List<string> listFail = new List<string>();
            List<string> listWLog = func.GetWsLog();
            StringBuilder bValue = new StringBuilder();


            foreach (var iLogBody in listWLog)
            {
                string condition = string.Empty;
                int countBillingNumber = 0;
                string[] lines = iLogBody.Split(new string[] { @"\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var iLines in lines)
                {
                    if (iLines.StartsWith("H;"))
                    {
                        condition += "'"+ iLines.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)[1]+"', ";
                        countBillingNumber += 1;
                    }
                    if (iLines.StartsWith("K;"))
                    {
                        string bodyValue = "<tr> <td height=10 width='25%'>{0}</td></tr>";
                        if (bValue.ToString().IndexOf(string.Format(bodyValue,iLines)) == -1)
                        {
                            bValue.Append(string.Format(bodyValue, iLines));
                        }
                        
                    }
                }
                if (!string.IsNullOrEmpty(condition))
                {
                    condition = condition.Remove(condition.Length - 2, 2);
                    if (countBillingNumber > func.GetCountData(string.Format("({0})",condition)))
                    {
                        listFail.Add(iLogBody);
                    }
                }
            }

            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string WSLink = appConfigFacade.Retrieve("WsLinkDsk").Value;
            string WSPass = appConfigFacade.Retrieve("WSPass").Value;
            string WSEmailTo = appConfigFacade.Retrieve("WSEmailTo").Value;
            string WSEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"] ;


            foreach (string iWlogFail in listFail)
            {
                var content = new StringContent(iWlogFail);
                client.DefaultRequestHeaders.Add("x-pass-header", WSPass);
                var response = await client.PostAsync(WSLink, content);
            }

            if (listFail.Count > 0)
            {
                string body = string.Format(bodyTemplate, bValue.ToString());
                SendEmail(body, WSEmailFrom, WSEmailTo);
            }
            
        }

        public static void SendEmail(string htmlString, string EmailFrom, string EmailTo)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(EmailFrom);
                string[] EmailToList = EmailTo.Split(';');
                foreach (string em in EmailToList)
                {
                    message.To.Add(new MailAddress(em));
                }

                message.Subject = "SPBilling Upload Fail";
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
