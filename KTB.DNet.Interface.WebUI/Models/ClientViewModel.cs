#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Client ViewModel class
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class ClientViewModel : BaseViewModel
    {
        [Display(Name = "Id")]
        public Guid ClientId { get; set; }

        [Required]
        [Display(Name = "Nama Client")]
        public string Name { get; set; }

        [Display(Name = "Application Id")]
        [Required]
        public Guid AppId { get; set; }

        public MsApplicationViewModel MsApplication { get; set; }

        public List<EndpointPermissionViewModel> Permissions { get; set; }

        [Display(Name = "Permissions")]
        //[Required(ErrorMessage = "Please select at least one permission")]
        public List<int> ListOfSelectedPermissionId { get; set; }

        [Display(Name = "Permissions")]
        //[Required(ErrorMessage = "Please select at least one item")]
        public List<int> ListOfSelectedRoleId { get; set; }
    }
}