#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchaserequisitionDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 14/01/2020 9:03:11
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_purchaserequisitionDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_lastactionbyidyominame { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public string ktb_remarksname { get; set; }

		public string xts_purchaserequisitionnumber { get; set; }

		public Guid xts_purchaserequisitionid { get; set; }

		public Guid xts_provinceid { get; set; }

		public string xts_eventdata { get; set; }

		public string statuscodename { get; set; }

		public DateTime xts_exchangeratedate { get; set; }

		public string xts_status { get; set; }

		public string xts_description { get; set; }

		public string modifiedbyyominame { get; set; }

		public string owneridtype { get; set; }

		public decimal xts_totalbaseamount_base { get; set; }

		public Guid createdonbehalfby { get; set; }

		public bool ktb_isfailed { get; set; }

		public string modifiedbyname { get; set; }

		public string xts_shippingsiteidname { get; set; }

		public string ktb_isbodypaintname { get; set; }

		public string ktb_log { get; set; }

		public string owneridname { get; set; }

		public string xts_provinceidname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xts_address2 { get; set; }

		public string xts_lastactionbyidname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public decimal exchangerate { get; set; }

		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		public string ktb_ribbondata { get; set; }

		public Guid owningteam { get; set; }

		public decimal xts_totalbaseamount { get; set; }

		public string ktb_purchasetypename { get; set; }

		public string ktb_ordertype { get; set; }

		public Guid xts_shippingsiteid { get; set; }

		public decimal xts_totalamount { get; set; }

		public decimal xts_totaldiscountamount_base { get; set; }

		public int statecode { get; set; }

		public string ktb_pickingticket { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string xts_taxablename { get; set; }

		public Guid ktb_chassisno { get; set; }

		public decimal xts_exchangerateamount { get; set; }

		public string xts_prpotypeidname { get; set; }

		public decimal xts_totalconsumptiontaxamount { get; set; }

		public string xts_locking { get; set; }

		public Guid xts_prpotypeid { get; set; }

		public string ktb_chassisnoname { get; set; }

		public Guid xts_countryid { get; set; }

		public decimal xts_totalamount_base { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string createdbyyominame { get; set; }

		public string xts_handling { get; set; }

		public Guid xts_cityid { get; set; }

		public string xts_exchangeratetypeidname { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string ktb_chassisno_newname { get; set; }

		public string ktb_purpose { get; set; }

		public string xts_statusname { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string ktb_isfailedname { get; set; }

		public string xts_countryidname { get; set; }

		public DateTime ktb_deliverydate { get; set; }

		public string ktb_ordertypename { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string ktb_parttypename { get; set; }

		public string ktb_parttype { get; set; }

		public string ktb_remarks { get; set; }

		public string createdbyname { get; set; }

		public string ktb_interfacestatus { get; set; }

		public Guid xts_businessunitid { get; set; }

		public Guid xts_exchangeratetypeid { get; set; }

		public decimal xts_totaldiscountamount { get; set; }

		public string xts_address3 { get; set; }

		public string xts_cityidname { get; set; }

		public string xts_handlingname { get; set; }

		public string ktb_interfacehandling { get; set; }

		public string xts_postalcode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public Guid createdby { get; set; }

		public bool ktb_isinterfaced { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_address1 { get; set; }

		public string ktb_purposename { get; set; }

		public decimal xts_exchangerateamount_base { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string ktb_purchasepriorityname { get; set; }

		public Guid owningbusinessunit { get; set; }

		public bool ktb_isbodypaint { get; set; }

		public string ktb_isinterfacedname { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string ktb_interfacehandlingname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string ktb_purchaserequeisitionno { get; set; }

		public DateTime xts_lastactiondate { get; set; }

		public string ktb_interfacestatusname { get; set; }

		public bool xts_taxable { get; set; }

		public Guid ktb_chassisno_new { get; set; }

		public string ktb_purchasetype { get; set; }

		public Guid xts_lastactionbyid { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public string ktb_purchasepriority { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
