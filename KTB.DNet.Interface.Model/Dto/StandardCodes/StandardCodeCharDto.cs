#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCodeCharDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion


namespace KTB.DNet.Interface.Model
{
    public class StandardCodeCharDto : DtoBase
    {
        public int ID { get; set; }

        public string Category { get; set; }

        public string ValueId { get; set; }

        public string ValueCode { get; set; }

        public string ValueDesc { get; set; }

        public int Sequence { get; set; }
    }
}
