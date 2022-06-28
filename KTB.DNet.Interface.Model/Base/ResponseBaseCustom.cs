#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ResponseBase  class
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
using System.Collections.Generic;
using System.ComponentModel;
using KTB.DNET.Interface.Model;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ResponseBaseCustom<TObjectResult> : ResponseBase<TObjectResult>
    {
        public System.Guid _idGuid { get; set; }
    }
}
