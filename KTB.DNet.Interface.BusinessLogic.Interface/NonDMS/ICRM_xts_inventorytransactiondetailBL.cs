#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_inventorytransactiondetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 13:46:48
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_inventorytransactiondetailBL : IBaseInterface<CRM_xts_inventorytransactiondetailParameterDto, CRM_xts_inventorytransactiondetailFilterDto, CRM_xts_inventorytransactiondetailDto>
    {
        ResponseBase<List<CRM_xts_inventorytransactiondetailDto>> ReadList(CRM_xts_inventorytransactiondetailFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_inventorytransactiondetailDto> Create(CRM_xts_inventorytransactiondetailCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_inventorytransactiondetailDto>> BulkCreate(List<CRM_xts_inventorytransactiondetailCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_inventorytransactiondetailDto> Update(CRM_xts_inventorytransactiondetailUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_inventorytransactiondetailDto> Delete(CRM_xts_inventorytransactiondetailDeleteParameterDto paramDelete);
    }
}