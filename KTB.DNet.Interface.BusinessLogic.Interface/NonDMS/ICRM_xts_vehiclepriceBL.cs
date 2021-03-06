#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_vehicleprice class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 05 Feb 2021 13:44:53
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_vehiclepriceBL : IBaseInterface<CRM_xts_vehiclepriceParameterDto, CRM_xts_vehiclepriceFilterDto, CRM_xts_vehiclepriceDto>
    {
        ResponseBase<List<CRM_xts_vehiclepriceDto>> ReadList(CRM_xts_vehiclepriceFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_vehiclepriceDto> Create(CRM_xts_vehiclepriceCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_vehiclepriceDto>> BulkCreate(List<CRM_xts_vehiclepriceCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_vehiclepriceDto> Update(CRM_xts_vehiclepriceUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_vehiclepriceDto> Delete(CRM_xts_vehiclepriceDeleteParameterDto paramDelete);
    }
}
