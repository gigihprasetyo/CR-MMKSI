#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_inventorytransferdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 02 Sep 2020 08:32:18
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_inventorytransferdetailBL : IBaseInterface<CRM_xts_inventorytransferdetailParameterDto, CRM_xts_inventorytransferdetailFilterDto, CRM_xts_inventorytransferdetailDto>
    {
        ResponseBase<List<CRM_xts_inventorytransferdetailDto>> ReadList(CRM_xts_inventorytransferdetailFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_inventorytransferdetailDto> Create(CRM_xts_inventorytransferdetailCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_inventorytransferdetailDto>> BulkCreate(List<CRM_xts_inventorytransferdetailCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_inventorytransferdetailDto> Update(CRM_xts_inventorytransferdetailUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_inventorytransferdetailDto> Delete(CRM_xts_inventorytransferdetailDeleteParameterDto paramDelete);
    }
}