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
    public class ServiceReminderFollowUpDto : DtoBase
    {
        public int ID { get; set; }
        public int ServiceReminderID { get; set; }
        public Byte FollowUpStatus { get; set; }
        public string FollowUpAction { get; set; }
        public DateTime FollowUpDate { get; set; }
        public DateTime BookingDate { get; set; }
        public string WorkOrderNumber { get; set; }

    }
}

