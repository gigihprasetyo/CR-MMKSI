	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSalesReturn business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2019 5:45:18 PM
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
    public class VWI_CRM_PRT_SparepartSalesReturnBL : AbstractBusinessLogic, IVWI_CRM_PRT_SparepartSalesReturnBL
    {
        #region Variables
        private IVWI_CRM_PRT_SparepartSalesReturnRepository<VWI_CRM_PRT_SparepartSalesReturn, int> _vWI_CRM_PRT_SparepartSalesReturnRepo;
        #endregion

        #region Constructor
        public VWI_CRM_PRT_SparepartSalesReturnBL(IVWI_CRM_PRT_SparepartSalesReturnRepository<VWI_CRM_PRT_SparepartSalesReturn, int> vWI_CRM_PRT_SparepartSalesReturnRepo)
        {
            _vWI_CRM_PRT_SparepartSalesReturnRepo = vWI_CRM_PRT_SparepartSalesReturnRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_PRT_SparepartSalesReturn by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_PRT_SparepartSalesReturnDto>> Read(VWI_CRM_PRT_SparepartSalesReturnFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_PRT_SparepartSalesReturnDto>> ReadList(VWI_CRM_PRT_SparepartSalesReturnFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_PRT_SparepartSalesReturnDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_PRT_SparepartSalesReturn), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
						// filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_PRT_SparepartSalesReturn), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", dealerCompanyCode,false,criterias);
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

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_PRT_SparepartSalesReturn), filterDto);

                List<VWI_CRM_PRT_SparepartSalesReturn> data = _vWI_CRM_PRT_SparepartSalesReturnRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_PRT_SparepartSalesReturn, VWI_CRM_PRT_SparepartSalesReturnDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_PRT_SparepartSalesReturn), filterDto);
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
        /// Delete VWI_CRM_PRT_SparepartSalesReturn by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_SparepartSalesReturnDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_PRT_SparepartSalesReturn
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_SparepartSalesReturnDto> Create(VWI_CRM_PRT_SparepartSalesReturnParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_PRT_SparepartSalesReturn
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_SparepartSalesReturnDto> Update(VWI_CRM_PRT_SparepartSalesReturnParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}