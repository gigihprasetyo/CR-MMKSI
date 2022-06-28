#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositBL  Business Logic
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 14 Sep 2021
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
using KTB.DNet.Interface.Repository.Dapper.DNet;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_DepositBL : AbstractBusinessLogic, IVWI_DepositBL
    {
        #region Variables
        private IVWI_DepositRepository<VWI_Deposit_IF, int> _VWI_DepositRepo;
        private IVWI_DepositLineRepository<VWI_DepositLine_IF, int> _VWI_DepositSPMRepo;
        #endregion

        #region Constructor
        public VWI_DepositBL(IVWI_DepositRepository<VWI_Deposit_IF, int> VWI_DepositRepo, IVWI_DepositLineRepository<VWI_DepositLine_IF, int> VWI_DepositSPMRepo)
        {
            _VWI_DepositRepo = VWI_DepositRepo;
            _VWI_DepositSPMRepo = VWI_DepositSPMRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_Deposit by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_DepositDto>> Read(VWI_DepositFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_DepositDto>> ReadList(VWI_DepositFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_DepositDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_Deposit_IF), filterDto);

                // filter by Dealer
                criterias = Helper.UpdateStrCriteria(typeof(VWI_Deposit_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_Deposit_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = criterias.Replace("VWI_Deposit_IF", "VWI_Deposit");

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_Deposit_IF), filterDto);

                List<VWI_Deposit_IF> data = _VWI_DepositRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                var Header = data.ConvertList<VWI_Deposit_IF, VWI_DepositDto>();
                foreach (VWI_DepositDto item in Header)
                {
                    VWI_DepositFilterDto filterDto_ = new VWI_DepositFilterDto();
                    var sortColl_ = string.Empty;
                    int totalRow_ = 0;
                    int filteredTotalRow_ = 0;
                    string rawSql_ = string.Empty;
                    var innerQueryCriteria_ = string.Empty;
                    var criterias_ = "";

                    // filter by Dealer
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_DepositLineDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias_);
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_DepositLineDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DepositID", item.ID.ToString(), false, criterias_);
                    criterias_ = criterias_.Replace("VWI_DepositLineDto", "VWI_DepositLine");

                    sortColl_ = Helper.UpdateSortColumnDapper(typeof(VWI_DepositLineDto), filterDto_);

                    List<VWI_DepositLine_IF> dataDetail = _VWI_DepositSPMRepo.Search(
                                        criterias_, innerQueryCriteria_, sortColl_, 1, pageSize, out filteredTotalRow_, out totalRow_);

                    item.DepositLine = dataDetail.ConvertList<VWI_DepositLine_IF, VWI_DepositLineDto>();
                }

                if (data != null && data.Count > 0)
                {
                    result.lst = Header;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_Deposit_IF), filterDto);
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
        /// Delete VWI_Deposit by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_Deposit
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositDto> Create(VWI_DepositParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_Deposit
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositDto> Update(VWI_DepositParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}
