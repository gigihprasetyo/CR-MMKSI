#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : VWI_CRM_xjp_vehiclecostinput class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 17 Sep 2021 09:41:46
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_CRM_xjp_vehiclecostinputBL : IBaseInterface<VWI_CRM_xjp_vehiclecostinputParameterDto, VWI_CRM_xjp_vehiclecostinputFilterDto, VWI_CRM_xjp_vehiclecostinputDto>
    {
        ResponseBase<List<VWI_CRM_xjp_vehiclecostinputDto>> ReadList(VWI_CRM_xjp_vehiclecostinputFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
