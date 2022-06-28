#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xid_registrationprogressstage business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 08:51:00
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
    public class VWI_CRM_xid_registrationprogressstageBL : AbstractBusinessLogic, IVWI_CRM_xid_registrationprogressstageBL
    {
        #region Variables
        private IVWI_CRM_xid_registrationprogressstageRepository<VWI_CRM_xid_registrationprogressstage, int> _vWI_CRM_xid_registrationprogressstageRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xid_registrationprogressstageBL(IVWI_CRM_xid_registrationprogressstageRepository<VWI_CRM_xid_registrationprogressstage, int> vWI_CRM_xid_registrationprogressstageRepo)
        {
            _vWI_CRM_xid_registrationprogressstageRepo = vWI_CRM_xid_registrationprogressstageRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xid_registrationprogressstage by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xid_registrationprogressstageDto>> Read(VWI_CRM_xid_registrationprogressstageFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xid_registrationprogressstageDto>> ReadList(VWI_CRM_xid_registrationprogressstageFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xid_registrationprogressstageDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xid_registrationprogressstage), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xid_registrationprogressstage), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xid_registrationprogressstage), filterDto);

                List<VWI_CRM_xid_registrationprogressstage> data = _vWI_CRM_xid_registrationprogressstageRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_xid_registrationprogressstage, VWI_CRM_xid_registrationprogressstageDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xid_registrationprogressstage), filterDto);
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
        /// Delete VWI_CRM_xid_registrationprogressstage by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xid_registrationprogressstageDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xid_registrationprogressstage
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xid_registrationprogressstageDto> Create(VWI_CRM_xid_registrationprogressstageParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xid_registrationprogressstage
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xid_registrationprogressstageDto> Update(VWI_CRM_xid_registrationprogressstageParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}
