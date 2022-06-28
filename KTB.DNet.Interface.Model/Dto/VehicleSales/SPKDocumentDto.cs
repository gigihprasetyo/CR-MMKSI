#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDocumentDto  class
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
    public class SPKDocumentDto : DtoBase
    {
        public string SPKNumber { get; set; }
        public string DealerCode { get; set; }
        public AttachmentDto Document { get; set; }
    }
}
