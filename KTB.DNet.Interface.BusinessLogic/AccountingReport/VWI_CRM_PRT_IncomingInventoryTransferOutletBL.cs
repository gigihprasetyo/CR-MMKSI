	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransferOutlet business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/01/2020 10:25:21
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
    public class VWI_CRM_PRT_IncomingInventoryTransferOutletBL : AbstractBusinessLogic, IVWI_CRM_PRT_IncomingInventoryTransferOutletBL
    {
        #region Variables
        private IVWI_CRM_PRT_IncomingInventoryTransferOutletRepository<VWI_CRM_PRT_IncomingInventoryTransferOutlet, int> _vWI_CRM_PRT_IncomingInventoryTransferOutletRepo;
        #endregion

        #region Constructor
        public VWI_CRM_PRT_IncomingInventoryTransferOutletBL(IVWI_CRM_PRT_IncomingInventoryTransferOutletRepository<VWI_CRM_PRT_IncomingInventoryTransferOutlet, int> vWI_CRM_PRT_IncomingInventoryTransferOutletRepo)
        {
            _vWI_CRM_PRT_IncomingInventoryTransferOutletRepo = vWI_CRM_PRT_IncomingInventoryTransferOutletRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_PRT_IncomingInventoryTransferOutlet by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_PRT_IncomingInventoryTransferOutletDto>> Read(VWI_CRM_PRT_IncomingInventoryTransferOutletFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_PRT_IncomingInventoryTransferOutletDto>> ReadList(VWI_CRM_PRT_IncomingInventoryTransferOutletFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_PRT_IncomingInventoryTransferOutletDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
				var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_PRT_IncomingInventoryTransferOutlet), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
                        // filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_PRT_IncomingInventoryTransferOutlet), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode,false,criterias);
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

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_PRT_IncomingInventoryTransferOutlet), filterDto);

                List<VWI_CRM_PRT_IncomingInventoryTransferOutlet> data = _vWI_CRM_PRT_IncomingInventoryTransferOutletRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_PRT_IncomingInventoryTransferOutlet, VWI_CRM_PRT_IncomingInventoryTransferOutletDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_PRT_IncomingInventoryTransferOutlet), filterDto);
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
        /// Delete VWI_CRM_PRT_IncomingInventoryTransferOutlet by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_IncomingInventoryTransferOutletDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_IncomingInventoryTransferOutletDto> Create(VWI_CRM_PRT_IncomingInventoryTransferOutletParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_IncomingInventoryTransferOutletDto> Update(VWI_CRM_PRT_IncomingInventoryTransferOutletParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}