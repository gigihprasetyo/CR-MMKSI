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
    public static class CBUReturnReminder
    {

        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
        public static void SendMail()
        {
            try
            {
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);
                string WSEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"]; ;

                string CBUReturnEmailToRSD = appConfigFacade.Retrieve("CBUReturnEmailToRSD").Value;
                string CBUReturnEmailToWSD = appConfigFacade.Retrieve("CBUReturnEmailToWSD").Value;
                string CBUReturnEmailToVCD = appConfigFacade.Retrieve("CBUReturnEmailToVCD").Value;

                ChassisMasterClaimEmailQueue Data = new ChassisMasterClaimEmailQueue();
                List<ChassisMasterClaimEmailQueue> DataRSD = Data.RetrieveUnsendToRSD;
                List<ChassisMasterClaimEmailQueue> DataWSD = Data.RetrieveUnsendToWSD;
                List<ChassisMasterClaimEmailQueue> DataVCD = Data.RetrieveUnsendToVCD;
                List<ChassisMasterClaimDMSReminder> DataDMS = new ChassisMasterClaimDMSReminder().RetrieveUnsendData;
                string bodyValue = "<tr><td height='10'>{0}</td><td height='10'>{1}</td><td height='10'>{2}</td><td height='10'>{3}</td><td height='10'>{4}</td></tr>";
                string bodyValueDMS = "<tr><td style=\"text-align: center; vertical-align: middle;\">{0}</td><td style=\"text-align: center; vertical-align: middle;\">{1}</td><td style=\"text-align: center; vertical-align: middle;\">{2}</td></tr>";
                // Nomor Claim - Dealer - Status - Tanggal Input Claim - Tanggal Update Transaksi

                if (DataRSD.Count > 0)
                {
                    try
                    {
                        StringBuilder bValue = new StringBuilder();
                        foreach (ChassisMasterClaimEmailQueue data in DataRSD)
                        {
                            ChassisMasterClaimHeader cHeader = new ChassisMasterClaimHeaderFacade(User).Retrieve(data.ClaimNumber);
                            if (cHeader.ID == 0)
                            {
                                continue;
                            }
                            string _ClaimNumber = data.ClaimNumber;
                            string _Dealer = cHeader.Dealer.SearchTerm1;
                            EnumCBUReturn.StatusProsesRetur _Status = (EnumCBUReturn.StatusProsesRetur)data.StatusReturnProcess;
                            string _ClaimDate = cHeader.ClaimDate.ToString("dd/MM/yyyy HH:mm:ss");
                            string _ClaimUpdate = data.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss");
                            bValue.Append(string.Format(bodyValue, _ClaimNumber, _Dealer, _Status.ToString().Replace("_", " "), _ClaimDate, _ClaimUpdate));
                        }

                        string body = string.Format(bodyTemplate, bValue.ToString());
                        SendEmail(body, WSEmailFrom, CBUReturnEmailToRSD);
                    }
                    catch (Exception ex)
                    {
                        string cuk = ex.Message;
                    }
                    finally
                    {
                        UpdateQueue(DataRSD);
                    }
                }

                if (DataWSD.Count > 0)
                {
                    try
                    {
                        StringBuilder bValue = new StringBuilder();
                        foreach (ChassisMasterClaimEmailQueue data in DataWSD)
                        {
                            ChassisMasterClaimHeader cHeader = new ChassisMasterClaimHeaderFacade(User).Retrieve(data.ClaimNumber);
                            if (cHeader.ID == 0)
                            {
                                continue;
                            }
                            string _ClaimNumber = data.ClaimNumber;
                            string _Dealer = cHeader.Dealer.SearchTerm1;
                            EnumCBUReturn.StatusProsesRetur _Status = (EnumCBUReturn.StatusProsesRetur)data.StatusReturnProcess;
                            string _ClaimDate = cHeader.ClaimDate.ToString("dd/MM/yyyy HH:mm:ss");
                            string _ClaimUpdate = data.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss");
                            bValue.Append(string.Format(bodyValue, _ClaimNumber, _Dealer, _Status.ToString().Replace("_", " "), _ClaimDate, _ClaimUpdate));
                        }

                        string body = string.Format(bodyTemplate, bValue.ToString());
                        SendEmail(body, WSEmailFrom, CBUReturnEmailToWSD);
                    }
                    catch (Exception ex)
                    {
                        string cuk = ex.Message;
                    }
                    finally
                    {
                        UpdateQueue(DataWSD);
                    }
                }

                if (DataVCD.Count > 0)
                {
                    try
                    {
                        StringBuilder bValue = new StringBuilder();
                        foreach (ChassisMasterClaimEmailQueue data in DataVCD)
                        {
                            ChassisMasterClaimHeader cHeader = new ChassisMasterClaimHeaderFacade(User).Retrieve(data.ClaimNumber);
                            if (cHeader.ID == 0)
                            {
                                continue;
                            }
                            string _ClaimNumber = data.ClaimNumber;
                            string _Dealer = cHeader.Dealer.SearchTerm1;
                            EnumCBUReturn.StatusClaim _Status = (EnumCBUReturn.StatusClaim)data.StatusClaim;
                            string _ClaimDate = cHeader.ClaimDate.ToString("dd/MM/yyyy HH:mm:ss");
                            string _ClaimUpdate = data.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss");
                            bValue.Append(string.Format(bodyValue, _ClaimNumber, _Dealer, _Status.ToString().Replace("_", " "), _ClaimDate, _ClaimUpdate));
                        }

                        string body = string.Format(bodyTemplate, bValue.ToString());
                        SendEmail(body, WSEmailFrom, CBUReturnEmailToVCD);
                    }
                    catch (Exception ex)
                    {
                        string cuk = ex.Message;
                    }
                    finally
                    {
                        UpdateQueue(DataVCD);
                    }
                }

                if (DataDMS.Count > 0)
                {
                    try
                    {
                        //List<ChassisMasterClaimDMSReminder> OrderedDataDMS = DataDMS.OrderBy(y => y.ChassisMaster.Dealer.ID).ToList();
                        foreach (int item in DataDMS.Select(m => m.ChassisMaster.Dealer.ID).Distinct().ToList())
                        {
                            StringBuilder bValue = new StringBuilder();
                            int i = 1;
                            List<ChassisMasterClaimDMSReminder> FilteredData = DataDMS
                                                                                .Where(y => y.ChassisMaster.Dealer.ID == item)
                                                                                .Select(y => y).ToList();
                            Dealer oDealer = FilteredData.Select(y => y.ChassisMaster.Dealer).FirstOrDefault();
                            ArrayList ArrContact = new ContactDealerFacade(User).Retrieve(oDealer.DealerCode);
                            string emailTO = "";
                            string emailCC = "";
                            getEmail(ArrContact, ref emailTO, ref emailCC);
                            foreach (ChassisMasterClaimDMSReminder data in FilteredData)
                            {
                                ChassisMasterClaimDMSReminder cHeader = new ChassisMasterClaimDMSReminderFacade(User).Retrieve(data.ChassisMaster.ChassisNumber);
                                if (cHeader.ID == 0)
                                {
                                    continue;
                                }
                                bValue.Append(string.Format(bodyValueDMS, i, cHeader.ChassisMaster.ChassisNumber, cHeader.ChassisMaster.Dealer.SearchTerm1));
                                i++;
                            }
                            string body = string.Format(bodyTemplateDMS, bValue.ToString());
                            SendEmail(body, WSEmailFrom, emailTO, emailCC);
                        }
                    }
                    catch (Exception ex)
                    {
                        string cuk = ex.Message;
                    }
                    finally
                    {
                        UpdateDataDMS(DataDMS);
                    }
                }
            }
            catch (Exception e)
            {
                string cuk = e.Message;
            }
        }

        private static void getEmail(ArrayList Data, ref string emailTO, ref string emailCC)
        {
            foreach (ContactDealer item in Data)
            {
                if (item.Tipe.ToLower() == "to")
                {
                    if (emailTO.Length == 0)
                    {
                        emailTO = item.Email1;
                        if (item.Email2.Length > 0)
                        {
                            emailTO = emailTO + ";" + item.Email2;
                        }
                    }
                    else
                    {
                        emailTO = emailTO + ";" + item.Email1;
                        if (item.Email2.Length > 0)
                        {
                            emailTO = emailTO + ";" + item.Email2;
                        }
                    }
                } 
                else if (item.Tipe.ToLower() == "cc")
                {
                    if (emailCC.Length == 0)
                    {
                        emailCC = item.Email1;
                        if (item.Email2.Length > 0)
                        {
                            emailCC = emailCC + ";" + item.Email2;
                        }
                    }
                    else
                    {
                        emailCC = emailCC + ";" + item.Email1;
                        if (item.Email2.Length > 0)
                        {
                            emailCC = emailCC + ";" + item.Email2;
                        }
                    }
                }
            }
        }

        public static void SendEmail(string htmlString, string EmailFrom, string EmailTo, string EmailCC = "")
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

                message.Subject = "Chassis Master Claim change Status";
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

        private static void UpdateQueue(List<ChassisMasterClaimEmailQueue> oData)
        {
            ChassisMasterClaimEmailQueueFacade emailQueueFacade = new ChassisMasterClaimEmailQueueFacade(User);
            foreach (ChassisMasterClaimEmailQueue item in oData)
            {
                item.IsSend = 1;
                int result = emailQueueFacade.Update(item);
            }
        }

        private static void UpdateDataDMS(List<ChassisMasterClaimDMSReminder> oData)
        {
            ChassisMasterClaimDMSReminderFacade emailQueueFacade = new ChassisMasterClaimDMSReminderFacade(User);
            foreach (ChassisMasterClaimDMSReminder item in oData)
            {
                item.IsSent = 1;
                int result = emailQueueFacade.Update(item);
            }
        }

        static string bodyTemplate = @"<FONT face=Arial size=1>
                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                        <tr>
                                        <td colspan=3 height=50>
                                        Dengan hormat,&nbsp;
                                        <br><br>Berikut Data Transaksi Claim yang harus di Proses :
                                        </td>
                                        </tr>
                                        <tr>
                                        <td colspan=3 height=10></td>
                                        </tr>
                                        <tr><td>
                                        <table border='1' width='100%'>
                                        <tr>
                                        <th height='10'>Nomor Claim</th>
                                        <th height='10'>Dealer</th>
                                        <th height='10'>Status</th>
                                        <th height='10'>Tanggal Input Claim</th>
                                        <th height='10'>Tanggal Update Transaksi</th>
                                        </tr>

                                        {0}

                                        </table>
                                        </td></tr>
                                        <tr>
                                        <td style='height: 50px;'></td>
                                        </tr>

                                        </table>
                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>

                                        <tr>
                                        <td width='100%'>Silahkan login D_Net untuk memproses transaksi tersebut.</td>
                                        </tr>

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


        static string bodyTemplateDMS = @"<FONT face=Arial size=1>
                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                        <tr>
                                        <td colspan=3 height=50>
                                        Dengan hormat,&nbsp;
                                        <br><br>Berikut terdapat nomor Chassis yang di Retur dan Perlu di Input Transaksi Claim :
                                        </td>
                                        </tr>
                                        <tr>
                                        <td colspan=3 height=10></td>
                                        </tr>
                                        <tr><td>
                                        <table border='1' width='50%'>
                                        <tr>
                                        <th width='5%'>No</th>
                                        <th width='45%'>Chassis Number</th>
                                        <th width='50%'>Dealer Alokasi</th>
                                        </tr>

                                        {0}

                                        </table>
                                        </td></tr>
                                        <tr>
                                        <td style='height: 50px;'></td>
                                        </tr>

                                        </table>
                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>

                                        <tr>
                                        <td width='100%'>Silahkan lakukan penginputan untuk Chassis tersebut di DMS.</td>
                                        </tr>

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
    }
}
