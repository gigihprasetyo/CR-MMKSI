#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceHistory business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 16/10/2018 3:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_PRHistoryPOStatusCancelBL : AbstractBusinessLogic, IVWI_PRHistoryPOStatusCancelBL
    {
        #region Variables
        private readonly IMapper _prHIstoryPoStatusCancelMapper;
        IPRHistoryPOStatusCancelRepository<VWI_PRHistoryPOStatusCancel, int> _pRHistoryPOStatusCancelRepository;
        #endregion

        #region Constructor
        public VWI_PRHistoryPOStatusCancelBL(IPRHistoryPOStatusCancelRepository<VWI_PRHistoryPOStatusCancel, int> pRHistoryPOStatusCancelRepository)
        {
            _prHIstoryPoStatusCancelMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_PRHistoryPOStatusCancel).ToString());
            _pRHistoryPOStatusCancelRepository = pRHistoryPOStatusCancelRepository;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Read PR History PO Status Cancel
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_PRHistoryPOStatusCancelDto>> Read(VWI_PRHistoryPOStatusCancelFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_PRHistoryPOStatusCancelDto>>();
            var list = new List<VWI_PRHistoryPOStatusCancel>();
            var sortColl = new SortCollection();
            int filteredTotalRow = 0;
            int totalRow = 0;

            try
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(VWI_PRHistoryPOStatusCancel), "DealerCode", MatchType.Exact, DealerCode));

                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_PRHistoryPOStatusCancel), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_PRHistoryPOStatusCancel), filterDto, sortColl);

                List<VWI_PRHistoryPOStatusCancel> data = _pRHistoryPOStatusCancelRepository.Search(
                                    criterias, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_PRHistoryPOStatusCancel, VWI_PRHistoryPOStatusCancelDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_PRHistoryPOStatusCancel), filterDto);
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

