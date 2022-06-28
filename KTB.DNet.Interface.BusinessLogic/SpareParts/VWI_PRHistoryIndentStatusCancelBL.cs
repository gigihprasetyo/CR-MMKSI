#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PRHistoryIndentStatusCancel business logic class
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
    public class VWI_PRHistoryIndentStatusCancelBL : AbstractBusinessLogic, IVWI_PRHistoryIndentStatusCancelBL
    {
        #region Variables
        private readonly IMapper _vwi_prhistoryindentstatuscancelMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VWI_PRHistoryIndentStatusCancelBL()
        {
            _vwi_prhistoryindentstatuscancelMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_PRHistoryIndentStatusCancel).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_PRHistoryIndentStatusCancel by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_PRHistoryIndentStatusCancelDto>> Read(VWI_PRHistoryIndentStatusCancelFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_PRHistoryIndentStatusCancel), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_PRHistoryIndentStatusCancelDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_PRHistoryIndentStatusCancel), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_PRHistoryIndentStatusCancel), filterDto, sortColl);

                // get data
                var data = _vwi_prhistoryindentstatuscancelMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_PRHistoryIndentStatusCancel>().ToList();
                    var listData = list.Select(item => _mapper.Map<VWI_PRHistoryIndentStatusCancelDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_PRHistoryIndentStatusCancel), filterDto);
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
    }
}