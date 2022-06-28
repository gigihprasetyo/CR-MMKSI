#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleTypeDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System.Collections;

namespace KTB.DNet.Interface.Model
{
    public class VehicleTypeDto : DtoBase
    {
        public int ID { get; set; }

        public string VehicleTypeCode { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public byte IsVehicleKind1 { get; set; }

        public byte IsVehicleKind2 { get; set; }

        public byte IsVehicleKind3 { get; set; }

        public byte IsVehicleKind4 { get; set; }

        public int MaxTOPDays { get; set; }

        public ArrayList PameranDisplays { get; set; }

        public ArrayList SAPCustomers { get; set; }

        public ArrayList ConditionMasters { get; set; }

        public ArrayList EventSaless { get; set; }

        public string SAPModel { get; set; }

        public CategoryDto Category;

        public ProductCategoryDto ProductCategory;

        public VehicleModelDto VehicleModel;

        public VehicleClassDto VehicleClass;

        public SPLDetailDto SPLDetail;

    }
}
