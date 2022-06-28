
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MasterAccrued Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/10/2019 - 9:15:17
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

	public class MasterAccruedMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMasterAccrued"
		private m_UpdateStatement as string = "up_UpdateMasterAccrued"
		private m_RetrieveStatement as string = "up_RetrieveMasterAccrued"
		private m_RetrieveListStatement as string = "up_RetrieveMasterAccruedList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMasterAccrued"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim masterAccrued as MasterAccrued = nothing
			while dr.Read
			
				masterAccrued = me.CreateObject(dr)
			            
			end while        					
			
			return masterAccrued
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim masterAccruedList as ArrayList = new ArrayList
			
			while dr.Read
					dim masterAccrued as MasterAccrued = me.CreateObject(dr)
					masterAccruedList.Add(masterAccrued)
			end while
			     
			return masterAccruedList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim masterAccrued as MasterAccrued = ctype(obj, MasterAccrued)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,masterAccrued.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim masterAccrued as MasterAccrued = ctype(obj, MasterAccrued)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@BussinessAreaCode",DbType.AnsiString,masterAccrued.BussinessAreaCode)
			DbCommandWrapper.AddInParameter("@AccKey",DbType.AnsiString,masterAccrued.AccKey)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,masterAccrued.Description)
			DbCommandWrapper.AddInParameter("@Type",DbType.AnsiString,masterAccrued.Type)
			DbCommandWrapper.AddInParameter("@Status",DbType.AnsiString,masterAccrued.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,masterAccrued.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,masterAccrued.LastUpdateBy)
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
		
			dim masterAccrued as MasterAccrued = ctype(obj, MasterAccrued)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,masterAccrued.ID)
			DbCommandWrapper.AddInParameter("@BussinessAreaCode",DbType.AnsiString,masterAccrued.BussinessAreaCode)
			DbCommandWrapper.AddInParameter("@AccKey",DbType.AnsiString,masterAccrued.AccKey)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,masterAccrued.Description)
			DbCommandWrapper.AddInParameter("@Type",DbType.AnsiString,masterAccrued.Type)
			DbCommandWrapper.AddInParameter("@Status",DbType.AnsiString,masterAccrued.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,masterAccrued.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,masterAccrued.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MasterAccrued
		
			dim masterAccrued as MasterAccrued = new MasterAccrued
			
			masterAccrued.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("BussinessAreaCode")) then masterAccrued.BussinessAreaCode = dr("BussinessAreaCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("AccKey")) then masterAccrued.AccKey = dr("AccKey").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Description")) then masterAccrued.Description = dr("Description").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Type")) then masterAccrued.Type = dr("Type").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then masterAccrued.Status = dr("Status").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then masterAccrued.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then masterAccrued.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then masterAccrued.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then masterAccrued.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then masterAccrued.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("MasterCostCenterID")) Then
                masterAccrued.MasterCostCenter = New MasterCostCenter(CType(dr("MasterCostCenterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("MasterInternalOrderID")) Then
                masterAccrued.MasterInternalOrder = New MasterInternalOrder(CType(dr("MasterInternalOrderID"), Integer))
            End If

            Return masterAccrued
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MasterAccrued) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MasterAccrued),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MasterAccrued).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

