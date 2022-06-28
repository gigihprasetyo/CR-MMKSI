using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTB.DNet.Salesforce.Class;
using KTB.DNET.BusinessFacade;
using System.Security.Principal;

namespace KTB.DNet.Salesforce.Jobs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start program...");
                var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
                var dtSet = new DataSet();
                dtSet = new SFMasterObjectFacade(User).RetrieveSp("EXEC sp_SF_MasterObjectActive");
                if (dtSet.Tables.Count > 0)
                {
                    DataTable dtTable = dtSet.Tables[0];
                    if (dtTable.Rows.Count > 0)
                    {
                        Console.WriteLine("Process " + dtTable.Rows.Count.ToString() + " master object");
                        foreach (DataRow item in dtTable.Rows)
                        {
                            switch (item["Name"].ToString().ToLower())
                            {
                                //case "leadcustomer":
                                //    {
                                //        Console.WriteLine("Process " + item["Name"].ToString() + " master object");
                                //        var _leadCustomer = new LeadCustomerParser();
                                //        if (_leadCustomer.IsTransferToSalesforce())
                                //        {
                                //            _leadCustomer.Process();
                                //            //CaseManagementProcess(); //pending, intensitas masih rendah
                                //        }
                                //        break;
                                //    }
                                case "contact":
                                    {
                                        Console.WriteLine("Process " + item["Name"].ToString() + " master object");
                                        new ContactParser().Process(Convert.ToInt32(item["ID"].ToString()));
                                        break;
                                    }
                                case "purchasetransaction":
                                    {
                                        Console.WriteLine("Process " + item["Name"].ToString() + " master object");
                                        new PurchaseTransactionParser().Process(Convert.ToInt32(item["ID"].ToString()));
                                        break;
                                    }
                                case "servicehistory":
                                    {
                                        Console.WriteLine("Process " + item["Name"].ToString() + " master object");
                                        new ServiceHistoryParser().Process(Convert.ToInt32(item["ID"].ToString()));
                                        break;
                                    }
                                case "msp":
                                    {
                                        Console.WriteLine("Process " + item["Name"].ToString() + " master object");
                                        new SmartPackageParser().Process(Convert.ToInt32(item["ID"].ToString()));
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            catch (Exception a)
            {

                a = null;
            }

            

        }

    }
}
