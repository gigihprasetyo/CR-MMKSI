#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceValidatorParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/10/2018 9:46
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace KTB.DNet.Interface.Model
{
    public class ServiceValidatorParameterDto : ParameterDtoBase, IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string ServiceType { get; set; }

        [AntiXss]
        public string ChassisNumber { get; set; }

        [AntiXss]
        public string DealerCode { get; set; }

        [DisplayFormat(DataFormatString = "ddMMyyyy")]
        public DateTime ServiceDate { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string WorkOrderNumber { get; set; }

        [AntiXss]
        public string VisitType { get; set; }

        public int StandKM { get; set; }

        [AntiXss]
        public string EngineNumber { get; set; }

        #region PDI Parameters
        [StringLength(1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Kind { get; set; }
        [AntiXss]
        public string PDIStatus { get; set; }
        [DisplayFormat(DataFormatString = "ddMMyyyy")]
        public DateTime PDIDate { get; set; }
        [AntiXss]
        public string ReleaseBy { get; set; }
        [DisplayFormat(DataFormatString = "ddMMyyyy")]
        public DateTime ReleaseDate { get; set; }
        #endregion

        #region Free Service Parameters
        [AntiXss]
        public string FSKindCode { get; set; }
        public int MileAge { get; set; }
        [DisplayFormat(DataFormatString = "ddMMyyyy")]
        public DateTime SoldDate { get; set; }
        public Boolean isBB { get; set; }
        #endregion

        #region PM Parameters
        public string DealerBranchCode { get; set; }
        public string PMKindCode { get; set; }
        public string BookingNo { get; set; }
        public string Remarks { get; set; }
        public string PMStatus { get; set; }
        #endregion

        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Utils.IsAlphaNumeric(ChassisNumber)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, ChassisNumber))); }

            if (ServiceType.Equals("PDI", System.StringComparison.OrdinalIgnoreCase))
            {
                if (PDIDate.Date > DateTime.Now.Date)
                {
                    results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDateCompareError, FieldResource.PDI, ValidationResource.GreaterThan, FieldResource.TodayDate)));
                }
            }
            else if (ServiceType.Equals("PM", System.StringComparison.OrdinalIgnoreCase))
            {
                if (ServiceDate.Date > DateTime.Now.Date) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDateCompareError, FieldResource.ServiceDate, ValidationResource.GreaterThan, FieldResource.TodayDate))); }

                if (!string.IsNullOrEmpty(VisitType))
                {
                    List<string> visitTypeList = new List<string>(new[] { "WO", "WIBO", "WI", "BO" });
                    bool isVisitInRange = visitTypeList.IndexOf(VisitType) != -1;
                    if (!isVisitInRange)
                    {
                        results.Add(new ValidationResult(ErrorCode.DataOptionNotMatch + "#" + string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.VisitType)));
                    }
                }
                else
                {
                    VisitType = "WI";
                }

                if (!VisitType.Equals("WI", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(BookingNo))
                {
                    results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgBookingNoIsMandatoryForNonWI, FieldResource.VisitType)));
                }
            }
            else if (ServiceType.Equals("FS", System.StringComparison.OrdinalIgnoreCase))
            {
                if (!Utils.IsAlphaNumeric(ChassisNumber)) { results.Add(new ValidationResult(FieldResource.ChassisNumber + string.Format(MessageResource.ErrorMsgDataInvalid, ChassisNumber))); }

                //if (!isBB && SoldDate == Constants.DATETIME_DEFAULT_VALUE)
                //{
                //    results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.SoldDate)));
                //}

                //if (SoldDate < Constants.DATETIME_DEFAULT_VALUE || SoldDate >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value)
                //{
                //    results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.SoldDate)));
                //}
            }

            return results;
        }
    }
}

