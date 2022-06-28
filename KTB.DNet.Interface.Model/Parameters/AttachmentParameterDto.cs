#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AttachmentParameterDto  class
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
using Embarr.WebAPI.AntiXss;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class AttachmentParameterDto
    {
        public string FileName { get; set; }
        [AntiXss]
        public string Base64OfStream { get; set; }
    }
}
