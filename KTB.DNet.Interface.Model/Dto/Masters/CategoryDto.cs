#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CategoryDto  class
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
    public class CategoryDto : DtoBase
    {
        public int ID { get; set; }

        public string CategoryCode { get; set; }

        public string Description { get; set; }

        ProductCategoryDto ProductCategoryDto { get; set; }
    }
}
