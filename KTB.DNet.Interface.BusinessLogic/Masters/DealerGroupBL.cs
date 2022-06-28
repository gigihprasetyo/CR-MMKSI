#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DealerGroup business logic class
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
    public class DealerGroupBL : AbstractBusinessLogic, IDealerGroupBL
    {
        #region Variables
        private readonly IMapper _dealergroupMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public DealerGroupBL()
        {
            _dealergroupMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerGroup).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get DealerGroup by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DealerGroupDto>> Read(DealerGroupFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(DealerGroup), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<DealerGroupDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(DealerGroup), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(DealerGroup), filterDto, sortColl);

                // get data
                var data = _dealergroupMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<DealerGroup>().ToList();
                    var listData = list.Select(item => _mapper.Map<DealerGroupDto>(item)).ToList();

                    result.lst = listData;
                    // return output _id
                    result._id = -1;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(DealerGroup), filterDto);
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
        /// Delete DealerGroup by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<DealerGroupDto> Delete(int id)
        {
            var result = new ResponseBase<DealerGroupDto>();

            try
            {
                var dealergroup = (DealerGroup)_dealergroupMapper.Retrieve(id);
                if (dealergroup != null)
                {
                    dealergroup.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _dealergroupMapper.Update(dealergroup, DNetUserName);
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
        /// Create a new DealerGroup
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<DealerGroupDto> Create(DealerGroupParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update DealerGroup
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<DealerGroupDto> Update(DealerGroupParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

