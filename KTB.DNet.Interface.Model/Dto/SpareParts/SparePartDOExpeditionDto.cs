#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDOExpeditionDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class SparePartDOExpeditionDto : DtoBase
    {
        public int ID { get; set; }

        public string ExpeditionNo { get; set; }

        public string ExpeditionName { get; set; }

        public DateTime ETA { get; set; }

        public DateTime ETD { get; set; }

        public DateTime ATD { get; set; }

        public DateTime ATA { get; set; }
    }
}
