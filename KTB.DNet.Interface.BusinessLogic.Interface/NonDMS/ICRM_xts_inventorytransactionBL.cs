#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_inventorytransaction class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 11:50:48
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_inventorytransactionBL : IBaseInterface<CRM_xts_inventorytransactionParameterDto, CRM_xts_inventorytransactionFilterDto, CRM_xts_inventorytransactionDto>
    {
        ResponseBase<List<CRM_xts_inventorytransactionDto>> ReadList(CRM_xts_inventorytransactionFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_inventorytransactionDto> Create(CRM_xts_inventorytransactionCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_inventorytransactionDto>> BulkCreate(List<CRM_xts_inventorytransactionCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_inventorytransactionDto> Update(CRM_xts_inventorytransactionUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_inventorytransactionDto> Delete(CRM_xts_inventorytransactionDeleteParameterDto paramDelete);
    }
}
