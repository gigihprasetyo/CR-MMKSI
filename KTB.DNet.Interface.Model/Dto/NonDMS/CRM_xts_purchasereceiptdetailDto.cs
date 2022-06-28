#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchasereceiptdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 15:47:31
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_purchasereceiptdetailDto : DtoBase
    {
        public Guid xts_purchasereceiptdetailid { get; set; }
        public string ktb_categoryno { get; set; }

		public string ktb_deliveryorderno { get; set; }

		public DateTime? ktb_gidate { get; set; }

		public string ktb_materialdescription { get; set; }

		public string ktb_materialnumber { get; set; }

		public decimal? ktb_parkingamount { get; set; }

		public string ktb_productsap { get; set; }

		public string ktb_purchaseorderno { get; set; }

		public string ktb_salesorderno { get; set; }

		public string ktb_updatetosparepartstockname { get; set; }

		public string ktb_vehiclekindcode { get; set; }

		public string ktb_vehiclekinddescription { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public int? statecode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public decimal? xts_basereceivedquantity { get; set; }

		public Guid xts_batchid { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_chassismodel { get; set; }

		public string xts_chassisnumberregister { get; set; }

		public decimal? xts_discountamount { get; set; }

		public decimal? xts_discountpercentage { get; set; }

		public string xts_engineno { get; set; }

		public Guid xts_inventoryunitid { get; set; }

		public string xts_keyno { get; set; }

		public decimal? xts_landedcost { get; set; }

		public Guid xts_locationid { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public bool? xts_processed { get; set; }

		public string xts_productdescription { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public Guid xts_productid { get; set; }

		public Guid xts_productinteriorcolorid { get; set; }

		public string xts_productionyear { get; set; }

		public Guid xts_productstyleid { get; set; }

		public decimal? xts_productvolume { get; set; }

		public decimal? xts_productweight { get; set; }

		public Guid xts_purchaseorderdetailid { get; set; }

		public string xts_purchasereceiptdetail { get; set; }

		public Guid xts_purchasereceiptid { get; set; }

		public Guid xts_purchaseunitid { get; set; }

		public decimal? xts_receivedquantity { get; set; }

		public string xts_referencenumber { get; set; }

		public Guid xts_servicepartandmaterialid { get; set; }

		public Guid xts_siteid { get; set; }

		public Guid xts_stockid { get; set; }

		public decimal? xts_titleregistrationfee { get; set; }

		public decimal? xts_totalamount { get; set; }

		public decimal? xts_totalbaseamount { get; set; }

		public decimal? xts_totalconsumptiontaxamount { get; set; }

		public decimal? xts_totalvolume { get; set; }

		public decimal? xts_totalweight { get; set; }

		public decimal? xts_transactionamount { get; set; }

		public decimal? xts_unitcost { get; set; }

		public Guid xts_warehouseid { get; set; }

		public decimal? xts_withholdingtaxamount { get; set; }

		public Guid xts_withholdingtaxid { get; set; }

    }
}