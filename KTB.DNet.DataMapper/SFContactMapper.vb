
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFContact Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 11:32:26 AM
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

	public class SFContactMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSFContact"
		private m_UpdateStatement as string = "up_UpdateSFContact"
		private m_RetrieveStatement as string = "up_RetrieveSFContact"
		private m_RetrieveListStatement as string = "up_RetrieveSFContactList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSFContact"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sFContact as SFContact = nothing
			while dr.Read
			
				sFContact = me.CreateObject(dr)
			            
			end while        					
			
			return sFContact
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sFContactList as ArrayList = new ArrayList
			
			while dr.Read
					dim sFContact as SFContact = me.CreateObject(dr)
					sFContactList.Add(sFContact)
			end while
			     
			return sFContactList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sFContact as SFContact = ctype(obj, SFContact)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFContact.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sFContact as SFContact = ctype(obj, SFContact)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, sFContact.EndCustomer.ID)
			DbCommandWrapper.AddInParameter("@IsSynchronize",DbType.Boolean,sFContact.IsSynchronize)
			DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFContact.SynchronizeDate)
            DbCommandWrapper.AddInParameter("@IsActive", DbType.Boolean, sFContact.IsActive)
            DbCommandWrapper.AddInParameter("@SFID", DbType.AnsiString, sFContact.SFID)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFContact.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sFContact.LastUpdateBy)
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
		
			dim sFContact as SFContact = ctype(obj, SFContact)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFContact.ID)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, sFContact.EndCustomer.ID)
			DbCommandWrapper.AddInParameter("@IsSynchronize",DbType.Boolean,sFContact.IsSynchronize)
			DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFContact.SynchronizeDate)
            DbCommandWrapper.AddInParameter("@IsActive", DbType.Boolean, sFContact.IsActive)
            DbCommandWrapper.AddInParameter("@SFID", DbType.AnsiString, sFContact.SFID)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFContact.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sFContact.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SFContact
		
			dim sFContact as SFContact = new SFContact
			
			sFContact.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then sFContact.EndCustomer = New EndCustomer(ID:=CType(dr("EndCustomerID"), Integer))
			if not dr.IsDBNull(dr.GetOrdinal("IsSynchronize")) then sFContact.IsSynchronize = ctype(dr("IsSynchronize"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("SynchronizeDate")) then sFContact.SynchronizeDate = ctype(dr("SynchronizeDate"), DateTime) 
            If Not dr.IsDBNull(dr.GetOrdinal("IsActive")) Then sFContact.IsActive = CType(dr("IsActive"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("SFID")) Then sFContact.SFID = dr("SFID").ToString
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sFContact.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sFContact.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sFContact.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sFContact.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sFContact.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sFContact
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SFContact) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SFContact),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SFContact).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

