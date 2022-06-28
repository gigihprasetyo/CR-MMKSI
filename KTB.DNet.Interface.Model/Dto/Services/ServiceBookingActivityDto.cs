
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceBookingActivityDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 10/09/2020 3:32
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KTB.DNet.Interface.Model
{
    public class ServiceBookingActivityDto : DtoBase
    {
        public int ID { get; set; }
        public int ServiceBookingID { get; set; }
        public string ServiceTypeID { get; set; }
        public string KindCode { get; set; }
    }
}

