﻿#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_outsourceworkshopconfiguration business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/28/2019 08:51:00
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
    public class VWI_CRM_xts_outsourceworkshopconfigurationBL : AbstractBusinessLogic, IVWI_CRM_xts_outsourceworkshopconfigurationBL
    {
        #region Variables
        private IVWI_CRM_xts_outsourceworkshopconfigurationRepository<VWI_CRM_xts_outsourceworkshopconfiguration, int> _vWI_CRM_xts_outsourceworkshopconfigurationRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_outsourceworkshopconfigurationBL(IVWI_CRM_xts_outsourceworkshopconfigurationRepository<VWI_CRM_xts_outsourceworkshopconfiguration, int> vWI_CRM_xts_outsourceworkshopconfigurationRepo)
        {
            _vWI_CRM_xts_outsourceworkshopconfigurationRepo = vWI_CRM_xts_outsourceworkshopconfigurationRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_outsourceworkshopconfiguration by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_outsourceworkshopconfigurationDto>> Read(VWI_CRM_xts_outsourceworkshopconfigurationFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xts_outsourceworkshopconfigurationDto>> ReadList(VWI_CRM_xts_outsourceworkshopconfigurationFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_outsourceworkshopconfigurationDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_outsourceworkshopconfiguration), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by Company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_outsourceworkshopconfiguration), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_outsourceworkshopconfiguration), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_outsourceworkshopconfiguration), filterDto);

                        List<VWI_CRM_xts_outsourceworkshopconfiguration> data = _vWI_CRM_xts_outsourceworkshopconfigurationRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_xts_outsourceworkshopconfiguration, VWI_CRM_xts_outsourceworkshopconfigurationDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_outsourceworkshopconfiguration), filterDto);
                        }

                        result.success = true;
                    }
                    else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to Dealer Configuration is not set");
                    }
                }
                else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                }
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
        /// Delete VWI_CRM_xts_outsourceworkshopconfiguration by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_outsourceworkshopconfigurationDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_outsourceworkshopconfiguration
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_outsourceworkshopconfigurationDto> Create(VWI_CRM_xts_outsourceworkshopconfigurationParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_outsourceworkshopconfiguration
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_outsourceworkshopconfigurationDto> Update(VWI_CRM_xts_outsourceworkshopconfigurationParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}