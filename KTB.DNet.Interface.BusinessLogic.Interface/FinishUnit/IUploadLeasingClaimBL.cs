using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IDSFLeasingClaimBL : IBaseInterface<DSFLeasingClaimParameterDto, DSFLeasingClaimFilterDto, DSFLeasingClaimDto>
    {
        bool Insert(List<DSFLeasingClaimCreateParameter> dataFromClient, out List<DSFLeasingClaimCreateResponse> dataForResponse);
        FileStream GetFile(string regnumber,string path, out string filename);
        //ResponseBase<List<DSFLeasingClaimDAPPDto>> ReadLeasingDAPP(DSFLeasingClaimDAPPFilterDto filterDto, int pageSize);
        ResponseBase<List<DSFLeasingClaimDAPPDto>> ReadLeasingDAPP(string _status, string _sourceData, string _lastUpdateTime);
        bool Update(List<ResubmitClaimParamater> data, out List<ResubmitClaimResponse> dataForResponse);
    }

    public interface IDSFLeasingClaimDocumentBL : IBaseInterface<DSFLeasingClaimDocumentParameterDto, DSFLeasingClaimDocumentFilterDto, DSFLeasingClaimDocumentDto>
    {
        
    }
}
