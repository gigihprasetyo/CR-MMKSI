
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MonthlyDocumentToFakturEvidance Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 29/07/2019 - 9:49:21
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

	public class MonthlyDocumentToFakturEvidanceMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMonthlyDocumentToFakturEvidance"
		private m_UpdateStatement as string = "up_UpdateMonthlyDocumentToFakturEvidance"
		private m_RetrieveStatement as string = "up_RetrieveMonthlyDocumentToFakturEvidance"
		private m_RetrieveListStatement as string = "up_RetrieveMonthlyDocumentToFakturEvidanceList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMonthlyDocumentToFakturEvidance"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim monthlyDocumentToFakturEvidance as MonthlyDocumentToFakturEvidance = nothing
			while dr.Read
			
				monthlyDocumentToFakturEvidance = me.CreateObject(dr)
			            
			end while        					
			
			return monthlyDocumentToFakturEvidance
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim monthlyDocumentToFakturEvidanceList as ArrayList = new ArrayList
			
			while dr.Read
					dim monthlyDocumentToFakturEvidance as MonthlyDocumentToFakturEvidance = me.CreateObject(dr)
					monthlyDocumentToFakturEvidanceList.Add(monthlyDocumentToFakturEvidance)
			end while
			     
			return monthlyDocumentToFakturEvidanceList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim monthlyDocumentToFakturEvidance as MonthlyDocumentToFakturEvidance = ctype(obj, MonthlyDocumentToFakturEvidance)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,monthlyDocumentToFakturEvidance.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim monthlyDocumentToFakturEvidance as MonthlyDocumentToFakturEvidance = ctype(obj, MonthlyDocumentToFakturEvidance)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@FakturNumber",DbType.AnsiString,monthlyDocumentToFakturEvidance.FakturNumber)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, monthlyDocumentToFakturEvidance.UploadDate)
            DbCommandWrapper.AddInParameter("@PlanningTransferDate", DbType.DateTime, monthlyDocumentToFakturEvidance.PlanningTransferDate)
			DbCommandWrapper.AddInParameter("@PaymentDescription",DbType.AnsiString,monthlyDocumentToFakturEvidance.PaymentDescription)
			DbCommandWrapper.AddInParameter("@EvidancePath",DbType.AnsiString,monthlyDocumentToFakturEvidance.EvidancePath)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,monthlyDocumentToFakturEvidance.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,monthlyDocumentToFakturEvidance.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FileNamePath", DbType.AnsiString, monthlyDocumentToFakturEvidance.FileNamePath)

            DbCommandWrapper.AddInParameter("@MonthlyDocumentID", DbType.Int32, Me.GetRefObject(monthlyDocumentToFakturEvidance.MonthlyDocument))

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
		
			dim monthlyDocumentToFakturEvidance as MonthlyDocumentToFakturEvidance = ctype(obj, MonthlyDocumentToFakturEvidance)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,monthlyDocumentToFakturEvidance.ID)
			DbCommandWrapper.AddInParameter("@FakturNumber",DbType.AnsiString,monthlyDocumentToFakturEvidance.FakturNumber)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, monthlyDocumentToFakturEvidance.UploadDate)
            DbCommandWrapper.AddInParameter("@PlanningTransferDate", DbType.DateTime, monthlyDocumentToFakturEvidance.PlanningTransferDate)
			DbCommandWrapper.AddInParameter("@PaymentDescription",DbType.AnsiString,monthlyDocumentToFakturEvidance.PaymentDescription)
			DbCommandWrapper.AddInParameter("@EvidancePath",DbType.AnsiString,monthlyDocumentToFakturEvidance.EvidancePath)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,monthlyDocumentToFakturEvidance.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,monthlyDocumentToFakturEvidance.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
            DbCommandWrapper.AddInParameter("@FileNamePath", DbType.AnsiString, monthlyDocumentToFakturEvidance.FileNamePath)

            DbCommandWrapper.AddInParameter("@MonthlyDocumentID", DbType.Int32, Me.GetRefObject(monthlyDocumentToFakturEvidance.MonthlyDocument))

			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MonthlyDocumentToFakturEvidance
		
			dim monthlyDocumentToFakturEvidance as MonthlyDocumentToFakturEvidance = new MonthlyDocumentToFakturEvidance
			
			monthlyDocumentToFakturEvidance.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("FakturNumber")) then monthlyDocumentToFakturEvidance.FakturNumber = dr("FakturNumber").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then monthlyDocumentToFakturEvidance.UploadDate = CType(dr("UploadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanningTransferDate")) Then monthlyDocumentToFakturEvidance.PlanningTransferDate = CType(dr("PlanningTransferDate"), DateTime)
			if not dr.IsDBNull(dr.GetOrdinal("PaymentDescription")) then monthlyDocumentToFakturEvidance.PaymentDescription = dr("PaymentDescription").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("EvidancePath")) then monthlyDocumentToFakturEvidance.EvidancePath = dr("EvidancePath").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then monthlyDocumentToFakturEvidance.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then monthlyDocumentToFakturEvidance.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then monthlyDocumentToFakturEvidance.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then monthlyDocumentToFakturEvidance.LastUpdateBy = dr("LastUpdateBy").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then monthlyDocumentToFakturEvidance.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePath")) Then monthlyDocumentToFakturEvidance.FileNamePath = dr("FileNamePath").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("MonthlyDocumentID")) Then
                monthlyDocumentToFakturEvidance.MonthlyDocument = New MonthlyDocument(CType(dr("MonthlyDocumentID"), Integer))
            End If

			return monthlyDocumentToFakturEvidance
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MonthlyDocumentToFakturEvidance) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MonthlyDocumentToFakturEvidance),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MonthlyDocumentToFakturEvidance).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

