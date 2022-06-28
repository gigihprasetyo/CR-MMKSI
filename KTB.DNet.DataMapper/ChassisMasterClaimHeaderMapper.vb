#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterClaimHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 9/7/2020 - 9:07:00 AM
'//
'// ===========================================================================	
#end region


#region ".NET Base Class Namespace Imports"
imports System
imports System.Data
imports System.Collections
#end region

#region "Custom Namespace Imports"
imports Microsoft.Practices.EnterpriseLibrary.Data
imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
imports Microsoft.Practices.EnterpriseLibrary.Logging
imports KTB.DNet.DataMapper.Framework
imports KTB.DNet.Domain
imports KTB.DNet.Domain.Search
#end region

namespace KTB.DNet.DataMapper

	public class ChassisMasterClaimHeaderMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertChassisMasterClaimHeader"
		private m_UpdateStatement as string = "up_UpdateChassisMasterClaimHeader"
		private m_RetrieveStatement as string = "up_RetrieveChassisMasterClaimHeader"
		private m_RetrieveListStatement as string = "up_RetrieveChassisMasterClaimHeaderList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteChassisMasterClaimHeader"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim ChassisMasterClaimHeader as ChassisMasterClaimHeader = nothing
			while dr.Read
			
				ChassisMasterClaimHeader = me.CreateObject(dr)
			            
			end while        					
			
			return ChassisMasterClaimHeader
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim ChassisMasterClaimHeaderList as ArrayList = new ArrayList
			
			while dr.Read
					dim ChassisMasterClaimHeader as ChassisMasterClaimHeader = me.CreateObject(dr)
					ChassisMasterClaimHeaderList.Add(ChassisMasterClaimHeader)
			end while
			     
			return ChassisMasterClaimHeaderList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim ChassisMasterClaimHeader as ChassisMasterClaimHeader = ctype(obj, ChassisMasterClaimHeader)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,ChassisMasterClaimHeader.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim ChassisMasterClaimHeader as ChassisMasterClaimHeader = ctype(obj, ChassisMasterClaimHeader)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@ReporterIssue",DbType.AnsiString,ChassisMasterClaimHeader.ReporterIssue)
			DbCommandWrapper.AddInParameter("@ClaimDate",DbType.DateTime,ChassisMasterClaimHeader.ClaimDate)
			DbCommandWrapper.AddInParameter("@DealerPIC",DbType.AnsiString,ChassisMasterClaimHeader.DealerPIC)
			DbCommandWrapper.AddInParameter("@ClaimNumber",DbType.AnsiString,ChassisMasterClaimHeader.ClaimNumber)
			DbCommandWrapper.AddInParameter("@StatusID",DbType.Int32,ChassisMasterClaimHeader.StatusID)
			DbCommandWrapper.AddInParameter("@DateOccur",DbType.DateTime,ChassisMasterClaimHeader.DateOccur)
			DbCommandWrapper.AddInParameter("@PlaceOccur",DbType.AnsiString,ChassisMasterClaimHeader.PlaceOccur)
			DbCommandWrapper.AddInParameter("@ResponClaim",DbType.Int32,ChassisMasterClaimHeader.ResponClaim)
			DbCommandWrapper.AddInParameter("@ChassisNumberReplacement",DbType.AnsiString,ChassisMasterClaimHeader.ChassisNumberReplacement)
			DbCommandWrapper.AddInParameter("@ClaimPoint",DbType.AnsiString,ChassisMasterClaimHeader.ClaimPoint)
			DbCommandWrapper.AddInParameter("@Remark",DbType.AnsiString,ChassisMasterClaimHeader.Remark)
			DbCommandWrapper.AddInParameter("@StatusStockDMS",DbType.Int16,ChassisMasterClaimHeader.StatusStockDMS)
			DbCommandWrapper.AddInParameter("@StatusProcessRetur",DbType.Int16,ChassisMasterClaimHeader.StatusProcessRetur)
			DbCommandWrapper.AddInParameter("@IsTransferSAP",DbType.Int16,ChassisMasterClaimHeader.IsTransferSAP)
			DbCommandWrapper.AddInParameter("@RepairEstimationDate",DbType.DateTime,ChassisMasterClaimHeader.RepairEstimationDate)
			DbCommandWrapper.AddInParameter("@CompletionDate",DbType.DateTime,ChassisMasterClaimHeader.CompletionDate)
			DbCommandWrapper.AddInParameter("@Nominal",DbType.Currency,ChassisMasterClaimHeader.Nominal)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, ChassisMasterClaimHeader.TransferDate)
            DbCommandWrapper.AddInParameter("@EngineNumberReplacement", DbType.AnsiString, ChassisMasterClaimHeader.EngineNumberReplacement)
            DbCommandWrapper.AddInParameter("@SORetur", DbType.AnsiString, ChassisMasterClaimHeader.SORetur)
            DbCommandWrapper.AddInParameter("@DORetur", DbType.AnsiString, ChassisMasterClaimHeader.DORetur)
            DbCommandWrapper.AddInParameter("@BillingRetur", DbType.AnsiString, ChassisMasterClaimHeader.BillingRetur)
            DbCommandWrapper.AddInParameter("@SONormalRetur", DbType.AnsiString, ChassisMasterClaimHeader.SONormalRetur)
            DbCommandWrapper.AddInParameter("@DONormalRetur", DbType.AnsiString, ChassisMasterClaimHeader.DONormalRetur)
            DbCommandWrapper.AddInParameter("@BillingNormalRetur", DbType.AnsiString, ChassisMasterClaimHeader.BillingNormalRetur)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ChassisMasterClaimHeader.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateTIme",DbType.DateTime,ChassisMasterClaimHeader.LastUpdateTIme)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,ChassisMasterClaimHeader.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(ChassisMasterClaimHeader.ChassisMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ChassisMasterClaimHeader.Dealer))
            DbCommandWrapper.AddInParameter("@PODestinationID", DbType.Int32, Me.GetRefObject(ChassisMasterClaimHeader.PODestination))
            DbCommandWrapper.AddInParameter("@ChassisPODestinationID", DbType.Int32, Me.GetRefObject(ChassisMasterClaimHeader.ChassisPODestination))
            DbCommandWrapper.AddInParameter("@LogisticCompanyID", DbType.Int32, Me.GetRefObject(ChassisMasterClaimHeader.ChassisMasterLogisticCompany))

			return DbCommandWrapper
			
		end function
	
		protected overrides function GetNewID(byval dbCommandWrapper as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) as integer

			return ctype(dbCommandWrapper.GetParameterValue("@ID"), integer)

		end function
		
		protected overrides function GetPagingRetrieveCommand as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_PagingQuery)
			DbCommandWrapper.AddInParameter("@Table",DbType.String,m_TableName)
			DbCommandWrapper.AddInParameter("@PK",DbType.String,"ID")
							
			return DbCommandWrapper
		
		end function
		
		protected overrides function GetRetrieveCommand as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DinamicQuery)
			DbCommandWrapper.AddInParameter("@sqlQuery",DbType.String,"SELECT " + m_TableName + ".* FROM " + m_TableName)
			
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetRetrieveListParameter as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_RetrieveListStatement)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetRetrieveParameter(byval id as integer) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_RetrieveStatement)
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,id)
				
			return DbCommandWrapper
			
		end function

		protected overrides function GetUpdateParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim ChassisMasterClaimHeader as ChassisMasterClaimHeader = ctype(obj, ChassisMasterClaimHeader)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,ChassisMasterClaimHeader.ID)
			DbCommandWrapper.AddInParameter("@ReporterIssue",DbType.AnsiString,ChassisMasterClaimHeader.ReporterIssue)
			DbCommandWrapper.AddInParameter("@ClaimDate",DbType.DateTime,ChassisMasterClaimHeader.ClaimDate)
			DbCommandWrapper.AddInParameter("@DealerPIC",DbType.AnsiString,ChassisMasterClaimHeader.DealerPIC)
			DbCommandWrapper.AddInParameter("@ClaimNumber",DbType.AnsiString,ChassisMasterClaimHeader.ClaimNumber)
			DbCommandWrapper.AddInParameter("@StatusID",DbType.Int32,ChassisMasterClaimHeader.StatusID)
			DbCommandWrapper.AddInParameter("@DateOccur",DbType.DateTime,ChassisMasterClaimHeader.DateOccur)
			DbCommandWrapper.AddInParameter("@PlaceOccur",DbType.AnsiString,ChassisMasterClaimHeader.PlaceOccur)
			DbCommandWrapper.AddInParameter("@ResponClaim",DbType.Int32,ChassisMasterClaimHeader.ResponClaim)
			DbCommandWrapper.AddInParameter("@ChassisNumberReplacement",DbType.AnsiString,ChassisMasterClaimHeader.ChassisNumberReplacement)
			DbCommandWrapper.AddInParameter("@ClaimPoint",DbType.AnsiString,ChassisMasterClaimHeader.ClaimPoint)
			DbCommandWrapper.AddInParameter("@Remark",DbType.AnsiString,ChassisMasterClaimHeader.Remark)
			DbCommandWrapper.AddInParameter("@StatusStockDMS",DbType.Int16,ChassisMasterClaimHeader.StatusStockDMS)
			DbCommandWrapper.AddInParameter("@StatusProcessRetur",DbType.Int16,ChassisMasterClaimHeader.StatusProcessRetur)
			DbCommandWrapper.AddInParameter("@IsTransferSAP",DbType.Int16,ChassisMasterClaimHeader.IsTransferSAP)
			DbCommandWrapper.AddInParameter("@RepairEstimationDate",DbType.DateTime,ChassisMasterClaimHeader.RepairEstimationDate)
			DbCommandWrapper.AddInParameter("@CompletionDate",DbType.DateTime,ChassisMasterClaimHeader.CompletionDate)
            DbCommandWrapper.AddInParameter("@Nominal", DbType.Currency, ChassisMasterClaimHeader.Nominal)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, ChassisMasterClaimHeader.TransferDate)
            DbCommandWrapper.AddInParameter("@EngineNumberReplacement", DbType.AnsiString, ChassisMasterClaimHeader.EngineNumberReplacement)
            DbCommandWrapper.AddInParameter("@SORetur", DbType.AnsiString, ChassisMasterClaimHeader.SORetur)
            DbCommandWrapper.AddInParameter("@DORetur", DbType.AnsiString, ChassisMasterClaimHeader.DORetur)
            DbCommandWrapper.AddInParameter("@BillingRetur", DbType.AnsiString, ChassisMasterClaimHeader.BillingRetur)
            DbCommandWrapper.AddInParameter("@SONormalRetur", DbType.AnsiString, ChassisMasterClaimHeader.SONormalRetur)
            DbCommandWrapper.AddInParameter("@DONormalRetur", DbType.AnsiString, ChassisMasterClaimHeader.DONormalRetur)
            DbCommandWrapper.AddInParameter("@BillingNormalRetur", DbType.AnsiString, ChassisMasterClaimHeader.BillingNormalRetur)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,ChassisMasterClaimHeader.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,ChassisMasterClaimHeader.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateTIme", DbType.DateTime, DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)

            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(ChassisMasterClaimHeader.ChassisMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ChassisMasterClaimHeader.Dealer))
            DbCommandWrapper.AddInParameter("@PODestinationID", DbType.Int32, Me.GetRefObject(ChassisMasterClaimHeader.PODestination))
            DbCommandWrapper.AddInParameter("@ChassisPODestinationID", DbType.Int32, Me.GetRefObject(ChassisMasterClaimHeader.ChassisPODestination))
            DbCommandWrapper.AddInParameter("@LogisticCompanyID", DbType.Int32, Me.GetRefObject(ChassisMasterClaimHeader.ChassisMasterLogisticCompany))

			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as ChassisMasterClaimHeader
		
			dim ChassisMasterClaimHeader as ChassisMasterClaimHeader = new ChassisMasterClaimHeader
			
			ChassisMasterClaimHeader.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("ReporterIssue")) then ChassisMasterClaimHeader.ReporterIssue = dr("ReporterIssue").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ClaimDate")) then ChassisMasterClaimHeader.ClaimDate = ctype(dr("ClaimDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("DealerPIC")) then ChassisMasterClaimHeader.DealerPIC = dr("DealerPIC").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ClaimNumber")) then ChassisMasterClaimHeader.ClaimNumber = dr("ClaimNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("StatusID")) then ChassisMasterClaimHeader.StatusID = ctype(dr("StatusID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("DateOccur")) then ChassisMasterClaimHeader.DateOccur = ctype(dr("DateOccur"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("PlaceOccur")) then ChassisMasterClaimHeader.PlaceOccur = dr("PlaceOccur").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ResponClaim")) then ChassisMasterClaimHeader.ResponClaim = ctype(dr("ResponClaim"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("ChassisNumberReplacement")) then ChassisMasterClaimHeader.ChassisNumberReplacement = dr("ChassisNumberReplacement").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ClaimPoint")) then ChassisMasterClaimHeader.ClaimPoint = dr("ClaimPoint").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Remark")) then ChassisMasterClaimHeader.Remark = dr("Remark").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("StatusStockDMS")) then ChassisMasterClaimHeader.StatusStockDMS = ctype(dr("StatusStockDMS"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("StatusProcessRetur")) then ChassisMasterClaimHeader.StatusProcessRetur = ctype(dr("StatusProcessRetur"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("IsTransferSAP")) then ChassisMasterClaimHeader.IsTransferSAP = ctype(dr("IsTransferSAP"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("RepairEstimationDate")) then ChassisMasterClaimHeader.RepairEstimationDate = ctype(dr("RepairEstimationDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("CompletionDate")) then ChassisMasterClaimHeader.CompletionDate = ctype(dr("CompletionDate"), DateTime) 
            If Not dr.IsDBNull(dr.GetOrdinal("Nominal")) Then ChassisMasterClaimHeader.Nominal = CType(dr("Nominal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferDate")) Then ChassisMasterClaimHeader.TransferDate = CType(dr("TransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumberReplacement")) Then ChassisMasterClaimHeader.EngineNumberReplacement = dr("EngineNumberReplacement").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SORetur")) Then ChassisMasterClaimHeader.SORetur = dr("SORetur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DORetur")) Then ChassisMasterClaimHeader.DORetur = dr("DORetur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingRetur")) Then ChassisMasterClaimHeader.BillingRetur = dr("BillingRetur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONormalRetur")) Then ChassisMasterClaimHeader.SONormalRetur = dr("SONormalRetur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DONormalRetur")) Then ChassisMasterClaimHeader.DONormalRetur = dr("DONormalRetur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNormalRetur")) Then ChassisMasterClaimHeader.BillingNormalRetur = dr("BillingNormalRetur").ToString
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then ChassisMasterClaimHeader.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then ChassisMasterClaimHeader.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then ChassisMasterClaimHeader.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTIme")) then ChassisMasterClaimHeader.LastUpdateTIme = ctype(dr("LastUpdateTIme"), DateTime) 
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ChassisMasterClaimHeader.LastUpdateBy = dr("LastUpdateBy").ToString


			if not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) then
                ChassisMasterClaimHeader.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
			end if
			if not dr.IsDBNull(dr.GetOrdinal("DealerID")) then
                ChassisMasterClaimHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
			end if
			if not dr.IsDBNull(dr.GetOrdinal("PODestinationID")) then
                ChassisMasterClaimHeader.PODestination = New PODestination(CType(dr("PODestinationID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisPODestinationID")) Then
                ChassisMasterClaimHeader.ChassisPODestination = New PODestination(CType(dr("ChassisPODestinationID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("LogisticCompanyID")) Then
                ChassisMasterClaimHeader.ChassisMasterLogisticCompany = New ChassisMasterLogisticCompany(CType(dr("LogisticCompanyID"), Integer))
            End If

            Return ChassisMasterClaimHeader

		end function
		
		private sub SetTableName()
		
			if not (gettype (ChassisMasterClaimHeader) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(ChassisMasterClaimHeader),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(ChassisMasterClaimHeader).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace
