#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_vehicleinformation class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 14:37:07
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_vehicleinformationBL : IBaseInterface<CRM_xts_vehicleinformationParameterDto, CRM_xts_vehicleinformationFilterDto, CRM_xts_vehicleinformationDto>
    {
        ResponseBase<List<CRM_xts_vehicleinformationDto>> ReadList(CRM_xts_vehicleinformationFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_vehicleinformationDto> Create(CRM_xts_vehicleinformationCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_vehicleinformationDto>> BulkCreate(List<CRM_xts_vehicleinformationCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_vehicleinformationDto> Update(CRM_xts_vehicleinformationUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_vehicleinformationDto> Delete(CRM_xts_vehicleinformationDeleteParameterDto paramDelete);
    }
}