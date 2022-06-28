#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DealerBranchParameterDto  class
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
    public class DealerBranchParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        public int DealerID { get; set; }

        [AntiXss]
        public string Name { get; set; }

        [AntiXss]
        public string Status { get; set; }

        [AntiXss]
        public string Address { get; set; }

        public int CityID { get; set; }

        [AntiXss]
        public string ZipCode { get; set; }

        public int ProvinceID { get; set; }

        [AntiXss]
        public string Phone { get; set; }

        [AntiXss]
        public string Fax { get; set; }

        [AntiXss]
        public string Website { get; set; }

        [AntiXss]
        public string Email { get; set; }

        [AntiXss]
        public string TypeBranch { get; set; }

        [AntiXss]
        public string DealerBranchCode { get; set; }

        [AntiXss]
        public string Term1 { get; set; }

        [AntiXss]
        public string Term2 { get; set; }

        public int MainAreaID { get; set; }

        public int Area1ID { get; set; }

        public int Area2ID { get; set; }

        [AntiXss]
        public string BranchAssignmentNo { get; set; }

        public DateTime BranchAssigmentDate { get; set; }

        [AntiXss]
        public string SalesUnitFlag { get; set; }

        [AntiXss]
        public string ServiceFlag { get; set; }

        [AntiXss]
        public string SparepartFlag { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
