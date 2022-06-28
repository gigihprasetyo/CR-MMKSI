#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartMasterDto  class
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
    public class SparePartMasterDto : ReadDtoBase
    {
        #region Public Properties

        //public int ID { get; set; }

        //public short ProductCategoryID { get; set; }

        public string PartNumber { get; set; }

        public string PartName { get; set; }

        public string AltPartNumber { get; set; }

        public string AltPartName { get; set; }

        public string AltPartNumberReff { get; set; }

        public string AltPartNumberReffName { get; set; }

        public string PartCode { get; set; }

        public string ModelCode { get; set; }

        public string TypeCode { get; set; }

        //public int Stock { get; set; }

        public decimal RetailPrice { get; set; }

        //public string PartStatus { get; set; }
        public string ProductType { get; set; }

        public short Status { get; set; }

        //public short AccessoriesType { get; set; }

        public bool SparePartProductFixedPrice { get; set; }
        #endregion

        #region Custom Properties

        #endregion
    }
}
