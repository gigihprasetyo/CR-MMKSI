	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_SLS_DailyActivityMonitoring_SPK business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/21/2019 1:53:33 PM
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
    public class VWI_CRM_SLS_DailyActivityMonitoring_SPKBL : AbstractBusinessLogic, IVWI_CRM_SLS_DailyActivityMonitoring_SPKBL
    {
        #region Variables
        private IVWI_CRM_SLS_DailyActivityMonitoring_SPKRepository<VWI_CRM_SLS_DailyActivityMonitoring_SPK, int> _vWI_CRM_SLS_DailyActivityMonitoring_SPKRepo;
        #endregion

        #region Constructor
        public VWI_CRM_SLS_DailyActivityMonitoring_SPKBL(IVWI_CRM_SLS_DailyActivityMonitoring_SPKRepository<VWI_CRM_SLS_DailyActivityMonitoring_SPK, int> vWI_CRM_SLS_DailyActivityMonitoring_SPKRepo)
        {
            _vWI_CRM_SLS_DailyActivityMonitoring_SPKRepo = vWI_CRM_SLS_DailyActivityMonitoring_SPKRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_SLS_DailyActivityMonitoring_SPK by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_SLS_DailyActivityMonitoring_SPKDto>> Read(VWI_CRM_SLS_DailyActivityMonitoring_SPKFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_SLS_DailyActivityMonitoring_SPKDto>> ReadList(VWI_CRM_SLS_DailyActivityMonitoring_SPKFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_SLS_DailyActivityMonitoring_SPKDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_SLS_DailyActivityMonitoring_SPK), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
						// filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_SLS_DailyActivityMonitoring_SPK), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", dealerCompanyCode,false,criterias);
                    }
                    else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to Dealer Configuration is not set");
                        return result;
                    }
                }else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                    return result;
                }

                sortColl = Helper.UpdateSortColumn(typeof(VWI_CRM_SLS_DailyActivityMonitoring_SPK), filterDto, sortColl);

                List<VWI_CRM_SLS_DailyActivityMonitoring_SPK> data = _vWI_CRM_SLS_DailyActivityMonitoring_SPKRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_SLS_DailyActivityMonitoring_SPK, VWI_CRM_SLS_DailyActivityMonitoring_SPKDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_SLS_DailyActivityMonitoring_SPK), filterDto);
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
        /// Delete VWI_CRM_SLS_DailyActivityMonitoring_SPK by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_SLS_DailyActivityMonitoring_SPKDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_SLS_DailyActivityMonitoring_SPKDto> Create(VWI_CRM_SLS_DailyActivityMonitoring_SPKParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_SLS_DailyActivityMonitoring_SPKDto> Update(VWI_CRM_SLS_DailyActivityMonitoring_SPKParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}