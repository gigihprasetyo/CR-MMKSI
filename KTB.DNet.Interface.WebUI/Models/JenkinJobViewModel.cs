#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : JenkinJob ViewModel class
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
    public class JenkinsJobViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string BuildNumber { get; set; }

        public string LastSuccess { get; set; }

        public string LastFailure { get; set; }

        public Dictionary<string, string> Parameter { get; set; }

        //public Build LastBuild { get; set; }
        //public Build LastFailedBuild { get; set; }
        //public Build LastSuccessfulBuild { get; set; }
        //public Build nextBuildNumber { get; set; }

        public int LastSuccessfulBuildNumber { get; set; }
        public string LastSuccessfulBuildTimestamp { get; set; }
        public int LastFailedBuildNumber { get; set; }
        public string LastFailedBuildTimestamp { get; set; }

    }
}