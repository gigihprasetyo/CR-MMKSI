#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_campaignresponse class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 08:58:34
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_campaignresponseBL : IBaseInterface<CRM_campaignresponseParameterDto, CRM_campaignresponseFilterDto, CRM_campaignresponseDto>
    {
        ResponseBase<List<CRM_campaignresponseDto>> ReadList(CRM_campaignresponseFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_campaignresponseDto> Create(CRM_campaignresponseCreateParameterDto paramCreate);
        ResponseBase<List<CRM_campaignresponseDto>> BulkCreate(List<CRM_campaignresponseCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_campaignresponseDto> Update(CRM_campaignresponseUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_campaignresponseDto> Delete(CRM_campaignresponseDeleteParameterDto paramDelete);
    }
}