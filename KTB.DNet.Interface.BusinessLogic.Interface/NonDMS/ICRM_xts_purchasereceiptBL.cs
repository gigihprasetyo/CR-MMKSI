#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchasereceipt class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 26 Aug 2020 14:50:37
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_purchasereceiptBL : IBaseInterface<CRM_xts_purchasereceiptParameterDto, CRM_xts_purchasereceiptFilterDto, CRM_xts_purchasereceiptDto>
    {
        ResponseBase<List<CRM_xts_purchasereceiptDto>> ReadList(CRM_xts_purchasereceiptFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_purchasereceiptDto> Create(CRM_xts_purchasereceiptCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_purchasereceiptDto>> BulkCreate(List<CRM_xts_purchasereceiptCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_purchasereceiptDto> Update(CRM_xts_purchasereceiptUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_purchasereceiptDto> Delete(CRM_xts_purchasereceiptDeleteParameterDto paramDelete);
    }
}
