#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SaveUserRole ViewModel class
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
    public class SaveUserRoleViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public List<int> ListOfRoleId { get; set; }

    }
}