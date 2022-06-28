#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerRequestParameterDto  class
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
    public class CustomerRequestParameterDto : ParameterDtoBase, IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int ID { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string RequestNo { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string RefRequestNo { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string RequestType { get; set; }

        public int RequestUserID { get; set; }

        public DateTime RequestDate { get; set; }

        public int Status { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ProcessUserID { get; set; }

        public DateTime ProcessDate { get; set; }

        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string CustomerCode { get; set; }

        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ReffCode { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Name1 { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Name2 { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Name3 { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Alamat { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Kelurahan { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Kecamatan { get; set; }

        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PostalCode { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PreArea { get; set; }

        [StringLength(1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PrintRegion { get; set; }

        [StringLength(30, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PhoneNo { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Email { get; set; }

        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Attachment { get; set; }

        public short Status1 { get; set; }

        public short TipePerusahaan { get; set; }

        public short MCPStatus { get; set; }

        public short LKPPStatus { get; set; }

        public int CityID { get; set; }

        public int DealerID { get; set; }

        public int SPKHeaderID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Utils.IsNOTelpValid(PhoneNo)) { results.Add(new ValidationResult("Phone Number " + MessageResource.ErrorMsgDataInvalid)); }
            if (!Utils.IsEmailValid(Email)) { results.Add(new ValidationResult("Email " + MessageResource.ErrorMsgDataInvalid)); }

            return results;
        }
    }
}
