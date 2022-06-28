#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartMasterPrice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/13/2020 - 7:31:43 PM
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

	public class SparePartMasterPriceMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSparePartMasterPrice"
		private m_UpdateStatement as string = "up_UpdateSparePartMasterPrice"
		private m_RetrieveStatement as string = "up_RetrieveSparePartMasterPrice"
		private m_RetrieveListStatement as string = "up_RetrieveSparePartMasterPriceList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSparePartMasterPrice"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sparePartMasterPrice as SparePartMasterPrice = nothing
			while dr.Read
			
				sparePartMasterPrice = me.CreateObject(dr)
			            
			end while        					
			
			return sparePartMasterPrice
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sparePartMasterPriceList as ArrayList = new ArrayList
			
			while dr.Read
					dim sparePartMasterPrice as SparePartMasterPrice = me.CreateObject(dr)
					sparePartMasterPriceList.Add(sparePartMasterPrice)
			end while
			     
			return sparePartMasterPriceList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sparePartMasterPrice as SparePartMasterPrice = ctype(obj, SparePartMasterPrice)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sparePartMasterPrice.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sparePartMasterPrice as SparePartMasterPrice = ctype(obj, SparePartMasterPrice)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            'DbCommandWrapper.AddInParameter("@SparePartMasterID",DbType.Int32,sparePartMasterPrice.SparePartMasterID)
			DbCommandWrapper.AddInParameter("@RetailPrice",DbType.Currency,sparePartMasterPrice.RetailPrice)
			DbCommandWrapper.AddInParameter("@ValidFrom",DbType.DateTime,sparePartMasterPrice.ValidFrom)
			DbCommandWrapper.AddInParameter("@ValidTo",DbType.DateTime,sparePartMasterPrice.ValidTo)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,sparePartMasterPrice.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sparePartMasterPrice.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sparePartMasterPrice.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartMasterPrice.SparePartMaster))
						
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
		
			dim sparePartMasterPrice as SparePartMasterPrice = ctype(obj, SparePartMasterPrice)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sparePartMasterPrice.ID)
            'DbCommandWrapper.AddInParameter("@SparePartMasterID",DbType.Int32,sparePartMasterPrice.SparePartMasterID)
			DbCommandWrapper.AddInParameter("@RetailPrice",DbType.Currency,sparePartMasterPrice.RetailPrice)
			DbCommandWrapper.AddInParameter("@ValidFrom",DbType.DateTime,sparePartMasterPrice.ValidFrom)
			DbCommandWrapper.AddInParameter("@ValidTo",DbType.DateTime,sparePartMasterPrice.ValidTo)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,sparePartMasterPrice.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sparePartMasterPrice.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sparePartMasterPrice.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartMasterPrice.SparePartMaster))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SparePartMasterPrice
		
			dim sparePartMasterPrice as SparePartMasterPrice = new SparePartMasterPrice
			
			sparePartMasterPrice.ID = ctype(dr("ID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) then sparePartMasterPrice.SparePartMasterID = ctype(dr("SparePartMasterID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) then sparePartMasterPrice.RetailPrice = ctype(dr("RetailPrice"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) then sparePartMasterPrice.ValidFrom = ctype(dr("ValidFrom"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("ValidTo")) then sparePartMasterPrice.ValidTo = ctype(dr("ValidTo"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then sparePartMasterPrice.Status = ctype(dr("Status"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sparePartMasterPrice.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sparePartMasterPrice.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sparePartMasterPrice.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sparePartMasterPrice.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sparePartMasterPrice.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                sparePartMasterPrice.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If

			return sparePartMasterPrice
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SparePartMasterPrice) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SparePartMasterPrice),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SparePartMasterPrice).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace
