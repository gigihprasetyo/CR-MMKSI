#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_ktb_vendorlogistic business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 13:32:00
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
    public class VWI_CRM_ktb_vendorlogisticBL : AbstractBusinessLogic, IVWI_CRM_ktb_vendorlogisticBL
    {
        #region Variables
        private IVWI_CRM_ktb_vendorlogisticRepository<VWI_CRM_ktb_vendorlogistic, int> _vWI_CRM_ktb_vendorlogisticRepo;
        #endregion

        #region Constructor
        public VWI_CRM_ktb_vendorlogisticBL(IVWI_CRM_ktb_vendorlogisticRepository<VWI_CRM_ktb_vendorlogistic, int> vWI_CRM_ktb_vendorlogisticRepo)
        {
            _vWI_CRM_ktb_vendorlogisticRepo = vWI_CRM_ktb_vendorlogisticRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_ktb_vendorlogistic by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_vendorlogisticDto>> Read(VWI_CRM_ktb_vendorlogisticFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_ktb_vendorlogisticDto>> ReadList(VWI_CRM_ktb_vendorlogisticFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_ktb_vendorlogisticDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_vendorlogistic), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_vendorlogistic), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_vendorlogistic), filterDto);

                List<VWI_CRM_ktb_vendorlogistic> data = _vWI_CRM_ktb_vendorlogisticRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_ktb_vendorlogistic, VWI_CRM_ktb_vendorlogisticDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_vendorlogistic), filterDto);
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
        /// Delete VWI_CRM_ktb_vendorlogistic by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_vendorlogisticDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_ktb_vendorlogistic
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_vendorlogisticDto> Create(VWI_CRM_ktb_vendorlogisticParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_ktb_vendorlogistic
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_vendorlogisticDto> Update(VWI_CRM_ktb_vendorlogisticParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}
