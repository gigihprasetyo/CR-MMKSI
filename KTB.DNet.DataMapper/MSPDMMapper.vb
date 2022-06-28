
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPDM Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/11/2018 - 2:54:15 PM
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

	public class MSPDMMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMSPDM"
		private m_UpdateStatement as string = "up_UpdateMSPDM"
		private m_RetrieveStatement as string = "up_RetrieveMSPDM"
		private m_RetrieveListStatement as string = "up_RetrieveMSPDMList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMSPDM"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim mSPDM as MSPDM = nothing
			while dr.Read
			
				mSPDM = me.CreateObject(dr)
			            
			end while        					
			
			return mSPDM
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim mSPDMList as ArrayList = new ArrayList
			
			while dr.Read
					dim mSPDM as MSPDM = me.CreateObject(dr)
					mSPDMList.Add(mSPDM)
			end while
			     
			return mSPDMList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim mSPDM as MSPDM = ctype(obj, MSPDM)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPDM.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim mSPDM as MSPDM = ctype(obj, MSPDM)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@MSPDCID", DbType.Int32, mSPDM.MSPDC.ID)
			DbCommandWrapper.AddInParameter("@DebitMemoNo",DbType.AnsiString,mSPDM.DebitMemoNo)
			DbCommandWrapper.AddInParameter("@Amount",DbType.Currency,mSPDM.Amount)
			DbCommandWrapper.AddInParameter("@DocType",DbType.AnsiString,mSPDM.DocType)
			DbCommandWrapper.AddInParameter("@DocumentDate",DbType.DateTime,mSPDM.DocumentDate)
			DbCommandWrapper.AddInParameter("@FileName",DbType.AnsiString,mSPDM.FileName)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPDM.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,mSPDM.LastUpdateBy)
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
		
			dim mSPDM as MSPDM = ctype(obj, MSPDM)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPDM.ID)
            DbCommandWrapper.AddInParameter("@MSPDCID", DbType.Int32, mSPDM.MSPDC.ID)
			DbCommandWrapper.AddInParameter("@DebitMemoNo",DbType.AnsiString,mSPDM.DebitMemoNo)
			DbCommandWrapper.AddInParameter("@Amount",DbType.Currency,mSPDM.Amount)
			DbCommandWrapper.AddInParameter("@DocType",DbType.AnsiString,mSPDM.DocType)
			DbCommandWrapper.AddInParameter("@DocumentDate",DbType.DateTime,mSPDM.DocumentDate)
			DbCommandWrapper.AddInParameter("@FileName",DbType.AnsiString,mSPDM.FileName)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPDM.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,mSPDM.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MSPDM
		
			dim mSPDM as MSPDM = new MSPDM
			
			mSPDM.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("MSPDCID")) Then mSPDM.MSPDC = New MSPDC(ID:=dr("MSPDCID").ToString)
			if not dr.IsDBNull(dr.GetOrdinal("DebitMemoNo")) then mSPDM.DebitMemoNo = dr("DebitMemoNo").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Amount")) then mSPDM.Amount = ctype(dr("Amount"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("DocType")) then mSPDM.DocType = dr("DocType").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DocumentDate")) then mSPDM.DocumentDate = ctype(dr("DocumentDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("FileName")) then mSPDM.FileName = dr("FileName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then mSPDM.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then mSPDM.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then mSPDM.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then mSPDM.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then mSPDM.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return mSPDM
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MSPDM) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MSPDM),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MSPDM).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

