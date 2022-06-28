#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ClientUser ViewModel class
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
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class ClientUserViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "ClientId")]
        public Guid ClientId { get; set; }

        [Required]
        [Display(Name = "Client")]
        public ClientViewModel Client { get; set; }

        public int UserId { get; set; }
    }
}