#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_LeadCustomerSalesForceDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class VWI_LeadCustomerSalesForceDto
    {
        public int DNetID { get; set; }
        public string SumberData { get; set; }
        public string CreatedBy { get; set; }
        public string CreateDate { get; set; }
        public string CreateDate_YYYYMMDD { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string CustomerTypeID { get; set; }
        public string CustomerType { get; set; }
        public string SalesmanCode { get; set; }
        public string Name { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SexID { get; set; }
        public string Sex { get; set; }
        public string AgeSegmentID { get; set; }
        public string AgeSegment { get; set; }
        public string CustomerStatusID { get; set; }
        public string CustomerStatus { get; set; }
        public string InformationTypeID { get; set; }
        public string InformationType { get; set; }
        public string InformationSourceID { get; set; }
        public string InformationSource { get; set; }
        public string CustomerPurposeID { get; set; }
        public string CustomerPurpose { get; set; }
        public string ProspectDate { get; set; }
        public string ProspectDate_YYYYMMDD { get; set; }
        public string VechileTypeCode { get; set; }
        public string Description { get; set; }
        public string CurrVehicleType { get; set; }
        public string CurrVehicleBrand { get; set; }
        public string Note { get; set; }
        public string Keterangan { get; set; }
        public string StatusResponseID { get; set; }
        public string StatusResponse { get; set; }
        public string WebID { get; set; }
        public DateTime LastUpdateTime { get; set; }

    }
}
