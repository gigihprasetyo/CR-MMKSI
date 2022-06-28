using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.BusinessFacade.PO;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.BusinessFacade.Service;
using KTB.DNet.Domain.Search;
using System.Net;
using System.Net.Http;
using KTB.DNET.BusinessFacade.Service;

namespace KTB.DNet.SFIntegration.Parser
{
    public static class ServiceReminderParser
    {
        public static ServiceReminder Parse(DataRow row)
        {
            List<ServiceReminder> arrSvcReminder = new List<ServiceReminder>();
            var tempDealerBranch = new DealerBranch();
            var tempActualDealer = new Dealer();
            var tempActualDealerBranch = new DealerBranch();
            var tempChassisMaster = new ChassisMaster();
            var tempCategory = new Category();
            var tempAssistServiceIncoming = new AssistServiceIncoming();
            var tempDealer = new Dealer();
            var tempPMKind = new PMKind();

            ServiceReminder temp = new ServiceReminder();

            if (row["SalesforceID"] != DBNull.Value) { temp.SalesforceID = row["SalesforceID"].ToString(); }
            if (row["DealerID"] != DBNull.Value) { tempDealer.ID = (int)row["DealerID"]; temp.Dealer = tempDealer; }
            if (row["DealerBranchID"] != DBNull.Value) { tempDealerBranch.ID = (int)row["DealerBranchID"]; temp.DealerBranch = tempDealerBranch; }
            if (row["ChassisNumber"] != DBNull.Value) { temp.ChassisNumber = row["ChassisNumber"].ToString(); }
            if (row["EngineNumber"] != DBNull.Value) { temp.EngineNumber = row["EngineNumber"].ToString(); }
            if (row["ChassisMasterID"] != DBNull.Value){ tempChassisMaster.ID = (int)row["ChassisMasterID"]; temp.ChassisMaster = tempChassisMaster;}                  
            if (row["VehicleType"] != DBNull.Value){ temp.VehicleType = row["VehicleType"].ToString();}
            if (row["CategoryID"] != DBNull.Value) { tempCategory.ID = (short)row["CategoryID"]; temp.Category = tempCategory; }
            if (row["ServiceReminderDate"] != DBNull.Value){ temp.ServiceReminderDate = Convert.ToDateTime(row["ServiceReminderDate"]);}
            if (row["MaxFUDealerDate"] != DBNull.Value){ temp.MaxFUDealerDate = Convert.ToDateTime(row["MaxFUDealerDate"]);}
            if (row["BookingDate"] != DBNull.Value){ temp.BookingDate = Convert.ToDateTime(row["BookingDate"]);}
            if (row["BookingTime"] != DBNull.Value){ temp.BookingTime = row["BookingTime"].ToString();}
            if (row["CaseNumber"] != DBNull.Value){ temp.CaseNumber = row["CaseNumber"].ToString();}
            if (row["CustomerName"] != DBNull.Value){ temp.CustomerName = row["CustomerName"].ToString();}
            if (row["CustomerPhoneNumber"] != DBNull.Value){ temp.CustomerPhoneNumber = row["CustomerPhoneNumber"].ToString();}
            if (row["PMKindID"] != DBNull.Value){ temp.PMKind = new PMKind((int)row["PMKindIDNext"]);}
            if (row["TransactionType"] != DBNull.Value){ temp.TransactionType = (byte)row["TransactionType"];}
            //if (row["AssistServiceIncomingID"] != DBNull.Value){ tempAssistServiceIncoming.ID = (int)row["AssistServiceIncomingID"]; temp.AssistServiceIncoming = tempAssistServiceIncoming;}
            if (row["WONumber"] != DBNull.Value){ temp.WONumber = row["WONumber"].ToString();}
            if (row["ServiceActualDate"] != DBNull.Value){ temp.ServiceActualDate = Convert.ToDateTime(row["ServiceActualDate"]);}
            if (row["ActualKM"] != DBNull.Value){ temp.ActualKM = (int)row["ActualKM"];}
            if (row["ActualServiceDealerID"] != DBNull.Value){ tempActualDealer.ID = (int)row["ActualServiceDealerID"]; temp.ActualServiceDealer = tempActualDealer;}
            if (row["ActualServiceDealerBranchID"] != DBNull.Value){ tempActualDealerBranch.ID = (int)row["ActualServiceDealerBranchID"]; temp.ActualServiceDealerBranch = tempActualDealerBranch;}
            if (row["PKTDate"] != DBNull.Value){ temp.PKTDate = Convert.ToDateTime(row["PKTDate"]);}
            if (row["Remark"] != DBNull.Value){ temp.Remark = row["Remark"].ToString();}
            if (row["Status"] != DBNull.Value){ temp.Status = (byte)row["Status"];}                    
            if (row["RowStatus"] != DBNull.Value){ temp.RowStatus = (short)row["RowStatus"];}
            if (row["CreatedBy"] != DBNull.Value) { temp.CreatedBy = row["CreatedBy"].ToString(); }                    
            if (row["CreatedTime"] != DBNull.Value){ temp.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);}
            if (row["LastUpdateBy"] != DBNull.Value){ temp.LastUpdateBy = row["LastUpdateBy"].ToString();}    
            if (row["LastUpdateTime"] != DBNull.Value){ temp.LastUpdateTime = Convert.ToDateTime(row["LastUpdateTime"]);}

            return temp;
        }

        public static KTB.DNet.SFIntegration.Model.ServiceReminder ParseToSFObj(DataRow row, GenericPrincipal User)
        {
            var sFObj = new KTB.DNet.SFIntegration.Model.ServiceReminder();

            //var svcReminderTbl = svcReminderDataSet.Tables[0];
            var dealer = new DealerFacade(User).Retrieve((int)row["DealerID"]);
            var pMKind = new PMKindFacade(User).Retrieve((int)row["PMKindID"]);

            sFObj.salesforce_id = row["SalesforceID"].ToString();
            sFObj.Chassis_Number__c = row["ChassisNumber"].ToString();
            sFObj.Customer_Name__c = row["CustomerName"].ToString();
            sFObj.Customer_Phone__c = row["CustomerPhoneNumber"].ToString();
            sFObj.Dealer__r = new KTB.DNet.SFIntegration.Model.DealerSF() { Code__c = dealer.DealerCode };
            sFObj.Description__c = row["Remark"].ToString();
            sFObj.Engine_Number__c = row["EngineNumber"].ToString();
            sFObj.Service_Type__c = pMKind.KindDescription;
                
            int status = (byte)row["Status"];
            if (status == 3 || status == 5 || status == 6)
                sFObj.Service_Status__c = "Complete";
            else
                sFObj.Service_Status__c = "New";

            if (sFObj.salesforce_id.Length > 10)
                sFObj.Date_Sync_With_Dnet__c = DateTime.Now.ToString("yyyy-MM-dd");

            sFObj.Car__r = new KTB.DNet.SFIntegration.Model.Car__r() { Code__c = row["VechileTypeCode"].ToString() };

            return sFObj;
        }
    
        public static List<KTB.DNet.SFIntegration.Model.chassisHistoricalData> ParseHistoricalSvcReminder(DataTable datas)
        {
            var historicalData = new List<KTB.DNet.SFIntegration.Model.chassisHistoricalData>();

            foreach(DataRow row in datas.Rows)
            {
                var temp = new KTB.DNet.SFIntegration.Model.chassisHistoricalData();
                temp.ActualKM = (int)row["ActualKM"];
                temp.DeltaDays = (int)row["DeltaDays"];
                historicalData.Add(temp);
            }

            return historicalData;
        }
    
        public static List<PMKind> ParsePMKind(DataTable datas)
        {
            List<PMKind> list = new List<PMKind>();

            foreach(DataRow row in datas.Rows)
            {
                PMKind temp = new PMKind();
                if (row["ID"] != DBNull.Value) { temp.ID = (int)row["ID"]; }
                if (row["KindCode"] != DBNull.Value) { temp.KindCode = row["KindCode"].ToString(); }
                if (row["KM"] != DBNull.Value) { temp.KM = (int)row["KM"]; }
                if (row["KindDescription"] != DBNull.Value) { temp.KindDescription = row["KindDescription"].ToString(); }

                list.Add(temp);
            }

            return list;
        }
    }
}
