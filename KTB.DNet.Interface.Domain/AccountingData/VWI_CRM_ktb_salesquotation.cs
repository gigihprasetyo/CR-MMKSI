#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_ktb_salesquotation  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/21/2020 16:46:00
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_ktb_salesquotation
    {
        public Int64 IDRow { get; set; }
        public Guid activestageid { get; set; }
        public string traversedpath { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public Guid bpf_xts_newvehiclesalesquoteid { get; set; }
        public int statecode { get; set; }
        public string statecodename { get; set; }
        public string activestageidname { get; set; }
        public Guid createdonbehalfby { get; set; }
        public int importsequencenumber { get; set; }
        public string organizationidname { get; set; }
        public int bpf_duration { get; set; }
        public int utcconversiontimezonecode { get; set; }
        public string createdbyyominame { get; set; }
        public string modifiedbyname { get; set; }
        public Int64 versionnumber { get; set; }
        public Guid modifiedby { get; set; }
        public string modifiedbyyominame { get; set; }
        public Guid createdby { get; set; }
        public int timezoneruleversionnumber { get; set; }
        public string statuscodename { get; set; }
        public string processidname { get; set; }
        public string createdonbehalfbyyominame { get; set; }
        public Guid processid { get; set; }
        public DateTime modifiedon { get; set; }
        public string bpf_xts_newvehiclesalesquoteidname { get; set; }
        public Guid businessprocessflowinstanceid { get; set; }
        public string modifiedonbehalfbyyominame { get; set; }
        public int statuscode { get; set; }
        public string createdbyname { get; set; }
        public DateTime createdon { get; set; }
        public Guid organizationid { get; set; }
        public DateTime activestagestartedon { get; set; }
        public DateTime completedon { get; set; }
        public string modifiedonbehalfbyname { get; set; }
        public string bpf_name { get; set; }
        public string createdonbehalfbyname { get; set; }
        public DateTime overriddencreatedon { get; set; }
    }
}
