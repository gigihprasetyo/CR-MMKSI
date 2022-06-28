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

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class DiscountProposalLogic
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);

        public static void SendMail()
        {
            try
            {
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);                
                string WSEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"]; ;
                
                DataTable dt = new DataTable();
                dt = DT_ForSendEmail();

                string _body = string.Empty;
                string _wSEmailTo = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    _body = dr["EmailBody"].ToString();
                    _wSEmailTo = dr["SendTo"].ToString();
                    if (!string.IsNullOrWhiteSpace(_body) && !string.IsNullOrWhiteSpace(_wSEmailTo))
                    {
                        if (DoSendEmail(_body, WSEmailFrom, _wSEmailTo))
                        {
                            UpdateStatusSendMail(int.Parse(dr["StatusChangeHistoryID"].ToString()));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string cuk = e.Message;
            }
        }

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            DiscountProposalHeaderFacade DPHFac = new DiscountProposalHeaderFacade(User);
            int _result = new int();

            if (e.Cancelled)
            {
                _result = DPHFac.InsertDataLogForSendMail("Send canceled", token, 0);
            }
            if (e.Error != null)
            {
                _result = DPHFac.InsertDataLogForSendMail(e.Error.ToString(), token, 0);
            }
            else
            {
                _result = DPHFac.InsertDataLogForSendMail("Message succes sent.", token, 1);
            }
            mailSent = true;
        }

        public static void UpdateStatusSendMail(int statusChangeHistoryID)
        {
            if (!string.IsNullOrEmpty(statusChangeHistoryID.ToString()))
            {
                StatusChangeHistorySendEmailFlagFacade SCHFac = new StatusChangeHistorySendEmailFlagFacade(User);
                CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(StatusChangeHistorySendEmailFlag), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit.opAnd(new Criteria(typeof(StatusChangeHistorySendEmailFlag), "StatusChangeHistory.id", MatchType.Exact, statusChangeHistoryID));
                StatusChangeHistorySendEmailFlag obj = SCHFac.Retrieve(crit).Cast<StatusChangeHistorySendEmailFlag>().FirstOrDefault();
                if (obj != null)
                {
                    obj.IsSendEmail = 1;
                    int result = SCHFac.Update(obj);
                }
            }
        }

        public static Boolean DoSendEmail(string bodyMail, string EmailFrom, string EmailTo)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(EmailFrom);

                string strBcc = string.Empty;
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);                
                CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                crit.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "EmailBcc_DP"));
                AppConfig obj = appConfigFacade.Retrieve(crit).Cast<AppConfig>().FirstOrDefault();
                if (obj != null)
                {
                    string[] arrBcc = obj.Value.Split(';');
                    foreach(string bcc in arrBcc)
                    {
                        message.Bcc.Add(bcc);
                    }
                }
                else
                {
                    message.Bcc.Add(strBcc);
                }
                string[] EmailToList = EmailTo == "" ? new string[] { } : EmailTo.Split(';');
                foreach (string em in EmailToList)
                {
                    message.To.Add(new MailAddress(em));
                }

                if (EmailToList.Length > 0)
                {
                    message.Subject = "Discount Proposal";
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = bodyMail;
                    string SmtpAddress = WebConfigurationManager.AppSettings["SMTP"];
                    SmtpClient SmtpServer = new SmtpClient(SmtpAddress);
                    SmtpServer.Port = 25;
                    SmtpServer.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                    string userState = bodyMail;
                    SmtpServer.SendAsync(message, userState);
                    //SmtpServer.Send(message);
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
            DiscountProposalHeaderFacade DPHFac = new DiscountProposalHeaderFacade(User);
            DataTable result = DPHFac.RetrieveDataTableForSendMail();
            return result;
        }

        
        private static string bodyTemplate = @"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='widtd=device-widtd, initial-scale=1.0'>
    <title>Document</title>
    <style>
        .table1, .table2{
            border-collapse: collapse;
        }
        .table2 {
            width: 1000px;
        }
        .table1 th{
            border: 1px solid black;
            background-color: lightskyblue;
            padding-left: 20px;
            padding-right: 20px;
        }
        .table2 th{
            border: 1px solid black;
            background-color: lightskyblue;
            padding-left: 5px;
            padding-right: 0px;
            text-align: left;
        }
        .table1 td, .table2 td{
            border: 1px solid black;
            padding-left: 5px;
            padding-right: 5px;
        }
        .table1 .text-center, .table2 .text-center {
            text-align: center;
        }
        .table1 .text-right, .table2 .text-right {
            text-align: right;
        }

        .section-title{
            font-weight: bold;
        }

        body {
            font-family: Calibri;
            font-size: 0.9em;
        }
    </style>
</head>
<body>
    <section>&nbsp;Dear Sirs/Madam</section>
    <br>
    <section>
        &nbsp;Aplikasi Program Fleet Customer dengan detail dibawah ini telah diajukan ke MMKSI :
    </section>
    <section>
        <table>
            <tr>
                <td>Tanggal Pengajuan</td>
                <td>:</td>
                <td>Selasa, 4 Agustus 2020</td>
            </tr>
            <tr>
                <td>Dealer</td>
                <td>:</td>
                <td>100074 - BRA LT.METEN JKT</td>
            </tr>
            <tr>
                <td>No. Aplikasi Dealer</td>
                <td>:</td>
                <td>015/BRA/DP/2020</td>
            </tr>
            <tr>
                <td>Nama Fleet Customer</td>
                <td>:</td>
                <td>PT. CSM CORPORATAMA</td>
            </tr>
            <tr>
                <td>Waktu Pengiriman</td>
                <td>:</td>
                <td>May 2020</td>
            </tr>
            <tr>
                <td>Laporan PO/SPK</td>
                <td>:</td>
                <td>PO Pembelian Kendaraan 1.pdf</td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>PO Pembelian Kendaraan 1.pdf</td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>PO Pembelian Kendaraan 1.pdf</td>
            </tr>
            <tr>
                <td>Submision Status</td>
                <td>:</td>
                <td>Baru</td>
            </tr>
        </table>
    </section>
    <br><br>
    <section>
        
        <div class='section-title'>Detail Kendaraan</div>
        <table class='table1'>
            <tr>
                <th>No</th>
                <th>Model</th>
                <th>Tipe</th>
                <th>Warna</th>
                <th>Assy Year</th>
                <th>Model Year</th>
                <th>Unit</th>
                <th style='width: 120px;'>Permohonan Diskon (per unit)</th>
            </tr>
            <tr>
                <td class='text-center'>1</td>
                <td>PHEV</td>
                <td>OUTLENDER PHEV 2.4L ULT (4x4) A/T (AB10/AB11)</td>
                <td>Hitam Mutiara</td>
                <td class='text-center'>2020</td>
                <td class='text-center'>-</td>
                <td class='text-center'>4</td>
                <td class='text-right'>9.000.000</td>
            </tr>
        </table>
    </section>
    <br>
    <section>
        <div class='section-title'>Rincian Kendaraan</div>
        <table class='table2'>
            <tr>
                <th colspan='3'>Keterangan Kendaraan :</th>
            </tr>
            <tr>
                <td style='width: 300px;'>Model</td>
                <td style='width: 200px;' class='text-center'>XPANDER</td>
                <td style='width: 200px;' class='text-center'>PHEV</td>
            </tr>
            <tr>
                <td>Tipe</td>
                <td style='width: 200px;' class='text-center'>XPANDER 1.5L SPORT-L (4x2) M/T (NH20/NH21)</td>
                <td style='width: 200px;' class='text-center'>OUTLANDER PHEV 2.4L ULT (4x4) A/T (AB10/AB11)</td>
            </tr>
            <tr>
                <th colspan='3'>Cost Dealer :</th>
            </tr>
            <tr>
                <td style='width: 300px;'>Harga Tebus</td>
                <td style='width: 200px;' class='text-center'>310000000</td>
                <td style='width: 200px;' class='text-center'>330000000</td>
            </tr>
            <tr>
                <td style='width: 300px;'>Logistic Cost</td>
                <td style='width: 200px;' class='text-center'>2000000</td>
                <td style='width: 200px;' class='text-center'>2500000</td>
            </tr>
        </table>

    </section>
    <br><br>
    <footer>
        <article>Best Regard,</article>
        <br>
        <article>D-NET MMKSI</article>
        <br>
        <article>Note : This email is auto generated by DNET System</article>
    </footer>
</body>
</html>
";

    }
}
