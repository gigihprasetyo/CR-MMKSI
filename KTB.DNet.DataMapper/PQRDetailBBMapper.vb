#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRDetailBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 3:22:00 PM
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

	public class PQRDetailBBMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertPQRDetailBB"
		private m_UpdateStatement as string = "up_UpdatePQRDetailBB"
		private m_RetrieveStatement as string = "up_RetrievePQRDetailBB"
		private m_RetrieveListStatement as string = "up_RetrievePQRDetailBBList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeletePQRDetailBB"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim PQRDetailBB as PQRDetailBB = nothing
			while dr.Read
			
				PQRDetailBB = me.CreateObject(dr)
			            
			end while        					
			
			return PQRDetailBB
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim PQRDetailBBList as ArrayList = new ArrayList
			
			while dr.Read
					dim PQRDetailBB as PQRDetailBB = me.CreateObject(dr)
					PQRDetailBBList.Add(PQRDetailBB)
			end while
			     
			return PQRDetailBBList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim PQRDetailBB as PQRDetailBB = ctype(obj, PQRDetailBB)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,PQRDetailBB.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim PQRDetailBB as PQRDetailBB = ctype(obj, PQRDetailBB)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,0)
			DbCommandWrapper.AddInParameter("@ItemType",DbType.AnsiString,PQRDetailBB.ItemType)
			DbCommandWrapper.AddInParameter("@ItemNumber",DbType.AnsiString,PQRDetailBB.ItemNumber)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,PQRDetailBB.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,PQRDetailBB.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

			DbCommandWrapper.AddInParameter("@PQRHeaderBBID",DbType.Int32,Me.GetRefObject(PQRDetailBB.PQRHeaderBB))
						
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
		
			dim PQRDetailBB as PQRDetailBB = ctype(obj, PQRDetailBB)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,PQRDetailBB.ID)
			DbCommandWrapper.AddInParameter("@ItemType",DbType.AnsiString,PQRDetailBB.ItemType)
			DbCommandWrapper.AddInParameter("@ItemNumber",DbType.AnsiString,PQRDetailBB.ItemNumber)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,PQRDetailBB.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,PQRDetailBB.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
			DbCommandWrapper.AddInParameter("@PQRHeaderBBID",DbType.Int32, Me.GetRefObject(PQRDetailBB.PQRHeaderBB))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as PQRDetailBB
		
			dim PQRDetailBB as PQRDetailBB = new PQRDetailBB
			
			PQRDetailBB.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("ItemType")) then PQRDetailBB.ItemType = dr("ItemType").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ItemNumber")) then PQRDetailBB.ItemNumber = dr("ItemNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then PQRDetailBB.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then PQRDetailBB.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then PQRDetailBB.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then PQRDetailBB.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then PQRDetailBB.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("PQRHeaderBBID")) then
				PQRDetailBB.PQRHeaderBB = new PQRHeaderBB (ctype(dr("PQRHeaderBBID"), integer))
			end if
			
			return PQRDetailBB
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (PQRDetailBB) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(PQRDetailBB),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(PQRDetailBB).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

