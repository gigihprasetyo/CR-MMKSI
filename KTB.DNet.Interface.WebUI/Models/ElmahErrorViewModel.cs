#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Role ViewModel class
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

namespace KTB.DNet.Interface.WebUI.Models
{
    public class ElmahErrorViewModel
    {
        public Guid ErrorId { get; set; }
        public string Application { get; set; }
        public string Host { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public int StatusCode { get; set; }
        public DateTime TimeUtc { get; set; }
        public DateTime TimeLocal { get; set; }
        public int Sequence { get; set; }
        public string Verb { get; set; }
        public string URL { get; set; }
    }
}