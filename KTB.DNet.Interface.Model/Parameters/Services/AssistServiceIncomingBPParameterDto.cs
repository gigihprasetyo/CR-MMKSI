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
    public class AssistServiceIncomingBPParameterDto : ParameterDtoBase
    {
        public int ID { get; set; }

        public int AssistUploadLogID { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public DateTime? TglPengajuanEstimasi { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public DateTime? TglPersetujuanEstimasi { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public DateTime? TglBukaTransaksi { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yy HH:mm")]
        public DateTime? WaktuMasuk { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public DateTime? TglJanjiSelesai { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public DateTime? TglTutupTransaksi { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yy HH:mm")]
        public DateTime? WaktuKeluar { get; set; }

        public int? DealerID { get; set; }

        [AntiXss]
        public string DealerCode { get; set; }

        public int? TrTraineMekanikID { get; set; }

        [AntiXss]
        public string KodeMekanik { get; set; }

        [AntiXss]
        public string NoWorkOrder { get; set; }

        public int? ChassisMasterID { get; set; }

        [AntiXss]
        public string KodeChassis { get; set; }

        public string VehicleModelDesc { get; set; }

        public string VehicleColorDesc { get; set; }

        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBePositiveInteger")]
        public int? KMService { get; set; }

        public int? WorkOrderCategoryID { get; set; }

        [AntiXss]
        public string WorkOrderCategoryCode { get; set; }

        public int? ServiceTypeID { get; set; }

        [AntiXss]
        public string ServiceTypeCode { get; set; }

        public string ServiceBooking { get; set; }

        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBePositiveInteger")]
        public decimal? TotalLC { get; set; }

        public decimal? TotalSubOrder { get; set; }

        public decimal? TotalCat { get; set; }

        public decimal? TotalNonCat { get; set; }

        [AntiXss]
        public string DamageCategory { get; set; }

        [AntiXss]
        public string TotalPanel { get; set; }

        [AntiXss]
        public string MethodofPayment { get; set; }

        [AntiXss]
        public string InsuranceName { get; set; }

        [AntiXss]
        public string RemarksSystem { get; set; }

        [AntiXss]
        public string RemarksSpecial { get; set; }

        [AntiXss]
        public string RemarksBM { get; set; }

        public int? WOStatus { get; set; }

        [Range(0, 1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeZeroOrOne")]
        public int? StatusAktif { get; set; }

        [Range(0, 1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeZeroOrOne")]
        public int? ValidateSystemStatus { get; set; }

        public string CustomerOwnerName { get; set; }

        public string CustomerOwnerPhoneNumber { get; set; }

        public string CustomerVisitName { get; set; }

        public string CustomerVisitPhoneNumber { get; set; }

    }

    public class AssistServiceIncomingBPCreateParameterDto : AssistServiceIncomingBPParameterDto, IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglPengajuanEstimasi { get { return base.TglPengajuanEstimasi; } set { base.TglPengajuanEstimasi = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglPersetujuanEstimasi { get { return base.TglPersetujuanEstimasi; } set { base.TglPersetujuanEstimasi = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglBukaTransaksi { get { return base.TglBukaTransaksi; } set { base.TglBukaTransaksi = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy HH:mm")]
        public new DateTime? WaktuMasuk { get { return base.WaktuMasuk; } set { base.WaktuMasuk = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglJanjiSelesai { get { return base.TglJanjiSelesai; } set { base.TglJanjiSelesai = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string KodeChassis { get { return base.KodeChassis; } set { base.KodeChassis = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int? KMService { get { return base.KMService; } set { base.KMService = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string WorkOrderCategoryCode { get { return base.WorkOrderCategoryCode; } set { base.WorkOrderCategoryCode = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string ServiceTypeCode { get { return base.ServiceTypeCode; } set { base.ServiceTypeCode = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int? WOStatus { get { return base.WOStatus; } set { base.WOStatus = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string CustomerOwnerName { get { return base.CustomerOwnerName; } set { base.CustomerOwnerName = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string CustomerOwnerPhoneNumber { get { return base.CustomerOwnerPhoneNumber; } set { base.CustomerOwnerPhoneNumber = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string CustomerVisitName { get { return base.CustomerVisitName; } set { base.CustomerVisitName = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string CustomerVisitPhoneNumber { get { return base.CustomerVisitPhoneNumber; } set { base.CustomerVisitPhoneNumber = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string DealerCode { get { return base.DealerCode; } set { base.DealerCode = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string NoWorkOrder { get { return base.NoWorkOrder; } set { base.NoWorkOrder = value; } }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            ValidateMethodePembayaran(results);

            ValidateTotalLC(results);

            ValidateBooking(results);

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
            if (!string.IsNullOrEmpty(MethodofPayment))
            {
                if (!MethodofPayment.Equals("KREDIT", StringComparison.OrdinalIgnoreCase) && !MethodofPayment.Equals("TUNAI", StringComparison.OrdinalIgnoreCase) && !MethodofPayment.Equals("ASURANSI", StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(new ValidationResult(string.Format(ValidationResource.InvalidMetodePembayaranBP, MethodofPayment)));
                }
            }
        }

        private void ValidateBooking(List<ValidationResult> results)
        {
            if (!string.IsNullOrEmpty(ServiceBooking))
            {
                if (!ServiceBooking.Equals("Yes", StringComparison.OrdinalIgnoreCase) && !ServiceBooking.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(new ValidationResult("Service Booking harus berisi Yes atau No"));
                }
            }
        }
    }

    public class AssistServiceIncomingBPUpdateParameterDto : AssistServiceIncomingBPParameterDto, IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int ID { get { return base.ID; } set { base.ID = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglPengajuanEstimasi { get { return base.TglPengajuanEstimasi; } set { base.TglPengajuanEstimasi = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglPersetujuanEstimasi { get { return base.TglPersetujuanEstimasi; } set { base.TglPersetujuanEstimasi = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglBukaTransaksi { get { return base.TglBukaTransaksi; } set { base.TglBukaTransaksi = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy HH:mm")]
        public new DateTime? WaktuMasuk { get { return base.WaktuMasuk; } set { base.WaktuMasuk = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglJanjiSelesai { get { return base.TglJanjiSelesai; } set { base.TglJanjiSelesai = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglTutupTransaksi { get { return base.TglBukaTransaksi; } set { base.TglBukaTransaksi = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy HH:mm")]
        public new DateTime? WaktuKeluar { get { return base.WaktuKeluar; } set { base.WaktuKeluar = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string KodeMekanik { get { return base.KodeMekanik; } set { base.KodeMekanik = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string VehicleModelDesc { get { return base.VehicleModelDesc; } set { base.VehicleModelDesc = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string VehicleColorDesc { get { return base.VehicleColorDesc; } set { base.VehicleColorDesc = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int? KMService { get { return base.KMService; } set { base.KMService = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string ServiceTypeCode { get { return base.ServiceTypeCode; } set { base.ServiceTypeCode = value; } }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            ValidateMethodePembayaran(results);

            ValidateTotalLC(results);

            ValidateBooking(results);

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
            if (!string.IsNullOrEmpty(MethodofPayment))
            {
                if (!MethodofPayment.Equals("KREDIT", StringComparison.OrdinalIgnoreCase) && !MethodofPayment.Equals("TUNAI", StringComparison.OrdinalIgnoreCase) && !MethodofPayment.Equals("ASURANSI", StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(new ValidationResult(string.Format(ValidationResource.InvalidMetodePembayaranBP, MethodofPayment)));
                }
            }
            if (!string.IsNullOrEmpty(VehicleModelDesc) || !string.IsNullOrEmpty(VehicleColorDesc))
            {
                string maxLengthVehicleModelColorDesc = "Maksimum char '{0}' adalah 30 char";
                //if (VehicleModelDesc.Length > 30)
                //{
                //    results.Add(new ValidationResult(string.Format(maxLengthVehicleModelColorDesc, "VehicleModelDesc")));
                //}
                if (VehicleColorDesc.Length > 30)
                {
                    results.Add(new ValidationResult(string.Format(maxLengthVehicleModelColorDesc, "VehicleColorDesc")));
                }
            }
        }

        private void ValidateBooking(List<ValidationResult> results)
        {
            if (!string.IsNullOrEmpty(ServiceBooking))
            {
                if (!ServiceBooking.Equals("Yes", StringComparison.OrdinalIgnoreCase) && !ServiceBooking.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(new ValidationResult("Service Booking harus berisi Yes atau No"));
                }
            }
        }
    }

    public class AssistServiceIncomingBPDeleteParameterDto : AssistServiceIncomingBPParameterDto
    {

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public new DateTime? TglTutupTransaksi { get { return base.TglBukaTransaksi; } set { base.TglBukaTransaksi = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy HH:mm")]
        public new DateTime? WaktuKeluar { get { return base.WaktuKeluar; } set { base.WaktuKeluar = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string DealerCode { get { return base.DealerCode; } set { base.DealerCode = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new string NoWorkOrder { get { return base.NoWorkOrder; } set { base.NoWorkOrder = value; } }

    }
}

