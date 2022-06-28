#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Enum ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models.Common
{
    public class EnumViewModel
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public dynamic Value { get; set; }
    }
}