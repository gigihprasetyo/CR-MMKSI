using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using KTB.DNET.BusinessFacade;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Web.Configuration;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class ServiceDueDateReminderLogic
    {

        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
        public static void SendMail()
        {
            try
            {
                string WSEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"];
                string bodyValue = " <tr> <td width='40' align='RIGHT'>{0}</td> <td width='150' align='LEFT'>{1}</td> <td width='150' align='LEFT'>{2}</td> <td width='150' align='center'>{3}</td> <td width='150' align='center'>{4}</td> <td width='100' align='RIGHT'>{5}</td> <td width='100' align='RIGHT'>{6}</td> <td width='100' align='RIGHT'>{7}</td> <td width='100' align='RIGHT'>{8}</td> <td width='100' align='RIGHT'>{9}</td> </tr>";

                ServiceDueDateNotification Data = new ServiceDueDateNotification();
                List<MSPExPayment> UnCompletePayment = Data.RetrieveUnCompletePayment;

                if (UnCompletePayment.Count > 0)
                {
                    try
                    {
                        List<MSPExPayment> dealerList = UnCompletePayment.DistinctBy(x => x.Dealer.ID).ToList();
                        if (dealerList.Count > 0)
                        {
                            foreach (MSPExPayment item in dealerList)
                            {
                                StringBuilder bValue = new StringBuilder();
                                List<MSPExPayment> UnCompletePaymentDealer = Data.RetrieveUnCompletePaymentByDealer(item.Dealer);
                                int varNo = 1;

                                foreach (MSPExPayment paymentData in UnCompletePaymentDealer)
                                {
                                    //{0} = No
                                    //{1} = Kode Dealer
                                    //{2} = Credit Account
                                    //{3} = Tgl Rencana Transfer
                                    //{4} = No Registrasi Pembayaran
                                    //{5} = Total Amount
                                    //{6} = No Debit Charge
                                    //{7} = No Debit Memo
                                    //{8} = No Reg MSP Extended
                                    string dCharge = string.Empty;
                                    string dMemo = string.Empty;
                                    string nMSPEx = string.Empty;
                                    string nChassis = string.Empty;

                                    foreach (MSPExPaymentDetail detail in paymentData.MSPExPaymentDetails)
                                    {
                                        ArrayList arlDC = new MSPExDebitChargeFacade(User).RetrieveByRegistration(detail.MSPExRegistration);
                                        foreach (MSPExDebitCharge DC in arlDC)
                                        {
                                            if (dCharge.Trim().Length == 0)
                                            {
                                                dCharge = DC.DebitChargeNo;
                                            }
                                            else
                                            {
                                                dCharge = dCharge + ", " + DC.DebitChargeNo;
                                            }
                                        }

                                        ArrayList arlDM = new MSPExDebitMemoFacade(User).RetrieveByMSPRegistration(detail.MSPExRegistration);
                                        foreach (MSPExDebitMemo DM in arlDM)
                                        {
                                            if (dMemo.Trim().Length == 0)
                                            {
                                                dMemo = DM.DebitMemoNo;
                                            }
                                            else
                                            {
                                                dMemo = dMemo + ", " + DM.DebitMemoNo;
                                            }
                                        }

                                        if (nMSPEx.Trim().Length == 0)
                                        {
                                            nMSPEx = detail.MSPExRegistration.RegNumber;
                                        }
                                        else
                                        {
                                            nMSPEx = nMSPEx + ", " + detail.MSPExRegistration.RegNumber;
                                        }

                                        if (nChassis.Trim().Length == 0)
                                        {
                                            nChassis = detail.MSPExRegistration.ChassisMaster.ChassisNumber;
                                        }
                                        else
                                        {
                                            nChassis = nChassis + ", " + detail.MSPExRegistration.ChassisMaster.ChassisNumber;
                                        }
                                    }

                                    bValue.Append(string.Format(bodyValue, varNo,
                                                                            paymentData.Dealer.DealerCode.ToString(),
                                                                            paymentData.Dealer.CreditAccount.ToString(),
                                                                            paymentData.PlanTransferDate.ToString("dd/MM/yyyy"),
                                                                            paymentData.RegNumber.ToString(),
                                                                            "Rp. " + paymentData.TotalAmount.ToString("N0"),
                                                                            dCharge.ToString(),
                                                                            dMemo.ToString(),
                                                                            nMSPEx.ToString(),
                                                                            nChassis.ToString()
                                                                            ));
                                    varNo++;
                                }
                                string dealerCity = "";
                                if (item.Dealer.City != null)
                                {
                                    dealerCity = item.Dealer.City.CityName;
                                }

                                string body = string.Format(bodyTemplate, item.Dealer.DealerCode, item.Dealer.DealerName, dealerCity, bValue.ToString(), DateTime.Now.ToString("dd/MM/yyyy"), "Extended ", "Extended");
                                StandardCode stdCode = new StandardCodeFacade(User).GetByCategoryValueCode("EnumServiceEmailNotificationKind", "MspExtendedPayment");
                                ArrayList arlEmailDealer = new ServiceDueDateNotificationFacade(User).Retrieve(item.Dealer.DealerCode, stdCode.ValueId.ToString());
                                string emailTO = "";
                                string emailCC = "";
                                getEmail(arlEmailDealer, ref emailTO, ref emailCC);
                                SendEmail(body, "Reminder Pelunasan MSP Extended Payment", WSEmailFrom, emailTO, emailCC);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string cuk = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                string cuk = ex.Message;
            }
        }

        public static void SendMailDocumentServiceOutstanding()
        {

            try
            {
                string WSEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"];
                string bodyValue = " <tr> <td width='40' align='RIGHT'>{0}</td> <td width='150' align='LEFT'>{1}</td> <td width='150' align='LEFT'>{2}</td> <td width='150' align='center'>{3}</td> <td width='150' align='center'>{4}</td> <td width='100' align='RIGHT'>{5}</td> <td width='100' align='RIGHT'>{6}</td> " +
                " <td width='150' align='LEFT'>{7}</td> <td width='100' align='LEFT'>{8}</td> <td width='100' align='LEFT'>{9}</td> <td width='100' align='LEFT'>{10}</td> <td width='100' align='LEFT'>{11}</td> <td width='40' align='LEFT'>{12}</td> <td width='150' align='RIGHT'>{13}</td> <td width='40' align='LEFT'>{14}</td> <td width='300' align='LEFT'>{15}</td> <td width='100' align='LEFT'>{16}</td> <td width='100' align='LEFT'>{17}</td> </tr>";

                ServiceDueDateNotification Data = new ServiceDueDateNotification();
                List<MonthlyDocument> liOutStandingServiceDocument = Data.RetrieveOutStandingServiceDocument();

                if (liOutStandingServiceDocument.Count > 0)
                {
                    try
                    {
                        List<MonthlyDocument> dealerList = liOutStandingServiceDocument.DistinctBy(x => x.Dealer.ID).ToList();
                        if (dealerList.Count > 0)
                        {
                            foreach (MonthlyDocument item in dealerList)
                            {
                                StringBuilder bValue = new StringBuilder();
                                List<MonthlyDocument> liOutStandingServiceDocumentDealer = Data.RetrieveOutStandingServiceDocument(item.Dealer);
                                int varNo = 1;

                                foreach (MonthlyDocument MonthlyData in liOutStandingServiceDocumentDealer.DistinctBy(x => new { x.ProductCategory.AccountCode, x.ProductCategory.Code, x.AccountingNo, x.BillingNo, x.BillingDate }).ToList())
                                {
                                    //{0} = No
                                    //{1} = Kode Dealer
                                    //{2} = Account Code
                                    //{3} = Code
                                    //{4} = Accounting No
                                    //{5} = Billing No
                                    //{6} = Billing Date
                                    //{7} = Parked Name
                                    //{8} = Doc No
                                    //{9} = Reff
                                    //{10} = Assignment
                                    //{11} = Due Date
                                    //{12} = Year
                                    //{13} = Amount
                                    //{14} = Curr
                                    //{15} = Text
                                    //{16} = Dokument
                                    //{17} = Clearing
                                    string DocString = "";
                                    if (MonthlyData.Kind == 0 || MonthlyData.Kind == 10)
                                    {
                                        DocString = "DEPOSIT";
                                    }
                                    else if (MonthlyData.Kind == 1 || MonthlyData.Kind == 6 || MonthlyData.Kind == 7 || MonthlyData.Kind == 22)
                                    {
                                        DocString = "WSC";
                                    }
                                    else if (MonthlyData.Kind == 3 )
                                    {
                                        DocString = "PDI";
                                    }
                                    else if (MonthlyData.Kind == 2 || MonthlyData.Kind == 4 || MonthlyData.Kind == 5 || MonthlyData.Kind == 11 || MonthlyData.Kind == 12 || MonthlyData.Kind == 13 || MonthlyData.Kind == 14
                                        || MonthlyData.Kind == 15 || MonthlyData.Kind == 19 || MonthlyData.Kind == 23)
                                    {
                                        DocString = "FS";
                                    }
                                    else if (MonthlyData.Kind == 8)
                                    {
                                        DocString = "PM";
                                    }


                                    bValue.Append(string.Format(bodyValue, varNo,
                                                                            MonthlyData.Dealer.DealerCode.ToString(),
                                                                            MonthlyData.ProductCategory.AccountCode.ToString(),
                                                                            MonthlyData.ProductCategory.Code.ToString(),
                                                                            MonthlyData.AccountingNo.ToString(),
                                                                            MonthlyData.BillingNo.ToString(),
                                                                            MonthlyData.BillingDate.ToString("dd/MM/yyyy"),
                                                                            MonthlyData.ParkedName.ToString(),
                                                                            MonthlyData.AccountingNo.ToString(),
                                                                            MonthlyData.BillingNo.ToString(),
                                                                            MonthlyData.BillingDate.ToString("yyyyMMdd"),
                                                                            MonthlyData.BillingDate.ToString("dd/MM/yyyy"),
                                                                            MonthlyData.PeriodeYear.ToString(),
                                                                            MonthlyData.Amount.ToString("#,###"),
                                                                            MonthlyData.Currencies.ToString(),
                                                                            MonthlyData.Description.ToString(),
                                                                            DocString,
                                                                            MonthlyData.NoClearing.ToString()
                                                                            ));
                                    varNo++;
                                }

                                string dealerCity = "";
                                if (item.Dealer.City != null)
                                {
                                    dealerCity = item.Dealer.City.CityName;
                                }

                                string body = string.Format(bodyTemplateOutstanding, item.Dealer.DealerCode, item.Dealer.DealerName, dealerCity, bValue.ToString(), DateTime.Now.ToString("dd/MM/yyyy"));
                                StandardCode stdCode = new StandardCodeFacade(User).GetByCategoryValueCode("EnumServiceEmailNotificationKind", "DocumentServiceOutstanding");
                                ArrayList arlEmailDealer = new ServiceDueDateNotificationFacade(User).Retrieve(item.Dealer.DealerCode, stdCode.ValueId.ToString());
                                string emailTO = "";
                                string emailCC = "";
                                getEmail(arlEmailDealer, ref emailTO, ref emailCC);
                                SendEmail(body, "Reminder Pelunasan Outstanding Nomor Accounting", WSEmailFrom, emailTO, emailCC);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string cuk = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                string cuk = ex.Message;
            }
        }

        public static void SendMailMSPRegular()
        {
            try
            {
                string WSEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"];
                string bodyValue = " <tr> <td width='40' align='RIGHT'>{0}</td> <td width='150' align='LEFT'>{1}</td> <td width='150' align='LEFT'>{2}</td> <td width='150' align='center'>{3}</td> <td width='150' align='center'>{4}</td> <td width='100' align='RIGHT'>{5}</td> <td width='100' align='RIGHT'>{6}</td> <td width='100' align='RIGHT'>{7}</td> <td width='100' align='RIGHT'>{8}</td> <td width='100' align='RIGHT'>{9}</td> </tr>";

                ServiceDueDateNotification Data = new ServiceDueDateNotification();
                List<MSPTransferPayment> UnCompletePayment = Data.RetrieveUnCompleteRegularPayment;

                if (UnCompletePayment.Count > 0)
                {
                    try
                    {
                        List<MSPTransferPayment> dealerList = UnCompletePayment.DistinctBy(x => x.Dealer.ID).ToList();
                        if (dealerList.Count > 0)
                        {
                            foreach (MSPTransferPayment item in dealerList)
                            {
                                StringBuilder bValue = new StringBuilder();
                                List<MSPTransferPayment> UnCompletePaymentDealer = Data.RetrieveUnCompleteRegularPaymentByDealer(item.Dealer);
                                int varNo = 1;

                                foreach (MSPTransferPayment paymentData in UnCompletePaymentDealer)
                                {
                                    //{0} = No
                                    //{1} = Kode Dealer
                                    //{2} = Credit Account
                                    //{3} = Tgl Rencana Transfer
                                    //{4} = No Registrasi Pembayaran
                                    //{5} = Total Amount
                                    //{6} = No Debit Charge
                                    //{7} = No Debit Memo
                                    //{8} = No Reg MSP Extended
                                    string dCharge = string.Empty;
                                    string dMemo = string.Empty;
                                    string nMSPEx = string.Empty;
                                    string nChassis = string.Empty;


                                    foreach (MSPTransferPaymentDetail detail in paymentData.MSPTransferPaymentDetails)
                                    {
                                        ArrayList arlDC = new MSPDCFacade(User).RetrieveByRegistration(detail.MSPRegistrationHistory.MSPRegistration);
                                        foreach (MSPDC DC in arlDC)
                                        {
                                            if (dCharge.Trim().Length == 0)
                                            {
                                                dCharge = DC.DebitChargeNo;
                                            }
                                            else
                                            {
                                                dCharge = dCharge + ", " + DC.DebitChargeNo;
                                            }
                                        }

                                        ArrayList arlDM = new MSPDMFacade(User).RetrieveByMSPRegistration(detail.MSPRegistrationHistory.MSPRegistration);
                                        foreach (MSPDM DM in arlDM)
                                        {
                                            if (dMemo.Trim().Length == 0)
                                            {
                                                dMemo = DM.DebitMemoNo;
                                            }
                                            else
                                            {
                                                dMemo = dMemo + ", " + DM.DebitMemoNo;
                                            }
                                        }

                                        if (nMSPEx.Trim().Length == 0)
                                        {
                                            nMSPEx = detail.MSPRegistrationHistory.MSPRegistration.MSPCode;
                                        }
                                        else
                                        {
                                            nMSPEx = nMSPEx + ", " + detail.MSPRegistrationHistory.MSPRegistration.MSPCode;
                                        }

                                        if (nChassis.Trim().Length == 0)
                                        {
                                            nChassis = detail.MSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber;
                                        }
                                        else
                                        {
                                            nChassis = nChassis + ", " + detail.MSPRegistrationHistory.MSPRegistration.ChassisMaster.ChassisNumber;
                                        }
                                    }

                                    bValue.Append(string.Format(bodyValue, varNo,
                                                                            paymentData.Dealer.DealerCode.ToString(),
                                                                            paymentData.Dealer.CreditAccount.ToString(),
                                                                            paymentData.PlanTransferDate.ToString("dd/MM/yyyy"),
                                                                            paymentData.RegNumber.ToString(),
                                                                            "Rp. " + paymentData.TotalAmount.ToString("N0"),
                                                                            dCharge.ToString(),
                                                                            dMemo.ToString(),
                                                                            nMSPEx.ToString(),
                                                                            nChassis.ToString()
                                                                            ));
                                    varNo++;
                                }
                                string dealerCity = "";
                                if (item.Dealer.City != null)
                                {
                                    dealerCity = item.Dealer.City.CityName;
                                }

                                string body = string.Format(bodyTemplate, item.Dealer.DealerCode, item.Dealer.DealerName, dealerCity, bValue.ToString(), DateTime.Now.ToString("dd/MM/yyyy"), "Payment ", "");
                                StandardCode stdCode = new StandardCodeFacade(User).GetByCategoryValueCode("EnumServiceEmailNotificationKind", "MspPayment");
                                ArrayList arlEmailDealer = new ServiceDueDateNotificationFacade(User).Retrieve(item.Dealer.DealerCode, stdCode.ValueId.ToString());
                                string emailTO = "";
                                string emailCC = "";
                                getEmail(arlEmailDealer, ref emailTO, ref emailCC);
                                SendEmail(body, "Reminder Pelunasan MSP Payment", WSEmailFrom, emailTO, emailCC);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string cuk = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                string cuk = ex.Message;
            }
        }


        public static void SendEmail(string htmlString, string subject, string EmailFrom, string EmailTo, string EmailCC = "")
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
                if (EmailCC.Length > 0)
                {
                    string[] EmailCCList = EmailCC.Split(';');
                    foreach (string em in EmailCCList)
                    {
                        message.CC.Add(new MailAddress(em));
                    }
                }

                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                string SmtpAddress = WebConfigurationManager.AppSettings["SMTP"];
                SmtpClient SmtpServer = new SmtpClient(SmtpAddress);
                SmtpServer.Port = 25;
                SmtpServer.Send(message);
            }
            catch (Exception e)
            {
                string cuk = e.Message;
            }
        }

        private static void getEmail(ArrayList Data, ref string emailTO, ref string emailCC)
        {
            foreach (ServiceDueDateNotification item in Data)
            {
                if (item.PositionRecipient.ToLower() == "to")
                {
                    if (emailTO.Length == 0)
                    {
                        emailTO = item.EmailDealer;
                    }
                    else
                    {
                        emailTO = emailTO + ";" + item.EmailDealer;
                    }
                }
                else if (item.PositionRecipient.ToLower() == "cc")
                {
                    if (emailCC.Length == 0)
                    {
                        emailCC = item.EmailDealer;
                    }
                    else
                    {
                        emailCC = emailCC + ";" + item.EmailDealer;
                    }
                }
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        #region Template MSP
        static string bodyTemplate = @"<table border='0' cellspacing='2' cellpadding='0' width='800'>
                                        <tr>
                                            <td>
                                                <table id='TblHeader' border='0' cellspacing='0' cellpadding='0' width='400'>
                                                    <tr>
                                                        <td width='100'>Kode Dealer</td>
                                                        <td width='1'>:&nbsp;</td>
                                                        <td>{0}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Nama Dealer</td>
                                                        <td>:&nbsp;</td>
                                                        <td>{1}</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Kota</td>
                                                        <td>:&nbsp;</td>
                                                        <td>{2}</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='COLOR: blue'></td>
                                        </tr>
                                        <tr>
                                            <td style='COLOR: blue'></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Dengan hormat,<br>
                                                Berikut MSP {5}yang akan atau telah Jatuh Tempo :<br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id='TblData' border='1' cellspacing='0' cellpadding='1'>
                                                    <tr>
                                                        <td width='40' align='center'><b>No</b></td>
                                                        <td width='150' align='center'><b>Kode Dealer</b></td>
                                                        <td width='150' align='center'><b>Credit Account</b></td>
                                                        <td width='150' align='center'><b>Tgl Rencana Transfer</b></td>
                                                        <td width='150' align='center'><b>No Registrasi Pembayaran</b></td>
                                                        <td width='150' align='center'><b>Total Amount</b></td>
                                                        <td width='100' align='center'><b>No Debit Charge</b></td>
                                                        <td width='100' align='center'><b>No Debit Memo</b></td>
                                                        <td width='100' align='center'><b>No Reg MSP {6}</b></td>
                                                        <td width='100' align='center'><b>No Rangka</b></td>
                                                    </tr>
                                                    {3}
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='COLOR: blue'></td>
                                        </tr>
                                        <tr>
                                            <td style='COLOR: blue'>
                                                <b>
                                                    Silahkan lakukan pembayaran atas MSP {5}Tersebut*.

                                                </b>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style='COLOR: black'>
                                                <i>
                                                    *)Silahkan abaikan Email ini jika sudah melakukan Pembayaran

                                                </i>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='COLOR: blue'></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Terima kasih atas perhatian dan kerja samanya.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='COLOR: blue'></td>
                                        </tr>
                                        <tr>
                                            <td style='COLOR: blue'></td>
                                        </tr>
                                        <tr>
                                            <td align='right'>
                                                <table id='TblFooter' border='0' cellspacing='0' cellpadding='0' width='400'>
                                                    <tr>
                                                        <td align='RIGHT'>Jakarta, {4}</td>
                                                    </tr>
                                                    <tr>
                                                        <td align='center'></td>
                                                    </tr>
                                                    <tr>
                                                        <td align='center'><u><u></u></u></td>
                                                    </tr>
                                                    <tr>
                                                        <td align='center'></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                    </table>";
        #endregion

        #region Template Outstanding
        static string bodyTemplateOutstanding = @"<table border='0' cellspacing='2' cellpadding='0' width='800'>
                                                <tr>
                                                    <td>
                                                        <table id='TblHeader' border='0' cellspacing='0' cellpadding='0' width='400'>
                                                            <tr>
                                                                <td width='100'>Kode Dealer</td>
                                                                <td width='1'>:&nbsp;</td>
                                                                <td>{0}</td>
                                                            </tr>
                                                            <tr>
                                                                <td>Nama Dealer</td>
                                                                <td>:&nbsp;</td>
                                                                <td>{1}</td>
                                                            </tr>
                                                            <tr>
                                                                <td>Kota</td>
                                                                <td>:&nbsp;</td>
                                                                <td>{2}</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style='COLOR: blue'></td>
                                                </tr>
                                                <tr>
                                                    <td style='COLOR: blue'></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Dengan hormat,<br>
                                                        Berikut Nomor Accounting yang telah Outstanding :<br>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id='TblData' border='1' cellspacing='0' cellpadding='1'>
                                                            <tr>
                                                                <td width='40' align='center'><b>No</b></td>
                                                                <td width='150' align='center'><b>Kode Dealer</b></td>
                                                                <td width='150' align='center'><b>Account Code</b></td>
                                                                <td width='150' align='center'><b>Code</b></td>
                                                                <td width='150' align='center'><b>Accounting No</b></td>
                                                                <td width='150' align='center'><b>Billing No</b></td>
                                                                <td width='100' align='center'><b>Billing Date</b></td>
                                                                <td width='150' align='center'><b>Parked Name</b></td>
                                                                <td width='150' align='center'><b>Doc No</b></td>
                                                                <td width='100' align='center'><b>Reff</b></td>
                                                                <td width='150' align='center'><b>Assignment</b></td>
                                                                <td width='100' align='center'><b>Due Date</b></td>
                                                                <td width='100' align='center'><b>Year</b></td>
                                                                <td width='150' align='right'><b>Amount</b></td>
                                                                <td width='70' align='center'><b>Curr</b></td>
                                                                <td width='200' align='left'><b>Text</b></td>
                                                                <td width='150' align='left'><b>Dokumen</b></td>
                                                                <td width='150' align='center'><b>Clearing</b></td>
                                                            </tr>
                                                            {3}
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style='COLOR: blue'></td>
                                                </tr>
                                                <tr>
                                                    <td style='COLOR: blue'>
                                                        <b>
                                                            Silahkan lakukan pembayaran atas Outstanding Tersebut*.

                                                        </b>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style='COLOR: black'>
                                                        <i>
                                                            *)Silahkan abaikan Email ini jika sudah melakukan Pembayaran

                                                        </i>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style='COLOR: blue'></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Terima kasih atas perhatian dan kerja samanya.
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style='COLOR: blue'></td>
                                                </tr>
                                                <tr>
                                                    <td style='COLOR: blue'></td>
                                                </tr>
                                                <tr>
                                                    <td align='right'>
                                                        <table id='TblFooter' border='0' cellspacing='0' cellpadding='0' width='400'>
                                                            <tr>
                                                                <td align='RIGHT'>Jakarta, {4}</td>
                                                            </tr>
                                                            <tr>
                                                                <td align='center'></td>
                                                            </tr>
                                                            <tr>
                                                                <td align='center'><u><u></u></u></td>
                                                            </tr>
                                                            <tr>
                                                                <td align='center'></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                            </table>";
        #endregion
    }
}
