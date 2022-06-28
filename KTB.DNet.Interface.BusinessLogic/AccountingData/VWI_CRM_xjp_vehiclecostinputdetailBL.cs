#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_xjp_vehiclecostinputdetail business logic class
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
    public class VWI_CRM_xjp_vehiclecostinputdetailBL : AbstractBusinessLogic, IVWI_CRM_xjp_vehiclecostinputdetailBL
    {
        #region Variables
        private IVWI_CRM_xjp_vehiclecostinputdetailRepository<VWI_CRM_xjp_vehiclecostinputdetail, int> _vWI_CRM_xjp_vehiclecostinputdetailRepo;
        #endregion

        #region Constructor
        public VWI_CRM_xjp_vehiclecostinputdetailBL(IVWI_CRM_xjp_vehiclecostinputdetailRepository<VWI_CRM_xjp_vehiclecostinputdetail, int> vWI_CRM_xjp_vehiclecostinputdetailRepo)
        {
            _vWI_CRM_xjp_vehiclecostinputdetailRepo = vWI_CRM_xjp_vehiclecostinputdetailRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_xjp_vehiclecostinputdetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_xjp_vehiclecostinputdetailDto>> Read(VWI_CRM_xjp_vehiclecostinputdetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_xjp_vehiclecostinputdetailDto>> ReadList(VWI_CRM_xjp_vehiclecostinputdetailFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_xjp_vehiclecostinputdetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_xjp_vehiclecostinputdetail), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xjp_vehiclecostinputdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_xjp_vehiclecostinputdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_xjp_vehiclecostinputdetail), filterDto);

                        List<VWI_CRM_xjp_vehiclecostinputdetail> data = _vWI_CRM_xjp_vehiclecostinputdetailRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_xjp_vehiclecostinputdetail, VWI_CRM_xjp_vehiclecostinputdetailDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_xjp_vehiclecostinputdetail), filterDto);
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
        /// Delete VWI_CRM_xjp_vehiclecostinputdetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_xjp_vehiclecostinputdetailDto> Delete(int id)
        {
            return null;
        }

        public ResponseBase<VWI_CRM_xjp_vehiclecostinputdetailDto> Create(VWI_CRM_xjp_vehiclecostinputdetailDtoParameter objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<VWI_CRM_xjp_vehiclecostinputdetailDto> Update(VWI_CRM_xjp_vehiclecostinputdetailDtoParameter objUpdate)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}