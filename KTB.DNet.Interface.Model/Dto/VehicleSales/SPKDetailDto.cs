#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetailDto  class
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
    public class SPKDetailDto//:DtoBase
    {
        public int ID { get; set; }
        //public short LineItem { get; set; }
        //public string VehicleTypeCode { get; set; }
        //public string VehicleColorCode { get; set; }
        //public string VehicleColorName { get; set; }
        //public byte Additional { get; set; }
        //public string Remarks { get; set; }
        //public int Quantity { get; set; }
        //public decimal Amount { get; set; }
        //public decimal TotalAmount { get; set; }
        //public string RejectedReason { get; set; }
        //public byte Status { get; set; }       
        public List<SPKDetailCustomerDto> SPKDetailCustomers { get; set; }
    }
}
