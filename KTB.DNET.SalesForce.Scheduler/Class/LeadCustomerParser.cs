using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.BusinessFacade.FinishUnit;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.BusinessFacade;
using KTB.DNet.Domain;
using KTB.DNet.WebApi.Models;
using KTB.DNet.Domain.Search;
using System.Text.RegularExpressions;
using System.Security.Principal;

namespace KTB.DNet.Salesforce.Class
{
    public class LeadCustomerParser
    {
        #region LeadCustomerProcess

        public void Process()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            //String sql = "select * from v_temp_to_sf";
            //String sql = "select ID from SPKHeader where SPKNumber in ('1709007627','1709007622','1709007619','1709007618')";
            String sql = "select ID from SPKHeader where SPKNumber = '1709006131'";

            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SPKHeader), "ID", MatchType.InSet, "(" + sql + ")"));
            SPKHeaderFacade facade = new SPKHeaderFacade(User);
            ArrayList arr = facade.Retrieve(criterias);

            ArrayList arrTypeCode = GetNewType();

            Console.WriteLine("Total SPK :" + arr.Count.ToString() + "\n");
            int iCount = 0;
            foreach (SPKHeader spk in arr)
            {
                iCount++;
                Console.WriteLine("Processing " + iCount.ToString() + " of " + arr.Count.ToString() + ". SPK Number : " + spk.SPKNumber + " at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");

                bool isSms = false;
                foreach (SPKDetail detail in spk.SPKDetails)
                {
                    foreach (VechileType type in arrTypeCode)
                    {
                        if (detail.VehicleTypeCode == type.VechileTypeCode)
                        {
                            isSms = true;
                            break;
                        }
                    }
                }

                if (spk.SPKCustomer.SAPCustomer != null)
                {

                    CriteriaComposite critResponse = new CriteriaComposite(new Criteria(typeof(SAPCustomerResponse), "RowStatus", MatchType.Exact, (short)(DBRowStatus.Active)));
                    critResponse.opAnd(new Criteria(typeof(SAPCustomerResponse), "SAPCustomer.ID", MatchType.Exact, spk.SPKCustomer.SAPCustomer.ID));
                    critResponse.opAnd(new Criteria(typeof(SAPCustomerResponse), "Status", MatchType.Exact, (int)(EnumSAPCustomerResponse.SAPCustomerResponse.SPK)));
                    ArrayList arrResponse = new KTB.DNET.BusinessFacade.SAPCustomerResponseFacade(User).Retrieve(critResponse);

                    SAPCustomerResponse response = new SAPCustomerResponse();
                    if (arrResponse.Count == 0)
                    {
                        response.SAPCustomer = spk.SPKCustomer.SAPCustomer;
                        response.Status = (int)(EnumSAPCustomerResponse.SAPCustomerResponse.SPK);
                        KTB.DNET.BusinessFacade.SAPCustomerResponseFacade responseFacade = new KTB.DNET.BusinessFacade.SAPCustomerResponseFacade(User);
                        int id = responseFacade.Insert(response);

                        response = new SAPCustomerResponse();
                        response = responseFacade.Retrieve(id);
                    }
                    else
                    {
                        response = (SAPCustomerResponse)(arrResponse[0]);
                    }

                    bool vResult = true;
                    string msg = "";
                    if (spk.SPKCustomer.SAPCustomer.SalesforceID != "")
                    {
                        vResult = UpdateOpportunity(spk, isSms, ref msg);
                    }
                    else
                    {
                        vResult = InsertOpportunity(spk, isSms, ref msg);
                        Console.WriteLine("------> Process tansfer SPK : " + spk.SPKNumber + " " + vResult.ToString() + " err : " + msg + "\n");
                    }

                    if (vResult)
                    {
                        response.IsSend = 1;
                        KTB.DNET.BusinessFacade.SAPCustomerResponseFacade responseFacade = new KTB.DNET.BusinessFacade.SAPCustomerResponseFacade(User);
                        int iResult = responseFacade.Update(response);
                    }
                    else
                    {

                    }
                }
            }
            Console.WriteLine("Process tansfer selesai !" + "\n");
            //Console.ReadLine();
        }

        public static bool InsertOpportunity(SPKHeader spk, bool isSms, ref string msg)
        {
            bool vReturn = false;
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            string strJson = "";
            try
            {
                var objParam = new KTB.DNet.WebApi.Models.paramWalkinOpportunity();

                objParam.Dealer_Code__c = spk.Dealer.DealerCode;
                objParam.Dealer_Name__c = spk.Dealer.DealerName;
                objParam.Consumen_Type__c = EnumTipePelangganCustomerRequest.RetrieveTipePelangganCustomerRequest(spk.SPKCustomer.TipeCustomer);
                objParam.Name = spk.SPKCustomer.Name1;
                objParam.Address__c = spk.SPKCustomer.Alamat;
                if (EmailAddressCheck(spk.SPKCustomer.Email))
                { objParam.Email__c = spk.SPKCustomer.Email; }
                else
                { objParam.Email__c = String.Empty; }
                objParam.Gender__c = String.Empty;
                objParam.Mobile_Phone__c = String.Empty;
                objParam.ID_Type__c = String.Empty;
                objParam.ID_Number__c = String.Empty;
                objParam.MMKSI_WEB_ID__c = String.Empty;

                if (spk.SPKCustomer.SPKCustomerProfiles.Count > 0)
                {
                    foreach (SPKCustomerProfile _profiles in spk.SPKCustomer.SPKCustomerProfiles)
                    {
                        if (_profiles.ProfileHeader.ID == 27) { objParam.Gender__c = (_profiles.ProfileValue == "LK" ? "Male" : "Female"); }

                        if (_profiles.ProfileHeader.ID == 29)
                        {
                            EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest tipeCustomer = (EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest)(spk.SPKCustomer.TipeCustomer);
                            if (tipeCustomer == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan)
                            {
                                objParam.ID_Type__c = "KTP";
                            }
                            else
                            {
                                objParam.ID_Type__c = "TDP";
                            }
                            objParam.ID_Number__c = _profiles.ProfileValue;
                        }
                        if (_profiles.ProfileHeader.ID == 30)
                        {
                            objParam.Mobile_Phone__c = _profiles.ProfileValue;
                        }
                    }
                }
                else
                {
                    if (spk.SPKCustomer.SAPCustomer != null)
                    {
                        objParam.Gender__c = (spk.SPKCustomer.SAPCustomer.Sex == 1 ? "Male" : "Female");
                        objParam.Mobile_Phone__c = spk.SPKCustomer.HpNo;
                    }
                }
                if (spk.SPKCustomer.SAPCustomer != null)
                {
                    objParam.Information_Type__c = EnumInformationType.GetStringInformationType(spk.SPKCustomer.SAPCustomer.InformationType);
                    objParam.Customer_Purposes__c = EnumCustomerPurpose.GetStringCustomerPurpose(spk.SPKCustomer.SAPCustomer.CustomerPurpose);
                    objParam.LeadSource = EnumInformationSource.GetStringInformationSource(spk.SPKCustomer.SAPCustomer.InformationSource);
                    if (spk.SPKCustomer.SAPCustomer.ProspectDate == null)
                    { objParam.Prospect_Date__c = spk.SPKCustomer.SAPCustomer.CreatedTime.ToString("yyyy-MM-dd"); }
                    else
                    { objParam.Prospect_Date__c = spk.SPKCustomer.SAPCustomer.ProspectDate.ToString("yyyy-MM-dd"); }
                    if (spk.SPKCustomer.SAPCustomer.SalesforceID != "")
                    { objParam.MMKSI_WEB_ID__c = spk.SPKCustomer.SAPCustomer.WebID; }
                    else { objParam.MMKSI_WEB_ID__c = string.Empty; }
                }
                else
                {
                    objParam.Information_Type__c = String.Empty;
                    objParam.Customer_Purposes__c = String.Empty;
                    objParam.LeadSource = String.Empty;
                    objParam.Prospect_Date__c = spk.SPKCustomer.CreatedTime.ToString("yyyy-MM-dd");
                    objParam.MMKSI_WEB_ID__c = string.Empty;
                }

                objParam.SPK_No__c = spk.SPKNumber;
                objParam.Dealer_SPK_No__c = spk.DealerSPKNumber;
                objParam.SPK_Status__c = "Closed Won";
                objParam.Validation_Key__c = spk.ValidationKey;

                objParam.StageName = EnumSAPCustomerResponse.GetStringValue((int)(EnumSAPCustomerResponse.SAPCustomerResponse.SPK));
                objParam.CloseDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                objParam.AccountID = GetAccount();
                objParam.Is_Valid_To_Send_SMS__c = (isSms == true ? "true" : "false");

                if (spk.SPKDetails.Count > 0)
                {
                    SPKDetail _spkDetail = (SPKDetail)(spk.SPKDetails[0]);
                    if (_spkDetail.VechileColor.VechileType != null)
                    {
                        objParam.Car_Code__c = _spkDetail.VechileColor.VechileType.VechileTypeCode;
                        objParam.Car_Type__c = _spkDetail.VechileColor.VechileType.Description;
                    }
                    else
                    {
                        objParam.Car_Code__c = _spkDetail.VehicleTypeCode;
                        VechileType objVT = new VechileTypeFacade(User).Retrieve(_spkDetail.VehicleTypeCode);
                        if (objVT != null)
                        { objParam.Car_Type__c = objVT.Description; }
                        else { objParam.Car_Type__c = String.Empty; }
                    }
                    objParam.Quantity__c = (string)_spkDetail.Quantity.ToString();
                }
                else
                {
                    if (spk.SPKCustomer.SAPCustomer != null)
                    {
                        if (spk.SPKCustomer.SAPCustomer.VechileType != null)
                        {
                            objParam.Car_Code__c = spk.SPKCustomer.SAPCustomer.VechileType.VechileTypeCode;
                            objParam.Car_Type__c = spk.SPKCustomer.SAPCustomer.VechileType.Description;
                        }
                        else
                        {
                            objParam.Car_Code__c = spk.SPKCustomer.SAPCustomer.VehicleTypeCode;
                            VechileType objVT = new VechileTypeFacade(User).Retrieve(spk.SPKCustomer.SAPCustomer.VehicleTypeCode);
                            if (objVT != null)
                            { objParam.Car_Type__c = objVT.Description; }
                            else { objParam.Car_Type__c = String.Empty; }
                        }
                        objParam.Quantity__c = (string)(spk.SPKCustomer.SAPCustomer.Qty.ToString());
                    }
                    else
                    {
                        objParam.Car_Code__c = String.Empty;
                        objParam.Car_Type__c = String.Empty;
                        objParam.Quantity__c = String.Empty;
                    }
                }

                Task.Run(
                () => KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", paramWalkinOpportunity.SObjectTypeName), objParam)
            ).Wait();

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

            }
            catch
            {
                vReturn = false;
            }

            vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
            msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;

            #region Log
            CreateLog(vReturn.ToString(), msg.ToString(), "K:SALESFORCEINSERTOPPORTUNITY\n", strJson);
            #endregion

            return vReturn;
        }

        public static bool UpdateOpportunity(SPKHeader spk, bool isSms, ref string msg)
        {
            bool vReturn = false;
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            string strJson = "";
            try
            {
                var objParam = new KTB.DNet.WebApi.Models.paramUpdateOpportunity();
                objParam.id = spk.SPKCustomer.SAPCustomer.SalesforceID;
                objParam.Dealer_Code__c = spk.Dealer.DealerCode;
                objParam.Dealer_Name__c = spk.Dealer.DealerName;

                objParam.SPK_No__c = spk.SPKNumber;
                objParam.Dealer_SPK_No__c = spk.DealerSPKNumber;
                objParam.SPK_Status__c = "Closed Won";
                objParam.Validation_Key__c = spk.ValidationKey;

                objParam.StageName = EnumSAPCustomerResponse.GetStringValue((int)(EnumSAPCustomerResponse.SAPCustomerResponse.SPK));
                objParam.Is_Valid_To_Send_SMS__c = (isSms == true ? "true" : "false");

                if (spk.SPKDetails.Count > 0)
                {
                    SPKDetail _spkDetail = (SPKDetail)(spk.SPKDetails[0]);
                    if (_spkDetail.VechileColor.VechileType != null)
                    {
                        objParam.Car_Code__c = _spkDetail.VechileColor.VechileType.VechileTypeCode;
                        objParam.Car_Type__c = _spkDetail.VechileColor.VechileType.Description;
                    }
                    else
                    {
                        objParam.Car_Code__c = _spkDetail.VehicleTypeCode;
                        VechileType objVT = new VechileTypeFacade(User).Retrieve(_spkDetail.VehicleTypeCode);
                        if (objVT != null)
                        { objParam.Car_Type__c = objVT.Description; }
                        else { objParam.Car_Type__c = String.Empty; }
                    }
                }
                else
                {
                    if (spk.SPKCustomer.SAPCustomer != null)
                    {
                        if (spk.SPKCustomer.SAPCustomer.VechileType != null)
                        {
                            objParam.Car_Code__c = spk.SPKCustomer.SAPCustomer.VechileType.VechileTypeCode;
                            objParam.Car_Type__c = spk.SPKCustomer.SAPCustomer.VechileType.Description;
                        }
                        else
                        {
                            objParam.Car_Code__c = spk.SPKCustomer.SAPCustomer.VehicleTypeCode;
                            VechileType objVT = new VechileTypeFacade(User).Retrieve(spk.SPKCustomer.SAPCustomer.VehicleTypeCode);
                            if (objVT != null)
                            { objParam.Car_Type__c = objVT.Description; }
                            else { objParam.Car_Type__c = String.Empty; }
                        }
                    }
                    else
                    {
                        objParam.Car_Code__c = String.Empty;
                        objParam.Car_Type__c = String.Empty;
                    }
                }

                Task.Run(
                () => KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", paramWalkinOpportunity.SObjectTypeName), objParam)
            ).Wait();

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

            }
            catch
            {
                vReturn = false;
            }

            vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
            msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;

            #region Log
            CreateLog(vReturn.ToString(), msg.ToString(), "K:SALESFORCEUPDATEOPPORTUNITY\n", strJson);
            #endregion

            return vReturn;
        }

        #endregion LeadCustomerProcess


        #region CaseManagementProcess

        public static void CaseManagementProcess()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            String sql = "";

            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(CustomerCaseResponse), "ID", MatchType.InSet, "(" + sql + ")"));
            KTB.DNET.BusinessFacade.CustomerCaseResponseFacade facade = new KTB.DNET.BusinessFacade.CustomerCaseResponseFacade(User);
            ArrayList arr = facade.Retrieve(criterias);

            ArrayList arrTypeCode = GetNewType();

            Console.WriteLine("Total SPK :" + arr.Count.ToString() + "\n");

            foreach (CustomerCaseResponse resp in arr)
            {
                Console.WriteLine("Processing SPK :" + resp.CustomerCase.CaseDate + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");

                if (resp.CustomerCase != null)
                {

                    bool vResult = true;
                    string msg = "";
                    vResult = UpdateCase(resp, ref msg);

                    if (vResult)
                    {
                        resp.IsSend = 1;
                        int iResult = facade.Update(resp);
                    }
                }
            }
            Console.WriteLine("Process tansfer selesai !" + "\n");
            Console.ReadLine();
        }

        public static bool UpdateCase(CustomerCaseResponse objResponse, ref string msg)
        {
            bool vReturn = false;
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            string strJson = "";
            try
            {
                var objParam = new KTB.DNet.WebApi.Models.paramUpdateCase();
                objParam.id = objResponse.CustomerCase.SalesforceID;
                objParam.status = EnumCustomerCaseResponse.GetStringCustomerResponse(objResponse.Status);
                objParam.comment = objResponse.Description;

                Task.Run(
                () => KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", paramUpdateCase.SObjectTypeName), objParam)
            ).Wait();

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);
            }
            catch (Exception ex)
            {
                vReturn = false;
            }
            vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
            msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;

            #region Log
            CreateLog(vReturn.ToString(), msg.ToString(), "K:SALESFORCEUPDATECASE\n", strJson);
            #endregion

            return vReturn;
        }

        #endregion

        #region Functions

        public static void CreateLog(string vReturn, string msg, string logCategory, string strJson)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            #region Log
            WsLog wslog = new WsLog();
            wslog.Source = "Internal";
            wslog.Status = vReturn.ToString();
            wslog.Message = msg;
            wslog.Body = String.Concat(logCategory, strJson);
            wslog.RowStatus = 0;
            wslog.CreatedBy = "WebService";

            WsLogFacade wslogfacade = new WsLogFacade(User);
            wslogfacade.Insert(wslog);
            #endregion
        }

        public static ArrayList GetNewType()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            ArrayList arr = new ArrayList();
            try
            {
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);
                string ConsumerKey = appConfigFacade.Retrieve("SPKModelCodeFilter").Value;
                if (ConsumerKey != null)
                {
                    string[] arrCode = ConsumerKey.Split(';');
                    string strCode = "";
                    foreach (string value in arrCode)
                    {
                        if (strCode == "")
                        {
                            strCode = "'" + value + "'";
                        }
                        else
                        {
                            strCode = strCode + ",'" + value + "'";
                        }
                        strCode = "(" + strCode + ")";
                    }

                    CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias.opAnd(new Criteria(typeof(VechileType), "VechileModel.VechileModelCode", MatchType.InSet, strCode));
                    VechileTypeFacade facade = new VechileTypeFacade(User);
                    arr = facade.Retrieve(criterias);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return arr;
        }

        public static bool EmailAddressCheck(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public static string GetAccount()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string accountID = appConfigFacade.Retrieve("Account_SF_ID").Value;

            return accountID;
        }

        public bool IsTransferToSalesforce()
        {
            bool vReturn = false;
            try
            {
                var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);
                string value = appConfigFacade.Retrieve("TransferToSF").Value;
                if (value == "1")
                {
                    vReturn = true;
                }
            }
            catch
            {
                vReturn = false;
            }

            return vReturn;
        }

        #endregion
        
    }
}
