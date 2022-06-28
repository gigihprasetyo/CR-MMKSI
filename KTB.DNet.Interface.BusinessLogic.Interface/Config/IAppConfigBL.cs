#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AppConfig interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 9/11/2018 23:34
//
// ===========================================================================	
#endregion

using KTB.DNet.Domain;
using KTB.DNet.Interface.Model;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IAppConfigBL : IBaseInterface<AppConfigParameterDto, AppConfigFilterDto, AppConfigDto>
    {
        ResponseBase<AppConfigDto> GetByName(string name);

        AppConfig GetConfigByName(string name);

        AppConfig GetConfigByName(string name, string appId);
    }
}
