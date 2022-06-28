#region Summary
// ===========================================================================
// AUTHOR        : Mitrais Team
// PURPOSE       : VWI_ServicePlace Dto class.
// SPECIAL NOTES : 
// ---------------------
// Copyright  2018 
// ---------------------
// $History      : $
// Generated on 28/11/2018 - 11:13:18
//
// ===========================================================================	
#endregion

namespace KTB.DNet.Interface.Model
{
    /// <summary>
    /// VWI_ServicePlace Dto class
    /// </summary>
    public class VWI_ServicePlaceDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string ServicePlaceCode { get; set; }
        public string ServicePlaceDescription { get; set; }
        public int Status { get; set; }
    }
}

