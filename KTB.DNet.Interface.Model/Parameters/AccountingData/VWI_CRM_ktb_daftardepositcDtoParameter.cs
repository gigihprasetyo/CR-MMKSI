
#region Namespace Imports
using System;
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_ktb_daftardepositcDtoParameter : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string ktb_daftardepositc { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public decimal ktb_service_base { get; set; }
        [AntiXss]
        public decimal ktb_service { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public decimal ktb_availabledeposit { get; set; }
        [AntiXss]
        public decimal ktb_totaldebit { get; set; }
        [AntiXss]
        public decimal ktb_beginningbalance_base { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public string ktb_idheader { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public decimal ktb_totaldebit_base { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public decimal ktb_ro { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string ktb_dealerid { get; set; }
        [AntiXss]
        public string owningbusinessunitname { get; set; }
        [AntiXss]
        public decimal ktb_beginningbalance { get; set; }
        [AntiXss]
        public decimal ktb_totalcredit_base { get; set; }
        [AntiXss]
        public decimal ktb_endingbalance_base { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string ktb_period { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public decimal ktb_availabledeposit_base { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public Guid ktb_businessunitid { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public Guid ktb_daftardepositcid { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public decimal ktb_endingbalance { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public decimal ktb_ro_base { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public decimal ktb_inclearing { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public decimal ktb_giroreceive { get; set; }
        [AntiXss]
        public decimal ktb_totalcredit { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public decimal ktb_giroreceive_base { get; set; }
        [AntiXss]
        public string ktb_businessunitidname { get; set; }
        [AntiXss]
        public decimal ktb_inclearing_base { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string SourceType { get; set; }
        [AntiXss]
        public Int16 RowStatus { get; set; }
        [AntiXss] 
        public DateTime LastSyncDate { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
