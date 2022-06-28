#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Deployment ViewModel class
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

namespace KTB.DNet.Interface.WebUI.Models
{
    public class CategorizedJenkinsJobViewModel
    {
        public List<JenkinsJobViewModel> JenkinsJobs { get; set; }

        public string Name { get; set; }
    }
}