#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_UnmatchSPKChassisFilterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion


using KTB.DNet.Interface.Resources;
using System.ComponentModel.DataAnnotations;
namespace KTB.DNet.Interface.Model
{
    public class VWI_UnmatchSPKChassisFilterDto : FilterDtoBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string ChassisNumber { get; set; }
    }
}
