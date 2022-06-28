#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistServiceIncoming business logic class
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
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Domain;
using System.Linq.Expressions;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class AssistServiceIncomingBL : AbstractBusinessLogic, IAssistServiceIncomingBL
    {
        #region Variables
        private readonly IMapper _assistServiceIncomingMapper;
        private readonly IMapper _assistServiceTypeMapper;
        private readonly IMapper _assistWorkOrderCategoryMapper;
        private readonly IMapper _assistServicePlaceMapper;
        private readonly IMapper _stallMasterMapper;
        private readonly IMapper _serviceBookingMapper;
        private readonly IMapper _dealerMapper;


        private readonly IMapper _mekanikMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private bool IsWOCompleted;
        private bool IsWOCanceled;
        private IAssistChassisMasterRepository<AssistChassisMaster, int> _assistChassisMasterRepo;
        #endregion

        #region Constructor
        public AssistServiceIncomingBL(IAssistChassisMasterRepository<AssistChassisMaster,int> assistChassisMasterRepository)
        {
            _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(DNet.Domain.Dealer).ToString());
            _assistServiceIncomingMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistServiceIncoming).ToString());
            _assistServiceTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistServiceType).ToString());
            _assistWorkOrderCategoryMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistWorkOrderCategory).ToString());
            _assistServicePlaceMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistServicePlace).ToString());
            _stallMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(StallMaster).ToString());
            _serviceBookingMapper = MapperFactory.GetInstance().GetMapper(typeof(ServiceBooking).ToString());
            _mekanikMapper = MapperFactory.GetInstance().GetMapper(typeof(TrTrainee).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _assistChassisMasterRepo = assistChassisMasterRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get AssistServiceIncoming by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistServiceIncomingDto>> Read(AssistServiceIncomingFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(AssistServiceIncoming), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(AssistServiceIncoming), "DealerCode", MatchType.Exact, this.DealerCode));
            var result = new ResponseBase<List<AssistServiceIncomingDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(AssistServiceIncoming), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(AssistServiceIncoming), filterDto, sortColl);

                // get data
                var data = _assistServiceIncomingMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<AssistServiceIncoming>().ToList();
                    var listData = list.Select(item => _mapper.Map<AssistServiceIncomingDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(AssistServiceIncoming), filterDto);
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

        /// <summary>
        /// Delete AssistServiceIncoming by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<AssistServiceIncomingDto> Delete(int id)
        {
            var result = new ResponseBase<AssistServiceIncomingDto>();

            try
            {
                var assistServiceIncoming = (AssistServiceIncoming)_assistServiceIncomingMapper.Retrieve(id);
                if (assistServiceIncoming != null)
                {
                    assistServiceIncoming.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _assistServiceIncomingMapper.Update(assistServiceIncoming, DNetUserName);
                    if (nResult != 0)
                    {
                        result.success = true;
                        result._id = id;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    ErrorMsgHelper.DeleteNotAvailable(result.messages);
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

        /// <summary>
        /// Create a new AssistServiceIncoming
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<AssistServiceIncomingDto> Create(AssistServiceIncomingParameterDto objCreate)
        {
            #region Declarations
            var result = new ResponseBase<AssistServiceIncomingDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            KTB.DNet.Domain.Dealer dealer = null;
            TrTrainee mekanik = null;
            DealerBranch dealerBranch = null;
            AssistServiceType serviceType = null;
            AssistServicePlace servicePlace = null;
            AssistWorkOrderCategory workOrderCategory = null;
            AssistServiceIncoming assistServiceIncoming = null;
            #endregion

            try
            {
                ValidateWOStatus(objCreate, validationResults, ref isValid);

                if (isValid) { isValid = ValidateRequiredFields(objCreate, validationResults); }

                if (isValid) { isValid = ValidateTglBukaTglTutup(objCreate, validationResults); }

                if (isValid) { isValid = ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer); }

                if (isValid && IsWOCompleted) { isValid = ValidationHelper.ValidateTrTrainee(objCreate.KodeMekanik, validationResults, ref mekanik, objCreate.DealerCode); }

                if (isValid) { isValid = ValidationHelper.ValidateAssistServiceType(objCreate.ServiceTypeCode, validationResults, ref serviceType); }

                if (isValid) { isValid = ValidationHelper.ValidateAssistServicePlace(objCreate.ServicePlaceCode, validationResults, ref servicePlace); }

                if (isValid) { isValid = ValidationHelper.ValidateAssistWorkOrderCategory(objCreate.WorkOrderCategoryCode, validationResults, ref workOrderCategory); }

                if (isValid) { isValid = ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, objCreate.DealerBranchCode, ref dealerBranch); }

                if (isValid) { isValid = ValidateServiceIncoming(objCreate, validationResults, ref assistServiceIncoming); }

                if (isValid)
                {
                    if (!string.IsNullOrEmpty(objCreate.StallCode))
                    {
                        isValid = ValidateStallCode(objCreate.StallCode, validationResults);
                    }
                }
                if (isValid) { isValid = ValidateBookingCode(objCreate.BookingID, objCreate.ServiceTypeCode, validationResults); }

                if (isValid)
                {
                    AssistServiceIncoming newServiceIncoming = null;
                    newServiceIncoming = _mapper.Map<AssistServiceIncoming>(objCreate);
                    newServiceIncoming.AssistServicePlace = servicePlace;
                    newServiceIncoming.AssistServiceType = serviceType;
                    newServiceIncoming.AssistWorkOrderCategory = workOrderCategory;
                    newServiceIncoming.ChassisMaster = GetChassisMaster(objCreate.KodeChassis);
                    if (!string.IsNullOrEmpty(objCreate.StallCode))
                    {
                        newServiceIncoming.StallMaster = GetStallMaster(objCreate.StallCode);
                    }
                    if (!string.IsNullOrEmpty(objCreate.BookingID))
                    {
                        var service = GetServiceBooking(objCreate.BookingID);
                        newServiceIncoming.ServiceBooking = service;
                        newServiceIncoming.BookingCode = service.ServiceBookingCode;
                    }

                    newServiceIncoming.Dealer = dealer;
                    if (dealerBranch != null)
                    {
                        newServiceIncoming.DealerBranch = dealerBranch;
                        newServiceIncoming.DealerBranchCode = dealerBranch.DealerBranchCode;
                    }
                    newServiceIncoming.Model = FillModel(newServiceIncoming);
                    newServiceIncoming.CreatedBy = DNetUserName;
                    newServiceIncoming.CreatedTime = DateTime.Now;
                    newServiceIncoming.LastUpdateBy = DNetUserName;
                    newServiceIncoming.LastUpdateTime = DateTime.Now;
                    newServiceIncoming.StatusAktif = (short)1; // set to 1 as default
                    newServiceIncoming.ValidateSystemStatus = 1; // set to 1 as default

                    int id = _assistServiceIncomingMapper.Insert(newServiceIncoming, DNetUserName);

                    result.success = id > 0;
                    if (!result.success)
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }

                    result._id = id;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<AssistServiceIncomingDto>(validationResults, null);
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


        /// <summary>
        /// Update AssistServiceIncoming
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<AssistServiceIncomingDto> Update(AssistServiceIncomingParameterDto objUpdate)
        {
            #region Declarations
            var result = new ResponseBase<AssistServiceIncomingDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            KTB.DNet.Domain.Dealer dealer = null;
            DealerBranch dealerBranch = null;
            AssistServiceType serviceType = null;
            AssistServicePlace servicePlace = null;
            AssistWorkOrderCategory workOrderCategory = null;
            AssistServiceIncoming assistServiceIncoming = null;
            TrTrainee trTrainee = null;
            AssistServiceIncoming newServiceIncoming = null;
            #endregion

            try
            {
                ValidateWOStatus(objUpdate, validationResults, ref isValid);

                if (isValid) { isValid = ValidateRequiredFields(objUpdate, validationResults); }

                if (isValid) { isValid = ValidateTglBukaTglTutup(objUpdate, validationResults); }

                if (isValid) { isValid = ValidationHelper.ValidateDealer(objUpdate.DealerCode, validationResults, this.DealerCode, ref dealer); }

                if (isValid) { isValid = ValidateStallCodeDealerInterface(objUpdate, validationResults); }
                
                if (isValid && IsWOCompleted) { isValid = ValidationHelper.ValidateTrTrainee(objUpdate.KodeMekanik, validationResults, ref trTrainee, objUpdate.DealerCode); }

                if (isValid) { isValid = ValidationHelper.ValidateAssistServiceType(objUpdate.ServiceTypeCode, validationResults, ref serviceType); }

                if (isValid) { isValid = ValidationHelper.ValidateAssistServicePlace(objUpdate.ServicePlaceCode, validationResults, ref servicePlace); }

                if (isValid) { isValid = ValidationHelper.ValidateAssistWorkOrderCategory(objUpdate.WorkOrderCategoryCode, validationResults, ref workOrderCategory); }

                if (isValid) { isValid = ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, objUpdate.DealerBranchCode, ref dealerBranch); }

                if (isValid) { isValid = ValidateServiceIncoming(objUpdate, validationResults, ref assistServiceIncoming, false); }

                if (isValid)
                {
                    if (string.IsNullOrEmpty(objUpdate.StallCode))
                    {
                        //validationResults.Add(new DNetValidationResult("Stall Code tidak boleh kosong"));
                        //isValid = false;
                        isValid = true;
                    }
                    else
                    {
                        isValid = ValidateStallCode(objUpdate.StallCode, validationResults);
                    }
                }

                if (isValid)
                {
                    isValid = ValidateBookingCode(objUpdate.BookingID, objUpdate.ServiceTypeCode, validationResults);

                }

                if (isValid)
                {
                    string createdByDB = assistServiceIncoming.CreatedBy;
                    DateTime createdTimeDB = assistServiceIncoming.CreatedTime;
                    int statusAktif = IsWOCompleted ? 1 : assistServiceIncoming.StatusAktif;
                    int oldID = assistServiceIncoming.ID;

                    if (IsWOCompleted && assistServiceIncoming.WOStatus == objUpdate.WOStatus)
                    {
                        newServiceIncoming = assistServiceIncoming;
                        newServiceIncoming.TotalLC = objUpdate.TotalLC;
                        newServiceIncoming.LastUpdateBy = DNetUserName;
                        newServiceIncoming.LastUpdateTime = DateTime.Now;
                    }
                    else
                    {
                        newServiceIncoming = _mapper.Map<AssistServiceIncomingParameterDto, AssistServiceIncoming>(objUpdate, assistServiceIncoming);
                        newServiceIncoming.AssistServicePlace = servicePlace;
                        newServiceIncoming.AssistServiceType = serviceType;
                        newServiceIncoming.AssistWorkOrderCategory = workOrderCategory;
                        newServiceIncoming.ChassisMaster = GetChassisMaster(objUpdate.KodeChassis);
                        newServiceIncoming.Dealer = dealer;
                        if (dealerBranch != null)
                        {
                            newServiceIncoming.DealerBranch = dealerBranch;
                            newServiceIncoming.DealerBranchCode = dealerBranch.DealerBranchCode;
                        }
                        newServiceIncoming.LastUpdateBy = DNetUserName;
                        newServiceIncoming.LastUpdateTime = DateTime.Now;

                        // set with the existing data
                        newServiceIncoming.StatusAktif = (short)statusAktif;
                        newServiceIncoming.ValidateSystemStatus = 1;
                        newServiceIncoming.ID = oldID;
                        newServiceIncoming.CreatedBy = createdByDB;
                        newServiceIncoming.CreatedTime = createdTimeDB;
                    }
                    newServiceIncoming.Model = FillModel(newServiceIncoming);
                    if (!string.IsNullOrEmpty(objUpdate.StallCode))
                    {
                        newServiceIncoming.StallMaster = GetStallMaster(objUpdate.StallCode);
                    }
                    if (!string.IsNullOrEmpty(objUpdate.BookingID))
                    {
                        var sevice = GetServiceBooking(objUpdate.BookingID);
                        newServiceIncoming.ServiceBooking = sevice;
                        newServiceIncoming.BookingCode = sevice.ServiceBookingCode;
                    }
                    var id = _assistServiceIncomingMapper.Update(newServiceIncoming, DNetUserName);

                    result.success = id > 0;
                    if (result.success)
                    {
                        result._id = objUpdate.ID;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<AssistServiceIncomingDto>(validationResults, null);
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
        #endregion

        #region Private Methods
        private string FillModel(AssistServiceIncoming assistServiceIncoming)
        {
            string model = assistServiceIncoming.Model;
            if (string.IsNullOrEmpty(model))
            {
                if(assistServiceIncoming.ChassisMaster != null)
                {
                    model = assistServiceIncoming.ChassisMaster.VechileColor.VechileType.VechileModel.IndDescription;
                    if (string.IsNullOrEmpty(model))
                    {
                        var sortColl = new SortCollection();

                        int totalRow = 0;
                        int filteredTotalRow = 0;

                        var criteria = new CriteriaComposite(new Criteria(typeof(AssistChassisMaster), "ColorCode", MatchType.Exact, assistServiceIncoming.ChassisMaster.VechileColor.ColorCode));
                        criteria.opAnd(new Criteria(typeof(AssistChassisMaster), "VEchileTypeCode", MatchType.Exact, assistServiceIncoming.ChassisMaster.VechileColor.VechileType.VechileTypeCode));

                        sortColl = null;

                        List<AssistChassisMaster> lstAssistChassisMaster = _assistChassisMasterRepo.Search(criteria, sortColl, 1, 100, out filteredTotalRow, out totalRow);
                        if (lstAssistChassisMaster.Count > 0)
                        {
                            model = lstAssistChassisMaster[0].MMCComModel3;
                        }
                    }
                }      
            }

            return model;
        }

        /// <summary>
        /// Validate WO Status completed
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <param name="woStatus"></param>
        /// <param name="validationResults"></param>
        private bool ValidateWOStatusCompleted(AssistServiceIncomingParameterDto objUpdate, int woStatus, List<DNetValidationResult> validationResults)
        {
            if (woStatus == _enumBL.GetByCategoryAndCode("WOStatus", "Completed").ValueId)
            {
                string dealerCode = string.IsNullOrEmpty(objUpdate.DealerBranchCode) ? objUpdate.DealerCode : objUpdate.DealerBranchCode;
                validationResults.Add(new DNetValidationResult(
                    string.Format(
                    MessageResource.ErrorMsgServiceIncomingAlreadyCompleted,
                    objUpdate.KodeChassis,
                    objUpdate.NoWorkOrder,
                    objUpdate.WorkOrderCategoryCode,
                    dealerCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate WO Status
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        private void ValidateWOStatus(AssistServiceIncomingParameterDto objCreate, List<DNetValidationResult> validationResults, ref bool isValid)
        {
            if (!_enumBL.IsExistByCategoryAndValue("WOStatus", objCreate.WOStatus.ToString()))
            {
                isValid = false;
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgWOStatusInvalid, objCreate.WOStatus)));
            }
            else
            {
                var std = _enumBL.GetByCategoryAndValue("WOStatus", objCreate.WOStatus.ToString());
                IsWOCompleted = std.ValueCode.Equals("Completed", StringComparison.OrdinalIgnoreCase);
                IsWOCanceled = std.ValueCode.Equals("Canceled", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Validate mandatory fields
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="isValid"></param>
        /// <param name="validationResults"></param>
        private bool ValidateRequiredFields(AssistServiceIncomingParameterDto objCreate, List<DNetValidationResult> validationResults)
        {
            if (IsWOCompleted)
            {
                // closed date is mandatory
                if (objCreate.TglTutupTransaksi == Constants.DATETIME_DEFAULT_VALUE)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, MessageResource.ErrorMsgClosedDateMandatory)));
                }

                // waktu keluar is mandatory
                if (objCreate.WaktuKeluar == Constants.DATETIME_DEFAULT_VALUE)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, MessageResource.ErrorMsgReleaseTimeMandatory)));
                }

                // kode mekanik is mandatory
                if (string.IsNullOrEmpty(objCreate.KodeMekanik))
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, MessageResource.ErrorMsgMecanicCodeMandatory)));
                }

                // metode pembayaran is mandatory
                if (string.IsNullOrEmpty(objCreate.MetodePembayaran))
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, MessageResource.ErrorMsgPaymentMethodMandatory)));
                }

            }
            else if (IsWOCanceled)
            {
                // closed date is mandatory
                if (objCreate.TglTutupTransaksi == Constants.DATETIME_DEFAULT_VALUE)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, MessageResource.ErrorMsgClosedDateMandatory)));
                }

                // waktu keluar is mandatory
                if (objCreate.WaktuKeluar == Constants.DATETIME_DEFAULT_VALUE)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, MessageResource.ErrorMsgReleaseTimeMandatory)));
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate tanggal buka tutup
        /// </summary>
        /// <param name="objCreateOrUpdate"></param>
        /// <param name="isValid"></param>
        /// <param name="validationResults"></param>
        private bool ValidateTglBukaTglTutup(AssistServiceIncomingParameterDto objCreateOrUpdate, List<DNetValidationResult> validationResults)
        {
            // tgl buka transaksi shouldn't more than today 
            if (objCreateOrUpdate.TglBukaTransaksi.Date > DateTime.Now.Date)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.InvalidDateTglBukaMoreThanToday, objCreateOrUpdate.TglBukaTransaksi, DateTime.Now.ToString("mm/dd/yy"))));
            }

            // tgl tutup transaksi shouldn't less than tgl buka transaksi
            if (IsWOCompleted && objCreateOrUpdate.TglTutupTransaksi < objCreateOrUpdate.TglBukaTransaksi)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.InvalidDateTglTutupLessThanTglBuka, objCreateOrUpdate.TglTutupTransaksi, objCreateOrUpdate.TglBukaTransaksi)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// validasi mandatory stall code for dealer interface
        /// </summary>
        /// <param name="objCreateOrUpdate"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateStallCodeDealerInterface(AssistServiceIncomingParameterDto objCreateOrUpdate, List<DNetValidationResult> validationResults)
        {
            CriteriaComposite criteriaDealers = new CriteriaComposite(new Criteria(typeof(DNet.Domain.Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriaDealers.opAnd(new Criteria(typeof(DNet.Domain.Dealer), "DealerCode", MatchType.Exact, objCreateOrUpdate.DealerCode));
            criteriaDealers.opAnd(new Criteria(typeof(DNet.Domain.Dealer), "DealerGroup.ID", MatchType.InSet, "(9,14)")); // 9:SUN, 14:Bosowa
           
            var dealerSystems = _dealerMapper.RetrieveByCriteria(criteriaDealers);

            if(dealerSystems.Count > 0)
            {
                if(objCreateOrUpdate.StallCode == string.Empty || objCreateOrUpdate.StallCode == null)
                {
                    validationResults.Add(new DNetValidationResult("StallCode wajib diinput."));
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate service incoming
        /// </summary>
        /// <param name="objCreateOrUpdate"></param>
        /// <param name="assistServiceIncoming"></param>
        /// <returns></returns>
        private bool ValidateServiceIncoming(AssistServiceIncomingParameterDto objCreateOrUpdate, List<DNetValidationResult> validationResults, ref AssistServiceIncoming assistServiceIncoming, bool isCreate = true)
        {
            // get serv incoming 
            CriteriaComposite criteriaServIncoming = new CriteriaComposite(new Criteria(typeof(AssistServiceIncoming), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriaServIncoming.opAnd(new Criteria(typeof(AssistServiceIncoming), "KodeChassis", MatchType.Exact, objCreateOrUpdate.KodeChassis));
            criteriaServIncoming.opAnd(new Criteria(typeof(AssistServiceIncoming), "NoWorkOrder", MatchType.Exact, objCreateOrUpdate.NoWorkOrder));
            criteriaServIncoming.opAnd(new Criteria(typeof(AssistServiceIncoming), "WorkOrderCategoryCode", MatchType.Exact, objCreateOrUpdate.WorkOrderCategoryCode));
            if (string.IsNullOrEmpty(objCreateOrUpdate.DealerBranchCode))
                criteriaServIncoming.opAnd(new Criteria(typeof(AssistServiceIncoming), "DealerCode", MatchType.Exact, objCreateOrUpdate.DealerCode));
            else
                criteriaServIncoming.opAnd(new Criteria(typeof(AssistServiceIncoming), "DealerBranchCode", MatchType.Exact, objCreateOrUpdate.DealerBranchCode));
            criteriaServIncoming.opAnd(new Criteria(typeof(AssistServiceIncoming), "ValidateSystemStatus", MatchType.Exact, 1));

            var servIncomings = _assistServiceIncomingMapper.RetrieveByCriteria(criteriaServIncoming);

            if (isCreate)
            {
                if (servIncomings.Count > 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgServiceIncomingExist, objCreateOrUpdate.KodeChassis, objCreateOrUpdate.NoWorkOrder, objCreateOrUpdate.WorkOrderCategoryCode)));
                    return false;
                }
            }
            else
            {
                if (servIncomings.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgServiceIncomingNotExist, objCreateOrUpdate.KodeChassis, objCreateOrUpdate.NoWorkOrder, objCreateOrUpdate.WorkOrderCategoryCode)));
                    return false;
                }

                assistServiceIncoming = servIncomings[0] as AssistServiceIncoming;
            }

            return true;
        }

        private ChassisMaster GetChassisMaster(string chassisNumber)
        {
            ChassisMaster chassisMaster = null;

            // get by criteria
            IMapper _chassisMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            var masters = _chassisMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMaster), "ChassisNumber", "Category.ProductCategory.Code", chassisNumber, ConfigurationManager.AppSettings["CompanyCode"]));
            if (masters.Count > 0)
            {
                // cast the object
                chassisMaster = masters[0] as ChassisMaster;
            }

            return chassisMaster;
        }
        private bool ValidateStallCode(string stallCode, List<DNetValidationResult> validationResults)
        {
            var vreturn = true;
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(StallMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(StallMaster), "StallCode", MatchType.Exact, stallCode));
            var stallMaster = _stallMasterMapper.RetrieveByCriteria(criteria);


            if (stallMaster.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("Stall Code tidak valid"));
                vreturn = false;
            }


            return vreturn;
        }
        private StallMaster GetStallMaster(string stallCode)
        {
            StallMaster stallMaster = null;

            // get by criteria
            IMapper _stallMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(StallMaster).ToString());
            var masters = _stallMasterMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StallMaster), "StallCode", "RowStatus", stallCode, (short)DBRowStatus.Active));
            if (masters.Count > 0)
            {
                // cast the object
                stallMaster = masters[0] as StallMaster;
            }

            return stallMaster;
        }

        private ServiceBooking GetServiceBooking(string bookingID)
        {
            ServiceBooking serviceBooking = null;

            // get by criteria
            IMapper _serviceBookingMapper = MapperFactory.GetInstance().GetMapper(typeof(ServiceBooking).ToString());
            var masters = _serviceBookingMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ServiceBooking), "ID", "RowStatus", bookingID, (short)DBRowStatus.Active));
            if (masters.Count > 0)
            {
                // cast the object
                serviceBooking = masters[0] as ServiceBooking;
            }

            return serviceBooking;
        }


        private bool ValidateBookingCode(string bookingID, string serviceTypeCode, List<DNetValidationResult> validationResults)
        {
            var vreturn = true;
            if (string.IsNullOrEmpty(bookingID))
            {
                if (serviceTypeCode.Contains("SB"))
                {
                    //validationResults.Add(new DNetValidationResult("BookingID tidak boleh kosong"));
                    //vreturn = false;
                    vreturn = true;
                }
                else
                {
                    vreturn = true;
                }
            }
            else
            {
                CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ServiceBooking), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteria.opAnd(new Criteria(typeof(ServiceBooking), "ID", MatchType.Exact, bookingID));
                var serviceBooking = _serviceBookingMapper.RetrieveByCriteria(criteria);

                if (serviceBooking.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult("BookingID tidak valid"));
                    vreturn = false;
                }
            }
            return vreturn;
        }
        #endregion
    }
}

