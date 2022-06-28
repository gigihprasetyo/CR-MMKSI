	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_newvehicleexteriorcolor business logic class
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
    public class VWI_CRM_xts_newvehicleexteriorcolorBL : AbstractBusinessLogic, IVWI_CRM_xts_newvehicleexteriorcolorBL
    {
        #region Variables
        private IVWI_CRM_xts_newvehicleexteriorcolorRepository<VWI_CRM_xts_newvehicleexteriorcolor, int> _vWI_CRM_xts_newvehicleexteriorcolorRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_newvehicleexteriorcolorBL(IVWI_CRM_xts_newvehicleexteriorcolorRepository<VWI_CRM_xts_newvehicleexteriorcolor, int> vWI_CRM_xts_newvehicleexteriorcolorRepo)
        {
            _vWI_CRM_xts_newvehicleexteriorcolorRepo = vWI_CRM_xts_newvehicleexteriorcolorRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_newvehicleexteriorcolor by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_newvehicleexteriorcolorDto>> Read(VWI_CRM_xts_newvehicleexteriorcolorFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_xts_newvehicleexteriorcolorDto>> ReadList(VWI_CRM_xts_newvehicleexteriorcolorFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_newvehicleexteriorcolorDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                // Level dta apm
                var innerQueryCriteria = string.Empty;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_newvehicleexteriorcolor), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_newvehicleexteriorcolor), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_newvehicleexteriorcolor), filterDto);

                List<VWI_CRM_xts_newvehicleexteriorcolor> data = _vWI_CRM_xts_newvehicleexteriorcolorRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xts_newvehicleexteriorcolor, VWI_CRM_xts_newvehicleexteriorcolorDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_newvehicleexteriorcolor), filterDto);
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
        /// Delete VWI_CRM_xts_newvehicleexteriorcolor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_newvehicleexteriorcolorDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_newvehicleexteriorcolor
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_newvehicleexteriorcolorDto> Create(VWI_CRM_xts_newvehicleexteriorcolorParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_newvehicleexteriorcolor
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_newvehicleexteriorcolorDto> Update(VWI_CRM_xts_newvehicleexteriorcolorParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}