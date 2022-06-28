#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ResponseBase  class
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
using System.Collections.Generic;
using System.ComponentModel;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class ResponseBase<TObjecResult>
    {
        public bool success { get; set; }

        public int total { get; set; }

        [DefaultValue(-1)]
        public int _id { get; set; }

        public TObjecResult lst { get; set; }

        private List<MessageBase> _messages = new List<MessageBase>();

        public List<MessageBase> messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
            }
        }
    }
}
