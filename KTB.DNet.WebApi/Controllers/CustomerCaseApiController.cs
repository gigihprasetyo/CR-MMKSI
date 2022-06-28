using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using KTB.DNet.Domain;
using KTB.DNET.BusinessFacade;
using KTB.DNET.BusinessFacade.Service;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.WebApi.Models;
using KTB.DNet.WebApi.Helpers;
using System.Collections;
using KTB.DNet.Domain.Search;
using System.Globalization;
using KTB.DNet.BusinessFacade.FinishUnit;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web.Configuration;
using KTB.DNet.BusinessFacade.Service;

namespace KTB.DNet.WebApi.Controllers
{
    [RoutePrefix("api/CustomerCaseApi")]
    public class CustomerCaseApiController : BaseApiController
    {
        static GenericPrincipal User = new GenericPrincipal(new GenericIdentity("Salesforce"), null);
        string newJsonBody = string.Empty;

        /**
        *   @api {post} /api/customercaseapi/create Create Customer Case
        *   @apiName Create a new Customer Case
        *   @apiGroup Customer Case
        *
        *   @apiHeader {String} X-Authorization-Token Token value
        *   @apiHeaderExample {String} Header Example:
        *       {
        *           "X-Authorization-Token": "THIS.IS.TOKEN.TAKEN.FROM.RETURN.RESULT.OF /api/authapi/token"
        *       }
        *   
        *	@apiParam {String} SalesforceID ID of Sales Force
        *   @apiParam {String} DealerCode dealer of Lead
        *	@apiParam {String} CaseNumber case number of CustomerCase
        *	@apiParam {String} CustomerName customer name of CustomerCase
        *	@apiParam {String} Phone phone of CustomerCase
        *	@apiParam {String} Email email of CustomerCase
        *	@apiParam {String} Category category of CustomerCase
        *	@apiParam {String} SubCategory1 sub category 1 of CustomerCase
        *	@apiParam {String} SubCategory2 sub category 2 of CustomerCase
        *	@apiParam {String} SubCategory3 sub category 3 of CustomerCase
        *	@apiParam {String} SubCategory4 sub category 4 of CustomerCase
        *	@apiParam {String} CallerType caller type of CustomerCase
        *	@apiParam {String} CarType car type of CustomerCase
        *	@apiParam {String} Variants variant of CustomerCase
        *	@apiParam {String} EngineNumber engine number of CustomerCase
        *	@apiParam {String} ChassisNumber chassis number of CustomerCase
        *	@apiParam {Number} Odometer odometer of CustomerCase
        *	@apiParam {String} PlateNumber plate number of CustomerCase
        *	@apiParam {Number=0,1,2} [Priority] priority of CustomerCase
        *	@apiParam {String="Medium", "High"} PriorityValue priority value of CustomerCase. If <code>Priority</code> parameter is define, then this value will be ignored
        *	@apiParam {String} CaseNumberReff case number reff of CustomerCase
        *	@apiParam {Date} CaseDate case date of CustomerCase
        *	@apiParam {String} Subject subject of CustomerCase
        *	@apiParam {String} Description description of CustomerCase
        *   @apiParam {Number=0,1} [RowStatus=0] row status of Lead
        *   @apiParam {String} [CreatedBy] created by of Lead
        *   @apiParam {Date} [CreatedTime] created time of Lead
        *   @apiParam {String} [LastUpdateBy] last update by of Lead
        *   @apiParam {Date} [LastUpdateTime] last update time of Lead
        *
        *   @apiParamExample {json} Body Example:
        *       {
        *           "SalesforceID": "String",
        *           "DealerCode": "String",
        *           "CaseNumber": "String",
        *           "CustomerName": "String",
        *           "Phone": "String",
        *           "Email": "String",
        *           "Category": "String",
        *           "SubCategory1": "String",
        *           "SubCategory2": "String",
        *           "SubCategory3": "String",
        *           "SubCategory4": "String",
        *           "CallerType": "String",
        *           "CarType": "String",
        *           "Variants": "String",
        *           "EngineNumber": "String",
        *           "ChassisNumber": "String",
        *           "Odometer": Number,
        *           "PlateNumber": "String",
        *           "PriorityValue": "High",
        *           "CaseNumberReff": "String",
        *           "CaseDate": "Date",
        *           "Subject": "String",
        *           "Description": "String"
        *           "ServiceType": "String"
        *           "BookingDatetime": "Date"
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
            bool success = false; string message = string.Empty; bool isvalid = false; string jsonBody = String.Empty; bool isNewCase = false;
            CustomerCase customercase = new CustomerCase();
            if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);

            try
            {
                jsonBody = Request.Content.ReadAsStringAsync().Result;

                if (jsonBody.Contains("\"BookingDatetime\":null"))
                {
                    DateTime now = DateTime.Now;
                    string replacement = "\"BookingDatetime\":\"" + now.ToString() + "\"";
                    newJsonBody = jsonBody.Replace("\"BookingDatetime\":null", replacement);
                }
                else
                    newJsonBody = jsonBody;

                customercase = JsonConvert.DeserializeObject<CustomerCase>(newJsonBody);

                #region Mapping

                #region Dealer
                if ((customercase.DealerCode != null) && (customercase.DealerCode != String.Empty))
                {
                    DealerFacade dealerFacade = new DealerFacade(User);
                    customercase.Dealer = dealerFacade.GetDealer(customercase.DealerCode);
                }
                #endregion

                #region Priority
                if ((customercase.Priority.Equals(0)) && (customercase.PriorityValue != null) && (customercase.PriorityValue != String.Empty))
                {
                    customercase.Priority = (short)((customercase.PriorityValue.ToLower().Equals("high")) ? 2 : 1);
                }
                #endregion

                #region StatusDesc
                if ((customercase.StatusDesc != null) && (customercase.StatusDesc != String.Empty))
                {
                    // customercase.Status = (short)((customercase.StatusDesc.ToLower().Equals("new")) ? 0 : 1);

                    string statusVal = customercase.StatusDesc.ToLower();
                    switch (statusVal)
                    {
                        case "new":
                            customercase.Status =  (short)EnumCustomerCaseResponse.CustomerCaseResponse.NewStatus;
                            break;
                        case "re-open":
                            customercase.Status = (short)EnumCustomerCaseResponse.CustomerCaseResponse.Re_Open;
                            break;
                        case "in progress":
                            customercase.Status = (short)EnumCustomerCaseResponse.CustomerCaseResponse.Inprogres;
                            break;
                        case "escalated":
                            customercase.Status = (short)EnumCustomerCaseResponse.CustomerCaseResponse.Escalated;
                            break;
                        case "closed":
                            customercase.Status = (short)EnumCustomerCaseResponse.CustomerCaseResponse.Closed;
                            break;
                        case "re-schedule":
                            customercase.Status = (short)EnumCustomerCaseResponse.CustomerCaseResponse.Re_Schedule;
                            break;
                        case "cancellation":
                            customercase.Status = (short)EnumCustomerCaseResponse.CustomerCaseResponse.Cancellation;
                            break;
                    }
                }
                #endregion

                #endregion

                #region Validation

                //SalesforceID
                if ((customercase.SalesforceID == null) || (customercase.SalesforceID == String.Empty))
                    message = String.Concat(message, "SalesforceID can't be empty. \n");
                //DealerCode
                //CaseNumber
                if ((customercase.CaseNumber == null) || (customercase.CaseNumber == String.Empty))
                    message = String.Concat(message, "CaseNumber can't be empty. \n");
                //CustomerName
                if ((customercase.CustomerName == null) || (customercase.CustomerName == String.Empty))
                    message = String.Concat(message, "CustomerName can't be empty. \n");
                ////Phone
                //if ((customercase.Phone == null) || (customercase.Phone == String.Empty))
                //    message = String.Concat(message, "Phone can't be empty. \n");
                ////Email
                //if ((customercase.Email == null) || (customercase.Email == String.Empty))
                //    message = String.Concat(message, "Email can't be empty. \n");
                ////Category
                //if ((customercase.Category == null) || (customercase.Category == String.Empty))
                //    message = String.Concat(message, "Category can't be empty. \n");
                ////SubCategory1
                //if ((customercase.SubCategory1 == null) || (customercase.SubCategory1 == String.Empty))
                //    message = String.Concat(message, "SubCategory1 can't be empty. \n");
                ////SubCategory2
                //if ((customercase.SubCategory2 == null) || (customercase.SubCategory2 == String.Empty))
                //    message = String.Concat(message, "SubCategory2 can't be empty. \n");
                ////SubCategory3
                //if ((customercase.SubCategory3 == null) || (customercase.SubCategory3 == String.Empty))
                //    message = String.Concat(message, "SubCategory3 can't be empty. \n");
                ////SubCategory4
                //if ((customercase.SubCategory4 == null) || (customercase.SubCategory4 == String.Empty))
                //    message = String.Concat(message, "SubCategory4 can't be empty. \n");
                ////CallerType
                //if ((customercase.CallerType == null) || (customercase.CallerType == String.Empty))
                //    message = String.Concat(message, "CallerType can't be empty. \n");
                ////CarType
                //if ((customercase.CarType == null) || (customercase.CarType == String.Empty))
                //    message = String.Concat(message, "CarType can't be empty. \n");
                ////Variants
                //if ((customercase.Variants == null) || (customercase.Variants == String.Empty))
                //    message = String.Concat(message, "Variants can't be empty. \n");
                ////EngineNumber
                //if ((customercase.EngineNumber == null) || (customercase.EngineNumber == String.Empty))
                //    message = String.Concat(message, "EngineNumber can't be empty. \n");
                ////ChassisNumber
                //if ((customercase.ChassisNumber == null) || (customercase.ChassisNumber == String.Empty))
                //    message = String.Concat(message, "ChassisNumber can't be empty. \n");                
                ////PlateNumber
                //if ((customercase.PlateNumber == null) || (customercase.PlateNumber == String.Empty))
                //    message = String.Concat(message, "PlateNumber can't be empty. \n");
                ////CaseNumberReff
                //if ((customercase.CaseNumberReff == null) || (customercase.CaseNumberReff == String.Empty))
                //    message = String.Concat(message, "CaseNumberReff can't be empty. \n");

                //InformationSource
                if (customercase.Priority == null)
                    message = String.Concat(message, "Wrong PriorityValue [", customercase.PriorityValue, "]. \n");

                if (!message.Length.Equals(0)) isvalid = false;
                else isvalid = true;
                #endregion

                if (isvalid)
                {
                    //customercase.CreatedBy = "WebService";
                    //KTB.DNET.BusinessFacade.CustomerCaseFacade objCustomerCaseFacade = new KTB.DNET.BusinessFacade.CustomerCaseFacade(User);
                    //objCustomerCaseFacade.Insert(customercase);

                    //success = true; message = "";

                    ArrayList dataResult;
                    CriteriaComposite criterias;
                    criterias = new CriteriaComposite(new Criteria(typeof(CustomerCase), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias.opAnd(new Criteria(typeof(CustomerCase), "SalesforceID", MatchType.Exact, customercase.SalesforceID));
                    criterias.opAnd(new Criteria(typeof(CustomerCase), "CaseNumber", MatchType.Exact, customercase.CaseNumber));
                    CustomerCaseFacade objCustomerCaseFacade = new CustomerCaseFacade(User);
                    dataResult = objCustomerCaseFacade.Retrieve(criterias);

                    if (dataResult.Count <= 0)
                    {
                        isNewCase = true;
                        var res = objCustomerCaseFacade.Insert(customercase);
                        if (res <= 0)
                            throw new ArgumentException("Insert customer case gagal");

                        customercase.ID = Convert.ToInt32(res);

                        if (!string.IsNullOrEmpty(customercase.ReservationNumber))
                        {
                            ServiceBookingFacade objSvcFacade = new ServiceBookingFacade(User);
                            ServiceBooking objSb = objSvcFacade.Retrieve(customercase.ReservationNumber);
                            if (objSb.ID != 0)
                            {
                                try
                                {
                                    objSb.Status = Convert.ToInt32(EnumStallMaster.StatusBooking.Booked).ToString();
                                    res = objSvcFacade.Update(objSb);
                                    if (res < 1)
                                        throw new System.ArgumentException("Update Status Service Booking gagal");
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }

                        if (validateChassisMaster(customercase.ChassisNumber, User) && (customercase.ServiceType == "FS" || customercase.ServiceType == "PM"))
                        {
                            if (customercase.SubCategory1.Contains("Service Booking") &&
                                (customercase.SubCategory2 == null || string.IsNullOrEmpty(customercase.SubCategory2) ||
                                string.IsNullOrWhiteSpace(customercase.SubCategory2) || customercase.SubCategory2 == "3.3.2 Periodical Maintenance") ||
                                customercase.SubCategory2 == "3.3.1 Free Service")
                            {
                                processServiceReminder(customercase, User);
                            }
                        }
                    }
                    else
                    {
                        //CustomerCase objCustCaseEdit = (CustomerCase)dataResult[dataResult.Count - 1];
                        CustomerCase objCustCase = (CustomerCase)dataResult[0];
                        CustomerCase customerCaseUpdated = new CustomerCase();
                        customerCaseUpdated = customercase;
                        customerCaseUpdated.ID = objCustCase.ID;
                        objCustomerCaseFacade.Update(customerCaseUpdated);

                        //Update Service Booking to "Batal" When Status Customer Case "Closed"
                        if (customerCaseUpdated.Status == (short)EnumCustomerCaseResponse.CustomerCaseResponse.Closed)
                        {
                            criterias = new CriteriaComposite(new Criteria(typeof(CustomerCaseResponse), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criterias.opAnd(new Criteria(typeof(CustomerCaseResponse), "CustomerCase.SalesforceID", MatchType.Exact, customercase.SalesforceID));
                            criterias.opAnd(new Criteria(typeof(CustomerCaseResponse), "CustomerCase.CaseNumber", MatchType.Exact, customercase.CaseNumber));
                            criterias.opAnd(new Criteria(typeof(CustomerCaseResponse), "ServiceBooking.ID", MatchType.Greater, 0));

                            Sort sort = new Sort(typeof(CustomerCaseResponse), "ID", Sort.SortDirection.DESC);
                            SortCollection sorts = new SortCollection();
                            sorts.Add(sort);

                            CustomerCaseResponseFacade objCustomerCaseRespFacade = new CustomerCaseResponseFacade(User);
                            var arr = objCustomerCaseRespFacade.Retrieve(criterias, sorts);
                            if (arr.Count > 0)
                            {
                                CustomerCaseResponse ccResp = (CustomerCaseResponse)arr[0];
                                var svcBook = ccResp.ServiceBooking;
                                if (svcBook != null && svcBook.Status != ((int)EnumStallMaster.StatusBooking.Batal).ToString())
                                {
                                    svcBook.Status = ((int)EnumStallMaster.StatusBooking.Batal).ToString();
                                    ServiceBookingFacade svcBookFacade = new ServiceBookingFacade(User);
                                    svcBookFacade.Update(svcBook);
                                }
                            }

                        }
                    }

                    //Send SMS
                    if((customercase.Dealer != null) && (isNewCase || customercase.Status == (short)EnumCustomerCaseResponse.CustomerCaseResponse.Re_Open))
                    {
                        ArrayList contacts = objCustomerCaseFacade.RetrievePhoneNumber(customercase.ID);
                        if(contacts.Count > 0)
                        {
                            Dictionary<string, string> dcConfig = objCustomerCaseFacade.GetBodyMessage(customercase.ID);
                            AppConfigFacade funcConfig = new AppConfigFacade(User);
                            foreach(string phoneNumber in contacts)
                            {
                                if(phoneNumber.Length > 7)
                                {
                                    MessageServiceUser msg = new MessageServiceUser();
                                    msg.ClientID = dcConfig.FirstOrDefault(x => x.Key == "ClientID").Value;
                                    msg.UserName = dcConfig.FirstOrDefault(x => x.Key == "Username").Value;
                                    msg.Password = dcConfig.FirstOrDefault(x => x.Key == "Password").Value;
                                    msg.FID = customercase.ID.ToString();
                                    msg.TypeMessage = "1";
                                    msg.BodyMessage = dcConfig.FirstOrDefault(x => x.Key == "BodyMessage").Value;
                                    msg.DestinationNo = phoneNumber;


                                    APIHelpers<MessagerResponse> api = new APIHelpers<MessagerResponse>();
                                    api.JsonContent = JsonConvert.SerializeObject(msg);
                                    api.Url = funcConfig.Retrieve("MSLeadApiUrl").Value;// "http://localhost/MMKSI.MessageServices/Api/MessageService/";
                                    api.ProxyAddress = funcConfig.Retrieve("MSLeadApiProxyAddress").Value;
                                    api.ProxyPort = funcConfig.Retrieve("MSLeadApiProxyPort").Value;
                                    Task.Run(() => api.POST()).Wait();
                                }
                            }
                        }
                    }
                    success = true; message = "";
                }
                else
                {
                    success = false;
                    this.SendEmail(customercase);
                }
            }
            catch (Exception ex)
            {
                success = false; message = ex.Message + ex.StackTrace.ToString();
                this.SendEmail(customercase);
            }

            #region Log
            WsLog wslog = new WsLog();
            wslog.Source = GetClientIp();
            wslog.Status = success.ToString();
            wslog.Message = message;
            wslog.Body = String.Concat("K:SALESFORCECUSTOMERCASE\n", newJsonBody);
            wslog.RowStatus = 0;
            wslog.CreatedBy = "WebService";

            WsLogFacade wslogfacade = new WsLogFacade(User);
            wslogfacade.Insert(wslog);
            #endregion

            return this.result(success, "-1", 0, message, null);
        }

        [HttpPost]
        [Route("TestSvcReminderUpdateCreate")]
        public IDictionary<string, object> TestSvcReminderUpdateCreate()
        {
            bool success = false; string message = string.Empty; bool isvalid = false; string jsonBody = String.Empty;
            if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);

            jsonBody = Request.Content.ReadAsStringAsync().Result;
            var customercase = JsonConvert.DeserializeObject<CustomerCase>(jsonBody);

            if ((customercase.DealerCode != null) && (customercase.DealerCode != String.Empty))
            {
                DealerFacade dealerFacade = new DealerFacade(User);
                customercase.Dealer = dealerFacade.GetDealer(customercase.DealerCode);
            }

            processServiceReminder(customercase, User);

            message = "oke";
            return this.result(success, "-1", 0, message, null);
        }

        private bool validateSFID(ArrayList arrCustCase, string sfid)
        {
            foreach (CustomerCase s in arrCustCase)
            {
                if (s.SalesforceID == sfid)
                    return false;
            }

            return true;
        }

        private ServiceReminder getSvcReminderByChassis(string chassisNumber, ServiceReminderFacade svcReminderFacade)
        {
            CriteriaComposite criterias;
            criterias = new CriteriaComposite(new Criteria(typeof(ServiceReminder), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ServiceReminder), "ChassisNumber", MatchType.Exact, chassisNumber));
            var sortColl = new SortCollection();
            sortColl.Add(new Sort(typeof(ServiceReminder), "ActualKM", Sort.SortDirection.DESC));
            ArrayList arrSvcReminder = svcReminderFacade.Retrieve(criterias, sortColl);

            if (arrSvcReminder.Count == 0 || arrSvcReminder.IsNull())
                return null;
            else
            {
                return (ServiceReminder)arrSvcReminder[0];
            }
        }

        private PMKind getPMKindByKindCode(string code)
        {
            CriteriaComposite criterias;
            criterias = new CriteriaComposite(new Criteria(typeof(PMKind), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(PMKind), "KindCode", MatchType.Exact, code));
            KTB.DNet.BusinessFacade.Service.PMKindFacade pMKindFacade = new KTB.DNet.BusinessFacade.Service.PMKindFacade(User);

            return (PMKind)pMKindFacade.Retrieve(criterias)[0];
        }

        private void createSvcReminder(CustomerCase custCase, ServiceReminderFacade svcReminderFacade, ref ServiceReminder svcReminder, System.Security.Principal.IPrincipal _user)
        {
            GenericPrincipal User1 = new System.Security.Principal.GenericPrincipal(new GenericIdentity("Salesforce"), null);

            PMKind newPMKind = new PMKind();
            CriteriaComposite criterias;
            criterias = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, custCase.ChassisNumber));
            var arrChassisMaster = new KTB.DNet.BusinessFacade.FinishUnit.ChassisMasterFacade(_user).Retrieve(criterias);

            if (arrChassisMaster.Count > 0)
            {
                ChassisMaster chassisMaster = (ChassisMaster)arrChassisMaster[0];
                svcReminder.ChassisMaster = chassisMaster;
                svcReminder.ChassisNumber = chassisMaster.ChassisNumber;
                svcReminder.EngineNumber = chassisMaster.EngineNumber;
                svcReminder.VehicleType = chassisMaster.VechileColor.VechileType.Description;
                svcReminder.Category = chassisMaster.Category;
            }
            else
            {
                Category category = new Category();
                svcReminder.ChassisNumber = custCase.ChassisNumber;
                svcReminder.EngineNumber = custCase.EngineNumber;
                svcReminder.VehicleType = custCase.Variants;
                if (custCase.CarType == "PC")
                    category.ID = 1;
                else
                    category.ID = 2;

                svcReminder.Category = category;
            }

            //svcReminder.SalesforceID = custCase.SalesforceID;
            svcReminder.Dealer = custCase.Dealer;
            svcReminder.ServiceReminderDate = custCase.BookingDatetime;
            svcReminder.MaxFUDealerDate = custCase.BookingDatetime.AddDays(30);
            svcReminder.BookingDate = DateTime.ParseExact(custCase.BookingDatetime.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            svcReminder.BookingTime = custCase.BookingDatetime.ToString("HH:mm:ss");
            svcReminder.CaseNumber = custCase.CaseNumber;
            svcReminder.CustomerName = custCase.CustomerName;
            svcReminder.CustomerPhoneNumber = custCase.Phone;
            svcReminder.TransactionType = 2;
            svcReminder.ActualKM = custCase.Odometer;
            svcReminder.Status = 1;
            svcReminder.Remark = "Salesforce : " + custCase.Subject;
            if (!string.IsNullOrEmpty(custCase.SubCategory4))
                svcReminder.Remark = "Salesforce : " + custCase.SubCategory4;

            var lastSvcReminder = getSvcReminderByChassis(custCase.ChassisNumber, svcReminderFacade);
            if (!lastSvcReminder.IsNull())
            {
                PMKind pMkind = getPMKindByKindCode(lastSvcReminder.PMKind.KindCode);
                int newKindCode = Convert.ToInt32(pMkind.KindCode) + 2;

                newPMKind = getPMKindByKindCode(newKindCode.ToString("D2"));
            }
            else
                newPMKind = getPMKindByKindCode("01");

            svcReminder.PMKind = newPMKind;
            svcReminder.CreatedBy = "Salesforce";

            bool PKTStatus = false;
            var tempPKTDate = getPKT(custCase.ChassisNumber, _user, ref PKTStatus);
            if (PKTStatus)
                svcReminder.PKTDate = tempPKTDate;

            try
            {
                var res = new ServiceReminderFacade(User1).Insert(svcReminder);
                if (res < 1)
                    throw new System.ArgumentException("Insert Service Reminder baru gagal");

                svcReminder.ID = res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void updateSvcReminder(CustomerCase custCase, ServiceReminder svcReminder, ServiceReminderFacade svcReminderFacade, System.Security.Principal.IPrincipal _user, byte status = 2)
        {
            GenericPrincipal User1 = new System.Security.Principal.GenericPrincipal(new GenericIdentity("Salesforce"), null);

            if (status != 3)
            {
                svcReminder.BookingDate = DateTime.ParseExact(custCase.BookingDatetime.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                svcReminder.BookingTime = custCase.BookingDatetime.ToString("HH:mm:ss");
            }
            svcReminder.MaxFUDealerDate = custCase.BookingDatetime.AddDays(30);
            svcReminder.CustomerName = custCase.CustomerName;
            svcReminder.CustomerPhoneNumber = custCase.Phone;
            svcReminder.CaseNumber = custCase.CaseNumber;
            svcReminder.TransactionType = 2;
            svcReminder.LastUpdateBy = "Salesforce";
            svcReminder.Status = status;

            try
            {
                var res = new ServiceReminderFacade(User1).Update(svcReminder);
                if (res < 1)
                    throw new System.ArgumentException("Update gagal");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void createServiceReminderFollowUp(CustomerCase custCase, ServiceReminder svcReminder, System.Security.Principal.IPrincipal _user, int status)
        {
            GenericPrincipal User1 = new System.Security.Principal.GenericPrincipal(new GenericIdentity("Salesforce"), null);

            ServiceReminderFollowUp svcReminderFU = new ServiceReminderFollowUp();
            svcReminderFU.ServiceReminder = svcReminder;
            svcReminderFU.FollowUpAction = custCase.Subject ?? "";
            svcReminderFU.FollowUpDate = DateTime.Now;
            svcReminderFU.CreatedBy = "Salesforce";
            svcReminderFU.FollowUpStatus = status;
            svcReminderFU.BookingDate = custCase.BookingDatetime;

            if (!string.IsNullOrEmpty(custCase.ReservationNumber))
                svcReminderFU.ServiceBooking = new ServiceBookingFacade(User1).Retrieve(custCase.ReservationNumber);

            try
            {
                var res = new ServiceReminderFollowUpFacade(User1).Insert(svcReminderFU);
                if (res < 1)
                    throw new System.ArgumentException("Insert Service Reminder baru gagal");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void processServiceReminder(CustomerCase custCase, System.Security.Principal.IPrincipal _user)
        {
            CriteriaComposite criterias;
            criterias = new CriteriaComposite(new Criteria(typeof(ServiceReminder), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ServiceReminder), "ChassisNumber", MatchType.Exact, custCase.ChassisNumber));
            criterias.opAnd(new Criteria(typeof(ServiceReminder), "Status", MatchType.InSet, "(1,2,4)"), "(", true);
            criterias.opOr(new Criteria(typeof(ServiceReminder), "Status", MatchType.Exact, 3), "(", true);
            criterias.opAnd(new Criteria(typeof(ServiceReminder), "ActualServiceDealer", MatchType.IsNull, true), "))", false);

            ServiceReminderFacade svcReminderFacade = new ServiceReminderFacade(_user);
            ArrayList arrSvcReminder = svcReminderFacade.Retrieve(criterias);
            try
            {
                if (arrSvcReminder.Count > 0)
                {
                    ServiceReminder svcReminder = (ServiceReminder)arrSvcReminder[0];
                    if (custCase.Dealer.ID == svcReminder.Dealer.ID)
                    {
                        updateSvcReminder(custCase, svcReminder, svcReminderFacade, _user);
                        createServiceReminderFollowUp(custCase, svcReminder, _user, 2);
                    }
                    else
                    {
                        ServiceReminder svcReminderToCreate = new ServiceReminder();
                        updateSvcReminder(custCase, svcReminder, svcReminderFacade, _user, 3);
                        createServiceReminderFollowUp(custCase, svcReminder, _user, 3);
                        createSvcReminder(custCase, svcReminderFacade, ref svcReminderToCreate, _user);
                        createServiceReminderFollowUp(custCase, svcReminderToCreate, _user, 1);
                    }
                }
                else
                {
                    ServiceReminder svcReminderToCreate = new ServiceReminder();
                    createSvcReminder(custCase, svcReminderFacade, ref svcReminderToCreate, _user);
                    createServiceReminderFollowUp(custCase, svcReminderToCreate, _user, 1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool validateChassisMaster(string chassisNumber, System.Security.Principal.IPrincipal _user)
        {
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, 0));
            criteria.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber));
            ArrayList res = new ChassisMasterFacade(_user).Retrieve(criteria);
            if (res.IsNull() || res.Count < 1)
                return false;
            else
                return true;
        }

        private DateTime getPKT(string chassisNumber, System.Security.Principal.IPrincipal _user, ref bool status)
        {
            var PKTDate = new DateTime();
            var pktDataSet = new ServiceReminderFacade(_user).RetrieveSp("exec sp_ServiceReminder_Get_PKT @ChassisNumber=" + chassisNumber);
            if (pktDataSet.Tables.Count > 0)
            {
                var pktTbl = pktDataSet.Tables[0];
                if (pktTbl.Rows[0]["PKTDate"] != DBNull.Value)
                {
                    PKTDate = Convert.ToDateTime(pktTbl.Rows[0]["PKTDate"]);
                    status = true;
                }
                else
                    status = false;
            }

            return PKTDate;
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
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

        private void SendEmail(CustomerCase customerCaseObj)
        {
            try
            {
                if (customerCaseObj.Dealer != null)
                {
                    string bodyTemplate = @"<FONT face=Arial size=1>
                                                <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                <tr>
                                                <td colspan=3 align=center><b>Customer Case data Tidak Naik Ke D-NET </b></td>
                                                </tr>
                                                <tr>
                                                <td colspan=3 height=50></td>
                                                </tr>
                                                <tr>
                                                <td colspan=3 height=50>
                                                Data {0} gagal masuk ke Customer Case data D-Net.
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

                    message.Subject = "Customer Case Data Fail";
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = string.Format(bodyTemplate, newJsonBody);
                    string SmtpAddress = WebConfigurationManager.AppSettings["SMTP"];
                    SmtpClient SmtpServer = new SmtpClient(SmtpAddress);
                    SmtpServer.Port = 25;
                    SmtpServer.Send(message);

                }
            }
            catch (Exception e)
            {
                string cuk = e.Message;
            }
        }

    }
}
