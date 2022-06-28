#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_OpenFakturForPDI interface
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

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_OpenFakturForPDIBL : IBaseViewInterface<FilterDtoBase, VWI_OpenFakturForPDIDto>
    {
        ResponseBase<List<VWI_OpenFakturForPDIDto>> Read(VWI_OpenFakturForPDIFilterDto filterDto, int pageSize);
    }
}
