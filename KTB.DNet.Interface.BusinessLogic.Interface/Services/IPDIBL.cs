#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PDI interface
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
using KTB.DNet.Interface.Model.Parameters.Services;
using System.IO;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IPDIBL : IBaseInterface<PDIParameterDto, PDIFilterDto, PDIDto>
    {
        ResponseBase<PDIDto> Delete(PDIDeleteParameterDto paramDelete);
        FileStream GetFile(PDIGetFileParameter paramGetFile, out string filename);
    }
}
