#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : VWI_CRM_ktb_externaldealerinterfacelog class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_QA 
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 29 Apr 2021 09:23:25
 ===========================================================================
*/
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
using AutoMapper;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_CRM_ktb_externaldealerinterfacelogBL : AbstractBusinessLogic, IVWI_CRM_ktb_externaldealerinterfacelogBL
    {
        #region Variables
        private IVWI_CRM_ktb_externaldealerinterfacelogRepository<VWI_CRM_ktb_externaldealerinterfacelog, int> _VWI_CRM_ktb_externaldealerinterfacelogRepo;
        #endregion

        #region Constructor
        public VWI_CRM_ktb_externaldealerinterfacelogBL(IVWI_CRM_ktb_externaldealerinterfacelogRepository<VWI_CRM_ktb_externaldealerinterfacelog, int> VWI_CRM_ktb_externaldealerinterfacelogRepo)
        {
            _VWI_CRM_ktb_externaldealerinterfacelogRepo = VWI_CRM_ktb_externaldealerinterfacelogRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_ktb_externaldealerinterfacelog by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_externaldealerinterfacelogDto>> Read(VWI_CRM_ktb_externaldealerinterfacelogFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_ktb_externaldealerinterfacelogDto>> ReadList(VWI_CRM_ktb_externaldealerinterfacelogFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {

            var result = new ResponseBase<List<VWI_CRM_ktb_externaldealerinterfacelogDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_externaldealerinterfacelog), filterDto);


                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {

                        // filter by Company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_externaldealerinterfacelog), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_externaldealerinterfacelog), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);
                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_externaldealerinterfacelog), filterDto, "VWI_CRM_ktb_externaldealerinterfacelog");

                        List<VWI_CRM_ktb_externaldealerinterfacelog> data = _VWI_CRM_ktb_externaldealerinterfacelogRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            VWI_CRM_ktb_externaldealerinterfacelog x = new VWI_CRM_ktb_externaldealerinterfacelog();
                            result.lst = data.ConvertList<VWI_CRM_ktb_externaldealerinterfacelog, VWI_CRM_ktb_externaldealerinterfacelogDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_externaldealerinterfacelog), filterDto);
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
        /// Create a new VWI_CRM_ktb_externaldealerinterfacelog
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_externaldealerinterfacelogDto> Create(VWI_CRM_ktb_externaldealerinterfacelogParameterDto objCreate)
        {
            return null;
        }



        /// <summary>
        /// Update VWI_CRM_ktb_externaldealerinterfacelog
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_externaldealerinterfacelogDto> Update(VWI_CRM_ktb_externaldealerinterfacelogParameterDto paramUpdate)
        {
            return null;
        }


        /// <summary>
        /// Delete VWI_CRM_ktb_externaldealerinterfacelog by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_externaldealerinterfacelogDto> Delete(int ID)
        {
            return null;
        }

        #endregion


    }
}
