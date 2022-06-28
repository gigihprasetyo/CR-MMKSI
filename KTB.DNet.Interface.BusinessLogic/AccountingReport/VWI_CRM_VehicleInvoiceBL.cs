	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_VehicleInvoice business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 02/03/2021 6:32:17
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
    public class VWI_CRM_VehicleInvoiceBL : AbstractBusinessLogic, IVWI_CRM_VehicleInvoiceBL
    {
        #region Variables
        private IVWI_CRM_VehicleInvoiceRepository<VWI_CRM_VehicleInvoice, int> _vWI_CRM_VehicleInvoiceRepo;
        private IVWI_CRM_VehicleInvoiceDetailRepository<VWI_CRM_VehicleInvoiceDetail, int> _vWI_CRM_VehicleInvoiceDetailRepo;
        #endregion

        #region Constructor
        public VWI_CRM_VehicleInvoiceBL(IVWI_CRM_VehicleInvoiceRepository<VWI_CRM_VehicleInvoice, int> vWI_CRM_VehicleInvoiceRepo, IVWI_CRM_VehicleInvoiceDetailRepository<VWI_CRM_VehicleInvoiceDetail, int> vWI_CRM_VehicleInvoiceDetailRepo)
        {
            _vWI_CRM_VehicleInvoiceRepo = vWI_CRM_VehicleInvoiceRepo;
            _vWI_CRM_VehicleInvoiceDetailRepo = vWI_CRM_VehicleInvoiceDetailRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_VehicleInvoice by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_VehicleInvoiceDto>> Read(VWI_CRM_VehicleInvoiceFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_VehicleInvoiceDto>> ReadList(VWI_CRM_VehicleInvoiceFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_VehicleInvoiceDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
				var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_VehicleInvoice), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
						// filter by dealer company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_VehicleInvoice), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode,false,criterias);
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

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_VehicleInvoice), filterDto);

                List<VWI_CRM_VehicleInvoice> data = _vWI_CRM_VehicleInvoiceRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    // get SOInvoice Detail
                    foreach (var item in data)
                    {
                        List<VWI_CRM_VehicleInvoiceDetail> details = _vWI_CRM_VehicleInvoiceDetailRepo.SearchCustom("WHERE VWI_CRM_VehicleInvoiceDetail.xts_accountreceivableinvoiceid = '" + item.xts_accountreceivableinvoiceid.ToString() + "'");

                        if (details != null && details.Count > 0)
                        {
                            decimal jumlahDPP = 0;
                            decimal jumlahPPN = 0;
                            foreach (var getCount in details)
                            {
                                jumlahDPP += getCount.DPP;
                                jumlahPPN += (getCount.DPP * getCount.Tax1) / 100;

                            }

                            item.JUMLAH_DPP = Math.Ceiling(jumlahDPP);
                            item.JUMLAH_PPN = Math.Floor(jumlahPPN);
                            item.VehicleInvoiceDetails = new List<VWI_CRM_VehicleInvoiceDetail>();
                            item.VehicleInvoiceDetails.AddRange(details);
                        }

                    }

                    result.lst = data.ConvertList<VWI_CRM_VehicleInvoice, VWI_CRM_VehicleInvoiceDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_VehicleInvoice), filterDto);
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
        /// Delete VWI_CRM_VehicleInvoice by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_VehicleInvoiceDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_VehicleInvoice
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_VehicleInvoiceDto> Create(VWI_CRM_VehicleInvoiceParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_VehicleInvoice
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_VehicleInvoiceDto> Update(VWI_CRM_VehicleInvoiceParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}