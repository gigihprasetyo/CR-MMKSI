#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKCustomerProfileDto  class
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
    public class SPKDetailCustomerProfileDto : DtoBase
    {
        public int ID { get; set; }

        public SPKDetailCustomerDto SPKDetailCustomer { get; set; }

        public ProfileGroupDto ProfileGroup { get; set; }

        public ProfileHeaderDto ProfileHeader { get; set; }

        public string ProfileValue { get; set; }
    }
}
