#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SaveUserPermission ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class SaveUserPermissionViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public List<UserPermissionViewModel> ListOfUserPermission { get; set; }
    }
}