
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcReportMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/7/2011 - 10:50:41 AM
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

	public class CcReportMasterMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertCcReportMaster"
		private m_UpdateStatement as string = "up_UpdateCcReportMaster"
		private m_RetrieveStatement as string = "up_RetrieveCcReportMaster"
		private m_RetrieveListStatement as string = "up_RetrieveCcReportMasterList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteCcReportMaster"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim ccReportMaster as CcReportMaster = nothing
			while dr.Read
			
				ccReportMaster = me.CreateObject(dr)
			            
			end while        					
			
			return ccReportMaster
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim ccReportMasterList as ArrayList = new ArrayList
			
			while dr.Read
					dim ccReportMaster as CcReportMaster = me.CreateObject(dr)
					ccReportMasterList.Add(ccReportMaster)
			end while
			     
			return ccReportMasterList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim ccReportMaster as CcReportMaster = ctype(obj, CcReportMaster)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,ccReportMaster.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim ccReportMaster as CcReportMaster = ctype(obj, CcReportMaster)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@Sequence",DbType.Int32,ccReportMaster.Sequence)
			DbCommandWrapper.AddInParameter("@RptDesc",DbType.AnsiString,ccReportMaster.RptDesc)
			DbCommandWrapper.AddInParameter("@RptType",DbType.Int16,ccReportMaster.RptType)
			DbCommandWrapper.AddInParameter("@DefaFileName",DbType.AnsiString,ccReportMaster.DefaFileName)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,ccReportMaster.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,ccReportMaster.LastUpdateBy)
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
		
			dim ccReportMaster as CcReportMaster = ctype(obj, CcReportMaster)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,ccReportMaster.ID)
			DbCommandWrapper.AddInParameter("@Sequence",DbType.Int32,ccReportMaster.Sequence)
			DbCommandWrapper.AddInParameter("@RptDesc",DbType.AnsiString,ccReportMaster.RptDesc)
			DbCommandWrapper.AddInParameter("@RptType",DbType.Int16,ccReportMaster.RptType)
			DbCommandWrapper.AddInParameter("@DefaFileName",DbType.AnsiString,ccReportMaster.DefaFileName)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,ccReportMaster.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,ccReportMaster.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as CcReportMaster
		
			dim ccReportMaster as CcReportMaster = new CcReportMaster
			
			ccReportMaster.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Sequence")) then ccReportMaster.Sequence = ctype(dr("Sequence"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("RptDesc")) then ccReportMaster.RptDesc = dr("RptDesc").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RptType")) then ccReportMaster.RptType = ctype(dr("RptType"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("DefaFileName")) then ccReportMaster.DefaFileName = dr("DefaFileName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then ccReportMaster.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then ccReportMaster.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then ccReportMaster.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then ccReportMaster.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then ccReportMaster.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return ccReportMaster
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (CcReportMaster) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(CcReportMaster),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(CcReportMaster).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

