	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_AX_PRT_StockMovement business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/9/2019 1:55:37 PM
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
    public class VWI_AX_PRT_StockMovementBL : AbstractBusinessLogic, IVWI_AX_PRT_StockMovementBL
    {
        #region Variables
        private IVWI_AX_PRT_StockMovementRepository<VWI_AX_PRT_StockMovement, int> _vWI_AX_PRT_StockMovementRepo;
        #endregion

        #region Constructor
        public VWI_AX_PRT_StockMovementBL(IVWI_AX_PRT_StockMovementRepository<VWI_AX_PRT_StockMovement, int> vWI_AX_PRT_StockMovementRepo)
        {
            _vWI_AX_PRT_StockMovementRepo = vWI_AX_PRT_StockMovementRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_AX_PRT_StockMovement by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_AX_PRT_StockMovementDto>> Read(VWI_AX_PRT_StockMovementFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_AX_PRT_StockMovementDto>> ReadList(VWI_AX_PRT_StockMovementFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_AX_PRT_StockMovementDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_AX_PRT_StockMovement), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {
                    // filter by dealer company
                    if(listDealer.Count > 0)
                    {
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_AX_PRT_StockMovement), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Company", dealerCompanyCode,false,criterias);
                    }else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                        return result;
                    }
                }else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                    return result;
                }

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_AX_PRT_StockMovement), filterDto);

                List<VWI_AX_PRT_StockMovement> data = _vWI_AX_PRT_StockMovementRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_AX_PRT_StockMovement, VWI_AX_PRT_StockMovementDto>();
                    result.total = filteredTotalRow;
                    result.success = true;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_AX_PRT_StockMovement), filterDto);
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
        /// Delete VWI_AX_PRT_StockMovement by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_PRT_StockMovementDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_AX_PRT_StockMovement
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_PRT_StockMovementDto> Create(VWI_AX_PRT_StockMovementParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_AX_PRT_StockMovement
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_PRT_StockMovementDto> Update(VWI_AX_PRT_StockMovementParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}