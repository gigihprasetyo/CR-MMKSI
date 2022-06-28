
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFSynchronizeLog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 4:30:42 PM
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

	public class SFSynchronizeLogMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSFSynchronizeLog"
		private m_UpdateStatement as string = "up_UpdateSFSynchronizeLog"
		private m_RetrieveStatement as string = "up_RetrieveSFSynchronizeLog"
		private m_RetrieveListStatement as string = "up_RetrieveSFSynchronizeLogList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSFSynchronizeLog"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sFSynchronizeLog as SFSynchronizeLog = nothing
			while dr.Read
			
				sFSynchronizeLog = me.CreateObject(dr)
			            
			end while        					
			
			return sFSynchronizeLog
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sFSynchronizeLogList as ArrayList = new ArrayList
			
			while dr.Read
					dim sFSynchronizeLog as SFSynchronizeLog = me.CreateObject(dr)
					sFSynchronizeLogList.Add(sFSynchronizeLog)
			end while
			     
			return sFSynchronizeLogList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sFSynchronizeLog as SFSynchronizeLog = ctype(obj, SFSynchronizeLog)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFSynchronizeLog.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sFSynchronizeLog as SFSynchronizeLog = ctype(obj, SFSynchronizeLog)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@SFStagingLogID", DbType.Int32, sFSynchronizeLog.SFStagingLog.ID)
			DbCommandWrapper.AddInParameter("@TransactionID",DbType.Int32,sFSynchronizeLog.TransactionID)
            'DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFSynchronizeLog.SynchronizeDate)
			DbCommandWrapper.AddInParameter("@IsSuccess",DbType.Boolean,sFSynchronizeLog.IsSuccess)
			DbCommandWrapper.AddInParameter("@ErrorMessage",DbType.AnsiString,sFSynchronizeLog.ErrorMessage)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFSynchronizeLog.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sFSynchronizeLog.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

						
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
		
			dim sFSynchronizeLog as SFSynchronizeLog = ctype(obj, SFSynchronizeLog)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFSynchronizeLog.ID)
            DbCommandWrapper.AddInParameter("@SFStagingLogID", DbType.Int32, sFSynchronizeLog.SFStagingLog.ID)
			DbCommandWrapper.AddInParameter("@TransactionID",DbType.Int32,sFSynchronizeLog.TransactionID)
            'DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFSynchronizeLog.SynchronizeDate)
			DbCommandWrapper.AddInParameter("@IsSuccess",DbType.Boolean,sFSynchronizeLog.IsSuccess)
			DbCommandWrapper.AddInParameter("@ErrorMessage",DbType.AnsiString,sFSynchronizeLog.ErrorMessage)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFSynchronizeLog.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sFSynchronizeLog.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SFSynchronizeLog
		
			dim sFSynchronizeLog as SFSynchronizeLog = new SFSynchronizeLog
			
			sFSynchronizeLog.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("SFStagingLogID")) Then sFSynchronizeLog.SFStagingLog = New SFStagingLog(ID:=CType(dr("SFStagingLogID"), Integer))
			if not dr.IsDBNull(dr.GetOrdinal("TransactionID")) then sFSynchronizeLog.TransactionID = ctype(dr("TransactionID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("SynchronizeDate")) then sFSynchronizeLog.SynchronizeDate = ctype(dr("SynchronizeDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("IsSuccess")) then sFSynchronizeLog.IsSuccess = ctype(dr("IsSuccess"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("ErrorMessage")) then sFSynchronizeLog.ErrorMessage = dr("ErrorMessage").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sFSynchronizeLog.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sFSynchronizeLog.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sFSynchronizeLog.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sFSynchronizeLog.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sFSynchronizeLog.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sFSynchronizeLog
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SFSynchronizeLog) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SFSynchronizeLog),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SFSynchronizeLog).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

