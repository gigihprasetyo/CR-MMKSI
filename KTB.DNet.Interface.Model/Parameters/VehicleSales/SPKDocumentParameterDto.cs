#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDocumentParameterDto  class
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
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SPKDocumentParameterDto : DtoBase
    {
        [Display(ResourceType = typeof(FieldResource), Name = "SPKNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string SPKNumber { get; set; }
    }
}
