#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xid_registrationmonitoring class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 17:42:37
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xid_registrationmonitoringBL : IBaseInterface<CRM_xid_registrationmonitoringParameterDto, CRM_xid_registrationmonitoringFilterDto, CRM_xid_registrationmonitoringDto>
    {
        ResponseBase<List<CRM_xid_registrationmonitoringDto>> ReadList(CRM_xid_registrationmonitoringFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xid_registrationmonitoringDto> Create(CRM_xid_registrationmonitoringCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xid_registrationmonitoringDto>> BulkCreate(List<CRM_xid_registrationmonitoringCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xid_registrationmonitoringDto> Update(CRM_xid_registrationmonitoringUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xid_registrationmonitoringDto> Delete(CRM_xid_registrationmonitoringDeleteParameterDto paramDelete);
    }
}
