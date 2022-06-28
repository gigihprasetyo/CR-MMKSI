#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_transferorderrequestingParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 14:10:00
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
    public class VWI_CRM_xts_transferorderrequestingParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public string xts_stockidname { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public decimal xts_amount { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public DateTime xts_requesteddeliverydate { get; set; }
        [AntiXss]
        public Guid xts_productconfigurationid { get; set; }
        [AntiXss]
        public string xjp_idempotentmessage { get; set; }
        [AntiXss]
        public Guid xts_salesorderbusinessunitid { get; set; }
        [AntiXss]
        public string xts_locationidname { get; set; }
        [AntiXss]
        public Guid xts_productinteriorcolorid { get; set; }
        [AntiXss]
        public string xts_status { get; set; }
        [AntiXss]
        public DateTime xts_requestsentdate { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public Guid xts_personinchargeid { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string xts_transferstep { get; set; }
        [AntiXss]
        public Guid xts_newvehiclesalesorderid { get; set; }
        [AntiXss]
        public string xts_productstyleidname { get; set; }
        [AntiXss]
        public string xts_newvehiclesalesorderidname { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string xts_transfercategory { get; set; }
        [AntiXss]
        public string xts_eventdata { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public string xts_productidname { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public string xts_uomidname { get; set; }
        [AntiXss]
        public Guid xts_requestedbusinessunitid { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string xts_parentbusinessunitsalesorderidname { get; set; }
        [AntiXss]
        public string xts_warehouseidname { get; set; }
        [AntiXss]
        public Guid xts_productstyleid { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_handling { get; set; }
        [AntiXss]
        public int xts_quantity { get; set; }
        [AntiXss]
        public decimal xts_amount_base { get; set; }
        [AntiXss]
        public Guid xts_parentbusinessunitsalesorderid { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public Guid xts_productexteriorcolorid { get; set; }
        [AntiXss]
        public string xts_referencenumber { get; set; }
        [AntiXss]
        public string xts_requestedbusinessunitidname { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
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
        public Guid modifiedby { get; set; }
        [AntiXss]
        public Guid xts_transferorderrequestingid { get; set; }
        [AntiXss]
        public string xts_personinchargeidname { get; set; }
        [AntiXss]
        public string xts_transferstepname { get; set; }
        [AntiXss]
        public DateTime xts_transactiondate { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public Guid xts_siteid { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_productexteriorcoloridname { get; set; }
        [AntiXss]
        public string xts_productconfigurationidname { get; set; }
        [AntiXss]
        public string xts_lasttransferorderrequestednumber { get; set; }
        [AntiXss]
        public string xts_handlingname { get; set; }
        [AntiXss]
        public Guid xts_productid { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public string xts_salesorderbusinessunitidname { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string xts_requestorcomment { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public Guid xts_locationid { get; set; }
        [AntiXss]
        public string xts_productinteriorcoloridname { get; set; }
        [AntiXss]
        public string xts_transfercategoryname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_siteidname { get; set; }
        [AntiXss]
        public Guid xts_warehouseid { get; set; }
        [AntiXss]
        public string xts_requestedcomment { get; set; }
        [AntiXss]
        public string xts_transferorderrequestingnumber { get; set; }
        [AntiXss]
        public Guid xts_uomid { get; set; }
        [AntiXss]
        public string xts_productdescription { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public Guid xts_stockid { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
