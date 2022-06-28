	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_ktb_leasingcompany business logic class
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
    public class VWI_CRM_ktb_leasingcompanyBL : AbstractBusinessLogic, IVWI_CRM_ktb_leasingcompanyBL
    {
        #region Variables
        private IVWI_CRM_ktb_leasingcompanyRepository<VWI_CRM_ktb_leasingcompany, int> _vWI_CRM_ktb_leasingcompanyRepo;
        #endregion

        #region Constructor
        public VWI_CRM_ktb_leasingcompanyBL(IVWI_CRM_ktb_leasingcompanyRepository<VWI_CRM_ktb_leasingcompany, int> vWI_CRM_ktb_leasingcompanyRepo)
        {
            _vWI_CRM_ktb_leasingcompanyRepo = vWI_CRM_ktb_leasingcompanyRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_ktb_leasingcompany by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_leasingcompanyDto>> Read(VWI_CRM_ktb_leasingcompanyFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_ktb_leasingcompanyDto>> ReadList(VWI_CRM_ktb_leasingcompanyFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_ktb_leasingcompanyDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_leasingcompany), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_leasingcompany), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                // level data = APM
                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_leasingcompany), filterDto);

                List<VWI_CRM_ktb_leasingcompany> data = _vWI_CRM_ktb_leasingcompanyRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_ktb_leasingcompany, VWI_CRM_ktb_leasingcompanyDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_leasingcompany), filterDto);
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
        /// Delete VWI_CRM_ktb_leasingcompany by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_leasingcompanyDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_ktb_leasingcompany
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_leasingcompanyDto> Create(VWI_CRM_ktb_leasingcompanyParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_ktb_leasingcompany
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_leasingcompanyDto> Update(VWI_CRM_ktb_leasingcompanyParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}