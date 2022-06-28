using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using KTB.DNet.WebAPI.SMSGetway.Models;
using KTB.DNet.WebAPI.SMSGetway.Providers;
using KTB.DNet.WebAPI.SMSGetway.Results;
using DNET.Monitoring.DAL;
using System.Data;
using System.Web.Configuration;
using System.Net;
using System.Text;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Specialized;
using KTB.DNet.WebAPI.SMSGetway.Helpers;
using KTB.DNet.WebAPI.SMSGetway.Helpers.Interfaces;
using System.Reflection;
using System.Diagnostics;

namespace KTB.DNet.WebAPI.SMSGetway.Controllers
{
    public class SMSController : ApiController
    {
        static DBConnection strConnString;
        static SMSResult objResult;
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpPost]
        public SMSResult Post(SMSUser obj)
        {
            int idSMSUser = 0;
            SMSConfig objConf = new SMSConfig();
            objResult = new SMSResult();
            objResult.Message = "";
            try
            {
                if (ProcessingParamter(obj, ref idSMSUser, ref objConf))
                {
                    Task.Run(() => ProcessingToSMSGateWay(objConf, idSMSUser)).Wait();
                }

            }
            catch (Exception ex)
            {
                objResult.Code = "-1:Errorr Hanlder";
                objResult.Status = "Fail";
                objResult.Message += " " + ex.Message.ToString();

            }

            return objResult;
        }

        //[HttpPost]
        //public SMSResult PostNew(SMSUser obj)
        //{
        //    IServiceMessage<SMSResult> sms = new SMSHelper(obj);
        //    sms.SendMessage();

        //    return sms.GetResponse;
        //}


        private static async Task ProcessingToSMSGateWay(SMSConfig objPar, int idSMSUser)
        {
            int id = 0;
            objResult = new SMSResult();
            try
            {
                insertIntoSMSOutbox(objPar, idSMSUser, ref id);

                //var json = JsonConvert.SerializeObject(objPar);
                //var data = new StringContent(json, Encoding.UTF8, "application/json");

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("u", objPar.APIUser));
                nvc.Add(new KeyValuePair<string, string>("p", objPar.APIPassword));
                nvc.Add(new KeyValuePair<string, string>("d", objPar.DestinationNo));
                nvc.Add(new KeyValuePair<string, string>("m", objPar.BodyMassage));

                var url = objPar.APIUrl;
                var client = new HttpClient();


                string strProx = WebConfigurationManager.AppSettings["ProxyAddress"];
                string strPorxyPort = WebConfigurationManager.AppSettings["ProxyPort"];
                if (strProx != null && strProx != "")
                {
                    System.Net.WebProxy wp = new System.Net.WebProxy(strProx, Convert.ToInt32(strPorxyPort));
                    var httpClientHandler = new HttpClientHandler
                    {
                        Proxy = wp,
                    };

                    client = new HttpClient(handler: httpClientHandler, disposeHandler: true);

                }
                
                var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };
                var res = await client.SendAsync(req);
                // var response = await client.PostAsync(url, data);

                string result = res.Content.ReadAsStringAsync().Result;

                
                if (res.IsSuccessStatusCode && !String.IsNullOrEmpty(result))
                {
                    UpdateSMSOutbox(id, result);
                }else
                {
                    objResult.Code = "-3:HTTP Error";
                    objResult.Message = string.Format("Error occurred, the status code is: {0}", res.StatusCode);
                    objResult.Status = "Fail";
                }

            }
            catch (Exception ex)
            {
                InsertIntoSMSLog(ex.Message, id);

            }

        }

        private static void UpdateSMSOutbox(int id, string APIResponse)
        {
            try
            {
                setConn();

                string TransactionID = APIResponse.Split('-')[1].ToString();
                string status = APIResponse.Split('-')[0].ToString();

                String strQuery = "UpdateSMSOutbox";
                using (GlobalDatabase db = new GlobalDatabase(strConnString))
                {
                    db.ClearParameter();
                    db.AddParameter("@id", id);
                    db.AddParameter("@APIResponse", APIResponse);
                    db.AddParameter("@TransactionID", TransactionID);
                    db.AddParameter("@status", status);

                    db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                }
                switch (status)
                {
                    case "0":
                        objResult.Code = "0:Normal";
                        objResult.Message = string.Format("SMS has been sent to SMS Gateway  TRID : {0} ", TransactionID);
                        objResult.Status = "OK";
                        break;
                    case "1":
                        objResult.Code = "1:Normal";
                        objResult.Message = string.Format("Invalid Parameter  {0}", TransactionID);
                        objResult.Status = "Fail";
                        break;
                    case "2":
                        objResult.Code = "2:Normal";
                        objResult.Message = string.Format("Internal Error  {0} ", TransactionID);
                        objResult.Status = "Fail";
                        break;
                    case "3":
                        objResult.Code = "3:Normal";
                        objResult.Message = string.Format("Invalid MSISDN / Destination  {0} ", TransactionID);
                        objResult.Status = "Fail";
                        break;
                    case "4":
                        objResult.Code = "4:Normal";
                        objResult.Message = string.Format("Invalid Username / Password  {0} ", TransactionID);
                        objResult.Status = "Fail";
                        break;
                    case "5":
                        objResult.Code = "5:Normal";
                        objResult.Message = string.Format("Membership is not available  {0} ", TransactionID);
                        objResult.Status = "Fail";
                        break;
                    case "6":
                        objResult.Code = "6:Normal";
                        objResult.Message = string.Format("IP is not whitelist  {0} ", TransactionID);
                        objResult.Status = "Fail";
                        break;
                    case "7":
                        objResult.Code = "7:Normal";
                        objResult.Message = string.Format("Not enough credit  {0} ", TransactionID);
                        objResult.Status = "Fail";
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

        private static void setConn()
        {
            strConnString = new DBConnection();
            strConnString.DataSource = WebConfigurationManager.AppSettings["DataSource"];
            strConnString.DBName = WebConfigurationManager.AppSettings["DBName"];
            strConnString.UserName = WebConfigurationManager.AppSettings["UserName"];
            strConnString.Password = WebConfigurationManager.AppSettings["Password"];
            strConnString.DBType = EnumDBType.DBType.SqlServer;
        }

        private static void insertIntoSMSOutbox(SMSConfig objPar, int idSMSUser, ref int id)
        {
            try
            {
                setConn();
                String strQuery = "InsertIntoSMSOutbox";
                using (GlobalDatabase db = new GlobalDatabase(strConnString))
                {
                    db.ClearParameter();
                    db.AddOutputParameter("@ID", 0);
                    db.AddParameter("@SMSUserID", idSMSUser);
                    db.AddParameter("@DestinationNumber", objPar.DestinationNo);
                    db.AddParameter("@Message", objPar.BodyMassage);
                    db.AddParameter("@IsUsingModem", objPar.IsUsingModem);


                    db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                    var res = db.RetrieveParamterOutput();
                    id = (int)res["ID"];


                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void InsertIntoSMSLog(string par, int smsoutboxid)
        {

            try
            {
                setConn();

                String strQuery = "InsertIntoSMSLog";
                using (GlobalDatabase db = new GlobalDatabase(strConnString))
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

        private bool ProcessingParamter(SMSUser obj, ref int id, ref SMSConfig objConf)
        {
            setConn();

            DataTable dt = new DataTable();

            using (GlobalDatabase db = new GlobalDatabase(strConnString))
            {
                String strQuery = "UpGetSMSConfig";

                db.AddParameter("@UserName", obj.UserName);
                db.AddParameter("@Password", obj.Password);
                db.AddParameter("@ClientID", obj.ClientID);

                dt = db.ExecuteDataTable(strQuery, CommandType.StoredProcedure);

                bool isUsingModem = false;
                Boolean isActive = true;
                if (dt.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dt.Rows[0]["ID"]);
                    isActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    isUsingModem = Convert.ToBoolean(dt.Rows[0]["IsUsingModem"]);
                    objConf.IsUsingModem = isUsingModem;
                    objConf.DestinationNo = obj.DestinationNo;
                    objConf.BodyMassage = obj.BodyMessage;
                    objConf.IsActive = isActive;

                    //jika user tidak aktiv
                    if (!isActive)
                    {
                        objResult.Code = "-2:Credential";
                        objResult.Message = "Account is Expired";
                        objResult.Status = "Fail";
                        id = 0;
                        objConf = null;
                        return false;
                    }

                    if (!isUsingModem)
                    {
                        objConf.APIUrl = Convert.ToString(dt.Rows[0]["APIUrl"]);
                        objConf.APIUser = Convert.ToString(dt.Rows[0]["APIUser"]);
                        objConf.APIPassword = Convert.ToString(dt.Rows[0]["APIPassword"]);

                        return true;
                    }
                    else
                    {
                        strQuery = "SP_OUTBOX";
                        db.ClearParameter();

                        db.AddParameter("@DestinationNumber", obj.DestinationNo);
                        db.AddParameter("@SmsText", obj.BodyMessage);
                        db.AddParameter("@action", "INSERT");
                        db.AddParameter("@Category", "API");
                        db.AddParameter("@Status", "UNSENT");
                        db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                        objResult.Code = "0:Normal";
                        objResult.Message = "SMS will be Send using Modem";
                        objResult.Status = "OK";
                        //insert ke OutBox pake modem biasa
                        return false;
                    }
                }
                else
                {
                    objResult.Code = "-3:Credential";
                    objResult.Message = "Invalid Credential";
                    objResult.Status = "Fail";

                    id = -1;
                    return false;
                }
            }
            return false;
        }
    }
}