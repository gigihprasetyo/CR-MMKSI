	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransfer business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2019 4:57:58 PM
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
    public class VWI_CRM_PRT_IncomingInventoryTransferBL : AbstractBusinessLogic, IVWI_CRM_PRT_IncomingInventoryTransferBL
    {
        #region Variables
        private IVWI_CRM_PRT_IncomingInventoryTransferRepository<VWI_CRM_PRT_IncomingInventoryTransfer, int> _vWI_CRM_PRT_IncomingInventoryTransferRepo;
        #endregion

        #region Constructor
        public VWI_CRM_PRT_IncomingInventoryTransferBL(IVWI_CRM_PRT_IncomingInventoryTransferRepository<VWI_CRM_PRT_IncomingInventoryTransfer, int> vWI_CRM_PRT_IncomingInventoryTransferRepo)
        {
            _vWI_CRM_PRT_IncomingInventoryTransferRepo = vWI_CRM_PRT_IncomingInventoryTransferRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_PRT_IncomingInventoryTransfer by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_PRT_IncomingInventoryTransferDto>> Read(VWI_CRM_PRT_IncomingInventoryTransferFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_PRT_IncomingInventoryTransferDto>> ReadList(VWI_CRM_PRT_IncomingInventoryTransferFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_PRT_IncomingInventoryTransferDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_PRT_IncomingInventoryTransfer), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {						
						// filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_PRT_IncomingInventoryTransfer), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", dealerCompanyCode,false,criterias);
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

                sortColl = Helper.UpdateSortColumn(typeof(VWI_CRM_PRT_IncomingInventoryTransfer), filterDto, sortColl);

                List<VWI_CRM_PRT_IncomingInventoryTransfer> data = _vWI_CRM_PRT_IncomingInventoryTransferRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_PRT_IncomingInventoryTransfer, VWI_CRM_PRT_IncomingInventoryTransferDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_PRT_IncomingInventoryTransfer), filterDto);
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
        /// Delete VWI_CRM_PRT_IncomingInventoryTransfer by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_IncomingInventoryTransferDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_PRT_IncomingInventoryTransfer
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_IncomingInventoryTransferDto> Create(VWI_CRM_PRT_IncomingInventoryTransferParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_PRT_IncomingInventoryTransfer
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_IncomingInventoryTransferDto> Update(VWI_CRM_PRT_IncomingInventoryTransferParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}