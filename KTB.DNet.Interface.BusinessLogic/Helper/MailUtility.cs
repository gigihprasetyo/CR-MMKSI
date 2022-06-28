#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MailUtility class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public static class MailUtility
    {
        #region Public Methods
        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="mailFrom"></param>
        /// <param name="mailTo"></param>
        /// <param name="mailToIdentity"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <param name="attachment"></param>
        public static string SendMail(string smtpServer, string mailFrom, string mailTo, string mailSubject, string mailBody, bool isBodyHtml = true, Attachment attachment = null)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                // construct mail message
                mailMessage.From = new MailAddress(mailFrom);
                //mailMessage.To.Add(new MailAddress(mailTo));
                mailMessage.Subject = mailSubject;
                mailMessage.IsBodyHtml = isBodyHtml;
                mailMessage.Body = mailBody;
                mailMessage.Priority = MailPriority.Normal;

                foreach(var addr in mailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMessage.To.Add(addr);
                }

                // check if any attachment
                if (attachment != null)
                    mailMessage.Attachments.Add(attachment);

                using (SmtpClient smtpClient = new SmtpClient(smtpServer))
                {
                    try
                    {
                        // send mail
                         smtpClient.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="objPartShopOld"></param>
        /// <param name="objPartShopNew"></param>
        /// <param name="dealer"></param>
        /// <returns></returns>
        public static bool SendPartShopEmail(AutoMapper.IMapper mapper, PartShop objPartShopOld, PartShop objPartShopNew, Dealer dealer, List<DNetValidationResult> validationResults, bool isUpdate)
        {
            string valueEmail = string.Empty;
            string result = string.Empty;
            string smtp = "";
            var _appConfigBL = new AppConfigBL(mapper);
            var cfgSmtp = _appConfigBL.GetConfigByName("SMTP");
            if (cfgSmtp == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, "Config: SMTP")));
            }
            else
            {
                smtp = cfgSmtp.Value;
            }

            string emailFrom = "";
            string emailTo = "";

            // Get emailfrom config data
            var cfgEmailFrom = _appConfigBL.GetConfigByName("EmailFrom");
            if (cfgEmailFrom == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, "Config: EmailFrom")));
            }
            else
            {
                emailFrom = cfgEmailFrom.Value;
            }

            // get emailto config data
            var cfgEmailSPAdmin = _appConfigBL.GetConfigByName("EmailSPAdmin");

            if (cfgEmailFrom == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, "Config: EmailSPAdmin")));
            }
            else
            {
                emailTo = cfgEmailSPAdmin.Value;
            }

            if (validationResults.Count == 0)
            {
                if (isUpdate)
                {
                    valueEmail = GeneratePartShopUpdateEmail(objPartShopOld, objPartShopNew, dealer);
                    result = MailUtility.SendMail(smtp, emailFrom, emailTo, "[MMKSI-DNet] Parts - Update Part Shop", valueEmail, true);
                }
                else
                {
                    valueEmail = GeneratePartShopRequestEmail(objPartShopOld, dealer);
                    result = MailUtility.SendMail(smtp, emailFrom, emailTo, "[MMKSI-DNet] Parts - Request Part Shop Code", valueEmail, true);
                }
                
                if (result != null)
                    validationResults.Add(new DNetValidationResult(result));
            }

            return true;
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Generate email format
        /// </summary>
        /// <param name="objPartShopOld"></param>
        /// <param name="objPartShopNew"></param>
        /// <param name="dealer"></param>
        /// <returns></returns>
        private static string GeneratePartShopUpdateEmail(PartShop objPartShopOld, PartShop objPartShopNew, Dealer dealer)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("<HTML>");
            sb.Append("<Body>");
            sb.Append("<FONT face=Arial size=1>");
            sb.Append("<table width=700>");
            sb.Append("<tr>");
            sb.Append("<td colspan=5 align=center><b>Update Data Part Shop</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan=5 height=50></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan=5 height=50>");
            sb.Append("Dengan hormat, ");
            sb.Append(("<br><br>Berikut perubahan data Part Shop :<b> "
                            + (objPartShopOld.PartShopCode + " </b>")));
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table border=1 width=700 cellpadding=0>");
            sb.Append("<tr>");
            sb.Append("<td height=50 colspan=3 align=center><b>Data</b></td>");
            sb.Append("<td height=50 align=center><b>Lama</b></td>");
            sb.Append("<td height=50 align=center><b>Baru</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=50 colspan=3 align=left><b>Nama</b></td>");
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopOld.Name + "</b></td>")));
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopNew.Name + "</b></td>")));
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=50 colspan=3 align=left><b>Alamat</b></td>");
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopOld.Address + "</b></td>")));
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopNew.Address + "</b></td>")));
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=50 colspan=3 align=left><b>Kota</b></td>");
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopOld.CityPart.CityName + "</b></td>")));
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopNew.CityPart.CityName + "</b></td>")));
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=50 colspan=3 align=left><b>Propinsi</b></td>");
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopOld.CityPart.Province.ProvinceName + "</b></td>")));
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopNew.CityPart.Province.ProvinceName + "</b></td>")));
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=50 colspan=3 align=left><b>Telp</b></td>");
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopOld.Phone + "</b></td>")));
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopNew.Phone + "</b></td>")));
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=50 colspan=3 align=left><b>Fax</b></td>");
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopOld.Fax + "</b></td>")));
            sb.Append(("<td height=50 align=left><b>"
                            + (objPartShopNew.Fax + "</b></td>")));
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table width=700>");
            sb.Append("<tr>");
            sb.Append("<td><font color=\'blue\'>Informasi perubahan data Part Shop, silahkan akses pada link berikut : <a href" +
                " = https://d-net.mitsubishi-motors.co.id/default_parts.aspx?screenid=9963>https://d-net.mitsubishi-m" +
                "otors.co.id</a></font></td>");
            sb.Append("</tr>");

            if (!(dealer == null))
            {
                sb.Append("<tr>");
                sb.Append(("<td>" + (" Diajukan oleh dealer :"
                                + (dealer.DealerCode + (" - "
                                + (dealer.DealerName + "</td>"))))));
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            sb.Append("</FONT>");
            sb.Append("</Body>");
            sb.Append("</HTML>");
            return sb.ToString();
        }

        /// <summary>
        /// Generate email format
        /// </summary>
        /// <param name="objPartShop"></param>
        /// <param name="dealer"></param>
        /// <returns></returns>
        private static string GeneratePartShopRequestEmail(PartShop objPartShop, Dealer dealer)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("<FONT face=Arial size=1>");
            sb.Append("<table width=700>");
            sb.Append("<tr>");
            sb.Append("<td colspan=5 align=center><b>Request Part Shop Code</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan=5 height=50></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan=5 height=50>");
            sb.Append("Dengan hormat,&nbsp;");
            sb.Append("<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Berikut daftar Part Shop untuk dibuatkan kode :");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan=5 height=10></td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td height=50 colspan=6 align=center><hr><b>Daftar Part Shop</b></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table border=1 width=700 cellpadding=0>");
            sb.Append("<tr>");
            //sb.Append("<td width=30>No</td>");
            sb.Append("<td width=100>Dealer Yang Mendaftarkan</td>");
            sb.Append("<td width=295>Nama</td>");
            sb.Append("<td width=295>Alamat</td>");
            sb.Append("<td width=100>Telephone/Pc</td>");
            sb.Append("<td width=100>Fax/Pc</td>");
            sb.Append("<td width=125>Propinsi</td>");
            sb.Append("<td width=100>Kota</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td>" + dealer.DealerCode + " / " + dealer.SearchTerm2 + "</td>");

            sb.Append("<td>" + objPartShop.Name + "</td>");
            sb.Append("<td>" + objPartShop.Address + "</td>");
            sb.Append("<td>" + objPartShop.Phone + "</td>");
            sb.Append("<td>" + objPartShop.Fax + "</td>");

            if (objPartShop.CityPart == null)
            {
                sb.Append("<td>&nbsp;</td>");
                sb.Append("<td>&nbsp;</td>");
            }
            else
            {
                sb.Append("<td>" + objPartShop.CityPart.Province.ProvinceName + "</td>");
                sb.Append("<td>" + objPartShop.CityPart.CityName + "</td>");
            }
            sb.Append("</tr>");

            sb.Append("</table>");

            sb.Append("<table width=700>");

            sb.Append("<tr>");
            sb.Append("<td><font color='blue'>Mohon untuk dibuatkan kode untuk daftar part shop diatas. Link : <a href = https://d-net.mitsubishi-motors.co.id/default_parts.aspx?screenid=9963>https://d-net.mitsubishi-motors.co.id</a></font></td>");
            sb.Append("</tr>");

            if (dealer != null)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + " Diajukan oleh dealer :" + dealer.DealerCode + " - " + dealer.DealerName + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append("</FONT>");

            return sb.ToString();
        }
        #endregion
    }
}
