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

namespace KTB.DNet.Salesforce.Class
{
    public class ServiceHistoryParser
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
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_InsertPMHeaderToSFServiceHistory '" + objID.ToString() + "'");
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

                //CriteriaComposite crt = new CriteriaComposite(new Criteria(typeof(SFServiceHistory), "RowStatus", MatchType.Exact, 0));
                //crt.opAnd(new Criteria(typeof(SFServiceHistory), "IsActive", MatchType.Exact, Convert.ToInt32(true)));
                //crt.opAnd(new Criteria(typeof(SFServiceHistory), "IsSynchronize", MatchType.Exact, Convert.ToInt32(false)));
                //ArrayList arr = new SFServiceHistoryFacade(User).Retrieve(crt);

                ArrayList arr = new ArrayList();
                arr = new SFServiceHistoryFacade(User).RetieveListOfItemToSend();
                if (arr.Count > 0)
                {
                    Console.WriteLine("Process " + arr.Count + " data");
                    int n = 1;
                    foreach (SFServiceHistory item in arr)
                    {
                        Console.Write("Process SFServiceHistory " + n.ToString() + " of " + arr.Count.ToString());

                        //crt = new CriteriaComposite(new Criteria(typeof(SFPurchaseTransaction), "EndCustomer.ID", MatchType.Exact, item.PMHeader.ChassisMaster.EndCustomer.ID));
                        //crt.opAnd(new Criteria(typeof(SFPurchaseTransaction),"RowStatus",MatchType.Exact,0));
                        //crt.opAnd(new Criteria(typeof(SFPurchaseTransaction), "IsActive", MatchType.Exact, 1));
                        //crt.opAnd(new Criteria(typeof(SFPurchaseTransaction), "IsSynchronize", MatchType.Exact, 1));
                        //ArrayList arrDt = new SFPurchaseTransactionFacade(User).Retrieve(crt);
                        //if (arrDt.Count == 0)
                        //{
                        //    new LogParser().InsertSynchronizeLog(StagingLogID, item.ID, false, "Purchase history belum synchronize dengan sales force.");
                        //    continue;
                        //}

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
                                        new LogParser().InsertErrorLog(objID, ex);
                                    }
                                    Console.WriteLine(" = Success");
                                }
                                else
                                {
                                    Console.WriteLine(" = Fail - " + KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message);
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
    }
}
