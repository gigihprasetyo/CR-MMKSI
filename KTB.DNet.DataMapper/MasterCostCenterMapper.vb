
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MasterCostCenter Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/10/2019 - 9:15:04
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

	public class MasterCostCenterMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMasterCostCenter"
		private m_UpdateStatement as string = "up_UpdateMasterCostCenter"
		private m_RetrieveStatement as string = "up_RetrieveMasterCostCenter"
		private m_RetrieveListStatement as string = "up_RetrieveMasterCostCenterList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMasterCostCenter"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim masterCostCenter as MasterCostCenter = nothing
			while dr.Read
			
				masterCostCenter = me.CreateObject(dr)
			            
			end while        					
			
			return masterCostCenter
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim masterCostCenterList as ArrayList = new ArrayList
			
			while dr.Read
					dim masterCostCenter as MasterCostCenter = me.CreateObject(dr)
					masterCostCenterList.Add(masterCostCenter)
			end while
			     
			return masterCostCenterList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim masterCostCenter as MasterCostCenter = ctype(obj, MasterCostCenter)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,masterCostCenter.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim masterCostCenter as MasterCostCenter = ctype(obj, MasterCostCenter)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@BussinessAreaCode",DbType.AnsiString,masterCostCenter.BussinessAreaCode)
			DbCommandWrapper.AddInParameter("@CostCenterCode",DbType.AnsiString,masterCostCenter.CostCenterCode)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,masterCostCenter.Description)
			DbCommandWrapper.AddInParameter("@Type",DbType.AnsiString,masterCostCenter.Type)
            DbCommandWrapper.AddInParameter("@Value", DbType.AnsiString, masterCostCenter.Value)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, masterCostCenter.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,masterCostCenter.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,masterCostCenter.LastUpdateBy)
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
		
			dim masterCostCenter as MasterCostCenter = ctype(obj, MasterCostCenter)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,masterCostCenter.ID)
			DbCommandWrapper.AddInParameter("@BussinessAreaCode",DbType.AnsiString,masterCostCenter.BussinessAreaCode)
			DbCommandWrapper.AddInParameter("@CostCenterCode",DbType.AnsiString,masterCostCenter.CostCenterCode)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,masterCostCenter.Description)
			DbCommandWrapper.AddInParameter("@Type",DbType.AnsiString,masterCostCenter.Type)
            DbCommandWrapper.AddInParameter("@Value", DbType.AnsiString, masterCostCenter.Value)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, masterCostCenter.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,masterCostCenter.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,masterCostCenter.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MasterCostCenter
		
			dim masterCostCenter as MasterCostCenter = new MasterCostCenter
			
			masterCostCenter.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("BussinessAreaCode")) then masterCostCenter.BussinessAreaCode = dr("BussinessAreaCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CostCenterCode")) then masterCostCenter.CostCenterCode = dr("CostCenterCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Description")) then masterCostCenter.Description = dr("Description").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Type")) then masterCostCenter.Type = dr("Type").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("Value")) Then masterCostCenter.Value = dr("Value").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then masterCostCenter.Status = dr("Status").ToString
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then masterCostCenter.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then masterCostCenter.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then masterCostCenter.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then masterCostCenter.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then masterCostCenter.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return masterCostCenter
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MasterCostCenter) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MasterCostCenter),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MasterCostCenter).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

