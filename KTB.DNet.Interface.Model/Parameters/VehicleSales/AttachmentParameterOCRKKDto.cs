#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : AttachmentParameterOCRKKDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 12/08/2021 
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class AttachmentParameterOCRKKDto
    {
        public string FileName { get; set; }
        [AntiXss]
        public string Base64OfStream { get; set; }
        public string ImagePath { get; set; }
        public string ImageID { get; set; }
    }
}
