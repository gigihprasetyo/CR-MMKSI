using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using KTB.DNet.BusinessFacade.FinishUnit;
using System.Security.Principal;
using System.Data;
using KTB.DNet.SFIntegration.Model;
using KTB.DNet.SFIntegration.Parser;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;

namespace KTB.DNet.SFIntegration.BusinessLogic
{
    public static class VehicleMaster
    {
        public static string Message { get; set; }
        public static bool IsSuccess { get; set; }

        public static void GetVehicleMasterData()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetSF"), null);
            List<SqlParameter> Param = new List<SqlParameter>();
            string SP_VehicleMasterSF = "sp_GetVechileData_SalesForce";
            DataTable dt = new DataTable();
            dt = new VechileTypeFacade(User).RetrieveUsingSP(SP_VehicleMasterSF, Param);
            List<object> VehicleMasterSFList = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                VehicleMasterSF oVehicleMasterSF = new VehicleMasterSF();
                oVehicleMasterSF.Name = row["Name"].ToString();
                oVehicleMasterSF.Code__c = row["Code__c"].ToString();
                oVehicleMasterSF.Brand__c = row["Brand__c"].ToString();
                oVehicleMasterSF.Variant__c = row["Variant__c"].ToString();
                oVehicleMasterSF.Type__c = row["Type__c"].ToString();
                oVehicleMasterSF.Fuel_Type__c = row["Fuel_Type__c"].ToString();
                VehicleMasterSFList.Add(oVehicleMasterSF);
            }

            if (VehicleMasterSFList.Count > 0)
            {
                string vReturn;
                string msg;
                string logCategory = "K:SALESFORCEVEHICLEMASTER\n";
                string strJson;
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(VehicleMasterSFList);
                MainParser.SendData(User, String.Concat("services/apexrest/", VehicleMasterSF.SObjectTypeName), VehicleMasterSFList).Wait();
                vReturn = MainParser.IsSuccess.ToString();
                msg = MainParser.Message.ToString();
                CreateLog(vReturn, msg, logCategory, strJson);
            }
        }

        public static void SalesForceUpdateCase()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetSF"), null);
            List<SqlParameter> Param = new List<SqlParameter>();
            //string SP_VehicleMasterSF = "paramUpdateCase";
            DataTable dt = new DataTable();
            //dt = new VechileTypeFacade(User).RetrieveUsingSP(SP_VehicleMasterSF, Param);
            //List<object> VehicleMasterSFList = new List<object>();
            //foreach (DataRow row in dt.Rows)
            //{
            ParamUpdateCase objParam = new ParamUpdateCase();
            objParam.id = "5000I00001sY6HuQAK";
            objParam.status = "In Progress";
            objParam.Status_By_Dealer__c = "In Progress";
            objParam.comment = "Emasnya sudah ada dan akan kami infokan konsumen tersebut.";
            //}
            //List<object> paramUpdateCaseList = new List<object>();
            //paramUpdateCaseList.Add(objParam);
            //if (VehicleMasterSFList.Count > 0)
            //{
            MainParser.SendData(User, String.Concat("services/apexrest/", "paramUpdateCase"), objParam).Wait();
            //}
        }

        static void CreateLog(string vReturn, string msg, string logCategory, string strJson)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            WsLog wslog = new WsLog();
            wslog.Source = "172.17.31.63";
            wslog.Status = vReturn.ToString();
            wslog.Message = msg;
            wslog.Body = String.Concat(logCategory, strJson);
            wslog.RowStatus = 0;
            wslog.CreatedBy = "WebService";

            WsLogFacade wslogfacade = new WsLogFacade(User);
            wslogfacade.Insert(wslog);

        }

    }

}
