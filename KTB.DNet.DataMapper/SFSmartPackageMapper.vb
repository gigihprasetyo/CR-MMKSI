
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFSmartPackage Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/12/2018 - 4:52:35 PM
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

	public class SFSmartPackageMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSFSmartPackage"
		private m_UpdateStatement as string = "up_UpdateSFSmartPackage"
		private m_RetrieveStatement as string = "up_RetrieveSFSmartPackage"
		private m_RetrieveListStatement as string = "up_RetrieveSFSmartPackageList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSFSmartPackage"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sFSmartPackage as SFSmartPackage = nothing
			while dr.Read
			
				sFSmartPackage = me.CreateObject(dr)
			            
			end while        					
			
			return sFSmartPackage
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sFSmartPackageList as ArrayList = new ArrayList
			
			while dr.Read
					dim sFSmartPackage as SFSmartPackage = me.CreateObject(dr)
					sFSmartPackageList.Add(sFSmartPackage)
			end while
			     
			return sFSmartPackageList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sFSmartPackage as SFSmartPackage = ctype(obj, SFSmartPackage)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFSmartPackage.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sFSmartPackage as SFSmartPackage = ctype(obj, SFSmartPackage)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@MSPRegistrationHistoryID", DbType.Int32, sFSmartPackage.MSPRegistrationHistory.ID)
			DbCommandWrapper.AddInParameter("@IsSynchronize",DbType.Boolean,sFSmartPackage.IsSynchronize)
			DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFSmartPackage.SynchronizeDate)
			DbCommandWrapper.AddInParameter("@IsActive",DbType.Boolean,sFSmartPackage.IsActive)
			DbCommandWrapper.AddInParameter("@SFID",DbType.AnsiString,sFSmartPackage.SFID)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFSmartPackage.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sFSmartPackage.LastUpdateBy)
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
		
			dim sFSmartPackage as SFSmartPackage = ctype(obj, SFSmartPackage)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFSmartPackage.ID)
            DbCommandWrapper.AddInParameter("@MSPRegistrationHistoryID", DbType.Int32, sFSmartPackage.MSPRegistrationHistory.ID)
			DbCommandWrapper.AddInParameter("@IsSynchronize",DbType.Boolean,sFSmartPackage.IsSynchronize)
			DbCommandWrapper.AddInParameter("@SynchronizeDate",DbType.DateTime,sFSmartPackage.SynchronizeDate)
			DbCommandWrapper.AddInParameter("@IsActive",DbType.Boolean,sFSmartPackage.IsActive)
			DbCommandWrapper.AddInParameter("@SFID",DbType.AnsiString,sFSmartPackage.SFID)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFSmartPackage.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sFSmartPackage.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SFSmartPackage
		
			dim sFSmartPackage as SFSmartPackage = new SFSmartPackage
			
			sFSmartPackage.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("MSPRegistrationHistoryID")) Then sFSmartPackage.MSPRegistrationHistory = New MSPRegistrationHistory(ID:=CType(dr("MSPRegistrationHistoryID"), Integer))
			if not dr.IsDBNull(dr.GetOrdinal("IsSynchronize")) then sFSmartPackage.IsSynchronize = ctype(dr("IsSynchronize"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("SynchronizeDate")) then sFSmartPackage.SynchronizeDate = ctype(dr("SynchronizeDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("IsActive")) then sFSmartPackage.IsActive = ctype(dr("IsActive"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("SFID")) then sFSmartPackage.SFID = dr("SFID").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sFSmartPackage.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sFSmartPackage.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sFSmartPackage.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sFSmartPackage.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sFSmartPackage.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sFSmartPackage
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SFSmartPackage) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SFSmartPackage),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SFSmartPackage).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

