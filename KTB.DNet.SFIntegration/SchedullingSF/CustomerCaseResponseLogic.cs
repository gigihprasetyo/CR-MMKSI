using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain.Search;
using KTB.DNet.WebApi.Models;
using System.Security.Principal;
using KTB.DNET.BusinessFacade;
using KTB.DNet.WebApi.Models.SalesForce;
using System.Threading.Tasks;
using System;
using KTB.DNet.DataMapper.Framework;
using System.Data;
using KTB.DNet.SFIntegration.Parser;
using System.Linq;
using System.Collections;

namespace KTB.DNet.SFIntegration.SchedullingSF
{
    public static class CustomerCaseResponseLogic
    {
        private static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
        private static string _strJson;
        private static string _valTransferToSF;

        public static void Process() 
        {
            const string sp_GetPendingCustomerCaseResponse = "sp_GetPendingCustomerCaseResponse";

            CustomerCaseResponseFacade ccResFacade = new CustomerCaseResponseFacade(User);
            var dt = ccResFacade.RetrieveSp(sp_GetPendingCustomerCaseResponse).Tables[0];
            
            bool isSuccess;

            foreach (DataRow row in dt.Rows)
            {
                CustomerCaseResponse ccRes = CustomerCaseResponseParser.Parse(row);
                isSuccess = UpdateCase(ccRes);
                if (isSuccess)
                {
                    ccRes.IsSend = 1;
                    ccResFacade.Update(ccRes);
                    ccResFacade.RetrieveSp(string.Format("EXEC sp_UpdateIsSentCustomerCaseResp @ID={0}, @CustomerCaseID={1}", ccRes.ID, ccRes.CustomerCase.ID));
                }
                else 
                {
                    if (ccRes.IsSend == 0)
                        ccRes.IsSend = 2;
                    else
                        ccRes.IsSend += 1;

                    ccResFacade.Update(ccRes);
                }
            }
        }

        public static bool UpdateCase(CustomerCaseResponse objresponse){
            bool vReturn = true;
            string msg = string.Empty;
            
            _valTransferToSF = GetConfig();

            if (_valTransferToSF.Equals("1"))
            {
                try
                {
                    paramUpdateCase objParam = new paramUpdateCase
                    {
                        id = objresponse.CustomerCase.SalesforceID,
                        status = EnumCustomerCaseResponse.GetStringCustomerResponse(objresponse.Status),
                        Status_By_Dealer__c = EnumCustomerCaseResponse.GetStringCustomerResponse(objresponse.Status),
                        comment = objresponse.Description
                    };

                    if (objresponse.CustomerCase.SubCategory1.Contains("Test Drive")){
                        objParam.Confirm_Test_Drive_Request_Date__c = objresponse.BookingDatetime.ToString("yyyy-MM-dd hh:mm:ss");
                        objParam.Confirm_Service_Booking_Request_Date__c = "";
                    }
                    else if (objresponse.CustomerCase.SubCategory1.Contains("Service Booking")) {
                        objParam.Confirm_Service_Booking_Request_Date__c = objresponse.BookingDatetime.ToString("yyyy-MM-dd hh:mm:ss");
                        objParam.Confirm_Test_Drive_Request_Date__c = "";
                    }
                    else 
                    {
                        objParam.Confirm_Service_Booking_Request_Date__c = "";
                        objParam.Confirm_Test_Drive_Request_Date__c = "";
                    }

                    if (objresponse.Status == 6){
                        objParam.Confirm_Service_Booking_Request_Date__c = "";
                        objParam.Confirm_Test_Drive_Request_Date__c = "";
                    }

                    if (objresponse.Response > 0){
                        StandardCodeFacade stdFacade = new StandardCodeFacade(User);
                        CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        crit.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.Partial, "CustomerCase.Response"));
                        crit.opAnd(new Criteria(typeof(StandardCode), "ValueId", MatchType.Exact, objresponse.Response));
                        objParam.Dealer_Respons__c = stdFacade.Retrieve(crit).Cast<StandardCode>().FirstOrDefault().ValueDesc;
                    }
                    else 
                    {
                        objParam.Dealer_Respons__c = "";
                    }

                    Task.Run(() => SalesForce.Send(User, string.Concat("services/apexrest/", paramUpdateCase.SObjectTypeName), objParam, false).Wait(30000));

                    _strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

                    if (!SalesForce.IsSuccess)
                    {
                        vReturn = false;
                        msg = SalesForce.Message;
                        CreateWSLog(vReturn, msg, string.Format("K:SALESFORCEIUPDATECASE\n{0}", _strJson));
                    }
                }
                catch (Exception ex){
                    vReturn = false;
                    msg = string.Format("Gagal update data case di salesforce {0}", ex.Message);
                    _strJson = string.Format("CustomerCase.ID : {0} Error : {1}", objresponse.CustomerCase.ID, ex.Message);
                    CreateWSLog(vReturn, msg, string.Format("K:SALESFORCEIUPDATECASE\n{0}", _strJson));
                }
            }

            return vReturn;
        }

        private static string GetConfig(){
            AppConfigFacade objFacade = new AppConfigFacade(User);
            AppConfig objAppConf = objFacade.Retrieve("TransferToSF");
            if (objAppConf != null && objAppConf.ID > 0) {
                return objAppConf.Value.Trim();
            }

            return "";
        }

        private static void CreateWSLog(bool vReturn, string errMsg, string bodyMessage){
            try 
            {
                WsLog wslog = new WsLog{
                    Source = "Internal",
                    Status = vReturn.ToString(),
                    Message = errMsg,
                    Body = bodyMessage,
                    CreatedBy = "DNetHangfire"
                };

                WsLogFacade facade = new WsLogFacade(User);
                facade.Insert(wslog);
            }
            catch
            {
            }
        }
    }
}
