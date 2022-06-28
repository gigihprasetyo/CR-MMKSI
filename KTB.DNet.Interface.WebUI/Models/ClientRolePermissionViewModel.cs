#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ClientRolePermission ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

using System;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class ClientRolePermissionViewModel
    {
        //public int Id { get; set; }

        //public EndpointPermissionViewModel Permission { get; set; }

        //public int PermissionId { get; set; }

        [Display(Name = "Client")]
        public Guid ClientId { get; set; }

        //[Display(Name = "Role")]
        //public int RoleId { get; set; }

        [Display(Name = "Client Role")]
        public int ClientRoleId { get; set; }

        [Display(Name = "Permissions")]
        public int[] PermissionIds { get; set; }
    }
}