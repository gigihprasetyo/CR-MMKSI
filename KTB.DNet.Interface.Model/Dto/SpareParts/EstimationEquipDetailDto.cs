#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipDetailDto  class
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
using KTB.DNet.Interface.Model.CustomAttribute;
using System;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class EstimationEquipDetailDto : DtoBase
    {
        public int ID { get; set; }

        public int EstimationEquipHeaderID { get; set; }

        public int SparePartMasterID { get; set; }

        public Decimal Harga { get; set; }

        public Decimal Discount { get; set; }

        public int TotalForecast { get; set; }

        public int EstimationUnit { get; set; }

        public int Status { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime ConfirmedDate { get; set; }

        public string Remark { get; set; }
    }
}

