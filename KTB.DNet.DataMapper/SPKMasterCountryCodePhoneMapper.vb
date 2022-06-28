#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKMasterCountryCodePhone Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 6/22/2021 - 11:38:04 AM
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

	public class SPKMasterCountryCodePhoneMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSPKMasterCountryCodePhone"
		private m_UpdateStatement as string = "up_UpdateSPKMasterCountryCodePhone"
		private m_RetrieveStatement as string = "up_RetrieveSPKMasterCountryCodePhone"
		private m_RetrieveListStatement as string = "up_RetrieveSPKMasterCountryCodePhoneList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSPKMasterCountryCodePhone"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sPKMasterCountryCodePhone as SPKMasterCountryCodePhone = nothing
			while dr.Read
			
				sPKMasterCountryCodePhone = me.CreateObject(dr)
			            
			end while        					
			
			return sPKMasterCountryCodePhone
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sPKMasterCountryCodePhoneList as ArrayList = new ArrayList
			
			while dr.Read
					dim sPKMasterCountryCodePhone as SPKMasterCountryCodePhone = me.CreateObject(dr)
					sPKMasterCountryCodePhoneList.Add(sPKMasterCountryCodePhone)
			end while
			     
			return sPKMasterCountryCodePhoneList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sPKMasterCountryCodePhone as SPKMasterCountryCodePhone = ctype(obj, SPKMasterCountryCodePhone)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sPKMasterCountryCodePhone.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sPKMasterCountryCodePhone as SPKMasterCountryCodePhone = ctype(obj, SPKMasterCountryCodePhone)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@CountryName",DbType.AnsiString,sPKMasterCountryCodePhone.CountryName)
			DbCommandWrapper.AddInParameter("@CountryCode",DbType.AnsiString,sPKMasterCountryCodePhone.CountryCode)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKMasterCountryCodePhone.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sPKMasterCountryCodePhone.CreatedBy)
            DbCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, sPKMasterCountryCodePhone.CreatedTime)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, sPKMasterCountryCodePhone.LastUpdatedby)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, sPKMasterCountryCodePhone.LastUpdatedTime)

						
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
		
			dim sPKMasterCountryCodePhone as SPKMasterCountryCodePhone = ctype(obj, SPKMasterCountryCodePhone)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sPKMasterCountryCodePhone.ID)
			DbCommandWrapper.AddInParameter("@CountryName",DbType.AnsiString,sPKMasterCountryCodePhone.CountryName)
			DbCommandWrapper.AddInParameter("@CountryCode",DbType.AnsiString,sPKMasterCountryCodePhone.CountryCode)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKMasterCountryCodePhone.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sPKMasterCountryCodePhone.CreatedBy)
            DbCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, sPKMasterCountryCodePhone.CreatedTime)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, sPKMasterCountryCodePhone.LastUpdatedby)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, sPKMasterCountryCodePhone.LastUpdatedTime)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SPKMasterCountryCodePhone
		
			dim sPKMasterCountryCodePhone as SPKMasterCountryCodePhone = new SPKMasterCountryCodePhone
			
			sPKMasterCountryCodePhone.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("CountryName")) then sPKMasterCountryCodePhone.CountryName = dr("CountryName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CountryCode")) then sPKMasterCountryCodePhone.CountryCode = dr("CountryCode").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPKMasterCountryCodePhone.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPKMasterCountryCodePhone.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPKMasterCountryCodePhone.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then sPKMasterCountryCodePhone.LastUpdatedby = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then sPKMasterCountryCodePhone.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
			
			return sPKMasterCountryCodePhone
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SPKMasterCountryCodePhone) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SPKMasterCountryCodePhone),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SPKMasterCountryCodePhone).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace
