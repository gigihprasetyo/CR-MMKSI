	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_ktb_perlengkapanstandard business logic class
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
    public class VWI_CRM_ktb_perlengkapanstandardBL : AbstractBusinessLogic, IVWI_CRM_ktb_perlengkapanstandardBL
    {
        #region Variables
        private IVWI_CRM_ktb_perlengkapanstandardRepository<VWI_CRM_ktb_perlengkapanstandard, int> _vWI_CRM_ktb_perlengkapanstandardRepo;
        #endregion

        #region Constructor
        public VWI_CRM_ktb_perlengkapanstandardBL(IVWI_CRM_ktb_perlengkapanstandardRepository<VWI_CRM_ktb_perlengkapanstandard, int> vWI_CRM_ktb_perlengkapanstandardRepo)
        {
            _vWI_CRM_ktb_perlengkapanstandardRepo = vWI_CRM_ktb_perlengkapanstandardRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_ktb_perlengkapanstandard by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_perlengkapanstandardDto>> Read(VWI_CRM_ktb_perlengkapanstandardFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_ktb_perlengkapanstandardDto>> ReadList(VWI_CRM_ktb_perlengkapanstandardFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_ktb_perlengkapanstandardDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_perlengkapanstandard), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_perlengkapanstandard), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                // data level APM
                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_perlengkapanstandard), filterDto);

                List<VWI_CRM_ktb_perlengkapanstandard> data = _vWI_CRM_ktb_perlengkapanstandardRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_ktb_perlengkapanstandard, VWI_CRM_ktb_perlengkapanstandardDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_perlengkapanstandard), filterDto);
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
        /// Delete VWI_CRM_ktb_perlengkapanstandard by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_perlengkapanstandardDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_ktb_perlengkapanstandard
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_perlengkapanstandardDto> Create(VWI_CRM_ktb_perlengkapanstandardParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_ktb_perlengkapanstandard
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_perlengkapanstandardDto> Update(VWI_CRM_ktb_perlengkapanstandardParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}