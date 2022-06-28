#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_PQRBL  Business Logic
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021/06/29
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
    public class VWI_PQRBL : AbstractBusinessLogic, IVWI_PQRBL
    {
        #region Variables
        private IVWI_PQRRepository<VWI_PQR, int> _VWI_PQRRepo;
        private IVWI_PQRSparePartMasterRepository<VWI_PQRSparePartMaster, int> _VWI_PQRSPMRepo;
        #endregion

        #region Constructor
        public VWI_PQRBL(IVWI_PQRRepository<VWI_PQR, int> VWI_PQRRepo, IVWI_PQRSparePartMasterRepository<VWI_PQRSparePartMaster, int> VWI_PQRSPMRepo)
        {
            _VWI_PQRRepo = VWI_PQRRepo;
            _VWI_PQRSPMRepo = VWI_PQRSPMRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_PQR by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_PQRDto>> Read(VWI_PQRFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_PQRDto>> ReadList(VWI_PQRFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_PQRDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_PQR), filterDto);

                // filter by Dealer
                criterias = Helper.UpdateStrCriteria(typeof(VWI_PQR), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "DealerCode", DealerCode, false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_PQR), filterDto);

                List<VWI_PQR> data = _VWI_PQRRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                foreach (VWI_PQR item in data)
                {
                    VWI_PQRFilterDto filterDto_ = new VWI_PQRFilterDto();
                    var sortColl_ = string.Empty;
                    int totalRow_ = 0;
                    int filteredTotalRow_ = 0;
                    string rawSql_ = string.Empty;
                    var innerQueryCriteria_ = string.Empty;
                    var criterias_ = "";

                    // filter by Dealer
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_PQRSparePartMaster), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias_);
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_PQRSparePartMaster), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "PQRNo", item.PQRNo, false, criterias_);
                    criterias_ = Helper.UpdateStrCriteria(typeof(VWI_PQRSparePartMaster), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ChassisNumber", item.ChassisNumber, false, criterias_);

                    sortColl_ = Helper.UpdateSortColumnDapper(typeof(VWI_PQRSparePartMaster), filterDto_);

                    List<VWI_PQRSparePartMaster> dataSPM = _VWI_PQRSPMRepo.Search(
                                        criterias_, innerQueryCriteria_, sortColl_, 1, pageSize, out filteredTotalRow_, out totalRow_);

                    item.SparePartMaster = dataSPM;
                }

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_PQR, VWI_PQRDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_PQR), filterDto);
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
        /// Delete VWI_PQR by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_PQRDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_PQR
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_PQRDto> Create(VWI_PQRParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_PQR
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_PQRDto> Update(VWI_PQRParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}
