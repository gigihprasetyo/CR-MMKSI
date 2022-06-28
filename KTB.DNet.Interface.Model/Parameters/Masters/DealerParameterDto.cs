#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DealerParameterDto  class
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
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class DealerParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerName")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DealerName { get; set; }

        [AntiXss]
        public string Status { get; set; }

        [AntiXss]
        public string Title { get; set; }

        [AntiXss]
        public string SearchTerm1 { get; set; }

        [AntiXss]
        public string SearchTerm2 { get; set; }

        [AntiXss]
        public string Address { get; set; }

        [AntiXss]
        public string ZipCode { get; set; }

        [AntiXss]
        public string Phone { get; set; }

        [AntiXss]
        public string Fax { get; set; }

        [AntiXss]
        public string Website { get; set; }

        [AntiXss]
        public string Email { get; set; }

        [AntiXss]
        public string SalesUnitFlag { get; set; }

        [AntiXss]
        public string ServiceFlag { get; set; }

        [AntiXss]
        public string SparepartFlag { get; set; }

        [AntiXss]
        public string SPANumber { get; set; }

        public DateTime SPADate { get; set; }

        public int FreePPh22Indicator { get; set; }

        public DateTime FreePPh22From { get; set; }

        public DateTime FreePPh22To { get; set; }

        public int DueDate { get; set; }

        [AntiXss]
        public string AgreementNo { get; set; }

        public DateTime AgreementDate { get; set; }

        [AntiXss]
        public string CreditAccount { get; set; }

        [AntiXss]
        public string LegalStatus { get; set; }

        public DealerDto MainDealer { get; set; }

        //private _area1 As Area1
        //private _area2 As Area2
        //private _mainArea As MainArea
        //private _city As City
        //private _dealerGroup As DealerGroup
        //private _province As Province

        public ArrayList DealerAdditionals { get; set; }

        public ArrayList BuletinOrganizations { get; set; }

        public ArrayList Deposits { get; set; }

        public ArrayList DepositC2s { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
