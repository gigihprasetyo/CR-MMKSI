#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_purchaserequisitionpurchaseordertype business logic class
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
    public class VWI_CRM_xts_purchaserequisitionpurchaseordertypeBL : AbstractBusinessLogic, IVWI_CRM_xts_purchaserequisitionpurchaseordertypeBL
    {
        #region Variables
        private IVWI_CRM_xts_purchaserequisitionpurchaseordertypeRepository<VWI_CRM_xts_purchaserequisitionpurchaseordertype, int> _vWI_CRM_xts_purchaserequisitionpurchaseordertypeRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_purchaserequisitionpurchaseordertypeBL(IVWI_CRM_xts_purchaserequisitionpurchaseordertypeRepository<VWI_CRM_xts_purchaserequisitionpurchaseordertype, int> vWI_CRM_xts_purchaserequisitionpurchaseordertypeRepo)
        {
            _vWI_CRM_xts_purchaserequisitionpurchaseordertypeRepo = vWI_CRM_xts_purchaserequisitionpurchaseordertypeRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_purchaserequisitionpurchaseordertype by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_purchaserequisitionpurchaseordertypeDto>> Read(VWI_CRM_xts_purchaserequisitionpurchaseordertypeFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xts_purchaserequisitionpurchaseordertypeDto>> ReadList(VWI_CRM_xts_purchaserequisitionpurchaseordertypeFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_purchaserequisitionpurchaseordertypeDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_purchaserequisitionpurchaseordertype), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by dealer company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_purchaserequisitionpurchaseordertype), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_purchaserequisitionpurchaseordertype), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_company", dealerCompanyCode, false, criterias);
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

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_purchaserequisitionpurchaseordertype), filterDto);

                List<VWI_CRM_xts_purchaserequisitionpurchaseordertype> data = _vWI_CRM_xts_purchaserequisitionpurchaseordertypeRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xts_purchaserequisitionpurchaseordertype, VWI_CRM_xts_purchaserequisitionpurchaseordertypeDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_purchaserequisitionpurchaseordertype), filterDto);
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
        /// Delete VWI_CRM_xts_purchaserequisitionpurchaseordertype by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_purchaserequisitionpurchaseordertypeDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_purchaserequisitionpurchaseordertypeDto> Create(VWI_CRM_xts_purchaserequisitionpurchaseordertypeParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_purchaserequisitionpurchaseordertypeDto> Update(VWI_CRM_xts_purchaserequisitionpurchaseordertypeParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}