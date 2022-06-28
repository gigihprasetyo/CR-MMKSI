#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : AssistServiceIncomingBPIF BL class
 SPECIAL NOTES : DNet WebApi Project
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Created on 2021-03-23
 ===========================================================================
*/
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
    public class AssistServiceIncomingBPBL : AbstractBusinessLogic, IAssistServiceIncomingBPBL
    {
        #region Variables
        private readonly IMapper _assistServiceTypeMapper;
        private readonly IMapper _assistWorkOrderCategoryMapper;
        private readonly IMapper _assistServicePlaceMapper;
        private readonly IMapper _mekanikMapper;
        private readonly IMapper _vehicleModelMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private bool IsWOCompleted;
        private bool IsWOCanceled;
        private IAssistChassisMasterRepository<AssistChassisMaster, int> _assistChassisMasterRepo;
        private IAssistServiceIncomingBPRepository<AssistServiceIncomingBPIF, int> _AssistServiceIncomingBPRepo;
        #endregion

        #region Constructor
        public AssistServiceIncomingBPBL(IAssistServiceIncomingBPRepository<AssistServiceIncomingBPIF, int> AssistServiceIncomingBPRepo, IAssistChassisMasterRepository<AssistChassisMaster, int> assistChassisMasterRepository)
        {
            _AssistServiceIncomingBPRepo = AssistServiceIncomingBPRepo;
            _assistServiceTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistServiceType).ToString());
            _assistWorkOrderCategoryMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistWorkOrderCategory).ToString());
            _assistServicePlaceMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistServicePlace).ToString());
            _mekanikMapper = MapperFactory.GetInstance().GetMapper(typeof(TrTrainee).ToString());
            _vehicleModelMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileModel).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _assistChassisMasterRepo = assistChassisMasterRepository;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get AssistServiceIncomingBPIF by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistServiceIncomingBPDto>> Read(AssistServiceIncomingBPFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<AssistServiceIncomingBPDto>> ReadList(AssistServiceIncomingBPFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<AssistServiceIncomingBPDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(AssistServiceIncomingBPIF), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(AssistServiceIncomingBPIF), filterDto);

                List<AssistServiceIncomingBPIF> data = _AssistServiceIncomingBPRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    AssistServiceIncomingBPIF x = new AssistServiceIncomingBPIF();
                    result.lst = data.ConvertList<AssistServiceIncomingBPIF, AssistServiceIncomingBPDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(AssistServiceIncomingBPIF), filterDto);
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
        /// Create a new AssistServiceIncomingBPIF
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        /// 

        public ResponseBase<AssistServiceIncomingBPDto> Create(AssistServiceIncomingBPParameterDto objCreate)
        {
            return null;
        }

        public ResponseBase<AssistServiceIncomingBPDto> Create(AssistServiceIncomingBPCreateParameterDto objCreate)
        {
            #region Declarations
            var result = new ResponseBase<AssistServiceIncomingBPDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            KTB.DNet.Domain.Dealer dealer = null;
            TrTrainee mekanik = null;
            DealerBranch dealerBranch = null;
            AssistServiceType serviceType = null;
            AssistServicePlace servicePlace = null;
            AssistWorkOrderCategory workOrderCategory = null;
            AssistServiceIncomingBPIF AssistServiceIncomingBPIF = null;
            #endregion

            try
            {
                ValidateWOStatus(objCreate, validationResults, ref isValid);

                if (isValid) { isValid = ValidateRequiredFields(objCreate, validationResults); }

                if (isValid) { isValid = ValidateTglBukaTglTutup(objCreate, validationResults); }

                if (isValid) { isValid = ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer); }

                if (isValid) { isValid = ValidationHelper.ValidateTrTrainee(objCreate.KodeMekanik, validationResults, ref mekanik, objCreate.DealerCode); }

                if (isValid) { isValid = ValidationHelper.ValidateAssistServiceType(objCreate.ServiceTypeCode, validationResults, ref serviceType); }

                if (isValid) { isValid = ValidationHelper.ValidateAssistWorkOrderCategory(objCreate.WorkOrderCategoryCode, validationResults, ref workOrderCategory); }

                if (isValid) { isValid = ValidateServiceIncomingBP(objCreate, validationResults, ref AssistServiceIncomingBPIF); }

                if (isValid) { isValid = ValidateDataDamageCategory(objCreate, validationResults); }

                if (isValid) { isValid = ValidateVehicleModel(objCreate, validationResults); }

                if (isValid)
                {
                    ChassisMaster cM = GetChassisMaster(objCreate.KodeChassis.ToString());
                    AssistServiceIncomingBPIF createRow = objCreate.ConvertObject<AssistServiceIncomingBPIF>();
                    createRow.WorkOrderCategoryID = workOrderCategory.ID;
                    createRow.ServiceTypeID = serviceType.ID;
                    createRow.ChassisMasterID = cM.ID;
                    createRow.TrTraineMekanikID = mekanik.ID;
                    createRow.DealerID = dealer.ID;
                    createRow.CreatedBy = DNetUserName;
                    createRow.CreatedTime = DateTime.Now;
                    createRow.WOStatus = 2;  // set to 2 as default
                    createRow.StatusAktif = (short)1; // set to 1 as default
                    createRow.ValidateSystemStatus = 1; // set to 1 as default

                    var nResult = _AssistServiceIncomingBPRepo.Create(createRow);

                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result.total = 1;

                        AssistServiceIncomingBPIF resultData = nResult.Data as AssistServiceIncomingBPIF;
                        result._id = resultData.ID;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<AssistServiceIncomingBPDto>(validationResults, null);
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


        public ResponseBase<AssistServiceIncomingBPDto> Update(AssistServiceIncomingBPParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update AssistServiceIncomingBPIF
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<AssistServiceIncomingBPDto> Update(AssistServiceIncomingBPUpdateParameterDto objUpdate)
        {
            #region Declarations
            var result = new ResponseBase<AssistServiceIncomingBPDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            KTB.DNet.Domain.Dealer dealer = null;
            TrTrainee mekanik = null;
            DealerBranch dealerBranch = null;
            AssistServiceType serviceType = null;
            AssistServicePlace servicePlace = null;
            AssistWorkOrderCategory workOrderCategory = null;
            AssistServiceIncomingBPIF AssistServiceIncomingBPIF = null;
            #endregion

            try
            {
                ValidateWOStatus(objUpdate, validationResults, ref isValid);

                if (isValid) { isValid = ValidateRequiredFields(objUpdate, validationResults); }

                if (isValid) { isValid = ValidateTglBukaTglTutup(objUpdate, validationResults); }

                if (isValid) { isValid = ValidationHelper.ValidateTrTrainee(objUpdate.KodeMekanik, validationResults, ref mekanik, objUpdate.DealerCode); }

                if (isValid) { isValid = ValidationHelper.ValidateAssistServiceType(objUpdate.ServiceTypeCode, validationResults, ref serviceType); }

                if (isValid) { isValid = GetServiceIncomingBPUpdate(objUpdate, validationResults, ref AssistServiceIncomingBPIF); }

                if (isValid) { isValid = ValidateDataDamageCategory(objUpdate, validationResults); }

                if (isValid) { isValid = ValidateVehicleModel(objUpdate, validationResults); }

                if (isValid)
                {
                    AssistServiceIncomingBPIF updateRow = objUpdate.ConvertObject<AssistServiceIncomingBPIF>();
                    updateRow.ID = objUpdate.ID;
                    updateRow.TrTraineMekanikID = mekanik.ID;
                    updateRow.ServiceTypeID = serviceType.ID;

                    updateRow.NoWorkOrder = AssistServiceIncomingBPIF.NoWorkOrder;
                    updateRow.WorkOrderCategoryID = AssistServiceIncomingBPIF.WorkOrderCategoryID;
                    updateRow.WorkOrderCategoryCode = AssistServiceIncomingBPIF.WorkOrderCategoryCode;
                    updateRow.ChassisMasterID = AssistServiceIncomingBPIF.ChassisMasterID;
                    updateRow.KodeChassis = AssistServiceIncomingBPIF.KodeChassis;
                    updateRow.DealerID = AssistServiceIncomingBPIF.DealerID;
                    updateRow.DealerCode = AssistServiceIncomingBPIF.DealerCode;
                    updateRow.StatusAktif = AssistServiceIncomingBPIF.StatusAktif;
                    updateRow.ValidateSystemStatus = AssistServiceIncomingBPIF.ValidateSystemStatus;
                    updateRow.LastUpdateBy = DNetUserName;
                    updateRow.LastUpdateTime = DateTime.Now;

                    var nResult = _AssistServiceIncomingBPRepo.Update(updateRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result.total = 1;
                        AssistServiceIncomingBPIF resultData = nResult.Data as AssistServiceIncomingBPIF;
                        result._id = resultData.ID;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<AssistServiceIncomingBPDto>(validationResults, null);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }

            return result;
        }


        /// <summary>
        /// Delete AssistServiceIncomingBPIF by its id
        /// </summary>
        /// <param name="objDelete"></param>
        /// <returns></returns>
        public ResponseBase<AssistServiceIncomingBPDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBase<AssistServiceIncomingBPDto> Delete(AssistServiceIncomingBPDeleteParameterDto objDelete)
        {
            var result = new ResponseBase<AssistServiceIncomingBPDto>();
            AssistServiceIncomingBPIF AssistServiceIncomingBPIF = null;
            var validationResults = new List<DNetValidationResult>();
            KTB.DNet.Domain.Dealer dealer = null;
            var isValid = true;
            IsWOCanceled = true;

            try
            {
                if (isValid) { isValid = ValidateRequiredFields(objDelete, validationResults); }

                if (isValid) { isValid = ValidateTglBukaTglTutup(objDelete, validationResults); }

                if (isValid) { isValid = ValidationHelper.ValidateDealer(objDelete.DealerCode, validationResults, this.DealerCode, ref dealer); }

                if (isValid) { isValid = GetServiceIncomingBP(objDelete, validationResults, ref AssistServiceIncomingBPIF); }

                if (isValid)
                {
                    int oldID = AssistServiceIncomingBPIF.ID;
                    objDelete.WaktuMasuk = AssistServiceIncomingBPIF.WaktuMasuk;

                    if (isValid) { isValid = ValidateWktMasukWktKeluar(objDelete, validationResults); }

                    if (isValid)
                    {
                        AssistServiceIncomingBPIF deleteRow = objDelete.ConvertObject<AssistServiceIncomingBPIF>();
                        deleteRow.ID = oldID;
                        deleteRow.StatusAktif = -1;
                        deleteRow.WOStatus = 3;
                        deleteRow.RowStatus = 0;
                        deleteRow.LastUpdateBy = DNetUserName;
                        deleteRow.LastUpdateTime = DateTime.Now;

                        #region set data ID
                        deleteRow.WorkOrderCategoryID = AssistServiceIncomingBPIF.WorkOrderCategoryID;
                        deleteRow.ServiceTypeID = AssistServiceIncomingBPIF.ServiceTypeID;
                        deleteRow.ChassisMasterID = AssistServiceIncomingBPIF.ChassisMasterID;
                        deleteRow.TrTraineMekanikID = AssistServiceIncomingBPIF.TrTraineMekanikID;
                        deleteRow.DealerID = AssistServiceIncomingBPIF.DealerID;
                        deleteRow.KMService = AssistServiceIncomingBPIF.KMService;
                        deleteRow.TotalLC = AssistServiceIncomingBPIF.TotalLC;
                        deleteRow.TotalCat = AssistServiceIncomingBPIF.TotalCat;
                        deleteRow.TotalNonCat = AssistServiceIncomingBPIF.TotalNonCat;
                        deleteRow.TotalSubOrder = AssistServiceIncomingBPIF.TotalSubOrder;
                        #endregion

                        var nResult = _AssistServiceIncomingBPRepo.Update(deleteRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result.total = 1;

                            AssistServiceIncomingBPIF resultData = nResult.Data as AssistServiceIncomingBPIF;
                            result._id = resultData.ID;
                        }
                        else
                        {
                            ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                        }
                    }
                    else
                    {
                        return PopulateValidationError<AssistServiceIncomingBPDto>(validationResults, null);
                    }
                }
                else
                {
                    return PopulateValidationError<AssistServiceIncomingBPDto>(validationResults, null);
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

        /// <summary>
        /// Validate WO Status completed
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <param name="woStatus"></param>
        /// <param name="validationResults"></param>
        private bool ValidateWOStatusCompleted(AssistServiceIncomingBPParameterDto objUpdate, int woStatus, List<DNetValidationResult> validationResults)
        {
            if (woStatus == _enumBL.GetByCategoryAndCode("WOStatus", "Completed").ValueId)
            {
                string dealerCode = objUpdate.DealerCode;
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
        private void ValidateWOStatus(AssistServiceIncomingBPParameterDto objCreate, List<DNetValidationResult> validationResults, ref bool isValid)
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
        private bool ValidateRequiredFields(AssistServiceIncomingBPParameterDto objCreate, List<DNetValidationResult> validationResults)
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
                if (string.IsNullOrEmpty(objCreate.MethodofPayment))
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, MessageResource.ErrorMsgPaymentMethodMandatory)));
                }

                //// total LC is mandatory
                //if (objCreate.TotalLC == 0)
                //{
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, MessageResource.ErrorMsgTotalLCMandatory)));
                //    isValid = false;
                //}
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
        private bool ValidateTglBukaTglTutup(AssistServiceIncomingBPParameterDto objCreateOrUpdate, List<DNetValidationResult> validationResults)
        {
            // tgl buka transaksi shouldn't more than today 
            if (objCreateOrUpdate.TglBukaTransaksi != null && (Convert.ToDateTime(objCreateOrUpdate.TglBukaTransaksi).Date > DateTime.Now.Date))
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

        /// <summary>
        /// Validate service incoming
        /// </summary>
        /// <param name="objCreateOrUpdate"></param>
        /// <param name="AssistServiceIncomingBPIF"></param>
        /// <returns></returns>
        private bool ValidateServiceIncomingBP(AssistServiceIncomingBPParameterDto objCreateOrUpdate, List<DNetValidationResult> validationResults, ref AssistServiceIncomingBPIF AssistServiceIncomingBPIF, bool isCreate = true)
        {
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            var innerQueryCriteria = string.Empty;
            var page = 1;
            var size = 20;
            var criterias = "";
            criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "KodeChassis", objCreateOrUpdate.KodeChassis.ToString(), false, criterias);
            criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "NoWorkOrder", objCreateOrUpdate.NoWorkOrder, false, criterias);
            criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "WorkOrderCategoryCode", objCreateOrUpdate.WorkOrderCategoryCode, false, criterias);
            criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", objCreateOrUpdate.DealerCode, false, criterias);
            criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ValidateSystemStatus", objCreateOrUpdate.ValidateSystemStatus.ToString(), false, criterias);
            criterias = criterias.Replace("BPIF", "BP");

            List<AssistServiceIncomingBPIF> data = _AssistServiceIncomingBPRepo.Search(
                                criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

            if (isCreate)
            {
                if (data.Count > 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgServiceIncomingBPExists, objCreateOrUpdate.KodeChassis, objCreateOrUpdate.NoWorkOrder, objCreateOrUpdate.WorkOrderCategoryCode)));
                    return false;
                }
            }
            else
            {
                if (data.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgServiceIncomingBPNotExists, objCreateOrUpdate.KodeChassis, objCreateOrUpdate.NoWorkOrder, objCreateOrUpdate.WorkOrderCategoryCode)));
                    return false;
                }

                AssistServiceIncomingBPIF = data.FirstOrDefault() as AssistServiceIncomingBPIF;
            }

            return true;
        }

        private bool ValidateDataDamageCategory(AssistServiceIncomingBPParameterDto objCreateOrUpdate, List<DNetValidationResult> validationResults)
        {
            string category = "AssistServiceIncomingBP.DamageCategory";
            string code = objCreateOrUpdate.DamageCategory;

            StandardCodeDto sd = new StandardCodeDto();
            try
            {
                sd = _enumBL.GetByCategoryAndCode(category, code);

                if (sd != null)
                {
                    string damageCategory = sd.ValueCode;
                    string damageCategoryDesc = sd.ValueDesc;

                    decimal Ringan = 5000000;
                    decimal Berat = 10000000;
                    if (objCreateOrUpdate.TotalLC <= Ringan && damageCategory == "Ringan")
                        return true;
                    else if ((objCreateOrUpdate.TotalLC > Ringan && objCreateOrUpdate.TotalLC <= Berat) && damageCategory == "Sedang")
                        return true;
                    else if (objCreateOrUpdate.TotalLC > Berat && damageCategory == "Berat")
                        return true;
                    else
                    {
                        validationResults.Add(new DNetValidationResult("Untuk DamageCategory " + damageCategory + " maka " + damageCategoryDesc));
                        return false;
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.InvalidJenisKerusakan)));
                    return false;
                }
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.InvalidJenisKerusakan)));
                return false;
            }
        }

        private bool GetServiceIncomingBP(AssistServiceIncomingBPParameterDto objDelete, List<DNetValidationResult> validationResults, ref AssistServiceIncomingBPIF AssistServiceIncomingBPIF)
        {
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            var innerQueryCriteria = string.Empty;
            var page = 1;
            var size = 20;
            var criterias = "";
            criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "NoWorkOrder", objDelete.NoWorkOrder, false, criterias);
            criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "StatusAktif", "1", false, criterias);
            criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", objDelete.DealerCode, false, criterias);
            criterias = criterias.Replace("BPIF", "BP");

            List<AssistServiceIncomingBPIF> data = _AssistServiceIncomingBPRepo.Search(
                                criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

            if (data.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgServiceIncomingBPNotExists2, objDelete.NoWorkOrder)));
                return false;
            }

            AssistServiceIncomingBPIF = data.FirstOrDefault() as AssistServiceIncomingBPIF;

            return true;
        }

        private bool GetServiceIncomingBPUpdate(AssistServiceIncomingBPParameterDto objDelete, List<DNetValidationResult> validationResults, ref AssistServiceIncomingBPIF AssistServiceIncomingBPIF)
        {
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            var innerQueryCriteria = string.Empty;
            var page = 1;
            var size = 20;
            var criterias = "";
            criterias = Helper.UpdateStrCriteria(typeof(AssistServiceIncomingBPIF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ID", objDelete.ID.ToString(), false, criterias);
            criterias = criterias.Replace("BPIF", "BP");

            List<AssistServiceIncomingBPIF> data = _AssistServiceIncomingBPRepo.Search(
                                criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

            if (data.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgServiceIncomingBPNotExists2, objDelete.NoWorkOrder)));
                return false;
            }

            AssistServiceIncomingBPIF = data.FirstOrDefault() as AssistServiceIncomingBPIF;

            return true;
        }

        /// <summary>
        /// Validate waktu masuk keluar
        /// </summary>
        /// <param name="objCreateOrUpdate"></param>
        /// <param name="isValid"></param>
        /// <param name="validationResults"></param>
        private bool ValidateWktMasukWktKeluar(AssistServiceIncomingBPParameterDto objCreateOrUpdate, List<DNetValidationResult> validationResults)
        {
            if (objCreateOrUpdate.WaktuKeluar < objCreateOrUpdate.WaktuMasuk)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.InvalidWktMasukWktKeluar, objCreateOrUpdate.WaktuKeluar, objCreateOrUpdate.WaktuMasuk)));
            }

            return validationResults.Count == 0;
        }

        private bool ValidateVehicleModel(AssistServiceIncomingBPParameterDto objCreateOrUpdate, List<DNetValidationResult> validationResults)
        {
            ChassisMaster cM = GetChassisMaster(objCreateOrUpdate.KodeChassis.ToString());
            if (cM == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format("Data KodeChassis '{0}' tidak ditemukan didata master", objCreateOrUpdate.KodeChassis)));
            }
            else
            {
                if (cM.VechileColor != null)
                {
                    if (cM.VechileColor.VechileType != null)
                    {
                        if (cM.VechileColor.VechileType.VechileModel != null)
                        {
                            if (cM.VechileColor.VechileType.VechileModel.IndDescription != null)
                            {
                                string VehicleModelbyChassis = cM.VechileColor.VechileType.VechileModel.IndDescription;
                                if (objCreateOrUpdate.VehicleColorDesc != VehicleModelbyChassis)
                                {
                                    objCreateOrUpdate.VehicleModelDesc = VehicleModelbyChassis;
                                }
                            }
                        }
                    }
                }
            }

            if (objCreateOrUpdate.VehicleModelDesc.Length > 30)
            {
                validationResults.Add(new DNetValidationResult(string.Format("Data VehicleModelDesc '{0}' maksimum 30 char", objCreateOrUpdate.VehicleModelDesc)));
            }

            return validationResults.Count == 0;
        }
        #endregion
    }
}
