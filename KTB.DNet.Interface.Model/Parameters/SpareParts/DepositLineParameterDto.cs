#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DepositLineParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class DepositLineParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public int DepositID { get; set; }
        [AntiXss]
        public string DocumentNo { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime ClearingDate { get; set; }
        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }
        [AntiXss]
        public string ReferenceNo { get; set; }
        [AntiXss]
        public string InvoiceNo { get; set; }
        [AntiXss]
        public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

