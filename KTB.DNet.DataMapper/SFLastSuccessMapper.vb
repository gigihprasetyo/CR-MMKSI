
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFLastSuccess Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/05/2018 - 7:59:48 AM
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

	public class SFLastSuccessMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSFLastSuccess"
		private m_UpdateStatement as string = "up_UpdateSFLastSuccess"
		private m_RetrieveStatement as string = "up_RetrieveSFLastSuccess"
		private m_RetrieveListStatement as string = "up_RetrieveSFLastSuccessList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSFLastSuccess"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sFLastSuccess as SFLastSuccess = nothing
			while dr.Read
			
				sFLastSuccess = me.CreateObject(dr)
			            
			end while        					
			
			return sFLastSuccess
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sFLastSuccessList as ArrayList = new ArrayList
			
			while dr.Read
					dim sFLastSuccess as SFLastSuccess = me.CreateObject(dr)
					sFLastSuccessList.Add(sFLastSuccess)
			end while
			     
			return sFLastSuccessList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sFLastSuccess as SFLastSuccess = ctype(obj, SFLastSuccess)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFLastSuccess.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sFLastSuccess as SFLastSuccess = ctype(obj, SFLastSuccess)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@SFMasterObjectID", DbType.Int32, sFLastSuccess.SFMasterObject.ID)
            DbCommandWrapper.AddInParameter("@SFStagingLogID", DbType.Int32, sFLastSuccess.SFStagingLog.ID)
			DbCommandWrapper.AddInParameter("@LastSuccessTime",DbType.DateTime,sFLastSuccess.LastSuccessTime)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFLastSuccess.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdatedBy",DbType.AnsiString,sFLastSuccess.LastUpdatedBy)
			DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,sFLastSuccess.LastUpdatedTime)

						
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
		
			dim sFLastSuccess as SFLastSuccess = ctype(obj, SFLastSuccess)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFLastSuccess.ID)
            DbCommandWrapper.AddInParameter("@SFMasterObjectID", DbType.Int32, sFLastSuccess.SFMasterObject.ID)
            DbCommandWrapper.AddInParameter("@SFStagingLogID", DbType.Int32, sFLastSuccess.SFStagingLog.ID)
			DbCommandWrapper.AddInParameter("@LastSuccessTime",DbType.DateTime,sFLastSuccess.LastSuccessTime)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFLastSuccess.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sFLastSuccess.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdatedBy",DbType.AnsiString,sFLastSuccess.LastUpdatedBy)
			'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SFLastSuccess
		
			dim sFLastSuccess as SFLastSuccess = new SFLastSuccess
			
			sFLastSuccess.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("SFMasterObjectID")) Then sFLastSuccess.SFMasterObject = New SFMasterObject(ID:=CType(dr("SFMasterObjectID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("SFStagingLogID")) Then sFLastSuccess.SFStagingLog = New SFStagingLog(ID:=CType(dr("SFStagingLogID"), Integer))
			if not dr.IsDBNull(dr.GetOrdinal("LastSuccessTime")) then sFLastSuccess.LastSuccessTime = ctype(dr("LastSuccessTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sFLastSuccess.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sFLastSuccess.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sFLastSuccess.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) then sFLastSuccess.LastUpdatedBy = dr("LastUpdatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) then sFLastSuccess.LastUpdatedTime = ctype(dr("LastUpdatedTime"), DateTime) 
			
			return sFLastSuccess
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SFLastSuccess) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SFLastSuccess),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SFLastSuccess).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

