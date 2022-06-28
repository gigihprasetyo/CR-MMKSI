#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKFaktur interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Model.Parameters;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISPKFakturBL : IBaseInterface<SPKFakturParameterDto, SPKFakturFilterDto, SPKFakturDto>
    {
        ResponseBase<List<SPKFakturDto>> GetSPKFakturBySpkHeaderID(int spkId);
    }
}
