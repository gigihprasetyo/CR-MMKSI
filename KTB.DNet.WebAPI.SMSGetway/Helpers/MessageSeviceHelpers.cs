using DNET.Monitoring.DAL;
using KTB.DNet.WebAPI.SMSGetway.Interfaces;
using KTB.DNet.WebAPI.SMSGetway.Models;
using KTB.DNet.WebAPI.SMSGetway.Models.Sandeza;
using KTB.DNet.WebAPI.SMSGetway.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Text;

namespace KTB.DNet.WebAPI.SMSGetway.Helpers
{
    public class MessageSeviceHelpers : IServiceMessage<MessageServicesResult>
    {
        private DBConnection dbConn;
        private MessageServicesUser MSUser;
        private MessageServicesResult MSResult;
        private MessageServicesConfig MSConfig;
        private GlobalHelpers Func;
        private Dictionary<int, string> ListChannels;
        private int MSUserID;
        private int MSConfigID;
        private int FID;
        private string Content;

        public MessageSeviceHelpers()
        {
            this.MSResult = new MessageServicesResult();
            this.MSConfig = new MessageServicesConfig();
            this.Func = new GlobalHelpers();
            this.ListChannels = new Dictionary<int, string>();

            this.dbConn = new DBConnection();
            this.dbConn.DataSource = WebConfigurationManager.AppSettings["DataSource"];
            this.dbConn.DBName = WebConfigurationManager.AppSettings["DBName"];
            this.dbConn.UserName = WebConfigurationManager.AppSettings["UserName"];
            this.dbConn.Password = WebConfigurationManager.AppSettings["Password"];
            this.dbConn.DBType = EnumDBType.DBType.SqlServer;
        }

        public MessageSeviceHelpers(MessageServicesUser msg)
            : this()
        {
            this.MSUser = msg;
        }

        public void SendMessage()
        {
            if (Validate())
            {
                try
                {
                    this.MSUser.BodyMessage = Func.cleanForJSON(this.MSUser.BodyMessage);
                    MessageServices req = new MessageServices();
                    req.RefID = Func.GenerateRefID();
                    req.Type = "1";
                    req.Time = Func.TimeNow();
                    req.Signature = Func.SHA256_hash(this.MSConfig.APIUser + this.MSConfig.APIPassword + req.Time);
                    req.Subject = MSConfig.Subject;
                    req.SenderID = MSConfig.SenderID;
                    req.Username = MSConfig.UserName;

                    if (!string.IsNullOrEmpty(MSUser.TypeMessage))
                    {
                        req.Type = MSUser.TypeMessage;
                    }

                    req.AddChannel(GetChannels(MSUser, MSConfig));
                    IJsonSerialize json = new JsonSerialize<MessageServices>(req);

                    this.InsertMSHistory(req);

                    APIHelpers<MessageResponse> api = new APIHelpers<MessageResponse>();
                    api.Url = MSConfig.APIUrl;
                    api.ProxyAddress = WebConfigurationManager.AppSettings["ProxyAddress"];
                    api.ProxyPort = WebConfigurationManager.AppSettings["ProxyPort"];
                    api.JsonContent = json.Serialize();
                    this.Content = api.JsonContent;
                    Task.Run(() => api.POST()).Wait();

                    if (api.ResultPost == null)
                    {
                        this.MSResult.Code = "-3:HTTP Error";
                        this.MSResult.Message = string.Format("Error occurred, the status code is: {0}", api.StatusCode);
                        this.MSResult.Status = "Fail";
                        return;
                    }
                    this.UpdateMSHistory(api.ResultPost, req);

                    this.MSResult.Code = api.ResultPost.rc + ":Normal";
                    this.MSResult.Message = GetMessageFromRC(api.ResultPost);
                    this.MSResult.Status = api.ResultPost.rc == "0" ? "OK" : "failed";
                    return;
                }
                catch (Exception ex)
                {
                }
                
            }
        }

        private string GetMessageFromRC(MessageResponse mR)
        {
            string msg = string.Empty;
            switch (mR.rc)
            {
                case "0":
                    msg = "Message has been sent. TRID : {0} ";
                    break;
                case "1":
                    msg = "Invalid Parameter / Invalid JSON Format {0}";
                    break;
                case "2":
                    msg = "Internal Error {0}";
                    break;
                case "3":
                    msg = "Invalid MSISDN / Destination {0}";
                    break;
                case "4":
                    msg = "Invalid Signature / Account Not Found {0}";
                    break;
                case "5":
                    msg = "Invalid Corporate {0}";
                    break;
                case "6":
                    msg = "Client IP not in Whitelist {0}";
                    break;
                case "7":
                    msg = "Not Enough Token {0}";
                    break;
                case "8":
                    msg = "Invalid Sender Id (sender_id) {0}";
                    break;
                case "9":
                    msg = "Invalid Reference Id (ref_id) {0}";
                    break;
                default:
                    break;
            }
            return string.Format(msg, mR.code_sms);
        }

        public MessageServicesResult GetResponse
        {
            get { return this.MSResult; }
        }

        private void InsertMSHistory()
        {
            try
            {
                String strQuery = "InsertMSHistory";

                Dictionary<int, object> result = new Dictionary<int, object>();
                string[] arrChannel = this.MSConfig.Channels.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrChannel.Length; i++)
                {
                    string msg = string.Empty;
                    switch (arrChannel[i].ToLower().Trim())
                    {
                        case "whatsapp":
                            msg = this.MSUser.BodyMessage;
                            break;
                        case "sms":
                            msg = GenerateMessage(this.MSUser.BodyMessage, this.MSConfig.TemplateValue);
                            break;
                        default:
                            break;
                    }
                    using (GlobalDatabase db = new GlobalDatabase(this.dbConn))
                    {
                        db.ClearParameter();
                        db.AddOutputParameter("@ID", 0);
                        db.AddParameter("@MSUserID", this.MSUserID);
                        db.AddParameter("@MSConfigID", this.MSConfigID);
                        db.AddParameter("@Channel", arrChannel[i]);
                        db.AddParameter("@DestinationNumber", this.MSUser.DestinationNo);
                        db.AddParameter("@Message", msg);
                        db.AddParameter("@IsUsingModem", this.MSConfig.IsUsingModem);

                        if (!string.IsNullOrEmpty(MSUser.FID))
                        {
                            db.AddParameter("@FID", Int32.Parse(MSUser.FID));
                        }
                        else { db.AddParameter("@FID", DBNull.Value); }

                        db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                        var res = db.RetrieveParamterOutput();

                        this.ListChannels.Add((int)res["ID"], msg);
                    }
                }


            }
            catch (Exception ex) { throw; }
        }

        private void InsertMSHistory(MessageServices msReq)
        {
            try
            {
                String strQuery = "InsertMSHistory";

                Dictionary<int, object> result = new Dictionary<int, object>();
                string[] arrChannel = this.MSConfig.Channels.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrChannel.Length; i++)
                {
                    string msg = string.Empty;
                    switch (arrChannel[i].ToLower().Trim())
                    {
                        case "whatsapp":
                            msg = this.MSUser.BodyMessage;
                            break;
                        case "sms":
                            byte[] asciByte = Encoding.ASCII.GetBytes(this.MSUser.BodyMessage);
                            msg = GenerateMessage1_6(this.MSUser.BodyMessage, this.MSConfig.TemplateValue);
                            break;
                        default:
                            break;
                    }
                    using (GlobalDatabase db = new GlobalDatabase(this.dbConn))
                    {
                        IJsonSerialize json = new JsonSerialize<MessageServices>(msReq);

                        db.ClearParameter();
                        db.AddOutputParameter("@ID", 0);
                        db.AddParameter("@MSUserID", this.MSUserID);
                        db.AddParameter("@MSConfigID", this.MSConfigID);
                        db.AddParameter("@RefID", msReq.RefID);
                        db.AddParameter("@Channel", arrChannel[i]);
                        db.AddParameter("@DestinationNumber", this.MSUser.DestinationNo);
                        db.AddParameter("@Message", msg);
                        db.AddParameter("@JsonContent", json.Serialize());
                        db.AddParameter("@IsUsingModem", this.MSConfig.IsUsingModem);

                        if (!string.IsNullOrEmpty(MSUser.FID))
                        {
                            db.AddParameter("@FID", Int32.Parse(MSUser.FID));
                        }
                        else { db.AddParameter("@FID", DBNull.Value); }

                        db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                        var res = db.RetrieveParamterOutput();

                        this.ListChannels.Add((int)res["ID"], msg);
                    }
                }


            }
            catch (Exception ex) { throw; }
        }

        private void UpdateMSHistory(MessageResponse response, MessageServices msg)
        {
            try
            {
                foreach (var iChannel in ListChannels)
                {
                    String strQuery = "UpdateMSHistory";
                    using (GlobalDatabase db = new GlobalDatabase(this.dbConn))
                    {

                        db.ClearParameter();
                        db.AddParameter("@ID", iChannel.Key);
                        db.AddParameter("@RefID", msg.RefID);
                        db.AddParameter("@TransactionID", response.code_sms);
                        db.AddParameter("@JsonContent", this.Content);
                        db.AddParameter("@Status", response.rc);

                        db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                    }
                }
            }
            catch (Exception ex) { throw; }
        }

        private bool Validate()
        {
            DataTable dt = new DataTable();
            bool isUsingModem = false;
            bool isActive = true;
            using (GlobalDatabase db = new GlobalDatabase(dbConn))
            {
                string spName = "UpGetMessageServiceConfig";
                db.AddParameter("@UserName", this.MSUser.UserName);
                db.AddParameter("@Password", this.MSUser.Password);
                db.AddParameter("@ClientID", this.MSUser.ClientID);

                dt = db.ExecuteDataTable(spName, CommandType.StoredProcedure);
                if (dt.Rows.Count > 0)
                {
                    this.MSUserID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    this.MSConfigID = Convert.ToInt32(dt.Rows[0]["MSConfigID"]);
                    isActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    isUsingModem = Convert.ToBoolean(dt.Rows[0]["IsUsingModem"]);
                    this.MSConfig.IsUsingModem = isUsingModem;
                    this.MSConfig.DestinationNo = this.MSUser.DestinationNo;
                    this.MSConfig.APIUser = dt.Rows[0]["APIUser"].ToString();
                    this.MSConfig.APIPassword = dt.Rows[0]["APIPassword"].ToString();
                    this.MSConfig.APIUrl = dt.Rows[0]["APIUrl"].ToString();
                    this.MSConfig.BodyMassage = this.MSUser.BodyMessage;
                    this.MSConfig.IsActive = isActive;
                }
                else
                {
                    this.MSResult.Code = "-3:Credential";
                    this.MSResult.Message = "Invalid Credential";
                    this.MSResult.Status = "Fail";

                    this.MSUserID = -1;
                    return false;
                }

                if (!isActive)
                {
                    this.MSResult.Code = "-2:Credential";
                    this.MSResult.Message = "Account is Expired";
                    this.MSResult.Status = "Fail";
                    this.MSUserID = 0;
                    this.MSConfig = null;
                    return false;
                }
                if (!isUsingModem)
                {
                    this.MSConfig.APIUrl = Convert.ToString(dt.Rows[0]["APIUrl"]);
                    this.MSConfig.APIUser = Convert.ToString(dt.Rows[0]["APIUser"]);
                    this.MSConfig.Subject = Convert.ToString(dt.Rows[0]["Subject"]);
                    this.MSConfig.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                    this.MSConfig.SenderID = Convert.ToString(dt.Rows[0]["SenderID"]);
                    this.MSConfig.Channels = Convert.ToString(dt.Rows[0]["Channels"]);
                    this.MSConfig.TemplateID = Convert.ToString(dt.Rows[0]["MSTemplateID"]);
                    this.MSConfig.TemplateValue = Convert.ToString(dt.Rows[0]["MSTemplateValue"]);
                    this.MSConfig.TemplateType = Convert.ToString(dt.Rows[0]["MSTemplateType"]);
                    this.MSConfig.WhatsAppHeader = Convert.ToString(dt.Rows[0]["WhatsAppHeader"]);
                    this.MSConfig.BackOn = Convert.ToString(dt.Rows[0]["BackOn"]);
                    this.MSConfig.BackExp = Convert.ToString(dt.Rows[0]["BackExp"]);

                    return true;
                }
                else
                {
                    spName = "SP_OUTBOX";
                    db.ClearParameter();

                    db.AddParameter("@DestinationNumber", this.MSUser.DestinationNo);
                    db.AddParameter("@SmsText", this.MSUser.BodyMessage);
                    db.AddParameter("@action", "INSERT");
                    db.AddParameter("@Category", "API");
                    db.AddParameter("@Status", "UNSENT");
                    db.ExecuteNonQuery(spName, CommandType.StoredProcedure);

                    this.MSResult.Code = "0:Normal";
                    this.MSResult.Message = "Message will be Send using Modem";
                    this.MSResult.Status = "OK";
                    //insert ke OutBox pake modem biasa
                    return false;
                }
            }
        }

        private Dictionary<int, object> GetChannels(MessageServicesUser mUsr, MessageServicesConfig mCfg)
        {
            Dictionary<int, object> result = new Dictionary<int, object>();
            string[] arrChannel = mCfg.Channels.Split(new char[] { ',', ';' }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arrChannel.Length; i++)
            {
                switch (arrChannel[i].ToLower().Trim())
                {
                    case "whatsapp":
                        WhatsAppChannel1_6 wa = new WhatsAppChannel1_6();
                        wa.Message = mUsr.BodyMessage;
                        wa.TemplateID = mCfg.TemplateID;
                        wa.MSISDN = mUsr.DestinationNo;
                        wa.Header = mCfg.WhatsAppHeader;
                        wa.SosialID = "7";
                        if (string.IsNullOrEmpty(mCfg.TemplateType)) { wa.Type = "2"; }
                        else{ wa.Type = mCfg.TemplateType;}

                        if (i != arrChannel.Length - 1 )
                        {
                             wa.BackupOn = mCfg.BackOn;
                             wa.BackupExp = mCfg.BackExp;
                        }
                        result.Add(i + 1, wa);
                        break;
                    case "sms":
                        SmsChannel sms = new SmsChannel();
                        sms.DestinationNo = mUsr.DestinationNo;
                        sms.Message = GenerateMessage1_6(mUsr.BodyMessage, mCfg.TemplateValue);
                        if (i != arrChannel.Length - 1)
                        {
                            sms.BackupOn = mCfg.BackOn;
                            sms.BackupExpired = mCfg.BackExp;
                        }
                        result.Add(i + 1, sms);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        private string GenerateMessage1_6(string data, string template)
        {
            if (string.IsNullOrEmpty(template)) { return data; }
            string[] arrData = data.Split(new string[]{"||"}, StringSplitOptions.None);
            string result = template;
            for (int i = 0; i < arrData.Length; i++)
            {
                if (template.IndexOf("[" + (i + 1).ToString() + "]") > -1)
                {
                    string msg = arrData[i].Split(new string[] { ":=:" }, StringSplitOptions.None)[1];
                    result = result.Replace("[" + (i + 1).ToString() + "]", msg.Replace("*", "").Replace("_", "").Replace("~", ""));
                }
            }
            return result;
        }

        private string GenerateMessage(string data, string template)
        {
            if (string.IsNullOrEmpty(template)) { return data; }
            string[] arrData = data.Split(new string[] { ";:;" }, StringSplitOptions.None);
            string result = template;
            for (int i = 0; i < arrData.Length; i++)
            {
                if (template.IndexOf("[" + (i + 1).ToString() + "]") > -1)
                {
                    result = result.Replace("[" + (i + 1).ToString() + "]", arrData[i].Replace("*", "").Replace("_", "").Replace("~", ""));
                }
            }
            return result;
        }

        
    }
}