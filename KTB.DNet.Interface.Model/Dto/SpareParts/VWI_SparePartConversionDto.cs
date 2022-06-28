#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartConversionDto  class
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
    public class VWI_SparePartConversionDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string TypeCode { get; set; }
        public string ModelCode { get; set; }
        public string UOMFrom { get; set; }
        public string UOMTo { get; set; }
        public int Qty { get; set; }
        public string PartNumberReff { get; set; }
        public int Status { get; set; }
        public string ProductType { get; set; }
    }
}

