#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_ktb_daerahlogistic business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 12:01:00
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
    public class VWI_CRM_ktb_daerahlogisticBL : AbstractBusinessLogic, IVWI_CRM_ktb_daerahlogisticBL
    {
        #region Variables
        private IVWI_CRM_ktb_daerahlogisticRepository<VWI_CRM_ktb_daerahlogistic, int> _vWI_CRM_ktb_daerahlogisticRepo;
        #endregion

        #region Constructor
        public VWI_CRM_ktb_daerahlogisticBL(IVWI_CRM_ktb_daerahlogisticRepository<VWI_CRM_ktb_daerahlogistic, int> vWI_CRM_ktb_daerahlogisticRepo)
        {
            _vWI_CRM_ktb_daerahlogisticRepo = vWI_CRM_ktb_daerahlogisticRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_ktb_daerahlogistic by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_daerahlogisticDto>> Read(VWI_CRM_ktb_daerahlogisticFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_ktb_daerahlogisticDto>> ReadList(VWI_CRM_ktb_daerahlogisticFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_ktb_daerahlogisticDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_daerahlogistic), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_daerahlogistic), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_daerahlogistic), filterDto);

                List<VWI_CRM_ktb_daerahlogistic> data = _vWI_CRM_ktb_daerahlogisticRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_ktb_daerahlogistic, VWI_CRM_ktb_daerahlogisticDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_daerahlogistic), filterDto);
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
        /// Delete VWI_CRM_ktb_daerahlogistic by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_daerahlogisticDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_ktb_daerahlogistic
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_daerahlogisticDto> Create(VWI_CRM_ktb_daerahlogisticParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_ktb_daerahlogistic
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_daerahlogisticDto> Update(VWI_CRM_ktb_daerahlogisticParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}
