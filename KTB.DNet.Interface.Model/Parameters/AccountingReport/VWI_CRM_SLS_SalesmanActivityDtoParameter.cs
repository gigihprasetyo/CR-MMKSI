#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_SalesmanActivityParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/01/2020 16:54:12
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
    public class VWI_CRM_SLS_SalesmanActivityParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string Atasan { get; set; }

		[AntiXss]
		public string Sales { get; set; }

		[AntiXss]
		public DateTime PostDate { get; set; }

		[AntiXss]
		public string Nama { get; set; }

		[AntiXss]
		public string Alamat { get; set; }

		[AntiXss]
		public string Telepon { get; set; }

		[AntiXss]
		public string ModelKendaraan { get; set; }

		[AntiXss]
		public string DeskripsiKendaraan { get; set; }

		[AntiXss]
		public decimal Harga { get; set; }

		[AntiXss]
		public string MacamCall { get; set; }

		[AntiXss]
		public string SumberInfo { get; set; }

		[AntiXss]
		public string Hasil { get; set; }

		[AntiXss]
		public string Keterangan { get; set; }

		[AntiXss]
		public string SPK { get; set; }

		[AntiXss]
		public string DO { get; set; }

		[AntiXss]
		public string Comment { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
