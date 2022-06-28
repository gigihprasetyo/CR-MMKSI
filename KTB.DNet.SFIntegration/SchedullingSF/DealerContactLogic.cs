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

namespace KTB.DNet.SFIntegration.SchedullingSF
{
    public static class DealerContactLogic
    {
        public static async Task WSSalesforce_Contact(bool isInit)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();

            //if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["InitData"]))
            if (isInit)
            {
                InsertToStaging(1);
            }
            InsertToSalesForce(1);
        }

        static void InsertToStaging(int objID)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            try
            {
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_InsertEndCustomerToSFContact '" + objID.ToString() + "'");
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
                String strJson = "";
                int StagingLogID = InsertStagingLog(objID, true, string.Empty);

                CriteriaComposite crt = new CriteriaComposite(new Criteria(typeof(SFContact), "RowStatus", MatchType.Exact, 0));
                crt.opAnd(new Criteria(typeof(SFContact), "IsActive", MatchType.Exact, Convert.ToInt32(true)));
                crt.opAnd(new Criteria(typeof(SFContact), "IsSynchronize", MatchType.Exact, Convert.ToInt32(false)));
                System.Collections.ArrayList arr = new SFContactFacade(User).RetieveListOfItemToSend();

                Console.WriteLine("Process " + arr.Count + " data");
                if (arr.Count > 0)
                {
                    int n = 1;
                    int index = 0;
                    foreach (SFContact item in arr)
                    {

                        item.RowStatus = short.Parse((Int32.Parse(item.RowStatus.ToString()) + 1).ToString());
                        new SFContactFacade(User).Update(item);

                        Console.Write("Process " + n.ToString() + " of " + arr.Count.ToString());
                        DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamContact " + item.EndCustomer.ID);
                        if (dtSet.Tables.Count > 0)
                        {
                            DataTable dtTbl = dtSet.Tables[0];
                            if (dtTbl.Rows.Count > 0)
                            {
                                if (dtTbl.Rows[0]["KtpNo"].ToString() == "TIDAK ADA")
                                {
                                    InsertSynchronizeLog(StagingLogID, item.ID, false, "Ktp No tidak ada");

                                    Console.WriteLine(" = Fail - KTP No tidak ada");
                                    n += 1;
                                    continue;
                                }

                                var objParam = new paramContact();
                                objParam.LastName = dtTbl.Rows[0]["LastName"].ToString();
                                objParam.Gender__c = dtTbl.Rows[0]["Gender"].ToString();
                                objParam.MobilePhone = dtTbl.Rows[0]["MobilePhone"].ToString();
                                objParam.Email = dtTbl.Rows[0]["Email"].ToString();
                                objParam.Address__c = dtTbl.Rows[0]["Address"].ToString();
                                objParam.Kabupaten_Kota__c = dtTbl.Rows[0]["Kabupaten"].ToString();
                                objParam.Provinsi__c = dtTbl.Rows[0]["Province"].ToString();
                                objParam.Kelurahan__c = dtTbl.Rows[0]["Kelurahan"].ToString();
                                objParam.ID_Type__c = "KTP";
                                objParam.ID_Number__c = dtTbl.Rows[0]["KtpNo"].ToString();
                                objParam.LeadSource = "DNET";

                                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

                                Task.Run(
                    () => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendData(sfMasterObject,
                        User, String.Concat("services/apexrest/", paramContact.SObjectTypeName), objParam, false)
                ).Wait();

                                if (KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess)
                                {
                                    //update SFID to SFContact Table
                                    item.IsSynchronize = true;
                                    item.SynchronizeDate = DateTime.Now;
                                    item.SFID = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;
                                    try
                                    {
                                        new SFContactFacade(User).Update(item);
                                    }
                                    catch (Exception ex)
                                    {
                                        InsertErrorLog(objID, ex);
                                    }

                                    Console.WriteLine(" = Success");
                                }
                                else
                                {
                                    if (KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message.ToLower().IndexOf("duplicates_detected") > -1)
                                    {
                                        item.IsSynchronize = true;
                                        item.IsActive = false;
                                        item.SynchronizeDate = DateTime.Now;
                                        item.SFID = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;
                                        try
                                        {
                                            new SFContactFacade(User).Update(item);
                                        }
                                        catch (Exception ex)
                                        {
                                            InsertErrorLog(objID, ex);
                                        }
                                    }
                                }

                                InsertSynchronizeLog(StagingLogID, item.ID, KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess, KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine(" = Fail - Data tidak ada saat get param");
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

    public class paramContact
    {
        public paramContact()
        { }
        public const String SObjectTypeName = "paramContactDnet";
        public String LastName { get; set; }
        public String Gender__c { get; set; }
        public String MobilePhone { get; set; }
        public String Email { get; set; }
        public String Address__c { get; set; }
        public String Kabupaten_Kota__c { get; set; }
        public String Provinsi__c { get; set; }
        public String Kelurahan__c { get; set; }
        public String ID_Type__c { get; set; }
        public String ID_Number__c { get; set; }
        public String LeadSource { get; set; }
    }


}
