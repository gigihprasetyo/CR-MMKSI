#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndpointPermission ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Framework.Enums;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class EndpointPermissionViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Permission name")]
        public string Name { get; set; }

        [Display(Name = "Permission Description")]
        public string Description { get; set; }

        [Display(Name = "Permission Code")]
        public string PermissionCode { get; set; }

        public string URI { get; set; }

        public int EndpointGroup { get; set; }

        public TransactionType EndpointType { get; set; }

        public OperationType OperationType { get; set; }

        public bool IsScheduled { get; set; }
        public bool IsLogEnabled { get; set; }
        public bool IsRuntimeLogEnabled { get; set; }

    }
}
