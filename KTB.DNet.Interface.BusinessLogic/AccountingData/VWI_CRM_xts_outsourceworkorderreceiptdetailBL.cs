#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_outsourceworkorderreceiptdetail business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2020 10:42:01
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
    public class VWI_CRM_xts_outsourceworkorderreceiptdetailBL : AbstractBusinessLogic, IVWI_CRM_xts_outsourceworkorderreceiptdetailBL
    {
        #region Variables
        private IVWI_CRM_xts_outsourceworkorderreceiptdetailRepository<VWI_CRM_xts_outsourceworkorderreceiptdetail, int> _vWI_CRM_xts_outsourceworkorderreceiptdetailRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_outsourceworkorderreceiptdetailBL(IVWI_CRM_xts_outsourceworkorderreceiptdetailRepository<VWI_CRM_xts_outsourceworkorderreceiptdetail, int> vWI_CRM_xts_outsourceworkorderreceiptdetailRepo)
        {
            _vWI_CRM_xts_outsourceworkorderreceiptdetailRepo = vWI_CRM_xts_outsourceworkorderreceiptdetailRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_outsourceworkorderreceiptdetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_outsourceworkorderreceiptdetailDto>> Read(VWI_CRM_xts_outsourceworkorderreceiptdetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xts_outsourceworkorderreceiptdetailDto>> ReadList(VWI_CRM_xts_outsourceworkorderreceiptdetailFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_outsourceworkorderreceiptdetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_outsourceworkorderreceiptdetail), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by dealer company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);
                    }
                    else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to Dealer Configuration is not set");
                        return result;
                    }
                }
                else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                    return result;
                }

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_outsourceworkorderreceiptdetail), filterDto);

                List<VWI_CRM_xts_outsourceworkorderreceiptdetail> data = _vWI_CRM_xts_outsourceworkorderreceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xts_outsourceworkorderreceiptdetail, VWI_CRM_xts_outsourceworkorderreceiptdetailDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_outsourceworkorderreceiptdetail), filterDto);
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
        /// Delete VWI_CRM_xts_outsourceworkorderreceiptdetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_outsourceworkorderreceiptdetailDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_outsourceworkorderreceiptdetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_outsourceworkorderreceiptdetailDto> Create(VWI_CRM_xts_outsourceworkorderreceiptdetailParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_outsourceworkorderreceiptdetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_outsourceworkorderreceiptdetailDto> Update(VWI_CRM_xts_outsourceworkorderreceiptdetailParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}