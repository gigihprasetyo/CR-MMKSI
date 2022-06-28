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

namespace KTB.DNet.Interface.WebUI.Models
{
    public class MsAppVersionViewModel : BaseViewModel
    {
        public int VersionId { get; set; }

        public Guid AppId { get; set; }

        public virtual MsApplicationViewModel MsApplication { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public bool IsCurrentDeployment { get; set; }
    }
}