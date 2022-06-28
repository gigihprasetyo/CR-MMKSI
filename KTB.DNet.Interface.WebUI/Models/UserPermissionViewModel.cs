#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : UserPermission ViewModel class
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

namespace KTB.DNet.Interface.WebUI.Models
{
    public class UserPermissionViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "ClientUserId")]
        public int ClientUserId { get; set; }

        [Display(Name = "Permission")]
        public EndpointPermissionViewModel Permission { get; set; }

        [Display(Name = "PermissionId")]
        public int PermissionId { get; set; }

        public bool IsCustomPermission { get; set; }
        public bool IsDismantledPermission { get; set; }
    }
}