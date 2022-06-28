
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitPameranExpense Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 05/16/2019 - 2:19:49 PM
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

	public class BabitPameranExpenseMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertBabitPameranExpense"
		private m_UpdateStatement as string = "up_UpdateBabitPameranExpense"
		private m_RetrieveStatement as string = "up_RetrieveBabitPameranExpense"
		private m_RetrieveListStatement as string = "up_RetrieveBabitPameranExpenseList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteBabitPameranExpense"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim babitPameranExpense as BabitPameranExpense = nothing
			while dr.Read
			
				babitPameranExpense = me.CreateObject(dr)
			            
			end while        					
			
			return babitPameranExpense
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim babitPameranExpenseList as ArrayList = new ArrayList
			
			while dr.Read
					dim babitPameranExpense as BabitPameranExpense = me.CreateObject(dr)
					babitPameranExpenseList.Add(babitPameranExpense)
			end while
			     
			return babitPameranExpenseList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim babitPameranExpense as BabitPameranExpense = ctype(obj, BabitPameranExpense)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,babitPameranExpense.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim babitPameranExpense as BabitPameranExpense = ctype(obj, BabitPameranExpense)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            'DbCommandWrapper.AddInParameter("@BabitHeaderID",DbType.Int32,babitPameranExpense.BabitHeaderID)
            'DbCommandWrapper.AddInParameter("@BabitParameterDetailID",DbType.Int32,babitPameranExpense.BabitParameterDetailID)
			DbCommandWrapper.AddInParameter("@Item",DbType.AnsiString,babitPameranExpense.Item)
			DbCommandWrapper.AddInParameter("@Qty",DbType.Int32,babitPameranExpense.Qty)
			DbCommandWrapper.AddInParameter("@Price",DbType.Currency,babitPameranExpense.Price)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,babitPameranExpense.Description)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,babitPameranExpense.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,babitPameranExpense.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitHeaderID", DbType.Int32, Me.GetRefObject(babitPameranExpense.BabitHeader))
            DbCommandWrapper.AddInParameter("@BabitParameterDetailID", DbType.Int32, Me.GetRefObject(babitPameranExpense.BabitParameterDetail))
						
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
		
			dim babitPameranExpense as BabitPameranExpense = ctype(obj, BabitPameranExpense)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,babitPameranExpense.ID)
            'DbCommandWrapper.AddInParameter("@BabitHeaderID",DbType.Int32,babitPameranExpense.BabitHeaderID)
            'DbCommandWrapper.AddInParameter("@BabitParameterDetailID",DbType.Int32,babitPameranExpense.BabitParameterDetailID)
			DbCommandWrapper.AddInParameter("@Item",DbType.AnsiString,babitPameranExpense.Item)
			DbCommandWrapper.AddInParameter("@Qty",DbType.Int32,babitPameranExpense.Qty)
			DbCommandWrapper.AddInParameter("@Price",DbType.Currency,babitPameranExpense.Price)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,babitPameranExpense.Description)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,babitPameranExpense.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,babitPameranExpense.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
            DbCommandWrapper.AddInParameter("@BabitHeaderID", DbType.Int32, Me.GetRefObject(babitPameranExpense.BabitHeader))
            DbCommandWrapper.AddInParameter("@BabitParameterDetailID", DbType.Int32, Me.GetRefObject(babitPameranExpense.BabitParameterDetail))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as BabitPameranExpense
		
			dim babitPameranExpense as BabitPameranExpense = new BabitPameranExpense
			
			babitPameranExpense.ID = ctype(dr("ID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("BabitHeaderID")) then babitPameranExpense.BabitHeaderID = ctype(dr("BabitHeaderID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("BabitParameterDetailID")) then babitPameranExpense.BabitParameterDetailID = ctype(dr("BabitParameterDetailID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Item")) then babitPameranExpense.Item = dr("Item").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Qty")) then babitPameranExpense.Qty = ctype(dr("Qty"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Price")) then babitPameranExpense.Price = ctype(dr("Price"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("Description")) then babitPameranExpense.Description = dr("Description").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then babitPameranExpense.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then babitPameranExpense.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then babitPameranExpense.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then babitPameranExpense.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then babitPameranExpense.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("BabitHeaderID")) Then
                babitPameranExpense.BabitHeader = New BabitHeader(CType(dr("BabitHeaderID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitParameterDetailID")) Then
                babitPameranExpense.BabitParameterDetail = New BabitParameterDetail(CType(dr("BabitParameterDetailID"), Short))
            End If

			return babitPameranExpense
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (BabitPameranExpense) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(BabitPameranExpense),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(BabitPameranExpense).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

