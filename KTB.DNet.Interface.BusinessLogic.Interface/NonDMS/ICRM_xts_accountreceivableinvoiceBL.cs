#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_accountreceivableinvoice class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 16:49:32
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_accountreceivableinvoiceBL : IBaseInterface<CRM_xts_accountreceivableinvoiceParameterDto, CRM_xts_accountreceivableinvoiceFilterDto, CRM_xts_accountreceivableinvoiceDto>
    {
        ResponseBase<List<CRM_xts_accountreceivableinvoiceDto>> ReadList(CRM_xts_accountreceivableinvoiceFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_accountreceivableinvoiceDto> Create(CRM_xts_accountreceivableinvoiceCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_accountreceivableinvoiceDto>> BulkCreate(List<CRM_xts_accountreceivableinvoiceCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_accountreceivableinvoiceDto> Update(CRM_xts_accountreceivableinvoiceUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_accountreceivableinvoiceDto> Delete(CRM_xts_accountreceivableinvoiceDeleteParameterDto paramDelete);
    }
}
