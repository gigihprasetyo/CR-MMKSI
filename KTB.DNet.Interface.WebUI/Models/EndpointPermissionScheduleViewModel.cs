#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndpointPermissionSchedule ViewModel class
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
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class EndpointScheduleViewModel
    {
        [Required]
        public int EndpointPermissionId { get; set; }

        [Required]
        public List<int> ListOfSelectedScheduleId { get; set; }

    }
}