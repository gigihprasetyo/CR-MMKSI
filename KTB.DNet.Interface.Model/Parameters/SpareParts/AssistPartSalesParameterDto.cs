#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartSalesParameterDto  class
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class AssistPartSalesParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        //public int AssistUploadLogID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "dd/MM/yy")]
        public DateTime TglTransaksi { get; set; }

        //[Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //public int DealerID { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string KodeCustomer { get; set; }

        //[Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //public int SalesChannelID { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string SalesChannelCode { get; set; }

        [DefaultValue(0)]
        public int TrTraineeSalesSparepartID { get; set; }

        //public int SalesmanHeaderID { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string KodeSalesman { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string NoWorkOrder { get; set; }

        //[Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //public int SparepartMasterID { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //[AntiXss]
        public string NoParts { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [Display(Name = "QtySales", ResourceType = typeof(FieldResource))]
        public Double Qty { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [Display(Name = "HargaBeli", ResourceType = typeof(FieldResource))]
        public int HargaBeli { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [Display(Name = "HargaJual", ResourceType = typeof(FieldResource))]
        public int HargaJual { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [Display(Name = "IsCampaign", ResourceType = typeof(FieldResource))]
        public Boolean IsCampaign { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string CampaignNo { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string CampaignDescription { get; set; }

        //[Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //public int DealerBranchID { get; set; }

        [AntiXss]
        public string DealerBranchCode { get; set; }

        //public string RemarksSystem { get; set; }

        [DefaultValue(1)]
        public short StatusAktif { get; set; }

        //[Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DefaultValue(1)]
        public short ValidateSystemStatus { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var results = new List<ValidationResult>();

            //if (ID.HasValue && ID <= 0)
            //{
            //    results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ID)));
            //}

            if (!IsCampaign)
            {
                if (!string.IsNullOrEmpty(CampaignDescription) || !string.IsNullOrEmpty(CampaignNo))
                {
                    results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgFlagCampaign)));
                }
            }

            if (this.HargaBeli < 0)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifIntegerSparePart, string.Format(FieldResource.HargaBeli), this.NoParts)));
            }

            if (this.HargaJual < 0)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifIntegerSparePart, string.Format(FieldResource.HargaJual), this.NoParts)));
            }
            //else
            //{
            //    if (string.IsNullOrEmpty(CampaignDescription) || string.IsNullOrEmpty(CampaignNo))
            //    {
            //        results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, MessageResource.ErrorMsgMandatoryField)));
            //    }
            //} 

            return results;
        }
    }

    public class AssistPartSalesCreateParameterDto : AssistPartSalesParameterDto
    {
    }

    public class AssistPartSalesUpdateParameterDto : AssistPartSalesParameterDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int ID { get { return base.ID; } set { base.ID = value; } }
    }
}

