#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchaserequisitionParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_purchaserequisitionParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_lastactionbyidyominame { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string ktb_remarksname { get; set; }

		[AntiXss]
		public string xts_purchaserequisitionnumber { get; set; }

		[AntiXss]
		public Guid xts_purchaserequisitionid { get; set; }

		[AntiXss]
		public Guid xts_provinceid { get; set; }

		[AntiXss]
		public string xts_eventdata { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public DateTime xts_exchangeratedate { get; set; }

		[AntiXss]
		public string xts_status { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount_base { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public bool ktb_isfailed { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_shippingsiteidname { get; set; }

		[AntiXss]
		public string ktb_isbodypaintname { get; set; }

		[AntiXss]
		public string ktb_log { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string xts_provinceidname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public string xts_lastactionbyidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount_base { get; set; }

		[AntiXss]
		public string ktb_ribbondata { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public decimal xts_totalbaseamount { get; set; }

		[AntiXss]
		public string ktb_purchasetypename { get; set; }

		[AntiXss]
		public string ktb_ordertype { get; set; }

		[AntiXss]
		public Guid xts_shippingsiteid { get; set; }

		[AntiXss]
		public decimal xts_totalamount { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount_base { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string ktb_pickingticket { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_taxablename { get; set; }

		[AntiXss]
		public Guid ktb_chassisno { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount { get; set; }

		[AntiXss]
		public string xts_prpotypeidname { get; set; }

		[AntiXss]
		public decimal xts_totalconsumptiontaxamount { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public Guid xts_prpotypeid { get; set; }

		[AntiXss]
		public string ktb_chassisnoname { get; set; }

		[AntiXss]
		public Guid xts_countryid { get; set; }

		[AntiXss]
		public decimal xts_totalamount_base { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_handling { get; set; }

		[AntiXss]
		public Guid xts_cityid { get; set; }

		[AntiXss]
		public string xts_exchangeratetypeidname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string ktb_chassisno_newname { get; set; }

		[AntiXss]
		public string ktb_purpose { get; set; }

		[AntiXss]
		public string xts_statusname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string ktb_isfailedname { get; set; }

		[AntiXss]
		public string xts_countryidname { get; set; }

		[AntiXss]
		public DateTime ktb_deliverydate { get; set; }

		[AntiXss]
		public string ktb_ordertypename { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string ktb_parttypename { get; set; }

		[AntiXss]
		public string ktb_parttype { get; set; }

		[AntiXss]
		public string ktb_remarks { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string ktb_interfacestatus { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Guid xts_exchangeratetypeid { get; set; }

		[AntiXss]
		public decimal xts_totaldiscountamount { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public string xts_cityidname { get; set; }

		[AntiXss]
		public string xts_handlingname { get; set; }

		[AntiXss]
		public string ktb_interfacehandling { get; set; }

		[AntiXss]
		public string xts_postalcode { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public string ktb_purposename { get; set; }

		[AntiXss]
		public decimal xts_exchangerateamount_base { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string ktb_purchasepriorityname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public bool ktb_isbodypaint { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string ktb_interfacehandlingname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_purchaserequeisitionno { get; set; }

		[AntiXss]
		public DateTime xts_lastactiondate { get; set; }

		[AntiXss]
		public string ktb_interfacestatusname { get; set; }

		[AntiXss]
		public bool xts_taxable { get; set; }

		[AntiXss]
		public Guid ktb_chassisno_new { get; set; }

		[AntiXss]
		public string ktb_purchasetype { get; set; }

		[AntiXss]
		public Guid xts_lastactionbyid { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string ktb_purchasepriority { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
