

#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerSalesOrganization Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 26/05/2020 - 23:22:22
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

	public class DealerSalesOrganizationMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertDealerSalesOrganization"
		private m_UpdateStatement as string = "up_UpdateDealerSalesOrganization"
		private m_RetrieveStatement as string = "up_RetrieveDealerSalesOrganization"
		private m_RetrieveListStatement as string = "up_RetrieveDealerSalesOrganizationList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteDealerSalesOrganization"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim dealerSalesOrganization as DealerSalesOrganization = nothing
			while dr.Read
			
				dealerSalesOrganization = me.CreateObject(dr)
			            
			end while        					
			
			return dealerSalesOrganization
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim dealerSalesOrganizationList as ArrayList = new ArrayList
			
			while dr.Read
					dim dealerSalesOrganization as DealerSalesOrganization = me.CreateObject(dr)
					dealerSalesOrganizationList.Add(dealerSalesOrganization)
			end while
			     
			return dealerSalesOrganizationList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim dealerSalesOrganization as DealerSalesOrganization = ctype(obj, DealerSalesOrganization)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int16,dealerSalesOrganization.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim dealerSalesOrganization as DealerSalesOrganization = ctype(obj, DealerSalesOrganization)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int16,2)
			DbCommandWrapper.AddInParameter("@DealerID",DbType.Int16,dealerSalesOrganization.DealerID)
			DbCommandWrapper.AddInParameter("@SalesOrganizationCode",DbType.AnsiString,dealerSalesOrganization.SalesOrganizationCode)
			DbCommandWrapper.AddInParameter("@DistributionChannel",DbType.AnsiString,dealerSalesOrganization.DistributionChannel)
			DbCommandWrapper.AddInParameter("@SalesDistrict",DbType.AnsiString,dealerSalesOrganization.SalesDistrict)
			DbCommandWrapper.AddInParameter("@CustomerGroup",DbType.AnsiString,dealerSalesOrganization.CustomerGroup)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,dealerSalesOrganization.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,dealerSalesOrganization.LastUpdateBy)
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
		
			dim dealerSalesOrganization as DealerSalesOrganization = ctype(obj, DealerSalesOrganization)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int16,dealerSalesOrganization.ID)
			DbCommandWrapper.AddInParameter("@DealerID",DbType.Int16,dealerSalesOrganization.DealerID)
			DbCommandWrapper.AddInParameter("@SalesOrganizationCode",DbType.AnsiString,dealerSalesOrganization.SalesOrganizationCode)
			DbCommandWrapper.AddInParameter("@DistributionChannel",DbType.AnsiString,dealerSalesOrganization.DistributionChannel)
			DbCommandWrapper.AddInParameter("@SalesDistrict",DbType.AnsiString,dealerSalesOrganization.SalesDistrict)
			DbCommandWrapper.AddInParameter("@CustomerGroup",DbType.AnsiString,dealerSalesOrganization.CustomerGroup)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,dealerSalesOrganization.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,dealerSalesOrganization.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as DealerSalesOrganization
		
			dim dealerSalesOrganization as DealerSalesOrganization = new DealerSalesOrganization
			
			dealerSalesOrganization.ID = ctype(dr("ID"), short) 
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then dealerSalesOrganization.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then dealerSalesOrganization.Dealer = New Dealer(CType(dr("DealerID"), Short))
			if not dr.IsDBNull(dr.GetOrdinal("SalesOrganizationCode")) then dealerSalesOrganization.SalesOrganizationCode = dr("SalesOrganizationCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DistributionChannel")) then dealerSalesOrganization.DistributionChannel = dr("DistributionChannel").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("SalesDistrict")) then dealerSalesOrganization.SalesDistrict = dr("SalesDistrict").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CustomerGroup")) then dealerSalesOrganization.CustomerGroup = dr("CustomerGroup").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then dealerSalesOrganization.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then dealerSalesOrganization.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then dealerSalesOrganization.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then dealerSalesOrganization.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then dealerSalesOrganization.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return dealerSalesOrganization
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (DealerSalesOrganization) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(DealerSalesOrganization),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(DealerSalesOrganization).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace
