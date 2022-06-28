using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface ISendASBSFIDBL : IBaseInterface<Model.SendASBSFIDParameterDto, SendASBSFIDFilterDto, SendASBSFIDDto>
    {
        ResponseBase<SendASBSFIDDto> SendCustomer(SendASBSFIDParameterDto.CustomerSFIDParameterDto param);
        ResponseBase<SendASBSFIDDto> SendSuspect(SendASBSFIDParameterDto.SuspectSFIDParameterDto param);
        ResponseBase<SendASBSFIDDto> SendProspect(SendASBSFIDParameterDto.SuspectSFIDParameterDto param);
        ResponseBase<SendASBSFIDDto> SendProspectCreate(SendASBSFIDParameterDto.CreateProspect param);
        ResponseBase<SendASBSFIDDto> SendSuspectContact(SendASBSFIDParameterDto.SuspectContactSFIDParameterDto param);
        ResponseBase<SendASBSFIDDto> SendActivity(SendASBSFIDParameterDto.ActivitySFIDParameterDto param);
        ResponseBase<SendASBSFIDDto> SendActivityContact(SendASBSFIDParameterDto.ActivityContactSFIDParameterDto param);
        ResponseBase<SendASBSFIDDto> SendActivitySuspect(SendASBSFIDParameterDto.ActivitySuspectQualifiedSend param);

    }
}
