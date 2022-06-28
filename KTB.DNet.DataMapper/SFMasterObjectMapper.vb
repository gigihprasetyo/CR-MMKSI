
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFMasterObject Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 11:33:17 AM
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

	public class SFMasterObjectMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSFMasterObject"
		private m_UpdateStatement as string = "up_UpdateSFMasterObject"
		private m_RetrieveStatement as string = "up_RetrieveSFMasterObject"
		private m_RetrieveListStatement as string = "up_RetrieveSFMasterObjectList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSFMasterObject"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sFMasterObject as SFMasterObject = nothing
			while dr.Read
			
				sFMasterObject = me.CreateObject(dr)
			            
			end while        					
			
			return sFMasterObject
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sFMasterObjectList as ArrayList = new ArrayList
			
			while dr.Read
					dim sFMasterObject as SFMasterObject = me.CreateObject(dr)
					sFMasterObjectList.Add(sFMasterObject)
			end while
			     
			return sFMasterObjectList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sFMasterObject as SFMasterObject = ctype(obj, SFMasterObject)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFMasterObject.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sFMasterObject as SFMasterObject = ctype(obj, SFMasterObject)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@SFConfigID", DbType.Int32, sFMasterObject.SFConfig.ID)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,sFMasterObject.Name)
			DbCommandWrapper.AddInParameter("@IsActive",DbType.Boolean,sFMasterObject.IsActive)
			DbCommandWrapper.AddInParameter("@IsCheckExecuteTime",DbType.Boolean,sFMasterObject.IsCheckExecuteTime)
			DbCommandWrapper.AddInParameter("@Remarks",DbType.AnsiString,sFMasterObject.Remarks)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFMasterObject.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sFMasterObject.LastUpdateBy)
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
		
			dim sFMasterObject as SFMasterObject = ctype(obj, SFMasterObject)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFMasterObject.ID)
            DbCommandWrapper.AddInParameter("@SFConfigID", DbType.Int32, sFMasterObject.SFConfig.ID)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,sFMasterObject.Name)
			DbCommandWrapper.AddInParameter("@IsActive",DbType.Boolean,sFMasterObject.IsActive)
			DbCommandWrapper.AddInParameter("@IsCheckExecuteTime",DbType.Boolean,sFMasterObject.IsCheckExecuteTime)
			DbCommandWrapper.AddInParameter("@Remarks",DbType.AnsiString,sFMasterObject.Remarks)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFMasterObject.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sFMasterObject.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SFMasterObject
		
			dim sFMasterObject as SFMasterObject = new SFMasterObject
			
			sFMasterObject.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("SFConfigID")) Then sFMasterObject.SFConfig = New SFConfig(ID:=CType(dr("SFConfigID"), Integer))
			if not dr.IsDBNull(dr.GetOrdinal("Name")) then sFMasterObject.Name = dr("Name").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("IsActive")) then sFMasterObject.IsActive = ctype(dr("IsActive"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("IsCheckExecuteTime")) then sFMasterObject.IsCheckExecuteTime = ctype(dr("IsCheckExecuteTime"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("Remarks")) then sFMasterObject.Remarks = dr("Remarks").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sFMasterObject.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sFMasterObject.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sFMasterObject.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sFMasterObject.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sFMasterObject.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sFMasterObject
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SFMasterObject) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SFMasterObject),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SFMasterObject).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

