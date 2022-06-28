#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ThreadLog ViewModel class
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
    public class ThreadLogViewModel
    {
        public long Id { get; set; }

        public long TransactionLogId { get; set; }

        public string MethodName { get; set; }

        public DateTime StartedTime { get; set; }

        public DateTime FinishedTime { get; set; }

        public int ExecutionTime { get; set; }
    }
}