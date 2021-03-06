#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_newvehicledeliveryorder class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 15:39:05
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_newvehicledeliveryorderBL : IBaseInterface<CRM_xts_newvehicledeliveryorderParameterDto, CRM_xts_newvehicledeliveryorderFilterDto, CRM_xts_newvehicledeliveryorderDto>
    {
        ResponseBase<List<CRM_xts_newvehicledeliveryorderDto>> ReadList(CRM_xts_newvehicledeliveryorderFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_newvehicledeliveryorderDto> Create(CRM_xts_newvehicledeliveryorderCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_newvehicledeliveryorderDto>> BulkCreate(List<CRM_xts_newvehicledeliveryorderCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_newvehicledeliveryorderDto> Update(CRM_xts_newvehicledeliveryorderUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_newvehicledeliveryorderDto> Delete(CRM_xts_newvehicledeliveryorderDeleteParameterDto paramDelete);
    }
}
