#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchasereceiptdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 15:47:31
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_purchasereceiptdetailBL : IBaseInterface<CRM_xts_purchasereceiptdetailParameterDto, CRM_xts_purchasereceiptdetailFilterDto, CRM_xts_purchasereceiptdetailDto>
    {
        ResponseBase<List<CRM_xts_purchasereceiptdetailDto>> ReadList(CRM_xts_purchasereceiptdetailFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_purchasereceiptdetailDto> Create(CRM_xts_purchasereceiptdetailCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_purchasereceiptdetailDto>> BulkCreate(List<CRM_xts_purchasereceiptdetailCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_purchasereceiptdetailDto> Update(CRM_xts_purchasereceiptdetailUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_purchasereceiptdetailDto> Delete(CRM_xts_purchasereceiptdetailDeleteParameterDto paramDelete);
    }
}
