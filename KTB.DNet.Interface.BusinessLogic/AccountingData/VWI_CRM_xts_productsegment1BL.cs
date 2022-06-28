#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_productsegment1 business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2020 14:19:00
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
    public class VWI_CRM_xts_productsegment1BL : AbstractBusinessLogic, IVWI_CRM_xts_productsegment1BL
    {
        #region Variables
        private IVWI_CRM_xts_productsegment1Repository<VWI_CRM_xts_productsegment1, int> _vWI_CRM_xts_productsegment1Repo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_productsegment1BL(IVWI_CRM_xts_productsegment1Repository<VWI_CRM_xts_productsegment1, int> vWI_CRM_xts_productsegment1Repo)
        {
            _vWI_CRM_xts_productsegment1Repo = vWI_CRM_xts_productsegment1Repo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_productsegment1 by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_productsegment1Dto>> Read(VWI_CRM_xts_productsegment1FilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xts_productsegment1Dto>> ReadList(VWI_CRM_xts_productsegment1FilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_productsegment1Dto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                // level data apm
                var innerQueryCriteria = string.Empty;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_productsegment1), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_productsegment1), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_productsegment1), filterDto);

                List<VWI_CRM_xts_productsegment1> data = _vWI_CRM_xts_productsegment1Repo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xts_productsegment1, VWI_CRM_xts_productsegment1Dto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_productsegment1), filterDto);
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
        /// Delete VWI_CRM_xts_productsegment1 by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_productsegment1Dto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_productsegment1
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_productsegment1Dto> Create(VWI_CRM_xts_productsegment1ParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_productsegment1
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_productsegment1Dto> Update(VWI_CRM_xts_productsegment1ParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}