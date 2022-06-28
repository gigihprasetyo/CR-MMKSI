	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : AX_TSTransStockMutations business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/03/2022 9:17:19
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
    public class AX_TSTransStockMutationsBL : AbstractBusinessLogic, IAX_TSTransStockMutationsBL
    {
        #region Variables
        private IAX_TSTransStockMutationsRepository<AX_TSTransStockMutations, int> _aX_TSTransStockMutationsRepo;
        #endregion

        #region Constructor
        public AX_TSTransStockMutationsBL(IAX_TSTransStockMutationsRepository<AX_TSTransStockMutations, int> aX_TSTransStockMutationsRepo)
        {
            _aX_TSTransStockMutationsRepo = aX_TSTransStockMutationsRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get AX_TSTransStockMutations by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<AX_TSTransStockMutationsDto>> Read(AX_TSTransStockMutationsFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<AX_TSTransStockMutationsDto>> ReadList(AX_TSTransStockMutationsFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<AX_TSTransStockMutationsDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
				var criterias = Helper.InitialStrCriteria(typeof(AX_TSTransStockMutations), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
                        // filter by dealer company
                        criterias = Helper.UpdateStrCriteria(typeof(AX_TSTransStockMutations), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "CompanyCode", dealerCompanyCode, false, criterias);
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

                sortColl = Helper.UpdateSortColumnDapper(typeof(AX_TSTransStockMutations), filterDto);

                List<AX_TSTransStockMutations> data = _aX_TSTransStockMutationsRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<AX_TSTransStockMutations, AX_TSTransStockMutationsDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(AX_TSTransStockMutations), filterDto);
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
        /// Delete AX_TSTransStockMutations by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<AX_TSTransStockMutationsDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new AX_TSTransStockMutations
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<AX_TSTransStockMutationsDto> Create(AX_TSTransStockMutationsParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update AX_TSTransStockMutations
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<AX_TSTransStockMutationsDto> Update(AX_TSTransStockMutationsParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}