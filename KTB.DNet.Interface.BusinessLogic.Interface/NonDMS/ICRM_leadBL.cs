#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_lead class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 14:17:25
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_leadBL : IBaseInterface<CRM_leadParameterDto, CRM_leadFilterDto, CRM_leadDto>
    {
        ResponseBase<List<CRM_leadDto>> ReadList(CRM_leadFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_leadDto> Create(CRM_leadCreateParameterDto paramCreate);
        ResponseBase<List<CRM_leadDto>> BulkCreate(List<CRM_leadCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_leadDto> Update(CRM_leadUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_leadDto> Delete(CRM_leadDeleteParameterDto paramDelete);
    }
}
