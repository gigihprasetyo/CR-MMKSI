#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : UserActivity ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Framework.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class UserActivityViewModel
    {
        public long Id { get; set; }

        [Required, MaxLength(256)]
        public string Username { get; set; }

        [MaxLength(256)]
        public string Endpoint { get; set; }

        [Required]
        public UserActivityType Activity { get; set; }

        [Required]
        public DateTime ActivityTime { get; set; }

        public string ActivityDesc { get; set; }

       
        public string DealerCode { get; set; }

        public Guid AppId { get; set; }

    }
}