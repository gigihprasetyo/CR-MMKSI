#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistServiceIncomingBPDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021-10-26
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ServiceMMSDto : DtoBase
    {
        public int? ID { get; set; }

        public string PBU { get; set; }

        public string BU { get; set; }

        public string BU_Branch { get; set; }

        public string WO_No { get; set; }

        public DateTime? WO_Service_Date { get; set; }

        public string ChassisNo { get; set; }

        public string PlateNo { get; set; }

        public DateTime? Next_Estimated_Service_Date { get; set; }

        public string Notes { get; set; }

        public int Status { get; set; }
    }
}
