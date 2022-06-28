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
    public static class PurchaseTransactionLogic
    {
        public static async Task WSSalesforce_PurchaseTransaction(bool isInit)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();

            //if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["InitData"]))
            if (isInit)
            {
                InsertToStaging(2);
            }
            InsertToSalesForce(2);
        }


        static void InsertToStaging(int objID)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            try
            {
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_InsertFakturToSFPurchaseTransaction '" + objID.ToString() + "'");
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
                String strJson = "";
                String msg = "";
                int PKTID = 0;
                bool vReturn = false;

                new SFPurchaseTransactionFacade(User).CheckChassisMasterPKT();

                CriteriaComposite crt = new CriteriaComposite(new Criteria(typeof(SFPurchaseTransaction), "RowStatus", MatchType.Exact, 0));
                crt.opAnd(new Criteria(typeof(SFPurchaseTransaction), "IsActive", MatchType.Exact, Convert.ToInt32(true)));
                crt.opAnd(new Criteria(typeof(SFPurchaseTransaction), "IsSynchronize", MatchType.Exact, Convert.ToInt32(false)));
                ArrayList arr = new ArrayList();
                //arr = new SFPurchaseTransactionFacade(User).Retrieve(crt);
                arr = new SFPurchaseTransactionFacade(User).RetieveListOfItemToSend();
                if (arr.Count > 0)
                {
                    Console.WriteLine("Process SFPurchaseTransaction" + arr.Count + " data");

                    int n = 1;
                    foreach (SFPurchaseTransaction item in arr)
                    {
                        item.RowStatus = short.Parse((Int32.Parse(item.RowStatus.ToString()) + 1).ToString());
                        new SFPurchaseTransactionFacade(User).Update(item);

                        Console.Write("Process SFPurchaseTransaction " + n.ToString() + " of " + arr.Count.ToString());
                        DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamPurchaseTransaction " + item.EndCustomer.ID);
                        if (dtSet.Tables.Count > 0)
                        {
                            DataTable dtTbl = dtSet.Tables[0];
                            if (dtTbl.Rows.Count > 0)
                            {
                                var objParam = new paramPurchaseTransaction();
                                objParam.ID_Number__c = dtTbl.Rows[0]["ID_Number__c"].ToString();
                                objParam.Open_Faktur_Date__c = dtTbl.Rows[0]["Open_Faktur_Date__c"].ToString();
                                objParam.No_Rangka__c = dtTbl.Rows[0]["No_Rangka__c"].ToString();
                                objParam.No_Mesin__c = dtTbl.Rows[0]["No_Mesin__c"].ToString();
                                objParam.Jenis_Kendaraan__c = dtTbl.Rows[0]["Jenis_Kendaraan__c"].ToString();
                                objParam.Kode_Dealer_Penjual__c = dtTbl.Rows[0]["Kode_Dealer_Penjual__c"].ToString();
                                objParam.Nama_Dealer_Penjual__c = dtTbl.Rows[0]["Nama_Dealer_Penjual__c"].ToString();
                                objParam.Garansi__c = string.Empty;
                                if (dtTbl.Rows[0]["PDI_Date__c"] != DBNull.Value)
                                {
                                    objParam.PDI_Date__c = dtTbl.Rows[0]["PDI_Date__c"].ToString();
                                }
                                objParam.SPK_No__c = dtTbl.Rows[0]["SPK_No__c"].ToString();
                                objParam.Tipe_Kendaraan__c = dtTbl.Rows[0]["Tipe_Kendaraan__c"].ToString();
                                if (dtTbl.Rows[0]["Tanggal_PKT"] != DBNull.Value)
                                {
                                    objParam.PKT_Date__c = dtTbl.Rows[0]["Tanggal_PKT"].ToString();
                                    PKTID = Int32.Parse(dtTbl.Rows[0]["PKTID"].ToString());
                                }
                                //objParam.Kode_Tipe_Kendaraan__c = dtTbl.Rows[0]["Kode_Tipe_Kendaraan__c"].ToString();
                                if (dtTbl.Rows[0]["Match_Date__c"] != DBNull.Value)
                                {
                                    objParam.Match_Date__c = dtTbl.Rows[0]["Match_Date__c"].ToString();
                                }

                                if (dtTbl.Rows[0]["Warranty_Date__c"] != DBNull.Value)
                                {
                                    objParam.Warranty_Date__c = dtTbl.Rows[0]["Warranty_Date__c"].ToString();
                                }

                                objParam.Sales_Name__c = dtTbl.Rows[0]["Sales_Name__c"].ToString();
                                objParam.Sales_Code__c = dtTbl.Rows[0]["Sales_Code__c"].ToString();

                                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);
                                Task.Run(() => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendData(sfMasterObject, User,
                                    String.Concat("services/apexrest/", paramPurchaseTransaction.SObjectTypeName), objParam, false)).Wait();

                                vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
                                msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;

                                //farid additional 20190201
                                InsertIntoWSLog(vReturn, msg, strJson, "K:SFMSPPurchaseTransaction");
                                InsertIntoSFReference(PKTID);
                                //--------------------------------------------------------------------------------


                                if (KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess)
                                {
                                    //update SFID and SynchronizeDate to SFPurchaseTransaction Table
                                    item.IsSynchronize = true;
                                    item.SynchronizeDate = DateTime.Now;
                                    item.SFID = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;
                                    try
                                    {
                                        new SFPurchaseTransactionFacade(User).Update(item);
                                    }
                                    catch (Exception ex)
                                    {
                                        InsertErrorLog(objID, ex);
                                    }

                                    Console.WriteLine(string.Format("{0} - Succes", n.ToString()));
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("{0} - Fail - {1}", n.ToString(), KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message));
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

        static void InsertIntoWSLog(bool vReturn, String msg, String strJson, String logCategory)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            int result = 0;
            WsLog objWSLog = new WsLog();

            objWSLog.Source = "Internal";
            objWSLog.Status = vReturn.ToString();
            objWSLog.Message = msg;
            objWSLog.Body = String.Concat(logCategory, strJson);
            objWSLog.RowStatus = 0;
            objWSLog.CreatedBy = "MSPWebService";

            result = new WsLogFacade(User).Insert(objWSLog);
        }

        static void InsertIntoSFReference(Int32 ID)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            int result = 0;
            SFReference objSFRef = new SFReference();

            objSFRef.RefID = ID;
            objSFRef.RefTable = "PKTChangeHistory";
            objSFRef.IsSend = true;
            objSFRef.FreeField2 = String.Empty;
            objSFRef.RowStatus = 0;
            objSFRef.CreatedBy = "System";

            result = new SFReferenceFacade(User).Insert(objSFRef);

        }


    }
}
