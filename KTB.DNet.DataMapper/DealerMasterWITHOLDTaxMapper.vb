
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerMasterWITHOLDTax Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 26/05/2020 - 23:21:54
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

	public class DealerMasterWITHOLDTaxMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertDealerMasterWITHOLDTax"
		private m_UpdateStatement as string = "up_UpdateDealerMasterWITHOLDTax"
		private m_RetrieveStatement as string = "up_RetrieveDealerMasterWITHOLDTax"
		private m_RetrieveListStatement as string = "up_RetrieveDealerMasterWITHOLDTaxList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteDealerMasterWITHOLDTax"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim dealerMasterWITHOLDTax as DealerMasterWITHOLDTax = nothing
			while dr.Read
			
				dealerMasterWITHOLDTax = me.CreateObject(dr)
			            
			end while        					
			
			return dealerMasterWITHOLDTax
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim dealerMasterWITHOLDTaxList as ArrayList = new ArrayList
			
			while dr.Read
					dim dealerMasterWITHOLDTax as DealerMasterWITHOLDTax = me.CreateObject(dr)
					dealerMasterWITHOLDTaxList.Add(dealerMasterWITHOLDTax)
			end while
			     
			return dealerMasterWITHOLDTaxList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim dealerMasterWITHOLDTax as DealerMasterWITHOLDTax = ctype(obj, DealerMasterWITHOLDTax)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,dealerMasterWITHOLDTax.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim dealerMasterWITHOLDTax as DealerMasterWITHOLDTax = ctype(obj, DealerMasterWITHOLDTax)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@WithholdTaxCode",DbType.AnsiString,dealerMasterWITHOLDTax.WithholdTaxCode)
			DbCommandWrapper.AddInParameter("@WithholdTaxtipe",DbType.AnsiString,dealerMasterWITHOLDTax.WithholdTaxtipe)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,dealerMasterWITHOLDTax.Description)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,dealerMasterWITHOLDTax.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,dealerMasterWITHOLDTax.LastUpdateBy)
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
		
			dim dealerMasterWITHOLDTax as DealerMasterWITHOLDTax = ctype(obj, DealerMasterWITHOLDTax)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,dealerMasterWITHOLDTax.ID)
			DbCommandWrapper.AddInParameter("@WithholdTaxCode",DbType.AnsiString,dealerMasterWITHOLDTax.WithholdTaxCode)
			DbCommandWrapper.AddInParameter("@WithholdTaxtipe",DbType.AnsiString,dealerMasterWITHOLDTax.WithholdTaxtipe)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,dealerMasterWITHOLDTax.Description)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,dealerMasterWITHOLDTax.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,dealerMasterWITHOLDTax.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as DealerMasterWITHOLDTax
		
			dim dealerMasterWITHOLDTax as DealerMasterWITHOLDTax = new DealerMasterWITHOLDTax
			
			dealerMasterWITHOLDTax.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("WithholdTaxCode")) then dealerMasterWITHOLDTax.WithholdTaxCode = dr("WithholdTaxCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("WithholdTaxtipe")) then dealerMasterWITHOLDTax.WithholdTaxtipe = dr("WithholdTaxtipe").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Description")) then dealerMasterWITHOLDTax.Description = dr("Description").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then dealerMasterWITHOLDTax.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then dealerMasterWITHOLDTax.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then dealerMasterWITHOLDTax.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then dealerMasterWITHOLDTax.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then dealerMasterWITHOLDTax.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return dealerMasterWITHOLDTax
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (DealerMasterWITHOLDTax) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(DealerMasterWITHOLDTax),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(DealerMasterWITHOLDTax).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace
