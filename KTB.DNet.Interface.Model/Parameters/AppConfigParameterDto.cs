#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AppConfigParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class AppConfigParameterDto : ParameterDtoBase
    {
        [AntiXss]
        public string AppID { get; set; }
        public int ID { get; set; }
        [AntiXss]
        public string Name { get; set; }
        public short Status { get; set; }
        [AntiXss]
        public string Value { get; set; }
    }
}
