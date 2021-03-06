#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_outsourceworkorderreceiptdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 18 Jan 2021 16:16:54
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_outsourceworkorderreceiptdetailBL : IBaseInterface<CRM_xts_outsourceworkorderreceiptdetailParameterDto, CRM_xts_outsourceworkorderreceiptdetailFilterDto, CRM_xts_outsourceworkorderreceiptdetailDto>
    {
        ResponseBase<List<CRM_xts_outsourceworkorderreceiptdetailDto>> ReadList(CRM_xts_outsourceworkorderreceiptdetailFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptdetailDto> Create(CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_outsourceworkorderreceiptdetailDto>> BulkCreate(List<CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptdetailDto> Update(CRM_xts_outsourceworkorderreceiptdetailUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptdetailDto> Delete(CRM_xts_outsourceworkorderreceiptdetailDeleteParameterDto paramDelete);
    }
}
