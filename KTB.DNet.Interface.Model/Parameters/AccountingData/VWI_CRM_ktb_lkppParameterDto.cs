#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_benefitParameterDto  class
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
    public class VWI_CRM_ktb_lkppParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public Guid ktb_lkppid { get; set; }
        [AntiXss]
        public String msdyn_companycode { get; set; }
        [AntiXss]
        public String ktb_kodedealer { get; set; }
        [AntiXss]
        public DateTime ktb_tanggalpengajuan { get; set; }
        [AntiXss]
        public String ktb_nopengadaan { get; set; }
        [AntiXss]
        public String ktb_metodepengadaan { get; set; }
        [AntiXss]
        public String ktb_namacustomer { get; set; }
        [AntiXss]
        public String ktb_deskripsi { get; set; }
        [AntiXss]
        public int ktb_status { get; set; }
        [AntiXss]
        public String ktb_catatan { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
