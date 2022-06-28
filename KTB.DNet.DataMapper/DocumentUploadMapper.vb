#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DocumentUpload Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 9/7/2020 - 9:28:39 AM
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

	public class DocumentUploadMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertDocumentUpload"
		private m_UpdateStatement as string = "up_UpdateDocumentUpload"
		private m_RetrieveStatement as string = "up_RetrieveDocumentUpload"
		private m_RetrieveListStatement as string = "up_RetrieveDocumentUploadList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteDocumentUpload"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim documentUpload as DocumentUpload = nothing
			while dr.Read
			
				documentUpload = me.CreateObject(dr)
			            
			end while        					
			
			return documentUpload
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim documentUploadList as ArrayList = new ArrayList
			
			while dr.Read
					dim documentUpload as DocumentUpload = me.CreateObject(dr)
					documentUploadList.Add(documentUpload)
			end while
			     
			return documentUploadList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim documentUpload as DocumentUpload = ctype(obj, DocumentUpload)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,documentUpload.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim documentUpload as DocumentUpload = ctype(obj, DocumentUpload)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@Type",DbType.Int32,documentUpload.Type)
			DbCommandWrapper.AddInParameter("@DocRegNumber",DbType.AnsiString,documentUpload.DocRegNumber)
			DbCommandWrapper.AddInParameter("@FileName",DbType.AnsiString,documentUpload.FileName)
			DbCommandWrapper.AddInParameter("@FileDescription",DbType.AnsiString,documentUpload.FileDescription)
			DbCommandWrapper.AddInParameter("@Path",DbType.AnsiString,documentUpload.Path)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,documentUpload.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            Return DbCommandWrapper
			
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
		
			dim documentUpload as DocumentUpload = ctype(obj, DocumentUpload)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,documentUpload.ID)
			DbCommandWrapper.AddInParameter("@Type",DbType.Int32,documentUpload.Type)
			DbCommandWrapper.AddInParameter("@DocRegNumber",DbType.AnsiString,documentUpload.DocRegNumber)
			DbCommandWrapper.AddInParameter("@FileName",DbType.AnsiString,documentUpload.FileName)
			DbCommandWrapper.AddInParameter("@FileDescription",DbType.AnsiString,documentUpload.FileDescription)
			DbCommandWrapper.AddInParameter("@Path",DbType.AnsiString,documentUpload.Path)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,documentUpload.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,documentUpload.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as DocumentUpload
		
			dim documentUpload as DocumentUpload = new DocumentUpload
			
			documentUpload.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Type")) then documentUpload.Type = ctype(dr("Type"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("DocRegNumber")) then documentUpload.DocRegNumber = dr("DocRegNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("FileName")) then documentUpload.FileName = dr("FileName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("FileDescription")) then documentUpload.FileDescription = dr("FileDescription").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Path")) then documentUpload.Path = dr("Path").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then documentUpload.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then documentUpload.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then documentUpload.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then documentUpload.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then documentUpload.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
			
			return documentUpload
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (DocumentUpload) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(DocumentUpload),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(DocumentUpload).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace
