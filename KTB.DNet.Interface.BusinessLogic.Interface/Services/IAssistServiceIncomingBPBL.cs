#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : AssistServiceIncomingBP IBL class
 SPECIAL NOTES : DNet WebApi Project
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Created on 2021-03-23
 ===========================================================================
*/
#endregion


using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IAssistServiceIncomingBPBL : IBaseInterface<AssistServiceIncomingBPParameterDto, AssistServiceIncomingBPFilterDto, AssistServiceIncomingBPDto>
    {
        ResponseBase<List<AssistServiceIncomingBPDto>> ReadList(AssistServiceIncomingBPFilterDto filterDto, int pageSize);
        ResponseBase<AssistServiceIncomingBPDto> Create(AssistServiceIncomingBPCreateParameterDto objCreate);
        ResponseBase<AssistServiceIncomingBPDto> Update(AssistServiceIncomingBPUpdateParameterDto objCreate);
        ResponseBase<AssistServiceIncomingBPDto> Delete(AssistServiceIncomingBPDeleteParameterDto objDelete);
    }
}
