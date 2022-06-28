	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransfer business logic class
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
    public class VWI_CRM_PRT_OutgoingInventoryTransferBL : AbstractBusinessLogic, IVWI_CRM_PRT_OutgoingInventoryTransferBL
    {
        #region Variables
        private IVWI_CRM_PRT_OutgoingInventoryTransferRepository<VWI_CRM_PRT_OutgoingInventoryTransfer, int> _vWI_CRM_PRT_OutgoingInventoryTransferRepo;
        #endregion

        #region Constructor
        public VWI_CRM_PRT_OutgoingInventoryTransferBL(IVWI_CRM_PRT_OutgoingInventoryTransferRepository<VWI_CRM_PRT_OutgoingInventoryTransfer, int> vWI_CRM_PRT_OutgoingInventoryTransferRepo)
        {
            _vWI_CRM_PRT_OutgoingInventoryTransferRepo = vWI_CRM_PRT_OutgoingInventoryTransferRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_PRT_OutgoingInventoryTransfer by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_PRT_OutgoingInventoryTransferDto>> Read(VWI_CRM_PRT_OutgoingInventoryTransferFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_PRT_OutgoingInventoryTransferDto>> ReadList(VWI_CRM_PRT_OutgoingInventoryTransferFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_PRT_OutgoingInventoryTransferDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_PRT_OutgoingInventoryTransfer), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {	
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_PRT_OutgoingInventoryTransfer), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode,false,criterias);
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

                sortColl = Helper.UpdateSortColumn(typeof(VWI_CRM_PRT_OutgoingInventoryTransfer), filterDto, sortColl);

                List<VWI_CRM_PRT_OutgoingInventoryTransfer> data = _vWI_CRM_PRT_OutgoingInventoryTransferRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_PRT_OutgoingInventoryTransfer, VWI_CRM_PRT_OutgoingInventoryTransferDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_PRT_OutgoingInventoryTransfer), filterDto);
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
        /// Delete VWI_CRM_PRT_OutgoingInventoryTransfer by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_OutgoingInventoryTransferDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_PRT_OutgoingInventoryTransfer
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_OutgoingInventoryTransferDto> Create(VWI_CRM_PRT_OutgoingInventoryTransferParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_PRT_OutgoingInventoryTransfer
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_OutgoingInventoryTransferDto> Update(VWI_CRM_PRT_OutgoingInventoryTransferParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}