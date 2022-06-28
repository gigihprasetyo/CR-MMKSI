#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRDamageCode Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 2:21:56 PM
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

	public class PQRDamageCodeMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertPQRDamageCode"
		private m_UpdateStatement as string = "up_UpdatePQRDamageCode"
		private m_RetrieveStatement as string = "up_RetrievePQRDamageCode"
		private m_RetrieveListStatement as string = "up_RetrievePQRDamageCodeList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeletePQRDamageCode"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim pQRDamageCode as PQRDamageCode = nothing
			while dr.Read
			
				pQRDamageCode = me.CreateObject(dr)
			            
			end while        					
			
			return pQRDamageCode
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim pQRDamageCodeList as ArrayList = new ArrayList
			
			while dr.Read
					dim pQRDamageCode as PQRDamageCode = me.CreateObject(dr)
					pQRDamageCodeList.Add(pQRDamageCode)
			end while
			     
			return pQRDamageCodeList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim pQRDamageCode as PQRDamageCode = ctype(obj, PQRDamageCode)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pQRDamageCode.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim pQRDamageCode as PQRDamageCode = ctype(obj, PQRDamageCode)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,0)
			DbCommandWrapper.AddInParameter("@DamageCode",DbType.AnsiString,pQRDamageCode.DamageCode)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int32,pQRDamageCode.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastModifiedBy",DbType.AnsiString,pQRDamageCode.LastModifiedBy)
			DbCommandWrapper.AddInParameter("@LastModifiedTime",DbType.AnsiString,pQRDamageCode.LastModifiedTime)

			DbCommandWrapper.AddInParameter("@PQRHeaderID",DbType.Int32,Me.GetRefObject(pQRDamageCode.PQRHeader))
						
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
		
			dim pQRDamageCode as PQRDamageCode = ctype(obj, PQRDamageCode)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,pQRDamageCode.ID)
			DbCommandWrapper.AddInParameter("@DamageCode",DbType.AnsiString,pQRDamageCode.DamageCode)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int32,pQRDamageCode.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,pQRDamageCode.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastModifiedBy",DbType.AnsiString,pQRDamageCode.LastModifiedBy)
			DbCommandWrapper.AddInParameter("@LastModifiedTime",DbType.AnsiString,pQRDamageCode.LastModifiedTime)
			
						
			DbCommandWrapper.AddInParameter("@PQRHeaderID",DbType.Int32, Me.GetRefObject(pQRDamageCode.PQRHeader))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as PQRDamageCode
		
			dim pQRDamageCode as PQRDamageCode = new PQRDamageCode
			
			pQRDamageCode.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("DamageCode")) then pQRDamageCode.DamageCode = dr("DamageCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then pQRDamageCode.RowStatus = ctype(dr("RowStatus"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then pQRDamageCode.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then pQRDamageCode.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastModifiedBy")) then pQRDamageCode.LastModifiedBy = dr("LastModifiedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastModifiedTime")) then pQRDamageCode.LastModifiedTime = dr("LastModifiedTime").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("PQRHeaderID")) then
				pQRDamageCode.PQRHeader = new PQRHeader (ctype(dr("PQRHeaderID"), integer))
			end if
			
			return pQRDamageCode
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (PQRDamageCode) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(PQRDamageCode),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(PQRDamageCode).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

