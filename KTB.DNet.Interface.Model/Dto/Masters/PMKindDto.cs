#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PMKindDto  class
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
    public class PMKindDto : DtoBase
    {
        public byte ID { get; set; }

        public string KindCode { get; set; }

        public int KM { get; set; }

        public string KindDescription { get; set; }
    }
}
