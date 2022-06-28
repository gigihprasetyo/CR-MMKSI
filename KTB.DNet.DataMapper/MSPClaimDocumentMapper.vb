
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPClaimDocument Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/31/2018 - 10:48:43 AM
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

	public class MSPClaimDocumentMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMSPClaimDocument"
		private m_UpdateStatement as string = "up_UpdateMSPClaimDocument"
		private m_RetrieveStatement as string = "up_RetrieveMSPClaimDocument"
		private m_RetrieveListStatement as string = "up_RetrieveMSPClaimDocumentList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMSPClaimDocument"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim mSPClaimDocument as MSPClaimDocument = nothing
			while dr.Read
			
				mSPClaimDocument = me.CreateObject(dr)
			            
			end while        					
			
			return mSPClaimDocument
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim mSPClaimDocumentList as ArrayList = new ArrayList
			
			while dr.Read
					dim mSPClaimDocument as MSPClaimDocument = me.CreateObject(dr)
					mSPClaimDocumentList.Add(mSPClaimDocument)
			end while
			     
			return mSPClaimDocumentList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim mSPClaimDocument as MSPClaimDocument = ctype(obj, MSPClaimDocument)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPClaimDocument.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim mSPClaimDocument as MSPClaimDocument = ctype(obj, MSPClaimDocument)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@MSPClaimID", DbType.Int32, mSPClaimDocument.MSPClaim.ID)
			DbCommandWrapper.AddInParameter("@KuitansiNumber",DbType.AnsiString,mSPClaimDocument.KuitansiNumber)
			DbCommandWrapper.AddInParameter("@LetterNumber",DbType.AnsiString,mSPClaimDocument.LetterNumber)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, mSPClaimDocument.TotalAmount)
            DbCommandWrapper.AddInParameter("@FileNameKuitansi", DbType.AnsiString, mSPClaimDocument.FileNameKuitansi)
            DbCommandWrapper.AddInParameter("@FileNameLetter", DbType.AnsiString, mSPClaimDocument.FileNameLetter)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPClaimDocument.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,mSPClaimDocument.LastUpdateBy)
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
		
			dim mSPClaimDocument as MSPClaimDocument = ctype(obj, MSPClaimDocument)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPClaimDocument.ID)
            DbCommandWrapper.AddInParameter("@MSPClaimID", DbType.Int32, mSPClaimDocument.MSPClaim.ID)
			DbCommandWrapper.AddInParameter("@KuitansiNumber",DbType.AnsiString,mSPClaimDocument.KuitansiNumber)
			DbCommandWrapper.AddInParameter("@LetterNumber",DbType.AnsiString,mSPClaimDocument.LetterNumber)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, mSPClaimDocument.TotalAmount)
            DbCommandWrapper.AddInParameter("@FileNameKuitansi", DbType.AnsiString, mSPClaimDocument.FileNameKuitansi)
            DbCommandWrapper.AddInParameter("@FileNameLetter", DbType.AnsiString, mSPClaimDocument.FileNameLetter)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPClaimDocument.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,mSPClaimDocument.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MSPClaimDocument
		
			dim mSPClaimDocument as MSPClaimDocument = new MSPClaimDocument
			
			mSPClaimDocument.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("MSPClaimID")) Then mSPClaimDocument.MSPClaim = New MSPClaim(ID:=CType(dr("MSPClaimID"), Integer))
			if not dr.IsDBNull(dr.GetOrdinal("KuitansiNumber")) then mSPClaimDocument.KuitansiNumber = dr("KuitansiNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LetterNumber")) then mSPClaimDocument.LetterNumber = dr("LetterNumber").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then mSPClaimDocument.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FileNameKuitansi")) Then mSPClaimDocument.FileNameKuitansi = dr("FileNameKuitansi").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNameLetter")) Then mSPClaimDocument.FileNameLetter = dr("FileNameLetter").ToString
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then mSPClaimDocument.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then mSPClaimDocument.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then mSPClaimDocument.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then mSPClaimDocument.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then mSPClaimDocument.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return mSPClaimDocument
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MSPClaimDocument) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MSPClaimDocument),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MSPClaimDocument).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

