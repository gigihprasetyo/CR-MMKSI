#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_customerpublic class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 26 Aug 2020 14:33:49
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_customerpublicBL : IBaseInterface<CRM_xts_customerpublicParameterDto, CRM_xts_customerpublicFilterDto, CRM_xts_customerpublicDto>
    {
        ResponseBase<List<CRM_xts_customerpublicDto>> ReadList(CRM_xts_customerpublicFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_customerpublicDto> Create(CRM_xts_customerpublicCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_customerpublicDto>> BulkCreate(List<CRM_xts_customerpublicCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_customerpublicDto> Update(CRM_xts_customerpublicUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_customerpublicDto> Delete(CRM_xts_customerpublicDeleteParameterDto paramDelete);
    }
}
