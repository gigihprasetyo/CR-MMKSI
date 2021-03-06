#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_ktb_servicetemplateproductnongenuine business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 16:09:00
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
    public class VWI_CRM_ktb_servicetemplateproductnongenuineBL : AbstractBusinessLogic, IVWI_CRM_ktb_servicetemplateproductnongenuineBL
    {
        #region Variables
        private IVWI_CRM_ktb_servicetemplateproductnongenuineRepository<VWI_CRM_ktb_servicetemplateproductnongenuine, int> _vWI_CRM_ktb_servicetemplateproductnongenuineRepo;
        #endregion

        #region Constructor
        public VWI_CRM_ktb_servicetemplateproductnongenuineBL(IVWI_CRM_ktb_servicetemplateproductnongenuineRepository<VWI_CRM_ktb_servicetemplateproductnongenuine, int> vWI_CRM_ktb_servicetemplateproductnongenuineRepo)
        {
            _vWI_CRM_ktb_servicetemplateproductnongenuineRepo = vWI_CRM_ktb_servicetemplateproductnongenuineRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_ktb_servicetemplateproductnongenuine by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_servicetemplateproductnongenuineDto>> Read(VWI_CRM_ktb_servicetemplateproductnongenuineFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_ktb_servicetemplateproductnongenuineDto>> ReadList(VWI_CRM_ktb_servicetemplateproductnongenuineFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_ktb_servicetemplateproductnongenuineDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_servicetemplateproductnongenuine), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by Company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_servicetemplateproductnongenuine), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_servicetemplateproductnongenuine), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_servicetemplateproductnongenuine), filterDto);

                        List<VWI_CRM_ktb_servicetemplateproductnongenuine> data = _vWI_CRM_ktb_servicetemplateproductnongenuineRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_ktb_servicetemplateproductnongenuine, VWI_CRM_ktb_servicetemplateproductnongenuineDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_servicetemplateproductnongenuine), filterDto);
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
        /// Delete VWI_CRM_ktb_servicetemplateproductnongenuine by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_servicetemplateproductnongenuineDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_ktb_servicetemplateproductnongenuine
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_servicetemplateproductnongenuineDto> Create(VWI_CRM_ktb_servicetemplateproductnongenuineParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_ktb_servicetemplateproductnongenuine
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_servicetemplateproductnongenuineDto> Update(VWI_CRM_ktb_servicetemplateproductnongenuineParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}