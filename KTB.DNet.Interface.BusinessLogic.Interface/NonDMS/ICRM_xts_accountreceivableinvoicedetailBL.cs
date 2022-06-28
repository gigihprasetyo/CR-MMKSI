#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_accountreceivableinvoicedetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 04 Sep 2020 09:14:51
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_accountreceivableinvoicedetailBL : IBaseInterface<CRM_xts_accountreceivableinvoicedetailParameterDto, CRM_xts_accountreceivableinvoicedetailFilterDto, CRM_xts_accountreceivableinvoicedetailDto>
    {
        ResponseBase<List<CRM_xts_accountreceivableinvoicedetailDto>> ReadList(CRM_xts_accountreceivableinvoicedetailFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_accountreceivableinvoicedetailDto> Create(CRM_xts_accountreceivableinvoicedetailCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_accountreceivableinvoicedetailDto>> BulkCreate(List<CRM_xts_accountreceivableinvoicedetailCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_accountreceivableinvoicedetailDto> Update(CRM_xts_accountreceivableinvoicedetailUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_accountreceivableinvoicedetailDto> Delete(CRM_xts_accountreceivableinvoicedetailDeleteParameterDto paramDelete);
    }
}