#region Namespace Imports
using System;
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model 
{
    public class VWI_CRM_ktb_daftardepositadetailDtoParameter : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string ktb_depositaid { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string ktb_description { get; set; }
        [AntiXss]
        public DateTime ktb_transactiondate { get; set; }
        [AntiXss]
        public string owningbusinessunitname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string ktb_documentno { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public Guid ktb_daftardepositaid { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public Guid ktb_daftardepositadetailid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string ktb_iddetail { get; set; }
        [AntiXss]
        public decimal ktb_amount_base { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string ktb_daftardepositaidname { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string ktb_refferenceno { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public decimal ktb_amount { get; set; }
        [AntiXss]
        public string ktb_statusdebet { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string ktb_daftardepositadetail { get; set; }
        [AntiXss]
        public string ktb_tipe { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
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
