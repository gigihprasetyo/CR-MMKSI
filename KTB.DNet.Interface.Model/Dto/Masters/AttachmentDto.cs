#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AttachmentDto  class
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
    public class AttachmentDto
    {
        public string FileName { get; set; }
        public string Base64OfStream { get; set; }
    }
}
