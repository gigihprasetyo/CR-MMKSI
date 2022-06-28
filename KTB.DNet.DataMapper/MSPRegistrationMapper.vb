
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPRegistration Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/12/2017 - 1:50:58 PM
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

	public class MSPRegistrationMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMSPRegistration"
		private m_UpdateStatement as string = "up_UpdateMSPRegistration"
		private m_RetrieveStatement as string = "up_RetrieveMSPRegistration"
		private m_RetrieveListStatement as string = "up_RetrieveMSPRegistrationList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMSPRegistration"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim mSPRegistration as MSPRegistration = nothing
			while dr.Read
			
				mSPRegistration = me.CreateObject(dr)
			            
			end while        					
			
			return mSPRegistration
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim mSPRegistrationList as ArrayList = new ArrayList
			
			while dr.Read
					dim mSPRegistration as MSPRegistration = me.CreateObject(dr)
					mSPRegistrationList.Add(mSPRegistration)
			end while
			     
			return mSPRegistrationList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim mSPRegistration as MSPRegistration = ctype(obj, MSPRegistration)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPRegistration.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim mSPRegistration as MSPRegistration = ctype(obj, MSPRegistration)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, mSPRegistration.MSPCustomer.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, mSPRegistration.Dealer.ID)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, mSPRegistration.ChassisMaster.ID)
            DbCommandWrapper.AddInParameter("@MSPCode", DbType.AnsiString, mSPRegistration.MSPCode)
            DbCommandWrapper.AddInParameter("@OldMSPCode", DbType.AnsiString, mSPRegistration.OldMSPCode)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPRegistration.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,mSPRegistration.LastUpdateBy)
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
		
			dim mSPRegistration as MSPRegistration = ctype(obj, MSPRegistration)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPRegistration.ID)
            DbCommandWrapper.AddInParameter("@MSPCustomerID", DbType.Int32, mSPRegistration.MSPCustomer.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, mSPRegistration.Dealer.ID)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, mSPRegistration.ChassisMaster.ID)
            DbCommandWrapper.AddInParameter("@MSPCode", DbType.AnsiString, mSPRegistration.MSPCode)
            DbCommandWrapper.AddInParameter("@OldMSPCode", DbType.AnsiString, mSPRegistration.OldMSPCode)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPRegistration.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,mSPRegistration.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
				
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MSPRegistration
		
			dim mSPRegistration as MSPRegistration = new MSPRegistration
			
			mSPRegistration.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then mSPRegistration.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                mSPRegistration.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MSPCode")) Then mSPRegistration.MSPCode = dr("MSPCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OldMSPCode")) Then mSPRegistration.OldMSPCode = dr("OldMSPCode").ToString
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then mSPRegistration.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then mSPRegistration.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then mSPRegistration.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then mSPRegistration.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then mSPRegistration.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("MSPCustomerID")) Then
                mSPRegistration.MSPCustomer = New MSPCustomer(CType(dr("MSPCustomerID"), Integer))
            End If

			return mSPRegistration
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MSPRegistration) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MSPRegistration),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MSPRegistration).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

