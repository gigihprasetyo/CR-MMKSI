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
    public static class DealerMaster
    {
        public static string Message { get; set; }
        public static bool IsSuccess { get; set; }

        public static void GetDealerMasterData()
        {
            
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetSF"), null);
            DealerFacade DF = new DealerFacade(User);
            string SP_GetData = "sp_GetDealerContact_SalesForce";
            DataTable dt = new DataTable();
            dt = DF.RetrieveUsingSP(SP_GetData);
            List<DealerMasterSF> listDealerContact = new List<DealerMasterSF>();
            foreach (DataRow row in dt.Rows)
            {
                DealerMasterSF data = new DealerMasterSF();
                data.Code__c = row["DealerCode"].ToString();
                data.CSO_Name__c = row["CSO_Name"].ToString();
                data.CSO_Email__c = row["CSO_Email"].ToString();
                data.CSO_Phone__c = row["CSO_Phone"].ToString();
                data.CSO_Mobile__c = row["CSO_Mobile"].ToString();
                data.CS_ASS_PC_Name__c = row["CSASS_Name"].ToString();
                data.CS_ASS_PC_Email__c = row["CSASS_Email"].ToString();
                data.CS_ASS_PC_Phone__c = row["CSASS_Phone"].ToString();
                data.CS_ASS_PC_Mobile__c = row["CSASS_Mobile"].ToString();
                data.CS_ASS_LCV_Name__c = row["CSASS_Name"].ToString();
                data.CS_ASS_LCV_Email__c = row["CSASS_Email"].ToString();
                data.CS_ASS_LCV_Phone__c = row["CSASS_Phone"].ToString();
                data.CS_ASS_LCV_Mobile__c = row["CSASS_Mobile"].ToString();
                data.CS_Sales_PC_Name__c = row["CSSales_Name"].ToString();
                data.CS_Sales_PC_Email__c = row["CSSales_Email"].ToString();
                data.CS_Sales_PC_Phone__c = row["CSSales_Phone"].ToString();
                data.CS_Sales_PC_Mobile__c = row["CSSales_Mobile"].ToString();
                data.CS_Sales_LCV_Name__c = row["CSSales_Name"].ToString();
                data.CS_Sales_LCV_Email__c = row["CSSales_Email"].ToString();
                data.CS_Sales_LCV_Phone__c = row["CSSales_Phone"].ToString();
                data.CS_Sales_LCV_Mobile__c = row["CSSales_Mobile"].ToString();
                data.Sales_Manager_Name__c = row["SalesManager_Name"].ToString();
                data.Sales_Manager_Email__c = row["SalesManager_Email"].ToString();
                data.Sales_Manager_Phone__c = row["SalesManager_Phone"].ToString();
                data.Sales_Manager_Mobile__c = row["SalesManager_Mobile"].ToString();
                data.Service_Manager_Name__c = row["ServiceManager_Name"].ToString();
                data.Service_Manager_Email__c = row["ServiceManager_Email"].ToString();
                data.Service_Manager_Phone__c = row["ServiceManager_Phone"].ToString();
                data.Service_Manager_Mobile__c = row["ServiceManager_Mobile"].ToString();
                data.Sparepart_Manager_Name__c = row["SparepartManager_Name"].ToString();
                data.Sparepart_Manager_Email__c = row["SparepartManager_Email"].ToString();
                data.Sparepart_Manager_Phone__c = row["SparepartManager_Phone"].ToString();
                data.Sparepart_Manager_Mobile__c = row["SparepartManager_Mobile"].ToString();
                data.Branch_Manager_Name__c = row["BranchManager_Name"].ToString();
                data.Branch_Manager_Email__c = row["BranchManager_Email"].ToString();
                data.Branch_Manager_Phone__c = row["BranchManager_Phone"].ToString();
                data.Branch_Manager_Mobile__c = row["BranchManager_Mobile"].ToString();
                data.name = row["DealerName"].ToString();

                int dealerId = Convert.ToInt32(row["DealerId"]);

                Dealer objDealer = DF.Retrieve(dealerId);
                //data.name = objDealer.DealerName;

                if (objDealer.DealerGroup == null)
                {
                    data.Group__c = "";
                }
                else
                {
                    data.Group__c = objDealer.DealerGroup.GroupName;
                }
                if (objDealer.Province == null){
                    data.Province__c = string.Empty;
                }
                else { data.Province__c = objDealer.Province.ProvinceName; }

                if (objDealer.City == null)
                {
                    data.City__c = string.Empty;
                }
                else { data.City__c = objDealer.City.CityName; }
                
                data.Address__c = objDealer.Address;
                data.Parent_Code__c = "";

                string strLayanan = "";
                if (objDealer.SalesUnitFlag == "1")
                {
                    strLayanan += "Sales";
                    if (objDealer.ServiceFlag == "1" || objDealer.SparepartFlag == "1") { strLayanan += ","; }
                }
                if (objDealer.ServiceFlag == "1")
                {
                    strLayanan += "Service";
                    if (objDealer.SparepartFlag == "1") { strLayanan += ","; }
                }
                if (objDealer.SparepartFlag == "1")
                {
                    strLayanan += "SparePart";
                }

                data.Layanan__c = strLayanan;
                data.Address__c = objDealer.Address;

                ArrayList arrCat = new ArrayList();
                DealerCategoryFacade dcFacade = new DealerCategoryFacade(User);
                CriteriaComposite cri = new CriteriaComposite(new Criteria(typeof(DealerCategory), "RowStatus", (short)(DBRowStatus.Active)));
                cri.opAnd(new Criteria(typeof(DealerCategory), "Dealer.ID", MatchType.Exact, (int)(objDealer.ID)));

                arrCat = dcFacade.Retrieve(cri);

                strLayanan = "";
                int x = 1;
                foreach (DealerCategory rowCat in arrCat)
                {
                    strLayanan += rowCat.Category.CategoryCode;
                    if (arrCat.Count > 1 && x != arrCat.Count)
                    {
                        strLayanan += ",";
                    }
                    x++;
                }

                data.Alokasi__c = strLayanan;
                data.Type__c = "Dealer";
                data.Telephone_1__c = objDealer.Phone;
                data.Telephone_2__c = "";
                data.Telephone_3__c = "";
                data.Telephone_4__c = "";
                data.Telephone_5__c = "";
                if (objDealer.Fax.Length > 5) { data.Fax__c = objDealer.Fax; }
                else { data.Fax__c = string.Empty; }
                data.Status__c = objDealer.StatusDealer;

                listDealerContact.Add(data);
            }

            if (listDealerContact.Count > 0)
            {
                string vReturn;
                string msg;
                string logCategory = "K:SALESFORCEINSERTDEALER\n";
                string strJson;
                foreach (DealerMasterSF data in listDealerContact)
                {
                    strJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    MainParser.SendData(User, String.Concat("services/apexrest/", DealerMasterSF.SObjectTypeName), data).Wait();
                    vReturn = MainParser.IsSuccess.ToString();
                    msg = MainParser.Message.ToString();
                    CreateLog(vReturn, msg, logCategory, strJson);
                }         
            }
        }

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
