

#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceBookingBL class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Runtime.ExceptionServices;
using System.Configuration;
using System.Threading.Tasks;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.BusinessValidation;
using KTB.DNet.Interface.Framework;
#endregion


namespace KTB.DNet.Interface.BusinessLogic
{
    public class ServiceBookingBL : AbstractBusinessLogic, IServiceBookingBL
    {
        #region Variables
        private readonly IMapper _serviceBookingMapper;
        private readonly IMapper _vehicleModelMapper;
        private readonly IMapper _servicestandardTimeMapper;
        private readonly IMapper _vechileTypeMapper;
        private readonly IMapper _stallMasterMapper;
        private readonly IMapper _fsKindMapper;
        private readonly IMapper _pmKindMapper;
        private readonly IMapper _recalregKindMapper;
        private readonly IMapper _vWI_ServiceCostEstimationMapper;
        private readonly IMapper _chassisMasterMapper;
        private readonly IMapper _assistServiceIncomingMapper;
        private readonly IMapper _dealerMapper;
        private readonly IMapper _trTraineeMapper;
        private readonly IMapper _vWI_ServiceBookingMapper;
        private readonly IMapper _appConfigMapper;
        IDealerSuggestionServiceRepository<KTB.DNet.Interface.Domain.DealerSuggestionService, int> _dealerSuggestionServiceRepository;
        IGetServiceTypeRepository<KTB.DNet.Interface.Domain.GetServiceType, int> _getServiceTypeRepository;
        IServiceRecommendationRepository<KTB.DNet.Interface.Domain.ServiceRecommendation, int> _serviceRecommendationRepository;
        IServiceBookingRepository<KTB.DNet.Interface.Domain.ServiceBookingRealtimeAll_IF, int> _serviceBookingRepository;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private TransactionManager _transactionManager;

        #endregion

        #region Constructor
        public ServiceBookingBL(IDealerSuggestionServiceRepository<KTB.DNet.Interface.Domain.DealerSuggestionService, int> dealerSuggestionServiceRepository,
            IGetServiceTypeRepository<KTB.DNet.Interface.Domain.GetServiceType, int> getServiceTypeRepository,
            IServiceRecommendationRepository<KTB.DNet.Interface.Domain.ServiceRecommendation, int> serviceRecommendationRepository,
            IServiceBookingRepository<KTB.DNet.Interface.Domain.ServiceBookingRealtimeAll_IF, int> serviceBookingRepository)
        {
            _serviceBookingMapper = MapperFactory.GetInstance().GetMapper(typeof(ServiceBooking).ToString());
            _vehicleModelMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileModel).ToString());
            _servicestandardTimeMapper = MapperFactory.GetInstance().GetMapper(typeof(ServiceStandardTime).ToString());
            _vechileTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _stallMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(StallMaster).ToString());
            _fsKindMapper = MapperFactory.GetInstance().GetMapper(typeof(FSKind).ToString());
            _pmKindMapper = MapperFactory.GetInstance().GetMapper(typeof(PMKind).ToString());
            _recalregKindMapper = MapperFactory.GetInstance().GetMapper(typeof(RecallCategory).ToString());
            _vWI_ServiceCostEstimationMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_ServiceCostEstimation).ToString());
            _chassisMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            _assistServiceIncomingMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistServiceIncoming).ToString());
            _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            _trTraineeMapper = MapperFactory.GetInstance().GetMapper(typeof(TrTrainee).ToString());
            _dealerSuggestionServiceRepository = dealerSuggestionServiceRepository;
            _getServiceTypeRepository = getServiceTypeRepository;
            _serviceRecommendationRepository = serviceRecommendationRepository;
            _serviceBookingRepository = serviceBookingRepository;
            _vWI_ServiceBookingMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_ServiceBooking).ToString());
            _appConfigMapper = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        public ResponseBase<ServiceBookingDto> Create(ServiceBookingParameterDto objCreate)
        {
            #region Declare
            var result = new ResponseBase<ServiceBookingDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            ServiceBooking serviceBooking;
            ServiceBooking servicebooking = null;

            //var serviceBookingCode = string.Empty;
            List<ServiceBookingActivity> serviceBookingActivity = new List<ServiceBookingActivity>();

            #endregion

            try
            {
                //validate Service Booking
                isValid = ValidateServiceBooking(objCreate, validationResults, out serviceBooking);
                if (objCreate.servicebookingactivity != null)
                {
                    if (isValid) { ValidateServiceBookingActivity(objCreate, validationResults, serviceBookingActivity); }
                }

                //generate Service Booking Code
                //if (isValid)
                //{
                //    serviceBookingCode = GenerateServiceBookingCode(serviceBooking.Dealer.ID);
                //    if (string.IsNullOrEmpty(serviceBookingCode))
                //    {
                //        isValid = false;
                //    }
                //}

                // insert if valid
                if (isValid)
                {
                    TrTrainee PreferredSAData = serviceBooking.TrTrainee;

                    servicebooking = _mapper.Map<ServiceBooking>(objCreate);
                    servicebooking.Dealer = serviceBooking.Dealer;
                    servicebooking.ChassisMaster = serviceBooking.ChassisMaster;
                    servicebooking.VechileType = serviceBooking.VechileType;
                    servicebooking.VechileModel = serviceBooking.VechileModel;
                    servicebooking.StallMaster = serviceBooking.StallMaster;
                    servicebooking.LastUpdatedBy = DNetUserName;
                    servicebooking.WorkingTimeEnd = objCreate.WorkingTimeEnd;
                    servicebooking.WorkingTimeStart = objCreate.WorkingTimeStart;
                    servicebooking.StandardTime = serviceBooking.StandardTime;
                    servicebooking.TrTrainee = PreferredSAData;

                    var createdObject = InsertWithTransactionManager(servicebooking, serviceBookingActivity, validationResults);
                    if (createdObject != null)
                    {
                        #region Mapping Data Result
                        ServiceBooking obj = (ServiceBooking)_serviceBookingMapper.Retrieve(createdObject.ID);
                        //List<ServiceBookingActivity> act = obj.ServiceBookingActivities.OfType<ServiceBookingActivity>().ToList();
                        //ServiceBookingDto dataresult = obj.ConvertObject<ServiceBookingDto>();
                        //dataresult.DealerID = obj.Dealer.ID;
                        //dataresult.VechileTypeID = obj.VechileType == null ? 0 : obj.VechileType.ID;
                        //dataresult.StallMasterID = obj.StallMaster == null ? 0 : obj.StallMaster.ID;
                        //dataresult.servicebookingactivity = act.ConvertList<ServiceBookingActivity, ServiceBookingActivityDto>();
                        //foreach (ServiceBookingActivityDto item in dataresult.servicebookingactivity)
                        //{
                        //     item.ServiceBookingID = dataresult.ID;
                        //}
                        #endregion

                        if (obj != null)
                        {
                            result._id = createdObject.ID;
                            result.success = true;
                            result.total = 1;
                            //result.lst = dataresult;
                        }
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<ServiceBookingDto>(validationResults, null);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<ServiceBookingRealtimeDto> RealtimeCreate(ServiceBookingRealtimeParameterDto objCreate)
        {
            #region Declare

            var result = new ResponseBase<ServiceBookingRealtimeDto>();
            var validationResults = new List<DNetValidationResult>();
            var validResults = new List<ValidResult>();
            var isUpdate = false;
            ServiceBooking svcBooking = new ServiceBooking();
            List<ServiceBookingActivity> svcBookActivities = new List<ServiceBookingActivity>();

            #endregion

            try
            {
                if (!string.IsNullOrEmpty(objCreate.UserID))
                {
                    objCreate.Status = EnumStallMaster.StatusBooking.Request.ToString();
                    svcBooking = _serviceBookingMapper.RetrieveByCriteria(
                        Helper.GenerateCriteria(typeof(ServiceBooking), "ServiceBookingCode", "Status", objCreate.UserID, ((int)EnumStallMaster.StatusBooking.Request).ToString())).Cast<ServiceBooking>().SingleOrDefault();
                    if (svcBooking != null)
                    {
                        isUpdate = true;
                        if (svcBooking.Status != ((int)EnumStallMaster.StatusBooking.Request).ToString())
                        {
                            validationResults.Add(new DNetValidationResult("Data Service Booking gagal update karena status"));
                            return PopulateValidationError<ServiceBookingRealtimeDto>(validationResults, null);
                        }
                    }
                    else
                    {
                        svcBooking = new ServiceBooking { ServiceBookingCode = objCreate.UserID };
                    }
                }

                PopulateData(objCreate, ref svcBooking, ref svcBookActivities, isUpdate);

                if (ServiceBookingValidation.ValidateServiceBooking(ref validResults, ref svcBooking, ref svcBookActivities, isUpdate))
                {
                    if (isUpdate)
                    {
                        var updatedObject = UpdateWithTransactionManager(svcBooking, svcBookActivities);
                        if (updatedObject != null)
                        {
                            var obj = (ServiceBooking)_serviceBookingMapper.Retrieve(updatedObject.ID);

                            if (obj != null)
                            {
                                result._id = updatedObject.ID;
                                result.lst = _mapper.Map<ServiceBookingRealtimeDto>(obj);
                                result.success = true;
                                result.total = 1;
                            }
                        }
                        else
                        {
                            ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                        }
                    }
                    else
                    {
                        var createdObject = InsertWithTransactionManager(svcBooking, svcBookActivities, validationResults);
                        if (createdObject != null)
                        {
                            var obj = (ServiceBooking)_serviceBookingMapper.Retrieve(createdObject.ID);

                            if (obj != null)
                            {
                                result._id = createdObject.ID;
                                result.lst = _mapper.Map<ServiceBookingRealtimeDto>(obj);
                                result.success = true;
                                result.total = 1;
                            }
                        }
                        else
                        {
                            ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                        }
                    }
                }
                else
                {
                    validationResults = validResults.Select(item => new DNetValidationResult(item.Message)).ToList();
                    return PopulateValidationError<ServiceBookingRealtimeDto>(validationResults, null);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<ServiceBookingDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<ServiceBookingDto>> Read(ServiceBookingFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<GetServiceTypeDto>> GetServiceType(GetServiceTypeParameterDto objServiceType)
        {
            var result = new ResponseBase<List<GetServiceTypeDto>>();

            try
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(Dealer), "DealerCode", MatchType.Exact, objServiceType.DealerCode));
                var dealer = _dealerMapper.RetrieveByCriteria(criterias).Cast<Dealer>().SingleOrDefault();
                int dealerID = dealer == null ? 0 : dealer.ID;

                var data = _getServiceTypeRepository.GetServiceType(objServiceType.ChassisNumber, dealerID);

                if (data.Count > 0)
                {
                    var list = data.Cast<KTB.DNet.Interface.Domain.GetServiceType>().ToList();
                    var listData = list.Select(item => _mapper.Map<GetServiceTypeDto>(item)).ToList();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(KTB.DNet.Interface.Domain.GetServiceType), null, "ChassisNumber", objServiceType.ChassisNumber);
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<List<ServiceRecommendationDto>> ServiceRecommendation(ServiceRecommendationParameterDto objServiceRecommendation)
        {
            var result = new ResponseBase<List<ServiceRecommendationDto>>();

            try
            {
                int chassisMasterId = 0;

                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, objServiceRecommendation.ChassisNumber));
                criterias.opAnd(new Criteria(typeof(ChassisMaster), "Category.ID", MatchType.InSet, "(1,2)"));

                var objCM = _chassisMasterMapper.RetrieveByCriteria(criterias).Cast<ChassisMaster>().SingleOrDefault();
                if (objCM != null && objCM.VechileColor != null && objCM.VechileColor.VechileType != null)
                    chassisMasterId = objCM.ID;

                var data = _serviceRecommendationRepository.GetRecommendation(chassisMasterId, objServiceRecommendation.PhoneNumber);

                if (data.Count > 0)
                {
                    var list = data.Cast<KTB.DNet.Interface.Domain.ServiceRecommendation>().ToList();
                    var listData = list.Select(item => _mapper.Map<ServiceRecommendationDto>(item)).ToList();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(KTB.DNet.Interface.Domain.ServiceRecommendation), null, "ChassisNumber", objServiceRecommendation.ChassisNumber);
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<List<ServiceBookingRealtimeReadDto>> RealtimeRead(ServiceBookingFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_ServiceBooking), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            if (DealerCode.All(Char.IsNumber))
            {
                criterias.opAnd(new Criteria(typeof(VWI_ServiceBooking), "DealerCode", MatchType.Exact, DealerCode));
                criterias.opAnd(new Criteria(typeof(VWI_ServiceBooking), "Status", MatchType.InSet, string.Format("('{0}', '{1}')",
                    EnumStallMaster.StatusBooking.Booked, EnumStallMaster.StatusBooking.Batal)));
            }

            var result = new ResponseBase<List<ServiceBookingRealtimeReadDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_ServiceBooking), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_ServiceBooking), filterDto, sortColl);

                // get data
                var data = _vWI_ServiceBookingMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_ServiceBooking>().ToList();
                    List<ServiceBookingRealtimeReadDto> listData = list.Select(item => _mapper.Map<ServiceBookingRealtimeReadDto>(item)).ToList();
                    decimal total = 0;

                    foreach (var svc in listData)
                    {
                        total = 0;
                        foreach (var act in svc.ServiceBookingActivities)
                        {
                            if (act.EstimationCosts != null)
                            {
                                foreach (var est in act.EstimationCosts)
                                {
                                    total += est.JasaService;
                                    if (est.Details != null)
                                    {
                                        foreach (var det in est.Details)
                                        {
                                            total += det.SubTotal;
                                        }
                                    }
                                    else
                                        est.Details = new List<ServiceCostEstimationDetailDto>();
                                }
                            }
                            else
                                act.EstimationCosts = new List<ServiceCostEstimationSummaryDto>();
                        }

                        svc.Total = total;
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceBooking), filterDto);
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<ServiceBookingDto> Update(ServiceBookingParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }
        public ResponseBase<ServiceBookingDto> Update(ServiceBookingUpdateParameterDto objUpdate)
        {
            #region declare
            var result = new ResponseBase<ServiceBookingDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            ServiceBooking serviceBooking;
            ServiceBooking serviceBookingExist = null;
            #endregion

            try
            {

                //Validate serviceBooking
                isValid = ValidateServiceBookingUpdate(objUpdate, validationResults, out serviceBooking);

                //Data is Exist 
                if (isValid)
                {
                    var criterias = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias.opAnd(new Criteria(typeof(ServiceBooking), "ID", MatchType.Exact, objUpdate.ID));

                    var data = _serviceBookingMapper.RetrieveByCriteria(criterias);
                    if (data.Count == 0)
                    {
                        isValid = false;
                        //result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.ID) });
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.ID)));
                    }
                    else
                    {
                        serviceBookingExist = (ServiceBooking)data[0];
                        //validate status 
                        if (!string.IsNullOrEmpty(objUpdate.Status))
                        {
                            //if (objUpdate.Status != _enumBL.GetByCategoryAndCode("ServiceBooking.Status", "Batal").ValueId.ToString())
                            //{
                            //    isValid = false;
                            //    validationResults.Add(new DNetValidationResult("Service Booking hanya dapat diupdate dengan status Batal (0)"));
                            //}
                            //if (serviceBookingExist.Status != _enumBL.GetByCategoryAndCode("ServiceBooking.Status", "Booked").ValueId.ToString())
                            //{
                            //    isValid = false;
                            //    validationResults.Add(new DNetValidationResult("Service Booking tidak dapat di update karena status saat ini adalah " + serviceBookingExist.Status));
                            //}
                            if (serviceBookingExist.Status == _enumBL.GetByCategoryAndCode("ServiceBooking.Status", "Batal").ValueId.ToString())
                            {
                                isValid = false;
                                validationResults.Add(new DNetValidationResult("Service Booking tidak dapat di update karena status saat ini adalah Batal (" + serviceBookingExist.Status + ")"));
                            }
                        }
                        //remove validation karena ada possibility kolom lain saat CR RealTimeService selain status
                        //else
                        //{
                        //    if(serviceBookingExist.Status==objUpdate.Status)
                        //    {
                        //        isValid = false;
                        //        validationResults.Add(new DNetValidationResult("Status Service Booking yang dikirim sama dengan Status saat ini"));
                        //    }
                        //}
                    }
                }

                // if valid and data is exist then update data
                if (isValid)
                {
                    var newServiceBooking = _mapper.Map<ServiceBooking>(serviceBookingExist);
                    newServiceBooking.CustomerName = string.IsNullOrEmpty(objUpdate.CustomerName) ? serviceBookingExist.CustomerName : serviceBooking.CustomerName;
                    newServiceBooking.CustomerPhoneNumber = string.IsNullOrEmpty(objUpdate.CustomerPhoneNumber) ? serviceBookingExist.CustomerPhoneNumber : serviceBooking.CustomerPhoneNumber;
                    newServiceBooking.Notes = string.IsNullOrEmpty(objUpdate.Notes) ? serviceBookingExist.Notes : serviceBooking.Notes;
                    newServiceBooking.ChassisMaster = string.IsNullOrEmpty(objUpdate.ChassisNumber) ? serviceBookingExist.ChassisMaster : serviceBooking.ChassisMaster;
                    newServiceBooking.ChassisNumber = string.IsNullOrEmpty(objUpdate.ChassisNumber) ? serviceBookingExist.ChassisNumber : serviceBooking.ChassisNumber;
                    newServiceBooking.PlateNumber = string.IsNullOrEmpty(objUpdate.PlateNumber) ? serviceBookingExist.PlateNumber : serviceBooking.PlateNumber;
                    newServiceBooking.VechileType = string.IsNullOrEmpty(objUpdate.VechileTypeCode) ? serviceBookingExist.VechileType : serviceBooking.VechileType;
                    newServiceBooking.VechileModel = string.IsNullOrEmpty(objUpdate.VechileTypeCode) ? serviceBookingExist.VechileModel : serviceBooking.VechileModel;
                    newServiceBooking.StallMaster = string.IsNullOrEmpty(objUpdate.StallMasterCode) ? serviceBookingExist.StallMaster : serviceBooking.StallMaster;
                    newServiceBooking.PickupType = string.IsNullOrEmpty(objUpdate.PickupType) ? serviceBookingExist.PickupType : serviceBooking.PickupType;
                    newServiceBooking.StallServiceType = string.IsNullOrEmpty(objUpdate.StallServiceType) ? serviceBookingExist.StallServiceType : serviceBooking.StallServiceType;
                    newServiceBooking.OdoMeter = string.IsNullOrEmpty(objUpdate.Odometer) ? serviceBookingExist.OdoMeter : serviceBooking.OdoMeter;
                    newServiceBooking.WorkingTimeStart = objUpdate.WorkingTimeStart == DateTime.MinValue ? serviceBookingExist.WorkingTimeStart : serviceBooking.WorkingTimeStart;
                    newServiceBooking.WorkingTimeEnd = objUpdate.WorkingTimeEnd == DateTime.MinValue ? serviceBookingExist.WorkingTimeEnd : serviceBooking.WorkingTimeEnd;
                    newServiceBooking.Status = string.IsNullOrEmpty(objUpdate.Status) ? serviceBookingExist.Status : serviceBooking.Status;
                    newServiceBooking.LastUpdatedBy = DNetUserName;
                    newServiceBooking.LastUpdatedTime = DateTime.Now;

                    var success = (int)_serviceBookingMapper.Update(newServiceBooking, DNetUserName);

                    #region Mapping Data Result
                    ServiceBooking obj = (ServiceBooking)_serviceBookingMapper.Retrieve(newServiceBooking.ID);
                    //List<ServiceBookingActivity> act = obj.ServiceBookingActivities.OfType<ServiceBookingActivity>().ToList();
                    //ServiceBookingDto dataresult = obj.ConvertObject<ServiceBookingDto>();
                    //dataresult.DealerID = obj.Dealer.ID;
                    //dataresult.VechileTypeID = obj.VechileType == null ? 0 : obj.VechileType.ID;
                    //dataresult.StallMasterID = obj.StallMaster == null ? 0 : obj.StallMaster.ID;
                    //dataresult.servicebookingactivity = act.ConvertList<ServiceBookingActivity, ServiceBookingActivityDto>();
                    //foreach (ServiceBookingActivityDto item in dataresult.servicebookingactivity)
                    //{
                    //    item.ServiceBookingID = dataresult.ID;
                    //}
                    #endregion

                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    // return output ID
                    result._id = objUpdate.ID;
                    result.total = 1;
                    //result.lst = dataresult;
                }
                else
                {
                    return PopulateValidationError<ServiceBookingDto>(validationResults, null);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<ServiceBookingRealtimeDto> RealtimeUpdate(ServiceBookingRealtimeParameterDto objUpdate)
        {
            #region Declare

            var result = new ResponseBase<ServiceBookingRealtimeDto>();
            var validationResults = new List<DNetValidationResult>();
            var validResults = new List<ValidResult>();
            int currDealer;

            #endregion

            try
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(ServiceBooking), "ServiceBookingCode", MatchType.Exact, string.IsNullOrEmpty(objUpdate.UserID) ? objUpdate.ServiceBookingCode : objUpdate.UserID));
                criterias.opAnd(new Criteria(typeof(ServiceBooking), "Status", MatchType.No, ((int)EnumStallMaster.StatusBooking.Batal).ToString()));

                ServiceBooking svcBooking = _serviceBookingMapper.RetrieveByCriteria(criterias).Cast<ServiceBooking>().SingleOrDefault();
                List<ServiceBookingActivity> svcBookActivities = new List<ServiceBookingActivity>();
                if (svcBooking != null)
                {
                    //if (svcBooking.Status != ((int)EnumStallMaster.StatusBooking.Request).ToString())
                    //{
                    //    validationResults.Add(new DNetValidationResult("Data Service Booking gagal update karena status"));
                    //    return PopulateValidationError<ServiceBookingRealtimeDto>(validationResults, null);
                    //}

                    currDealer = svcBooking.Dealer.ID;
                    PopulateData(objUpdate, ref svcBooking, ref svcBookActivities, true);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Data Service Booking tidak ditemukan"));
                    return PopulateValidationError<ServiceBookingRealtimeDto>(validationResults, null);
                }

                if (ServiceBookingValidation.ValidateServiceBooking(ref validResults, ref svcBooking, ref svcBookActivities, true))
                {
                    if (currDealer != svcBooking.Dealer.ID || !string.IsNullOrEmpty(objUpdate.UserID))
                        svcBooking.ServiceBookingCode = "";

                    var updatedObject = UpdateWithTransactionManager(svcBooking, svcBookActivities);
                    if (updatedObject != null)
                    {
                        var obj = (ServiceBooking)_serviceBookingMapper.Retrieve(updatedObject.ID);

                        if (obj != null)
                        {
                            result._id = updatedObject.ID;
                            result.lst = _mapper.Map<ServiceBookingRealtimeDto>(obj);
                            result.success = true;
                            result.total = 1;
                        }
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    validationResults = validResults.Select(item => new DNetValidationResult(item.Message)).ToList();
                    return PopulateValidationError<ServiceBookingRealtimeDto>(validationResults, null);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<VWI_ServiceCostEstimationDto> EstimationCost(VWI_ServiceCostEstimationParameterDto objEstimationCost)
        {
            string varian = string.Empty, vechileTypeCode = string.Empty;
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, objEstimationCost.ChassisNumber));
            var objCM = _chassisMasterMapper.RetrieveByCriteria(criterias).Cast<ChassisMaster>().SingleOrDefault();
            if (objCM != null && objCM.VechileColor != null && objCM.VechileColor.VechileType != null)
            {
                vechileTypeCode = objCM.VechileColor.VechileType.VechileTypeCode;
                varian = vechileTypeCode.Substring(0, 2);
            }
            else
            {
                DataTable dt = _chassisMasterMapper.RetrieveDataSet(string.Format("EXEC sp_GetVTRealtimeService '{0}', '{1}'", objEstimationCost.VechileModel, objEstimationCost.VariantType)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    vechileTypeCode = dt.Rows[0]["VechileTypeCode"].ToString();
                    varian = vechileTypeCode.Substring(0, 2);
                }
            }

            objEstimationCost.Parameters.ForEach(d =>
            {
                d.ServiceTypeID = _enumBL.GetByCategoryAndCode("ServiceBooking.ServiceType", d.ServiceTypeCode).ValueId;
            });

            criterias = new CriteriaComposite(new Criteria(typeof(VWI_ServiceCostEstimation), "ID", MatchType.Greater, 0));
            criterias.opAnd(new Criteria(typeof(VWI_ServiceCostEstimation), "DealerCode", MatchType.Exact, objEstimationCost.DealerCode));
            criterias.opAnd(new Criteria(typeof(VWI_ServiceCostEstimation), "VechileTypeCode", MatchType.Exact, vechileTypeCode));
            criterias.opAnd(new Criteria(typeof(VWI_ServiceCostEstimation), "ServiceType", MatchType.InSet,
                string.Format("({0})", string.Join(",", objEstimationCost.Parameters.Select(s => s.ServiceTypeID)))));
            criterias.opAnd(new Criteria(typeof(VWI_ServiceCostEstimation), "KindCode", MatchType.InSet,
                string.Format("({0})", string.Join(",", objEstimationCost.Parameters.Select(s => new { KindCode = string.Format("'{0}'", s.KindCode) }).Select(s => s.KindCode)))));
            criterias.opAnd(new Criteria(typeof(VWI_ServiceCostEstimation), "Varian", MatchType.Exact, varian));

            var result = new ResponseBase<VWI_ServiceCostEstimationDto>();
            var sortColl = new SortCollection();

            try
            {
                // get data
                var data = _vWI_ServiceCostEstimationMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_ServiceCostEstimation>().ToList();
                    VWI_ServiceCostEstimationDto listData = new VWI_ServiceCostEstimationDto
                    {
                        Summaries = list.Select(item => _mapper.Map<VWI_ServiceCostEstimationSummaryDto>(item)).ToList()
                    };

                    decimal total = 0;
                    listData.Summaries.ForEach(d =>
                    {
                        total += d.JasaService + d.Details.Sum(s => s.SubTotal);
                    });
                    listData.Total = total;

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceCostEstimation), null, "KindCode",
                        string.Join(", ", objEstimationCost.Parameters.Select(s => s.KindCode)));
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public async Task<ResponseBase<List<DealerSuggestionServiceDto>>> DealerSuggestion(DealerSuggestionServiceParameterDto objDealerSuggestionService)
        {
            string lastServiceDealer = string.Empty;
            int vechileModelID = 0, vechileTypeID = 0;
            var result = new ResponseBase<List<DealerSuggestionServiceDto>>();

            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "TimeServiceBookingCutOff"));
            var timeCutOff = _appConfigMapper.RetrieveByCriteria(criterias).Cast<AppConfig>().SingleOrDefault();
            if (timeCutOff != null)
            {
                DateTime tgl = Convert.ToDateTime(timeCutOff.Value);
                DateTime currTime = DateTime.Now;
                if (TimeSpan.Compare(tgl.TimeOfDay, currTime.TimeOfDay) > 0)
                    tgl = DateTime.Now;
                else
                    tgl = DateTime.Now.AddDays(1);

                if (DateTime.Compare(objDealerSuggestionService.RequestedDate, tgl.Date) < 1)
                {
                    ErrorMsgHelper.Exception(result.messages, string.Format("Silahkan melakukan booking dari tanggal {0}", tgl.AddDays(1).Date.ToString("dd-MM-yyyy")));
                    return result;
                }
            }

            criterias = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, objDealerSuggestionService.ChassisNumber));
            criterias.opAnd(new Criteria(typeof(ChassisMaster), "Category.ID", MatchType.InSet, "(1,2)"));

            var objCM = _chassisMasterMapper.RetrieveByCriteria(criterias).Cast<ChassisMaster>().SingleOrDefault();
            if (objCM != null && objCM.VechileColor != null && objCM.VechileColor.VechileType != null)
            {
                vechileModelID = objCM.VechileColor.VechileType.VechileModel.ID;
                vechileTypeID = objCM.VechileColor.VechileType.ID;

                criterias = new CriteriaComposite(new Criteria(typeof(AssistServiceIncoming), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(AssistServiceIncoming), "KodeChassis", MatchType.Exact, objDealerSuggestionService.ChassisNumber));
                var objSIU = _assistServiceIncomingMapper.RetrieveByCriteria(criterias).Cast<AssistServiceIncoming>().OrderByDescending(o => o.ID).FirstOrDefault();
                if (objSIU != null)
                    lastServiceDealer = objSIU.DealerCode;
            }
            else
            {
                DataTable dt = _chassisMasterMapper.RetrieveDataSet(string.Format("EXEC sp_GetVTRealtimeService '{0}', '{1}'", objDealerSuggestionService.VechileModel, objDealerSuggestionService.VariantType)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    vechileTypeID = Convert.ToInt32(dt.Rows[0]["VechileTypeID"].ToString());
                    vechileModelID = Convert.ToInt32(dt.Rows[0]["VechileModelID"].ToString());
                }
            }

            if (vechileModelID == 0 || vechileTypeID == 0)
            {
                ErrorMsgHelper.Exception(result.messages, "Model kendaraan atau Variant kendaraan atau Nomor Chassis tidak valid");
                return result;
            }

            objDealerSuggestionService.AssistServiceTypeCode = _enumBL.IsExistByCategoryAndCode("ServiceBooking.StallServiceType", objDealerSuggestionService.AssistServiceTypeCode) ?
                objDealerSuggestionService.AssistServiceTypeCode : _enumBL.GetByCategoryAndValue("ServiceBooking.StallServiceType", "1").ValueCode;

            objDealerSuggestionService.ServiceTypes.ForEach(d =>
            {
                d.ServiceTypeID = _enumBL.GetByCategoryAndCode("ServiceBooking.ServiceType", d.ServiceTypeCode).ValueId;
                d.VechileModelID = vechileModelID;
                d.VechileTypeID = vechileTypeID;
                d.AssistServiceTypeCode = objDealerSuggestionService.AssistServiceTypeCode;
            });

            short pickupType = (short)_enumBL.GetByCategoryAndCode("ServiceBooking.PickupType", objDealerSuggestionService.PickupType).ValueId;

            try
            {
                var data = await _dealerSuggestionServiceRepository.GetSuggestionDealerAsync(lastServiceDealer, objDealerSuggestionService.FavDealers,objDealerSuggestionService.SelectedDealers,
                    objDealerSuggestionService.CustomerLatitude, objDealerSuggestionService.CustomerLongitude, objDealerSuggestionService.RequestedDate,
                    pickupType, objDealerSuggestionService.ServiceTypes, objDealerSuggestionService.CheckinTime,
                    objDealerSuggestionService.CheckoutTime, objDealerSuggestionService.RequestTime, objDealerSuggestionService.ServiceBookingCode);

                if (data.Count > 0)
                {
                    var list = data.Cast<KTB.DNet.Interface.Domain.DealerSuggestionService>().ToList();
                    var listData = list.Select(item => _mapper.Map<DealerSuggestionServiceDto>(item)).ToList();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(KTB.DNet.Interface.Domain.DealerSuggestionService), null, "ChassisNumber", objDealerSuggestionService.ChassisNumber);

                    criterias = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Exact, "MsgErrorGeneral.API.DealerSuggestion"));
                    var msgErr = _appConfigMapper.RetrieveByCriteria(criterias).Cast<AppConfig>().SingleOrDefault();
                    if (msgErr != null)
                    {
                        result.messages.ForEach(d =>
                        {
                            d.ErrorMessage += string.Concat(" ", msgErr.Value);
                        });
                    }
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public ResponseBase<List<ServiceBookingRealtimeReadDto>> RealtimeAll(ServiceBookingFilterDto filterDto, int pageSize)
        {
            #region with mapper
            //var criterias = new CriteriaComposite(new Criteria(typeof(VWI_ServiceBooking), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            //criterias.opAnd(new Criteria(typeof(VWI_ServiceBooking), "Status", MatchType.InSet, string.Format("('{0}', '{1}')",
            //        EnumStallMaster.StatusBooking.Booked, EnumStallMaster.StatusBooking.Batal)));

            //var result = new ResponseBase<List<ServiceBookingRealtimeReadDto>>();
            //var sortColl = new SortCollection();
            //var totalRow = 0;

            //try
            //{
            //    // populate the criterias
            //    criterias = Helper.UpdateCriteria(typeof(VWI_ServiceBooking), filterDto, criterias);

            //    // populate the sort info
            //    sortColl = Helper.UpdateSortColumn(typeof(VWI_ServiceBooking), filterDto, sortColl);

            //    // get data
            //    var data = _vWI_ServiceBookingMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
            //    if (data.Count > 0)
            //    {
            //        var list = data.Cast<VWI_ServiceBooking>().ToList();
            //        List<ServiceBookingRealtimeReadDto> listData = list.Select(item => _mapper.Map<ServiceBookingRealtimeReadDto>(item)).ToList();
            //        decimal total = 0;

            //        foreach (var svc in listData)
            //        {
            //            total = 0;
            //            foreach (var act in svc.ServiceBookingActivities)
            //            {
            //                if (act.EstimationCosts != null)
            //                {
            //                    foreach (var est in act.EstimationCosts)
            //                    {
            //                        total += est.JasaService;
            //                        if (est.Details != null)
            //                        {
            //                            foreach (var det in est.Details)
            //                            {
            //                                total += det.SubTotal;
            //                            }
            //                        }
            //                        else
            //                            est.Details = new List<ServiceCostEstimationDetailDto>();
            //                    }
            //                }
            //                else
            //                    act.EstimationCosts = new List<ServiceCostEstimationSummaryDto>();
            //            }

            //            svc.Total = total;
            //        }

            //        result.lst = listData;
            //        result.total = totalRow;
            //    }
            //    else
            //    {
            //        ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceBooking), filterDto);
            //    }

            //    result.success = true;

            //}
            //catch (SqlException ex)
            //{
            //    ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    ErrorMsgHelper.Exception(result.messages, ex.Message);
            //}

            //return result;
            #endregion

            #region with dapper

            var result = new ResponseBase<List<ServiceBookingRealtimeReadDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            var innerQueryCriteria = string.Empty;
            var criterias = Helper.InitialStrCriteria(typeof(KTB.DNet.Interface.Domain.ServiceBookingRealtimeAll_IF), filterDto);

            sortColl = Helper.UpdateSortColumnDapper(typeof(KTB.DNet.Interface.Domain.ServiceBookingRealtimeAll_IF), filterDto);

            try
            {
                List<KTB.DNet.Interface.Domain.ServiceBookingRealtimeAll_IF> data = _serviceBookingRepository.SearchRealTimeAll(
                                criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    List<ServiceBookingRealtimeReadDto> listData = data.Select(item => _mapper.Map<ServiceBookingRealtimeReadDto>(item)).ToList();
                    decimal total = 0;

                    foreach (var svc in listData)
                    {
                        total = 0;
                        foreach (var act in svc.ServiceBookingActivities)
                        {
                            if (act.EstimationCosts != null)
                            {
                                foreach (var est in act.EstimationCosts)
                                {
                                    total += est.JasaService;
                                    if (est.Details != null)
                                    {
                                        foreach (var det in est.Details)
                                        {
                                            total += det.SubTotal;
                                        }
                                    }
                                    else
                                        est.Details = new List<ServiceCostEstimationDetailDto>();
                                }
                            }
                            else
                                act.EstimationCosts = new List<ServiceCostEstimationSummaryDto>();
                        }
                        svc.Total = total;
                    }

                    result.lst = listData;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(KTB.DNet.Interface.Domain.ServiceBookingRealtimeAll_IF), filterDto);
                }

                result.success = true;
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }
            return result;

            #endregion
        }

        #endregion

        #region Private Methods
        private bool ValidateServiceBooking(ServiceBookingParameterDto objCreate, List<DNetValidationResult> validationResults, out ServiceBooking serviceBooking)
        {
            bool isValid = true;
            serviceBooking = _mapper.Map<ServiceBooking>(objCreate);
            var DealerID = 0;
            var KindCode = string.Empty;
            var KindID = string.Empty;
            var servicetypeid = string.Empty;
            var assistservicetypecode = string.Empty;

            //GET KINDCODE 
            if (objCreate.servicebookingactivity != null)
            {
                foreach (ServiceBookingActivityParameterDto item in objCreate.servicebookingactivity)
                {

                    if (string.IsNullOrEmpty(KindCode) && !string.IsNullOrEmpty(item.KindCode))
                    {
                        KindCode = item.KindCode;
                        var servicetype = _enumBL.GetByCategoryAndValue("ServiceBooking.ServiceType", item.ServiceTypeID.ToString());
                        if (servicetype.ValueCode == "FS")
                        {
                            var fskind = _fsKindMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(FSKind), "RowStatus", "KindCode", (short)DBRowStatus.Active, item.KindCode));
                            if (fskind.Count > 0)
                            {
                                var fskindresult = fskind[0] as FSKind;
                                KindID = fskindresult.ID.ToString();
                            }
                        }
                        else if (servicetype.ValueCode == "PM")
                        {
                            var pmkind = _pmKindMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(PMKind), "RowStatus", "KindCode", (short)DBRowStatus.Active, item.KindCode));
                            if (pmkind.Count > 0)
                            {
                                var pmkindresult = pmkind[0] as PMKind;
                                KindID = pmkindresult.ID.ToString();
                            }
                        }
                        else if (servicetype.ValueCode == "FF")
                        {
                            var recallregcategory = _recalregKindMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(RecallCategory), "RowStatus", "RecallRegNo", (short)DBRowStatus.Active, item.KindCode));
                            if (recallregcategory.Count > 0)
                            {
                                var recallregcategoryresult = recallregcategory[0] as RecallCategory;
                                KindID = recallregcategoryresult.ID.ToString();
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(servicetypeid) && !string.IsNullOrEmpty(item.ServiceTypeID))
                    {
                        servicetypeid = item.ServiceTypeID;
                    }
                }
            }

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                serviceBooking.Dealer = dealer;
                DealerID = dealer.ID;
            }

            //Validate Duplicate
            /*
            if (objCreate.ServiceBookingCode != "" && objCreate.ServiceBookingCode != null)
            {
                var criteriaSvcBoking = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "ServiceBookingCode", MatchType.Exact, objCreate.ServiceBookingCode));
                //criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));
                criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "Dealer.DealerCode", MatchType.Exact, objCreate.DealerCode));
                var existingSvcBooking = _serviceBookingMapper.RetrieveByCriteria(criteriaSvcBoking);
                if (existingSvcBooking.Count > 0)
                {
                    validationResults.Add(new DNetValidationResult("Service Booking Code " + objCreate.ServiceBookingCode.ToString() + " untuk dealer " + objCreate.DealerCode + " sudah ada di database."));
                }
            }
            */

            //Validate Duplicate by StallMasterCode & WorkingTimeStart/End
            if (objCreate.WorkingTimeStart.ToString("yyyy-MM-dd") != "1753-01-01"
                && objCreate.WorkingTimeEnd.ToString("yyyy-MM-dd") != "1753-01-01"
                && objCreate.WorkingTimeStart != DateTime.MinValue
                && objCreate.WorkingTimeEnd != DateTime.MinValue)
            {
                if (objCreate.StallMasterCode != "" && objCreate.StallMasterCode != null)
                {
                    var criteriaSvcBoking = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "StallMaster.StallCode", MatchType.Exact, objCreate.StallMasterCode));
                    criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "Status", MatchType.InSet, "('1', '2')")); //Status Booked & Request
                    criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "Dealer.DealerCode", MatchType.Exact, objCreate.DealerCode));
                    criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "WorkingTimeStart", MatchType.GreaterOrEqual, objCreate.WorkingTimeStart.ToString("yyyy-MM-dd")));
                    criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "WorkingTimeEnd", MatchType.Lesser, objCreate.WorkingTimeEnd.AddDays(1).ToString("yyyy-MM-dd")));
                    //criteriaSvcBoking.opOr(new Criteria(typeof(ServiceBooking), "WorkingTimeEnd", MatchType.GreaterOrEqual, objCreate.WorkingTimeStart));
                    //criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "WorkingTimeEnd", MatchType.LesserOrEqual, objCreate.WorkingTimeEnd));

                    var existingSvcBooking = _serviceBookingMapper.RetrieveByCriteria(criteriaSvcBoking);
                    if (existingSvcBooking.Count > 0)
                    {
                        string[] arrStatus = { "1", "2" };
                        bool isStatusBooked = false;
                        bool isValidData = true;
                        foreach (ServiceBooking item in existingSvcBooking)
                        {
                            if (arrStatus.Contains(item.Status) && item.RowStatus == 0 && item.StallMaster.StallCode == objCreate.StallMasterCode && item.Dealer.DealerCode == objCreate.DealerCode)
                            {
                                if (item.Status == "1")
                                    isStatusBooked = true;

                                if (objCreate.WorkingTimeStart < item.WorkingTimeEnd && item.WorkingTimeStart < objCreate.WorkingTimeEnd)
                                {
                                    isValidData = false;
                                }
                                else if (objCreate.WorkingTimeEnd > item.WorkingTimeStart && item.WorkingTimeEnd > objCreate.WorkingTimeStart)
                                {
                                    isValidData = false;
                                }
                            }

                            if (!isValidData)
                                break;
                        }

                        if (!isValidData)
                        {
                            if (isStatusBooked)
                                validationResults.Add(new DNetValidationResult("Data dengan StallMasterCode " + objCreate.StallMasterCode.ToString() + " di rentang WorkingTimeStart '" + objCreate.WorkingTimeStart + "' dan '" + objCreate.WorkingTimeEnd + "' untuk dealer " + objCreate.DealerCode + " sudah ada di database."));
                            else if (!isStatusBooked)
                                validationResults.Add(new DNetValidationResult("Data dengan StallMasterCode " + objCreate.StallMasterCode.ToString() + " di rentang WorkingTimeStart '" + objCreate.WorkingTimeStart + "' dan '" + objCreate.WorkingTimeEnd + "' untuk dealer " + objCreate.DealerCode + " sedang dalam proses booking pada MMID."));
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(objCreate.ServiceBookingCode))
                {
                    var criteriaSvcBoking = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "ServiceBookingCode", MatchType.Exact, objCreate.ServiceBookingCode));
                    //criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));
                    criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "Dealer.DealerCode", MatchType.Exact, objCreate.DealerCode));
                    var existingSvcBooking = _serviceBookingMapper.RetrieveByCriteria(criteriaSvcBoking);
                    if (existingSvcBooking.Count > 0)
                    {
                        validationResults.Add(new DNetValidationResult("Service Booking Code " + objCreate.ServiceBookingCode.ToString() + " untuk dealer " + objCreate.DealerCode + " sudah ada di database."));
                    }
                }
            }

            // Validate Stall Service Type
            var stallserviceType = _enumBL.GetByCategoryAndValue("ServiceBooking.StallServiceType", objCreate.StallServiceType.ToString());
            if (stallserviceType != null)
            {
                serviceBooking.StallServiceType = Convert.ToInt16(stallserviceType.ValueId);
                assistservicetypecode = stallserviceType.ValueCode;
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Service Type " + objCreate.StallServiceType.ToString() + " tidak valid"));
            }

            // Validate Chassis Master
            ChassisMaster chassis = null;
            if (!string.IsNullOrEmpty(objCreate.ChassisNumber))
            {
                if (ValidateChassisMaster(objCreate.ChassisNumber, validationResults, ref chassis))
                {
                    serviceBooking.ChassisMaster = chassis;
                }
            }

            //Validate Duplicate
            if (objCreate.ServiceBookingCode != "" && objCreate.ServiceBookingCode != null)
            {
                var criteriaSvcBoking = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "ServiceBookingCode", MatchType.Exact, objCreate.ServiceBookingCode));
                criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));
                criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "Dealer.DealerCode", MatchType.Exact, objCreate.DealerCode));
                var existingSvcBooking = _serviceBookingMapper.RetrieveByCriteria(criteriaSvcBoking);
                if (existingSvcBooking.Count > 0)
                {
                    validationResults.Add(new DNetValidationResult("Service Booking Code " + objCreate.ServiceBookingCode.ToString() + " untuk dealer " + objCreate.DealerCode + " sudah ada di database."));
                }
            }

            // Validate Vehicle Type
            if (Convert.ToInt16(objCreate.IsMitsubishi) == 0)
            {
                serviceBooking.VechileType = null;
                serviceBooking.VechileModel = null;
                serviceBooking.StandardTime = 0;
                serviceBooking.ChassisMaster = null;
            }
            else
            {
                if (!string.IsNullOrEmpty(objCreate.VechileTypeCode))
                {
                    var vechiletype = _vechileTypeMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(VechileType), "RowStatus", "VechileTypeCode", (short)DBRowStatus.Active, objCreate.VechileTypeCode));
                    if (vechiletype.Count > 0)
                    {
                        var vechileTypeSB = vechiletype[0] as VechileType;
                        serviceBooking.VechileType = vechileTypeSB;

                        // Validate Vehicle Model
                        var vehiclemodel = _vehicleModelMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(VechileModel), "RowStatus", "ID", (short)DBRowStatus.Active, vechileTypeSB.VechileModel.ID));
                        if (vehiclemodel.Count > 0)
                        {
                            var vehiclemodelSB = vehiclemodel[0] as VechileModel;
                            serviceBooking.VechileModel = vehiclemodelSB;
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult("Vehicle Model dengan Vehilce Type Code" + objCreate.VechileTypeCode.ToString() + " tidak ditemukan"));
                        }

                        //Get Standard Time 
                        var criteriasstandardtime = new CriteriaComposite(new Criteria(typeof(ServiceStandardTime), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        criteriasstandardtime.opAnd(new Criteria(typeof(ServiceStandardTime), "VechileType.ID", MatchType.Exact, serviceBooking.VechileType.ID));
                        criteriasstandardtime.opAnd(new Criteria(typeof(ServiceStandardTime), "ServiceTypeID", MatchType.Exact, servicetypeid));
                        criteriasstandardtime.opAnd(new Criteria(typeof(ServiceStandardTime), "Dealer.ID", MatchType.Exact, DealerID));
                        criteriasstandardtime.opAnd(new Criteria(typeof(ServiceStandardTime), "KindCode", MatchType.Exact, KindCode));
                        criteriasstandardtime.opAnd(new Criteria(typeof(ServiceStandardTime), "AssistServiceTypeCode", MatchType.Exact, assistservicetypecode));

                        var datastandardtime = _servicestandardTimeMapper.RetrieveByCriteria(criteriasstandardtime);
                        if (datastandardtime.Count > 0)
                        {
                            var servicestandardtime = datastandardtime[0] as ServiceStandardTime;
                            serviceBooking.StandardTime = servicestandardtime.SystemStandardTime;
                        }
                        else
                        {
                            serviceBooking.StandardTime = 0;
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult("Vehicle Type Code " + objCreate.VechileTypeCode.ToString() + " tidak ditemukan"));
                    }
                }
            }


            // Validate Stall Master
            var stallmaster = _stallMasterMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StallMaster), "RowStatus", "StallCode", (short)DBRowStatus.Active, objCreate.StallMasterCode));
            if (stallmaster.Count > 0)
            {
                var stallmasterSB = stallmaster[0] as StallMaster;
                serviceBooking.StallMaster = stallmasterSB;
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Code " + objCreate.StallMasterCode.ToString() + " tidak ditemukan"));
            }

            // Validate Status
            var statusSB = _enumBL.GetByCategoryAndValue("ServiceBooking.Status", objCreate.Status.ToString());
            if (statusSB != null)
            {
                serviceBooking.Status = statusSB.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Status " + objCreate.Status.ToString() + " tidak valid"));
            }

            // Validate Pickup Type
            var pickupType = _enumBL.GetByCategoryAndValue("ServiceBooking.PickupType", objCreate.PickupType.ToString());
            if (pickupType != null)
            {
                serviceBooking.PickupType = Convert.ToInt16(pickupType.ValueId);
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Pickup Type " + objCreate.Status.ToString() + " tidak valid"));
            }


            // Validate IsMitsubishi
            if (objCreate.IsMitsubishi == "0")
            {
            }
            else if (objCreate.IsMitsubishi == "1")
            {
            }
            else
            {
                validationResults.Add(new DNetValidationResult("IsMitsubishi " + objCreate.Status.ToString() + " tidak valid"));
            }

            // Validate WorkingTimeStart & WorkingTimeEnd
            if (objCreate.WorkingTimeStart.ToString("yyyy-MM-dd") != "1753-01-01"
                && objCreate.WorkingTimeEnd.ToString("yyyy-MM-dd") != "1753-01-01"
                && objCreate.WorkingTimeStart != DateTime.MinValue
                && objCreate.WorkingTimeEnd != DateTime.MinValue)
            {
                if (objCreate.WorkingTimeStart > objCreate.WorkingTimeEnd)
                {
                    validationResults.Add(new DNetValidationResult("WorkingTimeEnd harus lebih besar dari WorkingTimeStart"));
                }
                else
                {
                    serviceBooking.WorkingTimeStart = objCreate.WorkingTimeStart;
                    serviceBooking.WorkingTimeEnd = objCreate.WorkingTimeEnd;
                }
            }

            // Validate PreferredSA to TrTrainee
            if (!string.IsNullOrEmpty(objCreate.PreferredSA))
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(TrTrainee), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(TrTrainee), "ID", MatchType.Exact, objCreate.PreferredSA));
                criterias.opAnd(new Criteria(typeof(TrTrainee), "Dealer.DealerCode", MatchType.Exact, objCreate.DealerCode));

                var trTraineeData = _trTraineeMapper.RetrieveByCriteria(criterias);
                if (trTraineeData == null || trTraineeData.Count < 1)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFound, "PreferredSA", objCreate.PreferredSA)));
                }
                else
                {
                    serviceBooking.TrTrainee = (TrTrainee)trTraineeData[0];
                }
            }

            if (validationResults.Count > 0)
            {
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateServiceBookingActivity(ServiceBookingParameterDto objCreate, List<DNetValidationResult> validationResults, List<ServiceBookingActivity> serviceBookingActivity)
        {
            foreach (ServiceBookingActivityParameterDto item in objCreate.servicebookingactivity)
            {
                ServiceBookingActivity servicebookingactivity = _mapper.Map<ServiceBookingActivity>(item);

                //var getservicebooking = _serviceBookingMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ServiceBooking), "RowStatus", "ServiceBookingCode", (short)DBRowStatus.Active, objCreate.ServiceBookingCode));
                //if (getservicebooking.Count > 0)
                //{
                //    var sb = getservicebooking[0] as ServiceBooking;
                //    servicebookingactivity.ServiceBooking = sb;
                //}
                //else
                //{
                //    validationResults.Add(new DNetValidationResult("Service Booking Code" + objCreate.ServiceBookingCode.ToString() + " tidak ditemukan"));
                //    return false;
                //}

                var servicetypeSB = _enumBL.GetByCategoryAndValue("ServiceBooking.ServiceType", servicebookingactivity.ServiceTypeID.ToString());
                if (servicetypeSB != null)
                {
                    if (servicetypeSB.ValueCode == "FS")
                    {
                        var fskind = _fsKindMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(FSKind), "RowStatus", "KindCode", (short)DBRowStatus.Active, item.KindCode));
                        if (fskind.Count > 0)
                        {
                            var fskindresult = fskind[0] as FSKind;
                            servicebookingactivity.KindCode = fskindresult.ID.ToString();
                        }
                    }
                    else if (servicetypeSB.ValueCode == "PM")
                    {
                        var pmkind = _pmKindMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(PMKind), "RowStatus", "KindCode", (short)DBRowStatus.Active, item.KindCode));
                        if (pmkind.Count > 0)
                        {
                            var pmkindresult = pmkind[0] as PMKind;
                            servicebookingactivity.KindCode = pmkindresult.ID.ToString();
                        }
                    }
                    else if (servicetypeSB.ValueCode == "FF")
                    {
                        var recallregcategory = _recalregKindMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(RecallCategory), "RowStatus", "RecallRegNo", (short)DBRowStatus.Active, item.KindCode));
                        if (recallregcategory.Count > 0)
                        {
                            var recallregcategoryresult = recallregcategory[0] as RecallCategory;
                            servicebookingactivity.KindCode = recallregcategoryresult.ID.ToString();
                        }
                    }
                    serviceBookingActivity.Add(servicebookingactivity);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Service Type " + servicebookingactivity.ServiceTypeID.ToString() + " tidak valid"));
                    return false;
                }
            }
            return true;
        }

        private bool ValidateServiceBookingUpdate(ServiceBookingUpdateParameterDto objUpdate, List<DNetValidationResult> validationResults, out ServiceBooking serviceBooking)
        {
            bool isValid = true;
            serviceBooking = _mapper.Map<ServiceBooking>(objUpdate);

            // Validate Dealer            
            Dealer dealer = null;
            if (!string.IsNullOrEmpty(objUpdate.DealerCode))
            {
                if (ValidationHelper.ValidateDealer(objUpdate.DealerCode, validationResults, this.DealerCode, ref dealer))
                {
                    serviceBooking.Dealer = dealer;
                }
            }

            // Validate Chassis Master
            ChassisMaster chassis = null;
            if (!string.IsNullOrEmpty(objUpdate.ChassisNumber))
            {
                if (ValidateChassisMaster(objUpdate.ChassisNumber, validationResults, ref chassis))
                {
                    serviceBooking.ChassisMaster = chassis;
                }
            }

            // Validate Vehicle Type
            if (!string.IsNullOrEmpty(objUpdate.VechileTypeCode))
            {
                var vechiletype = _vechileTypeMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(VechileType), "RowStatus", "VechileTypeCode", (short)DBRowStatus.Active, objUpdate.VechileTypeCode));
                if (vechiletype.Count > 0)
                {
                    var vechileTypeSB = vechiletype[0] as VechileType;
                    serviceBooking.VechileType = vechileTypeSB;

                    // Validate Vehicle Model
                    var vehiclemodel = _vehicleModelMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(VechileModel), "RowStatus", "ID", (short)DBRowStatus.Active, vechileTypeSB.VechileModel.ID));
                    if (vehiclemodel.Count > 0)
                    {
                        var vehiclemodelSB = vehiclemodel[0] as VechileModel;
                        serviceBooking.VechileModel = vehiclemodelSB;
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult("Vehicle Model ID " + vechileTypeSB.VechileModel.ID.ToString() + " tidak ditemukan"));
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Vehicle Type Code " + objUpdate.VechileTypeCode.ToString() + " tidak ditemukan"));
                }
            }

            //Validate Duplicate by StallMasterCode & WorkingTimeStart/End
            if (objUpdate.WorkingTimeStart.ToString("yyyy-MM-dd") != "1753-01-01"
                && objUpdate.WorkingTimeEnd.ToString("yyyy-MM-dd") != "1753-01-01"
                && objUpdate.WorkingTimeStart != DateTime.MinValue
                && objUpdate.WorkingTimeEnd != DateTime.MinValue)
            {
                if (objUpdate.StallMasterCode != "" && objUpdate.StallMasterCode != null && objUpdate.ID != 0)
                {
                    var criterias = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias.opAnd(new Criteria(typeof(ServiceBooking), "ID", MatchType.Exact, objUpdate.ID));
                    var data = _serviceBookingMapper.RetrieveByCriteria(criterias);

                    if (data.Count > 0)
                    {
                        ServiceBooking dataOnDB = (ServiceBooking)data[0];

                        var criteriaSvcBoking = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "ServiceBookingCode", MatchType.No, dataOnDB.ServiceBookingCode));
                        criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "StallMaster.StallCode", MatchType.Exact, objUpdate.StallMasterCode));
                        criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "Status", MatchType.InSet, "('1', '2')")); //Status Booked & Request
                        criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "Dealer.DealerCode", MatchType.Exact, objUpdate.DealerCode));
                        criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "WorkingTimeStart", MatchType.GreaterOrEqual, objUpdate.WorkingTimeStart.ToString("yyyy-MM-dd")));
                        criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "WorkingTimeEnd", MatchType.Lesser, objUpdate.WorkingTimeEnd.AddDays(1).ToString("yyyy-MM-dd")));
                        //criteriaSvcBoking.opOr(new Criteria(typeof(ServiceBooking), "WorkingTimeEnd", MatchType.GreaterOrEqual, objUpdate.WorkingTimeStart));
                        //criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "WorkingTimeEnd", MatchType.LesserOrEqual, objUpdate.WorkingTimeEnd));

                        var existingSvcBooking = _serviceBookingMapper.RetrieveByCriteria(criteriaSvcBoking);
                        if (existingSvcBooking.Count > 0)
                        {
                            string[] arrStatus = { "1", "2" };
                            bool isStatusBooked = false;
                            bool isValidData = true;
                            foreach (ServiceBooking item in existingSvcBooking)
                            {
                                if (arrStatus.Contains(item.Status) && item.RowStatus == 0 && item.StallMaster.StallCode == objUpdate.StallMasterCode && item.Dealer.DealerCode == objUpdate.DealerCode)
                                {
                                    if (item.Status == "1")
                                        isStatusBooked = true;

                                    if (objUpdate.WorkingTimeStart < item.WorkingTimeEnd && item.WorkingTimeStart < objUpdate.WorkingTimeEnd)
                                    {
                                        isValidData = false;
                                    }
                                    else if (objUpdate.WorkingTimeEnd > item.WorkingTimeStart && item.WorkingTimeEnd > objUpdate.WorkingTimeStart)
                                    {
                                        isValidData = false;
                                    }
                                }

                                if (!isValidData)
                                    break;
                            }

                            if (!isValidData)
                            {
                                if (isStatusBooked)
                                    validationResults.Add(new DNetValidationResult("Data dengan StallMasterCode " + objUpdate.StallMasterCode.ToString() + " di rentang WorkingTimeStart '" + objUpdate.WorkingTimeStart + "' dan '" + objUpdate.WorkingTimeEnd + "' untuk dealer " + objUpdate.DealerCode + " sudah ada di database."));
                                else if (!isStatusBooked)
                                    validationResults.Add(new DNetValidationResult("Data dengan StallMasterCode " + objUpdate.StallMasterCode.ToString() + " di rentang WorkingTimeStart '" + objUpdate.WorkingTimeStart + "' dan '" + objUpdate.WorkingTimeEnd + "' untuk dealer " + objUpdate.DealerCode + " sedang dalam proses booking pada MMID."));
                            }
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.ID)));
                    }
                }
            }
            else
            {
                if (objUpdate.StallMasterCode != "" && objUpdate.StallMasterCode != null && objUpdate.ID != 0)
                {
                    var criterias = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias.opAnd(new Criteria(typeof(ServiceBooking), "ID", MatchType.Exact, objUpdate.ID));
                    var data = _serviceBookingMapper.RetrieveByCriteria(criterias);

                    if (data.Count > 0)
                    {
                        ServiceBooking dataOnDB = (ServiceBooking)data[0];

                        var criteriaSvcBoking = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "ServiceBookingCode", MatchType.Exact, dataOnDB.ServiceBookingCode));
                        criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "ChassisMaster.ChassisNumber", MatchType.Exact, objUpdate.ChassisNumber));
                        criteriaSvcBoking.opAnd(new Criteria(typeof(ServiceBooking), "Dealer.DealerCode", MatchType.Exact, objUpdate.DealerCode));
                        var existingSvcBooking = _serviceBookingMapper.RetrieveByCriteria(criteriaSvcBoking);
                        if (existingSvcBooking.Count < 0)
                        {
                            validationResults.Add(new DNetValidationResult("Service Booking Code " + dataOnDB.ServiceBookingCode.ToString() + " untuk dealer " + objUpdate.DealerCode + " tidak ditemukan di database."));
                        }
                    }
                }
            }

            // Validate Stall Master
            if (!string.IsNullOrEmpty(objUpdate.StallMasterCode))
            {
                var stallmaster = _stallMasterMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StallMaster), "RowStatus", "StallCode", (short)DBRowStatus.Active, objUpdate.StallMasterCode));
                if (stallmaster.Count > 0)
                {
                    var stallmasterSB = stallmaster[0] as StallMaster;
                    serviceBooking.StallMaster = stallmasterSB;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Stall Code " + objUpdate.StallMasterCode.ToString() + " tidak ditemukan"));
                }
            }

            //// Validate Service Type
            //var servicetypeSB = _enumBL.GetByCategoryAndValue("ServiceBooking.ServiceType", objUpdate.ServiceType.ToString());
            //if (servicetypeSB != null)
            //{
            //    serviceBooking.ServiceTypeID = Convert.ToInt16(servicetypeSB.ValueId);
            //}
            //else
            //{
            //    validationResults.Add(new DNetValidationResult("Service Type " + objUpdate.ServiceType.ToString() + " tidak valid"));
            //}

            // Validate Status
            if (!string.IsNullOrEmpty(objUpdate.Status))
            {
                var statusSB = _enumBL.GetByCategoryAndValue("ServiceBooking.Status", objUpdate.Status.ToString());
                if (statusSB != null)
                {
                    serviceBooking.Status = statusSB.ValueId.ToString();
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Status " + objUpdate.Status.ToString() + " tidak valid"));
                }
            }

            // Validate Pickup Type
            if (!string.IsNullOrEmpty(objUpdate.PickupType))
            {
                var pickupType = _enumBL.GetByCategoryAndValue("ServiceBooking.PickupType", objUpdate.PickupType.ToString());
                if (pickupType != null)
                {
                    serviceBooking.PickupType = Convert.ToInt16(pickupType.ValueId);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Pickup Type " + objUpdate.PickupType.ToString() + " tidak valid"));
                }
            }

            // Validate Stall Service Type
            if (!string.IsNullOrEmpty(objUpdate.StallServiceType))
            {
                var stallserviceType = _enumBL.GetByCategoryAndValue("ServiceBooking.StallServiceType", objUpdate.StallServiceType.ToString());
                if (stallserviceType != null)
                {
                    serviceBooking.StallServiceType = Convert.ToInt16(stallserviceType.ValueId);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Stall Service Type " + objUpdate.StallServiceType.ToString() + " tidak valid"));
                }
            }

            // Validate IsMitsubishi
            if (!string.IsNullOrEmpty(objUpdate.IsMitsubishi) && objUpdate.IsMitsubishi.All(char.IsDigit))
            {
                if (Convert.ToInt16(objUpdate.IsMitsubishi) > 1)
                {
                    validationResults.Add(new DNetValidationResult("IsMitsubishi " + objUpdate.IsMitsubishi.ToString() + " tidak valid"));
                }
            }

            // Validate WorkingTimeStart & WorkingTimeEnd
            if (objUpdate.WorkingTimeStart.ToString("yyyy-MM-dd") != "1753-01-01"
                && objUpdate.WorkingTimeEnd.ToString("yyyy-MM-dd") != "1753-01-01"
                && objUpdate.WorkingTimeStart != DateTime.MinValue
                && objUpdate.WorkingTimeEnd != DateTime.MinValue)
            {
                if (objUpdate.WorkingTimeStart > objUpdate.WorkingTimeEnd)
                {
                    validationResults.Add(new DNetValidationResult("WorkingTimeEnd harus lebih besar dari WorkingTimeStart"));
                }
                else
                {
                    serviceBooking.WorkingTimeStart = objUpdate.WorkingTimeStart;
                    serviceBooking.WorkingTimeEnd = objUpdate.WorkingTimeEnd;
                }
            }

            // Validate PreferredSA to TrTrainee
            if (!string.IsNullOrEmpty(objUpdate.PreferredSA))
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(TrTrainee), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(TrTrainee), "ID", MatchType.Exact, objUpdate.PreferredSA));
                criterias.opAnd(new Criteria(typeof(TrTrainee), "Dealer.DealerCode", MatchType.Exact, DealerCode));

                var trTraineeData = _trTraineeMapper.RetrieveByCriteria(criterias);
                if (trTraineeData == null || trTraineeData.Count < 1)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFound, "PreferredSA", objUpdate.PreferredSA)));
                }
                else
                {
                    serviceBooking.TrTrainee = (TrTrainee)trTraineeData[0];
                }
            }

            if (validationResults.Count > 0)
            {
                isValid = false;
            }

            return isValid;
        }


        private string GenerateServiceBookingCode(int iD)
        {
            var servicebookingcode = string.Empty;
            var ServiceBookingGenerator = _serviceBookingMapper.RetrieveDataSet("SELECT [dbo].[ufn_CreateServiceBookingCode] (" + iD + ") as servicebookingcode");
            if (ServiceBookingGenerator.Tables.Count > 0)
            {
                if (ServiceBookingGenerator.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ServiceBookingGenerator.Tables[0].Rows)
                    {
                        servicebookingcode = item["servicebookingcode"].ToString();
                    }
                }
            }
            return servicebookingcode;
        }
        /// <summary>
        /// Trans manager handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(ServiceBooking))
            {
                ((ServiceBooking)args.DomainObject).ID = args.ID;
                ((ServiceBooking)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(ServiceBookingActivity))
            {
                ((ServiceBookingActivity)args.DomainObject).ID = args.ID;
                ((ServiceBookingActivity)args.DomainObject).MarkLoaded();
            }
        }


        /// <summary>
        /// Insert via trans manager
        /// </summary>
        /// <param name="servicebooking"></param>
        /// <param name="serviceBookingActivity"></param>
        /// <returns></returns>
        private ServiceBooking InsertWithTransactionManager(ServiceBooking servicebooking, List<ServiceBookingActivity> serviceBookingActivity, List<DNetValidationResult> validationResults)
        {
            ServiceBooking result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();
                    // add command to insert servicebooking header
                    this._transactionManager.AddInsert(servicebooking, DNetUserName);

                    if (serviceBookingActivity != null)
                    {
                        // add command to insert ServiceBookingActivity detail
                        foreach (ServiceBookingActivity item in serviceBookingActivity)
                        {
                            item.ServiceBooking = servicebooking;
                            this._transactionManager.AddInsert(item, DNetUserName);
                        }
                    }

                    this._transactionManager.PerformTransaction();
                    result = servicebooking;
                }
                catch (SqlException sqlException)
                {
                    ExceptionDispatchInfo.Capture(sqlException).Throw();
                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                finally
                {
                    this.RemoveTaskLocking();
                }
            }

            return result;


        }

        private ServiceBooking UpdateWithTransactionManager(ServiceBooking servicebooking, List<ServiceBookingActivity> serviceBookingActivity)
        {
            ServiceBooking result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    foreach (ServiceBookingActivity item in serviceBookingActivity)
                    {
                        item.ServiceBooking = servicebooking;
                        if (item.ID == 0)
                            this._transactionManager.AddInsert(item, DNetUserName);
                        else
                            this._transactionManager.AddUpdate(item, DNetUserName);
                    }

                    foreach (ServiceBookingActivity item in servicebooking.ServiceBookingActivities)
                    {
                        bool isExists = false;
                        foreach (ServiceBookingActivity itemExist in serviceBookingActivity)
                        {
                            if (item.ID == itemExist.ID)
                            {
                                isExists = true;
                                break;
                            }
                        }

                        if (!isExists)
                        {
                            item.RowStatus = (short)DBRowStatus.Deleted;
                            this._transactionManager.AddUpdate(item, DNetUserName);
                        }
                    }

                    this._transactionManager.AddUpdate(servicebooking, DNetUserName);

                    this._transactionManager.PerformTransaction();
                    result = servicebooking;
                }
                catch (SqlException sqlException)
                {
                    ExceptionDispatchInfo.Capture(sqlException).Throw();
                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                finally
                {
                    this.RemoveTaskLocking();
                }
            }

            return result;


        }

        public static bool ValidateChassisMaster(string chassisNumber, List<DNetValidationResult> validationResults, ref ChassisMaster chassisMaster)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMaster), "ChassisNumber", "Category.ProductCategory.Code", chassisNumber, ConfigurationManager.AppSettings["CompanyCode"]));
            if (masters.Count > 0)
            {
                // cast the object
                chassisMaster = masters[0] as ChassisMaster;
            }
            else
            {
                chassisMaster = null;
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, "ChassisNumber", "value '" + chassisNumber + "'")));
            }
            return validationResults.Count == 0;
        }

        private void PopulateData(ServiceBookingRealtimeParameterDto objParameter, ref ServiceBooking svcBooking,
            ref List<ServiceBookingActivity> svcBookActivities, bool isUpdate = false)
        {
            objParameter.ServiceBookingActivities.ForEach(d =>
            {
                d.ServiceTypeID = _enumBL.GetByCategoryAndCode("ServiceBooking.ServiceType", d.ServiceTypeCode).ValueId;
            });

            svcBookActivities = objParameter.ServiceBookingActivities.Select(item => _mapper.Map<ServiceBookingActivity>(item)).ToList();

            if (!isUpdate)
            {
                svcBooking = _mapper.Map<ServiceBooking>(objParameter);
                svcBooking.ServiceBookingCode = string.IsNullOrEmpty(objParameter.UserID) ? "" : objParameter.UserID;
                svcBooking.Status = ((int)EnumStallMaster.StatusBooking.Request).ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(objParameter.Status))
                    svcBooking.Status = _enumBL.GetByCategoryAndCode("ServiceBooking.Status", objParameter.Status).ValueId.ToString();
                svcBooking.ChassisNumber = objParameter.ChassisNumber;
                svcBooking.PlateNumber = objParameter.PlateNumber;
                svcBooking.CustomerName = objParameter.CustomerName;
                svcBooking.CustomerPhoneNumber = objParameter.CustomerPhoneNumber;
                svcBooking.OdoMeter = objParameter.Odometer;
                svcBooking.WorkingTimeStart = objParameter.WorkingTimeStart;
                svcBooking.WorkingTimeEnd = objParameter.WorkingTimeEnd;
                svcBooking.IncomingDateStart = objParameter.IncomingDateStart;
                svcBooking.IncomingDateEnd = objParameter.IncomingDateEnd;

                if (svcBookActivities.Count > 0)
                {
                    List<ServiceBookingActivity> tempActivities = svcBooking.ServiceBookingActivities.Cast<ServiceBookingActivity>().ToList();
                    int endIndex = tempActivities.Count > svcBookActivities.Count ? svcBookActivities.Count : tempActivities.Count;
                    for (int i = 0; i < endIndex; i++)
                    {
                        svcBookActivities[i].ID = tempActivities[i].ID;
                        svcBookActivities[i].CreatedBy = tempActivities[i].CreatedBy;
                    }
                }
            }

            svcBooking.Dealer = _dealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "RowStatus", "DealerCode", (short)DBRowStatus.Active, objParameter.DealerCode)).Cast<Dealer>().SingleOrDefault();
            svcBooking.PickupType = (short)_enumBL.GetByCategoryAndCode("ServiceBooking.PickupType", objParameter.PickupType).ValueId;
            svcBooking.StallServiceType = (short)_enumBL.GetByCategoryAndCode("ServiceBooking.StallServiceType", objParameter.StallServiceType).ValueId;
            svcBooking.ChassisNumber = objParameter.ChassisNumber;

            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, objParameter.ChassisNumber));
            criterias.opAnd(new Criteria(typeof(ChassisMaster), "Category.ID", MatchType.InSet, "(1,2)"));

            var objCM = _chassisMasterMapper.RetrieveByCriteria(criterias).Cast<ChassisMaster>().SingleOrDefault();

            if (objCM != null)
            {
                svcBooking.ChassisMaster = objCM;
                svcBooking.VechileModel = objCM.VechileColor.VechileType.VechileModel;
                svcBooking.VechileType = objCM.VechileColor.VechileType;
            }
            else
            {
                int vechileTypeId = 0, vechileModelId = 0;
                DataTable dt = _chassisMasterMapper.RetrieveDataSet(string.Format("EXEC sp_GetVTRealtimeService '{0}', '{1}'", objParameter.VechileModel, objParameter.VariantType)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    vechileTypeId = Convert.ToInt32(dt.Rows[0]["VechileTypeID"].ToString());
                    vechileModelId = Convert.ToInt32(dt.Rows[0]["VechileModelID"].ToString());
                }

                svcBooking.VechileType = _vechileTypeMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(VechileType), "RowStatus", "ID", (short)DBRowStatus.Active, vechileTypeId)).Cast<VechileType>().SingleOrDefault();
                svcBooking.VechileModel = _vehicleModelMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(VechileModel), "RowStatus", "ID", (short)DBRowStatus.Active, vechileModelId)).Cast<VechileModel>().SingleOrDefault();
            }

            svcBooking.TrTrainee = (TrTrainee)_trTraineeMapper.Retrieve(objParameter.ServiceAdvisorID);

            if (svcBooking.TrTrainee != null && svcBooking.TrTrainee.Dealer.ID != svcBooking.Dealer.ID)
                svcBooking.TrTrainee = null;

            svcBooking.Notes = string.Format("Remark : {0}GR Note : {1}{2}Complaint : {3}{4}Voucher : {5}",
                Environment.NewLine, objParameter.GRNote, Environment.NewLine, objParameter.Complaint, Environment.NewLine, objParameter.Voucher);
        }


        #endregion
    }
}

