
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPTransferPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/9/2018 - 11:28:28 AM
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

	public class MSPTransferPaymentMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMSPTransferPayment"
		private m_UpdateStatement as string = "up_UpdateMSPTransferPayment"
		private m_RetrieveStatement as string = "up_RetrieveMSPTransferPayment"
		private m_RetrieveListStatement as string = "up_RetrieveMSPTransferPaymentList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMSPTransferPayment"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim mSPTransferPayment as MSPTransferPayment = nothing
			while dr.Read
			
				mSPTransferPayment = me.CreateObject(dr)
			            
			end while        					
			
			return mSPTransferPayment
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim mSPTransferPaymentList as ArrayList = new ArrayList
			
			while dr.Read
					dim mSPTransferPayment as MSPTransferPayment = me.CreateObject(dr)
					mSPTransferPaymentList.Add(mSPTransferPayment)
			end while
			     
			return mSPTransferPaymentList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim mSPTransferPayment as MSPTransferPayment = ctype(obj, MSPTransferPayment)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPTransferPayment.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim mSPTransferPayment as MSPTransferPayment = ctype(obj, MSPTransferPayment)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, mSPTransferPayment.Dealer.ID)
			DbCommandWrapper.AddInParameter("@PaymentPurpose",DbType.Int16,mSPTransferPayment.PaymentPurpose)
			DbCommandWrapper.AddInParameter("@PlanTransferDate",DbType.DateTime,mSPTransferPayment.PlanTransferDate)
			DbCommandWrapper.AddInParameter("@RegNumber",DbType.AnsiString,mSPTransferPayment.RegNumber)
			DbCommandWrapper.AddInParameter("@IsNotOnTime",DbType.Int16,mSPTransferPayment.IsNotOnTime)
			DbCommandWrapper.AddInParameter("@TotalAmount",DbType.Currency,mSPTransferPayment.TotalAmount)
			DbCommandWrapper.AddInParameter("@TotalActualAmount",DbType.Currency,mSPTransferPayment.TotalActualAmount)
			DbCommandWrapper.AddInParameter("@ActualTransferDate",DbType.DateTime,mSPTransferPayment.ActualTransferDate)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,mSPTransferPayment.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPTransferPayment.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,mSPTransferPayment.LastUpdateBy)
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
		
			dim mSPTransferPayment as MSPTransferPayment = ctype(obj, MSPTransferPayment)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPTransferPayment.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, mSPTransferPayment.Dealer.ID)
			DbCommandWrapper.AddInParameter("@PaymentPurpose",DbType.Int16,mSPTransferPayment.PaymentPurpose)
			DbCommandWrapper.AddInParameter("@PlanTransferDate",DbType.DateTime,mSPTransferPayment.PlanTransferDate)
			DbCommandWrapper.AddInParameter("@RegNumber",DbType.AnsiString,mSPTransferPayment.RegNumber)
			DbCommandWrapper.AddInParameter("@IsNotOnTime",DbType.Int16,mSPTransferPayment.IsNotOnTime)
			DbCommandWrapper.AddInParameter("@TotalAmount",DbType.Currency,mSPTransferPayment.TotalAmount)
			DbCommandWrapper.AddInParameter("@TotalActualAmount",DbType.Currency,mSPTransferPayment.TotalActualAmount)
			DbCommandWrapper.AddInParameter("@ActualTransferDate",DbType.DateTime,mSPTransferPayment.ActualTransferDate)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,mSPTransferPayment.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPTransferPayment.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,mSPTransferPayment.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@IsValidation", DbType.Boolean, mSPTransferPayment.IsValidation)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MSPTransferPayment
		
			dim mSPTransferPayment as MSPTransferPayment = new MSPTransferPayment
			
			mSPTransferPayment.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then mSPTransferPayment.Dealer = New Dealer(ID:=CType(dr("DealerID"), Short))
			if not dr.IsDBNull(dr.GetOrdinal("PaymentPurpose")) then mSPTransferPayment.PaymentPurpose = ctype(dr("PaymentPurpose"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("PlanTransferDate")) then mSPTransferPayment.PlanTransferDate = ctype(dr("PlanTransferDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("RegNumber")) then mSPTransferPayment.RegNumber = dr("RegNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("IsNotOnTime")) then mSPTransferPayment.IsNotOnTime = ctype(dr("IsNotOnTime"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) then mSPTransferPayment.TotalAmount = ctype(dr("TotalAmount"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("TotalActualAmount")) then mSPTransferPayment.TotalActualAmount = ctype(dr("TotalActualAmount"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("ActualTransferDate")) then mSPTransferPayment.ActualTransferDate = ctype(dr("ActualTransferDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then mSPTransferPayment.Status = ctype(dr("Status"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then mSPTransferPayment.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then mSPTransferPayment.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then mSPTransferPayment.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then mSPTransferPayment.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then mSPTransferPayment.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return mSPTransferPayment
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MSPTransferPayment) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MSPTransferPayment),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MSPTransferPayment).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

