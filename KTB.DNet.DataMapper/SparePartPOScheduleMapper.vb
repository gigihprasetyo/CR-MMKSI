
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOSchedule Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 08/03/2016 - 11:45:03
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

	public class SparePartPOScheduleMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSparePartPOSchedule"
		private m_UpdateStatement as string = "up_UpdateSparePartPOSchedule"
		private m_RetrieveStatement as string = "up_RetrieveSparePartPOSchedule"
		private m_RetrieveListStatement as string = "up_RetrieveSparePartPOScheduleList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSparePartPOSchedule"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sparePartPOSchedule as SparePartPOSchedule = nothing
			while dr.Read
			
				sparePartPOSchedule = me.CreateObject(dr)
			            
			end while        					
			
			return sparePartPOSchedule
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sparePartPOScheduleList as ArrayList = new ArrayList
			
			while dr.Read
					dim sparePartPOSchedule as SparePartPOSchedule = me.CreateObject(dr)
					sparePartPOScheduleList.Add(sparePartPOSchedule)
			end while
			     
			return sparePartPOScheduleList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sparePartPOSchedule as SparePartPOSchedule = ctype(obj, SparePartPOSchedule)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sparePartPOSchedule.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sparePartPOSchedule as SparePartPOSchedule = ctype(obj, SparePartPOSchedule)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@OrderType",DbType.AnsiString,sparePartPOSchedule.OrderType)
            DbCommandWrapper.AddInParameter("@OrderDay", DbType.Int16, sparePartPOSchedule.OrderDay)
            DbCommandWrapper.AddInParameter("@Status", DbType.Boolean, sparePartPOSchedule.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sparePartPOSchedule.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sparePartPOSchedule.LastUpdateBy)
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
		
			dim sparePartPOSchedule as SparePartPOSchedule = ctype(obj, SparePartPOSchedule)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sparePartPOSchedule.ID)
			DbCommandWrapper.AddInParameter("@OrderType",DbType.AnsiString,sparePartPOSchedule.OrderType)
            DbCommandWrapper.AddInParameter("@OrderDay", DbType.Int16, sparePartPOSchedule.OrderDay)
            DbCommandWrapper.AddInParameter("@Status", DbType.Boolean, sparePartPOSchedule.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sparePartPOSchedule.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sparePartPOSchedule.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SparePartPOSchedule
		
			dim sparePartPOSchedule as SparePartPOSchedule = new SparePartPOSchedule
			
			sparePartPOSchedule.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("OrderType")) then sparePartPOSchedule.OrderType = dr("OrderType").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("OrderDay")) Then sparePartPOSchedule.OrderDay = CType(dr("OrderDay"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartPOSchedule.Status = CType(dr("Status"), Short)

			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sparePartPOSchedule.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sparePartPOSchedule.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sparePartPOSchedule.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sparePartPOSchedule.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sparePartPOSchedule.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sparePartPOSchedule
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SparePartPOSchedule) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SparePartPOSchedule),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SparePartPOSchedule).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
#Region "Custom Method"

#End Region
		
end class
end namespace

