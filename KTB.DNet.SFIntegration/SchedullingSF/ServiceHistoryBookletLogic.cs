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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.SchedullingSF
{
    public static class ServiceHistoryBookletLogic
    {
        public static async Task WSSalesforce_ServiceHistory(bool isInit)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();

            if (isInit)
            {
                InsertToStaging(3);
                //DummyDirectToSalesForce(3);
            }

            InsertToSalesForce(3);
            InsertToMMID(3);
        }

        static void DummyDirectToSalesForce(int objID)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SFMasterObject sfMasterObject = new SFMasterObjectFacade(User).Retrieve(objID);

           /* var objParam = new ParamServiceHistoryBookletSF();
            objParam.No_Rangka__c = "MHMU5TU2EF748291";
            objParam.MSP_No__c = "HFGO5672";
            objParam.Dnet_ID__c = "123123";
            objParam.Dealer_code__c = "100001";
            objParam.Mechanic_Name__c = "Gigih";
            objParam.Mechanic_Notes__c = "test note";
            objParam.Service_Start_Date__c = "2020-04-20";
            objParam.Service_Start_Time__c = "07:00:00.00z";
            objParam.Service_End_Date__c = "2020-05-10";
            objParam.Service_Start_Time__c = "15:00:00.00z";
            objParam.Work_Order_Number__c = "123456";
            objParam.Service_Kind__c = "Service Busy";
            objParam.Odometer__c = "50000";
            objParam.Service_Type__c = "FS 1";
            objParam.Stall_Code__c = "098765";
            objParam.Booking_Code__c = "BC12345";
            objParam.Status__c = "Active";

//            Task.Run(
//() => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendData(sfMasterObject,
//    User, String.Concat("services/apexrest/", paramServiceHistory.SObjectTypeName), objParam, false)
//).Wait();
            KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.SendData(sfMasterObject, User, String.Concat("/salesforce/services/", ParamServiceHistoryBookletSF.SObjectTypeName), objParam, false);
            if (KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess)
            {
                //update SFID to SFServiceHistory Table

                Console.WriteLine(" = Success");
            }
            else
            {
                Console.WriteLine(" = Fail - " + KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message);
            } */

            DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamServiceHistoryBooklet " + 0 + ", " + 1692869);
            //if (dtSet == null)
            //    return;
            if (dtSet.Tables.Count > 0)
            {
                DataTable dtTbl = dtSet.Tables[0];
                if (dtTbl.Rows.Count > 0)
                {
                    //dtTbl.ToCSV("");
                    var objParam = new ParamServiceHistoryBookletSF();
                    objParam.No_Rangka__c = dtTbl.Rows[0]["No_Rangka__c"].ToString();
                    //objParam.Old__c = dtTbl.Rows[0]["Old__c"].ToString();
                    objParam.MSP_No__c = dtTbl.Rows[0]["MSP_No__c"].ToString();
                    objParam.Dnet_ID__c = dtTbl.Rows[0]["Dnet_ID__c"].ToString();
                    objParam.Dealer_code__c = dtTbl.Rows[0]["Dealer_code__c"].ToString();
                    //objParam.Dealer_Name__c = dtTbl.Rows[0]["Dealer_Name__c"].ToString();
                    objParam.Service_Start_Date__c = dtTbl.Rows[0]["Service_Start_Date__c"].ToString();
                    objParam.Service_Start_Time__c = dtTbl.Rows[0]["Service_Start_Time__c"].ToString();
                    objParam.Service_End_Date__c = dtTbl.Rows[0]["Service_End_Date__c"].ToString();
                    objParam.Service_End_Time__c = dtTbl.Rows[0]["Service_End_Time__c"].ToString();
                    objParam.Mechanic_Name__c = dtTbl.Rows[0]["Mechanic_Name__c"].ToString();
                    objParam.Work_Order_Number__c = dtTbl.Rows[0]["Work_Order_Number__c"].ToString();
                    objParam.Service_Kind__c = dtTbl.Rows[0]["Service_Kind__c"].ToString();
                    //objParam.Kind_Code__c = dtTbl.Rows[0]["Kind_Code__c"].ToString();
                    //objParam.Kind_Description__c = dtTbl.Rows[0]["Kind_Description__c"].ToString();
                    objParam.Odometer__c = dtTbl.Rows[0]["Odometer__c"].ToString();
                    objParam.Service_Type__c = dtTbl.Rows[0]["Service_Type__c"].ToString();
                    objParam.Stall_Code__c = dtTbl.Rows[0]["Stall_Code__c"].ToString();
                    objParam.Booking_Code__c = dtTbl.Rows[0]["Booking_Code__c"].ToString();
                    objParam.Mechanic_Notes__c = dtTbl.Rows[0]["Mechanic_Notes__c"].ToString();
                    objParam.Status__c = dtTbl.Rows[0]["Status__c"].ToString();

                    Task.Run(
        () => KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.SendData(sfMasterObject,
            User, String.Concat("/salesforce/services/", ParamServiceHistoryBookletSF.SObjectTypeName), objParam, false)
    ).Wait();
                    if (KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.IsSuccess)
                    {
                        //update SFID to SFServiceHistory Table
                        //item.IsSynchronizeSF = true;
                        //item.SynchronizeDateSF = DateTime.Now;
                        //item.RetrySF = 0;
                        //item.SFID = KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.Message;
                        try
                        {
                           // new SFServiceHistoryBookletFacade(User).Update(item);
                        }
                        catch (Exception ex)
                        {
                            InsertErrorLog(objID, ex);
                        }
                        Console.WriteLine(" = Success");
                    }
                    else
                    {
                        Console.WriteLine(" = Fail - " + KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.Message);
                    }
                    //InsertSynchronizeLog(StagingLogID, item.ID, KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.IsSuccess, KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message);
                }
            }
            else
            {
               // InsertSynchronizeLog(StagingLogID, item.ID, false, "Status data sudah didelete.");
            }
        }

        static void InsertToStaging(int objID)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            try
            {
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_Insert_PMHeaderToSFServiceHistoryBooklet '" + objID.ToString() + "'");
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
                string strJson = "";
                string msg = "";
                bool vReturn = false;

                SFServiceHistoryBookletFacade objSFServiceHistoryBookletFacade = new SFServiceHistoryBookletFacade(User);
                ArrayList arr = objSFServiceHistoryBookletFacade.RetieveListOfItemToSend(1);
                
                if (arr.Count > 0)
                {
                    Console.WriteLine("Process " + arr.Count + " data");
                    int n = 1;
                    foreach (SFServiceHistoryBooklet item in arr)
                    {
                        item.RetrySF = short.Parse((Int32.Parse(item.RetrySF.ToString()) + 1).ToString());
                        new SFServiceHistoryBookletFacade(User).Update(item);

                        Console.Write("Process SFServiceBookletHistory " + n.ToString() + " of " + arr.Count.ToString());

                        DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamServiceHistoryBooklet "+item.AssistServiceIncomingID +", "+ item.PMHeaderID);
                        if (dtSet.Tables.Count > 0)
                        {
                            DataTable dtTbl = dtSet.Tables[0];
                            if (dtTbl.Rows.Count > 0)
                            {
                                //dtTbl.ToCSV("");
                                var objParam = new ParamServiceHistoryBookletSF();
                                objParam.No_Rangka__c = dtTbl.Rows[0]["No_Rangka__c"].ToString();
                                //objParam.Old__c = dtTbl.Rows[0]["Old__c"].ToString();
                                objParam.MSP_No__c = dtTbl.Rows[0]["MSP_No__c"].ToString();
                                objParam.Dnet_ID__c = dtTbl.Rows[0]["Dnet_ID__c"].ToString();
                                objParam.Dealer_code__c = dtTbl.Rows[0]["Dealer_code__c"].ToString();
                                //objParam.Dealer_Name__c = dtTbl.Rows[0]["Dealer_Name__c"].ToString();
                                objParam.Service_Start_Date__c = dtTbl.Rows[0]["Service_Start_Date__c"].ToString();
                                objParam.Service_Start_Time__c = dtTbl.Rows[0]["Service_Start_Time__c"].ToString();
                                objParam.Service_End_Date__c = dtTbl.Rows[0]["Service_End_Date__c"].ToString();
                                objParam.Service_End_Time__c = dtTbl.Rows[0]["Service_End_Time__c"].ToString();
                                objParam.Mechanic_Name__c = dtTbl.Rows[0]["Mechanic_Name__c"].ToString();
                                objParam.Work_Order_Number__c = dtTbl.Rows[0]["Work_Order_Number__c"].ToString();
                                objParam.Service_Kind__c = dtTbl.Rows[0]["Service_Kind__c"].ToString();
                                //objParam.Kind_Code__c = dtTbl.Rows[0]["Kind_Code__c"].ToString();
                                //objParam.Kind_Description__c = dtTbl.Rows[0]["Kind_Description__c"].ToString();
                                objParam.Odometer__c = dtTbl.Rows[0]["Odometer__c"].ToString();
                                objParam.Service_Type__c = dtTbl.Rows[0]["Service_Type__c"].ToString();
                                objParam.Stall_Code__c = dtTbl.Rows[0]["Stall_Code__c"].ToString();
                                objParam.Booking_Code__c = dtTbl.Rows[0]["Booking_Code__c"].ToString();
                                objParam.Mechanic_Notes__c = dtTbl.Rows[0]["Mechanic_Notes__c"].ToString();
                                objParam.Status__c = dtTbl.Rows[0]["Status__c"].ToString();

                                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

                                Task.Run(
                                    () => KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.SendData(sfMasterObject,
                                        User, String.Concat("/salesforce/services/", ParamServiceHistoryBookletSF.SObjectTypeName), objParam, false)
                                ).Wait();

                                if (KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.IsSuccess)
                                {
                                    var obj = JObject.Parse(KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.Message);
                                    vReturn = obj["Status"].ToString().Equals("0");
                                    msg = obj["Message"].ToString();

                                    if (vReturn)
                                    {
                                        //update SFID to SFServiceHistory Table
                                        item.IsSynchronizeSF = true;
                                        item.SynchronizeDateSF = DateTime.Now;
                                        item.RetrySF = 0;
                                        item.SFID = msg;
                                        try
                                        {
                                            new SFServiceHistoryBookletFacade(User).Update(item);
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

                                InsertIntoWSLog(vReturn, msg, strJson, "K:SFServiceHistoryBooklet");
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

        static void InsertToMMID(int objID)
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

                SFServiceHistoryBookletFacade objSFServiceHistoryBookletFacade = new SFServiceHistoryBookletFacade(User);
                ArrayList arr = objSFServiceHistoryBookletFacade.RetieveListOfItemToSend(2);

                if (arr.Count > 0)
                {
                    Console.WriteLine("Process " + arr.Count + " data");
                    int n = 1;
                    foreach (SFServiceHistoryBooklet item in arr)
                    {
                        item.RetryMMID = short.Parse((Int32.Parse(item.RetryMMID.ToString()) + 1).ToString());
                        new SFServiceHistoryBookletFacade(User).Update(item);

                        Console.Write("Process SFServiceBookletHistory MMID" + n.ToString() + " of " + arr.Count.ToString());

                        DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamServiceHistoryBooklet " + item.AssistServiceIncomingID + ", " + item.PMHeaderID);
                        if (dtSet.Tables.Count > 0)
                        {
                            DataTable dtTbl = dtSet.Tables[0];
                            if (dtTbl.Rows.Count > 0)
                            {
                                //dtTbl.ToCSV("");
                                var objParam = new ParamServiceHistoryBookletMMID();
                                objParam.No_Rangka__c = dtTbl.Rows[0]["No_Rangka__c"].ToString();
                                //objParam.Old__c = dtTbl.Rows[0]["Old__c"].ToString();
                                objParam.MSP_No__c = dtTbl.Rows[0]["MSP_No__c"].ToString();
                                objParam.Dnet_ID__c = dtTbl.Rows[0]["Dnet_ID__c"].ToString();
                                objParam.Dealer_code__c = dtTbl.Rows[0]["Dealer_code__c"].ToString();
                                //objParam.Dealer_Name__c = dtTbl.Rows[0]["Dealer_Name__c"].ToString();
                                objParam.Service_Start_Date__c = dtTbl.Rows[0]["Service_Start_Date__c"].ToString();
                                objParam.Service_Start_Time__c = dtTbl.Rows[0]["Service_Start_Time__c"].ToString();
                                objParam.Service_End_Date__c = dtTbl.Rows[0]["Service_End_Date__c"].ToString();
                                objParam.Service_End_Time__c = dtTbl.Rows[0]["Service_End_Time__c"].ToString();
                                objParam.Mechanic_Name__c = dtTbl.Rows[0]["Mechanic_Name__c"].ToString();
                                objParam.Work_Order_Number__c = dtTbl.Rows[0]["Work_Order_Number__c"].ToString();
                                objParam.Service_Kind__c = dtTbl.Rows[0]["Service_Kind__c"].ToString();
                                //objParam.Kind_Code__c = dtTbl.Rows[0]["Kind_Code__c"].ToString();
                                //objParam.Kind_Description__c = dtTbl.Rows[0]["Kind_Description__c"].ToString();
                                objParam.Odometer__c = dtTbl.Rows[0]["Odometer__c"].ToString();
                                objParam.Service_Type__c = dtTbl.Rows[0]["Service_Type__c"].ToString();
                                objParam.Stall_Code__c = dtTbl.Rows[0]["Stall_Code__c"].ToString();
                                objParam.Booking_Code__c = dtTbl.Rows[0]["Booking_Code__c"].ToString();
                                objParam.Mechanic_Notes__c = dtTbl.Rows[0]["Mechanic_Notes__c"].ToString();
                                objParam.Status__c = dtTbl.Rows[0]["Status__c"].ToString();

                                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

                                Task.Run(
                                    () => KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.SendData(sfMasterObject,
                                        User, String.Concat("/mmid/services/", ParamServiceHistoryBookletMMID.SObjectTypeName), objParam, false)
                                ).Wait();

                                if (KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.IsSuccess)
                                {
                                    var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<MMIDResult>(KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.Message);
                                    var details = obj.data.details;
                                    vReturn = !obj.error;

                                    if (vReturn && details == null)
                                        msg = obj.alerts.message + " without error";
                                    else
                                        msg = obj.alerts.message + ", with error details: " +obj.data.details.reason;

                                    if (vReturn && details == null)
                                    {
                                        //update SFID to SFServiceHistory Table
                                        item.IsSynchronizeMMID = true;
                                        item.SynchronizeDateMMID = DateTime.Now;
                                        item.RetryMMID = 0;
                                        try
                                        {
                                            new SFServiceHistoryBookletFacade(User).Update(item);
                                        }
                                        catch (Exception ex)
                                        {
                                            InsertErrorLog(objID, ex);
                                        }
                                        InsertIntoWSLog(vReturn, msg, strJson, "K:MMIDServiceHistoryBooklet");
                                        InsertSynchronizeLog(StagingLogID, item.ID, vReturn, msg);
                                    }
                                    else
                                    {
                                        InsertIntoWSLog(vReturn, msg, strJson, "K:MMIDServiceHistoryBooklet");
                                        InsertSynchronizeLog(StagingLogID, item.ID, false, msg);
                                    }
                                }
                                else
                                {
                                    vReturn = KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.IsSuccess;
                                    msg = KTB.DNet.WebApi.Models.MiddlewareSOA.MiddlewareSOA.Message;
                                }
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

        private static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
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
