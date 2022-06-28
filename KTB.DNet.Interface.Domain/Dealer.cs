#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Dealer  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace KTB.DNet.Interface.Domain
{
    [Table("Dealer")]
    public class Dealer
    {
        [Key]
        public Int16 Id { get; set; }

        public string DealerCode { get; set; }

        public string DealerName { get; set; }

        public string Status { get; set; }

        public string Title { get; set; }

        public string SearchTerm1 { get; set; }

        public string SearchTerm2 { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public string SalesUnitFlag { get; set; }

        public string ServiceFlag { get; set; }

        public string SparepartFlag { get; set; }

        public string SPANumber { get; set; }

        public DateTime SPADate { get; set; }

        public int FreePPh22Indicator { get; set; }

        public DateTime FreePPh22From { get; set; }

        public DateTime FreePPh22To { get; set; }

        public int DueDate { get; set; }

        public string AgreementNo { get; set; }

        public DateTime AgreementDate { get; set; }

        public string CreditAccount { get; set; }

        public string LegalStatus { get; set; }

        public Int16 RowStatus { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public string LastUpdateBy { get; set; }

        public DateTime LastUpdateTime { get; set; }

    }
}
