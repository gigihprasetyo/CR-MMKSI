#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ProductCategoryDto  class
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
    public class ProductCategoryDto : DtoBase
    {
        #region Public Properties

        public short Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string AccountCode { get; set; }

        #endregion
    }
}
