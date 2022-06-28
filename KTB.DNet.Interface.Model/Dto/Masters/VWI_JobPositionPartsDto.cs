#region Summary
// ===========================================================================
// AUTHOR        : Mitrais Team
// PURPOSE       : VWI_JobPositionParts Dto class.
// SPECIAL NOTES : 
// ---------------------
// Copyright  2019 
// ---------------------
// $History      : $
// Generated on 09/01/2019 - 9:07:31
//
// ===========================================================================	
#endregion

namespace KTB.DNet.Interface.Model
{
    /// <summary>
    /// VWI_JobPositionParts Dto class
    /// </summary>
    public class VWI_JobPositionPartsDto : DtoBase
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string PositionName { get; set; }
        public int ParentID { get; set; }
        public string ParentCode { get; set; }
        public string ParentPositionName { get; set; }
    }
}

