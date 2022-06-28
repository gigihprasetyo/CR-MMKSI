#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventorytransferDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/9/2020 2:44:55 PM
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_inventorytransferDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_transactiontypename { get; set; }

		public string xts_transferstatus { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public Guid xts_tositeid { get; set; }

		public string xts_referencenumber { get; set; }

		public string xts_receiptidname { get; set; }

		public string statuscodename { get; set; }

		public string xts_status { get; set; }

		public string modifiedbyyominame { get; set; }

		public Guid xts_inventorytransferid { get; set; }

		public string owneridtype { get; set; }

		public Guid xts_receiptid { get; set; }

		public string xts_log { get; set; }

		public bool ktb_isclaim { get; set; }

		public Guid xts_personinchargeid { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string modifiedbyname { get; set; }

		public bool xts_transferstep { get; set; }

		public string ktb_purchasereceiptidname { get; set; }

		public string owneridname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string xts_inventorytransfernumber { get; set; }

		public string xts_searchvehicle { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string xts_workorderidname { get; set; }

		public DateTime xts_receiptdate { get; set; }

		public Guid owningteam { get; set; }

		public string ktb_isclaimname { get; set; }

		public int statecode { get; set; }

		public string xts_tositeidname { get; set; }

		public string xts_transactiontype { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string xts_itemtypefortransfer { get; set; }

		public Guid ktb_parentbusinessunitid { get; set; }

		public string xts_locking { get; set; }

		public string xts_handling { get; set; }

		public Guid xts_fromsiteid { get; set; }

		public DateTime ktb_actualreceiptdate { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string createdbyyominame { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string xts_statusname { get; set; }

		public DateTime ktb_actualtransferdate { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string xts_itemtypefortransfername { get; set; }

		public string xts_personinchargeidname { get; set; }

		public Guid xts_tobusinessunitid { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string xts_fromsiteidname { get; set; }

		public Guid xts_workorderid { get; set; }

		public string createdbyname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public Guid ktb_purchasereceiptid { get; set; }

		public string ktb_ribbondataproductwarehouse { get; set; }

		public string xts_handlingname { get; set; }

		public string xts_sourcedata { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_transferstepname { get; set; }

		public Guid owninguser { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string xts_tobusinessunitidname { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string ktb_parentbusinessunitidname { get; set; }

		public string ktb_description { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public string xts_transferstatusname { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
