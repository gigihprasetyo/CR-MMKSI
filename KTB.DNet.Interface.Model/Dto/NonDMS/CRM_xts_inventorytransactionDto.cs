#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_inventorytransaction class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 11:50:48
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_xts_inventorytransactionDto : DtoBase
    {
        public Guid xts_inventorytransactionid { get; set; }
        public DateTime? ktb_actualreceiptdate { get; set; }

		public string ktb_itemtypefortransaction { get; set; }

		public string ktb_ribbondataproductwarehouse { get; set; }

		public bool? ktb_updatetosparepartstock { get; set; }

		public Guid modifiedby { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid ownerid { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_inventorytransactionnumber { get; set; }

		public Guid xts_inventorytransactionreferenceid { get; set; }

		public Guid xts_inventorytransferid { get; set; }

		public string xts_itemtypefortransaction { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Guid xts_personinchargeid { get; set; }

		public string xts_processcode { get; set; }

		public string xts_sourcedata { get; set; }

		public string xts_status { get; set; }

		public DateTime? xts_transactiondate { get; set; }

		public string xts_transactiontype { get; set; }

		public Guid xts_workorderid { get; set; }

    }
}
