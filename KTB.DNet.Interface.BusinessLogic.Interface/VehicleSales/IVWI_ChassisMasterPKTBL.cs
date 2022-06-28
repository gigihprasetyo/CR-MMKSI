#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_ChassisMasterPKT interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-11 17:29:00
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IVWI_ChassisMasterPKTBL : IBaseInterface<ChassisMasterPKTParameterDto, ChassisMasterPKTFilterDto, ChassisMasterPKTDto>
    {
        ResponseBase<List<ChassisMasterPKTDto>> ReadList(ChassisMasterPKTFilterDto filterDto, int pageSize, List<Dealer> listDealer, string dealerCompanyCode, string ConnectionString);
    }
}