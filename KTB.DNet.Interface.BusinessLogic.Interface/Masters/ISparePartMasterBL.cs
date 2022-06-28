#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartMaster interface
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

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISparePartMasterBL : IBaseInterface<SparePartMasterParameterDto, SparePartMasterFilterDto, SparePartMasterDto>
    {
        //ResponseBase<SparePartMasterDto> GetByPartNumberAndModelCode(string parktNumber, string modelCode);
        //ResponseBase<SparePartMasterDto> GetByPartNumber(string partNumber);
    }
}
