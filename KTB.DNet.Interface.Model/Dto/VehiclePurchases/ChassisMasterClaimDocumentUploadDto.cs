#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterClaimDocumentUpload  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 10/09/2020 3:32
//
// ===========================================================================	
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ChassisMasterClaimDocumentUploadDto : DtoBase
    {
        public int ID { get; set; }
        public int Type { get; set; }
        public string DocRegNumber { get; set; }
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public string Path { get; set; }
    }
}
