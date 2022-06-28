#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MsApplication ViewModel class
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class MsApplicationViewModel
    {
        [Display(Name = "AppId")]
        public Guid AppId { get; set; }

        [Required]
        //[Display(Name = "Nama Client")]
        public string Name { get; set; }

        [Display(Name = "Deployment Jenkins Job")]
        public string DeploymentJenkinsJobName { get; set; }

        [Display(Name = "Deployment Backup Folder")]
        public string DeploymentBackupFolder { get; set; }

        public List<EndpointPermissionViewModel> Permissions { get; set; }

        [Display(Name = "Permissions")]
        public List<int> ListOfSelectedPermissionId { get; set; }
    }
}