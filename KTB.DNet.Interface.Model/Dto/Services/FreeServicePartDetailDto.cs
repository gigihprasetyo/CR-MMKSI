#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FreeServiceDto  class
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
    public class FreeServicePartDetailDto : DtoBase
    {
        //public int PartID { get; set; }
        public string PartNumber { get; set; }
        public Decimal PartPrice { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal Qty { get; set; }
    }
}

