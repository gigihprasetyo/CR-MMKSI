#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartPaymentParameterDto  class
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_SparePartPaymentParameterDto : DtoBase, IValidatableObject
    {
        public string ID { get; set; }
        public string ReferenceNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime PostingDate { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string SONumber { get; set; }
        public Decimal Amount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

