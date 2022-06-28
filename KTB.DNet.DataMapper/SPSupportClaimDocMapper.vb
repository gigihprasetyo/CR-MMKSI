
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPSupportClaimDoc Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 24/03/2020 - 10:20:39
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

	public class SPSupportClaimDocMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSPSupportClaimDoc"
		private m_UpdateStatement as string = "up_UpdateSPSupportClaimDoc"
		private m_RetrieveStatement as string = "up_RetrieveSPSupportClaimDoc"
		private m_RetrieveListStatement as string = "up_RetrieveSPSupportClaimDocList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSPSupportClaimDoc"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sPSupportClaimDoc as SPSupportClaimDoc = nothing
			while dr.Read
			
				sPSupportClaimDoc = me.CreateObject(dr)
			            
			end while        					
			
			return sPSupportClaimDoc
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sPSupportClaimDocList as ArrayList = new ArrayList
			
			while dr.Read
					dim sPSupportClaimDoc as SPSupportClaimDoc = me.CreateObject(dr)
					sPSupportClaimDocList.Add(sPSupportClaimDoc)
			end while
			     
			return sPSupportClaimDocList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sPSupportClaimDoc as SPSupportClaimDoc = ctype(obj, SPSupportClaimDoc)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sPSupportClaimDoc.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sPSupportClaimDoc as SPSupportClaimDoc = ctype(obj, SPSupportClaimDoc)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@DocumentName",DbType.AnsiString,sPSupportClaimDoc.DocumentName)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,sPSupportClaimDoc.Description)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,sPSupportClaimDoc.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sPSupportClaimDoc.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sPSupportClaimDoc.LastUpdateBy)
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
		
			dim sPSupportClaimDoc as SPSupportClaimDoc = ctype(obj, SPSupportClaimDoc)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sPSupportClaimDoc.ID)
			DbCommandWrapper.AddInParameter("@DocumentName",DbType.AnsiString,sPSupportClaimDoc.DocumentName)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,sPSupportClaimDoc.Description)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,sPSupportClaimDoc.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sPSupportClaimDoc.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sPSupportClaimDoc.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SPSupportClaimDoc
		
			dim sPSupportClaimDoc as SPSupportClaimDoc = new SPSupportClaimDoc
			
			sPSupportClaimDoc.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("DocumentName")) then sPSupportClaimDoc.DocumentName = dr("DocumentName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Description")) then sPSupportClaimDoc.Description = dr("Description").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then sPSupportClaimDoc.Status = ctype(dr("Status"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sPSupportClaimDoc.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sPSupportClaimDoc.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sPSupportClaimDoc.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sPSupportClaimDoc.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sPSupportClaimDoc.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sPSupportClaimDoc
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SPSupportClaimDoc) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SPSupportClaimDoc),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SPSupportClaimDoc).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

