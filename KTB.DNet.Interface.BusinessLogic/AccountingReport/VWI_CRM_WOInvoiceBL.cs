	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_WOInvoice business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2021 19:21:36
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
    public class VWI_CRM_WOInvoiceBL : AbstractBusinessLogic, IVWI_CRM_WOInvoiceBL
    {
        #region Variables
        private IVWI_CRM_WOInvoiceRepository<VWI_CRM_WOInvoice, int> _vWI_CRM_WOInvoiceRepo;
        private IVWI_CRM_WOInvoiceDetailRepository<VWI_CRM_WOInvoiceDetail, int> _vWI_CRM_WOInvoiceDetailRepo;
        #endregion

        #region Constructor
        public VWI_CRM_WOInvoiceBL(IVWI_CRM_WOInvoiceRepository<VWI_CRM_WOInvoice, int> vWI_CRM_WOInvoiceRepo, IVWI_CRM_WOInvoiceDetailRepository<VWI_CRM_WOInvoiceDetail,int> vWI_CRM_WOInvoiceDetailRepo)
        {
            _vWI_CRM_WOInvoiceRepo = vWI_CRM_WOInvoiceRepo;
            _vWI_CRM_WOInvoiceDetailRepo = vWI_CRM_WOInvoiceDetailRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_WOInvoice by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_WOInvoiceDto>> Read(VWI_CRM_WOInvoiceFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_WOInvoiceDto>> ReadList(VWI_CRM_WOInvoiceFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_WOInvoiceDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
				var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_WOInvoice), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
						// filter by dealer company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_WOInvoice), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);

                    }else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to Dealer Configuration is not set");
						return result; 
                    }
                }else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
					return result;
                }

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_WOInvoice), filterDto);

                List<VWI_CRM_WOInvoice> data = _vWI_CRM_WOInvoiceRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    // get WOInvoice Detail
                    foreach(var item in data)
                    {
                        List<VWI_CRM_WOInvoiceDetail> details = _vWI_CRM_WOInvoiceDetailRepo.SearchCustom("WHERE VWI_CRM_WOInvoiceDetail.xts_workorderid = '"+ item.xts_workorderid.ToString()  + "'");

                        if(details != null && details.Count > 0)
                        {
                            decimal jumlahDPP = 0;
                            decimal jumlahPPN = 0;
                            foreach (var getCount in details)
                            {
                                jumlahDPP += getCount.DPP;
                                jumlahPPN += (getCount.DPP * getCount.xts_rate) / 100;

                            }

                            item.JUMLAH_DPP = Math.Ceiling(jumlahDPP);
                            item.JUMLAH_PPN = Math.Floor(jumlahPPN);
                            item.WOInvoiceDetails = new List<VWI_CRM_WOInvoiceDetail>();
                            item.WOInvoiceDetails.AddRange(details);
                        }
                        
                    }

                    result.lst = data.ConvertList<VWI_CRM_WOInvoice, VWI_CRM_WOInvoiceDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_WOInvoice), filterDto);
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
        /// Delete VWI_CRM_WOInvoice by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_WOInvoiceDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_WOInvoice
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_WOInvoiceDto> Create(VWI_CRM_WOInvoiceParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_WOInvoice
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_WOInvoiceDto> Update(VWI_CRM_WOInvoiceParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}