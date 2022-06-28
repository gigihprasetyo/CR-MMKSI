
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFServiceHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/07/2018 - 2:49:10 PM
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

	public class SFServiceHistoryMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSFServiceHistory"
		private m_UpdateStatement as string = "up_UpdateSFServiceHistory"
		private m_RetrieveStatement as string = "up_RetrieveSFServiceHistory"
		private m_RetrieveListStatement as string = "up_RetrieveSFServiceHistoryList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSFServiceHistory"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sFServiceHistory as SFServiceHistory = nothing
			while dr.Read
			
				sFServiceHistory = me.CreateObject(dr)
			            
			end while        					
			
			return sFServiceHistory
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sFServiceHistoryList as ArrayList = new ArrayList
			
			while dr.Read
					dim sFServiceHistory as SFServiceHistory = me.CreateObject(dr)
					sFServiceHistoryList.Add(sFServiceHistory)
			end while
			     
			return sFServiceHistoryList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sFServiceHistory as SFServiceHistory = ctype(obj, SFServiceHistory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFServiceHistory.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sFServiceHistory as SFServiceHistory = ctype(obj, SFServiceHistory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@PMHeaderID", DbType.Int32, sFServiceHistory.PMHeader.ID)
			DbCommandWrapper.AddInParameter("@IsSynchronize",DbType.Boolean,sFServiceHistory.IsSynchronize)
			DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFServiceHistory.SynchronizeDate)
			DbCommandWrapper.AddInParameter("@IsActive",DbType.Boolean,sFServiceHistory.IsActive)
			DbCommandWrapper.AddInParameter("@SFID",DbType.AnsiString,sFServiceHistory.SFID)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFServiceHistory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sFServiceHistory.LastUpdateBy)
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
		
			dim sFServiceHistory as SFServiceHistory = ctype(obj, SFServiceHistory)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFServiceHistory.ID)
            DbCommandWrapper.AddInParameter("@PMHeaderID", DbType.Int32, sFServiceHistory.PMHeader.ID)
			DbCommandWrapper.AddInParameter("@IsSynchronize",DbType.Boolean,sFServiceHistory.IsSynchronize)
			DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFServiceHistory.SynchronizeDate)
			DbCommandWrapper.AddInParameter("@IsActive",DbType.Boolean,sFServiceHistory.IsActive)
			DbCommandWrapper.AddInParameter("@SFID",DbType.AnsiString,sFServiceHistory.SFID)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFServiceHistory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sFServiceHistory.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SFServiceHistory
		
			dim sFServiceHistory as SFServiceHistory = new SFServiceHistory
			
			sFServiceHistory.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("PMHeaderID")) Then sFServiceHistory.PMHeader = New PMHeader(ID:=CType(dr("PMHeaderID"), Integer))
			if not dr.IsDBNull(dr.GetOrdinal("IsSynchronize")) then sFServiceHistory.IsSynchronize = ctype(dr("IsSynchronize"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("SynchronizeDate")) then sFServiceHistory.SynchronizeDate = ctype(dr("SynchronizeDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("IsActive")) then sFServiceHistory.IsActive = ctype(dr("IsActive"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("SFID")) then sFServiceHistory.SFID = dr("SFID").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sFServiceHistory.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sFServiceHistory.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sFServiceHistory.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sFServiceHistory.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sFServiceHistory.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sFServiceHistory
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SFServiceHistory) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SFServiceHistory),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SFServiceHistory).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

