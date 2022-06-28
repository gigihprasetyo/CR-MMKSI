#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : OCRKKParameterDto  class
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
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class OCRKKParameterDto : DtoBase
    {
        public AttachmentParameterOCRKKDto IdentityFile { get; set; }
        public List<OCRKKDetailParameterDto> FamilyMembers { get; set; }
    }
}
