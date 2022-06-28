#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_landedcost class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:02:59
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_landedcostBL : IBaseInterface<CRM_xts_landedcostParameterDto, CRM_xts_landedcostFilterDto, CRM_xts_landedcostDto>
    {
        ResponseBase<List<CRM_xts_landedcostDto>> ReadList(CRM_xts_landedcostFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_landedcostDto> Create(CRM_xts_landedcostCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_landedcostDto>> BulkCreate(List<CRM_xts_landedcostCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_landedcostDto> Update(CRM_xts_landedcostUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_landedcostDto> Delete(CRM_xts_landedcostDeleteParameterDto paramDelete);
    }
}
