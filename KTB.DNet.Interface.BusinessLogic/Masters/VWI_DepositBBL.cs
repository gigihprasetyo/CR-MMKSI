#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositBBL  Business Logic
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
    public class VWI_DepositBBL : AbstractBusinessLogic, IVWI_DepositBBL
    {
        #region Variables
        private IVWI_DepositBHeaderRepository<VWI_DepositBHeader_IF, int> _VWI_DepositBRepo;
        private IVWI_DepositBDetailRepository<VWI_DepositBDetail_IF, int> _VWI_DepositBSPMRepo;
        #endregion

        #region Constructor
        public VWI_DepositBBL(IVWI_DepositBHeaderRepository<VWI_DepositBHeader_IF, int> VWI_DepositBRepo, IVWI_DepositBDetailRepository<VWI_DepositBDetail_IF, int> VWI_DepositBSPMRepo)
        {
            _VWI_DepositBRepo = VWI_DepositBRepo;
            _VWI_DepositBSPMRepo = VWI_DepositBSPMRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_DepositB by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_DepositBHeaderDto>> Read(VWI_DepositBHeaderFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_DepositBHeaderDto>> ReadList(VWI_DepositBHeaderFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_DepositBHeaderDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_DepositBHeader_IF), filterDto);

                // filter by Dealer
                criterias = Helper.UpdateStrCriteria(typeof(VWI_DepositBHeader_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_DepositBHeader_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = criterias.Replace("VWI_DepositBHeader_IF", "VWI_DepositBHeader");

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_DepositBHeader_IF), filterDto);

                List<VWI_DepositBHeader_IF> data = _VWI_DepositBRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                var Header = data.ConvertList<VWI_DepositBHeader_IF, VWI_DepositBHeaderDto>();
                foreach (VWI_DepositBHeaderDto item in Header)
                {
                    VWI_DepositBHeaderFilterDto filterDto_ = new VWI_DepositBHeaderFilterDto();
                    var sortColl_ = string.Empty;
                    int totalRow_ = 0;
                    int filteredTotalRow_ = 0;
                    string rawSql_ = string.Empty;
                    var innerQueryCriteria_ = string.Empty;
                    var criterias_ = "";

                    // filter by Dealer
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_DepositBDetailDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias_);
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_DepositBDetailDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DepositBID", item.ID.ToString(), false, criterias_);
                    criterias_ = criterias_.Replace("VWI_DepositBDetailDto", "VWI_DepositBDetail");

                    sortColl_ = Helper.UpdateSortColumnDapper(typeof(VWI_DepositBDetailDto), filterDto_);

                    List<VWI_DepositBDetail_IF> dataDetail = _VWI_DepositBSPMRepo.Search(
                                        criterias_, innerQueryCriteria_, sortColl_, 1, pageSize, out filteredTotalRow_, out totalRow_);

                    item.DepositBDetail = dataDetail.ConvertList<VWI_DepositBDetail_IF, VWI_DepositBDetailDto>();
                }

                if (data != null && data.Count > 0)
                {
                    result.lst = Header;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_DepositBHeader_IF), filterDto);
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
        /// Delete VWI_DepositB by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositBHeaderDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_DepositB
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositBHeaderDto> Create(VWI_DepositBHeaderParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_DepositB
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositBHeaderDto> Update(VWI_DepositBHeaderParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}
