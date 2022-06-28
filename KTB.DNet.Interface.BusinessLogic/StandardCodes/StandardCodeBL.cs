#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCode business logic class
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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class StandardCodeBL : AbstractBusinessLogic, IStandardCodeBL
    {
        #region Variables
        private readonly IMapper _standardcodeMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public StandardCodeBL()
        {
            _standardcodeMapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        public StandardCodeBL(AutoMapper.IMapper mapper)
        {
            _standardcodeMapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get StandardCode by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<StandardCodeDto>> Read(StandardCodeFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<StandardCodeDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(StandardCode), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(StandardCode), filterDto, sortColl);

                // get data
                var data = _standardcodeMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<StandardCode>().ToList();
                    var listData = _mapper.Map<IList<StandardCode>, IList<StandardCodeDto>>(list).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(StandardCode), filterDto);
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
        /// Delete StandardCode by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<StandardCodeDto> Delete(int id)
        {
            var result = new ResponseBase<StandardCodeDto>();

            try
            {
                var standardcode = (StandardCode)_standardcodeMapper.Retrieve(id);
                if (standardcode != null)
                {
                    standardcode.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _standardcodeMapper.Update(standardcode, DNetUserName);
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
        /// Create a new StandardCode
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<StandardCodeDto> Create(StandardCodeParameterDto objCreate)
        {
            var result = new ResponseBase<StandardCodeDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                //Process Insert
                var stdCodeInsert = _mapper.Map<StandardCode>(objCreate);

                // insert to db
                var success = _standardcodeMapper.Insert(stdCodeInsert, stdCodeInsert.CreatedBy);
                if (success > 0)
                {
                    result.success = true;
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.DataCorrupt(result.messages);
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
        /// Update StandardCode
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<StandardCodeDto> Update(StandardCodeParameterDto objUpdate)
        {
            // initialize
            var result = new ResponseBase<StandardCodeDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            StandardCode standardCode = null;

            // existance validation
            ValidateStandardCode(objUpdate, validationResults, ref isValid, ref standardCode);

            try
            {
                if (isValid)
                {
                    // create standard code object
                    var newStandardCode = _mapper.Map<StandardCodeParameterDto, StandardCode>(objUpdate, standardCode);

                    // update
                    var success = _standardcodeMapper.Update(newStandardCode, DNetUserName);
                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<StandardCodeDto>(validationResults, null);
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
        /// Get enum by category and value id
        /// </summary>
        /// <param name="category"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public StandardCodeDto GetByCategoryAndValue(string category, string value)
        {
            var result = new StandardCodeDto();

            try
            {
                // default filter to get the Active Row Status only
                var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.EndsWith, category));
                criterias.opAnd(new Criteria(typeof(StandardCode), "ValueId", MatchType.Exact, value));

                var data = _standardcodeMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var list = data.Cast<StandardCode>().ToList();
                    var listData = _mapper.Map<IList<StandardCode>, IList<StandardCodeDto>>(list).ToList();

                    result = listData.FirstOrDefault();
                }
                else
                {
                    throw new Exception(string.Format(MessageResource.ErrorMsgEnumNotFound, category + " ValueId " + value));
                }
            }
            catch (SqlException ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }

        /// <summary>
        /// Get by category and code
        /// </summary>
        /// <param name="category"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public StandardCodeDto GetByCategoryAndCode(string category, string code)
        {
            var result = new StandardCodeDto();

            try
            {
                // default filter to get the Active Row Status only
                var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.EndsWith, category));
                criterias.opAnd(new Criteria(typeof(StandardCode), "ValueCode", MatchType.Exact, code));

                var data = _standardcodeMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var list = data.Cast<StandardCode>().ToList();
                    var listData = _mapper.Map<IList<StandardCode>, IList<StandardCodeDto>>(list).ToList();

                    result = listData.FirstOrDefault();
                }
                else
                {
                    throw new Exception(string.Format(MessageResource.ErrorMsgEnumNotFound, category));
                }
            }
            catch (SqlException ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }

        /// <summary>
        /// Get standard code by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<StandardCodeDto> GetByCategory(string category)
        {
            var result = new List<StandardCodeDto>();

            try
            {
                // default filter to get the Active Row Status only
                var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.EndsWith, category));

                var data = _standardcodeMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var list = data.Cast<StandardCode>().ToList();
                    result = _mapper.Map<IList<StandardCode>, IList<StandardCodeDto>>(list).ToList();
                }
                else
                {
                    throw new Exception(string.Format(MessageResource.ErrorMsgEnumNotFound, category));
                }
            }
            catch (SqlException ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }

        /// <summary>
        /// Get standard code by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<StandardCodeDto> GetAllByCategory(string category)
        {
            var result = new List<StandardCodeDto>();

            try
            {
                // default filter to get the Active Row Status only
                var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "Category", MatchType.EndsWith, category));

                var data = _standardcodeMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var list = data.Cast<StandardCode>().ToList();
                    result = _mapper.Map<IList<StandardCode>, IList<StandardCodeDto>>(list).ToList();
                }
                else
                {
                    throw new Exception(string.Format(MessageResource.ErrorMsgEnumNotFound, category));
                }
            }
            catch (SqlException ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }

        /// <summary>
        /// Check whether the passed category and desc are exist in database
        /// </summary>
        /// <param name="category"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsExistByCategoryAndCode(string category, string code)
        {
            try
            {
                GetByCategoryAndCode(category, code);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether the passed category and value are exist in database
        /// </summary>
        /// <param name="category"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsExistByCategoryAndValue(string category, string value)
        {
            try
            {
                GetByCategoryAndValue(category, value);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsExist(string category, string value)
        {
            bool result = false;
            var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var totalRow = 0;

            try
            {
                var filterDto = new StandardCodeFilterDto();
                filterDto.find = new List<MatchTypeFilter>();
                filterDto.find.AddRange(new List<MatchTypeFilter>{
                    new MatchTypeFilter{ MatchType = MatchType.EndsWith, PropertyName="Category", PropertyValue=category, SqlOperation=SQLOperation.And },
                    new MatchTypeFilter{ MatchType = MatchType.Exact, PropertyName="ValueId", PropertyValue= value, SqlOperation=SQLOperation.And }
                });

                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(StandardCode), filterDto, criterias);

                var data = _standardcodeMapper.RetrieveByCriteria(criterias, null, 1, 1, ref totalRow);
                if (data.Count > 0)
                {
                    result = true;
                }
            }
            catch (SqlException ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get standard code by its category
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="standardCode"></param>
        private void ValidateStandardCode(StandardCodeParameterDto objUpdate, List<DNetValidationResult> validationResults, ref bool isValid, ref StandardCode standardCode)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.Exact, objUpdate.Category));

            var standardCodes = _standardcodeMapper.RetrieveByCriteria(criterias);
            if (standardCodes.Count > 0)
            {
                // cast the object
                standardCode = standardCodes[0] as StandardCode;
            }
            else
            {
                isValid = false;
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, objUpdate.Category)));
            }
        }
        #endregion
    }
}

