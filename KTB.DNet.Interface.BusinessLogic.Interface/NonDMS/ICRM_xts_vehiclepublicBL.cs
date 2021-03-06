#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_vehiclepublic class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 08 Feb 2021 11:39:54
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_vehiclepublicBL : IBaseInterface<CRM_xts_vehiclepublicParameterDto, CRM_xts_vehiclepublicFilterDto, CRM_xts_vehiclepublicDto>
    {
        ResponseBase<List<CRM_xts_vehiclepublicDto>> ReadList(CRM_xts_vehiclepublicFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_vehiclepublicDto> Create(CRM_xts_vehiclepublicCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_vehiclepublicDto>> BulkCreate(List<CRM_xts_vehiclepublicCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_vehiclepublicDto> Update(CRM_xts_vehiclepublicUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_vehiclepublicDto> Delete(CRM_xts_vehiclepublicDeleteParameterDto paramDelete);
    }
}
