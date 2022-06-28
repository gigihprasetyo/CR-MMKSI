#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCodeChar business logic class
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
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class StandardCodeCharBL : AbstractBusinessLogic, IStandardCodeCharBL
    {
        #region Variables
        private readonly IMapper _standardcodeMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public StandardCodeCharBL()
        {
            _standardcodeMapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCodeChar).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        public StandardCodeCharBL(AutoMapper.IMapper mapper)
        {
            _standardcodeMapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCodeChar).ToString());
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Standard Code Char by their Code
        /// </summary>
        /// <param name="category"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public StandardCodeCharDto GetByCategoryAndCode(string category, string code)
        {
            var filter = new StandardCodeCharFilterDto
            {
                pages = 1,
                find = new List<MatchTypeFilter> 
                {
                    new MatchTypeFilter 
                    {
                        MatchType = MatchType.Exact,
                        PropertyName = "Category",
                        PropertyValue = category,
                        SqlOperation = SQLOperation.And
                     },
                     new MatchTypeFilter 
                    {
                        MatchType = MatchType.Exact,
                        PropertyName = "ValueCode",
                        PropertyValue = code,
                        SqlOperation = SQLOperation.And
                     }
                },
                sort = new List<SortFilter>()
            };

            var result = Read(filter, 20);
            if (result.lst != null)
            {
                return result.lst[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get Standard Code Char By Category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<StandardCodeCharDto> GetByCategory(string category)
        {
            var filter = new StandardCodeCharFilterDto
            {
                pages = 1,
                find = new List<MatchTypeFilter> 
                {
                    new MatchTypeFilter 
                    {
                        MatchType = MatchType.Exact,
                        PropertyName = "Category",
                        PropertyValue = category,
                        SqlOperation = SQLOperation.And
                     }
                },
                sort = new List<SortFilter>()
            };

            var result = Read(filter, 20);
            if (result.lst != null)
            {
                return result.lst;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get StandardCode by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<StandardCodeCharDto>> Read(StandardCodeCharFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(StandardCodeChar), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<StandardCodeCharDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(StandardCodeChar), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(StandardCodeChar), filterDto, sortColl);

                // get data
                var data = _standardcodeMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<StandardCodeChar>().ToList();
                    var listData = list.Select(item => _mapper.Map<StandardCodeCharDto>(item)).ToList();

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
        /// Delete StandardCodeChar by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<StandardCodeCharDto> Delete(int id)
        {
            var result = new ResponseBase<StandardCodeCharDto>();

            try
            {
                var standardcode = (StandardCodeChar)_standardcodeMapper.Retrieve(id);
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
        /// Create a new StandardCodeChar
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<StandardCodeCharDto> Create(StandardCodeCharParameterDto objCreate)
        {
            var result = new ResponseBase<StandardCodeCharDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // parse the object
                var stdCodeInsert = _mapper.Map<StandardCodeChar>(objCreate);

                // insert into db
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
        /// Update StandardCodeChar
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<StandardCodeCharDto> Update(StandardCodeCharParameterDto objUpdate)
        {
            // initialize
            var result = new ResponseBase<StandardCodeCharDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            StandardCodeChar standardCodeChar = null;

            // existance validation
            ValidateStandardCodeChar(objUpdate, validationResults, ref isValid, ref standardCodeChar);

            try
            {
                if (isValid)
                {
                    // create standard code object
                    var newStandardCode = _mapper.Map<StandardCodeCharParameterDto, StandardCodeChar>(objUpdate, standardCodeChar);

                    // update
                    var success = _standardcodeMapper.Update(newStandardCode, newStandardCode.LastUpdateBy);
                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<StandardCodeCharDto>(validationResults, null);
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
        /// Get standard code by its category
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="standardCodeChar"></param>
        private void ValidateStandardCodeChar(StandardCodeCharParameterDto objUpdate, List<DNetValidationResult> validationResults, ref bool isValid, ref StandardCodeChar standardCodeChar)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(StandardCodeChar), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(StandardCodeChar), "Category", MatchType.Exact, objUpdate.Category));

            var standardCodes = _standardcodeMapper.RetrieveByCriteria(criterias);
            if (standardCodes.Count > 0)
            {
                // cast the object
                standardCodeChar = standardCodes[0] as StandardCodeChar;
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

