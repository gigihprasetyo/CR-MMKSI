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
    public static class SmartPackageLogic
    {
        public static async Task WSSalesforce_SmartPackage(bool isInit)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();

            //if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["InitData"]))
            if (isInit)
            {
                InsertToStaging(4);
            }
            InsertToSalesForce(4);
        }


        static void InsertToStaging(int objID)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            try
            {
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_InsertMSPRegHistoryToSFSmartPackage '" + objID.ToString() + "'");
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

                CriteriaComposite crt = new CriteriaComposite(new Criteria(typeof(SFSmartPackage), "RowStatus", MatchType.Exact, 0));
                crt.opAnd(new Criteria(typeof(SFSmartPackage), "IsActive", MatchType.Exact, Convert.ToInt32(true)));
                crt.opAnd(new Criteria(typeof(SFSmartPackage), "IsSynchronize", MatchType.Exact, Convert.ToInt32(false)));
                ArrayList arr = new ArrayList();
                //arr =new SFSmartPackageFacade(User).Retrieve(crt);
                arr = new SFSmartPackageFacade(User).RetieveListOfItemToSend();
                if (arr.Count > 0)
                {
                    Console.WriteLine("Process " + arr.Count + " data");

                    int n = 1;

                    foreach (SFSmartPackage item in arr)
                    {
                        Console.Write("Process SFSmartPackage " + n.ToString() + " of " + arr.Count.ToString());

                        DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamSmartPackage " + item.MSPRegistrationHistory.ID);
                        if (dtSet.Tables.Count > 0)
                        {
                            DataTable dtTbl = dtSet.Tables[0];
                            if (dtTbl.Rows.Count > 0)
                            {
                                var objParam = new paramSmartPackage();
                                objParam.Name = dtTbl.Rows[0]["Name"].ToString();
                                objParam.Chassis_Number__c = dtTbl.Rows[0]["Chassis_Number__c"].ToString();
                                objParam.Durasi_MSP__c = dtTbl.Rows[0]["Durasi_MSP__c"].ToString();
                                objParam.Expired_Date__c = dtTbl.Rows[0]["Expired_Date__c"].ToString();
                                objParam.ID_Number__c = dtTbl.Rows[0]["ID_Number__c"].ToString();
                                objParam.Join_MSP_Date__c = dtTbl.Rows[0]["Join_MSP_Date__c"].ToString();
                                objParam.KM_MSP__c = dtTbl.Rows[0]["KM_MSP__c"].ToString();
                                objParam.Nama__c = dtTbl.Rows[0]["Nama__c"].ToString();
                                objParam.Phone__c = dtTbl.Rows[0]["Phone__c"].ToString();
                                objParam.Tipe_MSP__c = dtTbl.Rows[0]["Tipe_MSP__c"].ToString();
                                objParam.Upgrade_Date__c = dtTbl.Rows[0]["Upgrade_Date__c"].ToString();
                                objParam.Dnet_ID__c = dtTbl.Rows[0]["Dnet_ID__c"].ToString();

                                Task.Run(
                    () => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendData(sfMasterObject,
                        User, String.Concat("services/apexrest/", paramSmartPackage.SObjectTypeName), objParam, false)
                ).Wait();
                                if (KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess)
                                {
                                    //update SFID to SFSmartPackage Table
                                    item.IsSynchronize = true;
                                    item.SynchronizeDate = DateTime.Now;
                                    item.SFID = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;
                                    try
                                    {
                                        new SFSmartPackageFacade(User).Update(item);
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
