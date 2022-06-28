﻿#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_dimension6 business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 10:41:00 
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
    public class VWI_CRM_xts_dimension6BL : AbstractBusinessLogic, IVWI_CRM_xts_dimension6BL
    {
        #region Variables
        private IVWI_CRM_xts_dimension6Repository<VWI_CRM_xts_dimension6, int> _vWI_CRM_xts_dimension6Repo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_dimension6BL(IVWI_CRM_xts_dimension6Repository<VWI_CRM_xts_dimension6, int> vWI_CRM_xts_dimension6Repo)
        {
            _vWI_CRM_xts_dimension6Repo = vWI_CRM_xts_dimension6Repo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_dimension6 by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_dimension6Dto>> Read(VWI_CRM_xts_dimension6FilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xts_dimension6Dto>> ReadList(VWI_CRM_xts_dimension6FilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_dimension6Dto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_dimension6), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_dimension6), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_dimension6), filterDto);

                List<VWI_CRM_xts_dimension6> data = _vWI_CRM_xts_dimension6Repo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xts_dimension6, VWI_CRM_xts_dimension6Dto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_dimension6), filterDto);
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
        /// Delete VWI_CRM_xts_dimension6 by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_dimension6Dto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_dimension6
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_dimension6Dto> Create(VWI_CRM_xts_dimension6ParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_dimension6
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_dimension6Dto> Update(VWI_CRM_xts_dimension6ParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}
