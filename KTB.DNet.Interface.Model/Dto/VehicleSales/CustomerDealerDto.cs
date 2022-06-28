#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerDealerDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion


namespace KTB.DNet.Interface.Model
{
    public class CustomerDealerDto : DtoBase
    {
        public int ID { get; set; }

        public DealerDto Dealer { get; set; }

        public CustomerDto Customer { get; set; }
    }
}
