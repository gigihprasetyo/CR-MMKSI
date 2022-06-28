#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xjp_predeliveryinspection class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 10:06:07
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xjp_predeliveryinspectionBL : IBaseInterface<CRM_xjp_predeliveryinspectionParameterDto, CRM_xjp_predeliveryinspectionFilterDto, CRM_xjp_predeliveryinspectionDto>
    {
        ResponseBase<List<CRM_xjp_predeliveryinspectionDto>> ReadList(CRM_xjp_predeliveryinspectionFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xjp_predeliveryinspectionDto> Create(CRM_xjp_predeliveryinspectionCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xjp_predeliveryinspectionDto>> BulkCreate(List<CRM_xjp_predeliveryinspectionCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xjp_predeliveryinspectionDto> Update(CRM_xjp_predeliveryinspectionUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xjp_predeliveryinspectionDto> Delete(CRM_xjp_predeliveryinspectionDeleteParameterDto paramDelete);
    }
}