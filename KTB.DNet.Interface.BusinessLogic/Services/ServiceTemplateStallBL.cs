#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : Service Template business logic class
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
    public class ServiceTemplateStallBL : AbstractBusinessLogic, IServiceTemplateStallBL
    {
        #region Variables
        private IVWI_ServiceTemplateHeaderRepository<VWI_ServiceTemplateHeader, int> _serviceTemplateHeaderRepo;
        private IVWI_ServiceTemplateDetailRepository<VWI_ServiceTemplateDetail, int> _serviceTemplateDetailRepo;
        #endregion

        #region Constructor
        public ServiceTemplateStallBL(IVWI_ServiceTemplateHeaderRepository<VWI_ServiceTemplateHeader, int> serviceTemplateHeaderRepo, IVWI_ServiceTemplateDetailRepository<VWI_ServiceTemplateDetail, int> serviceTemplateDetailRepo)
        {
            _serviceTemplateHeaderRepo = serviceTemplateHeaderRepo;
            _serviceTemplateDetailRepo = serviceTemplateDetailRepo;
        }

        public ResponseBase<ServiceTemplateHeaderDto> Create(ServiceTemplateHeaderParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<ServiceTemplateHeaderDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<ServiceTemplateHeaderDto>> Read(ServiceTemplateHeaderFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        public ResponseBase<List<ServiceTemplateHeaderDto>> ReadList(ServiceTemplateHeaderFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<ServiceTemplateHeaderDto>>();
            var sortColl = string.Empty;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            int totalRow = 0;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(VWI_ServiceTemplateHeader), filterDto);
                //criterias = Helper.UpdateStrCriteria(typeof(ServiceTemplateHeader), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_ServiceTemplateHeader), filterDto);

                List<VWI_ServiceTemplateHeader> data = _serviceTemplateHeaderRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    // get Service Template Detail
                    foreach (var item in data)
                    {
                        List<VWI_ServiceTemplateDetail> details = _serviceTemplateDetailRepo.SearchCustom("WHERE VWI_ServiceTemplateDetail.ServiceTemplateHeaderID = '" + item.ID.ToString() + "'");

                        if (details != null && details.Count > 0)
                        {
                            item.servicetemplatedetails = new List<VWI_ServiceTemplateDetail>();
                            item.servicetemplatedetails.AddRange(details);
                        }

                    }

                    //get labor
                    foreach (var item in data)
                    {
                        List<VWI_ServiceTemplateDetail> details = _serviceTemplateDetailRepo.SearchCustom("WHERE VWI_ServiceTemplateDetail.ServiceTemplateHeaderID ='' AND VWI_ServiceTemplateDetail.KindID='" + item.KindID.ToString() + "' AND VWI_ServiceTemplateDetail.VechileTypeID='" + item.VechileTypeID.ToString() + "'");
                        VWI_ServiceTemplateDetail laborff = new VWI_ServiceTemplateDetail();
                        if (details != null && details.Count > 0)
                        {
                            //item.servicetemplatedetails = new List<VWI_ServiceTemplateDetail>();
                            item.servicetemplatedetails.AddRange(details);
                        }
                        if(item.KindID.Contains("FF"))
                        {
                            laborff.ServiceTemplateHeaderID = "";
                            laborff.VechileTypeID = item.VechileTypeID;
                            laborff.KindID = item.KindID;
                            laborff.ServiceTemplate = item.ServiceTemplate;
                            laborff.ServiceTemplateDetail = item.ServiceTemplate;
                            laborff.ProductType = "SERVICES";
                            laborff.Product = "LC";
                            laborff.ProductDescription = "";
                            laborff.PartCode = "";
                            laborff.PartCodeDescription = "";
                            laborff.Quantity = 1;
                            laborff.UnitPrice = "";
                            laborff.TotalPrice = 0;
                            item.servicetemplatedetails.Add(laborff);
                        }

                    }

                    result.lst = data.ConvertList<VWI_ServiceTemplateHeader, ServiceTemplateHeaderDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceTemplateHeader), filterDto);
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

        public ResponseBase<ServiceTemplateHeaderDto> Update(ServiceTemplateHeaderParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}