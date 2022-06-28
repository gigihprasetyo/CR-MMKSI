#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_workordertimeregister class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 12:25:09
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_workordertimeregisterBL : IBaseInterface<CRM_xts_workordertimeregisterParameterDto, CRM_xts_workordertimeregisterFilterDto, CRM_xts_workordertimeregisterDto>
    {
        ResponseBase<List<CRM_xts_workordertimeregisterDto>> ReadList(CRM_xts_workordertimeregisterFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_workordertimeregisterDto> Create(CRM_xts_workordertimeregisterCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_workordertimeregisterDto>> BulkCreate(List<CRM_xts_workordertimeregisterCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_workordertimeregisterDto> Update(CRM_xts_workordertimeregisterUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_workordertimeregisterDto> Delete(CRM_xts_workordertimeregisterDeleteParameterDto paramDelete);
    }
}
