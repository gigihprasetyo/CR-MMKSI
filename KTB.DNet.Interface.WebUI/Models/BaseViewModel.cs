#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Base ViewModel class
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
    public class BaseViewModel
    {
        public string CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; }
    }
}