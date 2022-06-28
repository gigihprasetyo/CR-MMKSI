#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_account class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 17:24:46
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_accountBL : IBaseInterface<CRM_accountParameterDto, CRM_accountFilterDto, CRM_accountDto>
    {
        ResponseBase<List<CRM_accountDto>> ReadList(CRM_accountFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_accountDto> Create(CRM_accountCreateParameterDto paramCreate);
        ResponseBase<List<CRM_accountDto>> BulkCreate(List<CRM_accountCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_accountDto> Update(CRM_accountUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_accountDto> Delete(CRM_accountDeleteParameterDto paramDelete);
    }
}