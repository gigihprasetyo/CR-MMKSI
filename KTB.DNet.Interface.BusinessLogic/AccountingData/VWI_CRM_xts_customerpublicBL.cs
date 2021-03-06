	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_customerpublic business logic class
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
    public class VWI_CRM_xts_customerpublicBL : AbstractBusinessLogic, IVWI_CRM_xts_customerpublicBL
    {
        #region Variables
        private IVWI_CRM_xts_customerpublicRepository<VWI_CRM_xts_customerpublic, int> _vWI_CRM_xts_customerpublicRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_customerpublicBL(IVWI_CRM_xts_customerpublicRepository<VWI_CRM_xts_customerpublic, int> vWI_CRM_xts_customerpublicRepo)
        {
            _vWI_CRM_xts_customerpublicRepo = vWI_CRM_xts_customerpublicRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_customerpublic by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_customerpublicDto>> Read(VWI_CRM_xts_customerpublicFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_xts_customerpublicDto>> ReadList(VWI_CRM_xts_customerpublicFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_customerpublicDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_customerpublic), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_customerpublic), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                // without filter by bu/ company (level organization)
                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_customerpublic), filterDto);

                List<VWI_CRM_xts_customerpublic> data = _vWI_CRM_xts_customerpublicRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xts_customerpublic, VWI_CRM_xts_customerpublicDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_customerpublic), filterDto);
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
        /// Delete VWI_CRM_xts_customerpublic by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_customerpublicDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_customerpublic
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_customerpublicDto> Create(VWI_CRM_xts_customerpublicParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_customerpublic
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_customerpublicDto> Update(VWI_CRM_xts_customerpublicParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}