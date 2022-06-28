#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Home ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Framework;
using System.Collections.Generic;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            TopApiList = new List<TopApiModel>();
            LatestErrors = new List<ErrorModel>();
            LatestTrans = new List<TransModel>();
        }

        public string RoleName { get; set; }

        public int UserCount { get; set; }

        public int RoleCount { get; set; }

        public int ClientCount { get; set; }

        public int PermissionCount { get; set; }

        public int DealerCount { get; set; }

        public List<TopApiModel> TopApiList { get; set; }

        public List<ErrorModel> LatestErrors { get; set; }

        public List<TransModel> LatestTrans { get; set; }

        public bool IsShowTopApiRank { get; set; }

        public bool IsShowFailedTrans { get; set; }

        public bool IsShowLatestTrans { get; set; }

        public bool IsDMSAdmin { get; set; }
    }

    public class ErrorModel
    {
        public long ID { get; set; }
        public string DealerCode { get; set; }
        public string Endpoint { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorTime { get; set; }
        public bool IsResolved { get; set; }
    }

    public class TransModel
    {
        public long ID { get; set; }
        public string Endpoint { get; set; }
        public string DealerCode { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedTime { get; set; }
        public string Status { get; set; }
        public bool IsResolved { get; set; }
    }
}