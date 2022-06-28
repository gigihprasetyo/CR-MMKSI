#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : SparePartOutstandingOrderDetail class
 SPECIAL NOTES : Generated from database BSIDNET_MMKSI_CR_Sparepart_BO
 GENERATED BY  : Ako
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 13 Jan 2021 10:55:39
 ===========================================================================
*/
#endregion

using System;

namespace KTB.DNet.Interface.Domain
{
    public class SparePartOutstandingOrderDetail
    {
        
		public int ID { get; set; }

		public int? SparePartOutstandingOrderID { get; set; }

		public string PartNumber { get; set; }
		public int? EstimateFillQty { get; set; }

		public string PartName { get; set; }

		public int? OrderQty { get; set; }

		public int? AllocationQty { get; set; }

		public decimal? AllocationAmount { get; set; }

		public int? OpenQty { get; set; }

		public decimal? OpenAmount { get; set; }

		public string SubtitutePartNumber { get; set; }

		public DateTime? EstimateFillDate { get; set; }

		public short? Status { get; set; }

		public string DMSPRNo { get; set; }
		public string PONumber { get; set; }

		public short? RowStatus { get; set; }

		//public string CreatedBy { get; set; }

		//public DateTime? CreatedTime { get; set; }

		//public string LastUpdateBy { get; set; }

		public DateTime? LastUpdateTime { get; set; }

    }
}
