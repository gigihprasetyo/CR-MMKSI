#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_ktb_saleschannel business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2019 08:26:00
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
    public class VWI_CRM_ktb_saleschannelBL : AbstractBusinessLogic, IVWI_CRM_ktb_saleschannelBL
    {
        #region Variables
        private IVWI_CRM_ktb_saleschannelRepository<VWI_CRM_ktb_saleschannel, int> _vWI_CRM_ktb_saleschannelRepo;
        #endregion

        #region Constructor
        public VWI_CRM_ktb_saleschannelBL(IVWI_CRM_ktb_saleschannelRepository<VWI_CRM_ktb_saleschannel, int> vWI_CRM_ktb_saleschannelRepo)
        {
            _vWI_CRM_ktb_saleschannelRepo = vWI_CRM_ktb_saleschannelRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_ktb_saleschannel by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_saleschannelDto>> Read(VWI_CRM_ktb_saleschannelFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_ktb_saleschannelDto>> ReadList(VWI_CRM_ktb_saleschannelFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_ktb_saleschannelDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_saleschannel), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_saleschannel), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                        // general master data
                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_saleschannel), filterDto);

                        List<VWI_CRM_ktb_saleschannel> data = _vWI_CRM_ktb_saleschannelRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_ktb_saleschannel, VWI_CRM_ktb_saleschannelDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_saleschannel), filterDto);
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
        /// Delete VWI_CRM_ktb_saleschannel by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_saleschannelDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_ktb_saleschannel
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_saleschannelDto> Create(VWI_CRM_ktb_saleschannelParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_ktb_saleschannel
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_saleschannelDto> Update(VWI_CRM_ktb_saleschannelParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}