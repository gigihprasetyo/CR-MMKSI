#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PartShopDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class PartShopDto : DtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public int CityPartID { get; set; }
        public string PartShopCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public byte Status { get; set; }
    }

    public class VPartShopDto
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string CityCode { get; set; }
        public string PartShopCode { get; set; }
        public string OldPartShopCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}

