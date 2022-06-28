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
    public class DealerCompanyPostModel : DataTablePostModel
    {
        public int ID { get; set; }
        public string DealerCompanyCode { get; set; }
        public string DealerCompanyName { get; set; }
        public int DealerGroupID { get; set; }
    }
}
