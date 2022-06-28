	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSalesToPartshop business logic class
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
    public class VWI_CRM_PRT_SparepartSalesToPartshopBL : AbstractBusinessLogic, IVWI_CRM_PRT_SparepartSalesToPartshopBL
    {
        #region Variables
        private IVWI_CRM_PRT_SparepartSalesToPartshopRepository<VWI_CRM_PRT_SparepartSalesToPartshop, int> _vWI_CRM_PRT_SparepartSalesToPartshopRepo;
        #endregion

        #region Constructor
        public VWI_CRM_PRT_SparepartSalesToPartshopBL(IVWI_CRM_PRT_SparepartSalesToPartshopRepository<VWI_CRM_PRT_SparepartSalesToPartshop, int> vWI_CRM_PRT_SparepartSalesToPartshopRepo)
        {
            _vWI_CRM_PRT_SparepartSalesToPartshopRepo = vWI_CRM_PRT_SparepartSalesToPartshopRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_PRT_SparepartSalesToPartshop by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_PRT_SparepartSalesToPartshopDto>> Read(VWI_CRM_PRT_SparepartSalesToPartshopFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_PRT_SparepartSalesToPartshopDto>> ReadList(VWI_CRM_PRT_SparepartSalesToPartshopFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_PRT_SparepartSalesToPartshopDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_PRT_SparepartSalesToPartshop), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
                        // filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_PRT_SparepartSalesToPartshop), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode,false,criterias);
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

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_PRT_SparepartSalesToPartshop), filterDto);

                List<VWI_CRM_PRT_SparepartSalesToPartshop> data = _vWI_CRM_PRT_SparepartSalesToPartshopRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_PRT_SparepartSalesToPartshop, VWI_CRM_PRT_SparepartSalesToPartshopDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_PRT_SparepartSalesToPartshop), filterDto);
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
        /// Delete VWI_CRM_PRT_SparepartSalesToPartshop by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_SparepartSalesToPartshopDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_PRT_SparepartSalesToPartshop
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_SparepartSalesToPartshopDto> Create(VWI_CRM_PRT_SparepartSalesToPartshopParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_PRT_SparepartSalesToPartshop
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_PRT_SparepartSalesToPartshopDto> Update(VWI_CRM_PRT_SparepartSalesToPartshopParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}