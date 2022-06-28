#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositABL  Business Logic
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 13 Sep 2021
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
    public class VWI_DepositABL : AbstractBusinessLogic, IVWI_DepositABL
    {
        #region Variables
        private IVWI_DepositARepository<VWI_DepositA_IF, int> _VWI_DepositARepo;
        private IVWI_DepositADetailRepository<VWI_DepositADetail_IF, int> _VWI_DepositASPMRepo;
        #endregion

        #region Constructor
        public VWI_DepositABL(IVWI_DepositARepository<VWI_DepositA_IF, int> VWI_DepositARepo, IVWI_DepositADetailRepository<VWI_DepositADetail_IF, int> VWI_DepositASPMRepo)
        {
            _VWI_DepositARepo = VWI_DepositARepo;
            _VWI_DepositASPMRepo = VWI_DepositASPMRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_DepositA by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_DepositADto>> Read(VWI_DepositAFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_DepositADto>> ReadList(VWI_DepositAFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_DepositADto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_DepositA_IF), filterDto);

                // filter by Dealer
                criterias = Helper.UpdateStrCriteria(typeof(VWI_DepositA_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_DepositA_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = criterias.Replace("VWI_DepositA_IF", "VWI_DepositA");

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_DepositA_IF), filterDto);

                List<VWI_DepositA_IF> data = _VWI_DepositARepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                var Header = data.ConvertList<VWI_DepositA_IF, VWI_DepositADto>();
                foreach (VWI_DepositADto item in Header)
                {
                    VWI_DepositAFilterDto filterDto_ = new VWI_DepositAFilterDto();
                    var sortColl_ = string.Empty;
                    int totalRow_ = 0;
                    int filteredTotalRow_ = 0;
                    string rawSql_ = string.Empty;
                    var innerQueryCriteria_ = string.Empty;
                    var criterias_ = "";

                    // filter by Dealer
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_DepositADetailDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias_);
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_DepositADetailDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DepositAID", item.ID.ToString(), false, criterias_);
                    criterias_ = criterias_.Replace("VWI_DepositADetailDto", "VWI_DepositADetail");

                    sortColl_ = Helper.UpdateSortColumnDapper(typeof(VWI_DepositADetailDto), filterDto_);

                    List<VWI_DepositADetail_IF> dataDetail = _VWI_DepositASPMRepo.Search(
                                        criterias_, innerQueryCriteria_, sortColl_, 1, pageSize, out filteredTotalRow_, out totalRow_);

                    item.DepositADetail = dataDetail.ConvertList<VWI_DepositADetail_IF, VWI_DepositADetailDto>();
                }

                if (data != null && data.Count > 0)
                {
                    result.lst = Header;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_DepositA_IF), filterDto);
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
        /// Delete VWI_DepositA by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositADto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_DepositA
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositADto> Create(VWI_DepositAParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_DepositA
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositADto> Update(VWI_DepositAParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}
