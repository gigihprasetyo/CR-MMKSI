#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventbatchDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 13:29:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_inventbatchDto : DtoBase
    {
        public string createdonbehalfbyyominame { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public int statecode { get; set; }
        public string xts_productidname { get; set; }
        public string statecodename { get; set; }
        public Guid createdonbehalfby { get; set; }
        public Guid xts_inventbatchid { get; set; }
        public int importsequencenumber { get; set; }
        public string organizationidname { get; set; }
        public string xts_company { get; set; }
        public int utcconversiontimezonecode { get; set; }
        public string createdbyyominame { get; set; }
        public string modifiedbyname { get; set; }
        public Int64 versionnumber { get; set; }
        public Guid modifiedby { get; set; }
        public string modifiedbyyominame { get; set; }
        public Guid createdby { get; set; }
        public int timezoneruleversionnumber { get; set; }
        public DateTime xts_expireddate { get; set; }
        public string statuscodename { get; set; }
        public DateTime modifiedon { get; set; }
        public string modifiedonbehalfbyname { get; set; }
        public string modifiedonbehalfbyyominame { get; set; }
        public int statuscode { get; set; }
        public string createdbyname { get; set; }
        public DateTime createdon { get; set; }
        public Guid organizationid { get; set; }
        public string xts_batchnumber { get; set; }
        public string createdonbehalfbyname { get; set; }
        public string xts_description { get; set; }
        public DateTime xts_receiptdate { get; set; }
        public Guid xts_productid { get; set; }
        public DateTime overriddencreatedon { get; set; }
    }
}
