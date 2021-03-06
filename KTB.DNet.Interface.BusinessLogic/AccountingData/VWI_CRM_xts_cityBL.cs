#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xts_city business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/15/2020 09:32:00 AM
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
    public class VWI_CRM_xts_cityBL : AbstractBusinessLogic, IVWI_CRM_xts_cityBL
    {
        #region Variables
        private IVWI_CRM_xts_cityRepository<VWI_CRM_xts_city, int> _vWI_CRM_xts_cityRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xts_cityBL(IVWI_CRM_xts_cityRepository<VWI_CRM_xts_city, int> vWI_CRM_xts_cityRepo)
        {
            _vWI_CRM_xts_cityRepo = vWI_CRM_xts_cityRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xts_city by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xts_cityDto>> Read(VWI_CRM_xts_cityFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xts_cityDto>> ReadList(VWI_CRM_xts_cityFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xts_cityDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xts_city), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by Company
                        //criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_city), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xts_city), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xts_city), filterDto);

                        List<VWI_CRM_xts_city> data = _vWI_CRM_xts_cityRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_xts_city, VWI_CRM_xts_cityDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xts_city), filterDto);
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
        /// Delete VWI_CRM_xts_city by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_cityDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_xts_city
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_cityDto> Create(VWI_CRM_xts_cityParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_xts_city
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xts_cityDto> Update(VWI_CRM_xts_cityParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}
