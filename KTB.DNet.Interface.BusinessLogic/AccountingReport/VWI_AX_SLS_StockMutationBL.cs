	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_AX_SLS_StockMutation business logic class
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
    public class VWI_AX_SLS_StockMutationBL : AbstractBusinessLogic, IVWI_AX_SLS_StockMutationBL
    {
        #region Variables
        private IVWI_AX_SLS_StockMutationRepository<VWI_AX_SLS_StockMutation, int> _vWI_AX_SLS_StockMutationRepo;
        #endregion

        #region Constructor
        public VWI_AX_SLS_StockMutationBL(IVWI_AX_SLS_StockMutationRepository<VWI_AX_SLS_StockMutation, int> vWI_AX_SLS_StockMutationRepo)
        {
            _vWI_AX_SLS_StockMutationRepo = vWI_AX_SLS_StockMutationRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_AX_SLS_StockMutation by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_AX_SLS_StockMutationDto>> Read(VWI_AX_SLS_StockMutationFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_AX_SLS_StockMutationDto>> ReadList(VWI_AX_SLS_StockMutationFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_AX_SLS_StockMutationDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
				var criterias = Helper.InitialStrCriteria(typeof(VWI_AX_SLS_StockMutation), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
                        // filter by dealer company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_AX_SLS_StockMutation), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Company", dealerCompanyCode,false,criterias);
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

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_AX_SLS_StockMutation), filterDto);

                List<VWI_AX_SLS_StockMutation> data = _vWI_AX_SLS_StockMutationRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_AX_SLS_StockMutation, VWI_AX_SLS_StockMutationDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_AX_SLS_StockMutation), filterDto);
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
        /// Delete VWI_AX_SLS_StockMutation by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_StockMutationDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_AX_SLS_StockMutation
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_StockMutationDto> Create(VWI_AX_SLS_StockMutationParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_AX_SLS_StockMutation
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_StockMutationDto> Update(VWI_AX_SLS_StockMutationParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}