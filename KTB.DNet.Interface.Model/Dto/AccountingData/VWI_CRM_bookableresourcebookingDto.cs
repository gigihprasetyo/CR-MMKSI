#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_bookingstatusDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-08
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_bookableresourcebookingDto : DtoBase
    {
        public Guid bookableresourcebookingid { get; set; }
        public string name { get; set; }
        public string bookingtype { get; set; }
        public Guid bookingstatus { get; set; }
        public Guid resource { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public int duration { get; set; }
        public Guid ownerid { get; set; }
        public Guid owningbusinessunit { get; set; }
        public int statecode { get; set; }
        public int statuscode { get; set; }
        public Guid createdby { get; set; }
        public DateTime createdon { get; set; }
        public Guid modifiedby { get; set; }
        public DateTime modifiedon { get; set; }
        public DateTime LastSyncDate { get; set; }
    }
}
