#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DataTablePostModel  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using System.Collections.Generic;

namespace KTB.DNet.Interface.Framework
{
    public class DataTablePostModel
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string Search { get; set; }
        public SearchOrder Order { get; set; }
        public Dictionary<object, object> searchParams { get; set; }
    }
}
