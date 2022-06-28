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
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using System.Collections;
using KTB.DNET.BusinessFacade;
using KTB.DNet.Domain.Search;

namespace KTB.DNet.SFIntegration.BusinessLogic
{
    public static class TeleSurveyMaster
    {

        private class TeleSurveySFWithID
        {
            public TeleSurveySFWithID()
            {
                SurveyData = new TeleSurveySF();
            }
            public string ID = "";
            public TeleSurveySF SurveyData;
        }

        public static string Message { get; set; }
        public static bool IsSuccess { get; set; }

        public static void GetTeleSurveyMasterData()
        {

            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetSF"), null);
            DealerFacade DF = new DealerFacade(User);
            string SP_GetData = "sp_GetTeleSurvey_SalesForce";
            DataTable dt = new DataTable();
            dt = DF.RetrieveUsingSP(SP_GetData);
            List<TeleSurveySFWithID> listTeleSurvey = new List<TeleSurveySFWithID>();
            foreach (DataRow row in dt.Rows)
            {
                TeleSurveySFWithID data = new TeleSurveySFWithID();
                data.ID = row["ID"].ToString();        
                data.SurveyData.Caller_type__c = row["Caller_type__c"].ToString();
                data.SurveyData.Car_Type__r.Code__c = row["Car_Type__r"].ToString();
                data.SurveyData.Dealer__r.Code__c = row["Dealer__r"].ToString();
                data.SurveyData.SuppliedName = row["SuppliedName"].ToString();
                data.SurveyData.SuppliedPhone = row["SuppliedPhone"].ToString();
                data.SurveyData.Customer_Address__c = row["Customer_Address__c"].ToString();
                data.SurveyData.Origin = row["Origin"].ToString();
                data.SurveyData.Plate_Number__c = row["Plate_Number__c"].ToString();
                data.SurveyData.Chassis_Number__c = row["Chassis_Number__c"].ToString();
                data.SurveyData.Engine_Number__c = row["Engine_Number__c"].ToString();
                data.SurveyData.Category__c = row["Category__c"].ToString();
                data.SurveyData.Sub_Category_1__c = row["Sub_Category_1__c"].ToString();
                data.SurveyData.Sub_Category_2__c = row["Sub_Category_2__c"].ToString();
                data.SurveyData.Sub_Category_3__c = row["Sub_Category_3__c"].ToString();
                data.SurveyData.Sub_Category_4__c = row["Sub_Category_4__c"].ToString();
                data.SurveyData.Description = row["Description"].ToString();
                data.SurveyData.Subject = row["Subject"].ToString();
                listTeleSurvey.Add(data);
            }

            if (listTeleSurvey.Count > 0)
            {

                // List<TeleSurveySF> listToProcess = Grouping100Data(listTeleSurvey);
                string vReturn;
                string msg;
                string logCategory = "K:SALESFORCEINSERTTELESURVEY\n";
                string strJson;
                bool IsError = false;


                foreach (TeleSurveySFWithID data in listTeleSurvey)
                {
                    //strJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    MainParser.SendData(User, String.Concat("services/apexrest/", TeleSurveySF.SObjectTypeName), data.SurveyData).Wait();
                    vReturn = MainParser.IsSuccess.ToString();
                    msg = MainParser.Message.ToString();

                    if (vReturn.ToLower() == "false" && msg.Contains("Car__c"))
                    {

                        TeleSurveyWithoutCarTypeSF dataWithoutCarType = new TeleSurveyWithoutCarTypeSF();
                        dataWithoutCarType.Clone(data.SurveyData);
                        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(dataWithoutCarType);
                        MainParser.SendData(User, String.Concat("services/apexrest/", TeleSurveySF.SObjectTypeName), dataWithoutCarType).Wait();
                        vReturn = MainParser.IsSuccess.ToString();
                        msg = MainParser.Message.ToString();
                    }

                    CreateLog(vReturn, msg, logCategory, data.ID);
                    ChangeStatusCcComplaint(data.ID, vReturn, msg);

                    if (vReturn.ToLower() == "false")
                    {
                        IsError = true;
                    }
                }

                if (IsError == true)
                {
                    throw new Exception("Please see wslog for further information.");
                }

            }
        }

        private static void ChangeStatusCcComplaint(string processedID, string vReturn, string msg)
        {
             var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            string[] listID = processedID.Split(',');
            CcComplaintFacade facade = new CcComplaintFacade(User);

            foreach (string id in listID)
            {
                CcComplaint data = facade.Retrieve(Convert.ToInt32(id));
                if (data.ID != 0)
                {
                    if(vReturn.ToLower() == "false")
                    {
                        data.Status = Convert.ToInt16(CcComplaint.EnumStatus.CANCEL_SALESFORCE);
                        facade.Update(data);
                    }
                    else
                    {
                        data.Status = Convert.ToInt16(CcComplaint.EnumStatus.SENT_SALESFORCE);
                        data.Salesforceid = msg;
                        facade.Update(data);
                    }
                }
            }
            

        }

        //private static List<TeleSurveySF> Grouping100Data(List<TeleSurveySF> listTeleSurvey)
        //{
        //    List<TeleSurveySF> result = new List<TeleSurveySF>();

        //    foreach (TeleSurveySF data in listTeleSurvey)
        //    {
        //        if (result.Count < 100)
        //        {
        //            List<TeleSurveySF> SameGroup = listTeleSurvey.Where(x => x.SuppliedPhone.ToLower().Trim() == data.SuppliedPhone.ToLower().Trim()
        //          && x.Sub_Category_1__c.ToLower().Trim() == data.Sub_Category_1__c.ToLower().Trim()
        //          && x.Sub_Category_2__c.ToLower().Trim() == data.Sub_Category_2__c.ToLower().Trim()).ToList();

        //            if(SameGroup == null)
        //            {
        //                result.Add(data);
        //            }
        //            else
        //            {
        //                TeleSurveySF GroupedData = new TeleSurveySF();
        //                GroupedData = SameGroup[0];
        //                GroupedData.Description = string.Empty;


        //                foreach (TeleSurveySF SameData in SameGroup)
        //                {
        //                    GroupedData.Description += SameData.Description + ",";
        //                }

        //                GroupedData.Description = GroupedData.Description.Remove(GroupedData.Description.Length - 1);

        //                result.Add(GroupedData);
        //            }

        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }

        //    return result;
        //}



        static void CreateLog(string vReturn, string msg, string logCategory, string strJson)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            WsLog wslog = new WsLog();
            wslog.Source = "Internal";
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
