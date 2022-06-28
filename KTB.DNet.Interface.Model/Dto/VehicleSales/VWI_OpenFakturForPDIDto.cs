#region Summary
// ===========================================================================
// AUTHOR        : Mitrais Team
// PURPOSE       : VWI_OpenFakturForPDI Dto class.
// SPECIAL NOTES : 
// ---------------------
// Copyright  2019 
// ---------------------
// $History      : $
// Generated on 09/01/2019 - 7:34:49
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    /// <summary>
    /// VWI_OpenFakturForPDI Dto class
    /// </summary>
    public class VWI_OpenFakturForPDIDto : ReadDtoBase
    {
        public int ID { get; set; }
        public DateTime OpenFakturDate { get; set; }
        public int SoldDealerID { get; set; }
        public string DealerCode { get; set; }
        public string ChassisNumber { get; set; }
    }
}

