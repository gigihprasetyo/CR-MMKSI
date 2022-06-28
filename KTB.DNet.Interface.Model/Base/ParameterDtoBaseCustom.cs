#region Summary
// ===========================================================================
// AUTHOR        : PT ATS
// PURPOSE       : ParameterDtoBaseCustom  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021
// ---------------------
// $History      : $
// Created on 19/10/2021 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Resources;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ParameterDtoBaseCustom : DtoBase
    {
        
        public string UpdatedBy { get; set; }

        public string ResendBy { get; set; }
        public long? LogId { get; set; }
    }
}
