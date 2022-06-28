#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistServiceIncomingDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_MobileServiceReminderDto
    {
        public string DealerCode { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime ServiceReminderDate { get; set; }
        public string KindCode { get; set; }
        public string KindDescription { get; set; }
        public string Remark { get; set; }
        public int ReminderType { get; set; }
        public int ReminderDelta { get; set; }
    }
}
