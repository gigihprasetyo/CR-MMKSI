#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xjp_registrationcolor business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 13:22:00
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
    public class VWI_CRM_xjp_registrationcolorBL : AbstractBusinessLogic, IVWI_CRM_xjp_registrationcolorBL
    {
        #region Variables
        private IVWI_CRM_xjp_registrationcolorRepository<VWI_CRM_xjp_registrationcolor, int> _vWI_CRM_xjp_registrationcolorRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xjp_registrationcolorBL(IVWI_CRM_xjp_registrationcolorRepository<VWI_CRM_xjp_registrationcolor, int> vWI_CRM_xjp_registrationcolorRepo)
        {
            _vWI_CRM_xjp_registrationcolorRepo = vWI_CRM_xjp_registrationcolorRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xjp_registrationcolor by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xjp_registrationcolorDto>> Read(VWI_CRM_xjp_registrationcolorFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xjp_registrationcolorDto>> ReadList(VWI_CRM_xjp_registrationcolorFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xjp_registrationcolorDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xjp_registrationcolor), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xjp_registrationcolor), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xjp_registrationcolor), filterDto);

                List<VWI_CRM_xjp_registrationcolor> data = _vWI_CRM_xjp_registrationcolorRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xjp_registrationcolor, VWI_CRM_xjp_registrationcolorDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xjp_registrationcolor), filterDto);
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
        /// Delete VWI_CRM_xjp_registrationcolor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xjp_registrationcolorDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xjp_registrationcolor
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xjp_registrationcolorDto> Create(VWI_CRM_xjp_registrationcolorParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xjp_registrationcolor
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xjp_registrationcolorDto> Update(VWI_CRM_xjp_registrationcolorParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}
