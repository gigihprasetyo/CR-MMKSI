#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPK interface
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
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISPKBL : IBaseInterface<SPKHeaderParameterDto, SPKHeaderFilterDto, SPKHeaderDto>
    {
        ResponseBase<SPKDocumentDto> GetSPKDocument(SPKDocumentParameterDto spkDocumentParam);

        ResponseBase<SPKDocumentDto> GetSPKCustomerKTP(SPKDocumentParameterDto spkDocumentParam);

        ResponseBase<List<VWI_SPKCustomerHaveRequestDto>> GetSPKCustomerHaveRequest(VWI_SPKCustomerHaveRequestFilterDto filterDto, int pageSize);

        ResponseBase<List<VWI_SPKTrackingDto>> Read(VWI_SPKTrackingFilterDto filterDto, int pageSize);
    }
}
