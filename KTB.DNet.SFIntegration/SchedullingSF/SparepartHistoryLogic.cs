using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using KTB.DNet.WebApi.Models;
using KTB.DNet.WebApi.Models.MiddlewareSOA;
using KTB.DNET.BusinessFacade;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.SchedullingSF
{
    public static class SparepartHistoryLogic
    {
        public static async Task WSSalesforce_SparepartHistory(bool isInit)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();

            if (isInit)
                InsertToStaging(5);

            InsertToSalesForce(5);
            InsertToMMID(5);
        }

        private static void InsertToStaging(int objID)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            try
            {
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_InsertPMHeaderToSFSparepartHistory '" + objID.ToString() + "'");
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

        private static void InsertToSalesForce(int objID)
        {
            try
            {
                GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
                SFMasterObject sfMasterObject = new SFMasterObjectFacade(User).Retrieve(objID);
                // create staging log
                int StagingLogID = InsertStagingLog(objID, true, string.Empty);
                string strJson = "";
                string msg = "";
                bool vReturn = false;

                SFSparepartHistoryFacade objSFSparepartHistoryFacade = new SFSparepartHistoryFacade(User);
                ArrayList arr = objSFSparepartHistoryFacade.RetieveListOfItemToSend(1);
                if (arr.Count > 0)
                {
                    Console.WriteLine("Process " + arr.Count + " data");
                    int n = 1;
                    foreach (SFSparepartHistory item in arr)
                    {
                        item.RetrySF = short.Parse((Int32.Parse(item.RetrySF.ToString()) + 1).ToString());
                        objSFSparepartHistoryFacade.Update(item);

                        Console.Write("Process SFSparepartHistory SF " + n.ToString() + " of " + arr.Count.ToString());

                        DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamSparepartHistory " + item.AssistPartSales.ID);
                        if (dtSet.Tables.Count > 0)
                        {
                            DataTable dtTbl = dtSet.Tables[0];
                            if (dtTbl.Rows.Count > 0)
                            {
                                var objParam = new ParamSparepartHistorySF();
                                objParam.SalesforceID = item.SFID;
                                objParam.Transaction_Date__c = dtTbl.Rows[0]["Transaction_Date__c"].ToString();
                                objParam.Parts_Code__c = dtTbl.Rows[0]["Parts_Code__c"].ToString();
                                objParam.Parts_Name__c = dtTbl.Rows[0]["Parts_Name__c"].ToString();
                                objParam.Quantity__c = dtTbl.Rows[0]["Quantity__c"].ToString();
                                objParam.Sales_Price__c = dtTbl.Rows[0]["Sales_Price__c"].ToString();
                                objParam.Is_Campaign__c = dtTbl.Rows[0]["Is_Campaign__c"].ToString();
                                objParam.Campaign_No__c = dtTbl.Rows[0]["Campaign_No__c"].ToString();
                                objParam.Campaign_Description__c = dtTbl.Rows[0]["Campaign_Description__c"].ToString();
                                objParam.Status__c = dtTbl.Rows[0]["Status__c"].ToString();
                                objParam.Dnet_ID__c = dtTbl.Rows[0]["Dnet_ID__c"].ToString();

                                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

                                Task.Run(
                                    () => KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.SendData(sfMasterObject,
                                        User, String.Concat("/salesforce/services/", ParamSparepartHistorySF.SObjectTypeName), objParam, false)
                                ).Wait();

                                if (KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.IsSuccess)
                                {
                                    var obj = JObject.Parse(KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.Message);
                                    vReturn = obj["Status"].ToString().Equals("0");
                                    msg = obj["Message"].ToString();

                                    if (vReturn)
                                    {
                                        //update SFID to SFSparepartHistory Table
                                        item.IsSynchronizeSF = true;
                                        item.SynchronizeDateSF = DateTime.Now;
                                        item.RetrySF = 0;
                                        item.SFID = msg;
                                        try
                                        {
                                            objSFSparepartHistoryFacade.Update(item);
                                        }
                                        catch (Exception ex)
                                        {
                                            InsertErrorLog(objID, ex);
                                        }
                                        Console.WriteLine(" = Success");
                                    }
                                }
                                else
                                {
                                    vReturn = KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.IsSuccess;
                                    msg = KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.Message;
                                }

                                InsertIntoWSLog(vReturn, msg, strJson, "K:SFSparepartHistory");
                                InsertSynchronizeLog(StagingLogID, item.ID, vReturn, msg);
                            }
                            else
                            {
                                InsertSynchronizeLog(StagingLogID, item.ID, false, "Data tidak valid.");
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

        private static void InsertToMMID(int objID)
        {
            try
            {
                GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
                SFMasterObject sfMasterObject = new SFMasterObjectFacade(User).Retrieve(objID);
                // create staging log
                int StagingLogID = InsertStagingLog(objID, true, string.Empty);
                string strJson = "";
                string msg = "";
                bool vReturn = false;

                SFSparepartHistoryFacade objSFSparepartHistoryFacade = new SFSparepartHistoryFacade(User);
                DataSet dsHist = objSFSparepartHistoryFacade.RetrieveSp("EXEC [sp_SF_ParamSparepartHistory_Retrieve] 2");
                if (dsHist.Tables.Count > 0)
                {
                    DataTable dtHist = dsHist.Tables[0];
                    if (dtHist.Rows.Count > 0)
                    {
                        {
                            Console.WriteLine("Process " + dtHist.Rows.Count + " data");
                            int n = 1;
                            foreach (DataRow item in dtHist.Rows)
                            {
                                var objList = new SFSparepartHistoryFacade(User).RetrieveList(item["KeyID"].ToString()).Cast<SFSparepartHistory>().ToList();
                                objList.ForEach(d =>
                                {
                                    d.RetryMMID = short.Parse((Int32.Parse(d.RetryMMID.ToString()) + 1).ToString());
                                });

                                objSFSparepartHistoryFacade.UpdateBatch(objList);

                                Console.Write("Process SFSparepartHistory MMID " + n.ToString() + " of " + dtHist.Rows.Count.ToString());

                                DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp(string.Format("EXEC sp_SF_ParamSparepartHistory {0}, '{1}'", 0, item["KeyID"].ToString()));
                                if (dtSet.Tables.Count > 0)
                                {
                                    DataTable dtTbl = dtSet.Tables[0];
                                    if (dtTbl.Rows.Count > 0)
                                    {
                                        var paramList = new List<object>();
                                        foreach (DataRow dr in dtTbl.Rows)
                                        {
                                            var objParam = new ParamSparepartHistoryMMID();
                                            objParam.Dnet_Sparepart_ID__c = dr["Dnet_Sparepart_ID__c"].ToString();
                                            objParam.Transaction_Date__c = dr["Transaction_Date__c"].ToString();
                                            //objParam.Dealer_Code__c = dtTbl.Rows[0]["Dealer_Code__c"].ToString();
                                            //objParam.No_Work_Order__c = dtTbl.Rows[0]["No_Work_Order__c"].ToString();
                                            objParam.Parts_Code__c = dr["Parts_Code__c"].ToString();
                                            objParam.Parts_Name__c = dr["Parts_Name__c"].ToString();
                                            objParam.Quantity__c = dr["Quantity__c"].ToString();
                                            //objParam.Harga_Jual__c = dtTbl.Rows[0]["Harga_Jual__c"].ToString();
                                            objParam.Is_Campaign__c = dr["Is_Campaign__c"].ToString();
                                            objParam.Campaign_No__c = dr["Campaign_No__c"].ToString();
                                            objParam.Campaign_Description__c = dr["Campaign_Description__c"].ToString();
                                            objParam.Status__c = dr["Status__c"].ToString();
                                            objParam.Dnet_ID__c = dr["Dnet_ID__c"].ToString();
                                            paramList.Add(objParam);
                                        }

                                        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(paramList);

                                        Task.Run(
                                            () => KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.SendData(sfMasterObject,
                                                User, String.Concat("/mmid/services/", ParamSparepartHistoryMMID.SObjectTypeName), parameters:paramList, isbuildarray:true)
                                        ).Wait();

                                        if (KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.IsSuccess)
                                        {
                                            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<MMIDResult>(KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.Message);
                                            vReturn = obj.data.processed > 0;
                                            msg = obj.alerts.message;

                                            if (obj.data.failed_count > 0)
                                            {
                                                msg += string.Concat(", with error detail: ", string.Join("|", obj.data.failed_details
                                                    .Select(s => new { Message = string.Format("AssistPartSalesID: {0}, Error: {1}", (Newtonsoft.Json.JsonConvert.DeserializeObject<ParamSparepartHistoryMMID>(s.data.ToString())).Dnet_Sparepart_ID__c, s.reason) })
                                                    .Select(s => s.Message)
                                                ));
                                            }

                                            if (vReturn)
                                            {
                                                //objList.Where(w => obj.data.failed_details
                                                //    .Select(s => new { Dnet_Sparepart_ID__c = ((ParamSparepartHistoryMMID)s.data).Dnet_Sparepart_ID__c }).Select(s => s.Dnet_Sparepart_ID__c)
                                                //    .Contains(w.AssistPartSales.ID.ToString())).ToList().ForEach(d =>
                                                //{
                                                //    d.IsSynchronizeMMID = false;
                                                //    d.SynchronizeDateMMID = DateTime.Now;
                                                //});

                                                List<SFSparepartHistory> updateList = new List<SFSparepartHistory>();
                                                if (obj.data.failed_count > 0)
                                                {
                                                    updateList = objList.Where(w => !obj.data.failed_details
                                                    .Select(s => new { Dnet_Sparepart_ID__c = (Newtonsoft.Json.JsonConvert.DeserializeObject<ParamSparepartHistoryMMID>(s.data.ToString())).Dnet_Sparepart_ID__c })
                                                    .Select(s => s.Dnet_Sparepart_ID__c).Contains(w.AssistPartSales.ID.ToString())).ToList();
                                                }
                                                else
                                                    updateList = objList;

                                                updateList.ForEach(d =>
                                                {
                                                    d.IsSynchronizeMMID = true;
                                                    d.SynchronizeDateMMID = DateTime.Now;
                                                    d.RetryMMID = 0;
                                                });

                                                try
                                                {
                                                    objSFSparepartHistoryFacade.UpdateBatch(updateList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    InsertErrorLog(objID, ex);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            vReturn = KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.IsSuccess;
                                            msg = KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.Message;
                                        }

                                        InsertIntoWSLog(vReturn, msg, strJson, "K:MMIDSparepartHistory");
                                        InsertSynchronizeLog(StagingLogID, Convert.ToInt32(item["ID"].ToString()), vReturn, msg);
                                    }
                                    else
                                    {
                                        InsertSynchronizeLog(StagingLogID, Convert.ToInt32(item["ID"].ToString()), false, "Data tidak valid.");
                                    }
                                }
                                else
                                {
                                    InsertSynchronizeLog(StagingLogID, Convert.ToInt32(item["ID"].ToString()), false, "Status data sudah didelete.");
                                }


                                n += 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InsertErrorLog(objID, ex);
            }

        }

        private static int InsertStagingLog(int SFMasterObjectID, bool IsSuccess, String ErrorMsg)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SFStagingLog obj = new SFStagingLog();
            obj.TransactionDate = DateTime.Now;
            obj.SFMasterObject = new SFMasterObject(ID: SFMasterObjectID);
            obj.IsSuccess = IsSuccess;
            obj.ErrorMessage = ErrorMsg;

            return new SFStagingLogFacade(User).Insert(obj);
        }

        private static int UpdateStagingLog(int SFMasterObjectID, bool IsSuccess, String ErrorMsg)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SFStagingLog obj = new SFStagingLog();
            obj.TransactionDate = DateTime.Now;
            obj.SFMasterObject = new SFMasterObject(ID: SFMasterObjectID);
            obj.IsSuccess = IsSuccess;
            obj.ErrorMessage = ErrorMsg;

            return new SFStagingLogFacade(User).Update(obj);
        }

        private static void InsertSynchronizeLog(int SFStagingLogID, int TransactionID, bool IsSuccess, string ErrorMsg)
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

        private static void InsertErrorLog(int SFMasterObjectID, Exception e)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SFErrorLog obj = new SFErrorLog();
            obj.SFMasterObject = new SFMasterObject(ID: SFMasterObjectID);
            obj.ExceptionMessage = e.Message;
            obj.ExceptionStartTrace = e.StackTrace;
            obj.ErrorDate = DateTime.Now;
            new SFErrorLogFacade(User).Insert(obj);
        }

        private static void InsertIntoWSLog(bool vReturn, String msg, String strJson, String logCategory)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            int result = 0;
            WsLog objWSLog = new WsLog();

            objWSLog.Source = "Internal";
            objWSLog.Status = vReturn.ToString();
            objWSLog.Message = msg;
            objWSLog.Body = String.Concat(logCategory, strJson);
            objWSLog.RowStatus = 0;
            objWSLog.CreatedBy = "DNetHangfire";

            result = new WsLogFacade(User).Insert(objWSLog);
        }
    }
}
