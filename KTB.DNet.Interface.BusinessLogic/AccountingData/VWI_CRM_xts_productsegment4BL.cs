#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_productsegment4 business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2020 15:15:00
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
    public class VWI_CRM_xts_productsegment4BL : AbstractBusinessLogic, IVWI_CRM_xts_productsegment4BL
    {
        #region Variables
        private IVWI_CRM_xts_productsegment4Repository<VWI_CRM_xts_productsegment4, int> _vWI_CRM_xts_productsegment4Repo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_productsegment4BL(IVWI_CRM_xts_productsegment4Repository<VWI_CRM_xts_productsegment4, int> vWI_CRM_xts_productsegment4Repo)
        {
            _vWI_CRM_xts_productsegment4Repo = vWI_CRM_xts_productsegment4Repo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_productsegment4 by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_productsegment4Dto>> Read(VWI_CRM_xts_productsegment4FilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xts_productsegment4Dto>> ReadList(VWI_CRM_xts_productsegment4FilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_productsegment4Dto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                // level data apm
                var innerQueryCriteria = string.Empty;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_productsegment4), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_productsegment4), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_productsegment4), filterDto);

                List<VWI_CRM_xts_productsegment4> data = _vWI_CRM_xts_productsegment4Repo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xts_productsegment4, VWI_CRM_xts_productsegment4Dto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_productsegment4), filterDto);
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
        /// Delete VWI_CRM_xts_productsegment4 by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_productsegment4Dto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_productsegment4
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_productsegment4Dto> Create(VWI_CRM_xts_productsegment4ParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_productsegment4
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_productsegment4Dto> Update(VWI_CRM_xts_productsegment4ParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}