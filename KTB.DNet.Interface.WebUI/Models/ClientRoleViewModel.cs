#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ClientRole ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class ClientRoleViewModel
    {
        public int Id { get; set; }

        public Guid ClientId { get; set; }

        public int ClientRoleId { get; set; }

        public int[] PermissionIds { get; set; }

        //additional field
        public int RoleId { get; set; }
    }
}