#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRAttachment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 2:22:34 PM
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

	public class PQRAttachmentMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertPQRAttachment"
		private m_UpdateStatement as string = "up_UpdatePQRAttachment"
		private m_RetrieveStatement as string = "up_RetrievePQRAttachment"
		private m_RetrieveListStatement as string = "up_RetrievePQRAttachmentList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeletePQRAttachment"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim pQRAttachment as PQRAttachment = nothing
			while dr.Read
			
				pQRAttachment = me.CreateObject(dr)
			            
			end while        					
			
			return pQRAttachment
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim pQRAttachmentList as ArrayList = new ArrayList
			
			while dr.Read
					dim pQRAttachment as PQRAttachment = me.CreateObject(dr)
					pQRAttachmentList.Add(pQRAttachment)
			end while
			     
			return pQRAttachmentList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim pQRAttachment as PQRAttachment = ctype(obj, PQRAttachment)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pQRAttachment.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim pQRAttachment as PQRAttachment = ctype(obj, PQRAttachment)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,0)
			DbCommandWrapper.AddInParameter("@Message",DbType.AnsiString,pQRAttachment.Message)
			DbCommandWrapper.AddInParameter("@AttachmentType",DbType.AnsiString,pQRAttachment.AttachmentType)
			DbCommandWrapper.AddInParameter("@Attachment",DbType.AnsiString,pQRAttachment.Attachment)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pQRAttachment.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,pQRAttachment.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

			DbCommandWrapper.AddInParameter("@PQRHeaderID",DbType.Int32,Me.GetRefObject(pQRAttachment.PQRHeader))
			DbCommandWrapper.AddInParameter("@AdditionalInfoID",DbType.Int32,Me.GetRefObject(pQRAttachment.PQRAdditionalInfo))
						
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
		
			dim pQRAttachment as PQRAttachment = ctype(obj, PQRAttachment)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pQRAttachment.ID)
			DbCommandWrapper.AddInParameter("@Message",DbType.AnsiString,pQRAttachment.Message)
			DbCommandWrapper.AddInParameter("@AttachmentType",DbType.AnsiString,pQRAttachment.AttachmentType)
			DbCommandWrapper.AddInParameter("@Attachment",DbType.AnsiString,pQRAttachment.Attachment)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,pQRAttachment.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,pQRAttachment.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
			DbCommandWrapper.AddInParameter("@PQRHeaderID",DbType.Int32, Me.GetRefObject(pQRAttachment.PQRHeader))
			DbCommandWrapper.AddInParameter("@AdditionalInfoID",DbType.Int32, Me.GetRefObject(pQRAttachment.PQRAdditionalInfo))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as PQRAttachment
		
			dim pQRAttachment as PQRAttachment = new PQRAttachment
			
			pQRAttachment.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Message")) then pQRAttachment.Message = dr("Message").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("AttachmentType")) then pQRAttachment.AttachmentType = dr("AttachmentType").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Attachment")) then pQRAttachment.Attachment = dr("Attachment").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then pQRAttachment.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then pQRAttachment.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then pQRAttachment.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then pQRAttachment.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then pQRAttachment.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("PQRHeaderID")) then
				pQRAttachment.PQRHeader = new PQRHeader (ctype(dr("PQRHeaderID"), integer))
			end if
			if not dr.IsDBNull(dr.GetOrdinal("AdditionalInfoID")) then
				pQRAttachment.PQRAdditionalInfo = new PQRAdditionalInfo (ctype(dr("AdditionalInfoID"), integer))
			end if
			
			return pQRAttachment
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (PQRAttachment) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(PQRAttachment),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(PQRAttachment).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

