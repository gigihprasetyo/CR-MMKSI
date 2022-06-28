#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_ktb_kontrabon class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 02 Sep 2020 14:43:44
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_ktb_kontrabonBL : IBaseInterface<CRM_ktb_kontrabonParameterDto, CRM_ktb_kontrabonFilterDto, CRM_ktb_kontrabonDto>
    {
        ResponseBase<List<CRM_ktb_kontrabonDto>> ReadList(CRM_ktb_kontrabonFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_ktb_kontrabonDto> Create(CRM_ktb_kontrabonCreateParameterDto paramCreate);
        ResponseBase<List<CRM_ktb_kontrabonDto>> BulkCreate(List<CRM_ktb_kontrabonCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_ktb_kontrabonDto> Update(CRM_ktb_kontrabonUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_ktb_kontrabonDto> Delete(CRM_ktb_kontrabonDeleteParameterDto paramDelete);
    }
}