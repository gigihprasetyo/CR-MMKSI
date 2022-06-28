#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_outsourceworkshopconfiguration class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:25:04
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_outsourceworkshopconfigurationBL : IBaseInterface<CRM_xts_outsourceworkshopconfigurationParameterDto, CRM_xts_outsourceworkshopconfigurationFilterDto, CRM_xts_outsourceworkshopconfigurationDto>
    {
        ResponseBase<List<CRM_xts_outsourceworkshopconfigurationDto>> ReadList(CRM_xts_outsourceworkshopconfigurationFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_outsourceworkshopconfigurationDto> Create(CRM_xts_outsourceworkshopconfigurationCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_outsourceworkshopconfigurationDto>> BulkCreate(List<CRM_xts_outsourceworkshopconfigurationCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_outsourceworkshopconfigurationDto> Update(CRM_xts_outsourceworkshopconfigurationUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_outsourceworkshopconfigurationDto> Delete(CRM_xts_outsourceworkshopconfigurationDeleteParameterDto paramDelete);
    }
}