#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_accountreceivablereceipt class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 04 Sep 2020 09:47:00
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_accountreceivablereceiptBL : IBaseInterface<CRM_xts_accountreceivablereceiptParameterDto, CRM_xts_accountreceivablereceiptFilterDto, CRM_xts_accountreceivablereceiptDto>
    {
        ResponseBase<List<CRM_xts_accountreceivablereceiptDto>> ReadList(CRM_xts_accountreceivablereceiptFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_accountreceivablereceiptDto> Create(CRM_xts_accountreceivablereceiptCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_accountreceivablereceiptDto>> BulkCreate(List<CRM_xts_accountreceivablereceiptCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_accountreceivablereceiptDto> Update(CRM_xts_accountreceivablereceiptUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_accountreceivablereceiptDto> Delete(CRM_xts_accountreceivablereceiptDeleteParameterDto paramDelete);
    }
}