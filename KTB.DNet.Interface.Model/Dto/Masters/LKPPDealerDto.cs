#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : LKPPDealerDto  class
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
    public class LKPPDealerDto : DtoBase
    {
        public int ID { get; set; }

        public string DealerCode { get; set; }

        //public DealerDto Dealer { get; set; }

        //public LKPPHeaderDto LKPPHeader { get; set; }
    }
}
