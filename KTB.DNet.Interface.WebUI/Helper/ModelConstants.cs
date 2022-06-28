#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ModelConstants.cs class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

namespace KTB.DNet.Interface.WebUI.Helper
{
    public static class ModelConstants
    {
        public static class AppId
        {
            public const string WebAPI = "WebAPI";
            public const string WebUI = "WebUI";
            public const string WebAuth = "WebAuth";
            public const string DNetUI = "DNetUI";
        }
        public static class BackupFolder
        {
            public const string QAKTB = "QA-KTB";
            public const string QAMMKSI = "QA-MMKSI";
            public const string ProdKTB = "Prod-KTB";
            public const string ProdMMKSI = "Prod-MMKSI";
        }
    }
}