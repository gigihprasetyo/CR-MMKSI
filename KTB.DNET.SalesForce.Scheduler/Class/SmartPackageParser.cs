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
    class SmartPackageParser
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
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_InsertMSPRegHistoryToSFSmartPackage '" + objID.ToString() + "'");
            }
            catch(Exception ex)
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
                        //crt = new CriteriaComposite(new Criteria(typeof(SFPurchaseTransaction), "EndCustomer.ID", MatchType.Exact, item.MSPRegistrationHistory.MSPRegistration.ChassisMaster.EndCustomer.ID));
                        //crt.opAnd(new Criteria(typeof(SFPurchaseTransaction), "RowStatus", MatchType.Exact, 0));
                        //crt.opAnd(new Criteria(typeof(SFPurchaseTransaction), "IsActive", MatchType.Exact, 1));
                        //crt.opAnd(new Criteria(typeof(SFPurchaseTransaction), "IsSynchronize", MatchType.Exact, 1));
                        //ArrayList arrDt = new SFPurchaseTransactionFacade(User).Retrieve(crt);
                        //if (arrDt.Count == 0)
                        //{
                        //    new LogParser().InsertSynchronizeLog(StagingLogID, item.ID, false, "Purchase history belum synchronize dengan sales force.");
                        //    continue;
                        //}
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
                        User, String.Concat("services/apexrest/", paramSmartPackage.SObjectTypeName), objParam,false)
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
