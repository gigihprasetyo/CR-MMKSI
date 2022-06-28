#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKHeaderDto  class
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
    public class SPKHeaderDto : DtoBase
    {
        public string SPKNumber { get; set; }

        public List<SPKDetailDto> SPKDetails { get; set; }

        public SPKCustomerDto SPKCustomer { get; set; }
    }
}
