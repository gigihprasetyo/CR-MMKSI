#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKFakturDto  class
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
    public class SPKFakturDto : DtoBase
    {
        public int ID { get; set; }

        public SPKHeaderDto SPKHeader { get; set; }

        public EndCustomerDto EndCustomer { get; set; }
    }
}
