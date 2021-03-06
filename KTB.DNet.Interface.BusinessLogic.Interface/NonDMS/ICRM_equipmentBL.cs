#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_equipment class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 18 Jan 2021 16:07:58
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_equipmentBL : IBaseInterface<CRM_equipmentParameterDto, CRM_equipmentFilterDto, CRM_equipmentDto>
    {
        ResponseBase<List<CRM_equipmentDto>> ReadList(CRM_equipmentFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_equipmentDto> Create(CRM_equipmentCreateParameterDto paramCreate);
        ResponseBase<List<CRM_equipmentDto>> BulkCreate(List<CRM_equipmentCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_equipmentDto> Update(CRM_equipmentUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_equipmentDto> Delete(CRM_equipmentDeleteParameterDto paramDelete);
    }
}
