#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APISAPCustomerExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using Swashbuckle.Examples;
using System;

namespace KTB.DNet.Interface.WebApi.Models
{
    #region API Create SAPCustomer Example Data
    public class APICreateLeadExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                CustomerName = "Kenshi Himura",
                CustomerAddress = "Jl Pulomas Kav 122 Jakarta Pusat",
                CustomerPurpose = 1,
                CountryCode = "62",
                Phone = "82132324345",
                AgeSegment = 1,
                Sex = 1,
                VehicleTypeCode = "VC01",
                Qty = 1,
                InformationType = 1,
                InformationSource = 1,
                StateCode = 0,
                SalesmanHeaderCode = "S-100005",
                CampaignCode = "Test Campaign Code",
                Status = 3,
                ProspectDate = DateTime.Now,
                DealerCode = "100109",
                BusinessSectorDetailID = 1,
                CustomerCode = "",
                CustomerType = 1,
                Email = "expander@pulomas.com",
                CurrVehicleBrand = "Mitsubishi",
                CurrVehicleType = "L300",
                Note = "Customer Note",
                WebID = "Testing",
                BirthDate = DateTime.Now,
                LeadStatus = 1,
                EstimatedCloseDate = DateTime.Now,
                StatusCode = 1
            };

            return obj;
        }
    }
    #endregion

    #region Update SAPCustomer Example
    public class APIUpdateLeadExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 373937,
                CustomerName = "Kenshi Himura",
                CustomerAddress = "Jl Pulomas Kav 122 Jakarta Pusat",
                CustomerPurpose = 1,
                CountryCode = "62",
                Phone = "82132324343",
                AgeSegment = 1,
                Sex = 1,
                VehicleTypeCode = "VC01",
                Qty = 1,
                InformationType = 1,
                InformationSource = 1,
                StateCode = 0,
                SalesmanHeaderCode = "S-100002",
                CampaignCode = "Test Campaign Code",
                Status = 3,
                ProspectDate = DateTime.Now,
                DealerCode = "100109",
                BusinessSectorDetailID = 1,
                CustomerCode = "",
                CustomerType = 1,
                Email = "expander@pulomas.com",
                CurrVehicleBrand = "Mitsubishi",
                CurrVehicleType = "L300",
                Note = "Customer Note",
                WebID = "Testing",
                BirthDate = DateTime.Now,
                Description = string.Empty,
                OriginatingLeadId = Guid.NewGuid(),
                LeadStatus = 1,
                EstimatedCloseDate = DateTime.Now,
                StatusCode = 1
            };

            return obj;
        }
    }
    #endregion

    #region API Read LeadSalesForce Example Data
    public class APIReadVWI_LeadCustomerSalesForceExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new VWI_LeadCustomerSalesForceFilterDto();
        }
    }
    #endregion



}