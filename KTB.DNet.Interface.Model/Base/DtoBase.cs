#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DtoBase  class
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
using KTB.DNet.Interface.Model.CustomAttribute;
using System;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class DtoBase
    {
        [IgnoreDataMember]
        public int RowStatus { get; set; }

        [IgnoreDataMember]
        public string CreatedBy { get; set; }

        [IgnoreDataMember]
        [DateTimeDisplayFormatAttribute]
        public DateTime CreatedTime { get; set; }

        [IgnoreDataMember]
        public string LastUpdateBy { get; set; }

        [IgnoreDataMember]
        public DateTime LastUpdateTime { get; set; }
    }
}
