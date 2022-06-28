
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFPurchaseTransaction Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/07/2018 - 2:48:48 PM
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

	public class SFPurchaseTransactionMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSFPurchaseTransaction"
		private m_UpdateStatement as string = "up_UpdateSFPurchaseTransaction"
		private m_RetrieveStatement as string = "up_RetrieveSFPurchaseTransaction"
		private m_RetrieveListStatement as string = "up_RetrieveSFPurchaseTransactionList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSFPurchaseTransaction"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sFPurchaseTransaction as SFPurchaseTransaction = nothing
			while dr.Read
			
				sFPurchaseTransaction = me.CreateObject(dr)
			            
			end while        					
			
			return sFPurchaseTransaction
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sFPurchaseTransactionList as ArrayList = new ArrayList
			
			while dr.Read
					dim sFPurchaseTransaction as SFPurchaseTransaction = me.CreateObject(dr)
					sFPurchaseTransactionList.Add(sFPurchaseTransaction)
			end while
			     
			return sFPurchaseTransactionList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sFPurchaseTransaction as SFPurchaseTransaction = ctype(obj, SFPurchaseTransaction)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFPurchaseTransaction.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sFPurchaseTransaction as SFPurchaseTransaction = ctype(obj, SFPurchaseTransaction)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, sFPurchaseTransaction.EndCustomer.ID)
			DbCommandWrapper.AddInParameter("@IsSynchronize",DbType.Boolean,sFPurchaseTransaction.IsSynchronize)
			DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFPurchaseTransaction.SynchronizeDate)
			DbCommandWrapper.AddInParameter("@IsActive",DbType.Boolean,sFPurchaseTransaction.IsActive)
            DbCommandWrapper.AddInParameter("@SFID", DbType.AnsiString, sFPurchaseTransaction.SFID)
            DbCommandWrapper.AddInParameter("@PDIDate", DbType.DateTime, sFPurchaseTransaction.PDIDate)
            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, sFPurchaseTransaction.PKTDate)
            DbCommandWrapper.AddInParameter("@WarrantyRequestDate", DbType.DateTime, sFPurchaseTransaction.WarrantyRequestDate)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, sFPurchaseTransaction.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, sFPurchaseTransaction.SalesmanName)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFPurchaseTransaction.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sFPurchaseTransaction.LastUpdateBy)
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
		
			dim sFPurchaseTransaction as SFPurchaseTransaction = ctype(obj, SFPurchaseTransaction)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFPurchaseTransaction.ID)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, sFPurchaseTransaction.EndCustomer.ID)
			DbCommandWrapper.AddInParameter("@IsSynchronize",DbType.Boolean,sFPurchaseTransaction.IsSynchronize)
			DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFPurchaseTransaction.SynchronizeDate)
			DbCommandWrapper.AddInParameter("@IsActive",DbType.Boolean,sFPurchaseTransaction.IsActive)
            DbCommandWrapper.AddInParameter("@SFID", DbType.AnsiString, sFPurchaseTransaction.SFID)
            DbCommandWrapper.AddInParameter("@PDIDate", DbType.DateTime, sFPurchaseTransaction.PDIDate)
            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, sFPurchaseTransaction.PKTDate)
            DbCommandWrapper.AddInParameter("@WarrantyRequestDate", DbType.DateTime, sFPurchaseTransaction.WarrantyRequestDate)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, sFPurchaseTransaction.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, sFPurchaseTransaction.SalesmanName)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFPurchaseTransaction.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sFPurchaseTransaction.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SFPurchaseTransaction
		
			dim sFPurchaseTransaction as SFPurchaseTransaction = new SFPurchaseTransaction
			
			sFPurchaseTransaction.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then sFPurchaseTransaction.EndCustomer = New EndCustomer(ID:=CType(dr("EndCustomerID"), Integer))
			if not dr.IsDBNull(dr.GetOrdinal("IsSynchronize")) then sFPurchaseTransaction.IsSynchronize = ctype(dr("IsSynchronize"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("SynchronizeDate")) then sFPurchaseTransaction.SynchronizeDate = ctype(dr("SynchronizeDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("IsActive")) then sFPurchaseTransaction.IsActive = ctype(dr("IsActive"), boolean) 
            If Not dr.IsDBNull(dr.GetOrdinal("SFID")) Then sFPurchaseTransaction.SFID = dr("SFID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIDate")) Then sFPurchaseTransaction.PDIDate = CType(dr("PDIDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PKTDate")) Then sFPurchaseTransaction.PKTDate = CType(dr("PKTDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WarrantyRequestDate")) Then sFPurchaseTransaction.WarrantyRequestDate = CType(dr("WarrantyRequestDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then sFPurchaseTransaction.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanName")) Then sFPurchaseTransaction.SalesmanName = dr("SalesmanName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sFPurchaseTransaction.RowStatus = CType(dr("RowStatus"), Short)
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sFPurchaseTransaction.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sFPurchaseTransaction.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sFPurchaseTransaction.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sFPurchaseTransaction.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sFPurchaseTransaction
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SFPurchaseTransaction) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SFPurchaseTransaction),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SFPurchaseTransaction).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

