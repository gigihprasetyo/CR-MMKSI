
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanPartHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/17/2011 - 4:08:38 PM
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

	public class SalesmanPartHistoryMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSalesmanPartHistory"
		private m_UpdateStatement as string = "up_UpdateSalesmanPartHistory"
		private m_RetrieveStatement as string = "up_RetrieveSalesmanPartHistory"
		private m_RetrieveListStatement as string = "up_RetrieveSalesmanPartHistoryList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSalesmanPartHistory"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim salesmanPartHistory as SalesmanPartHistory = nothing
			while dr.Read
			
				salesmanPartHistory = me.CreateObject(dr)
			            
			end while        					
			
			return salesmanPartHistory
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim salesmanPartHistoryList as ArrayList = new ArrayList
			
			while dr.Read
					dim salesmanPartHistory as SalesmanPartHistory = me.CreateObject(dr)
					salesmanPartHistoryList.Add(salesmanPartHistory)
			end while
			     
			return salesmanPartHistoryList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim salesmanPartHistory as SalesmanPartHistory = ctype(obj, SalesmanPartHistory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,salesmanPartHistory.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim salesmanPartHistory as SalesmanPartHistory = ctype(obj, SalesmanPartHistory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            'DbCommandWrapper.AddInParameter("@SalesmanHeaderID",DbType.Int32,salesmanPartHistory.SalesmanHeaderID)
            'DbCommandWrapper.AddInParameter("@SalesmanCategoryLevelID",DbType.Int32,salesmanPartHistory.SalesmanCategoryLevelID)
            DBCommandWrapper.AddInParameter("@SalesmanLevel", DbType.Int32, salesmanPartHistory.SalesmanLevel)
            DBCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, salesmanPartHistory.SalesmanCode)
			DbCommandWrapper.AddInParameter("@ChangedDate",DbType.DateTime,salesmanPartHistory.ChangedDate)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int32,salesmanPartHistory.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,salesmanPartHistory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,salesmanPartHistory.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, salesmanPartHistory.SalesmanHeader.ID)
            DBCommandWrapper.AddInParameter("@SalesmanCategoryLevelID", DbType.Int32, salesmanPartHistory.SalesmanCategoryLevel.ID)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, salesmanPartHistory.Dealer.ID)
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
		
			dim salesmanPartHistory as SalesmanPartHistory = ctype(obj, SalesmanPartHistory)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,salesmanPartHistory.ID)
            'DbCommandWrapper.AddInParameter("@SalesmanHeaderID",DbType.Int32,salesmanPartHistory.SalesmanHeaderID)
            'DbCommandWrapper.AddInParameter("@SalesmanCategoryLevelID",DbType.Int32,salesmanPartHistory.SalesmanCategoryLevelID)
            DBCommandWrapper.AddInParameter("@SalesmanLevel", DbType.Int32, salesmanPartHistory.SalesmanLevel)
            DBCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, salesmanPartHistory.SalesmanCode)
			DbCommandWrapper.AddInParameter("@ChangedDate",DbType.DateTime,salesmanPartHistory.ChangedDate)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int32,salesmanPartHistory.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,salesmanPartHistory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,salesmanPartHistory.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, salesmanPartHistory.SalesmanHeader.ID)
            DBCommandWrapper.AddInParameter("@SalesmanCategoryLevelID", DbType.Int32, salesmanPartHistory.SalesmanCategoryLevel.ID)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, salesmanPartHistory.Dealer.ID)
            

            Return DBCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SalesmanPartHistory
		
			dim salesmanPartHistory as SalesmanPartHistory = new SalesmanPartHistory
			
			salesmanPartHistory.ID = ctype(dr("ID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) then salesmanPartHistory.SalesmanHeaderID = ctype(dr("SalesmanHeaderID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("SalesmanCategoryLevelID")) then salesmanPartHistory.SalesmanCategoryLevelID = ctype(dr("SalesmanCategoryLevelID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanLevel")) Then salesmanPartHistory.SalesmanLevel = CType(dr("SalesmanLevel"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then salesmanPartHistory.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChangedDate")) Then salesmanPartHistory.ChangedDate = CType(dr("ChangedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then salesmanPartHistory.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanPartHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanPartHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanPartHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanPartHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanPartHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanPartHistory.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCategoryLevelID")) Then
                salesmanPartHistory.SalesmanCategoryLevel = New SalesmanCategoryLevel(CType(dr("SalesmanCategoryLevelID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                salesmanPartHistory.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            Return salesmanPartHistory
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SalesmanPartHistory) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SalesmanPartHistory),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SalesmanPartHistory).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

