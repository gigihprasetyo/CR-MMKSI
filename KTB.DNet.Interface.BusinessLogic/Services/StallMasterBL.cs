#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Stall Master business logic class
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
#endregion


namespace KTB.DNet.Interface.BusinessLogic
{
    public class StallMasterBL : AbstractBusinessLogic, IStallMasterBL
    {
        #region Variables
        private readonly IMapper _stallMasterMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private readonly IMapper _allocationReallTimeServiceMapper;
        #endregion

        #region Constructor
        public StallMasterBL()
        {
            _stallMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(StallMaster).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _allocationReallTimeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(AllocationRealTimeService).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get StallMaster by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<StallMasterDto>> Read(StallMasterFilterDto filterDto, int pageSize)
        {

            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(StallMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(StallMaster), "Dealer.DealerCode", MatchType.Exact, this.DealerCode));
            var result = new ResponseBase<List<StallMasterDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(StallMaster), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(StallMaster), filterDto, sortColl);

                // get data
                var data = _stallMasterMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<StallMaster>().ToList();
                    var listData = list.Select(item => _mapper.Map<StallMasterDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(StallMaster), filterDto);
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

        public ResponseBase<StallMasterDto> Create(StallMasterParameterDto objCreate)
        {
            #region Declare
            var result = new ResponseBase<StallMasterDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            StallMaster stallMaster;
            var stallCode = string.Empty;
            #endregion

            try
            {
                //validate Stall Master 
                isValid = ValidateStallMaster(objCreate, validationResults, out stallMaster);

                //generate stall code
                if (isValid)
                {
                    stallCode = GenerateStallCode(stallMaster.Dealer.ID, validationResults);
                }
                if (string.IsNullOrEmpty(stallCode))
                {
                    isValid = false;
                }

                // insert if valid
                if (isValid)
                {
                    StallMaster stallmaster = null;
                    stallmaster = _mapper.Map<StallMaster>(objCreate);
                    stallmaster.StallCode = stallCode;
                    stallmaster.Dealer = stallMaster.Dealer;
                    stallmaster.CreatedBy = DNetUserName;
                    stallmaster.CreatedTime = DateTime.Now;
                    stallmaster.LastUpdatedBy = DNetUserName;
                    stallmaster.LastUpdatedTime = DateTime.Now;

                    int id = _stallMasterMapper.Insert(stallmaster, DNetUserName);

                    result.success = id > 0;
                    if (!result.success)
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }

                    result._id = id;
                    result.total = 1;
                    result.lst = _mapper.Map<StallMasterDto>(stallmaster);
                    result.lst.ID = id;
                }
                else
                {
                    return PopulateValidationError<StallMasterDto>(validationResults, null);
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

        public ResponseBase<StallMasterDto> Update(StallMasterUpdateParameterDto objUpdate)
        {
            #region declare
            var result = new ResponseBase<StallMasterDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            StallMaster stallMaster;
            StallMaster stallMasterExist = null;
            #endregion

            try
            {
                //Validate Stall Master
                isValid = ValidateStallMasterUpdate(objUpdate, validationResults, out stallMaster);

                //Data is Exist 
                var criterias = new CriteriaComposite(new Criteria(typeof(StallMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(StallMaster), "ID", MatchType.Exact, objUpdate.ID));

                var data = _stallMasterMapper.RetrieveByCriteria(criterias);
                if (data.Count == 0)
                {
                    isValid = false;
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.ID) });
                }
                else
                {
                    stallMasterExist = (StallMaster)data[0];
                }
                if (stallMasterExist.StallCode != objUpdate.StallCode)
                {
                    isValid = false;
                    validationResults.Add(new DNetValidationResult("Stall Code tidak sesuai dengan ID yang dimasukkan."));
                }

                // if valid and data is exist then update data
                if (isValid)
                {
                    var newStallMaster = _mapper.Map<StallMaster>(stallMasterExist);
                    newStallMaster.StallCodeDealer = stallMaster.StallCodeDealer;
                    newStallMaster.StallName = stallMaster.StallName;
                    newStallMaster.StallLocation = stallMaster.StallLocation;
                    newStallMaster.StallType = stallMaster.StallType;
                    newStallMaster.StallCategory = stallMaster.StallCategory;
                    newStallMaster.IsBodyPaint = stallMaster.IsBodyPaint;
                    newStallMaster.Status = stallMaster.Status;
                    newStallMaster.LastUpdatedBy = DNetUserName;
                    newStallMaster.LastUpdatedTime = DateTime.Now;

                    var success = (int)_stallMasterMapper.Update(newStallMaster, DNetUserName);
                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<StallMasterDto>(validationResults, null);
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
        /// Delete Stall Master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<StallMasterDto> DeleteData(StallMasterDeleteParameterDto objDelete)
        {
            var result = new ResponseBase<StallMasterDto>();
            StallMaster stallMaster = null;
            StallMaster stallMasterRetrieve = null;
            var isValid = true;


            try
            {
                //Data is Exist 
                var criterias = new CriteriaComposite(new Criteria(typeof(StallMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(StallMaster), "Dealer.DealerCode", MatchType.Exact, objDelete.DealerCode));
                criterias.opAnd(new Criteria(typeof(StallMaster), "StallCode", MatchType.Exact, objDelete.StallCode));
                criterias.opAnd(new Criteria(typeof(StallMaster), "StallCodeDealer", MatchType.Exact, objDelete.StallCodeDealer));
                criterias.opAnd(new Criteria(typeof(StallMaster), "StallName", MatchType.Exact, objDelete.StallName));

                var data = _stallMasterMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    stallMaster = (StallMaster)data[0];
                }
                else
                {
                    isValid = false;
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.ID) });
                }
                if (isValid == true)
                {
                    stallMasterRetrieve = (StallMaster)_stallMasterMapper.Retrieve(stallMaster.ID);
                }
                if (stallMasterRetrieve != null)
                {
                    stallMasterRetrieve.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _stallMasterMapper.Update(stallMasterRetrieve, DNetUserName);
                    if (nResult != 0)
                    {
                        result.success = true;
                        result._id = stallMasterRetrieve.ID;
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

        public ResponseBase<StallMasterDto> Delete(int id)
        {
            throw new NotImplementedException();
        }
        public ResponseBase<StallMasterDto> Update(StallMasterParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        private bool ValidateStallMaster(StallMasterParameterDto objCreate, List<DNetValidationResult> validationResults, out StallMaster stallMaster)
        {
            bool isValid = true;
            stallMaster = _mapper.Map<StallMaster>(objCreate);

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                stallMaster.Dealer = dealer;
            }

            // Validate Stall Location
            var stallLocation = _enumBL.GetByCategoryAndValue("LokasiStall", objCreate.StallLocation.ToString());
            if (stallLocation != null)
            {
                stallMaster.StallLocation = stallLocation.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Location " + objCreate.StallLocation.ToString() + " tidak valid"));
            }

            //Validate Stall Type
            var stallType = _enumBL.GetByCategoryAndValue("TipeStall", objCreate.StallType.ToString());
            if (stallType != null)
            {
                stallMaster.StallType = stallType.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Type " + objCreate.StallType.ToString() + " tidak valid"));
            }

            //Validate Stall Type
            var stallCategory = _enumBL.GetByCategoryAndValue("KategoriStall", objCreate.StallCategory.ToString());
            if (stallCategory != null)
            {
                stallMaster.StallCategory = stallCategory.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Category " + objCreate.StallCategory.ToString() + " tidak valid"));
            }

            //Validate Is Body Paint
            var isBodyPaint = _enumBL.GetByCategoryAndValue("IsBodyPaint", objCreate.IsBodyPaint.ToString());
            if (isBodyPaint != null)
            {
                stallMaster.IsBodyPaint = isBodyPaint.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Is Body Paint " + objCreate.IsBodyPaint.ToString() + " tidak valid"));
            }

            //Validate Status
            var stallStatus = _enumBL.GetByCategoryAndValue("Status", objCreate.Status.ToString());
            if (stallStatus != null)
            {
                stallMaster.Status = stallStatus.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Status " + objCreate.Status.ToString() + " tidak valid"));
            }

            //Validate if StallType is RealTimeService check max allocation
            if (stallType != null)
            {
                if (stallType.ValueCode == "RealTimeService" && stallType.ValueId.ToString() == objCreate.StallType)
                {
                    var criteriaAllocation = new CriteriaComposite(new Criteria(typeof(AllocationRealTimeService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriaAllocation.opAnd(new Criteria(typeof(AllocationRealTimeService), "Dealer.DealerCode", MatchType.Exact, objCreate.DealerCode));

                    var AlokasiStall = _allocationReallTimeServiceMapper.RetrieveByCriteria(criteriaAllocation);
                    if (AlokasiStall != null && AlokasiStall.Count > 0)
                    {
                        AllocationRealTimeService alocationRTS = (AllocationRealTimeService)AlokasiStall[0];

                        var criterias = new CriteriaComposite(new Criteria(typeof(StallMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        criterias.opAnd(new Criteria(typeof(StallMaster), "Dealer.DealerCode", MatchType.Exact, objCreate.DealerCode));
                        criterias.opAnd(new Criteria(typeof(StallMaster), "StallType", MatchType.Exact, objCreate.StallType));

                        var data = _stallMasterMapper.RetrieveByCriteria(criterias);
                        if (data.Count >= alocationRTS.AlokasiStall)
                        {
                            validationResults.Add(new DNetValidationResult("Jumlah data Stall dengan StallType '" + objCreate.StallType + "' untuk Dealer '" + objCreate.DealerCode + "' tidak bisa melebihi batas AlokasiStall '" + alocationRTS.AlokasiStall.ToString() + "'"));
                        }
                    }
                }
            }

            if (validationResults.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }

        private bool ValidateStallMasterUpdate(StallMasterUpdateParameterDto objCreate, List<DNetValidationResult> validationResults, out StallMaster stallMaster)
        {
            bool isValid = true;
            stallMaster = _mapper.Map<StallMaster>(objCreate);

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                stallMaster.Dealer = dealer;
            }

            // Validate Stall Location
            var stallLocation = _enumBL.GetByCategoryAndValue("LokasiStall", objCreate.StallLocation.ToString());
            if (stallLocation != null)
            {
                stallMaster.StallLocation = stallLocation.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Location " + objCreate.StallLocation.ToString() + " tidak valid"));
            }

            //Validate Stall Type
            var stallType = _enumBL.GetByCategoryAndValue("TipeStall", objCreate.StallType.ToString());
            if (stallType != null)
            {
                stallMaster.StallType = stallType.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Type " + objCreate.StallType.ToString() + " tidak valid"));
            }

            //Validate Stall Type
            var stallCategory = _enumBL.GetByCategoryAndValue("KategoriStall", objCreate.StallCategory.ToString());
            if (stallCategory != null)
            {
                stallMaster.StallCategory = stallCategory.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Category " + objCreate.StallCategory.ToString() + " tidak valid"));
            }

            //Validate Is Body Paint
            var isBodyPaint = _enumBL.GetByCategoryAndValue("IsBodyPaint", objCreate.IsBodyPaint.ToString());
            if (isBodyPaint != null)
            {
                stallMaster.IsBodyPaint = isBodyPaint.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Is Body Paint " + objCreate.IsBodyPaint.ToString() + " tidak valid"));
            }

            //Validate Status
            var stallStatus = _enumBL.GetByCategoryAndValue("Status", objCreate.Status.ToString());
            if (stallStatus != null)
            {
                stallMaster.Status = stallStatus.ValueId.ToString();
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Status " + objCreate.Status.ToString() + " tidak valid"));
            }

            //Validate if StallType is RealTimeService check max allocation
            if (stallType != null)
            {
                if (stallType.ValueCode == "RealTimeService" && stallType.ValueId.ToString() == objCreate.StallType)
                {
                    var criteriaAllocation = new CriteriaComposite(new Criteria(typeof(AllocationRealTimeService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriaAllocation.opAnd(new Criteria(typeof(AllocationRealTimeService), "Dealer.DealerCode", MatchType.Exact, objCreate.DealerCode));

                    var AlokasiStall = _allocationReallTimeServiceMapper.RetrieveByCriteria(criteriaAllocation);
                    if (AlokasiStall != null && AlokasiStall.Count > 0)
                    {
                        AllocationRealTimeService alocationRTS = (AllocationRealTimeService)AlokasiStall[0];

                        var criterias = new CriteriaComposite(new Criteria(typeof(StallMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        criterias.opAnd(new Criteria(typeof(StallMaster), "Dealer.DealerCode", MatchType.Exact, objCreate.DealerCode));
                        criterias.opAnd(new Criteria(typeof(StallMaster), "StallType", MatchType.Exact, objCreate.StallType));
                        criterias.opAnd(new Criteria(typeof(StallMaster), "ID", MatchType.NotInSet, objCreate.ID));

                        var data = _stallMasterMapper.RetrieveByCriteria(criterias);
                        if (data.Count >= alocationRTS.AlokasiStall)
                        {
                            validationResults.Add(new DNetValidationResult("Jumlah data Stall dengan StallType '" + objCreate.StallType + "' untuk Dealer '" + objCreate.DealerCode + "' tidak bisa melebihi batas AlokasiStall '" + alocationRTS.AlokasiStall.ToString() + "'"));
                        }
                    }
                }
            }

            if (validationResults.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }
        private string GenerateStallCode(int DealerID, List<DNetValidationResult> validationResults)
        {
            var stallcode = string.Empty;
            var stallCodeGenerator = _stallMasterMapper.RetrieveDataSet("SELECT [dbo].[ufn_CreateStallCode] (" + DealerID + ") as stallcode");
            if (stallCodeGenerator.Tables.Count > 0)
            {
                if (stallCodeGenerator.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in stallCodeGenerator.Tables[0].Rows)
                    {
                        stallcode = item["stallcode"].ToString();
                    }
                }
            }

            if (string.IsNullOrEmpty(stallcode))
            {
                validationResults.Add(new DNetValidationResult("Stall Code melebihi batas penomoran yang ada."));
            }

            return stallcode;
        }


        #endregion
    }
}
