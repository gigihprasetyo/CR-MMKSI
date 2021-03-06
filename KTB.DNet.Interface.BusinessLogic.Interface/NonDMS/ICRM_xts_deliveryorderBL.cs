#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_deliveryorder class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 15:46:23
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_deliveryorderBL : IBaseInterface<CRM_xts_deliveryorderParameterDto, CRM_xts_deliveryorderFilterDto, CRM_xts_deliveryorderDto>
    {
        ResponseBase<List<CRM_xts_deliveryorderDto>> ReadList(CRM_xts_deliveryorderFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_deliveryorderDto> Create(CRM_xts_deliveryorderCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_deliveryorderDto>> BulkCreate(List<CRM_xts_deliveryorderCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_deliveryorderDto> Update(CRM_xts_deliveryorderUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_deliveryorderDto> Delete(CRM_xts_deliveryorderDeleteParameterDto paramDelete);
    }
}
