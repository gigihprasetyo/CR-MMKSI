#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_ordertypeParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 8:22:50
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
    public class VWI_CRM_xts_ordertypeParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string xts_discountdimension6fromname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension7id { get; set; }

		[AntiXss]
		public string ktb_defaultlocationwoname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension4id { get; set; }

		[AntiXss]
		public Guid xts_discdimension10id { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension4id { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_ardimension2idname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension3id { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_salesdimension10idname { get; set; }

		[AntiXss]
		public string ktb_ordertypetointerface { get; set; }

		[AntiXss]
		public string xts_discountaccountfromname { get; set; }

		[AntiXss]
		public string xts_salesaccountfromname { get; set; }

		[AntiXss]
		public string xts_discdimension9idname { get; set; }

		[AntiXss]
		public string xts_cogsdimension5idname { get; set; }

		[AntiXss]
		public string xts_salesdimension4idname { get; set; }

		[AntiXss]
		public string xts_salesaccountfrom { get; set; }

		[AntiXss]
		public string xts_behaviour { get; set; }

		[AntiXss]
		public bool ktb_isnoprice { get; set; }

		[AntiXss]
		public Guid xts_discdimension4id { get; set; }

		[AntiXss]
		public bool ktb_isbilltoatpm { get; set; }

		[AntiXss]
		public string xts_salesdimension4fromname { get; set; }

		[AntiXss]
		public string xts_discountdimension6from { get; set; }

		[AntiXss]
		public bool ktb_isfreeservice { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public bool xts_partspricecalculationmethod { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension10id { get; set; }

		[AntiXss]
		public Guid xts_ardimension1id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension4idname { get; set; }

		[AntiXss]
		public string ktb_transtype { get; set; }

		[AntiXss]
		public Guid xts_ardimension2id { get; set; }

		[AntiXss]
		public Guid xts_discdimension2id { get; set; }

		[AntiXss]
		public string xts_salesdimension4from { get; set; }

		[AntiXss]
		public string xts_salesdimension3from { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string xts_servicetemplateparentgroupidname { get; set; }

		[AntiXss]
		public Guid xts_ordertypeid { get; set; }

		[AntiXss]
		public string xts_salesdimension6fromname { get; set; }

		[AntiXss]
		public string xts_discountdimension1from { get; set; }

		[AntiXss]
		public string xts_salesdimension3idname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension9id { get; set; }

		[AntiXss]
		public Guid xts_salesdimension4id { get; set; }

		[AntiXss]
		public string xts_partspricecalculationmethodname { get; set; }

		[AntiXss]
		public bool ktb_interfacedtoexternalsystem { get; set; }

		[AntiXss]
		public string xts_salesdimension2fromname { get; set; }

		[AntiXss]
		public string xts_discdimension3idname { get; set; }

		[AntiXss]
		public string xts_salesdimension7idname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_discountdimension5fromname { get; set; }

		[AntiXss]
		public string xts_cogsdimension1idname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension1id { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension6idname { get; set; }

		[AntiXss]
		public Guid xts_discdimension7id { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string xts_ardimension3idname { get; set; }

		[AntiXss]
		public string ktb_ordertypetointerfacename { get; set; }

		[AntiXss]
		public Guid xts_ardimension5id { get; set; }

		[AntiXss]
		public Guid xts_salesdimension5id { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension6id { get; set; }

		[AntiXss]
		public Guid ktb_defaultwarehouseid { get; set; }

		[AntiXss]
		public bool ktb_bodypaint { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string ktb_isonlyshownongenuineproductinwopmsname { get; set; }

		[AntiXss]
		public string xts_ordertype { get; set; }

		[AntiXss]
		public bool ktb_isonlyshownongenuineproductinwopms { get; set; }

		[AntiXss]
		public string xts_cogsdimension3idname { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_salesdimension10id { get; set; }

		[AntiXss]
		public string xts_discdimension10idname { get; set; }

		[AntiXss]
		public string xts_ordervehiclename { get; set; }

		[AntiXss]
		public string xts_cogsdimension9idname { get; set; }

		[AntiXss]
		public string xts_discountdimension1fromname { get; set; }

		[AntiXss]
		public string xts_cogsdimension6idname { get; set; }

		[AntiXss]
		public string xts_discountdimension4fromname { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public Guid xts_ardimension6id { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension2id { get; set; }

		[AntiXss]
		public string xts_discountaccountfrom { get; set; }

		[AntiXss]
		public string xts_salesaccountidname { get; set; }

		[AntiXss]
		public string ktb_issmartpackagename { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_salesdimension5idname { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string ktb_isfreeservicename { get; set; }

		[AntiXss]
		public string xts_aftervso { get; set; }

		[AntiXss]
		public string xts_discdimension7idname { get; set; }

		[AntiXss]
		public string ktb_bodypaintname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_discdimension9id { get; set; }

		[AntiXss]
		public string xts_ardimension5idname { get; set; }

		[AntiXss]
		public string ktb_isnopricename { get; set; }

		[AntiXss]
		public string xts_cogsdimension7idname { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension5id { get; set; }

		[AntiXss]
		public string xts_cogsdimension4idname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension5id { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string ktb_description { get; set; }

		[AntiXss]
		public Guid xts_cogsaccountid { get; set; }

		[AntiXss]
		public string xts_cogsdimension8idname { get; set; }

		[AntiXss]
		public string ktb_ordertypetranstypename { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid xts_discountaccountid { get; set; }

		[AntiXss]
		public string xts_behaviourname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_salesdimension5from { get; set; }

		[AntiXss]
		public string ktb_interfacedtoexternalsystemname { get; set; }

		[AntiXss]
		public string xts_cogsdimension2idname { get; set; }

		[AntiXss]
		public Guid xts_servicetemplategroupid { get; set; }

		[AntiXss]
		public string xts_salesdimension6idname { get; set; }

		[AntiXss]
		public string xts_servicetemplategroupidname { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension6id { get; set; }

		[AntiXss]
		public Guid xts_discdimension3id { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_aftervsoname { get; set; }

		[AntiXss]
		public string xts_discountdimension3from { get; set; }

		[AntiXss]
		public string ktb_validatorwotruename { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string ktb_isprintgatepassreadytobeinvoicename { get; set; }

		[AntiXss]
		public bool ktb_issmartpackage { get; set; }

		[AntiXss]
		public string xts_salesdimension5fromname { get; set; }

		[AntiXss]
		public string xts_discdimension8idname { get; set; }

		[AntiXss]
		public string xts_discountdimension2from { get; set; }

		[AntiXss]
		public bool xts_aftersales { get; set; }

		[AntiXss]
		public string xts_salesdimension1from { get; set; }

		[AntiXss]
		public string xts_discdimension1idname { get; set; }

		[AntiXss]
		public Guid xts_ardimension3id { get; set; }

		[AntiXss]
		public string xts_discdimension2idname { get; set; }

		[AntiXss]
		public string xts_salesdimension1fromname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension1id { get; set; }

		[AntiXss]
		public string ktb_isbilltoatpmname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension6id { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension2id { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension3id { get; set; }

		[AntiXss]
		public string ktb_defaultwarehouseidname { get; set; }

		[AntiXss]
		public string xts_salesdimension9idname { get; set; }

		[AntiXss]
		public string xts_discountdimension5from { get; set; }

		[AntiXss]
		public string xts_salesdimension6from { get; set; }

		[AntiXss]
		public string xts_discdimension4idname { get; set; }

		[AntiXss]
		public string xts_aftersalesname { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension8id { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_discdimension1id { get; set; }

		[AntiXss]
		public string xts_salesdimension1idname { get; set; }

		[AntiXss]
		public string xts_salesdimension3fromname { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_ardimension4idname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid xts_discdimension8id { get; set; }

		[AntiXss]
		public Guid xts_discdimension5id { get; set; }

		[AntiXss]
		public Guid xts_discdimension6id { get; set; }

		[AntiXss]
		public string xts_salesdimension8idname { get; set; }

		[AntiXss]
		public bool xts_ordervehicle { get; set; }

		[AntiXss]
		public Guid xts_downpaymentdimension3id { get; set; }

		[AntiXss]
		public string xts_salesdimension2from { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string xts_discdimension5idname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension5idname { get; set; }

		[AntiXss]
		public string xts_discdimension6idname { get; set; }

		[AntiXss]
		public Guid xts_ardimension4id { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public Guid xts_cogsdimension1id { get; set; }

		[AntiXss]
		public string xts_discountdimension2fromname { get; set; }

		[AntiXss]
		public string xts_ardimension6idname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension3idname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension7id { get; set; }

		[AntiXss]
		public string xts_cogsaccountidname { get; set; }

		[AntiXss]
		public Guid xts_salesaccountid { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension1idname { get; set; }

		[AntiXss]
		public Guid ktb_defaultlocationwo { get; set; }

		[AntiXss]
		public string xts_salesdimension2idname { get; set; }

		[AntiXss]
		public bool ktb_isprintgatepassreadytobeinvoice { get; set; }

		[AntiXss]
		public string xts_discountdimension4from { get; set; }

		[AntiXss]
		public Guid xts_salesdimension8id { get; set; }

		[AntiXss]
		public string xts_discountdimension3fromname { get; set; }

		[AntiXss]
		public Guid xts_servicetemplateparentgroupid { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string ktb_ordertypetranstype { get; set; }

		[AntiXss]
		public string xts_ardimension1idname { get; set; }

		[AntiXss]
		public string xts_discountaccountidname { get; set; }

		[AntiXss]
		public string xts_downpaymentdimension2idname { get; set; }

		[AntiXss]
		public bool ktb_validatorwotrue { get; set; }

		[AntiXss]
		public Guid xts_salesdimension2id { get; set; }

		[AntiXss]
		public string xts_cogsdimension10idname { get; set; }

		[AntiXss]
		public Guid xts_salesdimension9id { get; set; }

		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[AntiXss]
		public string xts_regularservicename { get; set; }

		[AntiXss]
		public bool xts_regularservice { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
