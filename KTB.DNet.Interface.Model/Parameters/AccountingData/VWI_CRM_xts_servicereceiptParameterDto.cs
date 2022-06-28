#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipmentParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
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
    public class VWI_CRM_xts_servicereceiptParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public Guid xts_parentbusinessunitid { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public Guid xts_consumptiontaxid { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public Guid xts_serviceinstructionid { get; set; }
        [AntiXss]
        public string xjp_idempotentmessage { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string xts_status { get; set; }
        [AntiXss]
        public decimal xts_totalactualcost_base { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string xts_laststatus { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string xts_servicetypename { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public DateTime xts_subcontractinvoicedate { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public string xts_servicereceiptnumber { get; set; }
        [AntiXss]
        public decimal xts_actualtechnicalfee { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public decimal xts_actualpartsfee { get; set; }
        [AntiXss]
        public decimal xts_totalbaseamount { get; set; }
        [AntiXss]
        public Guid xts_servicereceiptid { get; set; }
        [AntiXss]
        public Int32 statecode { get; set; }
        [AntiXss]
        public decimal xts_actualsubcontractfee_base { get; set; }
        [AntiXss]
        public string xts_serviceinstructionidname { get; set; }
        [AntiXss]
        public Int32 timezoneruleversionnumber { get; set; }
        [AntiXss]
        public decimal xts_actualpartsfee_base { get; set; }
        [AntiXss]
        public Int32 xts_servicedestinationlookuptype { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount { get; set; }
        [AntiXss]
        public string xts_handling { get; set; }
        [AntiXss]
        public string xts_servicedestinationlookupname { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public Guid xts_servicedestinationid { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public Int32 utcconversiontimezonecode { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string xts_statusname { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public Int32 importsequencenumber { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public decimal xts_actualsubcontractfee { get; set; }
        [AntiXss]
        public decimal xts_subcontractcostincludetax_base { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_laststatusname { get; set; }
        [AntiXss]
        public DateTime xts_transactiondate { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public decimal xts_totalactualcost { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public decimal xts_consumptiontaxamount_base { get; set; }
        [AntiXss]
        public Guid xts_servicedestinationbusinessunitid { get; set; }
        [AntiXss]
        public decimal xts_subcontractcostincludetax { get; set; }
        [AntiXss]
        public string xts_servicedestinationidname { get; set; }
        [AntiXss]
        public string xts_servicedestinationbusinessunitidname { get; set; }
        [AntiXss]
        public string xts_consumptiontaxidname { get; set; }
        [AntiXss]
        public decimal xts_totalbaseamount_base { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string xts_vehicleidentificationnumber { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xts_servicedestinationdescription { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string xts_stockidname { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public decimal xts_actualtechnicalfee_base { get; set; }
        [AntiXss]
        public string xts_parentbusinessunitidname { get; set; }
        [AntiXss]
        public string xts_servicetype { get; set; }
        [AntiXss]
        public Int32 statuscode { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_handlingname { get; set; }
        [AntiXss]
        public string xts_subcontractinvoiceno { get; set; }
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
