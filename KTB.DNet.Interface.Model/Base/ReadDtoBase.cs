#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ReadDtoBase  class
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
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ReadDtoBase : DtoBase
    {
        public new DateTime LastUpdateTime { get; set; }
    }
}
