#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_serviceappointment class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:35:16
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_serviceappointmentBL : IBaseInterface<CRM_serviceappointmentParameterDto, CRM_serviceappointmentFilterDto, CRM_serviceappointmentDto>
    {
        ResponseBase<List<CRM_serviceappointmentDto>> ReadList(CRM_serviceappointmentFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_serviceappointmentDto> Create(CRM_serviceappointmentCreateParameterDto paramCreate);
        ResponseBase<List<CRM_serviceappointmentDto>> BulkCreate(List<CRM_serviceappointmentCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_serviceappointmentDto> Update(CRM_serviceappointmentUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_serviceappointmentDto> Delete(CRM_serviceappointmentDeleteParameterDto paramDelete);
    }
}