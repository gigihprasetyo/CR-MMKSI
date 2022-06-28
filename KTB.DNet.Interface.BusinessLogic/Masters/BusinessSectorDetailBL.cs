#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BusinessSectorDetail business logic class
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
    public class BusinessSectorDetailBL : AbstractBusinessLogic, IBusinessSectorDetailBL
    {
        #region Variables
        private readonly IMapper _businesssectordetailMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public BusinessSectorDetailBL()
        {
            _businesssectordetailMapper = MapperFactory.GetInstance().GetMapper(typeof(BusinessSectorDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get BusinessSectorDetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<BusinessSectorDetailDto>> Read(BusinessSectorDetailFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(BusinessSectorDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<BusinessSectorDetailDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(BusinessSectorDetail), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(BusinessSectorDetail), filterDto, sortColl);

                // get data
                var data = _businesssectordetailMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<BusinessSectorDetail>().ToList();
                    var listData = list.Select(item => _mapper.Map<BusinessSectorDetailDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(BusinessSectorDetail), filterDto);
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
        /// Delete BusinessSectorDetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<BusinessSectorDetailDto> Delete(int id)
        {
            var result = new ResponseBase<BusinessSectorDetailDto>();

            try
            {
                var businesssectordetail = (BusinessSectorDetail)_businesssectordetailMapper.Retrieve(id);
                if (businesssectordetail != null)
                {
                    businesssectordetail.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _businesssectordetailMapper.Update(businesssectordetail, DNetUserName);
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
        /// Create a new BusinessSectorDetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<BusinessSectorDetailDto> Create(BusinessSectorDetailParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update BusinessSectorDetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<BusinessSectorDetailDto> Update(BusinessSectorDetailParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

