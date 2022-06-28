#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistServiceIncomingParameterDto  class
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class AssistServiceIncomingParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public int AssistUploadLogID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public DateTime TglBukaTransaksi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy HH:mm")]
        public DateTime WaktuMasuk { get; set; }
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public DateTime TglTutupTransaksi { get; set; }
        [DisplayFormat(DataFormatString = "dd/MM/yy HH:mm")]
        public DateTime WaktuKeluar { get; set; }
        public int DealerID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }
        public int DealerBranchID { get; set; }
        [AntiXss]
        public string DealerBranchCode { get; set; }
        public int TrTraineMekanikID { get; set; }
        [AntiXss]
        public string KodeMekanik { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string NoWorkOrder { get; set; }
        public int ChassisMasterID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string KodeChassis { get; set; }
        public int WorkOrderCategoryID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string WorkOrderCategoryCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBePositiveInteger")]
        public int KMService { get; set; }
        public int ServicePlaceID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ServicePlaceCode { get; set; }
        public int ServiceTypeID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ServiceTypeCode { get; set; }
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBePositiveInteger")]
        public Decimal TotalLC { get; set; }
        [AntiXss]
        public string MetodePembayaran { get; set; }
        [AntiXss]
        public string Model { get; set; }
        [AntiXss]
        public string Transmition { get; set; }
        [AntiXss]
        public string DriveSystem { get; set; }
        [AntiXss]
        public string RemarksSystem { get; set; }
        [AntiXss]
        public string RemarksSpecial { get; set; }
        [AntiXss]
        public string RemarksBM { get; set; }
        [Range(0, 1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeZeroOrOne")]
        public int StatusAktif { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int WOStatus { get; set; }
        [Range(0, 1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeZeroOrOne")]
        public int ValidateSystemStatus { get; set; }

        // 20200702 : CR Global Service Reminder - Restia,Benny
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string CustomerOwnerName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string CustomerOwnerPhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string CustomerVisitName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string CustomerVisitPhoneNumber { get; set; }

        [AntiXss]
        public string StallCode { get; set; }

        [AntiXss]
        public string BookingID { get; set; }

        // end CR Globl Service Reminder

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            ValidateMethodePembayaran(results);

            ValidateTotalLC(results);

            return results;
        }

        private void ValidateTotalLC(List<ValidationResult> results)
        {
            if (TotalLC != 0)
            {
                if (TotalLC.ToString().Contains("."))
                {
                    results.Add(new ValidationResult(ValidationResource.InvalidTotalLC));
                }
            }
        }

        private void ValidateMethodePembayaran(List<ValidationResult> results)
        {
            if (!string.IsNullOrEmpty(MetodePembayaran))
            {
                if (!MetodePembayaran.Equals("KREDIT", StringComparison.OrdinalIgnoreCase) && !MetodePembayaran.Equals("TUNAI", StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(new ValidationResult(string.Format(ValidationResource.InvalidMetodePembayaran, MetodePembayaran)));
                }
            }
        }
    }
}

