#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_OpenFakturForPDI interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    /// <summary></summary>
    /// <seealso cref="KTB.DNet.Interface.BusinessLogic.Interface.IBaseViewInterface{KTB.DNet.Interface.Model.VWI_ChassisStatusFakturFilterDto, KTB.DNet.Interface.Model.VWI_ChassisStatusFakturDto}" />
    public interface IVWI_ChassisStatusFakturBL : IBaseViewInterface<VWI_ChassisStatusFakturFilterDto, VWI_ChassisStatusFakturDto>
    {
        /// <summary>Reads the specified chassis number.</summary>
        /// <param name="chassisNumber">The chassis number.</param>
        /// <returns></returns>
        ResponseBase<VWI_ChassisStatusFakturDto> Read(string chassisNumber);

        /// <summary>Reads the list.</summary>
        /// <param name="listChassis">The list chassis.</param>
        /// <returns></returns>
        ResponseBase<List<VWI_ChassisStatusFakturDto>> ReadList(List<VWI_ChassisStatusFakturFilterDto> listChassis);

        /// <summary>Reads the list asynchronous.</summary>
        /// <param name="listChassis">The list chassis.</param>
        /// <returns></returns>
        Task<ResponseBase<List<VWI_ChassisStatusFakturDto>>> ReadListAsync(List<VWI_ChassisStatusFakturFilterDto> listChassis);

        /// <summary>Reads the list by last update time.</summary>
        /// <param name="lastUpdateTime">The last update time.</param>
        /// <returns></returns>
        ResponseBase<List<VWI_ChassisStatusFakturDto>> ReadListByLastUpdateTime(DateTime lastUpdateTime);
    }
}
