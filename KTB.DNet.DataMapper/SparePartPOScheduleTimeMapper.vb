
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOScheduleTime Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 08/03/2016 - 11:48:28
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

	public class SparePartPOScheduleTimeMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSparePartPOScheduleTime"
		private m_UpdateStatement as string = "up_UpdateSparePartPOScheduleTime"
		private m_RetrieveStatement as string = "up_RetrieveSparePartPOScheduleTime"
		private m_RetrieveListStatement as string = "up_RetrieveSparePartPOScheduleTimeList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSparePartPOScheduleTime"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sparePartPOScheduleTime as SparePartPOScheduleTime = nothing
			while dr.Read
			
				sparePartPOScheduleTime = me.CreateObject(dr)
			            
			end while        					
			
			return sparePartPOScheduleTime
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sparePartPOScheduleTimeList as ArrayList = new ArrayList
			
			while dr.Read
					dim sparePartPOScheduleTime as SparePartPOScheduleTime = me.CreateObject(dr)
					sparePartPOScheduleTimeList.Add(sparePartPOScheduleTime)
			end while
			     
			return sparePartPOScheduleTimeList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sparePartPOScheduleTime as SparePartPOScheduleTime = ctype(obj, SparePartPOScheduleTime)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sparePartPOScheduleTime.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sparePartPOScheduleTime as SparePartPOScheduleTime = ctype(obj, SparePartPOScheduleTime)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@ScheduleTime", DbType.DateTime, sparePartPOScheduleTime.ScheduleTime)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, sparePartPOScheduleTime.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sparePartPOScheduleTime.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sparePartPOScheduleTime.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartPOScheduleID", DbType.Int32, Me.GetRefObject(sparePartPOScheduleTime.SparePartPOSchedule))

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
		
			dim sparePartPOScheduleTime as SparePartPOScheduleTime = ctype(obj, SparePartPOScheduleTime)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sparePartPOScheduleTime.ID)
            DbCommandWrapper.AddInParameter("@ScheduleTime", DbType.DateTime, sparePartPOScheduleTime.ScheduleTime)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, sparePartPOScheduleTime.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sparePartPOScheduleTime.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sparePartPOScheduleTime.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						

            DbCommandWrapper.AddInParameter("@SparePartPOScheduleID", DbType.Int32, Me.GetRefObject(sparePartPOScheduleTime.SparePartPOSchedule))

						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SparePartPOScheduleTime
		
			dim sparePartPOScheduleTime as SparePartPOScheduleTime = new SparePartPOScheduleTime
			
			sparePartPOScheduleTime.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("ScheduleTime")) Then sparePartPOScheduleTime.ScheduleTime = CType(dr("ScheduleTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartPOScheduleTime.Status = CType(dr("Status"), Boolean)
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sparePartPOScheduleTime.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sparePartPOScheduleTime.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sparePartPOScheduleTime.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sparePartPOScheduleTime.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sparePartPOScheduleTime.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("SparePartPOScheduleID")) then
                sparePartPOScheduleTime.SparePartPOSchedule = New SparePartPOSchedule(CType(dr("SparePartPOScheduleID"), Integer))
			end if
			
			return sparePartPOScheduleTime
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SparePartPOScheduleTime) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SparePartPOScheduleTime),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SparePartPOScheduleTime).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

