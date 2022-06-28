#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BusinessSectorHeader business logic class
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
    public class BusinessSectorHeaderBL : AbstractBusinessLogic, IBusinessSectorHeaderBL
    {
        #region Variables
        private readonly IMapper _businesssectorheaderMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public BusinessSectorHeaderBL()
        {
            _businesssectorheaderMapper = MapperFactory.GetInstance().GetMapper(typeof(BusinessSectorHeader).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get BusinessSectorHeader by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<BusinessSectorHeaderDto>> Read(BusinessSectorHeaderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(BusinessSectorHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<BusinessSectorHeaderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(BusinessSectorHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(BusinessSectorHeader), filterDto, sortColl);

                // get data
                var data = _businesssectorheaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<BusinessSectorHeader>().ToList();
                    var listData = list.Select(item => _mapper.Map<BusinessSectorHeaderDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(BusinessSectorHeader), filterDto);
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
        /// Delete BusinessSectorHeader by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<BusinessSectorHeaderDto> Delete(int id)
        {
            var result = new ResponseBase<BusinessSectorHeaderDto>();

            try
            {
                var businesssectorheader = (BusinessSectorHeader)_businesssectorheaderMapper.Retrieve(id);
                if (businesssectorheader != null)
                {
                    businesssectorheader.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _businesssectorheaderMapper.Update(businesssectorheader, DNetUserName);
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
        /// Create a new BusinessSectorHeader
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<BusinessSectorHeaderDto> Create(BusinessSectorHeaderParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update BusinessSectorHeader
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<BusinessSectorHeaderDto> Update(BusinessSectorHeaderParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

