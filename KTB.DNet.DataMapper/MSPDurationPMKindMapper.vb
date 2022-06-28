
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPDurationPMKind Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/23/2018 - 2:25:08 PM
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

	public class MSPDurationPMKindMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMSPDurationPMKind"
		private m_UpdateStatement as string = "up_UpdateMSPDurationPMKind"
		private m_RetrieveStatement as string = "up_RetrieveMSPDurationPMKind"
		private m_RetrieveListStatement as string = "up_RetrieveMSPDurationPMKindList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMSPDurationPMKind"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim mSPDurationPMKind as MSPDurationPMKind = nothing
			while dr.Read
			
				mSPDurationPMKind = me.CreateObject(dr)
			            
			end while        					
			
			return mSPDurationPMKind
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim mSPDurationPMKindList as ArrayList = new ArrayList
			
			while dr.Read
					dim mSPDurationPMKind as MSPDurationPMKind = me.CreateObject(dr)
					mSPDurationPMKindList.Add(mSPDurationPMKind)
			end while
			     
			return mSPDurationPMKindList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim mSPDurationPMKind as MSPDurationPMKind = ctype(obj, MSPDurationPMKind)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPDurationPMKind.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim mSPDurationPMKind as MSPDurationPMKind = ctype(obj, MSPDurationPMKind)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@Duration",DbType.Int16,mSPDurationPMKind.Duration)
            DbCommandWrapper.AddInParameter("@PMKindCode", DbType.AnsiString, mSPDurationPMKind.PMKindCode)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPDurationPMKind.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,mSPDurationPMKind.LastUpdateBy)
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
		
			dim mSPDurationPMKind as MSPDurationPMKind = ctype(obj, MSPDurationPMKind)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPDurationPMKind.ID)
			DbCommandWrapper.AddInParameter("@Duration",DbType.Int16,mSPDurationPMKind.Duration)
            DbCommandWrapper.AddInParameter("@PMKindCode", DbType.AnsiString, mSPDurationPMKind.PMKindCode)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,mSPDurationPMKind.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,mSPDurationPMKind.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as MSPDurationPMKind
		
			dim mSPDurationPMKind as MSPDurationPMKind = new MSPDurationPMKind
			
			mSPDurationPMKind.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Duration")) then mSPDurationPMKind.Duration = ctype(dr("Duration"), short) 
            If Not dr.IsDBNull(dr.GetOrdinal("PMKindCode")) Then mSPDurationPMKind.PMKindCode = dr("PMKindCode").ToString
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then mSPDurationPMKind.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then mSPDurationPMKind.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then mSPDurationPMKind.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then mSPDurationPMKind.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then mSPDurationPMKind.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return mSPDurationPMKind
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (MSPDurationPMKind) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(MSPDurationPMKind),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(MSPDurationPMKind).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

