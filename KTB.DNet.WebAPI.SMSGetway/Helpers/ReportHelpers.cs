using DNET.Monitoring.DAL;
using KTB.DNet.WebAPI.SMSGetway.Helpers.Interfaces;
using KTB.DNet.WebAPI.SMSGetway.Models;
using KTB.DNet.WebAPI.SMSGetway.Models.Sandeza;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace KTB.DNet.WebAPI.SMSGetway.Helpers
{
    public class ReportHelpers : IReport<MessageServicesResult>
    {
        MessageReport MsgReport;
        MessageServicesResult MsgResult;
        private string ClientIP;
        private DBConnection dbConn;

        public ReportHelpers(MessageReport msg, string clientIP)
        {
            this.dbConn = new DBConnection();
            this.dbConn.DataSource = WebConfigurationManager.AppSettings["DataSource"];
            this.dbConn.DBName = WebConfigurationManager.AppSettings["DBName"];
            this.dbConn.UserName = WebConfigurationManager.AppSettings["UserName"];
            this.dbConn.Password = WebConfigurationManager.AppSettings["Password"];
            this.dbConn.DBType = EnumDBType.DBType.SqlServer;

            this.ClientIP = clientIP;
            this.MsgReport = msg;
            this.MsgResult = new MessageServicesResult();
        }

        public void SetReport()
        {
            if (Validation())
            {
                string strQuery = "UpdateMSReport";
                string strLog = "up_InsertMessageServiceReport";

                using (GlobalDatabase db = new GlobalDatabase(this.dbConn))
                {

                    db.ClearParameter();
                    db.AddParameter("@RefID", this.MsgReport.ref_id);
                    db.AddParameter("@JsonBody", JsonConvert.SerializeObject(this.MsgReport));
                    db.AddParameter("@status", this.MsgReport.status);
                    db.AddParameter("@LastUpdateBy", this.ClientIP);

                    db.ExecuteNonQuery(strLog, CommandType.StoredProcedure);
                }


                using (GlobalDatabase db = new GlobalDatabase(this.dbConn))
                {

                    db.ClearParameter();
                    db.AddParameter("@RefID", this.MsgReport.ref_id);
                    db.AddParameter("@LastUpdateBy", this.ClientIP);
                    db.AddParameter("@Channel", this.MsgReport.channel);
                    db.AddParameter("@Status", this.MsgReport.status);
                    db.AddParameter("@Time", DateTime.ParseExact(this.MsgReport.time, "yyyy-MM-dd HH:mm:ss", null));

                    db.ExecuteNonQuery(strQuery, CommandType.StoredProcedure);
                }

                this.MsgResult.Code = "0";
                this.MsgResult.Status = "sucess";
            }
        }

        private bool ValidationMandatory()
        {
            string msgValidation = string.Empty;

            if (string.IsNullOrEmpty(MsgReport.ref_id))
            {
                msgValidation += "ref_id not empty, ";
            }
            if (string.IsNullOrEmpty(MsgReport.channel))
            {
                msgValidation += "channel not empty, ";
            }
            if (string.IsNullOrEmpty(MsgReport.status))
            {
                msgValidation += "status not empty, ";
            }
            if (string.IsNullOrEmpty(MsgReport.time))
            {
                msgValidation += "time not empty, ";
            }

            if (msgValidation.Length > 0)
            {
                this.MsgResult.Code = "-3:Credential";
                this.MsgResult.Message = msgValidation.Remove(msgValidation.Length - 2, 2);
                this.MsgResult.Status = "Fail";

                return false;
            }

            return true;
        }

        private bool Validation()
        {
            if (!Convert.ToBoolean(WebConfigurationManager.AppSettings["ValidationIP"]))
            {
                return ValidationMandatory();
            }
            using (GlobalDatabase db = new GlobalDatabase(dbConn))
            {
                string spName = "UpGetIP_Access";

                DataTable dt = db.ExecuteDataTable(spName, CommandType.StoredProcedure);
                if (dt.Rows.Count > 0)
                {
                    List<string> ListAccessIP = dt.Rows[0][0].ToString().Split(new string[] { ",", ";" },
                        StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (ListAccessIP.Where(x => x.Trim() == this.ClientIP).Count() > 0)
                    {
                        return ValidationMandatory();
                    }
                }

                this.MsgResult.Code = "-3:Credential";
                this.MsgResult.Message = "Invalid Credential";
                this.MsgResult.Status = "Fail";

                return false;
            }
        }

        public MessageServicesResult Result { get { return MsgResult; } }
    }
}