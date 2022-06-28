#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_VehicleInformation interface
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
    public interface IVWI_VehicleInformationBL : IBaseViewInterface<VWI_VehicleInformationFilterDto, VWI_VehicleInformationDto>
    {
        ResponseBase<List<VWI_VehicleInformationDto>> ReadWithView(VWI_VehicleInformationFilterDto filterDto, int pageSize);
    }
}
