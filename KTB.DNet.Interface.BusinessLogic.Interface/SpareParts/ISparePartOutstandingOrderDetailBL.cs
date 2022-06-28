#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : SparePartOutstandingOrderDetail class
 SPECIAL NOTES : Generated from database BSIDNET_MMKSI_CR_Sparepart_BO
 GENERATED BY  : Ako
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 13 Jan 2021 10:55:39
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISparePartOutstandingOrderDetailBL : IBaseInterface<SparePartOutstandingOrderDetailParameterDto, SparePartOutstandingOrderDetailFilterDto, SparePartOutstandingOrderDetailDto>
    {
        ResponseBase<List<SparePartOutstandingOrderDetailDto>> ReadList(SparePartOutstandingOrderDetailFilterDto filterDto, int pageSize);
            }
}
