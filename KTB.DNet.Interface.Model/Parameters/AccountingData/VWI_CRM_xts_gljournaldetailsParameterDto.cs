#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_gljournaldetailsParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-09-28
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
    public class VWI_CRM_xts_gljournaldetailsParameterDto : ParameterDtoBase, IValidatableObject
    {

        [AntiXss]
        public string xts_accounttypename { get; set; }
        public Guid xts_offsetaccountcustomerid { get; set; }
        public Guid modifiedby { get; set; }

        [AntiXss]
        public string xts_dimension2idname { get; set; }
        public Guid modifiedonbehalfby { get; set; }

        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }

        [AntiXss]
        public string organizationidname { get; set; }
        public int statuscode { get; set; }
        public Guid xts_accountid { get; set; }
        public Guid xts_offsetaccountid { get; set; }
        public Guid xts_accountcashandbankid { get; set; }
        public decimal xts_credit_base { get; set; }
        public Guid transactioncurrencyid { get; set; }

        [AntiXss]
        public string statuscodename { get; set; }

        [AntiXss]
        public string xts_accountcashandbankidname { get; set; }
        public DateTime xts_date { get; set; }

        [AntiXss]
        public string xts_accountidinformation { get; set; }

        [AntiXss]
        public string xts_description { get; set; }
        public DateTime overriddencreatedon { get; set; }
        public decimal xts_credit { get; set; }

        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        public DateTime modifiedon { get; set; }
        public decimal xts_debit { get; set; }

        [AntiXss]
        public string xts_voucher { get; set; }

        [AntiXss]
        public string xts_accountvendoridname { get; set; }
        public int timezoneruleversionnumber { get; set; }

        [AntiXss]
        public string transactioncurrencyidname { get; set; }

        [AntiXss]
        public string xts_offsetaccountidname { get; set; }

        [AntiXss]
        public string xts_invoicenumber { get; set; }
        public Int64 versionnumber { get; set; }

        [AntiXss]
        public string xts_offsetaccountcustomeridyominame { get; set; }
        public Guid xts_gljournalid { get; set; }

        [AntiXss]
        public string xts_offsetaccountvendoridname { get; set; }

        [AntiXss]
        public string createdbyname { get; set; }
        public int statecode { get; set; }
        public Guid xts_gljournalaccountid { get; set; }
        public Guid xts_accountvendorid { get; set; }
        public Guid xts_accountbankid { get; set; }
        public Guid xts_offsetaccountcashandbankid { get; set; }

        [AntiXss]
        public string xts_dimension3idname { get; set; }
        public int importsequencenumber { get; set; }
        public Guid xts_dimension3id { get; set; }
        public decimal xts_debit_base { get; set; }
        public DateTime createdon { get; set; }

        [AntiXss]
        public string xts_accountcustomeridname { get; set; }

        [AntiXss]
        public string xts_accountbankidname { get; set; }

        [AntiXss]
        public string xts_gljournalidname { get; set; }

        [AntiXss]
        public string xts_accountcustomeridyominame { get; set; }
        public Guid createdonbehalfby { get; set; }
        public int utcconversiontimezonecode { get; set; }

        [AntiXss]
        public string xts_accountidname { get; set; }
        public Guid xts_dimension1id { get; set; }

        [AntiXss]
        public string xts_dimension1idname { get; set; }

        [AntiXss]
        public string xts_offsetaccounttype { get; set; }

        [AntiXss]
        public string xts_company { get; set; }

        [AntiXss]
        public string xts_offsetaccountcustomeridname { get; set; }
        public Guid xts_offsetaccountbankid { get; set; }

        [AntiXss]
        public string xts_offsetaccountcashandbankidname { get; set; }

        [AntiXss]
        public string createdonbehalfbyname { get; set; }

        [AntiXss]
        public string xts_accountname { get; set; }
        public Guid xts_dimension5id { get; set; }

        [AntiXss]
        public string xts_accounttype { get; set; }

        [AntiXss]
        public string xts_offsetaccounttypename { get; set; }
        public Guid xts_dimension2id { get; set; }
        public Guid xts_offsetaccountvendorid { get; set; }

        [AntiXss]
        public string xts_offsetaccountbankidname { get; set; }
        public Guid xts_accountcustomerid { get; set; }
        public Guid xts_gljournaldetailsid { get; set; }

        [AntiXss]
        public string xts_currency { get; set; }

        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }

        [AntiXss]
        public string xts_dimension6idname { get; set; }

        [AntiXss]
        public decimal exchangerate { get; set; }

        [AntiXss]
        public string xts_transactionreference { get; set; }

        [AntiXss]
        public string createdbyyominame { get; set; }

        [AntiXss]
        public string xts_offsetaccountidinformation { get; set; }

        [AntiXss]
        public string xts_dimension4idname { get; set; }

        [AntiXss]
        public string xts_offsetaccountname { get; set; }
        public Guid createdby { get; set; }

        [AntiXss]
        public string xts_postingprofile { get; set; }

        [AntiXss]
        public string modifiedbyyominame { get; set; }
        public Guid xts_dimension4id { get; set; }

        [AntiXss]
        public string xts_dimension5idname { get; set; }

        [AntiXss]
        public string statecodename { get; set; }

        [AntiXss]
        public string xts_documentnumber { get; set; }
        public Guid organizationid { get; set; }
        public DateTime xts_duedate { get; set; }

        [AntiXss]
        public string xts_gljournalnumber { get; set; }

        [AntiXss]
        public string xts_errors { get; set; }

        [AntiXss]
        public string modifiedbyname { get; set; }
        public Guid xts_dimension6id { get; set; }

        [AntiXss]
        public string xts_gljournalaccountidname { get; set; }
        public DateTime xts_documentdate { get; set; }

        [AntiXss]
        public string xts_gljournaldetailsnumber { get; set; }

        [AntiXss]
        public string DealerCode { get; set; }

        [AntiXss]
        public string SourceType { get; set; }
        public int RowStatus { get; set; }
        public DateTime LastSyncDate { get; set; }
        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
