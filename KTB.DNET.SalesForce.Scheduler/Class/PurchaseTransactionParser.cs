using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.Domain.Search;
using KTB.DNet.Domain;
using System.Security.Principal;
using KTB.DNet.Salesforce.Class;
using KTB.DNET.BusinessFacade;
using KTB.DNet.WebApi.Models;
using System.Configuration;
using KTB.DNet.BusinessFacade.General;

namespace KTB.DNet.Salesforce.Class
{
    public class PurchaseTransactionParser
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

        public void Process(int objID)
        {
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["InitData"]))
            {
                InsertToStaging(objID);
            }

            InsertToSalesForce(objID);
        }

        static void InsertToStaging(int objID)
        {
            try
            {
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_InsertFakturToSFPurchaseTransaction '" + objID.ToString() + "'");
            }
            catch (Exception ex)
            {
                new LogParser().InsertErrorLog(objID, ex);
            }
        }

        static void InsertToSalesForce(int objID)
        {
            try
            {
                SFMasterObject sfMasterObject = new SFMasterObjectFacade(User).Retrieve(objID);
                // create staging log
                int StagingLogID = new LogParser().InsertStagingLog(objID, true, string.Empty);
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
                        //crt = new CriteriaComposite(new Criteria(typeof(SFContact), "EndCustomer.ID", MatchType.Exact, item.EndCustomer.ID));
                        //crt.opAnd(new Criteria(typeof(SFContact), "RowStatus", MatchType.Exact, 0));
                        //crt.opAnd(new Criteria(typeof(SFContact), "IsActive", MatchType.Exact, 1));
                        //crt.opAnd(new Criteria(typeof(SFContact), "IsSynchronize", MatchType.Exact, 1));
                        //ArrayList arrDt = new SFContactFacade(User).Retrieve(crt);
                        //if (arrDt.Count == 0)
                        //{
                        //    new LogParser().InsertSynchronizeLog(StagingLogID, item.ID, false, "Contact belum synchronize dengan sales force.");
                        //    continue;
                        //}
                        Console.Write("Process SFPurchaseTransaction " + n.ToString() + " of " + arr.Count.ToString());
                        DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamPurchaseTransaction "+ item.EndCustomer.ID);
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

                                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);
                                Task.Run(() => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendData(sfMasterObject, User, String.Concat("services/apexrest/", paramPurchaseTransaction.SObjectTypeName), objParam, false)).Wait();

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
                                        new LogParser().InsertErrorLog(objID, ex);
                                    }

                                    Console.WriteLine(string.Format("{0} - Succes", n.ToString()));
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("{0} - Fail - {1}", n.ToString(), KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message));
                                }
                                new LogParser().InsertSynchronizeLog(StagingLogID, item.ID, KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess, KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message);
                            }

                        }
                        else
                        {
                            new LogParser().InsertSynchronizeLog(StagingLogID, item.ID, false, "Status data sudah didelete.");
                        }

                        n += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                new LogParser().InsertErrorLog(objID, ex);
            }

        }

        static void InsertIntoWSLog(bool vReturn, String msg, String strJson, String logCategory)
        {

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

        static void InsertIntoSFReference(Int32 ID) {

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
