#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKChassis business logic class
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SPKChassisBL : AbstractBusinessLogic, ISPKChassisBL
    {
        #region Variables
        private readonly IMapper _invoiceRevisionMapper;
        private readonly IMapper _dealerSystemMapper;
        private readonly IMapper _spkChassisMapper;
        private readonly IMapper _revisionFakturMapper;
        private readonly IMapper _chassisMapper;
        private readonly IMapper _vehicleColorMapper;
        private readonly IMapper _vehicleTypeMapper;
        private readonly IMapper _spkHeaderMapper;
        private readonly IMapper _spkDetailMapper;
        private readonly IMapper _dealerMapper;
        private StandardCodeBL _enumBL;
        private readonly AutoMapper.IMapper _mapper;

        #endregion

        #region Constructor
        public SPKChassisBL()
        {
            _invoiceRevisionMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_InvoiceRevision).ToString());
            _dealerSystemMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerSystems).ToString());
            _spkChassisMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKChassis).ToString());
            _revisionFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(RevisionFaktur).ToString());
            _chassisMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            _vehicleColorMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            _vehicleTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _spkHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKHeader).ToString());
            _spkDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetail).ToString());
            _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());

            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new SPKChassis
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SPKChassisDto> Create(SPKChassisParameterDto objCreate)
        {
            #region Initialization
            var result = new ResponseBase<SPKChassisDto>();
            string vehicleColorCode = string.Empty;
            SPKDetail spkDetail = null;
            SPKHeader spkHeader = null;
            ChassisMaster chassisMaster = null;
            string vehicleTypeCode = string.Empty;
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            var isValidExist = false;
            int idexist = 0;
            #endregion

            try
            {
                // get chassis master
                if (isValid) { isValid = ValidationHelper.ValidateChassisMaster(objCreate.ChassisNumber, validationResults, ref chassisMaster); }

                // validate vehicle color and type
                if (isValid) { isValid = ValidateVehicleColorAndType(ref vehicleColorCode, chassisMaster, ref vehicleTypeCode, validationResults); }

                // check
                if (isValid)
                {
                    // get spk header
                    isValid = ValidationHelper.ValidateSPKHeader(objCreate.SPKNumber, validationResults, ref spkHeader);

                    if (isValid && (objCreate.MatchingType == 1 || objCreate.MatchingType == 3))
                    {
                        StandardCodeDto batalStatus = _enumBL.GetByCategoryAndCode("EnumStatusSPK.Status", "Batal");
                        string batalStatusSrt = batalStatus.ValueId.ToString();

                        // SPK Status should not Batal (3)
                        if (spkHeader.Status == batalStatusSrt)
                        {
                            isValid = false;
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, MessageResource.ErrorMsgCanceledSPKCouldNotBeMatched));
                        }
                    }

                    if (isValid)
                    {
                        // get spk detail
                        // update impacted by  CRSPKFaktur (user provide SPKDetailID)
                        isValid = ValidationHelper.ValidateSPKDetailByTypeCode(vehicleTypeCode, spkHeader, objCreate.SPKDetailID, validationResults, ref spkDetail);

                        if (isValid)
                        {
                            if (objCreate.MatchingType != 2)
                                isValid = ValidateSPKDetailQuantity(spkDetail, validationResults, chassisMaster.ID, objCreate.MatchingNumber,objCreate.MatchingType);
                        }
                    }
                }

                // proceed if no errors
                if (isValid)
                {
                    // create spkchassis object
                    var newSPKChassis = _mapper.Map<SPKChassis>(objCreate);

                    // update the other properties                 
                    newSPKChassis.LastUpdateBy = DNetUserName;
                    newSPKChassis.LastUpdateTime = DateTime.Now;
                    newSPKChassis.SPKDetail = spkDetail;
                    newSPKChassis.ChassisMaster = chassisMaster;

                    // check if the data already exist
                    int spkChassisId;
                    string createdBy;
                    DateTime createdTime;
                    bool isExist = IsDataExist(spkDetail, chassisMaster, validationResults, out spkChassisId, out createdBy, out createdTime);

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<SPKChassisDto>(validationResults, null);
                    }

                    if (isExist)
                    {
                        newSPKChassis.ID = spkChassisId;
                        newSPKChassis.CreatedBy = createdBy;
                        newSPKChassis.CreatedTime = createdTime;
                    }
                    else
                    {
                        newSPKChassis.CreatedTime = DateTime.Now;
                        newSPKChassis.CreatedBy = DNetUserName;
                    }

                    // validate if it is spk match faktur
                    bool isSPKMatchFaktur = ValidateDealerSystems();

                    //validasi existing
                    if (isValid)
                    {
                        isValid = ValidateSPKExist(objCreate, validationResults, ref idexist);
                        if (isValid)
                        {
                            isValidExist = true;
                        }
                    }

                    #region Matching Validation
                    // if match
                    if(!isValidExist)
                    {
                        if (objCreate.MatchingType == 1)
                        {
                            // validate match
                            ValidateMatch(objCreate, validationResults, newSPKChassis, isExist, isSPKMatchFaktur, ref result);
                        }
                        // if unmatch
                        else if (objCreate.MatchingType == 2)
                        {
                            // update if exist
                            if (isExist)
                            {
                                newSPKChassis.RowStatus = (short)DBRowStatus.Deleted;
                                UpdateSPKChassis(result, newSPKChassis);
                            }
                            else
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, "SPKChassis", "ChassissNumber = " + objCreate.ChassisNumber)));
                            }
                        }
                        // if rematch
                        else
                        {
                            // validate rematch
                            ValidateMatch(objCreate, validationResults, newSPKChassis, isExist, isSPKMatchFaktur, ref result, false);
                        }
                    }
                    #endregion
                }
                if(isValidExist)
                {
                    if(!validationResults.Any())
                    {
                        result.success = true;
                        result.total = 1;
                        result._id = idexist;
                        var x = new MessageBase();
                        x.ErrorCode = ErrorCode.Er_OK;
                        x.ErrorMessage = "Data dengan SPKDetailID " + objCreate.SPKDetailID + " ,Nomor Rangka " + objCreate.ChassisNumber + " ,Matching Type " + objCreate.MatchingType + " dan Matching Number " + objCreate.MatchingNumber + " already exist";
                        result.messages.Add(x);
                    }
                }
                if (validationResults.Any())
                {
                    return PopulateValidationError<SPKChassisDto>(validationResults, null);
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
        /// Update SPKChassis
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SPKChassisDto> Update(SPKChassisParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Delete SPKChassis by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SPKChassisDto> Delete(int id)
        {
            var result = new ResponseBase<SPKChassisDto>();

            try
            {
                var spkchassis = (SPKChassis)_spkChassisMapper.Retrieve(id);
                if (spkchassis != null)
                {
                    spkchassis.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _spkChassisMapper.Update(spkchassis, DNetUserName);
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
        /// Get SPKChassis by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SPKChassisDto>> Read(SPKChassisFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SPKChassis), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SPKChassisDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SPKChassis), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SPKChassis), filterDto, sortColl);

                // get data
                var data = _spkChassisMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKChassis>().ToList();
                    var listData = new List<SPKChassisDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var spkchassisDto = _mapper.Map<SPKChassisDto>(item);
                        spkchassisDto.ChassisMasterID = item.ChassisMaster.ID;
                        spkchassisDto.SPKDetailID = item.SPKDetail.ID;

                        // validate
                        var validSPKChassis = ValidateByUserDealerCode(filterDto, spkchassisDto);
                        if (validSPKChassis != null)
                            // add to list
                            listData.Add(spkchassisDto);
                    }

                    if (listData.Count > 0)
                    {
                        result.lst = listData;
                        result.total = totalRow;
                    }
                    else
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKChassis), filterDto);
                    }
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKChassis), filterDto);
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

        public ResponseBase<List<ValidateChassisDto>> ValidateChassis(List<ValidateChassisParameterDto> param)
        {
            #region Initialization
            var result = new ResponseBase<List<ValidateChassisDto>>();
            string vehicleColorCode = string.Empty;
            ChassisMaster chassisMaster = null;
            string vehicleTypeCode = string.Empty;
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            #endregion

            foreach(ValidateChassisParameterDto item in param)
            {
                isValid = true;
                chassisMaster = null;
                // get chassis master
                if (isValid)
                {
                    isValid = ValidationHelper.ValidateChassisMasterAll(item.ChassisNumber, validationResults, ref chassisMaster);
                    if(chassisMaster!=null)
                    {
                        isValid = true;
                    }
                    if(isValid==true)
                    {
                        if (item.EngineNumber != chassisMaster.EngineNumber)
                        {
                            isValid = false;
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataReferenceNotMatch, "Nomor Mesin "+item.EngineNumber+" tidak valid untuk Nomor Rangka "+item.ChassisNumber));
                        }
                    }
                }

                // validate vehicle color and type
                if (isValid)
                {
                    isValid = ValidateVehicleColorAndType(ref vehicleColorCode, chassisMaster, ref vehicleTypeCode, validationResults);
                    if(!String.IsNullOrEmpty(vehicleColorCode) || !String.IsNullOrEmpty(vehicleColorCode))
                    {
                        isValid = true;
                    }
                    if(isValid==true)
                    {
                        if (item.VehicleColorCode != vehicleColorCode)
                        {
                            isValid = false;
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataReferenceNotMatch, "Warna Kendaraan "+item.VehicleColorCode+" tidak valid untuk Nomor Rangka "+item.ChassisNumber));
                        }
                        else
                        {
                            if (item.SPKDetailID != 0)
                            {
                                isValid = ValidateSPKDetailID(item.SPKDetailID, validationResults, vehicleTypeCode, item.ChassisNumber);
                            }

                            if(isValid)
                            {
                                if (item.VehicleTypeCode != vehicleTypeCode)
                                {
                                    isValid = false;
                                    validationResults.Add(new DNetValidationResult(ErrorCode.DataReferenceNotMatch, "Tipe Kendaraan " + item.VehicleTypeCode + " tidak valid untuk Nomor Rangka " + item.ChassisNumber));
                                }
                                else
                                {
                                    result.total += 1;
                                    validationResults.Add(new DNetValidationResult(ErrorCode.Er_OK, "Data Nomor Rangka " + item.ChassisNumber + " valid"));
                                }
                            }
                            
                        }
                    }
                } 
            }
            if(result.total==param.Count())
            {
                result.success = true;
            }else
            {
                result.success = false;
            }
            return PopulateValidationError<List<ValidateChassisDto>>(validationResults, null);

            return result;
        }

        

        #endregion

        #region Private Methods
        /// <summary>
        /// Validate 
        /// </summary>
        /// <param name="vehicleColorCode"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="vehicleTypeCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateVehicleColorAndType(ref string vehicleColorCode, ChassisMaster chassisMaster, ref string vehicleTypeCode, List<DNetValidationResult> validationResults)
        {
            // get vehicle color
            VechileColor vehicleColor = _vehicleColorMapper.Retrieve(chassisMaster.VechileColor.ID) as VechileColor;
            if (vehicleColor != null)
            {
                vehicleColorCode = vehicleColor.ColorCode;
                // get vehicle type
                VechileType vehicleType = _vehicleTypeMapper.Retrieve(vehicleColor.VechileType.ID) as VechileType;
                if (vehicleType != null)
                {
                    vehicleTypeCode = vehicleType.VechileTypeCode;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.VechileTypeID, vehicleColor.VechileType.ID)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.VechileColorID, chassisMaster.VechileColor.ID)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate SPK detail ID
        /// </summary>
        /// <param name="sPKDetailID"></param>
        /// <returns></returns>
        private bool ValidateSPKDetailID(int sPKDetailID, List<DNetValidationResult> validationResults, string vehicleTypeCode, string chassisnumber)
        {

            var criteria = new CriteriaComposite(new Criteria(typeof(SPKDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(SPKDetail), "ID", MatchType.Exact, sPKDetailID));
            var masters = _spkDetailMapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                var spkdetail = masters[0] as SPKDetail;
                var vehicletypecode_spkdetail = spkdetail.VehicleTypeCode;
                if (vehicleTypeCode.ToString() != vehicletypecode_spkdetail.ToString())
                {
                    validationResults.Add(new DNetValidationResult("Vehicle Type Code " + vehicletypecode_spkdetail + " pada SPKDetailID " + sPKDetailID + " tidak sesuai dengan Vehicle Type Code " + vehicleTypeCode + " pada Chassis " + chassisnumber));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SPKDetail, sPKDetailID)));
            }
            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate match and rematch
        /// </summary>
        /// <param name="result"></param>
        /// <param name="validationResults"></param>
        /// <param name="newSPKChassis"></param>
        /// <param name="isExist"></param>
        /// <param name="isSPKMatchFaktur"></param>
        /// <param name="isMatch"></param>
        /// <param name="param"></param>
        private void ValidateMatch(SPKChassisParameterDto param, List<DNetValidationResult> validationResults, SPKChassis newSPKChassis, bool isExist, bool isSPKMatchFaktur, ref ResponseBase<SPKChassisDto> result, bool isMatch = true)
        {
            if (isMatch)
            {
                // match validation
                if (string.IsNullOrEmpty(newSPKChassis.MatchingNumber))
                    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgMatchingNumberNotFound));
            }
            else
            {
                // rematch validation
                if (!string.IsNullOrEmpty(newSPKChassis.ReferenceNumber))
                {
                    int relatedDataID;

                    // if contain reference number and has related data then delete it
                    if (IsDataExistByReferenceNumber(newSPKChassis.ReferenceNumber, out relatedDataID))
                    {
                        Delete(relatedDataID);
                    }
                    else
                    {
                        // if reference number invalid don't insert or update
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgRefNumberNotFound, newSPKChassis.ReferenceNumber)));
                    }
                }
                else
                {
                    // rematching should provide reference number
                    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgRematchButRefNumberNotFound));
                }
            }

            // if no errors
            if (validationResults.Count == 0)
            {
                bool isHaveRevisionFaktur = false;
                RevisionFaktur newRevisionFaktur = new RevisionFaktur();

                // if spk match faktur
                if (isSPKMatchFaktur)
                {
                    // check invoice revision
                    var invoiceRevisions = _invoiceRevisionMapper.RetrieveByCriteria(Helper.GenerateCriteriaAllStatus(typeof(VWI_InvoiceRevision), "ChassisNumber", param.ChassisNumber));
                    if (invoiceRevisions.Count > 0)
                    {
                        // get the latest
                        var invoiceRevisionsList = invoiceRevisions.Cast<VWI_InvoiceRevision>().ToList();
                        var invoiceRev = invoiceRevisionsList.OrderByDescending(x => x.RevisionDate).FirstOrDefault();

                        // error if spk number is null or empty
                        if (string.IsNullOrEmpty(invoiceRev.SPKNumber))
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSPKNumberInInvoiceRevisionNull, param.ChassisNumber)));
                        }
                        // error if chassisnumber and spknumber does not match
                        else if (!(invoiceRev.SPKNumber.Equals(param.SPKNumber, StringComparison.OrdinalIgnoreCase) && invoiceRev.ChassisNumber.Equals(param.ChassisNumber, StringComparison.OrdinalIgnoreCase)))
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgChassisAndSPKNotMatch, param.ChassisNumber, param.SPKNumber)));
                        }

                        // update remarks = blank on matching
                        isHaveRevisionFaktur = true;
                        newRevisionFaktur = (RevisionFaktur)_revisionFakturMapper.Retrieve(invoiceRev.ID);
                        if(newRevisionFaktur != null)
                        {
                            newRevisionFaktur.Remark = string.Empty;
                            newRevisionFaktur.LastUpdateBy = DNetUserName;
                        }
                    }
                }

                // update if exist

                if (validationResults.Count == 0)
                {
                    if (isExist)
                    {
                        UpdateSPKChassis(result, newSPKChassis);
                    }
                    else
                    {
                        InsertSPKChassis(result, newSPKChassis);
                    }

                    // update remark revision faktur
                    if (isHaveRevisionFaktur)
                    {
                        UpdateRevisionFaktur(newRevisionFaktur);
                    }
                }
            }
        }

        /// <summary>
        /// Validate SPK Detail Quantity
        /// </summary>
        /// <param name="spkDetail"></param>
        /// <returns></returns>
        private bool ValidateSPKDetailQuantity(SPKDetail spkDetail, List<DNetValidationResult> validationResults, int chassismasterID, string matchingNumber, int matchingType)
        {
            // validate total quantity of spk detail
            string spName = "up_RetrieveTotalMatchedSPKDetailSPKChassis";
            ArrayList param = new ArrayList();
            SqlParameter sl = new SqlParameter();
            sl.ParameterName = "@spkDetailID";
            var isvalid = false;

            sl.Value = spkDetail.ID;
            param.Add(sl);
            ArrayList listOfMatchedSPKChassis = _spkChassisMapper.RetrieveSP(spName, param);
            if (listOfMatchedSPKChassis.Count>0)
            {
                foreach (var i in listOfMatchedSPKChassis)
                {
                    var matchingspkchassis = i as SPKChassis;
                    if (matchingspkchassis.SPKDetail.ID == spkDetail.ID && matchingspkchassis.MatchingNumber == matchingNumber && matchingspkchassis.MatchingType == matchingType && matchingspkchassis.ChassisMaster.ID == chassismasterID)
                    {
                        isvalid = true;
                    }
                    else
                    {
                        if (isvalid != true)
                        {
                            isvalid = false;
                            validationResults.Add(new DNetValidationResult(
                            ErrorCode.DataTypeOrDataFormatInvalid,
                            string.Format(MessageResource.ErrorMsgSpkDetailHasAlreadyBeenMatched, spkDetail.VehicleTypeCode, spkDetail.VehicleColorCode)
                            ));
                        }
                    }
                }
                //if (!(spkDetail.Quantity > listOfMatchedSPKChassis.Count))
                //{
                //    validationResults.Add(new DNetValidationResult(
                //        ErrorCode.DataTypeOrDataFormatInvalid,
                //        string.Format(MessageResource.ErrorMsgSpkDetailHasAlreadyBeenMatched, spkDetail.VehicleTypeCode, spkDetail.VehicleColorCode)
                //        ));
                //    return false;
                //}
            }
            else
            {
                isvalid = true;
                //validationResults.Add(new DNetValidationResult("SPK Detail dengan tipe " + spkDetail.VehicleTypeCode + " dan warna " + spkDetail.VehicleColorCode + " sudah di matching dengan SPK lain"));
            }

            return isvalid;
        }

        /// <summary>
        /// Check the spk match faktur status
        /// </summary>
        /// <returns></returns>
        private bool ValidateDealerSystems()
        {
            int dealerID = GetCurrentDealerID();
            if (dealerID == 0)
                return false;

            var dealerSystems = _dealerSystemMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(DealerSystems), "Dealer.ID", dealerID));
            if (dealerSystems.Count > 0)
            {
                var dealer = (dealerSystems[0] as DealerSystems);
                return dealer.isSPKMatchFaktur;
            }

            return false;
        }

        /// <summary>
        /// Get current active dealer id
        /// </summary>
        /// <returns></returns>
        private int GetCurrentDealerID()
        {
            // get dealer ID
            var dealers = _dealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", this.DealerCode));
            if (dealers.Count > 0)
                return (dealers[0] as Dealer).ID;
            else
                return 0;
        }

        /// <summary>
        /// Validate by dealer id
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="chassisDto"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private SPKChassisDto ValidateByUserDealerCode(SPKChassisFilterDto filterDto, SPKChassisDto chassisDto)
        {
            int dealerID = GetCurrentDealerID();
            if (dealerID == 0)
                return null;

            // get valid chassis master
            var chassisMaster = _chassisMapper.Retrieve(chassisDto.ChassisMasterID);
            if (chassisMaster != null && dealerID == (chassisMaster as ChassisMaster).Dealer.ID)
            {
                // update 
                chassisDto.ChassisNumber = (chassisMaster as ChassisMaster).ChassisNumber;
                chassisDto.DealerCode = this.DealerCode;

                // get spk number
                var spkDetail = _spkDetailMapper.Retrieve(chassisDto.SPKDetailID);
                if (spkDetail != null)
                {
                    var spkHeader = _spkHeaderMapper.Retrieve((spkDetail as SPKDetail).SPKHeader.ID);
                    if (spkHeader != null)
                        chassisDto.SPKNumber = (spkHeader as SPKHeader).SPKNumber;
                }
            }

            return chassisDto;
        }

        /// <summary>
        /// Insert new SPK Chassis
        /// </summary>
        /// <param name="result"></param>
        /// <param name="newSPKChassis"></param>
        private void InsertSPKChassis(ResponseBase<SPKChassisDto> result, SPKChassis newSPKChassis)
        {
            // insert new spkchassis object
            var succeed = _spkChassisMapper.Insert(newSPKChassis, DNetUserName);
            result.success = succeed > 0;

            if (!result.success) ErrorMsgHelper.DataCorrupt(result.messages);

            // return output ID
            result._id = succeed;
            result.total = 1;
        }

        /// <summary>
        /// Update SPKChassis data
        /// </summary>
        /// <param name="result"></param>
        /// <param name="newSPKChassis"></param>
        private void UpdateSPKChassis(ResponseBase<SPKChassisDto> result, SPKChassis newSPKChassis)
        {
            var success = (int)_spkChassisMapper.Update(newSPKChassis, DNetUserName);
            result.success = success > 0;
            if (!result.success)
                ErrorMsgHelper.UpdateNotAvailable(result.messages);
            // return output ID
            result._id = newSPKChassis.ID;
            result.total = 1;
        }

        private void UpdateRevisionFaktur(RevisionFaktur newRevisionFaktur)
        {
            _revisionFakturMapper.Update(newRevisionFaktur, DNetUserName);            
        }

        /// <summary>
        /// Check if data already exist in db 
        /// </summary>
        /// <param name="spkDetail"></param>
        /// <param name="chassisMaster"></param>
        /// <returns></returns>
        private bool IsDataExist(SPKDetail spkDetail, ChassisMaster chassisMaster, List<DNetValidationResult> validationResults, out int id, out string createdBy, out DateTime createdTime)
        {
            id = 0;
            createdBy = string.Empty;
            createdTime = DateTime.MinValue;
            var criteriasChassis = new CriteriaComposite(new Criteria(typeof(SPKChassis), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriasChassis.opAnd(new Criteria(typeof(SPKChassis), "SPKDetail.ID", MatchType.Exact, spkDetail.ID));
            criteriasChassis.opAnd(new Criteria(typeof(SPKChassis), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            ArrayList oldChassis = _spkChassisMapper.RetrieveByCriteria(criteriasChassis);
            if (oldChassis.Count > 0)
            {
                id = (oldChassis[0] as SPKChassis).ID;
                createdBy = (oldChassis[0] as SPKChassis).CreatedBy;
                createdTime = (oldChassis[0] as SPKChassis).CreatedTime;
                return true;
            }
            else
            {
                // just in case already used by another spk
                //var criteriasChassisExist = new CriteriaComposite(new Criteria(typeof(SPKChassis), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                //criteriasChassisExist.opAnd(new Criteria(typeof(SPKChassis), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
                //oldChassis = _spkChassisMapper.RetrieveByCriteria(criteriasChassisExist);
                //if (oldChassis.Count > 0)
                //{
                //    SPKChassis spkChassis = oldChassis[0] as SPKChassis;
                //    if (spkChassis.SPKDetail != null && spkChassis.SPKDetail.SPKHeader != null)
                //        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSpkChassisExistDiffSPK, chassisMaster.ChassisNumber, spkChassis.SPKDetail.SPKHeader.SPKNumber)));
                //    else
                //        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSpkChassisAlreadyUsed, chassisMaster.ChassisNumber, "")));
                //}

                var criteriasChassisExist = new CriteriaComposite(new Criteria(typeof(SPKChassis), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));

                //sort.add(new Sort(SPKChassis, "ID", Sort.SortDirection.DESC));
                oldChassis = _spkChassisMapper.RetrieveByCriteria(criteriasChassisExist);
                SPKChassis spkChassis = null;
                if (oldChassis.Count > 0)
                {
                    if (oldChassis.Count == 1)
                    {
                        spkChassis = oldChassis[0] as SPKChassis;
                        if (spkChassis.MatchingType ==2)
                        {
                            spkChassis = null;
                        }
                    }
                    else
                    {
                        spkChassis = ValidateSPKChassisExist(oldChassis);
                    }

                    if (spkChassis != null)
                        if (spkChassis.SPKDetail != null && spkChassis.SPKDetail.SPKHeader != null)
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSpkChassisExistDiffSPK, chassisMaster.ChassisNumber, spkChassis.SPKDetail.SPKHeader.SPKNumber)));
                        else
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSpkChassisAlreadyUsed, chassisMaster.ChassisNumber, "")));
                }
            }

            return false;
        }

        private SPKChassis ValidateSPKChassisExist(ArrayList listDataSPKChassis)
        {
            SPKChassis dataSPKChassis = null;
            ArrayList spkChassisMatch = new ArrayList();

            for (int i = 0; i < listDataSPKChassis.Count; i++)
            {
                if (((SPKChassis)listDataSPKChassis[i]).MatchingType == 1 || ((SPKChassis)listDataSPKChassis[i]).MatchingType == 3)
                    spkChassisMatch.Add(listDataSPKChassis[i]);
            }

            for (int i = spkChassisMatch.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < listDataSPKChassis.Count; j++)
                {
                    if (spkChassisMatch.Count > 0)
                    {
                        if (((SPKChassis)spkChassisMatch[i]).ChassisMaster.ChassisNumber == ((SPKChassis)listDataSPKChassis[j]).ChassisMaster.ChassisNumber && ((SPKChassis)spkChassisMatch[i]).SPKDetail.SPKHeader.SPKNumber == ((SPKChassis)listDataSPKChassis[j]).SPKDetail.SPKHeader.SPKNumber)
                        {
                            if (((SPKChassis)listDataSPKChassis[j]).MatchingType == 2)
                            {
                                spkChassisMatch.RemoveAt(i);
                                break;                            
                            }
                        }
                    }
                }
            }

            if (spkChassisMatch.Count > 0)
                dataSPKChassis = spkChassisMatch[0] as SPKChassis;

            return dataSPKChassis;
        }

        /// <summary>
        /// Check if data already exist in db by its matching number
        /// </summary>
        /// <param name="matchingNumber"></param>
        /// <returns></returns>
        private bool IsDataExistByReferenceNumber(string matchingNumber, out int relatedDataID)
        {
            relatedDataID = 0;
            var criteriasChassis = new CriteriaComposite(new Criteria(typeof(SPKChassis), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriasChassis.opAnd(new Criteria(typeof(SPKChassis), "MatchingNumber", MatchType.Exact, matchingNumber));

            ArrayList relatedData = _spkChassisMapper.RetrieveByCriteria(criteriasChassis);
            if (relatedData.Count > 0)
            {
                relatedDataID = (relatedData[0] as SPKChassis).ID;
            }

            return relatedData.Count > 0;
        }

        /// <summary>
        /// Check if data already exist in db by its matching number
        /// </summary>
        /// <param name="matchingNumber"></param>
        /// <returns></returns>
        private bool ValidateSPKExist(SPKChassisParameterDto item, List<DNetValidationResult> validationResults, ref int idexist)
        {
            var isexist = false;

            var criteriasChassis = new CriteriaComposite(new Criteria(typeof(SPKChassis), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriasChassis.opAnd(new Criteria(typeof(SPKChassis), "MatchingNumber", MatchType.Exact, item.MatchingNumber));
            criteriasChassis.opAnd(new Criteria(typeof(SPKChassis), "MatchingType", MatchType.Exact, item.MatchingType));
            criteriasChassis.opAnd(new Criteria(typeof(SPKChassis), "SPKDetail.ID", MatchType.Exact, item.SPKDetailID));
            criteriasChassis.opAnd(new Criteria(typeof(SPKChassis), "ChassisMaster.ChassisNumber", MatchType.Exact, item.ChassisNumber));

            ArrayList relatedData = _spkChassisMapper.RetrieveByCriteria(criteriasChassis);
            if (relatedData.Count > 0)
            {
                idexist = (relatedData[0] as SPKChassis).ID;
                isexist = true;
            }
            return isexist;
        }

        #endregion
    }
}

