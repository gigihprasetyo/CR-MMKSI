
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetHasilSurveyHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 8/31/2017 - 1:41:10 PM
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

	public class FleetHasilSurveyHeaderMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertFleetHasilSurveyHeader"
		private m_UpdateStatement as string = "up_UpdateFleetHasilSurveyHeader"
		private m_RetrieveStatement as string = "up_RetrieveFleetHasilSurveyHeader"
		private m_RetrieveListStatement as string = "up_RetrieveFleetHasilSurveyHeaderList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteFleetHasilSurveyHeader"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim fleetHasilSurveyHeader as FleetHasilSurveyHeader = nothing
			while dr.Read
			
				fleetHasilSurveyHeader = me.CreateObject(dr)
			            
			end while        					
			
			return fleetHasilSurveyHeader
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim fleetHasilSurveyHeaderList as ArrayList = new ArrayList
			
			while dr.Read
					dim fleetHasilSurveyHeader as FleetHasilSurveyHeader = me.CreateObject(dr)
					fleetHasilSurveyHeaderList.Add(fleetHasilSurveyHeader)
			end while
			     
			return fleetHasilSurveyHeaderList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim fleetHasilSurveyHeader as FleetHasilSurveyHeader = ctype(obj, FleetHasilSurveyHeader)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,fleetHasilSurveyHeader.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim fleetHasilSurveyHeader as FleetHasilSurveyHeader = ctype(obj, FleetHasilSurveyHeader)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@SurveyDate",DbType.DateTime,fleetHasilSurveyHeader.SurveyDate)
			DbCommandWrapper.AddInParameter("@SurveyPerson",DbType.AnsiString,fleetHasilSurveyHeader.SurveyPerson)
			DbCommandWrapper.AddInParameter("@SurveyOccupation",DbType.AnsiString,fleetHasilSurveyHeader.SurveyOccupation)
			DbCommandWrapper.AddInParameter("@CustomerOccupation",DbType.AnsiString,fleetHasilSurveyHeader.CustomerOccupation)
			DbCommandWrapper.AddInParameter("@SuggestionSales",DbType.AnsiString,fleetHasilSurveyHeader.SuggestionSales)
			DbCommandWrapper.AddInParameter("@SuggestionService",DbType.AnsiString,fleetHasilSurveyHeader.SuggestionService)
			DbCommandWrapper.AddInParameter("@SuggestionSparepart",DbType.AnsiString,fleetHasilSurveyHeader.SuggestionSparepart)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,fleetHasilSurveyHeader.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,fleetHasilSurveyHeader.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

			DbCommandWrapper.AddInParameter("@FleetCustomerID",DbType.Int32,fleetHasilSurveyHeader.FleetCustomerID)
			DbCommandWrapper.AddInParameter("@DelaerID",DbType.Int16,fleetHasilSurveyHeader.DelaerID)
			DbCommandWrapper.AddInParameter("@ReceivedByFleetCustomerContactID",DbType.Int32,fleetHasilSurveyHeader.ReceivedByFleetCustomerContactID)
						
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
		
			dim fleetHasilSurveyHeader as FleetHasilSurveyHeader = ctype(obj, FleetHasilSurveyHeader)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,fleetHasilSurveyHeader.ID)
			DbCommandWrapper.AddInParameter("@SurveyDate",DbType.DateTime,fleetHasilSurveyHeader.SurveyDate)
			DbCommandWrapper.AddInParameter("@SurveyPerson",DbType.AnsiString,fleetHasilSurveyHeader.SurveyPerson)
			DbCommandWrapper.AddInParameter("@SurveyOccupation",DbType.AnsiString,fleetHasilSurveyHeader.SurveyOccupation)
			DbCommandWrapper.AddInParameter("@CustomerOccupation",DbType.AnsiString,fleetHasilSurveyHeader.CustomerOccupation)
			DbCommandWrapper.AddInParameter("@SuggestionSales",DbType.AnsiString,fleetHasilSurveyHeader.SuggestionSales)
			DbCommandWrapper.AddInParameter("@SuggestionService",DbType.AnsiString,fleetHasilSurveyHeader.SuggestionService)
			DbCommandWrapper.AddInParameter("@SuggestionSparepart",DbType.AnsiString,fleetHasilSurveyHeader.SuggestionSparepart)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,fleetHasilSurveyHeader.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,fleetHasilSurveyHeader.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)
			
						
			DbCommandWrapper.AddInParameter("@FleetCustomerID",DbType.Int32,fleetHasilSurveyHeader.FleetCustomerID)
			DbCommandWrapper.AddInParameter("@DelaerID",DbType.Int16,fleetHasilSurveyHeader.DelaerID)
			DbCommandWrapper.AddInParameter("@ReceivedByFleetCustomerContactID",DbType.Int32,fleetHasilSurveyHeader.ReceivedByFleetCustomerContactID)
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as FleetHasilSurveyHeader
		
			dim fleetHasilSurveyHeader as FleetHasilSurveyHeader = new FleetHasilSurveyHeader
			
			fleetHasilSurveyHeader.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("SurveyDate")) then fleetHasilSurveyHeader.SurveyDate = ctype(dr("SurveyDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("SurveyPerson")) then fleetHasilSurveyHeader.SurveyPerson = dr("SurveyPerson").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("SurveyOccupation")) then fleetHasilSurveyHeader.SurveyOccupation = dr("SurveyOccupation").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CustomerOccupation")) then fleetHasilSurveyHeader.CustomerOccupation = dr("CustomerOccupation").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("SuggestionSales")) then fleetHasilSurveyHeader.SuggestionSales = dr("SuggestionSales").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("SuggestionService")) then fleetHasilSurveyHeader.SuggestionService = dr("SuggestionService").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("SuggestionSparepart")) then fleetHasilSurveyHeader.SuggestionSparepart = dr("SuggestionSparepart").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then fleetHasilSurveyHeader.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then fleetHasilSurveyHeader.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then fleetHasilSurveyHeader.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then fleetHasilSurveyHeader.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then fleetHasilSurveyHeader.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("FleetCustomerID")) then
				fleetHasilSurveyHeader.FleetCustomerID = ctype(dr("FleetCustomerID"), integer)
			end if
			if not dr.IsDBNull(dr.GetOrdinal("DelaerID")) then
				fleetHasilSurveyHeader.DelaerID = ctype(dr("DelaerID"), short)
			end if
			if not dr.IsDBNull(dr.GetOrdinal("ReceivedByFleetCustomerContactID")) then
				fleetHasilSurveyHeader.ReceivedByFleetCustomerContactID = ctype(dr("ReceivedByFleetCustomerContactID"), integer)
			end if
			
			return fleetHasilSurveyHeader
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (FleetHasilSurveyHeader) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(FleetHasilSurveyHeader),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(FleetHasilSurveyHeader).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

