	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_SVC_ARReceiptDetailBasedOnWO business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2019 5:45:18 PM
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
    public class VWI_CRM_SVC_ARReceiptDetailBasedOnWOBL : AbstractBusinessLogic, IVWI_CRM_SVC_ARReceiptDetailBasedOnWOBL
    {
        #region Variables
        private IVWI_CRM_SVC_ARReceiptDetailBasedOnWORepository<VWI_CRM_SVC_ARReceiptDetailBasedOnWO, int> _vWI_CRM_SVC_ARReceiptDetailBasedOnWORepo;
        #endregion

        #region Constructor
        public VWI_CRM_SVC_ARReceiptDetailBasedOnWOBL(IVWI_CRM_SVC_ARReceiptDetailBasedOnWORepository<VWI_CRM_SVC_ARReceiptDetailBasedOnWO, int> vWI_CRM_SVC_ARReceiptDetailBasedOnWORepo)
        {
            _vWI_CRM_SVC_ARReceiptDetailBasedOnWORepo = vWI_CRM_SVC_ARReceiptDetailBasedOnWORepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_SVC_ARReceiptDetailBasedOnWO by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_SVC_ARReceiptDetailBasedOnWODto>> Read(VWI_CRM_SVC_ARReceiptDetailBasedOnWOFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_SVC_ARReceiptDetailBasedOnWODto>> ReadList(VWI_CRM_SVC_ARReceiptDetailBasedOnWOFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_SVC_ARReceiptDetailBasedOnWODto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_SVC_ARReceiptDetailBasedOnWO), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
						// filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_SVC_ARReceiptDetailBasedOnWO), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", dealerCompanyCode,false,criterias);
                    }
                    else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to Dealer Configuration is not set");
                        return result;
                    }
                }else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                    return result;
                }

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_SVC_ARReceiptDetailBasedOnWO), filterDto);

                List<VWI_CRM_SVC_ARReceiptDetailBasedOnWO> data = _vWI_CRM_SVC_ARReceiptDetailBasedOnWORepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_SVC_ARReceiptDetailBasedOnWO, VWI_CRM_SVC_ARReceiptDetailBasedOnWODto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_SVC_ARReceiptDetailBasedOnWO), filterDto);
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
        /// Delete VWI_CRM_SVC_ARReceiptDetailBasedOnWO by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_SVC_ARReceiptDetailBasedOnWODto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_SVC_ARReceiptDetailBasedOnWODto> Create(VWI_CRM_SVC_ARReceiptDetailBasedOnWOParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_SVC_ARReceiptDetailBasedOnWODto> Update(VWI_CRM_SVC_ARReceiptDetailBasedOnWOParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}