using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using KTB.DNET.BusinessFacade;
using KTB.DNET.BusinessFacade.SparePart;
using KTB.DNET.BusinessFacade.IndentPartEquipment;
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
    public static class IndenPartExpiredReminderLogic
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);

        public static void SendMailIndenPartExpired()
        {
            try
            {
                string WSEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"];
                string bodyValue = " <tr> <td width='40' align='RIGHT'>{0}</td> <td width='100' align='LEFT'>{1}</td> <td width='550' align='center'>{2}</td> <td width='150' align='center'>{3}</td> <td width='100' align='RIGHT'>{4}</td> <td width='100' align='RIGHT'>{5}</td> " +
                " <td width='150' align='LEFT'>{6}</td> <td width='150' align='RIGHT'>{7}</td>  </tr>";

                SparePartDueDateNotification Data = new SparePartDueDateNotification();
                List<v_EquipPO> listv_EquipPO = Data.RetrieveIndenPartExpired();

                if (listv_EquipPO.Count > 0)
                {
                    try
                    {
                        List<v_EquipPO> vDealerList = listv_EquipPO.DistBy(x => x.DealerCode).ToList();
                        if (vDealerList.Count > 0)
                        {
                            foreach (v_EquipPO item in vDealerList)
                            {
                                StringBuilder bValue = new StringBuilder();
                                List<v_EquipPO> listv_EquipPODealer = Data.RetrieveIndenPartExpired(item.DealerCode);
                                int varNo = 1;

                                foreach (v_EquipPO v_EquipPOData in listv_EquipPODealer.DistBy(x => new { x.RequestNo }).ToList())
                                {
                                    //{0} = No
                                    //{1} = Status
                                    //{2} = Kode Dealer
                                    //{3} = Nama Dealer
                                    //{4} = No Pengajuan
                                    //{5} = No Estimasi
                                    //{6} = Tanggal Pengajuan
                                    //{7} = Type Pembayaran
                                    //{8} = Deadline Kwitansi
                                    //{9} = Total Order
                                    Decimal TotalTagihan = 0;
                                    List<IndentPartDetail> eQData = Data.RetrieveIndenPartDetail(v_EquipPOData.ID);
                                    if (eQData.Count > 0)
                                    {
                                        foreach (IndentPartDetail dtl in eQData)
                                        {
                                            TotalTagihan += (dtl.Qty * dtl.EstimationEquipDetail.Harga) - ((dtl.EstimationEquipDetail.Discount / 100) * dtl.Qty * dtl.EstimationEquipDetail.Harga);
                                        }

                                    }

                                    bValue.Append(string.Format(bodyValue, varNo,
                                        v_EquipPOData.DealerCode.ToString(),
                                        v_EquipPOData.DealerName.ToString(),
                                        v_EquipPOData.RequestNo.ToString(),
                                        v_EquipPOData.EstimationNumber.ToString(),
                                        v_EquipPOData.CreatedTime.ToString("dd/MM/yyyy"),
                                        v_EquipPOData.PaymentTypeDesc.ToString(),
                                        v_EquipPOData.CreatedTime.AddDays(14).ToString("dd/MM/yyyy")
                                        //,TotalTagihan.ToString("#,###")
                                        ));
                                    varNo++;
                                }

                                string dealerCity = "";
                                if (item.DealerCode != null)
                                {
                                    Dealer dlr = new DealerFacade(User).Retrieve(item.DealerCode);
                                    if (dlr.City != null)
                                    {
                                        dealerCity = dlr.City.CityName;
                                    }
                                }

                                string body = string.Format(bodyTemplateIndenPart, item.DealerCode, item.DealerName, dealerCity, bValue.ToString(), DateTime.Now.ToString("dd/MM/yyyy"));
                                StandardCode stdCode = new StandardCodeFacade(User).GetByCategoryValueCode("EnumServiceEmailNotificationKind", "DocumentIndentPartExpired");
                                ArrayList arlEmailDealer = new ServiceDueDateNotificationFacade(User).Retrieve(item.DealerCode, stdCode.ValueId.ToString());
                                string emailTO = "";
                                string emailCC = "";
                                getEmailIndent(arlEmailDealer, ref emailTO, ref emailCC);
                                SendEmail(body, "Reminder Indent Part Expired", WSEmailFrom, emailTO, emailCC);
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

        private static void getEmailIndent(ArrayList Data, ref string emailTO, ref string emailCC)
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

        public static IEnumerable<TSource> DistBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
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


        #region Template IndenPartExpired
        static string bodyTemplateIndenPart = @"<table border='0' cellspacing='2' cellpadding='0' width='800'>
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
                                                        Berikut Nomor Pengajuan indent part yang harus dibayarkan melalui Deposit B :<br>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id='TblData' border='1' cellspacing='0' cellpadding='1'>
                                                            <tr>
                                                                <td width='40' align='center'><b>No</b></td>
                                                                <td width='100' align='center'><b>Kode Dealer</b></td>
                                                                <td width='550' align='center'><b>Nama Dealer</b></td>
                                                                <td width='150' align='center'><b>No Pengajuan</b></td>
                                                                <td width='150' align='center'><b>No Estimasi</b></td>
                                                                <td width='100' align='center'><b>Tanggal Pengajuan</b></td>
                                                                <td width='150' align='center'><b>Tipe Pembayaran</b></td>
                                                                <td width='100' align='center'><b>Deadline Kwitansi</b></td>
                                                                
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
                                                            Silakan dealer mengajukan pencairan deposit B di menu SERVICE – PROSES PENCAIRAN di DNET*.
                                                        </b>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style='COLOR: black'>
                                                        <i>
                                                            *)Silahkan abaikan Email ini jika sudah melakukan Pencairan Deposit B

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
