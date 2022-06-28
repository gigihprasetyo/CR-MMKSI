#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Throttle ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class ThrottleViewModel : BaseViewModel, IThrottleInfo
    {
        public int Id { get; set; }

        [Required]
        public int EndpointId { get; set; }

        [Display(Name = "Endpoint")]
        public EndpointPermissionViewModel Endpoint { get; set; }

        [Required]
        [Display(Name = "Request Limit")]
        public int RequestLimit { get; set; }

        [Required]
        [Display(Name = "Time (second)")]
        public int TimeInSeconds { get; set; }

        [Display(Name = "Enable")]
        public bool Enable { get; set; }

        public string GetURI()
        {
            string uri = string.Empty;
            if (this.Endpoint != null)
                uri = this.Endpoint.URI;
            return uri;
        }
    }
}