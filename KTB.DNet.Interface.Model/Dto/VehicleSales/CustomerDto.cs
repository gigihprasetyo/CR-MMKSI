#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerDto  class
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
    public class CustomerDto : DtoBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string ReffCode { get; set; }

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public string Name3 { get; set; }

        public string Alamat { get; set; }

        public string Kelurahan { get; set; }

        public string Kecamatan { get; set; }

        public string PostalCode { get; set; }

        public string PreArea { get; set; }

        public string PrintRegion { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string Attachment { get; set; }

        public short Status { get; set; }

        public short DeletionMark { get; set; }

        public string CompleteName { get; set; }

        public CityDto City { get; set; }

        public List<EndCustomerDto> EndCustomers { get; set; }

        public List<CustomerDealerDto> CustomerDealers { get; set; }

        public List<CustomerProfileDto> CustomerProfiles { get; set; }
    }
}
