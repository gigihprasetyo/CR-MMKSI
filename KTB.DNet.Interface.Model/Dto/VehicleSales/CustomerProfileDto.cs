#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerProfileDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System.Collections.Generic;

namespace KTB.DNet.Interface.Model
{
    public class CustomerProfileDto : DtoBase
    {
        public int ID { get; set; }

        public ProfileHeaderDto ProfileHeader { get; set; }

        public ProfileGroupDto ProfileGroup { get; set; }

        public CustomerDto Customer { get; set; }

        public string ProfileValue { get; set; }

        public List<CustomerProfileHistoryDto> CustomerProfileHistorys { get; set; }
    }
}
