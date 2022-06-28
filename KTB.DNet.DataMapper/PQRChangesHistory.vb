#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRChangesHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 2:22:21 PM
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

	public class PQRChangesHistoryMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertPQRChangesHistory"
		private m_UpdateStatement as string = "up_UpdatePQRChangesHistory"
		private m_RetrieveStatement as string = "up_RetrievePQRChangesHistory"
		private m_RetrieveListStatement as string = "up_RetrievePQRChangesHistoryList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeletePQRChangesHistory"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim pQRChangesHistory as PQRChangesHistory = nothing
			while dr.Read
			
				pQRChangesHistory = me.CreateObject(dr)
			            
			end while        					
			
			return pQRChangesHistory
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim pQRChangesHistoryList as ArrayList = new ArrayList
			
			while dr.Read
					dim pQRChangesHistory as PQRChangesHistory = me.CreateObject(dr)
					pQRChangesHistoryList.Add(pQRChangesHistory)
			end while
			     
			return pQRChangesHistoryList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim pQRChangesHistory as PQRChangesHistory = ctype(obj, PQRChangesHistory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pQRChangesHistory.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim pQRChangesHistory as PQRChangesHistory = ctype(obj, PQRChangesHistory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,0)
			DbCommandWrapper.AddInParameter("@DocumentType",DbType.AnsiString,pQRChangesHistory.DocumentType)
			DbCommandWrapper.AddInParameter("@OldStatus",DbType.AnsiString,pQRChangesHistory.OldStatus)
			DbCommandWrapper.AddInParameter("@NewStatus",DbType.AnsiString,pQRChangesHistory.NewStatus)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pQRChangesHistory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,pQRChangesHistory.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

			DbCommandWrapper.AddInParameter("@PQRHeaderID",DbType.Int32,pQRChangesHistory.PQRHeaderID)
						
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
		
			dim pQRChangesHistory as PQRChangesHistory = ctype(obj, PQRChangesHistory)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pQRChangesHistory.ID)
			DbCommandWrapper.AddInParameter("@DocumentType",DbType.AnsiString,pQRChangesHistory.DocumentType)
			DbCommandWrapper.AddInParameter("@OldStatus",DbType.AnsiString,pQRChangesHistory.OldStatus)
			DbCommandWrapper.AddInParameter("@NewStatus",DbType.AnsiString,pQRChangesHistory.NewStatus)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pQRChangesHistory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,pQRChangesHistory.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
			DbCommandWrapper.AddInParameter("@PQRHeaderID",DbType.Int32,pQRChangesHistory.PQRHeaderID)
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as PQRChangesHistory
		
			dim pQRChangesHistory as PQRChangesHistory = new PQRChangesHistory
			
			pQRChangesHistory.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("DocumentType")) then pQRChangesHistory.DocumentType = dr("DocumentType").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("OldStatus")) then pQRChangesHistory.OldStatus = dr("OldStatus").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("NewStatus")) then pQRChangesHistory.NewStatus = dr("NewStatus").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then pQRChangesHistory.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then pQRChangesHistory.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then pQRChangesHistory.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then pQRChangesHistory.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then pQRChangesHistory.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("PQRHeaderID")) then
				pQRChangesHistory.PQRHeaderID = ctype(dr("PQRHeaderID"), integer)
			end if
			
			return pQRChangesHistory
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (PQRChangesHistory) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(PQRChangesHistory),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(PQRChangesHistory).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

