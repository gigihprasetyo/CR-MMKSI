#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerVehicleDto  class
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
using System;
using System.Collections.Generic;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CustomerVehicleDto : DtoBase
    {
        public int ID { get; set; }

        public int DealerID { get; set; }

        public string DealerCode { get; set; }

        public int Status { get; set; }

        public string ReffCode { get; set; }

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public string Gedung { get; set; }

        public string Alamat { get; set; }

        public string Kelurahan { get; set; }

        public string Kecamatan { get; set; }

        public string PostalCode { get; set; }

        public string PreArea { get; set; }

        public int CityID { get; set; }

        public int CityCode { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public int Status1 { get; set; }

        public int TipePerusahaan { get; set; }

        public DateTime ProcessDate { get; set; }

        public DateTime RequestDate { get; set; }

        public List<CustomerRequestProfileDto> CustomerRequestProfiles { get; set; }
    }
}
