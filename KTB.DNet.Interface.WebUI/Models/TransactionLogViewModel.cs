#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : TransactionLog ViewModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.WebUI.Models
{
    public class TransactionLogViewModel : BaseViewModel
    {
        public long Id { get; set; }

        public string SenderIP { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public string Endpoint { get; set; }

        public string Input { get; set; }

        public string Output { get; set; }

        public string ErrorMessage { get; set; }

        public string ServerEndpoint { get; set; }

        public bool Status { get; set; }

        public string StatusStr { get { return this.Status ? "Success" : "Failed"; } set { } }

        public long? ParentId { get; set; }

        public string DealerCode { get; set; }

        public Guid ClientId { get; set; }

        public Guid AppId { get; set; }

        public bool IsResolved { get; set; }

        public bool IsParentTransaction { get; set; }

        public List<TransactionLogViewModel> ResendLog { get; set; }
    }
}