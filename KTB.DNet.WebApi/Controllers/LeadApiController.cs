using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.BusinessFacade.SAP;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.BusinessFacade.FinishUnit;
using KTB.DNet.BusinessFacade.Salesman;
using System.Web;
using System.ServiceModel.Channels;
using System.Collections;
using KTB.DNET.BusinessFacade.Salesman;
using System.Security.Principal;
using System.Text;
using KTB.DNET.BusinessFacade;
using KTB.DNet.WebApi.Models;
using KTB.DNet.WebApi.Helpers;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web.Configuration;

namespace KTB.DNet.WebApi.Controllers
{
    public class LeadApiController : BaseApiController
    {
        private CriteriaComposite criterias;
        private string JsonContent;

        /**
        *   @api {post} /api/leadapi/create Create Lead
        *   @apiName Create a new Lead
        *   @apiGroup Lead
	    *
	    *   @apiHeader {String} X-Authorization-Token Token value
        *   @apiHeaderExample {String} Header Example:
        *       {
        *           "X-Authorization-Token": "THIS.IS.TOKEN.TAKEN.FROM.RETURN.RESULT.OF /api/authapi/token"
        *       }
        *   
        *   @apiParam {String} SalesforceID ID of Sales Force
        *   @apiParam {String} WebID ID of Web
        *   @apiParam {String} DealerCode dealer of Lead
        *   @apiParam {String} [VehicleTypeCode] vehicle type of Lead
        *   @apiParam {String} [SalesmanHeaderCode] salesman header of Lead
        *   @apiParam {String} CustomerCode customer code of Lead
        *   @apiParam {String} CustomerName customer name of Lead
        *   @apiParam {Number=0,1,2,3} [CustomerType] customer type of Lead
        *   @apiParam {String="Perorangan", "Perusahaan", "BUMN/Pemerintah", "Lainnya"} CustomerTypeValue customer type Value of Lead. If <code>CustomerType</code> parameter is define, then this value will be ignored
        *   @apiParam {String} CustomerAddress customer address of Lead
        *   @apiParam {String} Phone phone of Lead
        *   @apiParam {String} Email email of Lead
        *   @apiParam {Number=1,2} [Sex] sex of Lead
        *   @apiParam {String="male", "female"} SexValue sex value of Lead. If <code>Sex</code> parameter is define, then this value will be ignored
        *   @apiParam {Number=0,1,2,3,4} [AgeSegment] age segment of Lead
        *   @apiParam {Date} AgeSegmentDate birth date of Lead. Format <code>YYYY-MM-DD</code>. If <code>AgeSegment</code> parameter is define, then this value will be ignored
        *   @apiParam {Number=0,1,2,3,4,5,6,7,8} [CustomerPurpose] customer purpose of Lead
        *   @apiParam {String="Tanya kendaraan", "Test Drive", "Memesan kendaraan", "Tanya promosi", "Tanya fasilitas dealer", "Komplain", "Mengantar saudara/ teman", "Lain lain"} CustomerPurposeValue customer purpose value of Lead. If <code>CustomerPurpose</code> parameter is define, then this value will be ignored
        *   @apiParam {Number=0,1,2,3} [InformationType] information type of Lead
        *   @apiParam {String="Incoming Call", "Walk In", "SalesForce"} [InformationTypeValue="SalesForce"] information type value of Lead.
        *   @apiParam {Number=1,2,3,4,5,6,7,8,9,10,11,12,13,14} [InformationSource] information source of Lead
        *   @apiParam {String="Surat Kabar", "Televisi", "Majalah", "Radio", "Rekomendasi", "Kunjungan Sales", "Pameran / Event", "Papan Reklame", "Internet", "Kebetulan melintas", "Microsite", "Mobile Apps", "Call center", "Social media"} InformationSourceValue information source value of Lead. If <code>InformationSource</code> parameter is define, then this value will be ignored
        *   @apiParam {Number=0,1} [Status=1] status of Lead
        *   @apiParam {Number} Qty quantity of Lead
        *   @apiParam {Date} ProspectDate prospect date of Lead. Format <code>YYYY-MM-DD</code>
        *   @apiParam {Boolean} [isSPK=False] isspk of Lead
        *   @apiParam {String} [CurrVehicleBrand] currvehiclebrand of Lead
        *   @apiParam {String} [CurrVehicleType] currvehicletype of Lead
        *   @apiParam {String} [Note] note of Lead
        *   @apiParam {Number=0,1} [RowStatus=0] row status of Lead
        *   @apiParam {String} [CreatedBy] created by of Lead
        *   @apiParam {Date} [CreatedTime] created time of Lead
        *   @apiParam {String} [LastUpdateBy] last update by of Lead
        *   @apiParam {Date} [LastUpdateTime] last update time of Lead
        *
        *   @apiParamExample {json} Body Example:
        *       {
        *           "SalesforceID": "String",
        *           "WebID": "String",
        *           "DealerCode": "String",
        *           "CustomerCode": "String",
        *           "CustomerName": "String",
        *           "CustomerTypeValue": "Perusahaan",
        *           "CustomerAddress": "String",
        *           "Phone": "String",
        *           "Email": "String",
        *           "SexValue": "male",
        *           "AgeSegmentDate": "Date",
        *           "CustomerPurposeValue": "Tanya kendaraan",
        *           "InformationSourceValue": "Surat Kabar",
        *           "Qty": Number,
        *           "ProspectDate": "Date",
        *           "CurrVehicleBrand": "String",
        *           "CurrVehicleType": "String",
        *           "Note": "String"
        *       }
        *
        *   @apiSuccess {Boolean} success Status of the api. <code>True</code> if success; <code>False</code> if fail;
        *   @apiSuccess {Number} total always return <code>0</code>.
        *   @apiSuccess {String} _id always return <code>-1</code>.
        *   @apiSuccess {String} message The reason if status is fail (<code>success is false</code>).
        *   @apiSuccess {String} lst always return empty <code>list</code>.
        *
        *   @apiSuccessExample {json} Success Response:
        *       {
        *           "success": true,
        *           "total": 0,
        *           "_id": -1,
        *           "message": "",
        *           "lst":[{}]
        *       }
        */
        [HttpPost]
        public IDictionary<string, object> Create()
        {
            bool success = true; string message = string.Empty; bool isvalid = false;
            if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);

            try
            {
                GenericPrincipal UserSalesforce = new System.Security.Principal.GenericPrincipal(new GenericIdentity("Salesforce"), null);
                var jsonBody = Request.Content.ReadAsStringAsync().Result;
                this.JsonContent = jsonBody;
                var sapcustomer = JsonConvert.DeserializeObject<SAPCustomer>(jsonBody);
                string DestinationNumber = string.Empty;

                #region Mapping
                System.Collections.ArrayList dataResult;

                int wsLogID = 0;
                //Save WsLog
                WsLog wLog = new WsLog();
                wLog.Body = "K:SALESFORCELEAD\n" + jsonBody;
                wLog.CreatedBy = "Salesforce";
                wLog.ErrorMessage = string.Empty;
                wLog.Status = success.ToString();
                wLog.RowStatus = 0;
                wLog.Source = GetClientIpAddress();
                wLog.ID = new WsLogFacade(UserSalesforce).Insert(wLog);

                #region Dealer
                if ((sapcustomer.DealerCode != null) && (sapcustomer.DealerCode != String.Empty))
                {
                    try
                    {
                        DealerFacade dealerFacade = new DealerFacade(User);
                        sapcustomer.Dealer = dealerFacade.GetDealer(sapcustomer.DealerCode);
                    }
                    catch (Exception e) { string err = e.Message.ToString(); };

                }
                #endregion

                #region VehicleType
                if ((sapcustomer.VehicleTypeCode != null) && (sapcustomer.VehicleTypeCode != String.Empty))
                {
                    criterias = new CriteriaComposite(new Criteria(typeof(VechileType), "VechileTypeCode", sapcustomer.VehicleTypeCode));
                    VechileTypeFacade vechileTypeFacade = new VechileTypeFacade(User); dataResult = vechileTypeFacade.Retrieve(criterias);
                    sapcustomer.VechileType = (VechileType)(dataResult.Count.Equals(0) ? null : dataResult[0]);
                }
                #endregion

                #region VehicleModel

                AppConfigFacade cfgConfig = new AppConfigFacade(User);
                if (string.IsNullOrEmpty(sapcustomer.VehicleModel))
                {
                    sapcustomer.VehicleModel = cfgConfig.Retrieve("LeadDefaultModel").Value;
                }
                if (string.IsNullOrEmpty(sapcustomer.Variants))
                {
                    sapcustomer.Variants = cfgConfig.Retrieve("LeadDefaultVarian").Value;
                }

                #endregion

                List<StandardCode> listInformationType = new StandardCodeFacade(UserSalesforce).RetrieveByCategory("EnumInformationType.InformationType")
                  .Cast<StandardCode>().ToList();
                List<StandardCode> listInformationTypeSF = new StandardCodeFacade(UserSalesforce).RetrieveByCategory("EnumInformationType.InformationType.Salesforce")
                  .Cast<StandardCode>().ToList();
                List<StandardCode> listInformationSource = new StandardCodeFacade(UserSalesforce).RetrieveByCategory("EnumInformationSource.InformationSource.Salesforce")
                    .Cast<StandardCode>().ToList();
                List<StandardCode> listCustomerPurpose = new StandardCodeFacade(UserSalesforce).RetrieveByCategory("EnumCustomerPurpose.CustomerPurpose")
                    .Cast<StandardCode>().ToList();

                SAPCustomerMapping scMap = new SAPCustomerMapping();


                #region InformationSource
                bool isLeadSource = true;
                //sapcustomer.InformationSource = 11;
                if ((sapcustomer.InformationSource.Equals(0)) && (sapcustomer.InformationSourceValue != null) && (sapcustomer.InformationSourceValue != String.Empty))
                {
                    //EnumInformationSourceOp enumInformationSourceOp = EnumInformationSourceOp.RetriveInformationSource(true).Cast<EnumInformationSourceOp>().Where(o => o.NameStatus.ToLower().Equals(sapcustomer.InformationSourceValue.ToLower())).FirstOrDefault();
                    //sapcustomer.InformationSource = (short)((enumInformationSourceOp == null) ? 0 : enumInformationSourceOp.ValStatus);
                    scMap.SourceLead = sapcustomer.InformationSourceValue;
                    if (listInformationSource.Where(x => x.ValueDesc.ToLower() == sapcustomer.InformationSourceValue.ToLower().Trim()).Count() > 0)
                    {
                        sapcustomer.InformationSource = short.Parse(listInformationSource.FirstOrDefault(x => x.ValueDesc.ToLower()
                           == sapcustomer.InformationSourceValue.ToLower().Trim()).ValueId.ToString());

                        AppConfigFacade funcConfig2 = new AppConfigFacade(UserSalesforce);
                        AppConfig cfg = funcConfig2.Retrieve("Lead.SalesforceNotif." + scMap.SourceLead);
                        if (cfg != null)
                        {
                            if (cfg.Value.ToString() == "0")
                            {
                                isLeadSource = false;
                            }
                        }

                    }
                    else
                    {
                        //sapcustomer.InformationSource = 0; 
                        sapcustomer.InformationSource = short.Parse(cfgConfig.Retrieve("LeadDefaultLeadSource").Value);
                    }//mapping di set 11 else
                }
                else
                {
                    sapcustomer.InformationSource = short.Parse(cfgConfig.Retrieve("LeadDefaultLeadSource").Value);
                    scMap.SourceLead = string.Empty;
                }
                #endregion


                #region SalesmanHeader
                SalesmanDSEAssignment objSls = new SalesmanDSEAssignment();
                bool isAuto = false;
                if ((sapcustomer.SalesmanHeaderCode != null) && (sapcustomer.SalesmanHeaderCode != String.Empty))
                {
                    criterias = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "SalesmanCode", sapcustomer.SalesmanHeaderCode));
                    SalesmanHeaderFacade salesmanHeaderFacade = new SalesmanHeaderFacade(User); dataResult = salesmanHeaderFacade.Retrieve(criterias);
                    sapcustomer.SalesmanHeader = (SalesmanHeader)(dataResult.Count.Equals(0) ? null : dataResult[0]);
                    if (dataResult.Count > 0)
                    {
                        DestinationNumber = ((SalesmanHeader)dataResult[0]).PhoneNumber;
                    }

                    //}
                    //                DestinationNumber = (SalesmanHeader)(dataResult.Count.Equals(0) ? null : dataResult[0].);
                }
                else if (sapcustomer.Dealer != null && isLeadSource)
                {
                    int salesmanHeaderID = 0;
                    if (!string.IsNullOrEmpty(sapcustomer.Phone))
                    {
                        //cek phone
                        CriteriaComposite ncriterias;
                        SAPCustomerMappingFacade nSapcustomerFacade = new SAPCustomerMappingFacade(UserSalesforce);
                        salesmanHeaderID = nSapcustomerFacade.GetCurrentSalesmanHeader(sapcustomer.Phone, sapcustomer.Dealer.ID);
                        if (salesmanHeaderID > 0)
                        {
                            ncriterias = new CriteriaComposite(new Criteria(typeof(SalesmanDSE), "Dealer.ID", sapcustomer.Dealer.ID));
                            ncriterias.opAnd(new Criteria(typeof(SalesmanDSE), "RowStatus", 0));
                            ncriterias.opAnd(new Criteria(typeof(SalesmanDSE), "Status", 1));
                            ncriterias.opAnd(new Criteria(typeof(SalesmanDSE), "SalesmanHeader.ID", salesmanHeaderID));
                            ncriterias.opAnd(new Criteria(typeof(SalesmanDSE), "SalesmanHeader.Status", 2));//Status Salesman Aktif
                            ncriterias.opAnd(new Criteria(typeof(SalesmanDSE), "SalesmanHeader.RowStatus", 0));//Status Salesman rowstatus Aktif
                            ArrayList xslsDSE = new SalesmanDSEFacade(User).Retrieve(ncriterias);
                            SalesmanDSE nSDE = (SalesmanDSE)xslsDSE[0];

                            sapcustomer.SalesmanHeader = nSDE.SalesmanHeader;
                            DestinationNumber = nSDE.PhoneNumber;
                        }
                    }
                    if (salesmanHeaderID == 0)
                    {
                        //Auto Assigment
                        isAuto = true;
                        criterias = new CriteriaComposite(new Criteria(typeof(SalesmanDSEAssignment), "Dealer.ID", sapcustomer.Dealer.ID));
                        ArrayList arrSlsAssignment = new SalesmanDSEAssignmentFacade(User).Retrieve(criterias);
                        if (arrSlsAssignment.Count == 0)
                        {
                            objSls.Dealer = sapcustomer.Dealer;
                            objSls.Priority = 0;
                            objSls.CreatedBy = "WebService";
                            objSls.ID = new SalesmanDSEAssignmentFacade(User).Insert(objSls);
                        }
                        else
                        {
                            objSls = (SalesmanDSEAssignment)arrSlsAssignment[0];
                        }

                        criterias = new CriteriaComposite(new Criteria(typeof(SalesmanDSE), "Dealer.ID", sapcustomer.Dealer.ID));
                        criterias.opAnd(new Criteria(typeof(SalesmanDSE), "RowStatus", 0));
                        criterias.opAnd(new Criteria(typeof(SalesmanDSE), "Status", 1));
                        criterias.opAnd(new Criteria(typeof(SalesmanDSE), "SalesmanHeader.Status", 2));//Status Salesman Aktif
                        criterias.opAnd(new Criteria(typeof(SalesmanDSE), "SalesmanHeader.RowStatus", 0));//Status Salesman rowstatus Aktif
                        ArrayList slsDSE = new SalesmanDSEFacade(User).Retrieve(criterias);
                        if (slsDSE.Count == 0)
                        {
                            sapcustomer.SalesmanHeader = null;
                        }
                        else
                        {
                            List<SalesmanDSE> listDSE = slsDSE.Cast<SalesmanDSE>().Where(x => x.SalesmanHeader.Dealer.ID == sapcustomer.Dealer.ID).ToList();
                            if (listDSE.Where(x => x.Priority > (objSls.Priority)).Count() > 0)
                            {
                                SalesmanDSE DSEsetNow = listDSE.OrderBy(y => y.Priority).
                                        FirstOrDefault(x => x.Priority > (objSls.Priority));

                                sapcustomer.SalesmanHeader = DSEsetNow.SalesmanHeader;
                                objSls.Priority = DSEsetNow.Priority;
                                DestinationNumber = DSEsetNow.PhoneNumber;
                            }
                            else
                            {
                                SalesmanDSE DSEFirstPriority = listDSE.OrderBy(y => y.Priority).FirstOrDefault();
                                sapcustomer.SalesmanHeader = DSEFirstPriority.SalesmanHeader;
                                objSls.Priority = DSEFirstPriority.Priority;
                                DestinationNumber = DSEFirstPriority.PhoneNumber;

                            }
                        }
                    }

                }
                #endregion

                #region CustomerType
                if ((sapcustomer.CustomerType.Equals(0)) && (sapcustomer.CustomerTypeValue != null) && (sapcustomer.CustomerTypeValue != String.Empty))
                {
                    EnumTipePelangganSPKCustomer enumCustomerType = new EnumTipePelangganSPKCustomer();
                    EnumTipePelanggan enumTipePelanggan = enumCustomerType.RetrieveType().Cast<EnumTipePelanggan>().Where(o => o.NameTipe.ToLower().Equals(sapcustomer.CustomerTypeValue.ToLower())).FirstOrDefault();
                    sapcustomer.CustomerType = (short)((enumTipePelanggan == null) ? 0 : enumTipePelanggan.ValTipe);
                }
                #endregion

                #region Sex
                if ((sapcustomer.Sex.Equals(0)) && (sapcustomer.SexValue != null) && (sapcustomer.SexValue != String.Empty))
                    sapcustomer.Sex = (byte)(sapcustomer.SexValue.ToLower().Equals("male") ? 1 : 2);
                #endregion

                #region AgeSegment
                if (sapcustomer.AgeSegmentDate == null) sapcustomer.AgeSegmentDate = DateTime.MinValue;
                //if (!sapcustomer.AgeSegmentDate.Equals(DateTime.MinValue))
                //{
                DateTime now = DateTime.Today; byte ageresult = 0;
                int age = now.Year - sapcustomer.AgeSegmentDate.Year;
                if (sapcustomer.AgeSegmentDate > now.AddYears(-age)) age--;
                if (age <= 29) ageresult = 1;
                else if (age <= 39) ageresult = 2;
                else if (age <= 49) ageresult = 3;
                else ageresult = 4;

                sapcustomer.AgeSegment = ageresult;
                //}
                #endregion


                #region CustomerPurpose
                if ((sapcustomer.CustomerPurpose.Equals(0)) && (sapcustomer.CustomerPurposeValue != null) && (sapcustomer.CustomerPurposeValue != String.Empty))
                {
                    //EnumCustomerPurposeOp enumCustomerPurposeOp = EnumCustomerPurposeOp.RetriveCustomerPurpose(true).Cast<EnumCustomerPurposeOp>().Where(o => o.NameStatus.ToLower().Equals(sapcustomer.CustomerPurposeValue.ToLower())).FirstOrDefault();
                    //sapcustomer.CustomerPurpose = (short)((enumCustomerPurposeOp == null) ? 0 : enumCustomerPurposeOp.ValStatus);
                    if (listCustomerPurpose.Where(x => x.ValueDesc.ToLower() == sapcustomer.CustomerPurposeValue.ToLower().Trim()).Count() > 0)
                    {
                        sapcustomer.CustomerPurpose = short.Parse(listCustomerPurpose.FirstOrDefault(x => x.ValueDesc.ToLower()
                            == sapcustomer.CustomerPurposeValue.ToLower().Trim()).ValueId.ToString());
                    }
                    else { sapcustomer.CustomerPurpose = 0; }
                }
                #endregion

                #region InformationType
                //sapcustomer.InformationType = 0;
                if ((sapcustomer.InformationType.Equals(0)) && (sapcustomer.InformationTypeValue != null) && (sapcustomer.InformationTypeValue != String.Empty))
                {
                    //EnumInformationTypeOp enumInformationTypeOp = EnumInformationTypeOp.RetriveInformationType(true).Cast<EnumInformationTypeOp>().Where(o => o.NameStatus.ToLower().Equals(sapcustomer.InformationTypeValue.ToLower())).FirstOrDefault();
                    //sapcustomer.InformationType = (short)((enumInformationTypeOp == null) ? 0 : enumInformationTypeOp.ValStatus);
                    scMap.SourceInformation = sapcustomer.InformationTypeValue;
                    if (listInformationType.Where(x => x.ValueDesc.ToLower() == sapcustomer.InformationTypeValue.ToLower().Trim()).Count() > 0)
                    {
                        sapcustomer.InformationType = short.Parse(listInformationType.FirstOrDefault(x => x.ValueDesc.ToLower()
                            == sapcustomer.InformationTypeValue.ToLower().Trim()).ValueId.ToString());
                    }
                    else
                    {
                        if ((sapcustomer.InformationSourceValue != null) && (sapcustomer.InformationSourceValue != String.Empty))
                        {
                            if (listInformationTypeSF.Where(x => x.ValueDesc.ToLower() == sapcustomer.InformationSourceValue.ToLower().Trim()).Count() > 0)
                            {
                                sapcustomer.InformationType = short.Parse(listInformationTypeSF.FirstOrDefault(x => x.ValueDesc.ToLower()
                                    == sapcustomer.InformationSourceValue.ToLower().Trim()).ValueId.ToString());
                            }
                            else
                            {
                                sapcustomer.InformationType = short.Parse(cfgConfig.Retrieve("LeadDefaultInformationSource").Value); //default value from appconfig
                            }
                        }
                        else
                        {
                            sapcustomer.InformationType = short.Parse(cfgConfig.Retrieve("LeadDefaultInformationSource").Value); //default value from appconfig
                        }
                    }
                }

                #endregion

                #endregion

                #region Validation

                //SalesforceID
                if ((sapcustomer.SalesforceID == null) || (sapcustomer.SalesforceID == String.Empty))
                    message = String.Concat(message, "SalesforceID can't be empty. \n");

                //WebID
                //if ((sapcustomer.WebID == null) || (sapcustomer.WebID == String.Empty))
                //    message = String.Concat(message, "WebID can't be empty. \n");

                //DealerCode
                if (sapcustomer.Dealer == null)
                    message = String.Concat(message, "Wrong DealerCode [", sapcustomer.DealerCode, "]. \n");

                //CustomerName
                if ((sapcustomer.CustomerName == null) || (sapcustomer.CustomerName == String.Empty))
                    message = String.Concat(message, "CustomerName can't be empty. \n");

                //CustomerType
                if (sapcustomer.CustomerType == null)
                    message = String.Concat(message, "Wrong CustomerTypeValue [", sapcustomer.CustomerTypeValue, "]. \n");

                //Phone
                if ((sapcustomer.Phone == null) || (sapcustomer.Phone == String.Empty))
                    message = String.Concat(message, "Phone can't be empty. \n");

                //CustomerPurpose
                if (sapcustomer.CustomerPurpose == null)
                    message = String.Concat(message, "Wrong CustomerPurposeValue [", sapcustomer.CustomerPurposeValue, "]. \n");

                //InformationType
                if (sapcustomer.InformationType == null)
                    message = String.Concat(message, "Wrong InformationTypeValue [", sapcustomer.InformationTypeValue, "]. \n");

                //InformationSource
                if (sapcustomer.InformationSource == null || sapcustomer.InformationSource == 0)
                    message = String.Concat(message, "Wrong InformationSourceValue [", sapcustomer.InformationSourceValue, "]. \n");

                if (!message.Length.Equals(0)) isvalid = false;
                else isvalid = true;
                #endregion

                if (isvalid)
                {
                    bool isUpdate = false;
                    bool isSendNotif = true;
                    bool isDuplicate = false;
                    int SapCustomerID = 0;

                    SAPCustomerFacade sapcustomerFacade = new SAPCustomerFacade(UserSalesforce);
                    criterias = new CriteriaComposite(new Criteria(typeof(SAPCustomer), "RowStatus", 0));
                    criterias.opAnd(new Criteria(typeof(SAPCustomer), "SalesforceID", sapcustomer.SalesforceID));
                    ArrayList arrSapCus = new SAPCustomerFacade(UserSalesforce).Retrieve(criterias);
                    if (arrSapCus.Count > 0)
                    {
                        foreach (var item in arrSapCus)
                        {
                            SAPCustomer iCust = (SAPCustomer)item;
                            if (iCust.SalesforceID == sapcustomer.SalesforceID)
                            {
                                isUpdate = true;
                                iCust.Dealer = sapcustomer.Dealer;
                                iCust.SalesmanHeader = sapcustomer.SalesmanHeader;
                                iCust.CustomerName = sapcustomer.CustomerName;
                                iCust.Phone = sapcustomer.Phone;
                                iCust.Email = sapcustomer.Email;
                                iCust.InformationSource = sapcustomer.InformationSource;
                                iCust.Variants = sapcustomer.Variants;
                                iCust.VehicleModel = sapcustomer.VehicleModel;

                                if (iCust.Status == 0) //new
                                {
                                    sapcustomer.Status = (int)EnumSAPCustomerStatus.SAPCustomerStatus.Suspect;
                                }
                                else { sapcustomer.Status = iCust.Status; }

                                if (iCust.SalesmanHeader != null)
                                {
                                    if (iCust.SalesmanHeader.Status == ((int)EnumSalesmanStatus.SalesmanStatus.Aktif).ToString())
                                    {
                                        sapcustomer.SalesmanHeader = iCust.SalesmanHeader;
                                        SapCustomerID = sapcustomerFacade.Update(iCust);
                                    }
                                    else
                                    {
                                        SapCustomerID = sapcustomerFacade.Update(iCust);
                                        if (isAuto)
                                        {
                                            new SalesmanDSEAssignmentFacade(UserSalesforce).Update(objSls);
                                        }
                                    }
                                }
                                else
                                {
                                    SapCustomerID = sapcustomerFacade.Update(iCust);
                                    if (isAuto)
                                    {
                                        new SalesmanDSEAssignmentFacade(UserSalesforce).Update(objSls);
                                    }
                                }
                                SapCustomerID = iCust.ID;//sapcustomer.ID;
                                break;
                            }
                        }
                    }
                    else
                    {
                        criterias = new CriteriaComposite(new Criteria(typeof(SAPCustomer), "RowStatus", 0));
                        criterias.opAnd(new Criteria(typeof(SAPCustomer), "VehicleModel", sapcustomer.VehicleModel));
                        criterias.opAnd(new Criteria(typeof(SAPCustomer), "Phone", sapcustomer.Phone));
                        criterias.opAnd(new Criteria(typeof(SAPCustomer), "Dealer.ID", sapcustomer.Dealer.ID));
                        criterias.opAnd(new Criteria(typeof(SAPCustomer), "CustomerName", sapcustomer.CustomerName));
                        criterias.opAnd(new Criteria(typeof(SAPCustomer), "Status", MatchType.NotInSet, string.Format("{0},{1}", (int)EnumSAPCustomerStatus.SAPCustomerStatus.Deal_SPK, (int)EnumSAPCustomerStatus.SAPCustomerStatus.Canceled)));
                        arrSapCus = new SAPCustomerFacade(UserSalesforce).Retrieve(criterias);
                        if (arrSapCus.Count > 0)
                        {
                            isSendNotif = false;
                            isDuplicate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        string strStrip = "";
                        if (sapcustomer.VehicleModel.ToString().Trim() != "" && sapcustomer.Variants.ToString().Trim() != "")
                        {
                            strStrip = " - ";
                        }
                        sapcustomer.Note = sapcustomer.VehicleModel.ToString() + strStrip + sapcustomer.Variants.ToString();

                        if (isDuplicate)
                        {
                            sapcustomer.Status = (int)EnumSAPCustomerStatus.SAPCustomerStatus.Canceled;
                            sapcustomer.Note = string.Format("Cancel data same with kode lead {0}", ((SAPCustomer)arrSapCus[0]).ID);
                            sapcustomer.LeadStatus = 8;
                        }
                        else
                        {
                            sapcustomer.Status = (int)EnumSAPCustomerStatus.SAPCustomerStatus.Suspect;
                            sapcustomer.LeadStatus = 1;
                        }

                        sapcustomer.Sequence = sapcustomerFacade.GetSequence(sapcustomer.VehicleModel);
                        sapcustomer.CountryCode = "62";

                        string strSequence = "0000" + sapcustomer.Sequence.ToString();
                        string strNewSequence = strSequence.Substring(strSequence.Length - 4, 4);
                        sapcustomer.Topic = "MMKSI Digital Lead-" + sapcustomer.CustomerName + " - " + sapcustomer.VehicleModel + " - " + strNewSequence;

                        SapCustomerID = sapcustomerFacade.Insert(sapcustomer);
                        if (isAuto)
                        {
                            new SalesmanDSEAssignmentFacade(UserSalesforce).Update(objSls);
                        }
                    }
                    success = true; message = string.Empty;
                    scMap.SAPCustomer = new SAPCustomer(SapCustomerID);
                    SAPCustomerMappingFacade scMapFac = new SAPCustomerMappingFacade(UserSalesforce);
                    scMapFac.InsertFromLead(scMap);

                    if (sapcustomer.SalesmanHeader != null && isSendNotif && isLeadSource)
                    {
                        Dictionary<string, string> dcConfig = sapcustomerFacade.GetBodyMessage(SapCustomerID);
                        AppConfigFacade funcConfig = new AppConfigFacade(UserSalesforce);
                        MessageServiceUser msg = new MessageServiceUser();
                        msg.ClientID = dcConfig.FirstOrDefault(x => x.Key == "ClientID").Value;
                        msg.UserName = dcConfig.FirstOrDefault(x => x.Key == "Username").Value;
                        msg.Password = dcConfig.FirstOrDefault(x => x.Key == "Password").Value;
                        msg.FID = SapCustomerID.ToString();
                        msg.TypeMessage = "1";
                        msg.BodyMessage = dcConfig.FirstOrDefault(x => x.Key == "BodyMessage").Value;
                        msg.DestinationNo = DestinationNumber;


                        APIHelpers<MessagerResponse> api = new APIHelpers<MessagerResponse>();
                        api.JsonContent = JsonConvert.SerializeObject(msg);
                        api.Url = funcConfig.Retrieve("MSLeadApiUrl").Value;// "http://localhost/MMKSI.MessageServices/Api/MessageService/";
                        api.ProxyAddress = funcConfig.Retrieve("MSLeadApiProxyAddress").Value;
                        api.ProxyPort = funcConfig.Retrieve("MSLeadApiProxyPort").Value;
                        Task.Run(() => api.POST()).Wait();
                    }

                }
                else
                {
                    wLog.Status = "false";
                    new WsLogFacade(UserSalesforce).Update(wLog);
                    success = false;
                    this.SendEmail();
                }
            }
            catch (Exception ex)
            {
                success = false; message = ex.Message;
                this.SendEmail();
            }

            return this.result(success, "-1", 0, message, null);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  
            Random _random = new Random();
            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string GetClientIpAddress(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop;
                prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }

        private void SendEmail()
        {
            try
            {
                string bodyTemplate = @"<FONT face=Arial size=1>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                            <tr>
                                            <td colspan=3 align=center><b>Lead data Tidak Naik Ke D-NET </b></td>
                                            </tr>
                                            <tr>
                                            <td colspan=3 height=50></td>
                                            </tr>
                                            <tr>
                                            <td colspan=3 height=50>
                                            Data {0} gagal masuk ke Lead data D-Net.
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan=3 height=10></td>
                                            </tr>

                                            <tr>
                                            <td colspan=5 height=10></td>
                                            </tr>

                                            </table>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>

                                            <tr>
                                            <td width='100%'>Terimakasih</td>
                                            </tr>

                                            <tr>
                                            <td >&nbsp;</td>
                                            </tr>

                                            <tr>
                                            </tr>
                                            </table>
                                            </FONT>";
                GenericPrincipal UserSalesforce = new System.Security.Principal.GenericPrincipal(new GenericIdentity("Salesforce"), null);
                AppConfigFacade funcConfig = new AppConfigFacade(UserSalesforce);
                string EmailFrom = funcConfig.Retrieve("MSLeadApiEmailFrom").Value;
                string EmailTo = funcConfig.Retrieve("MSLeadApiEmailTo").Value;

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(EmailFrom);
                string[] EmailToList = EmailTo.Split(';');
                foreach (string em in EmailToList)
                {
                    message.To.Add(new MailAddress(em));
                }

                message.Subject = "Lead Data Fail";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = string.Format(bodyTemplate, JsonContent);
                string SmtpAddress = WebConfigurationManager.AppSettings["SMTP"];
                SmtpClient SmtpServer = new SmtpClient(SmtpAddress);
                SmtpServer.Port = 25;
                SmtpServer.Send(message);
            }
            catch (Exception e)
            {
                string cuk = e.Message;
            }
        }
    }
}