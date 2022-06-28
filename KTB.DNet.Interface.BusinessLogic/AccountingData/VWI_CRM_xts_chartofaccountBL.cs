	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_chartofaccount business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2019 10:09:30 AM
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Framework;	
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_CRM_xts_chartofaccountBL : AbstractBusinessLogic, IVWI_CRM_xts_chartofaccountBL
    {
        #region Variables
        private IVWI_CRM_xts_chartofaccountRepository<VWI_CRM_xts_chartofaccount, int> _vWI_CRM_xts_chartofaccountRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_chartofaccountBL(IVWI_CRM_xts_chartofaccountRepository<VWI_CRM_xts_chartofaccount, int> vWI_CRM_xts_chartofaccountRepo)
        {
            _vWI_CRM_xts_chartofaccountRepo = vWI_CRM_xts_chartofaccountRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_chartofaccount by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_chartofaccountDto>> Read(VWI_CRM_xts_chartofaccountFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_xts_chartofaccountDto>> ReadList(VWI_CRM_xts_chartofaccountFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_chartofaccountDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                // Level data APM
                var innerQueryCriteria = string.Empty;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_chartofaccount), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_chartofaccount), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_chartofaccount), filterDto);

                List<VWI_CRM_xts_chartofaccount> data = _vWI_CRM_xts_chartofaccountRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xts_chartofaccount, VWI_CRM_xts_chartofaccountDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_chartofaccount), filterDto);
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
        /// Delete VWI_CRM_xts_chartofaccount by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_chartofaccountDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_chartofaccount
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_chartofaccountDto> Create(VWI_CRM_xts_chartofaccountParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_chartofaccount
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_chartofaccountDto> Update(VWI_CRM_xts_chartofaccountParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}