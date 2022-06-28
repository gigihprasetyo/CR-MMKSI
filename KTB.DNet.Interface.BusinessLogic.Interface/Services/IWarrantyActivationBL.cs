#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Warranty Activation interface
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
using System.Collections.Generic;
using System.IO;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IWarrantyActivationBL : IBaseInterface<WarrantyActivationParameterDto, WarrantyActivationFilterDto, WarrantyActivationDto>
    {
        ResponseBase<List<V_WarrantyActivationDto>> Read(V_WarrantyActivationFilterDto filterDto, int pageSize);
        FileStream GetFile(WarrantyActivationGetFileParameter paramGetFile, out string filename);
    }
}
