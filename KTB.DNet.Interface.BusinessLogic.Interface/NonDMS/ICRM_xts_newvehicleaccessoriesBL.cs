#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_newvehicleaccessories class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 20:01:32
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_newvehicleaccessoriesBL : IBaseInterface<CRM_xts_newvehicleaccessoriesParameterDto, CRM_xts_newvehicleaccessoriesFilterDto, CRM_xts_newvehicleaccessoriesDto>
    {
        ResponseBase<List<CRM_xts_newvehicleaccessoriesDto>> ReadList(CRM_xts_newvehicleaccessoriesFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_newvehicleaccessoriesDto> Create(CRM_xts_newvehicleaccessoriesCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_newvehicleaccessoriesDto>> BulkCreate(List<CRM_xts_newvehicleaccessoriesCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_newvehicleaccessoriesDto> Update(CRM_xts_newvehicleaccessoriesUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_newvehicleaccessoriesDto> Delete(CRM_xts_newvehicleaccessoriesDeleteParameterDto paramDelete);
    }
}
