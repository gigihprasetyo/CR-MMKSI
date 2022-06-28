
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcComplainFollowup Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/12/2011 - 11:22:58 AM
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

	public class CcComplainFollowupMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertCcComplainFollowup"
		private m_UpdateStatement as string = "up_UpdateCcComplainFollowup"
		private m_RetrieveStatement as string = "up_RetrieveCcComplainFollowup"
		private m_RetrieveListStatement as string = "up_RetrieveCcComplainFollowupList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteCcComplainFollowup"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim ccComplainFollowup as CcComplainFollowup = nothing
			while dr.Read
			
				ccComplainFollowup = me.CreateObject(dr)
			            
			end while        					
			
			return ccComplainFollowup
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim ccComplainFollowupList as ArrayList = new ArrayList
			
			while dr.Read
					dim ccComplainFollowup as CcComplainFollowup = me.CreateObject(dr)
					ccComplainFollowupList.Add(ccComplainFollowup)
			end while
			     
			return ccComplainFollowupList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim ccComplainFollowup as CcComplainFollowup = ctype(obj, CcComplainFollowup)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,ccComplainFollowup.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim ccComplainFollowup as CcComplainFollowup = ctype(obj, CcComplainFollowup)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,ccComplainFollowup.Status)
			DbCommandWrapper.AddInParameter("@Note",DbType.AnsiString,ccComplainFollowup.Note)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,ccComplainFollowup.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,ccComplainFollowup.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

			DbCommandWrapper.AddInParameter("@CcFollowupID",DbType.Int16,ccComplainFollowup.CcFollowupID)
			DbCommandWrapper.AddInParameter("@CcSurveyID",DbType.Int32,ccComplainFollowup.CcSurveyID)
						
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
		
			dim ccComplainFollowup as CcComplainFollowup = ctype(obj, CcComplainFollowup)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,ccComplainFollowup.ID)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int16,ccComplainFollowup.Status)
			DbCommandWrapper.AddInParameter("@Note",DbType.AnsiString,ccComplainFollowup.Note)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,ccComplainFollowup.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,ccComplainFollowup.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
			DbCommandWrapper.AddInParameter("@CcFollowupID",DbType.Int16,ccComplainFollowup.CcFollowupID)
			DbCommandWrapper.AddInParameter("@CcSurveyID",DbType.Int32,ccComplainFollowup.CcSurveyID)
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as CcComplainFollowup
		
			dim ccComplainFollowup as CcComplainFollowup = new CcComplainFollowup
			
			ccComplainFollowup.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then ccComplainFollowup.Status = ctype(dr("Status"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("Note")) then ccComplainFollowup.Note = dr("Note").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then ccComplainFollowup.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then ccComplainFollowup.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then ccComplainFollowup.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then ccComplainFollowup.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then ccComplainFollowup.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("CcFollowupID")) then
				ccComplainFollowup.CcFollowupID = ctype(dr("CcFollowupID"), short)
			end if
			if not dr.IsDBNull(dr.GetOrdinal("CcSurveyID")) then
				ccComplainFollowup.CcSurveyID = ctype(dr("CcSurveyID"), integer)
			end if
			
			return ccComplainFollowup
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (CcComplainFollowup) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(CcComplainFollowup),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(CcComplainFollowup).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

