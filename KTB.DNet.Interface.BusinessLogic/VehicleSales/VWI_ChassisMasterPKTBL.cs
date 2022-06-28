#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_ChassisMasterPKT business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-11 17:34:00
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData;
//using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
//using Microsoft.Practices.EnterpriseLibrary.Configuration;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_ChassisMasterPKTBL : AbstractBusinessLogic, IVWI_ChassisMasterPKTBL
    {
        #region Variables
        private IVWI_ChassisMasterPKTRepository<VWI_ChassisMasterPKT, int> _vWI_ChassisMasterPKTRepo;
        #endregion

        #region Constructor
        public VWI_ChassisMasterPKTBL(IVWI_ChassisMasterPKTRepository<VWI_ChassisMasterPKT, int> vWI_ChassisMasterPKTRepo)
        {
            _vWI_ChassisMasterPKTRepo = vWI_ChassisMasterPKTRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_ChassisMasterPKT by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<ChassisMasterPKTDto>> Read(ChassisMasterPKTFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<ChassisMasterPKTDto>> ReadList(ChassisMasterPKTFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode, string connectionString)
        {
            var result = new ResponseBase<List<ChassisMasterPKTDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_ChassisMasterPKT), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_ChassisMasterPKT), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_ChassisMasterPKT), filterDto);

                VWI_ChassisMasterPKTRepository _Repo = new VWI_ChassisMasterPKTRepository(connectionString);

                List<VWI_ChassisMasterPKT> data = _Repo.Search(
                                    criterias.ToString(), innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_ChassisMasterPKT, ChassisMasterPKTDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ChassisMasterPKT), filterDto);
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
        /// Delete VWI_ChassisMasterPKT by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterPKTDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_ChassisMasterPKT
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterPKTDto> Create(ChassisMasterPKTParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_ChassisMasterPKT
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMasterPKTDto> Update(ChassisMasterPKTParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}


