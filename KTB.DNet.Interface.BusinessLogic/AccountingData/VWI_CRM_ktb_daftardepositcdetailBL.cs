#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_ktb_daftardepositcdetail business logic class
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
    public class VWI_CRM_ktb_daftardepositcdetailBL : AbstractBusinessLogic, IVWI_CRM_ktb_daftardepositcdetailBL
    {
        #region Variables
        private IVWI_CRM_ktb_daftardepositcdetailRepository<VWI_CRM_ktb_daftardepositcdetail, int> _vWI_CRM_ktb_daftardepositcdetailRepo;
        #endregion

        #region Constructor
        public VWI_CRM_ktb_daftardepositcdetailBL(IVWI_CRM_ktb_daftardepositcdetailRepository<VWI_CRM_ktb_daftardepositcdetail, int> vWI_CRM_ktb_daftardepositcdetailRepo)
        {
            _vWI_CRM_ktb_daftardepositcdetailRepo = vWI_CRM_ktb_daftardepositcdetailRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_ktb_daftardepositcdetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_daftardepositcdetailDto>> Read(VWI_CRM_ktb_daftardepositcdetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_ktb_daftardepositcdetailDto>> ReadList(VWI_CRM_ktb_daftardepositcdetailFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_ktb_daftardepositcdetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_daftardepositcdetail), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_daftardepositcdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_daftardepositcdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_daftardepositcdetail), filterDto);

                        List<VWI_CRM_ktb_daftardepositcdetail> data = _vWI_CRM_ktb_daftardepositcdetailRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_ktb_daftardepositcdetail, VWI_CRM_ktb_daftardepositcdetailDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_daftardepositcdetail), filterDto);
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
        /// Delete VWI_CRM_ktb_daftardepositcdetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_daftardepositcdetailDto> Delete(int id)
        {
            return null;
        }

        public ResponseBase<VWI_CRM_ktb_daftardepositcdetailDto> Create(VWI_CRM_ktb_daftardepositcdetailDtoParameter objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<VWI_CRM_ktb_daftardepositcdetailDto> Update(VWI_CRM_ktb_daftardepositcdetailDtoParameter objUpdate)
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}