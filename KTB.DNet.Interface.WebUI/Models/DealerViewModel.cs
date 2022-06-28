#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Dealer ViewModel class
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
    public class DealerViewModel
    {
        public Int16 Id { get; set; }

        public string DealerCode { get; set; }

        public string DealerName { get; set; }

        public string Status { get; set; }

        public string Title { get; set; }
    }
}