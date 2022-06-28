
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MasterInternalOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/10/2019 - 9:14:47
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

	public class MasterInternalOrderMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMasterInternalOrder"
		private m_UpdateStatement as string = "up_UpdateMasterInternalOrder"
		private m_RetrieveStatement as string = "up_RetrieveMasterInternalOrder"
		private m_RetrieveListStatement as string = "up_RetrieveMasterInternalOrderList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMasterInternalOrder"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim masterInternalOrder as MasterInternalOrder = nothing
			while dr.Read
			
				masterInternalOrder = me.CreateObject(dr)
			            
			end while        					
			
			return masterInternalOrder
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim masterInternalOrderList as ArrayList = new ArrayList
			
			while dr.Read
					dim masterInternalOrder as MasterInternalOrder = me.CreateObject(dr)
					masterInternalOrderList.Add(masterInternalOrder)
			end while
			     
			return masterInternalOrderList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim masterInternalOrder as MasterInternalOrder = ctype(obj, MasterInternalOrder)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,masterInternalOrder.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim masterInternalOrder as MasterInternalOrder = ctype(obj, MasterInternalOrder)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@BussinessAreaCode",DbType.AnsiString,masterInternalOrder.BussinessAreaCode)
			DbCommandWrapper.AddInParameter("@InternalOrderCode",DbType.AnsiString,masterInternalOrder.InternalOrderCode)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,masterInternalOrder.Description)
			DbCommandWrapper.AddInParameter("@Type",DbType.AnsiString,masterInternalOrder.Type)
            DbCommandWrapper.AddInParameter("@Value", DbType.AnsiString, masterInternalOrder.Value)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, masterInternalOrder.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,masterInternalOrder.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,masterInternalOrder.LastUpdateBy)
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
		
			dim masterInternalOrder as MasterInternalOrder = ctype(obj, MasterInternalOrder)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,masterInternalOrder.ID)
			DbCommandWrapper.AddInParameter("@BussinessAreaCode",DbType.AnsiString,masterInternalOrder.BussinessAreaCode)
			DbCommandWrapper.AddInParameter("@InternalOrderCode",DbType.AnsiString,masterInternalOrder.InternalOrderCode)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,masterInternalOrder.Description)
			DbCommandWrapper.AddInParameter("@Type",DbType.AnsiString,masterInternalOrder.Type)
            DbCommandWrapper.AddInParameter("@Value", DbType.AnsiString, masterInternalOrder.Value)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, masterInternalOrder.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,masterInternalOrder.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,masterInternalOrder.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MasterInternalOrder
		
			dim masterInternalOrder as MasterInternalOrder = new MasterInternalOrder
			
			masterInternalOrder.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("BussinessAreaCode")) then masterInternalOrder.BussinessAreaCode = dr("BussinessAreaCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("InternalOrderCode")) then masterInternalOrder.InternalOrderCode = dr("InternalOrderCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Description")) then masterInternalOrder.Description = dr("Description").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then masterInternalOrder.Type = dr("Type").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Value")) Then masterInternalOrder.Value = dr("Value").ToString
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then masterInternalOrder.Status = dr("Status").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then masterInternalOrder.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then masterInternalOrder.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then masterInternalOrder.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then masterInternalOrder.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then masterInternalOrder.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return masterInternalOrder
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MasterInternalOrder) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MasterInternalOrder),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MasterInternalOrder).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

