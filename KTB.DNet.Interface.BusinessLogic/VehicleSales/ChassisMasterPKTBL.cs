#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterPKT business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
using KTB.DNet.BusinessValidation;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class ChassisMasterPKTBL : AbstractBusinessLogic, IChassisMasterPKTBL
    {
        #region Variables
        private readonly IMapper _chassisMasterPKTMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public ChassisMasterPKTBL()
        {
            _chassisMasterPKTMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new ChassisMasterPKT
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterPKTDto> Create(ChassisMasterPKTParameterDto objCreate)
        {
            var result = new ResponseBase<ChassisMasterPKTDto>();
            var validationResults = new List<DNetValidationResult>();
            var validResults = new List<ValidResult>();
            var isValid = true;

            try
            {
                #region Map Object from Param
                var chassisMasterPKT = _mapper.Map<ChassisMasterPKT>(objCreate);

                // map Chassis Number
                var chassisMasterParam = new ChassisMaster()
                {
                    ChassisNumber = objCreate.ChassisNumber
                };
                chassisMasterParam.MarkLoaded();
                chassisMasterPKT.ChassisMaster = chassisMasterParam;
                #endregion

                var chassisMasterPKTValidation = new ChassisMasterPKTValidation();
                validResults = chassisMasterPKTValidation.ValidateChassisMasterPKT(ref chassisMasterPKT, ref chassisMasterParam);
                isValid = validResults.Count == 0;

                if (isValid)
                {
                    //Ketika dimasukkan data :
                    //1. Check Chassis ke Chassismaster
                    //2. Check Chassis ke table ChassisMasterPKT
                    //3. kalo nomor 2 exist : Update TglPkt, kalo tidak maka create
                    var succeed = 0;
                    if (chassisMasterPKT.ID != 0)
                    {
                        //chassisMasterPKT.LastUpdateTime = DateTime.Now;
                        //chassisMasterPKT.PKTDate = objCreate.PKTDate;

                        //succeed = (int)_chassisMasterPKTMapper.Update(chassisMasterPKT, DNetUserName);
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorPKTExist, chassisMasterPKT.ChassisMaster.ChassisNumber)));
                        return PopulateValidationError<ChassisMasterPKTDto>(validationResults, null);
                    }
                    else
                    {
                        // create ChassisMasterPKT object

                        chassisMasterPKT.ChassisMaster = chassisMasterParam;
                        chassisMasterPKT.PKTDate = objCreate.PKTDate;
                        chassisMasterPKT.CreatedTime = DateTime.Now;

                        succeed = _chassisMasterPKTMapper.Insert(chassisMasterPKT, DNetUserName);
                    }

                    result.success = succeed > 0;
                    if (!result.success) ErrorMsgHelper.DataCorrupt(result.messages);

                    // return output ID
                    result._id = succeed;
                    result.total = 1;
                }
                else
                {
                    foreach (var validResult in validResults)
                    {
                        validationResults.Add(new DNetValidationResult(validResult.Message));
                    }
                    return PopulateValidationError<ChassisMasterPKTDto>(validationResults, null);
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
        /// Update ChassisMasterPKT
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterPKTDto> Update(ChassisMasterPKTParameterDto objUpdate)
        {
            var result = new ResponseBase<ChassisMasterPKTDto>();
            var validationResults = new List<DNetValidationResult>();
            var validResults = new List<ValidResult>();
            var isValid = true;

            try
            {
                #region Map Object from Param
                var chassisMasterPKT = _mapper.Map<ChassisMasterPKT>(objUpdate);

                // map Chassis Number
                var chassisMasterParam = new ChassisMaster()
                {
                    ChassisNumber = objUpdate.ChassisNumber
                };
                chassisMasterParam.MarkLoaded();
                chassisMasterPKT.ChassisMaster = chassisMasterParam;
                #endregion

                var chassisMasterPKTValidation = new ChassisMasterPKTValidation();
                validResults = chassisMasterPKTValidation.ValidateChassisMasterPKT(ref chassisMasterPKT, ref chassisMasterParam);
                isValid = validResults.Count == 0;

                if (isValid)
                {
                    validResults = chassisMasterPKTValidation.ValidateDealerChassisPKT(objUpdate.DealerCode);
                    isValid = validResults.Count == 0;
                }

                if (isValid)
                {
                    //Ketika dimasukkan data :
                    //1. Check Chassis ke Chassismaster
                    //2. Check Chassis ke table ChassisMasterPKT
                    //3. kalo nomor 2 exist : Update TglPkt, kalo tidak maka create
                    var succeed = 0;
                    if (chassisMasterPKT.ID != 0)
                    {
                        //chassisMasterPKT.LastUpdateTime = DateTime.Now;
                        //chassisMasterPKT.PKTDate = objUpdate.PKTDate;

                        //succeed = (int)_chassisMasterPKTMapper.Update(chassisMasterPKT, DNetUserName);
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorPKTExist, chassisMasterPKT.ChassisMaster.ChassisNumber)));
                        return PopulateValidationError<ChassisMasterPKTDto>(validationResults, null);
                    }
                    else
                    {
                        // create ChassisMasterPKT object

                        chassisMasterPKT.ChassisMaster = chassisMasterParam;
                        chassisMasterPKT.PKTDate = objUpdate.PKTDate;
                        chassisMasterPKT.CreatedTime = DateTime.Now;

                        succeed = _chassisMasterPKTMapper.Insert(chassisMasterPKT, DNetUserName);
                    }

                    result.success = succeed > 0;
                    if (!result.success) ErrorMsgHelper.DataCorrupt(result.messages);

                    // return output ID
                    result._id = succeed;
                    result.total = 1;
                }
                else
                {
                    foreach (var validResult in validResults)
                    {
                        validationResults.Add(new DNetValidationResult(validResult.Message));
                    }
                    return PopulateValidationError<ChassisMasterPKTDto>(validationResults, null);
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
        /// Delete ChassisMasterPKT by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterPKTDto> Delete(int id)
        {
            var result = new ResponseBase<ChassisMasterPKTDto>();

            try
            {
                var chassisMasterPKT = (ChassisMasterPKT)_chassisMasterPKTMapper.Retrieve(id);
                if (chassisMasterPKT != null)
                {
                    chassisMasterPKT.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _chassisMasterPKTMapper.Update(chassisMasterPKT, DNetUserName);
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
        /// Get ChassisMasterPKT by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<ChassisMasterPKTDto>> Read(ChassisMasterPKTFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(ChassisMasterPKT), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<ChassisMasterPKTDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = CustomUpdateCriteria(typeof(ChassisMasterPKT), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(ChassisMasterPKT), filterDto, sortColl);

                // get data
                var data = _chassisMasterPKTMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<ChassisMasterPKT>().ToList();
                    var listData = list.Select(item => _mapper.Map<ChassisMasterPKTDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ChassisMasterPKT), filterDto);
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

        #endregion

        #region Private Method
        /// <summary>
        /// Validate Pkt Date
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        private void ValidatePktDate(ChassisMasterPKTParameterDto objCreate, List<DNetValidationResult> validationResults)
        {
            if (objCreate.PKTDate.Date > DateTime.Now.Date)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDateCompareError, FieldResource.PKTDate, ValidationResource.GreaterThan, FieldResource.TodayDate)));
            }
        }

        /// <summary>
        /// Custom update criteria
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filterDto"></param>
        /// <param name="criterias"></param>
        /// <returns></returns>
        private CriteriaComposite CustomUpdateCriteria(Type type, FilterDtoBase filterDto, CriteriaComposite criterias)
        {
            if (filterDto.find != null && filterDto.find.Count > 0)
            {
                foreach (var filter in filterDto.find)
                {
                    // skip if the propertyName is null
                    if (filter.PropertyName == null ||
                        (filter.PropertyName.Equals("DealerCode", StringComparison.OrdinalIgnoreCase) && type != typeof(Dealer)) ||
                        (filter.PropertyName.Equals("ChassisNumber", StringComparison.OrdinalIgnoreCase))
                        )
                    {
                        continue;
                    }

                    // get the prop info to prevent an error caused by case sensitive name
                    PropertyInfo propInfo = type.GetProperty(filter.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    // just in case the property is not exist
                    var propName = propInfo == null ? filter.PropertyName : propInfo.Name;

                    switch (filter.SqlOperation)
                    {
                        case SQLOperation.And:
                            criterias.opAnd(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, filter.PropertyValue)));
                            break;
                        case SQLOperation.Or:
                            criterias.opOr(new Criteria(type, propName, filter.MatchType, Helper.GetPropertyValue(propInfo, filter.PropertyValue)));
                            break;
                    }
                }
            }

            return criterias;
        }

        /// <summary>
        /// Dealer ID by code
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        private int GetDealerId(string dealerCode)
        {
            Dealer dealer = new Dealer();
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();

            ValidationHelper.ValidateDealer(DealerCode, validationResults, this.DealerCode, ref dealer, false);

            return dealer.ID;
        }

        /// <summary>
        /// Validate Chassis master PKT by dealer
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<ChassisMasterPKT> ValidateByDealerID(ChassisMasterPKTFilterDto filterDto, List<ChassisMasterPKT> list)
        {
            List<ChassisMasterPKT> listChassisMasterPKT = new List<ChassisMasterPKT>();

            var chassisMasterNumber = string.Empty;
            var chassisMaster = filterDto.find.Where(x => x.PropertyName.Equals("ChassisNumber", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            chassisMasterNumber = chassisMaster == null ? null : chassisMaster.PropertyValue;

            int dealerID = 0;
            var dealerCode = Helper.GetDealerCodeFromFilter(filterDto);
            if (!string.IsNullOrEmpty(dealerCode) && !string.IsNullOrEmpty(chassisMasterNumber))
            {
                // get dealer ID
                dealerID = GetDealerId(dealerCode);

                if (dealerID == 0) return listChassisMasterPKT;

                foreach (var item in list)
                {
                    if (item.ChassisMaster.Dealer.ID == dealerID && item.ChassisMaster.ChassisNumber == chassisMasterNumber)
                    {
                        listChassisMasterPKT.Add(item);
                    }
                }
            }

            return listChassisMasterPKT;
        }

        #endregion
    }
}