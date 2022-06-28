using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.BusinessFacade.FinishUnit;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.BusinessFacade.Salesman;
using KTB.DNet.BusinessFacade;
using KTB.DNet.Domain;
using KTB.DNet.WebApi.Models;
using KTB.DNet.Domain.Search;
using System.Text.RegularExpressions;
using System.Security.Principal;
using KTB.DNet.Salesforce;
using KTB.DNet.Utility;
using KTB.DNET.BusinessFacade;

namespace KTB.DNet.Salesforce.Jobs
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {

               

                string strFileName = ("Log_SPK_Dealer_Branch" + DateTime.Now.ToString("yyyyMMdd HHmm") + ".txt");
                string startupPath = System.IO.Directory.GetCurrentDirectory() + "/Log/";

                if (!Directory.Exists(startupPath))
                {
                    Directory.CreateDirectory(startupPath);
                }
                string strStream = Path.Combine(startupPath, strFileName);

                ostrm = new FileStream(strStream, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
                Console.SetOut(writer);

                if (IsTransferToSalesforce())
                {

                    //CheckDealer();
                    LeadCustomerProcess();
                    //MasterDealerProcess();
                    //MasterDealerBranchProcess();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Log.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }

            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
        }

        #region "Checking Dealer Last Updated TIme"

        static void CheckDealer()
        {
            try
            {
                Console.WriteLine("Proses Pengambilan Data Dealer, Branch & SPK");

                var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

                SFReferenceFacade facade = new SFReferenceFacade(User);
                facade.RetrieveSP("Dealer");
                facade.RetrieveSP("DealerBranch");

                facade.RetrieveSP();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error pada pengambilan data. Pesan Error : " + e.ToString());
                return;
            }
        }

        #endregion

        #region "MasterDealer"

        static void MasterDealerProcess()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            SFReferenceFacade facade = new SFReferenceFacade(User);
            ArrayList arr = facade.RetrieveSP(1); // 1 = dealer

            int iCount = 1;
            Console.WriteLine("_____________________________________________________________________________________________________________ \n");
            Console.WriteLine("Total Dealer :" + arr.Count.ToString() + "\n");

            foreach (SFReference objpkt in arr)
            {

                Dealer objDealer;

                objDealer = new DealerFacade(User).Retrieve(objpkt.RefID);

                Console.WriteLine("Processing " + iCount.ToString() + " of " + arr.Count.ToString() + ". Dealer Code : " + objDealer.DealerCode + " at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");

                if (objpkt.SalesForceID == null || objpkt.SalesForceID == string.Empty)
                {
                    InsertMasterDealer(objpkt);

                    Console.WriteLine("Process tansfer selesai !" + "\n");
                }
                else
                {
                    //UpdateMasterDealer(objpkt);
                    InsertMasterDealer(objpkt);
                }
                iCount++;
            }
        }

        static bool InsertMasterDealer(SFReference objpkt)
        {

            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            bool vReturn = false;
            bool vResult = true;
            string msg = "";
            string strJson = "";

            Dealer objDealerSF = new DealerFacade(User).Retrieve(objpkt.RefID);

            //objDealerSF = new DealerFacade(User).Retrieve(objpkt.Dealer.DealerCode);

            var objParam = new KTB.DNet.WebApi.Models.paramMasterDealer();

            objParam.name = objDealerSF.DealerName;
            objParam.Code__c = objDealerSF.DealerCode;

            if (objDealerSF.DealerGroup == null)
            {
                objParam.Group__c = "";
            }
            else
            {
                objParam.Group__c = objDealerSF.DealerGroup.GroupName;
            }
            objParam.Province__c = objDealerSF.Province.ProvinceName;
            objParam.City__c = objDealerSF.City.CityName;
            objParam.Address__c = objDealerSF.Address;
            objParam.Parent_Code__c = "";

            string strLayanan = "";
            if (objDealerSF.SalesUnitFlag == "1")
            {
                strLayanan += "Sales";
                if (objDealerSF.ServiceFlag == "1" || objDealerSF.SparepartFlag == "1") { strLayanan += ","; }
            }
            if (objDealerSF.ServiceFlag == "1")
            {
                strLayanan += "Service";
                if (objDealerSF.SparepartFlag == "1") { strLayanan += ","; }
            }
            if (objDealerSF.SparepartFlag == "1")
            {
                strLayanan += "SparePart";
            }

            objParam.Layanan__c = strLayanan;
            objParam.Address__c = objDealerSF.Address;

            ArrayList arrCat = new ArrayList();
            DealerCategoryFacade dcFacade = new DealerCategoryFacade(User);
            CriteriaComposite cri = new CriteriaComposite(new Criteria(typeof(DealerCategory), "RowStatus", (short)(DBRowStatus.Active)));
            cri.opAnd(new Criteria(typeof(DealerCategory), "Dealer.ID", MatchType.Exact, (int)(objDealerSF.ID)));

            arrCat = dcFacade.Retrieve(cri);

            strLayanan = "";
            int x = 1;
            foreach (DealerCategory row in arrCat)
            {
                strLayanan += row.Category.CategoryCode;
                if (arrCat.Count > 1 && x != arrCat.Count)
                {
                    strLayanan += ",";
                }
                x++;
            }

            objParam.Alokasi__c = strLayanan;
            objParam.Type__c = "Dealer";
            objParam.Telephone_1__c = objDealerSF.Phone;
            objParam.Telephone_2__c = "";
            objParam.Telephone_3__c = "";
            objParam.Telephone_4__c = "";
            objParam.Telephone_5__c = "";
            if (objDealerSF.Fax.Length > 5) { objParam.Fax__c = objDealerSF.Fax; }
            else { objParam.Fax__c = string.Empty; }
            objParam.Status__c = objDealerSF.StatusDealer;

            Task.Run(() => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendDealerPost(User, String.Concat("services/apexrest/", paramMasterDealer.SObjectTypeName), objParam)).Wait();
            strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

            vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
            msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;

            #region Log
            CreateLog(vReturn.ToString(), msg.ToString(), "K:SALESFORCEINSERTDEALER\n", strJson);
            #endregion

            if (vReturn)
            {
                objpkt.IsSend = true;
                objpkt.SalesForceID = msg;
                UpdateDealer(objpkt);
                Console.WriteLine("Proses Insert (method Post Dealer) : Berhasil \n");
            }

            return true;

        }

        static void UpdateDealer(SFReference objpkt)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            SFReferenceFacade Fac = new SFReferenceFacade(User);
            Fac.Update(objpkt);
        }

        #endregion

        #region "MasterDealerBranch"

        static void MasterDealerBranchProcess()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            SFReferenceFacade facade = new SFReferenceFacade(User);
            ArrayList arr = facade.RetrieveSP(2); // 2 = Branch Dealer

            int iCount = 1;

            Console.WriteLine("_____________________________________________________________________________________________________________ \n");
            Console.WriteLine("Total Dealer Branch :" + arr.Count.ToString() + "\n");

            foreach (SFReference objpkt in arr)
            {
                DealerBranch objDealerBranch = new DealerBranchFacade(User).Retrieve(objpkt.RefID);

                Console.WriteLine("Processing " + iCount.ToString() + " of " + arr.Count.ToString() + ". Dealer Branch Code : " + objDealerBranch.DealerBranchCode + " at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");

                if (objpkt.SalesForceID == null)
                {
                    InsertMasterDealerBranch(objpkt);

                    Console.WriteLine("Process tansfer selesai !" + "\n");
                    //Console.ReadLine();
                }
                else
                {
                    //UpdateMasterDealer(objpkt);
                    InsertMasterDealerBranch(objpkt);
                }
                iCount++;
            }
        }

        static bool InsertMasterDealerBranch(SFReference objpkt)
        {

            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            bool vReturn = false;
            bool vResult = true;
            string msg = "";
            string strJson = "";

            Dealer objDealer;

            DealerBranch objDealerSF;

            //objDealer = new DealerFacade(User).Retrieve(objpkt.Dealer.DealerCode);

            objDealerSF = new DealerBranchFacade(User).Retrieve(objpkt.RefID);
            //objDealerSF.Dealer = objDealer;

            var objParam = new KTB.DNet.WebApi.Models.paramMasterDealer();

            objParam.name = objDealerSF.Name;
            objParam.Code__c = objDealerSF.DealerBranchCode;
            if (objDealerSF.Dealer.DealerGroup == null)
            {
                objParam.Group__c = "";
            }
            else
            {
                objParam.Group__c = objDealerSF.Dealer.DealerGroup.GroupName;
            }
            objParam.Province__c = objDealerSF.Province.ProvinceName;
            objParam.City__c = objDealerSF.City.CityName;
            objParam.Address__c = objDealerSF.Address;
            objParam.Parent_Code__c = objDealerSF.Dealer.DealerCode;

            string strLayanan = "";
            if (objDealerSF.SalesUnitFlag == "1")
            {
                strLayanan += "Sales";
                if (objDealerSF.ServiceFlag == "1" || objDealerSF.SparepartFlag == "1") { strLayanan += ","; }
            }
            if (objDealerSF.ServiceFlag == "1")
            {
                strLayanan += "Service";
                if (objDealerSF.SparepartFlag == "1") { strLayanan += ","; }
            }
            if (objDealerSF.SparepartFlag == "1")
            {
                strLayanan += "SparePart";
            }
            objParam.Layanan__c = strLayanan;

            ArrayList arrCat = new ArrayList();
            DealerCategoryFacade dcFacade = new DealerCategoryFacade(User);
            CriteriaComposite cri = new CriteriaComposite(new Criteria(typeof(DealerCategory), "RowStatus", (short)(DBRowStatus.Active)));
            cri.opAnd(new Criteria(typeof(DealerCategory), "Dealer.ID", MatchType.Exact, (int)(objDealerSF.Dealer.ID)));

            arrCat = dcFacade.Retrieve(cri);

            strLayanan = "";
            int x = 1;
            foreach (DealerCategory row in arrCat)
            {
                strLayanan += row.Category.CategoryCode;
                if (arrCat.Count > 1 && x != arrCat.Count)
                {
                    strLayanan += ",";
                }
                x++;
            }

            objParam.Alokasi__c = strLayanan;
            objParam.Type__c = "Branch";
            objParam.Address__c = objDealerSF.Address;
            objParam.Telephone_1__c = objDealerSF.Phone;
            objParam.Telephone_2__c = "";
            objParam.Telephone_3__c = "";
            objParam.Telephone_4__c = "";
            objParam.Telephone_5__c = "";
            objParam.Fax__c = objDealerSF.Fax;
            if (objDealerSF.Status == "1")
            { objParam.Status__c = "Aktif"; }
            else
            {
                objParam.Status__c = "Non Aktif";
            }

            Task.Run(() => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendDealerPost(User, String.Concat("services/apexrest/", paramMasterDealer.SObjectTypeName), objParam)).Wait();
            strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

            vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
            msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;

            Console.WriteLine(msg);

            #region Log
            CreateLog(vReturn.ToString(), msg.ToString(), "K:SALESFORCEINSERTDEALER\n", strJson);
            #endregion

            if (vReturn)
            {
                objpkt.IsSend = true;
                objpkt.SalesForceID = msg;
                UpdateDealerBranch(objpkt);
            }

            return true;

        }

        static void UpdateDealerBranch(SFReference objpkt)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            SFReferenceFacade Fac = new SFReferenceFacade(User);
            Fac.Update(objpkt);
        }

        #endregion

        #region LeadCustomerProcess

        static void LeadCustomerProcess()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            String sql = @"select * from SPKHeader where SPKNumber in ('2109010177','2109003326')";

            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SPKHeader), "ID", MatchType.InSet, "(" + sql + ")"));
            SPKHeaderFacade facade = new SPKHeaderFacade(User);
            ArrayList arr = facade.RetrieveSP(sql);

            //ArrayList arr = facade.RetrieveQuery(sql);

            ArrayList arrTypeCode = GetNewType();

            Console.WriteLine("_____________________________________________________________________________________________________________ \n");
            Console.WriteLine("Total SPK :" + arr.Count.ToString() + "\n");
            int iCount = 0;
            foreach (SPKHeader spk in arr)
            {
                iCount++;
                Console.WriteLine("Processing " + iCount.ToString() + " of " + arr.Count.ToString() + ". SPK Number : " + spk.SPKNumber + " at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");

                bool isSms = false;
                foreach (SPKDetail detail in spk.SPKDetails)
                {
                    foreach (VechileType type in arrTypeCode)
                    {
                        if (detail.VehicleTypeCode == type.VechileTypeCode)
                        {
                            isSms = true;
                            break;
                        }
                    }
                }

                if (spk.SPKCustomer.SAPCustomer != null)
                {

                    CriteriaComposite critResponse = new CriteriaComposite(new Criteria(typeof(SAPCustomerResponse), "RowStatus", MatchType.Exact, (short)(DBRowStatus.Active)));
                    critResponse.opAnd(new Criteria(typeof(SAPCustomerResponse), "SAPCustomer.ID", MatchType.Exact, spk.SPKCustomer.SAPCustomer.ID));
                    critResponse.opAnd(new Criteria(typeof(SAPCustomerResponse), "Status", MatchType.Exact, (int)(EnumSAPCustomerResponse.SAPCustomerResponse.SPK)));
                    ArrayList arrResponse = new KTB.DNET.BusinessFacade.SAPCustomerResponseFacade(User).Retrieve(critResponse);

                    SAPCustomerResponse response = new SAPCustomerResponse();
                    if (arrResponse.Count == 0)
                    {
                        response.SAPCustomer = spk.SPKCustomer.SAPCustomer;
                        response.Status = (int)(EnumSAPCustomerResponse.SAPCustomerResponse.SPK);
                        KTB.DNET.BusinessFacade.SAPCustomerResponseFacade responseFacade = new KTB.DNET.BusinessFacade.SAPCustomerResponseFacade(User);
                        int id = responseFacade.Insert(response);

                        response = new SAPCustomerResponse();
                        response = responseFacade.Retrieve(id);
                    }
                    else
                    {
                        response = (SAPCustomerResponse)(arrResponse[0]);
                    }

                    bool vResult = true;
                    string msg = "";
                    if (spk.SPKCustomer.SAPCustomer.SalesforceID != "")
                    {
                        vResult = UpdateOpportunity(spk, isSms, ref msg);
                        UpdateOpportunityDetail(spk);
                    }
                    else
                    {
                        vResult = InsertOpportunity(spk, isSms, ref msg);
                        Console.WriteLine("------> Process tansfer SPK : " + spk.SPKNumber + " " + vResult.ToString() + " err : " + msg + "\n");
                    }

                    if (vResult)
                    {
                        response.IsSend = 1;
                        KTB.DNET.BusinessFacade.SAPCustomerResponseFacade responseFacade = new KTB.DNET.BusinessFacade.SAPCustomerResponseFacade(User);
                        int iResult = responseFacade.Update(response);
                    }
                }
                else
                {
                    bool vResult = true;
                    string msg = "";
                    vResult = UpdateOpportunityDetail(spk);
                    vResult = InsertOpportunity(spk, isSms, ref msg);
                    Console.WriteLine("------> Process tansfer SPK : " + spk.SPKNumber + " " + vResult.ToString() + " err : " + msg + "\n");
                }

                UpdateSpkHeader(spk);

            }

            UpdateOpportunityDetail(null);

            Console.WriteLine("Process tansfer selesai !" + "\n");
        }

        static bool InsertOpportunity(SPKHeader spk, bool isSms, ref string msg)
        {
            bool vReturn = false;
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            string strJson = "";
            try
            {
                var objParam = new KTB.DNet.WebApi.Models.paramWalkinOpportunity();

                objParam.Dealer_Code__c = spk.Dealer.DealerCode;
                objParam.Dealer_Name__c = spk.Dealer.DealerName;
                objParam.Consumen_Type__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.TipeCustomer, "EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer");
                objParam.Name = Regex.Replace(Regex.Replace(spk.SPKCustomer.Name1, @"\t|\n|\r", ""), @"&|#", " Dan ");
                objParam.Address__c = Regex.Replace(Regex.Replace(spk.SPKCustomer.Alamat, @"\t|\n|\r", ""), @"&|#", " Dan ");
                if (EmailAddressCheck(spk.SPKCustomer.Email))
                { objParam.Email__c = spk.SPKCustomer.Email; }
                else
                { objParam.Email__c = String.Empty; }
                objParam.Gender__c = String.Empty;
                objParam.Mobile_Phone__c = String.Empty;
                objParam.ID_Type__c = String.Empty;
                objParam.ID_Number__c = String.Empty;
                objParam.MMKSI_WEB_ID__c = String.Empty;

                if (spk.SPKCustomer.SAPCustomer != null)
                {
                    objParam.Mobile_Phone__c = spk.SPKCustomer.HpNo;
                }

                if (spk.SPKCustomer.SPKCustomerProfiles.Count > 0)
                {
                    foreach (SPKCustomerProfile _profiles in spk.SPKCustomer.SPKCustomerProfiles)
                    {
                        if (_profiles.ProfileHeader.ID == 27) { objParam.Gender__c = (_profiles.ProfileValue == "LK" ? "Male" : "Female"); }

                        if (_profiles.ProfileHeader.ID == 29)
                        {
                            EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest tipeCustomer = (EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest)(spk.SPKCustomer.TipeCustomer);
                            if (tipeCustomer == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan)
                            {
                                objParam.ID_Type__c = "KTP";
                            }
                            else
                            {
                                objParam.ID_Type__c = "TDP";
                            }
                            objParam.ID_Number__c = Regex.Replace(Regex.Replace(_profiles.ProfileValue, @"\t|\n|\r", ""), @"&|#", " Dan ");
                        }
                        if (_profiles.ProfileHeader.ID == 30)
                        {
                            if (objParam.Mobile_Phone__c == "")
                            {
                                objParam.Mobile_Phone__c = _profiles.ProfileValue;
                            }
                            objParam.Phone_No__c = _profiles.ProfileValue;
                        }
                    }
                }
                else
                {

                }
                if (spk.SPKCustomer.SAPCustomer != null)
                {
                    objParam.Information_Type__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.InformationType, "EnumInformationType.InformationType"); 
                    //EnumInformationType.GetStringInformationType(spk.SPKCustomer.SAPCustomer.InformationType);
                    objParam.Customer_Purposes__c =  EnumCustomerPurpose.GetStringCustomerPurpose(spk.SPKCustomer.SAPCustomer.CustomerPurpose);
                    objParam.LeadSource = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.InformationSource, "EnumInformationSource.InformationSource.Salesforce");
                    //EnumInformationSource.GetStringInformationSource(spk.SPKCustomer.SAPCustomer.InformationSource);
                    if (spk.SPKCustomer.SAPCustomer.ProspectDate == null)
                    { objParam.Prospect_Date__c = spk.SPKCustomer.SAPCustomer.CreatedTime.ToString("yyyy-MM-dd"); }
                    else
                    { objParam.Prospect_Date__c = spk.SPKCustomer.SAPCustomer.ProspectDate.ToString("yyyy-MM-dd"); }
                    if (spk.SPKCustomer.SAPCustomer.SalesforceID != "")
                    { objParam.MMKSI_WEB_ID__c = spk.SPKCustomer.SAPCustomer.WebID; }
                    else { objParam.MMKSI_WEB_ID__c = string.Empty; }
                }
                else
                {
                    objParam.Information_Type__c = String.Empty;
                    objParam.Customer_Purposes__c = String.Empty;
                    objParam.LeadSource = String.Empty;
                    objParam.Prospect_Date__c = spk.SPKCustomer.CreatedTime.ToString("yyyy-MM-dd");
                    objParam.MMKSI_WEB_ID__c = string.Empty;
                }

                objParam.SPK_No__c = spk.SPKNumber;
                objParam.Dealer_SPK_No__c = spk.DealerSPKNumber;
                objParam.SPK_Status__c = CommonFunction.GetEnumDescription(short.Parse(spk.Status), "EnumStatusSPK.Status");
                objParam.Validation_Key__c = spk.ValidationKey;
                objParam.StageName = EnumSAPCustomerResponse.GetStringValue((int)(EnumSAPCustomerResponse.SAPCustomerResponse.SPK));
                objParam.CloseDate = GetSPK_TungguUnitDate(spk.SPKNumber).ToString("yyyy-MM-dd");
                objParam.AccountID = GetAccount();
                objParam.Is_Valid_To_Send_SMS__c = (isSms == true ? "true" : "false");

                if (spk.SPKDetails.Count > 0)
                {
                    SPKDetail _spkDetail = (SPKDetail)(spk.SPKDetails[0]);
                    if (_spkDetail.VechileColor.VechileType != null)
                    {
                        objParam.Car_Code__c = _spkDetail.VechileColor.VechileType.VechileTypeCode;
                    }
                    else
                    {
                        objParam.Car_Type__c = String.Empty;
                    }
                    objParam.Quantity__c = (string)_spkDetail.Quantity.ToString();
                }
                else
                {
                    if (spk.SPKCustomer.SAPCustomer != null)
                    {
                        if (spk.SPKCustomer.SAPCustomer.VechileType != null)
                        {
                            objParam.Car_Code__c = spk.SPKCustomer.SAPCustomer.VechileType.VechileTypeCode;
                        }
                        else
                        {
                            objParam.Car_Type__c = String.Empty;
                        }
                        objParam.Quantity__c = (string)(spk.SPKCustomer.SAPCustomer.Qty.ToString());
                    }
                    else
                    {
                        objParam.Car_Code__c = String.Empty;
                        objParam.Car_Type__c = String.Empty;
                        objParam.Quantity__c = String.Empty;
                    }
                }
                objParam.Salesman_Code__c = (new SalesmanHeaderFacade(User).Retrieve(spk.SalesmanHeader.ID)).SalesmanCode;
                objParam.Salesman_Name__c = (new SalesmanHeaderFacade(User).Retrieve(spk.SalesmanHeader.ID)).Name;
                objParam.Rejected_Reason__c = spk.RejectedReason;
                objParam.Dealer_SPK_Date__c = spk.DealerSPKDate.ToString("yyyy-MM-dd");
                objParam.Created_Date_SPK__c = spk.CreatedTime.ToString("yyyy-MM-dd");
                objParam.Company_Type__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.TipePerusahaan, "EnumTipePerusahaan"); //ganti dengan enum Mitrais EnumTipePerusahaan
                objParam.Postal_Code__c = spk.SPKCustomer.PostalCode;
                objParam.City__c = Regex.Replace(Regex.Replace(spk.SPKCustomer.City.CityName, @"\t|\n|\r", ""), @"&|#", " Dan ");
                objParam.Office_No__c = spk.SPKCustomer.OfficeNo;
                objParam.Home_No__c = spk.SPKCustomer.HomeNo;
                objParam.Web_ID_Dealer__c = spk.SPKCustomer.SAPCustomer.WebID;
                objParam.Current_Vehicle_Brand__c = spk.SPKCustomer.SAPCustomer.CurrVehicleBrand;
                objParam.Current_Vehicle_Type__c = spk.SPKCustomer.SAPCustomer.CurrVehicleType;
                objParam.Note__c = spk.SPKCustomer.SAPCustomer.Note;
                objParam.Age__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.AgeSegment, "EnumAgeSegment.AgeSegment");
                objParam.Lead_Name__c = Regex.Replace(Regex.Replace(spk.SPKCustomer.SAPCustomer.CustomerName, @"\t|\n|\r", ""), @"&|#", " Dan ");
                objParam.Lead_Address__c = Regex.Replace(Regex.Replace(spk.SPKCustomer.SAPCustomer.CustomerAddress, @"\t|\n|\r", ""), @"&|#", " Dan ");
                objParam.Lead_Consumen_Type__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.CustomerType, "EnumTipePelangganCustomerRequest");
                objParam.Lead_Email__c = spk.SPKCustomer.SAPCustomer.Email;
                objParam.Lead_Salesman_Code__c = spk.SPKCustomer.SAPCustomer.SalesmanHeader.SalesmanCode;
                objParam.Lead_Salesman_Name__c = spk.SPKCustomer.SAPCustomer.SalesmanHeader.Name;
                objParam.Lead_Dealer_Code__c = spk.SPKCustomer.SAPCustomer.Dealer.DealerCode;
                objParam.Lead_Status__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.Status, "EnumDNET.enumSAPCustomer");
                objParam.Lead_Phone__c = spk.SPKCustomer.SAPCustomer.Phone;
                objParam.Lead_Gender__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.Sex, "EnumGender.Gender");

                objParam.Quantity__c = (string)(spk.SPKCustomer.SAPCustomer.Qty.ToString());

                CriteriaComposite critdtl = new CriteriaComposite(new Criteria(typeof(SPKDetail), "RowStatus", MatchType.Exact, (short)(DBRowStatus.Active)));
                critdtl.opAnd(new Criteria(typeof(SPKDetail), "SPKHeader.ID", MatchType.Exact, spk.ID));

                ArrayList arrdtl = new SPKDetailFacade(User).Retrieve(critdtl);
                VechileType objVT;
                var objParamdtl = new KTB.DNet.WebApi.Models.paramOpportunityVehicle();

                List<paramOpportunityVehicle> listVehicle = new List<paramOpportunityVehicle>();

                foreach (SPKDetail dtl in arrdtl)
                {
                    objParamdtl = new KTB.DNet.WebApi.Models.paramOpportunityVehicle();
                    objParamdtl.Additional__c = CommonFunction.GetEnumDescription(dtl.Additional, "EnumSPKAdditional.SPKAdditionalParts");
                    objParamdtl.Color__c = dtl.VechileColor.ColorIndName;
                    objVT = new VechileTypeFacade(User).Retrieve(dtl.VehicleTypeCode);
                    objParamdtl.Car_Code__c = dtl.VehicleTypeCode;
                    objParamdtl.Dnet_ID__c = dtl.ID.ToString();
                    objParamdtl.Quantity__c = dtl.Quantity.ToString();
                    objParamdtl.Rejected_Reason__c = dtl.RejectedReason;
                    objParamdtl.Remarks__c = Regex.Replace(Regex.Replace(dtl.Remarks, @"\t|\n|\r", ""), @"&", " Dan ");
                    objParamdtl.Status__c = CommonFunction.GetEnumDescription(dtl.Status, "EnumStatusSPK.Status");

                    listVehicle.Add(objParamdtl);
                }

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);
                Task.Run(() => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendDealer(User, String.Concat("services/apexrest/", paramWalkinOpportunity.SObjectTypeName), objParam, listVehicle)).Wait();

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
            msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;

            Console.WriteLine(msg);

            #region Log

            CreateLog(vReturn.ToString(), msg.ToString(), "K:SALESFORCEINSERTOPPORTUNITY\n", strJson);
            if (vReturn == true)
            {
                msg = msg + updateSAPCustomer(spk, msg);

                InsertSFReferenceFromSPKHeader(spk);
                InsertSFReferenceFromSPKDetail(spk);

            }

            #endregion

            return vReturn;
        }

        static bool UpdateOpportunity(SPKHeader spk, bool isSms, ref string msg)
        {
            bool vReturn = false;
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            string strJson = "";
            try
            {
                var objParam = new KTB.DNet.WebApi.Models.paramUpdateOpportunity();
                objParam.id = spk.SPKCustomer.SAPCustomer.SalesforceID;
                objParam.Dealer_Code__c = spk.Dealer.DealerCode;
                objParam.Dealer_Name__c = spk.Dealer.DealerName;
                objParam.Consumen_Type__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.TipeCustomer, "EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer");
                objParam.Name = Regex.Replace(Regex.Replace(spk.SPKCustomer.Name1, @"\t|\n|\r", ""), @"&|#", " Dan ");
                objParam.Address__c = Regex.Replace(Regex.Replace(spk.SPKCustomer.Alamat, @"\t|\n|\r", ""), @"&|#", " Dan ");
                if (EmailAddressCheck(spk.SPKCustomer.Email))
                { objParam.Email__c = spk.SPKCustomer.Email; }
                else
                { objParam.Email__c = String.Empty; }
                objParam.Gender__c = String.Empty;
                objParam.Mobile_Phone__c = String.Empty;
                objParam.ID_Type__c = String.Empty;
                objParam.ID_Number__c = String.Empty;
                objParam.MMKSI_WEB_ID__c = String.Empty;

                if (spk.SPKCustomer.SAPCustomer != null)
                {
                    objParam.Mobile_Phone__c = spk.SPKCustomer.HpNo;
                }

                if (spk.SPKCustomer.SPKCustomerProfiles.Count > 0)
                {
                    foreach (SPKCustomerProfile _profiles in spk.SPKCustomer.SPKCustomerProfiles)
                    {
                        if (_profiles.ProfileHeader.ID == 27) { objParam.Gender__c = (_profiles.ProfileValue == "LK" ? "Male" : "Female"); }

                        if (_profiles.ProfileHeader.ID == 29)
                        {
                            EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest tipeCustomer = (EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest)(spk.SPKCustomer.TipeCustomer);
                            if (tipeCustomer == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan)
                            {
                                objParam.ID_Type__c = "KTP";
                            }
                            else
                            {
                                objParam.ID_Type__c = "TDP";
                            }
                            objParam.ID_Number__c = Regex.Replace(Regex.Replace(_profiles.ProfileValue, @"\t|\n|\r", ""), @"&|#", " Dan ");
                        }
                        if (_profiles.ProfileHeader.ID == 30)
                        {
                            if (objParam.Mobile_Phone__c == "")
                            {
                                objParam.Mobile_Phone__c = _profiles.ProfileValue;
                            }
                            objParam.Phone_No__c = _profiles.ProfileValue;
                        }
                    }
                }
                else
                {

                }
                if (spk.SPKCustomer.SAPCustomer != null)
                {
                    objParam.Information_Type__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.InformationType, "EnumInformationType.InformationType");
                    //EnumInformationType.GetStringInformationType(spk.SPKCustomer.SAPCustomer.InformationType);
                    objParam.Customer_Purposes__c = EnumCustomerPurpose.GetStringCustomerPurpose(spk.SPKCustomer.SAPCustomer.CustomerPurpose);
                    objParam.LeadSource = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.InformationSource, "EnumInformationSource.InformationSource.Salesforce");
                    //EnumInformationSource.GetStringInformationSource(spk.SPKCustomer.SAPCustomer.InformationSource);
                    if (spk.SPKCustomer.SAPCustomer.ProspectDate == null)
                    { objParam.Prospect_Date__c = spk.SPKCustomer.SAPCustomer.CreatedTime.ToString("yyyy-MM-dd"); }
                    else
                    { objParam.Prospect_Date__c = spk.SPKCustomer.SAPCustomer.ProspectDate.ToString("yyyy-MM-dd"); }
                    if (spk.SPKCustomer.SAPCustomer.SalesforceID != "")
                    { objParam.MMKSI_WEB_ID__c = spk.SPKCustomer.SAPCustomer.WebID; }
                    else { objParam.MMKSI_WEB_ID__c = string.Empty; }
                }
                else
                {
                    objParam.Information_Type__c = String.Empty;
                    objParam.Customer_Purposes__c = String.Empty;
                    objParam.LeadSource = String.Empty;
                    objParam.Prospect_Date__c = spk.SPKCustomer.CreatedTime.ToString("yyyy-MM-dd");
                    objParam.MMKSI_WEB_ID__c = string.Empty;
                }

                objParam.SPK_No__c = spk.SPKNumber;
                objParam.Dealer_SPK_No__c = spk.DealerSPKNumber;
                objParam.SPK_Status__c = CommonFunction.GetEnumDescription(short.Parse(spk.Status), "EnumStatusSPK.Status");
                objParam.Validation_Key__c = spk.ValidationKey;

                objParam.StageName = EnumSAPCustomerResponse.GetStringValue((int)(EnumSAPCustomerResponse.SAPCustomerResponse.SPK));
                objParam.CloseDate = GetSPK_TungguUnitDate(spk.SPKNumber).ToString("yyyy-MM-dd");
                objParam.AccountID = GetAccount();
                objParam.Is_Valid_To_Send_SMS__c = (isSms == true ? "true" : "false");

                if (spk.SPKDetails.Count > 0)
                {
                    SPKDetail _spkDetail = (SPKDetail)(spk.SPKDetails[0]);
                    if (_spkDetail.VechileColor.VechileType != null)
                    {
                        objParam.Car_Code__c = _spkDetail.VechileColor.VechileType.VechileTypeCode;
                    }
                    else
                    {
                        VechileType objVT = new VechileTypeFacade(User).Retrieve(_spkDetail.VehicleTypeCode);
                        if (objVT != null)
                        { objParam.Car_Type__c = objVT.Description; }
                        else { objParam.Car_Type__c = String.Empty; }
                    }
                    objParam.Quantity__c = (string)_spkDetail.Quantity.ToString();
                }
                else
                {
                    if (spk.SPKCustomer.SAPCustomer != null)
                    {
                        if (spk.SPKCustomer.SAPCustomer.VechileType != null)
                        {
                            objParam.Car_Code__c = spk.SPKCustomer.SAPCustomer.VechileType.VechileTypeCode;
                            objParam.Car_Type__c = spk.SPKCustomer.SAPCustomer.VechileType.Description;
                        }
                        else
                        {
                            objParam.Car_Code__c = spk.SPKCustomer.SAPCustomer.VehicleTypeCode;
                            VechileType objVT = new VechileTypeFacade(User).Retrieve(spk.SPKCustomer.SAPCustomer.VehicleTypeCode);
                            if (objVT != null)
                            { objParam.Car_Type__c = objVT.Description; }
                            else { objParam.Car_Type__c = String.Empty; }
                        }
                        objParam.Quantity__c = (string)(spk.SPKCustomer.SAPCustomer.Qty.ToString());
                    }
                    else
                    {
                        objParam.Car_Code__c = String.Empty;
                        objParam.Car_Type__c = String.Empty;
                        objParam.Quantity__c = String.Empty;
                    }
                }
                objParam.Salesman_Code__c = (new SalesmanHeaderFacade(User).Retrieve(spk.SalesmanHeader.ID)).SalesmanCode;
                objParam.Salesman_Name__c = (new SalesmanHeaderFacade(User).Retrieve(spk.SalesmanHeader.ID)).Name;
                objParam.Rejected_Reason__c = spk.RejectedReason;
                objParam.Dealer_SPK_Date__c = spk.DealerSPKDate.ToString("yyyy-MM-dd");
                objParam.Created_Date_SPK__c = spk.CreatedTime.ToString("yyyy-MM-dd");
                objParam.Company_Type__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.TipePerusahaan, "EnumTipePerusahaan");
                objParam.Postal_Code__c = spk.SPKCustomer.PostalCode;
                objParam.City__c = Regex.Replace(Regex.Replace(spk.SPKCustomer.City.CityName, @"\t|\n|\r", ""), @"&|#", " Dan ");
                objParam.Home_No__c = spk.SPKCustomer.HomeNo;
                objParam.Web_ID_Dealer__c = spk.SPKCustomer.SAPCustomer.WebID;
                objParam.Current_Vehicle_Brand__c = spk.SPKCustomer.SAPCustomer.CurrVehicleBrand;
                objParam.Current_Vehicle_Type__c = spk.SPKCustomer.SAPCustomer.CurrVehicleType;
                objParam.Note__c = spk.SPKCustomer.SAPCustomer.Note;
                objParam.Age__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.AgeSegment, "EnumAgeSegment.AgeSegment");
                objParam.Lead_Name__c = Regex.Replace(Regex.Replace(spk.SPKCustomer.SAPCustomer.CustomerName, @"\t|\n|\r", ""), @"&|#", " Dan ");
                objParam.Lead_Address__c = Regex.Replace(Regex.Replace(spk.SPKCustomer.SAPCustomer.CustomerAddress, @"\t|\n|\r", ""), @"&|#", " Dan ");
                objParam.Lead_Consumen_Type__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.CustomerType, "EnumTipePelangganCustomerRequest");
                objParam.Lead_Email__c = spk.SPKCustomer.SAPCustomer.Email;
                objParam.Lead_Salesman_Code__c = spk.SPKCustomer.SAPCustomer.SalesmanHeader.SalesmanCode;
                objParam.Lead_Salesman_Name__c = spk.SPKCustomer.SAPCustomer.SalesmanHeader.Name;
                objParam.Lead_Dealer_Code__c = spk.SPKCustomer.SAPCustomer.Dealer.DealerCode;
                objParam.Lead_Status__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.Status, "EnumDNET.enumSAPCustomer");
                objParam.Lead_Phone__c = spk.SPKCustomer.SAPCustomer.Phone;
                objParam.Lead_Gender__c = CommonFunction.GetEnumDescription(spk.SPKCustomer.SAPCustomer.Sex, "EnumGender.Gender");

                if (spk.SPKCustomer.SAPCustomer.SAPCustomerMapping != null)
                {
                    objParam.Name = null;
                    objParam.Address__c = null;
                    objParam.Email__c = null;
                    objParam.Mobile_Phone__c = null;
                    objParam.Information_Type__c = null;
                    objParam.Customer_Purposes__c = null;
                    objParam.LeadSource = null;
                    objParam.Phone_No__c = null;
                    objParam.Web_ID_Dealer__c = null;
                    objParam.Lead_Name__c = null;
                    objParam.Lead_Address__c = null;
                    objParam.Lead_Email__c = null;
                    objParam.Lead_Consumen_Type__c = null;
                }

                Task.Run(() => KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", paramUpdateOpportunity.SObjectTypeName), objParam)).Wait();
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);

                vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
                msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;

                #region Log
                InsertSFReferenceFromSPKHeader(spk);
                CreateLog(vReturn.ToString(), msg.ToString(), "K:SALESFORCEUPDATEOPPORTUNITY\n", strJson);
                #endregion

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return vReturn;
        }

        static bool UpdateOpportunityDetail(SPKHeader spk)
        {
            var strJson = "";
            bool vReturn = false;
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            var objParamdtl = new KTB.DNet.WebApi.Models.paramUpdateOpportunityVehicle();
            string msg = "";

            try
            {
                ArrayList arr = new ArrayList();
                SFReferenceFacade fca = new SFReferenceFacade(User);
                SFReference objSFRef;
                string strSFRef = "(";
                if (spk != null)
                {
                    arr = spk.SPKDetails;
                    foreach (SPKDetail spkdtl in arr)
                    {
                        strSFRef = strSFRef + spkdtl.ID.ToString() + ",";
                    }
                    strSFRef = strSFRef + "0)";
                    arr = fca.SPFunc("SPKDetail", strSFRef);

                }
                else
                {
                    arr = fca.SPFunc("SPKDetail");
                }

                Console.WriteLine("_____________________________________________________________________________________________________________ \n");
                Console.WriteLine("Total SPK Detail yang Akan DI Update / Insert :" + arr.Count.ToString() + "\n");

                int iCount = 0;

                foreach (SFReference rowdtl in arr)
                {
                    iCount++;
                    Console.WriteLine("Processing " + iCount.ToString() + " of " + arr.Count.ToString() + ". SPK Detail ID : " + rowdtl.RefID.ToString() + " at " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");

                    CriteriaComposite critdtl = new CriteriaComposite(new Criteria(typeof(SPKDetail), "RowStatus", MatchType.Exact, (short)(DBRowStatus.Active)));
                    critdtl.opAnd(new Criteria(typeof(SPKDetail), "ID", MatchType.Exact, rowdtl.RefID));

                    ArrayList arrdtl = new SPKDetailFacade(User).Retrieve(critdtl);
                    VechileType objVT;

                    List<paramUpdateOpportunityVehicle> listVehicle = new List<paramUpdateOpportunityVehicle>();

                    foreach (SPKDetail dtl in arrdtl)
                    {
                        if (dtl.SPKHeader.SPKCustomer.SAPCustomer == null || dtl.SPKHeader.SPKCustomer.SAPCustomer.SalesforceID == String.Empty || dtl.SPKHeader.SPKCustomer.SAPCustomer.SalesforceID == null)
                        { objParamdtl.Opportunity__c = String.Empty; }
                        else
                        { objParamdtl.Opportunity__c = dtl.SPKHeader.SPKCustomer.SAPCustomer.SalesforceID; }
                        objParamdtl.Additional__c = CommonFunction.GetEnumDescription(dtl.Additional, "EnumSPKAdditional.SPKAdditionalParts");
                        objParamdtl.Color__c = dtl.VechileColor.ColorIndName;

                        objVT = new VechileTypeFacade(User).Retrieve(dtl.VehicleTypeCode);

                        objParamdtl.Car_Code__c = dtl.VehicleTypeCode;
                        objParamdtl.Dnet_ID__c = dtl.ID.ToString();
                        objParamdtl.Quantity__c = dtl.Quantity.ToString();
                        objParamdtl.Rejected_Reason__c = dtl.RejectedReason;
                        objParamdtl.Remarks__c = Regex.Replace(Regex.Replace(dtl.Remarks, @"\t|\n|\r", ""), @"&", " Dan ");
                        objParamdtl.Status__c = CommonFunction.GetEnumDescription(dtl.Status, "EnumStatusSPK.Status");

                        listVehicle.Add(objParamdtl);

                        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParamdtl);
                        Task.Run(() => KTB.DNet.WebApi.Models.SalesForce.SalesForce.SendDealerPost(User, String.Concat("services/apexrest/", paramUpdateOpportunityVehicle.SObjectTypeName), objParamdtl)).Wait();


                        vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
                        msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;

                        Console.WriteLine(msg);
                        CreateLog(vReturn.ToString(), msg.ToString(), "K:SALESFORCEUPDATEOPPORTUNITYDETAIL\n", strJson);

                        if (vReturn)
                        {
                            rowdtl.IsSend = true;
                            fca.Update(rowdtl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return vReturn;
        }


        #endregion LeadCustomerProcess

        #region CaseManagementProcess

        static void CaseManagementProcess()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            String sql = "";

            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(CustomerCaseResponse), "ID", MatchType.InSet, "(" + sql + ")"));
            KTB.DNET.BusinessFacade.CustomerCaseResponseFacade facade = new KTB.DNET.BusinessFacade.CustomerCaseResponseFacade(User);
            ArrayList arr = facade.Retrieve(criterias);

            ArrayList arrTypeCode = GetNewType();

            Console.WriteLine("Total SPK :" + arr.Count.ToString() + "\n");

            foreach (CustomerCaseResponse resp in arr)
            {
                Console.WriteLine("Processing SPK :" + resp.CustomerCase.CaseDate + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\n");

                if (resp.CustomerCase != null)
                {

                    bool vResult = true;
                    string msg = "";
                    vResult = UpdateCase(resp, ref msg);

                    if (vResult)
                    {
                        resp.IsSend = 1;
                        int iResult = facade.Update(resp);
                    }
                }
            }
            Console.WriteLine("Process tansfer selesai !" + "\n");
            Console.ReadLine();
        }

        static bool UpdateCase(CustomerCaseResponse objResponse, ref string msg)
        {
            bool vReturn = false;
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            string strJson = "";
            try
            {
                var objParam = new KTB.DNet.WebApi.Models.paramUpdateCase();
                objParam.id = objResponse.CustomerCase.SalesforceID;
                //objParam.status = EnumCustomerCaseResponse.GetStringCustomerResponse(objResponse.Status);
                objParam.Status_By_Dealer__c = EnumCustomerCaseResponse.GetStringCustomerResponse(objResponse.Status);
                objParam.comment = objResponse.Description;

                Task.Run(
                () => KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", paramUpdateCase.SObjectTypeName), objParam)
            ).Wait();

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam);
            }
            catch (Exception ex)
            {
                vReturn = false;
            }
            vReturn = KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess;
            msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message;


            #region Log
            CreateLog(vReturn.ToString(), msg.ToString(), "K:SALESFORCEUPDATECASE\n", strJson);
            #endregion

            return vReturn;
        }

        #endregion

        #region Functions


        // FARID'S ADDED
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static int isSalesForceRecieved(SPKHeader spk)
        {

            int intReturn = 0;

            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(WsLog), "Body", MatchType.Partial, spk.SPKNumber));

            WsLogFacade facade = new WsLogFacade(User);
            ArrayList arr = facade.Retrieve(criterias);

            intReturn = arr.Count;

            return intReturn;
        }

        static string updateSAPCustomer(SPKHeader spk, String msg)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            SPKHeaderFacade facade = new SPKHeaderFacade(User);

            String Pesan = facade.UpdateSAPCustomer(spk, msg);
            return Pesan;
        }

        static void UpdateSpkHeader(SPKHeader SPKHead)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            SPKHead.IsSend = 1;
            SPKHeaderFacade spkheaderfacade = new SPKHeaderFacade(User);
            spkheaderfacade.Update(SPKHead.ID);
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //

        static void CreateLog(string vReturn, string msg, string logCategory, string strJson)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            WsLog wslog = new WsLog();
            wslog.Source = "Internal";
            wslog.Status = vReturn.ToString();
            wslog.Message = msg;
            wslog.Body = String.Concat(logCategory, strJson);
            wslog.RowStatus = 0;
            wslog.CreatedBy = "WebService";

            WsLogFacade wslogfacade = new WsLogFacade(User);
            wslogfacade.Insert(wslog);

        }

        static ArrayList GetNewType()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            ArrayList arr = new ArrayList();
            try
            {
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);
                string ConsumerKey = appConfigFacade.Retrieve("SPKModelCodeFilter").Value;
                if (ConsumerKey != null)
                {
                    string[] arrCode = ConsumerKey.Split(';');
                    string strCode = "";
                    foreach (string value in arrCode)
                    {
                        if (strCode == "")
                        {
                            strCode = "'" + value + "'";
                        }
                        else
                        {
                            strCode = strCode + ",'" + value + "'";
                        }
                        strCode = "(" + strCode + ")";
                    }

                    CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias.opAnd(new Criteria(typeof(VechileType), "VechileModel.VechileModelCode", MatchType.InSet, strCode));
                    VechileTypeFacade facade = new VechileTypeFacade(User);
                    arr = facade.Retrieve(criterias);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return arr;
        }

        static bool EmailAddressCheck(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        static string GetAccount()
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string accountID = appConfigFacade.Retrieve("Account_SF_ID").Value;

            return accountID;
        }

        static bool IsTransferToSalesforce()
        {
            bool vReturn = false;
            try
            {
                var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);
                string value = appConfigFacade.Retrieve("TransferToSF").Value;
                if (value == "1")
                {
                    vReturn = true;
                }
            }
            catch
            {
                vReturn = false;
            }

            return vReturn;
        }

        static DateTime GetSPK_TungguUnitDate(string spkNumber)
        {
            //DateTime dtReturn = System.DateTime.Now.ToString("yyyy-MM-dd");
            DateTime dtReturn = System.DateTime.Now;
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            CriteriaComposite critStatus = new CriteriaComposite(new Criteria(typeof(StatusChangeHistory), "RowStatus", MatchType.Exact, (short)(DBRowStatus.Active)));
            critStatus.opAnd(new Criteria(typeof(StatusChangeHistory), "DocumentType", MatchType.Exact, 6));
            critStatus.opAnd(new Criteria(typeof(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, spkNumber));
            critStatus.opAnd(new Criteria(typeof(StatusChangeHistory), "NewStatus", MatchType.Exact, (int)(EnumStatusSPK.Status.Tunggu_Unit)));

            SortCollection sortColl = new SortCollection();
            sortColl.Add(new Sort(typeof(StatusChangeHistory), "CreatedTime", Sort.SortDirection.DESC));

            ArrayList arrStatus = new StatusChangeHistoryFacade(User).Retrieve(critStatus, sortColl);

            StatusChangeHistory sc = new StatusChangeHistory();
            if (arrStatus.Count > 0)
            {
                sc = (StatusChangeHistory)(arrStatus[0]);
                dtReturn = sc.CreatedTime;
            }

            return dtReturn;
        }

        static void InsertSFReferenceFromSPKDetail(SPKHeader spk)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            SFReference objSFRef;
            foreach (SPKDetail rowdtl in spk.SPKDetails)
            {
                objSFRef = new SFReference();
                objSFRef.RefID = rowdtl.ID;
                objSFRef.RefTable = "SPKDetail";
                objSFRef.IsSend = true;
                objSFRef.SalesForceID = "";
                objSFRef.LastUpdateTime = rowdtl.LastUpdateTime;
                objSFRef.CreatedTime = rowdtl.CreatedTime;

                new SFReferenceFacade(User).Insert(objSFRef);
            }
        }


        static void InsertSFReferenceFromSPKHeader(SPKHeader spk)
        {

            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            StatusChangeHistory objsch = new StatusChangeHistory();
            StatusChangeHistoryFacade objschFac = new StatusChangeHistoryFacade(User);
            CriteriaComposite cri = new CriteriaComposite(new Criteria(typeof(StatusChangeHistory), "RowStatus", MatchType.Exact, (short)(DBRowStatus.Active)));
            cri.opAnd(new Criteria(typeof(StatusChangeHistory), "DocumentType", MatchType.Exact, 6));
            cri.opAnd(new Criteria(typeof(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, spk.SPKNumber));

            SortCollection sortColl = new SortCollection();
            sortColl.Add(new Sort(typeof(StatusChangeHistory), "CreatedTime", Sort.SortDirection.DESC));

            ArrayList arrStatus = objschFac.Retrieve(cri, sortColl);
            if (arrStatus.Count > 0)
            {
                objsch = (StatusChangeHistory)(arrStatus[0]);
            }

            SFReference objSFRef;

            objSFRef = new SFReference();
            objSFRef.RefID = objsch.id;
            objSFRef.RefTable = "SPKHeader.StatusChangeHistory";
            objSFRef.IsSend = true;
            objSFRef.SalesForceID = "";
            objSFRef.LastUpdateTime = spk.LastUpdateTime;
            objSFRef.CreatedTime = spk.CreatedTime;

            new SFReferenceFacade(User).Insert(objSFRef);


        }

        #endregion

    }
}
