#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_warehouse class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 05 Feb 2021 13:59:12
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_warehouseBL : IBaseInterface<CRM_xts_warehouseParameterDto, CRM_xts_warehouseFilterDto, CRM_xts_warehouseDto>
    {
        ResponseBase<List<CRM_xts_warehouseDto>> ReadList(CRM_xts_warehouseFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_warehouseDto> Create(CRM_xts_warehouseCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_warehouseDto>> BulkCreate(List<CRM_xts_warehouseCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_warehouseDto> Update(CRM_xts_warehouseUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_warehouseDto> Delete(CRM_xts_warehouseDeleteParameterDto paramDelete);
    }
}
