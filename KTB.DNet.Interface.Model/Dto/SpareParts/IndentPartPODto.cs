#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartPODto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using KTB.DNet.Domain;

namespace KTB.DNet.Interface.Model
{
    public class IndentPartPODto : DtoBase
    {
        public int ID { get; set; }
        public IndentPartDetail IndentPartDetail { get; set; }
        public SparePartPODetail SparePartPODetail { get; set; }
    }
}

