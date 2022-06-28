#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_termofpayment business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 08:37:00
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
    public class VWI_CRM_xts_termofpaymentBL : AbstractBusinessLogic, IVWI_CRM_xts_termofpaymentBL
    {
        #region Variables
        private IVWI_CRM_xts_termofpaymentRepository<VWI_CRM_xts_termofpayment, int> _vWI_CRM_xts_termofpaymentRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_termofpaymentBL(IVWI_CRM_xts_termofpaymentRepository<VWI_CRM_xts_termofpayment, int> vWI_CRM_xts_termofpaymentRepo)
        {
            _vWI_CRM_xts_termofpaymentRepo = vWI_CRM_xts_termofpaymentRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_termofpayment by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_termofpaymentDto>> Read(VWI_CRM_xts_termofpaymentFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xts_termofpaymentDto>> ReadList(VWI_CRM_xts_termofpaymentFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_termofpaymentDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_termofpayment), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by Company
                        //criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_termofpayment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_termofpayment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "RowStatus", "0", false, criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_termofpayment), filterDto);

                        List<VWI_CRM_xts_termofpayment> data = _vWI_CRM_xts_termofpaymentRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_xts_termofpayment, VWI_CRM_xts_termofpaymentDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_termofpayment), filterDto);
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
        /// Delete VWI_CRM_xts_termofpayment by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_termofpaymentDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_termofpayment
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_termofpaymentDto> Create(VWI_CRM_xts_termofpaymentParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_termofpayment
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_termofpaymentDto> Update(VWI_CRM_xts_termofpaymentParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}
