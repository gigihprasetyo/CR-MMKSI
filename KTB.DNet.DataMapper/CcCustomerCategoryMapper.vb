
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCustomerCategory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2011 - 11:03:54 AM
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

	public class CcCustomerCategoryMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertCcCustomerCategory"
		private m_UpdateStatement as string = "up_UpdateCcCustomerCategory"
		private m_RetrieveStatement as string = "up_RetrieveCcCustomerCategory"
		private m_RetrieveListStatement as string = "up_RetrieveCcCustomerCategoryList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteCcCustomerCategory"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim ccCustomerCategory as CcCustomerCategory = nothing
			while dr.Read
			
				ccCustomerCategory = me.CreateObject(dr)
			            
			end while        					
			
			return ccCustomerCategory
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim ccCustomerCategoryList as ArrayList = new ArrayList
			
			while dr.Read
					dim ccCustomerCategory as CcCustomerCategory = me.CreateObject(dr)
					ccCustomerCategoryList.Add(ccCustomerCategory)
			end while
			     
			return ccCustomerCategoryList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim ccCustomerCategory as CcCustomerCategory = ctype(obj, CcCustomerCategory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int16,ccCustomerCategory.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim ccCustomerCategory as CcCustomerCategory = ctype(obj, CcCustomerCategory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int16,2)
			DbCommandWrapper.AddInParameter("@Code",DbType.AnsiString,ccCustomerCategory.Code)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,ccCustomerCategory.Description)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,ccCustomerCategory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,ccCustomerCategory.LastUpdateBy)
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
		
			dim ccCustomerCategory as CcCustomerCategory = ctype(obj, CcCustomerCategory)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int16,ccCustomerCategory.ID)
			DbCommandWrapper.AddInParameter("@Code",DbType.AnsiString,ccCustomerCategory.Code)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,ccCustomerCategory.Description)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,ccCustomerCategory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,ccCustomerCategory.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as CcCustomerCategory
		
			dim ccCustomerCategory as CcCustomerCategory = new CcCustomerCategory
			
			ccCustomerCategory.ID = ctype(dr("ID"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("Code")) then ccCustomerCategory.Code = dr("Code").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Description")) then ccCustomerCategory.Description = dr("Description").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then ccCustomerCategory.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then ccCustomerCategory.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then ccCustomerCategory.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then ccCustomerCategory.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then ccCustomerCategory.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return ccCustomerCategory
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (CcCustomerCategory) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(CcCustomerCategory),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(CcCustomerCategory).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

