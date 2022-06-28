#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_DepositC2BL  Business Logic
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
    public class VWI_DepositC2BL : AbstractBusinessLogic, IVWI_DepositC2BL
    {
        #region Variables
        private IVWI_DepositC2Repository<VWI_DepositC2_IF, int> _VWI_DepositC2Repo;
        private IVWI_DepositC2LineRepository<VWI_DepositC2Line_IF, int> _VWI_DepositC2SPMRepo;
        #endregion

        #region Constructor
        public VWI_DepositC2BL(IVWI_DepositC2Repository<VWI_DepositC2_IF, int> VWI_DepositC2Repo, IVWI_DepositC2LineRepository<VWI_DepositC2Line_IF, int> VWI_DepositC2SPMRepo)
        {
            _VWI_DepositC2Repo = VWI_DepositC2Repo;
            _VWI_DepositC2SPMRepo = VWI_DepositC2SPMRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_DepositC2 by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_DepositC2Dto>> Read(VWI_DepositC2FilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_DepositC2Dto>> ReadList(VWI_DepositC2FilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_DepositC2Dto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_DepositC2_IF), filterDto);

                // filter by Dealer
                criterias = Helper.UpdateStrCriteria(typeof(VWI_DepositC2_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_DepositC2_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = criterias.Replace("VWI_DepositC2_IF", "VWI_DepositC2");

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_DepositC2_IF), filterDto);

                List<VWI_DepositC2_IF> data = _VWI_DepositC2Repo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                var Header = data.ConvertList<VWI_DepositC2_IF, VWI_DepositC2Dto>();
                foreach (VWI_DepositC2Dto item in Header)
                {
                    VWI_DepositC2FilterDto filterDto_ = new VWI_DepositC2FilterDto();
                    var sortColl_ = string.Empty;
                    int totalRow_ = 0;
                    int filteredTotalRow_ = 0;
                    string rawSql_ = string.Empty;
                    var innerQueryCriteria_ = string.Empty;
                    var criterias_ = "";

                    // filter by Dealer
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_DepositC2LineDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias_);
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_DepositC2LineDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DepositC2ID", item.ID.ToString(), false, criterias_);
                    criterias_ = criterias_.Replace("VWI_DepositC2LineDto", "VWI_DepositC2Line");

                    sortColl_ = Helper.UpdateSortColumnDapper(typeof(VWI_DepositC2LineDto), filterDto_);

                    List<VWI_DepositC2Line_IF> dataDetail = _VWI_DepositC2SPMRepo.Search(
                                        criterias_, innerQueryCriteria_, sortColl_, 1, pageSize, out filteredTotalRow_, out totalRow_);

                    item.DepositC2Line = dataDetail.ConvertList<VWI_DepositC2Line_IF, VWI_DepositC2LineDto>();
                }

                if (data != null && data.Count > 0)
                {
                    result.lst = Header;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_DepositC2_IF), filterDto);
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
        /// Delete VWI_DepositC2 by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositC2Dto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_DepositC2
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositC2Dto> Create(VWI_DepositC2ParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_DepositC2
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_DepositC2Dto> Update(VWI_DepositC2ParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}
