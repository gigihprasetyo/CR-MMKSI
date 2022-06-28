#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Rollback ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion


using System.Collections.Generic;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class RollbackViewModel
    {
        public RollbackViewModel()
        {
            JenkinsEnvironments = new List<JenkinsEnvironment>();
        }

        public List<JenkinsEnvironment> JenkinsEnvironments { get; set; }

    }

    public class JenkinsEnvironment
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public List<AppRollbackModel> AppRollbackModels { get; set; }

    }

    public class AppRollbackModel
    {
        public AppRollbackModel()
        {
            FolderList = new List<ExplorerModel>();
        }

        public string AppId { get; set; }

        public string AppName { get; set; }

        public List<ExplorerModel> FolderList { get; set; }
    }
}