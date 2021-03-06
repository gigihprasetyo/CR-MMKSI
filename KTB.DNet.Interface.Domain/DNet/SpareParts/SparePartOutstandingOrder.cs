#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : SparePartOutstandingOrder class
 SPECIAL NOTES : Generated from database BSIDNET_MMKSI_CR_Sparepart_BO
 GENERATED BY  : Ako
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 13 Jan 2021 10:52:07
 ===========================================================================
*/
#endregion

using System;

namespace KTB.DNet.Interface.Domain
{
    public class SparePartOutstandingOrder
    {
        
		public int ID { get; set; }

		public int? SparePartPOID { get; set; }

		public DateTime? PODate { get; set; }

		public DateTime? ValidTo { get; set; }

		public string OrderType { get; set; }

		public string DocumentType { get; set; }
		public string DMSPRNo { get; set; }
		public string PONumber { get; set; }

		public int? IsTransfer { get; set; }
		public short? RowStatus { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedTime { get; set; }

		public string LastUpdateBy { get; set; }

		public DateTime? LastUpdateTime { get; set; }

    }
}
