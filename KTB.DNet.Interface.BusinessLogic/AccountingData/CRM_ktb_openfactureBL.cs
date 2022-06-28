	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : CRM_ktb_openfacture business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 17/02/2021 11:49:03
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
    public class CRM_ktb_openfactureBL : AbstractBusinessLogic, ICRM_ktb_openfactureBL
    {
        #region Variables
        private ICRM_ktb_openfactureRepository<VWI_CRM_ktb_openfacture, int> _cRM_ktb_openfactureRepo;
        #endregion

        #region Constructor
        public CRM_ktb_openfactureBL(ICRM_ktb_openfactureRepository<VWI_CRM_ktb_openfacture, int> cRM_ktb_openfactureRepo)
        {
            _cRM_ktb_openfactureRepo = cRM_ktb_openfactureRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_ktb_openfacture by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_openfactureDto>> Read(VWI_CRM_ktb_openfactureFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_ktb_openfactureDto>> ReadList(VWI_CRM_ktb_openfactureFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_ktb_openfactureDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
				var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_openfacture), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
                        // filter by dealer company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_openfacture), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_openfacture), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);

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

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_openfacture), filterDto);

                List<VWI_CRM_ktb_openfacture> data = _cRM_ktb_openfactureRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_ktb_openfacture, VWI_CRM_ktb_openfactureDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_openfacture), filterDto);
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
        /// Delete CRM_ktb_openfacture by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_openfactureDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new CRM_ktb_openfacture
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_openfactureDto> Create(CRM_ktb_openfactureParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update CRM_ktb_openfacture
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_openfactureDto> Update(CRM_ktb_openfactureParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}