#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : IVWI_PQRBL  interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021/06/29
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_PQRBL : IBaseInterface<VWI_PQRParameterDto, VWI_PQRFilterDto, VWI_PQRDto>
    {
        ResponseBase<List<VWI_PQRDto>> ReadList(VWI_PQRFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode);
    }
}
