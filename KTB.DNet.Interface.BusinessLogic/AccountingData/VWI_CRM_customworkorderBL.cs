#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_customworkorder business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2019 10:09:30 AM
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
    public class VWI_CRM_customworkorderBL : AbstractBusinessLogic, IVWI_CRM_customworkorderBL
    {
        #region Variables
        private IVWI_CRM_customworkorderRepository<VWI_CRM_customworkorder, int> _vWI_CRM_customworkorderRepo;
        #endregion

        #region Constructor
        public VWI_CRM_customworkorderBL(IVWI_CRM_customworkorderRepository<VWI_CRM_customworkorder, int> vWI_CRM_customworkorderRepo)
        {
            _vWI_CRM_customworkorderRepo = vWI_CRM_customworkorderRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_customworkorder by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_customworkorderDto>> Read(VWI_CRM_customworkorderFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_customworkorderDto>> ReadList(VWI_CRM_customworkorderFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_customworkorderDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_customworkorder), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_customworkorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_customworkorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_customworkorder), filterDto);

                        List<VWI_CRM_customworkorder> data = _vWI_CRM_customworkorderRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_customworkorder, VWI_CRM_customworkorderDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_customworkorder), filterDto);
                        }

                        result.success = true;
                    }
                    else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to Dealer Configuration is not set");
                    }
                }
                else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                }
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
        /// Delete VWI_CRM_customworkorder by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_customworkorderDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_customworkorder
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_customworkorderDto> Create(VWI_CRM_customworkorderParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_customworkorder
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_customworkorderDto> Update(VWI_CRM_customworkorderParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}