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
    public class ContactParser
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
                new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_InsertEndCustomerToSFContact '" + objID.ToString() + "'");
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
                String strJson = "";
                int StagingLogID = new LogParser().InsertStagingLog(objID, true, string.Empty);
                
                CriteriaComposite crt = new CriteriaComposite(new Criteria(typeof(SFContact), "RowStatus", MatchType.Exact, 0));
                crt.opAnd(new Criteria(typeof(SFContact), "IsActive", MatchType.Exact, Convert.ToInt32(true)));
                crt.opAnd(new Criteria(typeof(SFContact), "IsSynchronize", MatchType.Exact, Convert.ToInt32(false)));
                ArrayList arr = new SFContactFacade(User).Retrieve(crt);

                Console.WriteLine("Process " + arr.Count + " data");
                if (arr.Count > 0)
                {
                    int n = 1;
                    foreach (SFContact item in arr)
                    {
                        Console.Write("Process " + n.ToString() + " of " + arr.Count.ToString());
                        DataSet dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_ParamContact " + item.EndCustomer.ID );
                        if (dtSet.Tables.Count > 0)
                        {
                            DataTable dtTbl = dtSet.Tables[0];
                            if (dtTbl.Rows.Count > 0)
                            {
                                if(dtTbl.Rows[0]["KtpNo"].ToString() == "TIDAK ADA"){
                                    new LogParser().InsertSynchronizeLog(StagingLogID, item.ID, false, "Ktp No tidak ada");

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
                        User, String.Concat("services/apexrest/", paramContact.SObjectTypeName), objParam,false)
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
                            Console.WriteLine(" = Fail - Data tidak ada saat get param");
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
