﻿#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_xts_province  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/15/2020 16:03:00 
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_xts_province
    {
        public Int64 IDRow { get; set; }
        public Guid xts_provinceid { get; set; }
        public int importsequencenumber { get; set; }
        public string organizationidname { get; set; }
        public string modifiedbyyominame { get; set; }
        public Guid xts_countryid { get; set; }
        public int utcconversiontimezonecode { get; set; }
        public string createdbyyominame { get; set; }
        public string modifiedbyname { get; set; }
        public Int64 versionnumber { get; set; }
        public Guid modifiedby { get; set; }
        public Guid createdby { get; set; }
        public int timezoneruleversionnumber { get; set; }
        public string statuscodename { get; set; }
        public bool ktb_isinterfaced { get; set; }
        public string ktb_isinterfacedname { get; set; }
        public DateTime modifiedon { get; set; }
        public string xts_countryidname { get; set; }
        public string modifiedonbehalfbyyominame { get; set; }
        public int statuscode { get; set; }
        public string createdbyname { get; set; }
        public DateTime createdon { get; set; }
        public Guid organizationid { get; set; }
        public string createdonbehalfbyname { get; set; }
        public string modifiedonbehalfbyname { get; set; }
        public DateTime overriddencreatedon { get; set; }
        public string xts_description { get; set; }
        public string xts_pkcombinationkey { get; set; }
        public Guid createdonbehalfby { get; set; }
        public string xts_province { get; set; }
        public string statecodename { get; set; }
        public int statecode { get; set; }
        public string xts_locking { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public string createdonbehalfbyyominame { get; set; }
        public string ktb_dnetid { get; set; }
    }
}
