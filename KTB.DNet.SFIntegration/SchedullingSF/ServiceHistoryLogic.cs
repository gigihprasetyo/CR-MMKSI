using System;
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
using KTB.DNet.Domain.Search;
using System.Net;
using System.Net.Http;
using KTB.DNET.BusinessFacade.SparePart;
using System.Net.Mail;
using System.Web.Configuration;
using KTB.DNET.BusinessFacade;
using KTB.DNet.WebApi.Models;
using System.Collections;

namespace KTB.DNet.SFIntegration.SchedullingSF
{
    public static class ServiceHistoryLogic
    {
        public static async Task WSSalesforce_ServiceHistory(bool isInit)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();

            if (isInit)
            {
                InsertToStaging(3);
            }
            InsertToSalesForce(3);
        }

        static void InsertToStaging(int objID)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            try
            {
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_InsertPMHeaderToSFServiceHistory '" + objID.ToString() + "'");
            }
            catch (Exception ex)
            {
                SFErrorLog obj = new SFErrorLog();
                obj.SFMasterObject = new SFMasterObject(ID: objID);
                obj.ExceptionMessage = ex.Message;
                obj.ExceptionStartTrace = ex.StackTrace;
                obj.ErrorDate = DateTime.Now;
                new SFErrorLogFacade(User).Insert(obj);
            }
        }

        static void InsertToSalesForce(int objID)
        {
            try
            {
                GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
                SFMasterObject sfMasterObject = new SFMasterObjectFacade(User).Retrieve(objID);
                // create staging log
                int StagingLogID = InsertStagingLog(objID, true, string.Empty);


                ArrayList arr = new ArrayList();
                arr = new SFServiceHistoryFacade(User).RetieveListOfItemToSend();
                if (arr.Count > 0)
                {
                    Console.WriteLine("Process " + arr.Count + " data");
                    int n = 1;
                    foreach (SFServiceHistory item in arr)
                    {
                        item.RowStatus = short.Parse((Int32.Parse(item.RowStatus.ToString()) + 1).ToString());
                        new SFServiceHistoryFacade(User).Update(item);

                        Console.Write("Process SFServiceHistory " + n.ToString() + " of " + arr.Count.ToString());

                        DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamServiceHistory " + item.PMHeader.ID);
                        if (dtSet.Tables.Count > 0)
                        {
                            DataTable dtTbl = dtSet.Tables[0];
                            if (dtTbl.Rows.Count > 0)
                            {
                                var objParam = new paramServiceHistory();
                                objParam.No_Rangka__c = dtTbl.Rows[0]["No_Rangka__c"].ToString();
                                objParam.Service_Type__c = dtTbl.Rows[0]["Service_Type__c"].ToString();
                                objParam.Service_Date__c = dtTbl.Rows[0]["Service_Date__c"].ToString();
                                objParam.Dealer_code__c = dtTbl.Rows[0]["Dealer_code__c"].ToString();
                                objParam.Odometer__c = dtTbl.Rows[0]["Odometer__c"].ToString();
                                objParam.MSP_No__c = dtTbl.Rows[0]["MSP_No__c"].ToString();
                                objParam.Dnet_ID__c = dtTbl.Rows[0]["Dnet_ID__c"].ToString();

                                Task.Run(
                    () => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendData(sfMasterObject,
                        User, String.Concat("services/apexrest/", paramServiceHistory.SObjectTypeName), objParam, false)
                ).Wait();
                                if (KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess)
                                {
                                    //update SFID to SFServiceHistory Table
                                    item.IsSynchronize = true;
                                    item.SynchronizeDate = DateTime.Now;
                                    item.SFID = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;
                                    try
                                    {
                                        new SFServiceHistoryFacade(User).Update(item);
                                    }
                                    catch (Exception ex)
                                    {
                                        InsertErrorLog(objID, ex);
                                    }
                                    Console.WriteLine(" = Success");
                                }
                                else
                                {
                                    Console.WriteLine(" = Fail - " + KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message);
                                }
                                InsertSynchronizeLog(StagingLogID, item.ID, KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess, KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message);
                            }
                        }
                        else
                        {
                            InsertSynchronizeLog(StagingLogID, item.ID, false, "Status data sudah didelete.");
                        }


                        n += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(objID, ex);
            }

        }

        static int InsertStagingLog(int SFMasterObjectID, bool IsSuccess, String ErrorMsg)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SFStagingLog obj = new SFStagingLog();
            obj.TransactionDate = DateTime.Now;
            obj.SFMasterObject = new SFMasterObject(ID: SFMasterObjectID);
            obj.IsSuccess = IsSuccess;
            obj.ErrorMessage = ErrorMsg;

            return new SFStagingLogFacade(User).Insert(obj);
        }

        static int UpdateStagingLog(int SFMasterObjectID, bool IsSuccess, String ErrorMsg)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SFStagingLog obj = new SFStagingLog();
            obj.TransactionDate = DateTime.Now;
            obj.SFMasterObject = new SFMasterObject(ID: SFMasterObjectID);
            obj.IsSuccess = IsSuccess;
            obj.ErrorMessage = ErrorMsg;

            return new SFStagingLogFacade(User).Update(obj);
        }

        static void InsertSynchronizeLog(int SFStagingLogID, int TransactionID, bool IsSuccess, string ErrorMsg)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SFSynchronizeLog obj = new SFSynchronizeLog();
            obj.SFStagingLog = new SFStagingLog(ID: SFStagingLogID);
            obj.TransactionID = TransactionID;
            obj.SynchronizeDate = DateTime.Now;
            obj.IsSuccess = IsSuccess;
            obj.ErrorMessage = ErrorMsg;
            new SFSynchronizeLogFacade(User).Insert(obj);
        }

        static void InsertErrorLog(int SFMasterObjectID, Exception e)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SFErrorLog obj = new SFErrorLog();
            obj.SFMasterObject = new SFMasterObject(ID: SFMasterObjectID);
            obj.ExceptionMessage = e.Message;
            obj.ExceptionStartTrace = e.StackTrace;
            obj.ErrorDate = DateTime.Now;
            new SFErrorLogFacade(User).Insert(obj);
        }
    }
}
