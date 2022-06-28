#region Summary
// ===========================================================================
// AUTHOR        : Mitrais Team
// PURPOSE       : VWI_ServiceType Dto class.
// SPECIAL NOTES : 
// ---------------------
// Copyright  2018 
// ---------------------
// $History      : $
// Generated on 28/11/2018 - 11:14:46
//
// ===========================================================================	
#endregion

namespace KTB.DNet.Interface.Model
{
    /// <summary>
    /// VWI_ServiceType Dto class
    /// </summary>
    public class VWI_ServiceTypeDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string ServiceTypeCode { get; set; }
        public string ServiceTypeDescription { get; set; }
        public int Status { get; set; }
    }
}

