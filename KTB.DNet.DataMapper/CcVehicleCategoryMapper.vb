
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcVehicleCategory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/7/2011 - 10:51:03 AM
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

	public class CcVehicleCategoryMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertCcVehicleCategory"
		private m_UpdateStatement as string = "up_UpdateCcVehicleCategory"
		private m_RetrieveStatement as string = "up_RetrieveCcVehicleCategory"
		private m_RetrieveListStatement as string = "up_RetrieveCcVehicleCategoryList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteCcVehicleCategory"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim ccVehicleCategory as CcVehicleCategory = nothing
			while dr.Read
			
				ccVehicleCategory = me.CreateObject(dr)
			            
			end while        					
			
			return ccVehicleCategory
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim ccVehicleCategoryList as ArrayList = new ArrayList
			
			while dr.Read
					dim ccVehicleCategory as CcVehicleCategory = me.CreateObject(dr)
					ccVehicleCategoryList.Add(ccVehicleCategory)
			end while
			     
			return ccVehicleCategoryList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim ccVehicleCategory as CcVehicleCategory = ctype(obj, CcVehicleCategory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int16,ccVehicleCategory.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim ccVehicleCategory as CcVehicleCategory = ctype(obj, CcVehicleCategory)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
			DbCommandWrapper.AddInParameter("@Code",DbType.AnsiString,ccVehicleCategory.Code)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,ccVehicleCategory.Description)
			DbCommandWrapper.AddInParameter("@AliasDescription",DbType.AnsiString,ccVehicleCategory.AliasDescription)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,ccVehicleCategory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,ccVehicleCategory.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(ccVehicleCategory.ProductCategory))
						
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
		
			dim ccVehicleCategory as CcVehicleCategory = ctype(obj, CcVehicleCategory)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int16,ccVehicleCategory.ID)
			DbCommandWrapper.AddInParameter("@Code",DbType.AnsiString,ccVehicleCategory.Code)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,ccVehicleCategory.Description)
			DbCommandWrapper.AddInParameter("@AliasDescription",DbType.AnsiString,ccVehicleCategory.AliasDescription)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,ccVehicleCategory.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,ccVehicleCategory.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(ccVehicleCategory.ProductCategory))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as CcVehicleCategory
		
			dim ccVehicleCategory as CcVehicleCategory = new CcVehicleCategory
			
			ccVehicleCategory.ID = ctype(dr("ID"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("Code")) then ccVehicleCategory.Code = dr("Code").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Description")) then ccVehicleCategory.Description = dr("Description").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("AliasDescription")) then ccVehicleCategory.AliasDescription = dr("AliasDescription").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then ccVehicleCategory.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then ccVehicleCategory.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then ccVehicleCategory.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then ccVehicleCategory.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then ccVehicleCategory.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                ccVehicleCategory.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If
			return ccVehicleCategory
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (CcVehicleCategory) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(CcVehicleCategory),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(CcVehicleCategory).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

