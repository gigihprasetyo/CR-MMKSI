#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_servicetemplateparentgroupdetail business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 16:35:00
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
    public class VWI_CRM_xts_servicetemplateparentgroupdetailBL : AbstractBusinessLogic, IVWI_CRM_xts_servicetemplateparentgroupdetailBL
    {
        #region Variables
        private IVWI_CRM_xts_servicetemplateparentgroupdetailRepository<VWI_CRM_xts_servicetemplateparentgroupdetail, int> _vWI_CRM_xts_servicetemplateparentgroupdetailRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_servicetemplateparentgroupdetailBL(IVWI_CRM_xts_servicetemplateparentgroupdetailRepository<VWI_CRM_xts_servicetemplateparentgroupdetail, int> vWI_CRM_xts_servicetemplateparentgroupdetailRepo)
        {
            _vWI_CRM_xts_servicetemplateparentgroupdetailRepo = vWI_CRM_xts_servicetemplateparentgroupdetailRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_servicetemplateparentgroupdetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_servicetemplateparentgroupdetailDto>> Read(VWI_CRM_xts_servicetemplateparentgroupdetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xts_servicetemplateparentgroupdetailDto>> ReadList(VWI_CRM_xts_servicetemplateparentgroupdetailFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_servicetemplateparentgroupdetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_servicetemplateparentgroupdetail), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by Company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_servicetemplateparentgroupdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_servicetemplateparentgroupdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_servicetemplateparentgroupdetail), filterDto);

                        List<VWI_CRM_xts_servicetemplateparentgroupdetail> data = _vWI_CRM_xts_servicetemplateparentgroupdetailRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_xts_servicetemplateparentgroupdetail, VWI_CRM_xts_servicetemplateparentgroupdetailDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_servicetemplateparentgroupdetail), filterDto);
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
        /// Delete VWI_CRM_xts_servicetemplateparentgroupdetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_servicetemplateparentgroupdetailDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_servicetemplateparentgroupdetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_servicetemplateparentgroupdetailDto> Create(VWI_CRM_xts_servicetemplateparentgroupdetailParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_servicetemplateparentgroupdetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_servicetemplateparentgroupdetailDto> Update(VWI_CRM_xts_servicetemplateparentgroupdetailParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}