#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerVehicleParameterDto  class
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
    public class CustomerVehicleParameterDto : ParameterDtoBase, IValidatableObject
    {
        private string _dealercode;
        private string _reffCode;
        private string _name1;
        private string _name2;
        private string _gedung;
        private string _alamat;
        private string _kelurahan;
        private string _kecamatan;
        private string _preArea;
        private string _jenisKelamin;
        private string _pendidikan;
        private string _email;
        private string _phoneNo;

        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "RequestType")]
        public int RequestType { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerCode")]
        [AntiXss]
        public string CustomerCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "RequestNo")]
        [AntiXss]
        public string RequestNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "RefRequestNo")]
        [AntiXss]
        public string RefRequestNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode
        {
            get { return _dealercode; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _dealercode = value.ToUpper();
                else
                    _dealercode = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "Status")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int Status { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ReffCode")]
        [AntiXss]
        public string ReffCode
        {
            get { return _reffCode; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _reffCode = value.ToUpper();
                else
                    _reffCode = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerName")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //[AntiXss]
        public string Name1
        {
            get { return _name1; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name1 = value.ToUpper().TrimEnd().TrimStart();
                else
                    _name1 = value.TrimEnd().TrimStart();
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerName2")]
        //[AntiXss]
        public string Name2
        {
            get { return _name2; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name2 = value.ToUpper();
                else
                    _name2 = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "Building")]
        [StringLength(40, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string Gedung
        {
            get { return _gedung; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _gedung = value.ToUpper();
                else
                    _gedung = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "Address")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(60, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string Alamat
        {
            get { return _alamat; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _alamat = value.ToUpper().TrimEnd().TrimStart();
                else
                    _alamat = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "Kelurahan")]
        //[AntiXss]
        public string Kelurahan
        {
            get { return _kelurahan; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _kelurahan = value.ToUpper().TrimStart().TrimEnd();
                else
                    _kelurahan = value.TrimStart().TrimEnd();
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "Kecamatan")]
        //[AntiXss]
        public string Kecamatan
        {
            get { return _kecamatan; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _kecamatan = value.ToUpper().TrimStart().TrimEnd();
                else
                    _kecamatan = value.TrimStart().TrimEnd();
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "PostalCode")]
        [DefaultValue("00000")]
        [AntiXss]
        public string PostalCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PreArea")]
        [AntiXss]
        public string PreArea
        {
            get { return _preArea; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _preArea = value.ToUpper();
                else
                    _preArea = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "CityCode")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string CityCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CountryCode")]
        [AntiXss]
        public string CountryCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PhoneNumber")]
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

        [Display(ResourceType = typeof(FieldResource), Name = "EMAIL")]
        [AntiXss]
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

        [Display(ResourceType = typeof(FieldResource), Name = "Kategori")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int Status1 { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "TipePerusahaan")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int TipePerusahaan { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //[AntiXss]
        public int PrintRegion { get; set; }

        public int TypePerorangan { get; set; }

        public int TypeIdentitas { get; set; }

        public DateTime TGLLAHIR { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ProcessDate")]
        public DateTime? ProcessDate { get; set; }

        #region CustomerRequestProfile

        [Display(ResourceType = typeof(FieldResource), Name = "Gender")]
        [AntiXss]
        public string JenisKelamin
        {
            get { return _jenisKelamin; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _jenisKelamin = value.ToUpper();
                else
                    _jenisKelamin = value;
            }
        }

        [Display(ResourceType = typeof(FieldResource), Name = "NOKTP")]
        [AntiXss]
        public string KTP { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PENDIDIKAN")]
        [AntiXss]
        public string Pendidikan
        {
            get { return _pendidikan; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _pendidikan = value.ToUpper();
                else
                    _pendidikan = value;
            }
        }

        #endregion

        [Display(ResourceType = typeof(FieldResource), Name = "GroupCategory")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public short KategoriGroup { get; set; }

        public OCRParameterDto OCRIdentity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Return if any errors
            if (results.Count > 0) return results;

            if (Utils.IsDefaultString(Name1)) { results.Add(new ValidationResult(ErrorCode.DataRequiredField + "#" + string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.Name))); }
            //if (!Utils.IsAlphanumericForName(Name1)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.CustomerName))); }
            if (!Utils.IsEmailValid(Email)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.EMAIL))); }
            if (!string.IsNullOrEmpty(PostalCode) && !Utils.IsNumeric(PostalCode)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.PostalCode))); }
            if (!Utils.IsAlphanumericIncludeSpecialCharacter(ReffCode)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.ReffCode))); }
            //if (!Utils.IsAlphanumericIncludeSpecialCharacter(Alamat)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.Address))); }
            //if (!string.IsNullOrEmpty(Kelurahan) && !Utils.IsAlphanumericIncludeSpecialCharacter(Kelurahan)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.Kelurahan))); }
            //if (!string.IsNullOrEmpty(Kecamatan) && !Utils.IsAlphanumericIncludeSpecialCharacter(Kecamatan)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.Kecamatan))); }
            if (!string.IsNullOrEmpty(PhoneNo) && !Utils.IsNumeric(PhoneNo)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.PhoneNumber))); }
            //if (!string.IsNullOrEmpty(Name1) && Name1.Length > 40) { results.Add(new ValidationResult(string.Format(ValidationResource.ValidationName1, FieldResource.Name))); }
            //if (!string.IsNullOrEmpty(Name2) && Name2.Length > 40) { results.Add(new ValidationResult(string.Format(ValidationResource.ValidationName2, FieldResource.Name))); }
            //if (!string.IsNullOrEmpty(Gedung) && Gedung.Length > 40) { results.Add(new ValidationResult(string.Format(ValidationResource.ValidationBuilding, FieldResource.Building))); }
            if (!string.IsNullOrEmpty(PostalCode) && PostalCode.Length != 5) { results.Add(new ValidationResult(string.Format(ValidationResource.ValidationPostalCodeLength, FieldResource.KODEPOS))); }
            if (PrintRegion != 0 && PrintRegion != 1) { results.Add(new ValidationResult("Input Print Region harus 0 atau 1")); }

            //if (CountryCode.Trim() == "62" && PhoneNo.Substring(0, 1) != "8") { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgInvalidPhoneWithZero, FieldResource.PhoneNumber, "Kode Negara '" + CountryCode.Trim() + "'"))); }

            int maxNameLength = 40;
            List<string> splitedName = StringHelper.Split(Name1, maxNameLength, " ");
            bool invalidName = splitedName.Count > 2;
            if (!invalidName)
            {
                Name1 = splitedName.Count > 0 ? splitedName[0] : Name1;
                Name2 = (splitedName.Count > 1 ? splitedName[1] : string.Empty) + " " + Name2;

                invalidName = Name2.Length > maxNameLength;

            }


            if (invalidName)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgInvalidTotalLength, "(Name1 + Name2)", 80)));
            }

            return results;
        }
    }

    // for swagger purpose
    public class CustomerVehicleUpdateParameterDto : ParameterDtoBase, IValidatableObject
    {

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string GUID { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string GUIDUpdate { get; set; }

        [AntiXss]
        public string CustomerNumber { get; set; }

        [AntiXss]
        public string CustomerType { get; set; }

        [AntiXss]
        public string ClassType { get; set; }

        [AntiXss]
        public string LevelData { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string CustomerClass { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public int InterfaceStatus { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string InterfaceMessage { get; set; }

        [AntiXss]
        public Int16 CustomerSubClass { get; set; }

        [AntiXss]
        public string ParentCustomerNo { get; set; }

        [AntiXss]
        public string FirstName { get; set; }

        [AntiXss]
        public string LastName { get; set; }

        [AntiXss]
        public string CountryCode { get; set; }

        [AntiXss]
        public string PhoneNo { get; set; }

        [AntiXss]
        public string OtherPhoneNo { get; set; }

        [AntiXss]
        public string Email { get; set; }

        [AntiXss]
        public string Gedung { get; set; }

        [AntiXss]
        public string Alamat { get; set; }

        [AntiXss]
        public string Kelurahan { get; set; }

        [AntiXss]
        public string Kecamatan { get; set; }

        [AntiXss]
        public string PostalCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string CityCode { get; set; }

        [AntiXss]
        public string POBox { get; set; }

        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        [AntiXss]
        public DateTime BirthDate { get; set; }

        [AntiXss]
        public Int16 IdentificationType { get; set; }

        [AntiXss]
        public string IdentificationNo { get; set; }

        [AntiXss]
        public string NPWPNo { get; set; }

        [AntiXss]
        public string NPWPName { get; set; }

        [AntiXss]
        public string PreArea { get; set; }

        [AntiXss]
        public Int16 PrintRegion { get; set; }

        [AntiXss]
        public bool InterfaceCustSales { get; set; }

        [AntiXss]
        public string Notes { get; set; }

        [AntiXss]
        public string JenisKelamin { get; set; }

        [AntiXss]
        public Int16 Status1 { get; set; }

        [AntiXss]
        public Int16 TipePerusahaan { get; set; }

        [AntiXss]
        public Int16 TypePerorangan { get; set; }

        [AntiXss]
        public Int16 TypeIdentitas { get; set; }

        [AntiXss]
        public string KTP { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        [AntiXss]
        public DateTime TGLLAHIR { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Return if any errors
            if (results.Count > 0) return results;
            if (Status1 == 0 && TypePerorangan==0 && TypeIdentitas==0 && KTP.Length != 16)
            {
                results.Add(new ValidationResult("Panjang No KTP tidak valid"));
            }
            else if (Status1 == 0 && TypePerorangan == 0 && TypeIdentitas == 1 && (KTP.Length < 12 || KTP.Length > 17))
            {
                results.Add(new ValidationResult("Panjang No SIM tidak valid"));
            }
            else if (Status1 == 0 && TypePerorangan == 1 && (TypeIdentitas == 2 || TypeIdentitas == 3) && KTP.Length != 11)
            {
                results.Add(new ValidationResult("PKTPanjang No KITAP/KITAS tidak valid"));
            }
            else if (Status1==1 && (KTP.Length < 9 || KTP.Length > 40))
            {
                results.Add(new ValidationResult("Nomor Identitas tidak valid"));
            }

            
            if(Status1 == 0 && TypePerorangan == 0 && TypeIdentitas == 0)
            {
                var tanggallahir = TGLLAHIR.ToString("ddMMyy");
                if (JenisKelamin == "LK")
                {
                    if(KTP.Substring(6,6)!= tanggallahir)
                    {
                        results.Add(new ValidationResult("No KTP tidak valid"));
                    }
                }
                else if (JenisKelamin=="PR")
                {
                    var dateday = Convert.ToInt32(KTP.Substring(6, 2))-40;
                    var datedayy = dateday.ToString("D2");
                    if (datedayy + KTP.Substring(8, 4) != tanggallahir)
                    {
                        results.Add(new ValidationResult("No KTP tidak valid"));
                    }
                }
            }

            return results;
        }
    }
}
