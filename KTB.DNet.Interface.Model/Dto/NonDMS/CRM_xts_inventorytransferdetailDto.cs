#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_inventorytransferdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 02 Sep 2020 08:32:18
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_inventorytransferdetailDto : DtoBase
    {
        public Guid xts_inventorytransferdetailid { get; set; }

		public string ktb_chassisnumber { get; set; }

		public decimal? ktb_cogstrx { get; set; }

		public decimal? ktb_latestpurchaseprice { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public decimal? xts_basequantity { get; set; }

		public Guid xts_frombatchid { get; set; }

		public Guid xts_frombusinessunitid { get; set; }

		public Guid xts_fromconfigurationid { get; set; }

		public Guid xts_fromexteriorcolorid { get; set; }

		public Guid xts_frominteriorcolorid { get; set; }

		public Guid xts_fromlocationid { get; set; }

		public Guid xts_fromserialid { get; set; }

		public Guid xts_fromsiteid { get; set; }

		public Guid xts_fromstyleid { get; set; }

		public Guid xts_fromwarehouseid { get; set; }

		public string xts_inventorytransferdetail { get; set; }

		public Guid xts_inventorytransferid { get; set; }

		public Guid xts_inventoryunitid { get; set; }

		public string xts_productdescription { get; set; }

		public Guid xts_productid { get; set; }

		public decimal? xts_quantity { get; set; }

		public string xts_remarks { get; set; }

		public Guid xts_servicepartsandmaterialid { get; set; }

		public string xts_sourcedata { get; set; }

		public Guid xts_stockid { get; set; }

		public Guid xts_stockinventorynewvehicleid { get; set; }

		public string xts_stocknumberlookupname { get; set; }

		public int? xts_stocknumberlookuptype { get; set; }

		public Guid xts_tobatchid { get; set; }

		public Guid xts_tobusinessunitid { get; set; }

		public Guid xts_toconfigurationid { get; set; }

		public Guid xts_toexteriorcolorid { get; set; }

		public Guid xts_tointeriorcolorid { get; set; }

		public Guid xts_tolocationid { get; set; }

		public Guid xts_toserialid { get; set; }

		public Guid xts_tositeid { get; set; }

		public Guid xts_tostyleid { get; set; }

		public Guid xts_towarehouseid { get; set; }

		public Guid xts_transactionunitid { get; set; }

		public string xts_vehicleidentificationnumber { get; set; }

    }
}
