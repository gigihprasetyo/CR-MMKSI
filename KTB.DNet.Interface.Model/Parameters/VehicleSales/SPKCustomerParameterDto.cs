#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKCustomerParameterDto  class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SPKCustomerParameterDto : DtoBase, IValidatableObject
    {

        private string _customerName;
        private string _customerName2;
        private string _alamat;
        private string _hpNo;
        private string _phoneNo;
        private string _kelurahan;
        private string _kecamatan;
        private string _email;
        private string _EMAIL;


        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerName")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //[StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _customerName = value.TrimStart().TrimEnd();
                else
                    _customerName = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerName2")]
        //[StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string CustomerName2
        {
            get { return _customerName2; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _customerName2 = value.TrimStart().TrimEnd();
                else
                    _customerName2 = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "Building")]
        [StringLength(40, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string Gedung { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Address")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(60, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        public string Alamat
        {
            get { return _alamat; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _alamat = value.TrimStart().TrimEnd();
                else
                    _alamat = value;
            }
        }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int BusinessSectorDetailID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CountryCode")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string CountryCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "HpNo")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string HpNo
        {
            get { return _hpNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _hpNo = value.Trim();
                else
                    _hpNo = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "OfficeNo")]
        [StringLength(60, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string OfficeNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "HomeNo")]
        [StringLength(60, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string HomeNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PhoneNumber")]
        [StringLength(30, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgStringLength")]
        [AntiXss]
        public string PhoneNo
        {
            get { return _phoneNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _phoneNo = value.Trim();
                else
                    _phoneNo = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "City")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string CityCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Kelurahan")]
        [StringLength(40, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        public string Kelurahan
        {
            get { return _kelurahan; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _kelurahan = value.TrimStart().TrimEnd();
                else
                    _kelurahan = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "Kecamatan")]
        [StringLength(35, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        public string Kecamatan
        {
            get { return _kecamatan; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _kecamatan = value.TrimStart().TrimEnd();
                else
                    _kecamatan = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerType")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public short TipeCustomer { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CompanyType")]
        public short? TipePerusahaan { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ReffCode")]
        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ReffCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PreArea")]
        [StringLength(20, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        public string PreArea { get; set; }

        [AntiXss]
        public string LKPPReference { get; set; }

        /* Duplicate Attribute with profile attribute */
        public string Email
        {
            get { return _email; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _email = value.Trim();
                else
                    _email = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "KODEPOS")]
        [StringLength(5, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataInvalid")]
        [AntiXss]
        public string PostalCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "BRANCH_MGR")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string BRANCH_MGR { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PART_MGR")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PART_MGR { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "POBOX")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string POBOX { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SERVICE_MGR")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string SERVICE_MGR { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SALES_MGR")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string SALES_MGR { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "KODEPOS")]
        [StringLength(5, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataInvalid")]
        [AntiXss]
        public string KODEPOS { get; set; } // PostalCode (pdf)

        [Display(ResourceType = typeof(FieldResource), Name = "EMAIL")]
        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        public string EMAIL
        {
            get { return _EMAIL; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _EMAIL = value.Trim();
                else
                    _EMAIL = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "JK")]
        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string JK { get; set; } // Jeniskelamin

        [Display(ResourceType = typeof(FieldResource), Name = "NOKTP")]
        [AntiXss]
        public string NOKTP { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "NOTELP")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string NOTELP { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PENDIDIKAN")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PENDIDIKAN { get; set; }

        //public DateTime TGLLAHIR { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int SAPCustomerID { get; set; }

        //[Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string ImagePath { get; set; }
        public OCRParameterDto OCRIdentity { get; set; }

        [DefaultValue(0)]
        public int PrintRegion { get; set; }

        public int TypePerorangan { get; set; }

        public int TypeIdentitas { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // custom validation
            if (!Utils.IsNumeric(KODEPOS)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataMustBeNumeric, FieldResource.PostalCode))); }
            if (!Utils.IsNumeric(PostalCode)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataMustBeNumeric, FieldResource.PostalCode))); }
            if (!Utils.IsNOTelpValid(PhoneNo)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgOfficePhoneNumberInvalid, FieldResource.PhoneNumber))); }
            if (!Utils.IsNOTelpValid(OfficeNo)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgOfficePhoneNumberInvalid, FieldResource.OfficeNumber))); }
            if (!Utils.IsNOTelpValid(HomeNo)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgOfficePhoneNumberInvalid, FieldResource.HomeNo))); }

            if (!Utils.IsAlphaNumeric(ReffCode)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ReffCode))); }
            //if (!Utils.IsAlphanumericForName(CustomerName)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.CustomerName))); }
            //if (!Utils.IsAlphanumericForName(CustomerName2)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.CustomerName2))); }
            //if (!Utils.IsAlphanumericIncludeSpecialCharacter(Gedung)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Building))); }
            //if (!Utils.IsAlphanumericIncludeSpecialCharacter(Alamat)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Address))); }
            //if (!Utils.IsAlphanumericIncludeSpecialCharacter(Kelurahan)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Kelurahan))); }
            //if (!Utils.IsAlphanumericIncludeSpecialCharacter(Kecamatan)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Kecamatan))); }

            // if TipeCustomer == Perusahaan, TipePerusahaan is mandatory
            if (TipeCustomer == 1 && !TipePerusahaan.HasValue) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.CompanyType))); }

            // if TipeCustomer == Perorangan, IdentityType is mandatory
            //if (TipeCustomer == 0 && OCRIdentity == null) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.OCRIdentity))); }

            // if TipeCustomer = Perusahaan , POBOX is mandatory
            if (TipeCustomer == 1 && string.IsNullOrEmpty(POBOX)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.POBOX))); }
            if (TipeCustomer == 0 && string.IsNullOrEmpty(ImagePath)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, "Image Path"))); }

            if (!Utils.IsEmailValid(EMAIL)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.EMAIL))); }

            //if (!Utils.IsNoHPValidSPK(HpNo)) { results.Add(new ValidationResult(MessageResource.ErrorMsgHpNoFormatSPK)); }

            if (!Utils.IsNumeric(HpNo)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.HandPhoneNumber))); }
            //if (CountryCode.Trim() == "62" && HpNo.Substring(0, 1) != "8") { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgInvalidPhoneWithZero, FieldResource.HandPhoneNumber, "Kode Negara '" + CountryCode.Trim() + "'"))); }

            if (!string.IsNullOrEmpty(NOTELP)) { if (!Utils.IsNOTelpValid(NOTELP)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, "NOTELP"))); } }

            if (!string.IsNullOrEmpty(PostalCode)) { if (PostalCode.Length < 5) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgKodePosLength, PostalCode))); } }

            if (!string.IsNullOrEmpty(KODEPOS)) { if (KODEPOS.Length < 5) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgKodePosLength, KODEPOS))); } }
            
            if (PrintRegion != 0 && PrintRegion != 1) { results.Add(new ValidationResult("Input Print Region harus 0 atau 1")); }

            int maxNameLength = 40;
            List<string> splitedName = StringHelper.Split(CustomerName, maxNameLength, " ");
            bool invalidName = splitedName.Count > 2;
            if (!invalidName)
            {
                CustomerName = splitedName.Count > 0 ? splitedName[0] : CustomerName;
                CustomerName2 = (splitedName.Count > 1 ? splitedName[1] : string.Empty) + " " + CustomerName2;

                invalidName = CustomerName2.Length > maxNameLength;

            }


            if (invalidName)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgInvalidTotalLength, "(CustomerName + CustomerName2)", 80)));
            }

            
            Alamat = Alamat == null ? Alamat : Alamat.ToUpper().TrimStart().TrimEnd();
            CustomerName = CustomerName == null ? CustomerName : CustomerName.ToUpper().TrimStart().TrimEnd();
            CustomerName2 = CustomerName2 == null ? CustomerName2 : CustomerName2.ToUpper().TrimStart().TrimEnd();
            Gedung = Gedung == null ? Gedung : Gedung.ToUpper().TrimStart().TrimEnd();
            Kelurahan = Kelurahan == null ? Kelurahan : Kelurahan.ToUpper().TrimStart().TrimEnd();
            Kecamatan = Kecamatan == null ? Kecamatan : Kecamatan.ToUpper().TrimStart().TrimEnd();
            PreArea = PreArea == null ? PreArea : PreArea.ToUpper().TrimStart().TrimEnd();

            return results;
        }
    }
}
