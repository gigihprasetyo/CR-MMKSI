#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_siteinquiry class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:47:11
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_siteinquiryDto : DtoBase
    {
        
		public Guid createdby { get; set; }

		public string createdbyname { get; set; }

		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string DealerCode { get; set; }

		public decimal? exchangerate { get; set; }

		public int? importsequencenumber { get; set; }

		public Guid modifiedby { get; set; }

		public string modifiedbyname { get; set; }

		public string modifiedbyyominame { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid organizationid { get; set; }

		public string organizationidname { get; set; }

		public DateTime? overriddencreatedon { get; set; }

		public short? RowStatus { get; set; }

		public string SourceType { get; set; }

		public int? statecode { get; set; }

		public string statecodename { get; set; }

		public int? statuscode { get; set; }

		public string statuscodename { get; set; }

		public int? timezoneruleversionnumber { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string transactioncurrencyidname { get; set; }

		public int? utcconversiontimezonecode { get; set; }

		public long? versionnumber { get; set; }

		public string xts_company { get; set; }

		public string xts_configurationdescription { get; set; }

		public Guid xts_configurationid { get; set; }

		public string xts_configurationidname { get; set; }

		public string xts_description { get; set; }

		public string xts_exteriorcolordescription { get; set; }

		public Guid xts_exteriorcolorid { get; set; }

		public string xts_exteriorcoloridname { get; set; }

		public string xts_interiorcolordescription { get; set; }

		public Guid xts_interiorcolorid { get; set; }

		public string xts_interiorcoloridname { get; set; }

		public string xts_locking { get; set; }

		public decimal? xts_maximumstock { get; set; }

		public decimal? xts_minimumstock { get; set; }

		public Guid xts_productclassid { get; set; }

		public string xts_productclassidname { get; set; }

		public Guid xts_productid { get; set; }

		public string xts_productidname { get; set; }

		public string xts_producttype { get; set; }

		public string xts_producttypename { get; set; }

		public decimal? xts_quantityavailable { get; set; }

		public decimal? xts_quantityavailableordered { get; set; }

		public decimal? xts_quantityavailablephysical { get; set; }

		public decimal? xts_quantityintransit { get; set; }

		public decimal? xts_quantitynotavailable { get; set; }

		public decimal? xts_quantityonhand { get; set; }

		public decimal? xts_quantityonorder { get; set; }

		public decimal? xts_quantityonpurchaseorder { get; set; }

		public decimal? xts_quantityonsalesorder { get; set; }

		public decimal? xts_quantityontransferorder { get; set; }

		public decimal? xts_quantityonworkorder { get; set; }

		public decimal? xts_quantityordered { get; set; }

		public decimal? xts_quantityorderedreserved { get; set; }

		public decimal? xts_quantityphysicalreserved { get; set; }

		public decimal? xts_quantityposted { get; set; }

		public decimal? xts_quantityreserved { get; set; }

		public bool? xts_recallproduct { get; set; }

		public string xts_recallproductname { get; set; }

		public bool? xts_regularstock { get; set; }

		public string xts_regularstockname { get; set; }

		public decimal? xts_reserved { get; set; }

		public Guid xts_siteid { get; set; }

		public string xts_siteidname { get; set; }

		public string xts_siteinquiry { get; set; }

		public Guid xts_siteinquiryid { get; set; }

		public string xts_styledescription { get; set; }

		public Guid xts_styleid { get; set; }

		public string xts_styleidname { get; set; }

		public decimal? xts_totalcost { get; set; }

		public decimal? xts_totalcost_base { get; set; }

		public decimal? xts_totalphysicalcost { get; set; }

		public decimal? xts_totalphysicalcost_base { get; set; }

		public decimal? xts_unitcost { get; set; }

		public decimal? xts_unitcost_base { get; set; }

    }
}
