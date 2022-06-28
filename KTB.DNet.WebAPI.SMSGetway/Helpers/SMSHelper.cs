using DNET.Monitoring.DAL;
using KTB.DNet.WebAPI.SMSGetway.Helpers.Interfaces;
using KTB.DNet.WebAPI.SMSGetway.Interfaces;
using KTB.DNet.WebAPI.SMSGetway.Models;
using KTB.DNet.WebAPI.SMSGetway.Models.Sandeza;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace KTB.DNet.WebAPI.SMSGetway.Helpers
{
    public class SMSHelper : GlobalHelpers, IServiceMessage<SMSResult>
    {
        private DBConnection dbConn;
        private SMSUser UserMessage;
        private SMSConfig ConfigMessage;
        private SMSResult ResultMessage;
        private int SMSUserID;
        private int SMSOutboxID;

        public SMSHelper()
        {
            this.SMSUserID = 0;
            this.ResultMessage = new SMSResult();
            this.ConfigMessage = new SMSConfig();
            
            this.dbConn = new DBConnection();
            this.dbConn.DataSource = WebConfigurationManager.AppSettings["DataSource"];
            this.dbConn.DBName = WebConfigurationManager.AppSettings["DBName"];
            this.dbConn.UserName = WebConfigurationManager.AppSettings["UserName"];
            this.dbConn.Password = WebConfigurationManager.AppSettings["Password"];
            this.dbConn.DBType = EnumDBType.DBType.SqlServer;
        }

        public SMSHelper(SMSUser obj):this()
        {
            this.UserMessage = obj;
        }

        public SMSResult GetResponse {
            get { return this.ResultMessage; }
        }

        public void SendMessage()
        {
            if (Validate())
            {
                this.InsertSMSOutbox();

                MessageServices req = new MessageServices();
                req.RefID = GenerateRefID();
                req.Time = TimeNow();
                req.Signature = SHA256_hash(this.ConfigMessage.APIUser + this.ConfigMessage.APIPassword + req.Time);
                req.Subject = string.Empty;
                req.SenderID = string.Empty;
                req.Username = string.Empty;

                SmsChannel objsms = new SmsChannel();
                objsms.DestinationNo = this.UserMessage.DestinationNo;
                objsms.Message = this.UserMessage.BodyMessage;

                req.AddChannel(1,objsms);
                IJsonSerialize json = new JsonSerialize<MessageServices>(req);

                APIHelpers<MessageResponse> api = new APIHelpers<MessageResponse>();
                api.Url = ConfigMessage.APIUrl;
                api.ProxyAddress = string.Empty;
                api.ProxyPort = string.Empty;
                api.JsonContent = json.Serialize();
                Task.Run(() => api.POST()).Wait();

                if (api.ResultPost == null)
                {
                    this.ResultMessage.Code = "-3:HTTP Error";
                    this.ResultMessage.Message = string.Format("Error occurred, the status code is: {0}", api.StatusCode);
                    this.ResultMessage.Status = "Fail";
                    return;
                }
                UpdateSMSOutbox(api);
                
            }
           
        }

        private void UpdateSMSOutbox(APIHelpers<MessageResponse> api)
        {
            try
            {
                String strQuery = "UpdateSMSOutbox";
                using (GlobalDatabase db = new GlobalDatabase(this.dbConn))
                {
                    db.ClearParameter();
                    db.AddParameter("@id", this.SMSOutboxID);
                    db.AddParameter("@APIResponse", api.JsonResult);
                    db.AddParameter("@TransactionID", api.ResultPost.ref_id);
                    db.AddParameter("@status", api.ResultPost.code_sms);

                    db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                }
                switch (api.ResultPost.code_sms)
                {
                    case "0":
                        this.ResultMessage.Code = "0:Normal";
                        this.ResultMessage.Message = string.Format("SMS has been sent to SMS Gateway  TRID : {0} ", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "OK";
                        break;
                    case "1":
                        this.ResultMessage.Code = "1:Normal";
                        this.ResultMessage.Message = string.Format("Invalid Parameter  {0}", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "Fail";
                        break;
                    case "2":
                        this.ResultMessage.Code = "2:Normal";
                        this.ResultMessage.Message = string.Format("Internal Error  {0} ", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "Fail";
                        break;
                    case "3":
                        this.ResultMessage.Code = "3:Normal";
                        this.ResultMessage.Message = string.Format("Invalid MSISDN / Destination  {0} ", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "Fail";
                        break;
                    case "4":
                        this.ResultMessage.Code = "4:Normal";
                        this.ResultMessage.Message = string.Format("Invalid Username / Password  {0} ", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "Fail";
                        break;
                    case "5":
                        this.ResultMessage.Code = "5:Normal";
                        this.ResultMessage.Message = string.Format("Membership is not available  {0} ", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "Fail";
                        break;
                    case "6":
                        this.ResultMessage.Code = "6:Normal";
                        this.ResultMessage.Message = string.Format("IP is not whitelist  {0} ", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "Fail";
                        break;
                    case "7":
                        this.ResultMessage.Code = "7:Normal";
                        this.ResultMessage.Message = string.Format("Not enough credit  {0} ", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "Fail";
                        break;
                    case "8":
                        this.ResultMessage.Code = "8:Normal";
                        this.ResultMessage.Message = string.Format("Not enough Sender ID  {0} ", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "Fail";
                        break;
                    case "9":
                        this.ResultMessage.Code = "9:Normal";
                        this.ResultMessage.Message = string.Format("Not enough Ref ID  {0} ", api.ResultPost.ref_id);
                        this.ResultMessage.Status = "Fail";
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool Validate()
        {
            DataTable dt = new DataTable();
            bool isUsingModem = false;
            bool isActive = true;
            using(GlobalDatabase db = new GlobalDatabase(dbConn))
	        {
                string spName = "UpGetSMSConfig";
                db.AddParameter("@UserName", this.UserMessage.UserName);
                db.AddParameter("@Password", this.UserMessage.Password);
                db.AddParameter("@ClientID", this.UserMessage.ClientID);

                dt = db.ExecuteDataTable(spName, CommandType.StoredProcedure);
                if (dt.Rows.Count > 0)
                {
                    this.SMSUserID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    isActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    isUsingModem = Convert.ToBoolean(dt.Rows[0]["IsUsingModem"]);
                    this.ConfigMessage.IsUsingModem = isUsingModem;
                    this.ConfigMessage.DestinationNo = this.UserMessage.DestinationNo;
                    this.ConfigMessage.BodyMassage = this.UserMessage.BodyMessage;
                    this.ConfigMessage.IsActive = isActive;
                }
                else {
                    this.ResultMessage.Code = "-3:Credential";
                    this.ResultMessage.Message = "Invalid Credential";
                    this.ResultMessage.Status = "Fail";

                    this.SMSUserID = -1;
                    return false;
                }

                if (!isActive)
                {
                    this.ResultMessage.Code = "-2:Credential";
                    this.ResultMessage.Message = "Account is Expired";
                    this.ResultMessage.Status = "Fail";
                    this.SMSUserID = 0;
                    this.ConfigMessage = null;
                    return false;
                }
                if (!isUsingModem)
                {
                    this.ConfigMessage.APIUrl = Convert.ToString(dt.Rows[0]["APIUrl"]);
                    this.ConfigMessage.APIUser = Convert.ToString(dt.Rows[0]["APIUser"]);
                    this.ConfigMessage.APIPassword = Convert.ToString(dt.Rows[0]["APIPassword"]);

                    return true;
                }
                else
                {
                    spName = "SP_OUTBOX";
                    db.ClearParameter();

                    db.AddParameter("@DestinationNumber", this.UserMessage.DestinationNo);
                    db.AddParameter("@SmsText", this.UserMessage.BodyMessage);
                    db.AddParameter("@action", "INSERT");
                    db.AddParameter("@Category", "API");
                    db.AddParameter("@Status", "UNSENT");
                    db.ExecuteNonQuery(spName, CommandType.StoredProcedure);
                    this.ResultMessage.Code = "0:Normal";
                    this.ResultMessage.Message = "SMS will be Send using Modem";
                    this.ResultMessage.Status = "OK";
                    //insert ke OutBox pake modem biasa
                    return false;
                }
	        }   
        }

        private void InsertSMSOutbox()
        {
            try
            {
                String strQuery = "InsertIntoSMSOutbox";
                using (GlobalDatabase db = new GlobalDatabase(this.dbConn))
                {
                    db.ClearParameter();
                    db.AddOutputParameter("@ID", 0);
                    db.AddParameter("@SMSUserID", this.SMSUserID);
                    db.AddParameter("@DestinationNumber", this.UserMessage.DestinationNo);
                    db.AddParameter("@Message", this.ConfigMessage.BodyMassage);
                    db.AddParameter("@IsUsingModem", this.ConfigMessage.IsUsingModem);

                    db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                    var res = db.RetrieveParamterOutput();
                    
                    this.SMSOutboxID = (int)res["ID"];
                }
            }
            catch (Exception ex){ throw; }
        }

        private void InsertIntoSMSLog(string par, int smsoutboxid)
        {
            try
            {
                string strQuery = "InsertIntoSMSLog";
                using (GlobalDatabase db = new GlobalDatabase(this.dbConn))
                {
                    db.ClearParameter();
                    db.AddOutputParameter("@id", 0);
                    db.AddParameter("@smsoutboxID", smsoutboxid);
                    db.AddParameter("@eventlog", par);

                    db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}