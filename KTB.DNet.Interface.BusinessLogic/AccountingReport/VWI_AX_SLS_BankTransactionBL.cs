	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_AX_SLS_BankTransaction business logic class
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
    public class VWI_AX_SLS_BankTransactionBL : AbstractBusinessLogic, IVWI_AX_SLS_BankTransactionBL
    {
        #region Variables
        private IVWI_AX_SLS_BankTransactionRepository<VWI_AX_SLS_BankTransaction, int> _vWI_AX_SLS_BankTransactionRepo;
        #endregion

        #region Constructor
        public VWI_AX_SLS_BankTransactionBL(IVWI_AX_SLS_BankTransactionRepository<VWI_AX_SLS_BankTransaction, int> vWI_AX_SLS_BankTransactionRepo)
        {
            _vWI_AX_SLS_BankTransactionRepo = vWI_AX_SLS_BankTransactionRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_AX_SLS_BankTransaction by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_AX_SLS_BankTransactionDto>> Read(VWI_AX_SLS_BankTransactionFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_AX_SLS_BankTransactionDto>> ReadList(VWI_AX_SLS_BankTransactionFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_AX_SLS_BankTransactionDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_AX_SLS_BankTransaction), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {
                    // filter by dealer company
                    if(listDealer.Count > 0)
                    {
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_AX_SLS_BankTransaction), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Company", dealerCompanyCode,false,criterias);
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

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_AX_SLS_BankTransaction), filterDto);

                List<VWI_AX_SLS_BankTransaction> data = _vWI_AX_SLS_BankTransactionRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_AX_SLS_BankTransaction, VWI_AX_SLS_BankTransactionDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_AX_SLS_BankTransaction), filterDto);
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
        /// Delete VWI_AX_SLS_BankTransaction by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_BankTransactionDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_AX_SLS_BankTransaction
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_BankTransactionDto> Create(VWI_AX_SLS_BankTransactionParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_AX_SLS_BankTransaction
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_BankTransactionDto> Update(VWI_AX_SLS_BankTransactionParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}