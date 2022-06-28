#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Role ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class RoleViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; }

        [Display(Name = "Level")]
        public string Level { get; set; }

        public List<EndpointPermissionViewModel> Permissions { get; set; }

        //[Display(Name = "Permissions")]        
        //[Required(ErrorMessage = "Please select at least one item")]
        //public List<int> SelectedPermissionId { get; set; }
    }
}