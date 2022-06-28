
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFStagingLog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 11:33:31 AM
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

	public class SFStagingLogMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSFStagingLog"
		private m_UpdateStatement as string = "up_UpdateSFStagingLog"
		private m_RetrieveStatement as string = "up_RetrieveSFStagingLog"
		private m_RetrieveListStatement as string = "up_RetrieveSFStagingLogList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSFStagingLog"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sFStagingLog as SFStagingLog = nothing
			while dr.Read
			
				sFStagingLog = me.CreateObject(dr)
			            
			end while        					
			
			return sFStagingLog
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sFStagingLogList as ArrayList = new ArrayList
			
			while dr.Read
					dim sFStagingLog as SFStagingLog = me.CreateObject(dr)
					sFStagingLogList.Add(sFStagingLog)
			end while
			     
			return sFStagingLogList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sFStagingLog as SFStagingLog = ctype(obj, SFStagingLog)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFStagingLog.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sFStagingLog as SFStagingLog = ctype(obj, SFStagingLog)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@SFMasterObjectID", DbType.Int32, sFStagingLog.SFMasterObject.ID)
            'DbCommandWrapper.AddInParameter("@TransactionDate",DbType.DateTime,sFStagingLog.TransactionDate)
			DbCommandWrapper.AddInParameter("@IsSuccess",DbType.Boolean,sFStagingLog.IsSuccess)
			DbCommandWrapper.AddInParameter("@ErrorMessage",DbType.AnsiString,sFStagingLog.ErrorMessage)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFStagingLog.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sFStagingLog.LastUpdateBy)
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
		
			dim sFStagingLog as SFStagingLog = ctype(obj, SFStagingLog)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFStagingLog.ID)
            DbCommandWrapper.AddInParameter("@SFMasterObjectID", DbType.Int32, sFStagingLog.SFMasterObject.ID)
            'DbCommandWrapper.AddInParameter("@TransactionDate",DbType.DateTime,sFStagingLog.TransactionDate)
			DbCommandWrapper.AddInParameter("@IsSuccess",DbType.Boolean,sFStagingLog.IsSuccess)
			DbCommandWrapper.AddInParameter("@ErrorMessage",DbType.AnsiString,sFStagingLog.ErrorMessage)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFStagingLog.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sFStagingLog.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SFStagingLog
		
			dim sFStagingLog as SFStagingLog = new SFStagingLog
			
			sFStagingLog.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("SFMasterObjectID")) Then sFStagingLog.SFMasterObject = New SFMasterObject(ID:=CType(dr("SFMasterObjectID"), Integer))
			if not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) then sFStagingLog.TransactionDate = ctype(dr("TransactionDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("IsSuccess")) then sFStagingLog.IsSuccess = ctype(dr("IsSuccess"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("ErrorMessage")) then sFStagingLog.ErrorMessage = dr("ErrorMessage").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sFStagingLog.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sFStagingLog.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sFStagingLog.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sFStagingLog.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sFStagingLog.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sFStagingLog
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SFStagingLog) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SFStagingLog),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SFStagingLog).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

