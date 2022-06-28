
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOScheduleDealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 08/03/2016 - 11:47:45
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

	public class SparePartPOScheduleDealerMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSparePartPOScheduleDealer"
		private m_UpdateStatement as string = "up_UpdateSparePartPOScheduleDealer"
		private m_RetrieveStatement as string = "up_RetrieveSparePartPOScheduleDealer"
		private m_RetrieveListStatement as string = "up_RetrieveSparePartPOScheduleDealerList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSparePartPOScheduleDealer"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sparePartPOScheduleDealer as SparePartPOScheduleDealer = nothing
			while dr.Read
			
				sparePartPOScheduleDealer = me.CreateObject(dr)
			            
			end while        					
			
			return sparePartPOScheduleDealer
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sparePartPOScheduleDealerList as ArrayList = new ArrayList
			
			while dr.Read
					dim sparePartPOScheduleDealer as SparePartPOScheduleDealer = me.CreateObject(dr)
					sparePartPOScheduleDealerList.Add(sparePartPOScheduleDealer)
			end while
			     
			return sparePartPOScheduleDealerList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sparePartPOScheduleDealer as SparePartPOScheduleDealer = ctype(obj, SparePartPOScheduleDealer)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sparePartPOScheduleDealer.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sparePartPOScheduleDealer as SparePartPOScheduleDealer = ctype(obj, SparePartPOScheduleDealer)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sparePartPOScheduleDealer.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sparePartPOScheduleDealer.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparePartPOScheduleDealer.Dealer))
            DbCommandWrapper.AddInParameter("@SparePartPOScheduleID", DbType.Int32, Me.GetRefObject(sparePartPOScheduleDealer.SparePartPOSchedule))
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
		
			dim sparePartPOScheduleDealer as SparePartPOScheduleDealer = ctype(obj, SparePartPOScheduleDealer)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sparePartPOScheduleDealer.ID)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sparePartPOScheduleDealer.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sparePartPOScheduleDealer.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
			DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparePartPOScheduleDealer.Dealer))
            DbCommandWrapper.AddInParameter("@SparePartPOScheduleID", DbType.Int32, Me.GetRefObject(sparePartPOScheduleDealer.SparePartPOSchedule))
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SparePartPOScheduleDealer
		
			dim sparePartPOScheduleDealer as SparePartPOScheduleDealer = new SparePartPOScheduleDealer
			
			sparePartPOScheduleDealer.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sparePartPOScheduleDealer.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sparePartPOScheduleDealer.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sparePartPOScheduleDealer.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sparePartPOScheduleDealer.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sparePartPOScheduleDealer.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("DealerID")) then
                sparePartPOScheduleDealer.Dealer = New Dealer(CType(dr("DealerID"), Short))
			end if
			if not dr.IsDBNull(dr.GetOrdinal("SparePartPOScheduleID")) then
                sparePartPOScheduleDealer.SparePartPOSchedule = New SparePartPOSchedule(CType(dr("SparePartPOScheduleID"), Integer))
			end if
			
			return sparePartPOScheduleDealer
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SparePartPOScheduleDealer) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SparePartPOScheduleDealer),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SparePartPOScheduleDealer).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

