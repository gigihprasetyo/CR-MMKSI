#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_newvehiclesalesorder class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 10:12:24
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ICRM_xts_newvehiclesalesorderBL : IBaseInterface<CRM_xts_newvehiclesalesorderParameterDto, CRM_xts_newvehiclesalesorderFilterDto, CRM_xts_newvehiclesalesorderDto>
    {
        ResponseBase<List<CRM_xts_newvehiclesalesorderDto>> ReadList(CRM_xts_newvehiclesalesorderFilterDto filterDto, int pageSize);
        ResponseBaseCustom<CRM_xts_newvehiclesalesorderDto> Create(CRM_xts_newvehiclesalesorderCreateParameterDto paramCreate);
        ResponseBase<List<CRM_xts_newvehiclesalesorderDto>> BulkCreate(List<CRM_xts_newvehiclesalesorderCreateParameterDto> lstObjCreate);
        ResponseBaseCustom<CRM_xts_newvehiclesalesorderDto> Update(CRM_xts_newvehiclesalesorderUpdateParameterDto paramUpdate);
        ResponseBaseCustom<CRM_xts_newvehiclesalesorderDto> Delete(CRM_xts_newvehiclesalesorderDeleteParameterDto paramDelete);
    }
}
