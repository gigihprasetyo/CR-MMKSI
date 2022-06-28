	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferOutlet business logic class
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
    public class VWI_CRM_PRT_OutgoingInventoryTransferOutletBL : AbstractBusinessLogic, IVWI_CRM_PRT_OutgoingInventoryTransferOutletBL
    {
        #region Variables
        private IVWI_CRM_PRT_OutgoingInventoryTransferOutletRepository<VWI_CRM_PRT_OutgoingInventoryTransferOutlet, int> _vWI_CRM_PRT_OutgoingInventoryTransferOutletRepo;
        #endregion

        #region Constructor
        public VWI_CRM_PRT_OutgoingInventoryTransferOutletBL(IVWI_CRM_PRT_OutgoingInventoryTransferOutletRepository<VWI_CRM_PRT_OutgoingInventoryTransferOutlet, int> vWI_CRM_PRT_OutgoingInventoryTransferOutletRepo)
        {
            _vWI_CRM_PRT_OutgoingInventoryTransferOutletRepo = vWI_CRM_PRT_OutgoingInventoryTransferOutletRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_PRT_OutgoingInventoryTransferOutlet by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_PRT_OutgoingInventoryTransferOutletDto>> Read(VWI_CRM_PRT_OutgoingInventoryTransferOutletFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_PRT_OutgoingInventoryTransferOutletDto>> ReadList(VWI_CRM_PRT_OutgoingInventoryTransferOutletFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_PRT_OutgoingInventoryTransferOutletDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
				var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_PRT_OutgoingInventoryTransferOutlet), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
                        // filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_PRT_OutgoingInventoryTransferOutlet), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode,false,criterias);
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

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_PRT_OutgoingInventoryTransferOutlet), filterDto);

                List<VWI_CRM_PRT_OutgoingInventoryTransferOutlet> data = _vWI_CRM_PRT_OutgoingInventoryTransferOutletRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_PRT_OutgoingInventoryTransferOutlet, VWI_CRM_PRT_OutgoingInventoryTransferOutletDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_PRT_OutgoingInventoryTransferOutlet), filterDto);
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
        /// Delete VWI_CRM_PRT_OutgoingInventoryTransferOutlet by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_OutgoingInventoryTransferOutletDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_OutgoingInventoryTransferOutletDto> Create(VWI_CRM_PRT_OutgoingInventoryTransferOutletParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_OutgoingInventoryTransferOutletDto> Update(VWI_CRM_PRT_OutgoingInventoryTransferOutletParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}