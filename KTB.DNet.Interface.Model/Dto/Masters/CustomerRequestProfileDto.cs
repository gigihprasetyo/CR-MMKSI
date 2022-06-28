#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerRequestProfileDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CustomerRequestProfileDto : DtoBase
    {
        public int ID { get; set; }
        public int CustomerRequestID { get; set; }
        public short ProfileHeaderID { get; set; }
        public short GroupID { get; set; }
        public string ProfileValue { get; set; }

        public List<CustomerRequestProfileHistoryDto> CustomerRequestProfileHistories { get; set; }
    }
}
