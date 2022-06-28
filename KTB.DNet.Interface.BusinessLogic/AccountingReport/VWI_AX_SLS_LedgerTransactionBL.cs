	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_AX_SLS_LedgerTransaction business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/9/2019 1:53:51 PM
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
    public class VWI_AX_SLS_LedgerTransactionBL : AbstractBusinessLogic, IVWI_AX_SLS_LedgerTransactionBL
    {
        #region Variables
        private IVWI_AX_SLS_LedgerTransactionRepository<VWI_AX_SLS_LedgerTransaction, int> _vWI_AX_SLS_LedgerTransactionRepo;
        #endregion

        #region Constructor
        public VWI_AX_SLS_LedgerTransactionBL(IVWI_AX_SLS_LedgerTransactionRepository<VWI_AX_SLS_LedgerTransaction, int> vWI_AX_SLS_LedgerTransactionRepo)
        {
            _vWI_AX_SLS_LedgerTransactionRepo = vWI_AX_SLS_LedgerTransactionRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_AX_SLS_LedgerTransaction by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_AX_SLS_LedgerTransactionDto>> Read(VWI_AX_SLS_LedgerTransactionFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_AX_SLS_LedgerTransactionDto>> ReadList(VWI_AX_SLS_LedgerTransactionFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_AX_SLS_LedgerTransactionDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_AX_SLS_LedgerTransaction),filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {
                    // filter by dealer company
                    if(listDealer.Count > 0)
                    {
                        innerQueryCriteria = Helper.UpdateStrCriteria(typeof(VWI_AX_SLS_LedgerTransaction), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Company", dealerCompanyCode,false, innerQueryCriteria);
                    }else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to Dealer Configuration is not set");
                        return result;
                    }
                }else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                    return result;
                }

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_AX_SLS_LedgerTransaction), filterDto);

                List<VWI_AX_SLS_LedgerTransaction> data = _vWI_AX_SLS_LedgerTransactionRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_AX_SLS_LedgerTransaction, VWI_AX_SLS_LedgerTransactionDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_AX_SLS_LedgerTransaction), filterDto);
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
        /// Delete VWI_AX_SLS_LedgerTransaction by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_LedgerTransactionDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_AX_SLS_LedgerTransaction
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_LedgerTransactionDto> Create(VWI_AX_SLS_LedgerTransactionParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_AX_SLS_LedgerTransaction
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_LedgerTransactionDto> Update(VWI_AX_SLS_LedgerTransactionParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}