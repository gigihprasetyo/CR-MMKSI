
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StallWorkingTime business logic class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using System.Runtime.ExceptionServices;
using DomainIF = KTB.DNet.Interface.Domain;
#endregion


namespace KTB.DNet.Interface.BusinessLogic
{
    public class StallWorkingTimeBL : AbstractBusinessLogic, IStallWorkingTimeBL
    {
        #region Variables
        private readonly IMapper _stallWorkingTimeMapper;
        private readonly IMapper _stallMasterMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        private IStallWorkingTimeRepository<Domain.StallWorkingTime_IF, int> _StallWorkingTimeRepo;
        #endregion

        #region Constructor
        public StallWorkingTimeBL(IStallWorkingTimeRepository<Domain.StallWorkingTime_IF, int> StallWorkingTimeRepo)
        {
            _stallWorkingTimeMapper = MapperFactory.GetInstance().GetMapper(typeof(StallWorkingTime).ToString());
            _stallMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(StallMaster).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _StallWorkingTimeRepo = StallWorkingTimeRepo;

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create StallWorkingTime
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<StallWorkingTimeDto> Create(StallWorkingTimeParameterDto objCreate)
        {
            #region Declare
            var result = new ResponseBase<StallWorkingTimeDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            StallWorkingTime stallWorkingTime;
            #endregion

            try
            {
                //validate Stall Working Time
                isValid = ValidateStallWorkingTime(objCreate, validationResults, out stallWorkingTime);

                // insert if valid
                if (isValid)
                {
                    StallWorkingTime stallworkingtime = null;
                    stallworkingtime = _mapper.Map<StallWorkingTime>(objCreate);
                    stallworkingtime.Dealer = stallWorkingTime.Dealer;
                    stallworkingtime.StallMaster = stallWorkingTime.StallMaster;
                    stallworkingtime.CreatedBy = DNetUserName;
                    stallworkingtime.CreatedTime = DateTime.Now;
                    stallworkingtime.LastUpdatedBy = DNetUserName;
                    stallworkingtime.LastUpdatedTime = DateTime.Now;
                    if (!string.IsNullOrEmpty(objCreate.VisitType))
                    {
                        stallworkingtime.VisitType = Convert.ToInt16(objCreate.VisitType);
                    }
                    if (stallworkingtime.RestTimeStart.ToString("yyyy-MM-dd HH:mm:ss") == "1753-01-01 00:00:00")
                    {
                        DateTime s = DateTime.Now;
                        TimeSpan ts = new TimeSpan(0, 0, 0);
                        s = s.Date + ts;
                        stallworkingtime.RestTimeStart = s;
                    }
                    if (stallworkingtime.RestTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") == "1753-01-01 00:00:00")
                    {
                        DateTime s = DateTime.Now;
                        TimeSpan ts = new TimeSpan(0, 0, 0);
                        s = s.Date + ts;
                        stallworkingtime.RestTimeEnd = s;
                    }

                    int id = _stallWorkingTimeMapper.Insert(stallworkingtime, DNetUserName);

                    result.success = id > 0;
                    if (!result.success)
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }

                    result._id = id;
                    result.total = 1;
                    //result.lst = _mapper.Map<StallWorkingTimeDto>(stallworkingtime);
                    //result.lst.ID = id;
                }
                else
                {
                    return PopulateValidationError<StallWorkingTimeDto>(validationResults, null);
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
        public ResponseBase<StallWorkingTimeDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<StallWorkingTimeDto>> Read(StallWorkingTimeFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<StallWorkingTimeDto> Update(StallWorkingTimeUpdateParameterDto objUpdate)
        {
            #region declare
            var result = new ResponseBase<StallWorkingTimeDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            StallWorkingTime stallWorkingTime;
            StallWorkingTime stallWorkingTimeExist = null;
            #endregion

            try
            {
                //Validate Stall Master
                isValid = ValidateStallWorkingTimeUpdate(objUpdate, validationResults, out stallWorkingTime);
                var IsNotID = false;

                //Data is Exist 
                var criterias = new CriteriaComposite(new Criteria(typeof(StallWorkingTime), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                if (objUpdate.ID != 0)
                {
                    criterias.opAnd(new Criteria(typeof(StallWorkingTime), "ID", MatchType.Exact, objUpdate.ID));
                }
                else
                {
                    IsNotID = true;
                    criterias.opAnd(new Criteria(typeof(StallWorkingTime), "Tanggal", MatchType.Exact, objUpdate.Tanggal));
                    criterias.opAnd(new Criteria(typeof(StallWorkingTime), "StallMaster.StallCode", MatchType.Exact, objUpdate.StallCode));
                }


                var data = _stallWorkingTimeMapper.RetrieveByCriteria(criterias);
                if (data.Count == 0)
                {
                    isValid = false;
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.ID) });
                }
                else
                {
                    stallWorkingTimeExist = (StallWorkingTime)data[0];
                }
                if (IsNotID == true && stallWorkingTimeExist.StallMaster.StallCode != objUpdate.StallCode && stallWorkingTimeExist.Tanggal != objUpdate.Tanggal)
                {
                    isValid = false;
                    validationResults.Add(new DNetValidationResult("Stall Code tidak sesuai dengan ID yang dimasukkan."));
                }

                // if valid and data is exist then update data
                if (isValid)
                {
                    var newStallWorkingTime = _mapper.Map<StallWorkingTime>(stallWorkingTimeExist);
                    newStallWorkingTime.Dealer = stallWorkingTime.Dealer;
                    newStallWorkingTime.LastUpdatedBy = DNetUserName;
                    newStallWorkingTime.LastUpdatedTime = DateTime.Now;
                    newStallWorkingTime.RestTimeStart = stallWorkingTime.RestTimeStart;
                    newStallWorkingTime.TimeStart = stallWorkingTime.TimeStart;
                    newStallWorkingTime.RestTimeEnd = stallWorkingTime.RestTimeEnd;
                    newStallWorkingTime.TimeEnd = stallWorkingTime.TimeEnd;
                    newStallWorkingTime.IsHoliday = Convert.ToInt16(objUpdate.IsHoliday);
                    newStallWorkingTime.Notes = objUpdate.Notes;
                    if (!string.IsNullOrEmpty(objUpdate.VisitType))
                    {
                        newStallWorkingTime.VisitType = Convert.ToInt16(objUpdate.VisitType);
                    }
                    else
                    {
                        newStallWorkingTime.VisitType = 0;
                    }
                    if (IsNotID == false)
                    {
                        newStallWorkingTime.Tanggal = objUpdate.Tanggal;
                        newStallWorkingTime.StallMaster = stallWorkingTime.StallMaster;
                    }
                    if (newStallWorkingTime.RestTimeStart.ToString("yyyy-MM-dd HH:mm:ss") == "1753-01-01 00:00:00")
                    {
                        DateTime s = DateTime.Now;
                        TimeSpan ts = new TimeSpan(0, 0, 0);
                        s = s.Date + ts;
                        newStallWorkingTime.RestTimeStart = s;
                    }
                    if (newStallWorkingTime.RestTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") == "1753-01-01 00:00:00")
                    {
                        DateTime s = DateTime.Now;
                        TimeSpan ts = new TimeSpan(0, 0, 0);
                        s = s.Date + ts;
                        newStallWorkingTime.RestTimeEnd = s;
                    }

                    var success = (int)_stallWorkingTimeMapper.Update(newStallWorkingTime, DNetUserName);
                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    // return output ID
                    result._id = stallWorkingTimeExist.ID;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<StallWorkingTimeDto>(validationResults, null);
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

        public ResponseBase<StallWorkingTimeDto> Update(StallWorkingTimeParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<StallWorkingTimeDto>> BulkCreate(List<StallWorkingTimeCreateListParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<StallWorkingTimeDto>>();
            var listOfExistingData = new List<StallWorkingTimeDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                if (lstObjCreate.Count > 0)
                {
                    ValidateDuplicateParamData(lstObjCreate, validationResults);
                }

                // if any error found
                if (validationResults.Any())
                {
                    return PopulateValidationError<List<StallWorkingTimeDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    List<DomainIF.StallWorkingTime_IF> _stallWorkingTime = new List<DomainIF.StallWorkingTime_IF>();
                    foreach (StallWorkingTimeCreateListParameterDto item in lstObjCreate)
                    {
                        if (item.RestTimeStart.ToString("yyyy-MM-dd HH:mm:ss") == "1753-01-01 00:00:00")
                        {
                            DateTime s = DateTime.Now;
                            TimeSpan ts = new TimeSpan(0, 0, 0);
                            s = s.Date + ts;
                            item.RestTimeStart = s;
                        }
                        if (item.RestTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") == "1753-01-01 00:00:00")
                        {
                            DateTime s = DateTime.Now;
                            TimeSpan ts = new TimeSpan(0, 0, 0);
                            s = s.Date + ts;
                            item.RestTimeEnd = s;
                        }

                        StallWorkingTime stallWT;
                        if (ValidateStallWorkingTimeCreateList(item, validationResults, out stallWT))
                        {
                            DomainIF.StallWorkingTime_IF stallworkingtime = null;
                            stallworkingtime = item.ConvertObject<DomainIF.StallWorkingTime_IF>();
                            stallworkingtime.DealerID = stallWT.Dealer.ID;
                            stallworkingtime.StallMasterID = stallWT.StallMaster.ID;
                            stallworkingtime.TimeStart = stallWT.TimeStart.TimeOfDay;
                            stallworkingtime.TimeEnd = stallWT.TimeEnd.TimeOfDay;
                            stallworkingtime.RestTimeStart = stallWT.RestTimeStart.TimeOfDay;
                            stallworkingtime.RestTimeEnd = stallWT.RestTimeEnd.TimeOfDay;
                            stallworkingtime.CreatedBy = DNetUserName;
                            stallworkingtime.CreatedTime = DateTime.Now;
                            stallworkingtime.LastUpdatedBy = DNetUserName;
                            stallworkingtime.LastUpdatedTime = DateTime.Now;
                            if (!string.IsNullOrEmpty(item.VisitType))
                            {
                                stallworkingtime.VisitType = Convert.ToInt16(item.VisitType);
                            }

                            _stallWorkingTime.Add(stallworkingtime);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<StallWorkingTimeDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _StallWorkingTimeRepo.BulkInsert(_stallWorkingTime);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = _stallWorkingTime.Count;
                        result.lst = null; //_stallWorkingTime.ConvertList<DomainIF.StallWorkingTime_IF, StallWorkingTimeDto>();
                    }
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

        public ResponseBase<List<StallWorkingTimeDto>> BulkUpdate(List<StallWorkingTimeUpdateListParameterDto> lstObjUpdate)
        {
            var result = new ResponseBase<List<StallWorkingTimeDto>>();
            var listOfExistingData = new List<StallWorkingTimeDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                if (lstObjUpdate.Count > 0)
                {
                    ValidateDuplicateParamUpdateData(lstObjUpdate, validationResults);
                }

                // if any error found
                if (validationResults.Any())
                {
                    return PopulateValidationError<List<StallWorkingTimeDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var StallWorkingTimeList = lstObjUpdate.ConvertList<StallWorkingTimeUpdateListParameterDto, StallWorkingTime>();

                    List<StallWorkingTime> _stallWorkingTime = new List<StallWorkingTime>();
                    foreach (StallWorkingTimeUpdateListParameterDto item in lstObjUpdate)
                    {
                        var isValid = true;
                        var IsNotID = false;
                        StallWorkingTime stallWorkingTime;
                        StallWorkingTime stallWorkingTimeExist = null;

                        if (item.RestTimeStart.ToString("yyyy-MM-dd HH:mm:ss") == "1753-01-01 00:00:00")
                        {
                            DateTime s = DateTime.Now;
                            TimeSpan ts = new TimeSpan(0, 0, 0);
                            s = s.Date + ts;
                            item.RestTimeStart = s;
                        }
                        if (item.RestTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") == "1753-01-01 00:00:00")
                        {
                            DateTime s = DateTime.Now;
                            TimeSpan ts = new TimeSpan(0, 0, 0);
                            s = s.Date + ts;
                            item.RestTimeEnd = s;
                        }

                        isValid = ValidateStallWorkingTimeUpdateList(item, validationResults, out stallWorkingTime);
                        isValid = ValidateDataIsExistParamUpdateData(item, IsNotID, validationResults, out stallWorkingTimeExist);

                        if (isValid)
                        {
                            var newStallWorkingTime = _mapper.Map<StallWorkingTime>(stallWorkingTimeExist);
                            newStallWorkingTime.Dealer = stallWorkingTime.Dealer;
                            newStallWorkingTime.CreatedBy = stallWorkingTimeExist.CreatedBy;
                            newStallWorkingTime.LastUpdatedBy = DNetUserName;
                            newStallWorkingTime.LastUpdatedTime = DateTime.Now;
                            newStallWorkingTime.RestTimeStart = stallWorkingTime.RestTimeStart;
                            newStallWorkingTime.TimeStart = stallWorkingTime.TimeStart;
                            newStallWorkingTime.RestTimeEnd = stallWorkingTime.RestTimeEnd;
                            newStallWorkingTime.TimeEnd = stallWorkingTime.TimeEnd;
                            newStallWorkingTime.IsHoliday = Convert.ToInt16(item.IsHoliday);
                            newStallWorkingTime.Notes = item.Notes;
                            if (!string.IsNullOrEmpty(item.VisitType))
                            {
                                newStallWorkingTime.VisitType = Convert.ToInt16(item.VisitType);
                            }
                            else
                            {
                                newStallWorkingTime.VisitType = 0;
                            }
                            if (IsNotID == false)
                            {
                                newStallWorkingTime.Tanggal = item.Tanggal;
                                newStallWorkingTime.StallMaster = stallWorkingTime.StallMaster;
                            }

                            _stallWorkingTime.Add(newStallWorkingTime);
                        }

                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<StallWorkingTimeDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = UpdateWithTransactionManager(_stallWorkingTime) != null;
                        result.success = isSuccess;
                        if (result.success)
                            result.total = StallWorkingTimeList.Count;
                        result.lst = null; //StallWorkingTimeList.ConvertList<StallWorkingTime, StallWorkingTimeDto>();
                    }
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
        #region Private Method
        private bool ValidateStallWorkingTime(StallWorkingTimeParameterDto objCreate, List<DNetValidationResult> validationResults, out StallWorkingTime stallWorkingTime)
        {
            bool isValid = true;
            stallWorkingTime = _mapper.Map<StallWorkingTime>(objCreate);

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                stallWorkingTime.Dealer = dealer;
            }

            // Validate Stall Master 
            StallMaster stallMaster;
            var isValidStall = ValidateStallMaster(objCreate.StallCode, out stallMaster);
            if (stallMaster != null && isValidStall == true)
            {
                stallWorkingTime.StallMaster = stallMaster;
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Code " + objCreate.StallCode.ToString() + " tidak ditemukan"));
            }

            //Validate Visit Type
            if (!string.IsNullOrEmpty(objCreate.VisitType))
            {
                var visitType = _enumBL.GetByCategoryAndValue(".VisitType", objCreate.VisitType.ToString());
                if (visitType != null)
                {
                    stallWorkingTime.VisitType = Convert.ToInt16(visitType.ValueId);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Vsit Type " + objCreate.VisitType.ToString() + " tidak valid"));
                }
            }

            //Validate Is Holiday
            if (objCreate.IsHoliday == "0" || objCreate.IsHoliday == "1")
            {
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Is Holiday " + objCreate.IsHoliday.ToString() + " tidak valid"));
            }

            //is exist 
            var criteriasexist = new CriteriaComposite(new Criteria(typeof(StallWorkingTime), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriasexist.opAnd(new Criteria(typeof(StallWorkingTime), "Tanggal", MatchType.Exact, objCreate.Tanggal));
            criteriasexist.opAnd(new Criteria(typeof(StallWorkingTime), "StallMaster.StallCode", MatchType.Exact, objCreate.StallCode));
            criteriasexist.opAnd(new Criteria(typeof(StallWorkingTime), "VisitType", MatchType.Exact, objCreate.VisitType));

            var datacriteriasexist = _stallWorkingTimeMapper.RetrieveByCriteria(criteriasexist);
            if (datacriteriasexist.Count > 0)
            {
                validationResults.Add(new DNetValidationResult("Data dengan StallCode " + objCreate.StallCode.ToString() + " dan Tanggal " + objCreate.Tanggal.ToString() + " dan VisitType " + objCreate.VisitType.ToString() + " sudah ada di sistem"));
            }

            if (validationResults.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }

        private bool ValidateStallWorkingTimeUpdate(StallWorkingTimeUpdateParameterDto objUpdate, List<DNetValidationResult> validationResults, out StallWorkingTime stallWorkingTime)
        {
            bool isValid = true;
            stallWorkingTime = _mapper.Map<StallWorkingTime>(objUpdate);

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objUpdate.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                stallWorkingTime.Dealer = dealer;
            }

            // Validate Stall Master 
            StallMaster stallMaster;
            var isValidStall = ValidateStallMaster(objUpdate.StallCode, out stallMaster);
            if (stallMaster != null && isValidStall == true)
            {
                stallWorkingTime.StallMaster = stallMaster;
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Code " + objUpdate.StallCode.ToString() + " tidak ditemukan"));
            }

            //Validate Visit Type
            if (!string.IsNullOrEmpty(objUpdate.VisitType))
            {
                var visitType = _enumBL.GetByCategoryAndValue(".VisitType", objUpdate.VisitType.ToString());
                if (visitType != null)
                {
                    stallWorkingTime.VisitType = Convert.ToInt16(visitType.ValueId);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Vsit Type " + objUpdate.VisitType.ToString() + " tidak valid"));
                }
            }

            //Validate Is Holiday
            if (objUpdate.IsHoliday == "0" || objUpdate.IsHoliday == "1")
            {
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Is Holiday " + objUpdate.IsHoliday.ToString() + " tidak valid"));
            }

            if (validationResults.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }

        private bool ValidateStallMaster(string stallCode, out StallMaster stallMaster)
        {
            bool isvalid = false;
            stallMaster = null;
            var criterias = new CriteriaComposite(new Criteria(typeof(StallMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(StallMaster), "StallCode", MatchType.Exact, stallCode));
            var data = _stallMasterMapper.RetrieveByCriteria(criterias);
            if (data.Count > 0)
            {
                stallMaster = (StallMaster)data[0];
                isvalid = true;
            }

            return isvalid;
        }

        private bool ValidateDuplicateParamData(List<StallWorkingTimeCreateListParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.StallCode.ToString().Trim() == item.StallCode.ToString().Trim() &&
                                                x.Tanggal.ToString().Trim() == item.Tanggal.ToString().Trim() &&
                                                x.VisitType.ToString().Trim() == item.VisitType.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1},{2})", "StallCode = " + item.StallCode.ToString(), "Tanggal = " + item.Tanggal.ToString(), "VisitType = " + item.VisitType.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateParamUpdateData(List<StallWorkingTimeUpdateListParameterDto> lstObjUpdate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjUpdate)
            {
                var lst = lstObjUpdate.Where(x => x.StallCode.ToString().Trim() == item.StallCode.ToString().Trim() &&
                                                x.Tanggal.ToString().Trim() == item.Tanggal.ToString().Trim() &&
                                                x.VisitType.ToString().Trim() == item.VisitType.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1},{2})", "StallCode = " + item.StallCode.ToString(), "Tanggal = " + item.Tanggal.ToString(), "VisitType = " + item.VisitType.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateStallWorkingTimeCreateList(StallWorkingTimeCreateListParameterDto objCreate, List<DNetValidationResult> validationResults, out StallWorkingTime stallWorkingTime)
        {
            bool isValid = true;
            StallWorkingTimeParameterDto obj = objCreate.ConvertObject<StallWorkingTimeParameterDto>();
            stallWorkingTime = _mapper.Map<StallWorkingTime>(obj);

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                stallWorkingTime.Dealer = dealer;
            }

            // Validate Stall Master 
            StallMaster stallMaster;
            var isValidStall = ValidateStallMaster(objCreate.StallCode, out stallMaster);
            if (stallMaster != null && isValidStall == true)
            {
                stallWorkingTime.StallMaster = stallMaster;
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Code " + objCreate.StallCode.ToString() + " tidak ditemukan"));
            }

            //Validate Visit Type
            if (!string.IsNullOrEmpty(objCreate.VisitType))
            {
                var visitType = _enumBL.GetByCategoryAndValue(".VisitType", objCreate.VisitType.ToString());
                if (visitType != null)
                {
                    stallWorkingTime.VisitType = Convert.ToInt16(visitType.ValueId);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Vsit Type " + objCreate.VisitType.ToString() + " tidak valid"));
                }
            }

            //Validate Is Holiday
            if (objCreate.IsHoliday == "0" || objCreate.IsHoliday == "1")
            {
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Is Holiday " + objCreate.IsHoliday.ToString() + " tidak valid"));
            }

            //is exist 
            var criteriasexist = new CriteriaComposite(new Criteria(typeof(StallWorkingTime), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriasexist.opAnd(new Criteria(typeof(StallWorkingTime), "Tanggal", MatchType.Exact, objCreate.Tanggal));
            criteriasexist.opAnd(new Criteria(typeof(StallWorkingTime), "StallMaster.StallCode", MatchType.Exact, objCreate.StallCode));
            criteriasexist.opAnd(new Criteria(typeof(StallWorkingTime), "VisitType", MatchType.Exact, objCreate.VisitType));

            var datacriteriasexist = _stallWorkingTimeMapper.RetrieveByCriteria(criteriasexist);
            if (datacriteriasexist.Count > 0)
            {
                validationResults.Add(new DNetValidationResult("Data dengan StallCode " + objCreate.StallCode.ToString() + " dan Tanggal " + objCreate.Tanggal.ToString() + " dan VistiType " + objCreate.VisitType.ToString() + " sudah ada di sistem"));
            }

            if (validationResults.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }

        private bool ValidateStallWorkingTimeUpdateList(StallWorkingTimeUpdateListParameterDto objUpdate, List<DNetValidationResult> validationResults, out StallWorkingTime stallWorkingTime)
        {
            bool isValid = true;
            StallWorkingTimeParameterDto obj = objUpdate.ConvertObject<StallWorkingTimeParameterDto>();
            stallWorkingTime = _mapper.Map<StallWorkingTime>(obj);

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objUpdate.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                stallWorkingTime.Dealer = dealer;
            }

            // Validate Stall Master 
            StallMaster stallMaster;
            var isValidStall = ValidateStallMaster(objUpdate.StallCode, out stallMaster);
            if (stallMaster != null && isValidStall == true)
            {
                stallWorkingTime.StallMaster = stallMaster;
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Stall Code " + objUpdate.StallCode.ToString() + " tidak ditemukan"));
            }

            //Validate Visit Type
            if (!string.IsNullOrEmpty(objUpdate.VisitType))
            {
                var visitType = _enumBL.GetByCategoryAndValue(".VisitType", objUpdate.VisitType.ToString());
                if (visitType != null)
                {
                    stallWorkingTime.VisitType = Convert.ToInt16(visitType.ValueId);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Vsit Type " + objUpdate.VisitType.ToString() + " tidak valid"));
                }
            }

            //Validate Is Holiday
            if (objUpdate.IsHoliday == "0" || objUpdate.IsHoliday == "1")
            {
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Is Holiday " + objUpdate.IsHoliday.ToString() + " tidak valid"));
            }

            if (validationResults.Count > 0)
            {
                isValid = false;
            }
            return isValid;
        }

        private bool ValidateDataIsExistParamUpdateData(StallWorkingTimeUpdateListParameterDto ObjUpdate, bool IsNotID, List<DNetValidationResult> validationResults, out StallWorkingTime stallWorkingTimeExist)
        {
            //Data is Exist 
            var criterias = new CriteriaComposite(new Criteria(typeof(StallWorkingTime), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            if (ObjUpdate.ID != 0)
            {
                criterias.opAnd(new Criteria(typeof(StallWorkingTime), "ID", MatchType.Exact, ObjUpdate.ID));
            }
            else
            {
                IsNotID = true;
                criterias.opAnd(new Criteria(typeof(StallWorkingTime), "Tanggal", MatchType.Exact, ObjUpdate.Tanggal));
                criterias.opAnd(new Criteria(typeof(StallWorkingTime), "StallMaster.StallCode", MatchType.Exact, ObjUpdate.StallCode));
                criterias.opAnd(new Criteria(typeof(StallWorkingTime), "VisitType", MatchType.Exact, ObjUpdate.VisitType));
            }

            var data = _stallWorkingTimeMapper.RetrieveByCriteria(criterias);
            if (data.Count == 0)
            {
                stallWorkingTimeExist = null;
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.ID)));
            }
            else
            {
                stallWorkingTimeExist = (StallWorkingTime)data[0];

                if (IsNotID == true && stallWorkingTimeExist.StallMaster.StallCode != ObjUpdate.StallCode && stallWorkingTimeExist.Tanggal != ObjUpdate.Tanggal)
                {
                    validationResults.Add(new DNetValidationResult("Stall Code tidak sesuai dengan ID yang dimasukkan."));
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Handler on executed insert command with transaction manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(StallWorkingTime))
            {
                ((StallWorkingTime)args.DomainObject).ID = args.ID;
                ((StallWorkingTime)args.DomainObject).MarkLoaded();
            }
        }

        private List<StallWorkingTime> UpdateWithTransactionManager(List<StallWorkingTime> listStall)
        {
            List<StallWorkingTime> result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    foreach (var item in listStall)
                    {
                        this._transactionManager.AddUpdate(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = listStall;
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
        #endregion
    }
}

