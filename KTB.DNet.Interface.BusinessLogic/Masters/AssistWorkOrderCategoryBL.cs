#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Area1 business logic class
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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class AssistWorkOrderCategoryBL : AbstractBusinessLogic, IAssistWorkOrderCategoryBL
    {
        #region Variables
        private readonly IMapper _assistWorkOrderCategoryMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public AssistWorkOrderCategoryBL()
        {
            _assistWorkOrderCategoryMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistWorkOrderCategory).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Area1 by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistWorkOrderCategoryDto>> Read(AssistWorkOrderCategoryFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(AssistWorkOrderCategory), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(AssistWorkOrderCategory), "Status", MatchType.Exact, 1));
            var result = new ResponseBase<List<AssistWorkOrderCategoryDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(AssistWorkOrderCategory), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(AssistWorkOrderCategory), filterDto, sortColl);

                // get data
                var data = _assistWorkOrderCategoryMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);

                if (data.Count > 0)
                {
                    var list = data.Cast<AssistWorkOrderCategory>().ToList();
                    var listData = list.Select(item => _mapper.Map<AssistWorkOrderCategoryDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(AssistWorkOrderCategory), filterDto);
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
        /// Delete Area1 by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<AssistWorkOrderCategoryDto> Delete(int id)
        {
            var result = new ResponseBase<AssistWorkOrderCategoryDto>();

            try
            {
                var assistWorkOrderType = (AssistWorkOrderType)_assistWorkOrderCategoryMapper.Retrieve(id);
                if (assistWorkOrderType != null)
                {
                    assistWorkOrderType.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _assistWorkOrderCategoryMapper.Update(assistWorkOrderType, DNetUserName);
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
        /// Create a new WorkOrderType
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<AssistWorkOrderCategoryDto> Create(AssistWorkOrderCategoryParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update WorkOrderType
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<AssistWorkOrderCategoryDto> Update(AssistWorkOrderCategoryParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

