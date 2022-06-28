#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : AssistServiceIncomingBPParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021-03-23
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ServiceMMSParameterDto : ParameterDtoBase
    {
        public int? ID { get; set; }

        [AntiXss]
        public string PBU { get; set; }

        [AntiXss]
        public string BU { get; set; }

        [AntiXss]
        public string BU_Branch { get; set; }

        public string WO_No { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime? WO_Service_Date { get; set; }

        [AntiXss]
        public string ChassisNo { get; set; }

        [AntiXss]
        public string PlateNo { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime? Next_Estimated_Service_Date { get; set; }

        public string Notes { get; set; }

        [AntiXss]
        public int Status { get; set; }
    }

    public class ServiceMMSCreateParameterDto : ServiceMMSParameterDto, IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string BU { get { return base.BU; } set { base.BU = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string WO_No { get { return base.WO_No; } set { base.WO_No = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public new DateTime? WO_Service_Date { get { return base.WO_Service_Date; } set { base.WO_Service_Date = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string ChassisNo { get { return base.ChassisNo; } set { base.ChassisNo = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string PlateNo { get { return base.PlateNo; } set { base.PlateNo = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public new DateTime? Next_Estimated_Service_Date { get { return base.Next_Estimated_Service_Date; } set { base.Next_Estimated_Service_Date = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string Notes { get { return base.Notes; } set { base.Notes = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int Status { get { return base.Status; } set { base.Status = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            ValidateStatus(results);

            ValidateNextEstimateServiceDate(results);

            return results;
        }

        private void ValidateStatus(List<ValidationResult> results)
        {
            if (Status != 0 && Status != 1)
            {
                results.Add(new ValidationResult("Status harus berisi 0 (Active) atau 1 (InActive)"));
            }
        }

        private void ValidateNextEstimateServiceDate(List<ValidationResult> results)
        {
            if (Next_Estimated_Service_Date != null)
            {
                if (Next_Estimated_Service_Date <= WO_Service_Date)
                {
                    string errMessage = "{0} {1} harus lebih besar dari {2} {3}";
                    results.Add(new ValidationResult(string.Format(errMessage, "Next Estimate Service Date", Convert.ToDateTime(Next_Estimated_Service_Date).ToString("dd/MM/yyyy"), "WO Service Date", Convert.ToDateTime(WO_Service_Date).ToString("dd/MM/yyyy"))));
                }
            }
        }
    }

    public class ServiceMMSUpdateParameterDto : ServiceMMSParameterDto, IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int? ID { get { return base.ID; } set { base.ID = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string BU { get { return base.BU; } set { base.BU = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int Status { get { return base.Status; } set { base.Status = value; } }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            ValidateStatus(results);

            return results;
        }

        private void ValidateStatus(List<ValidationResult> results)
        {
            if (Status != 0 && Status != 1)
            {
                results.Add(new ValidationResult("Status harus berisi 0 (Active) atau 1 (InActive)"));
            }
            else
            {
                if (Status == 0)
                {
                    if (string.IsNullOrEmpty(WO_No))
                    {
                        results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, WO_No)));
                    }
                    if (WO_Service_Date == null)
                    {
                        results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, WO_Service_Date)));
                    }
                    if (string.IsNullOrEmpty(ChassisNo))
                    {
                        results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, ChassisNo)));
                    }
                    if (string.IsNullOrEmpty(Notes))
                    {
                        results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, Notes)));
                    }
                    if (Next_Estimated_Service_Date != null && WO_Service_Date != null)
                    {
                        if (Next_Estimated_Service_Date <= WO_Service_Date)
                        {
                            string errMessage = "{0} {1} harus lebih besar dari {2} {3}";
                            results.Add(new ValidationResult(string.Format(errMessage, "Next Estimate Service Date", Convert.ToDateTime(Next_Estimated_Service_Date).ToString("dd/MM/yyyy"), "WO Service Date", Convert.ToDateTime(WO_Service_Date).ToString("dd/MM/yyyy"))));
                        }
                    }
                }
            }
        }
    }
}
