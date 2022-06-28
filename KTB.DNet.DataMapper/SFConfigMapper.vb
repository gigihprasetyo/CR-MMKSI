
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFConfig Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 11:32:06 AM
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

	public class SFConfigMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSFConfig"
		private m_UpdateStatement as string = "up_UpdateSFConfig"
		private m_RetrieveStatement as string = "up_RetrieveSFConfig"
		private m_RetrieveListStatement as string = "up_RetrieveSFConfigList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSFConfig"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim sFConfig as SFConfig = nothing
			while dr.Read
			
				sFConfig = me.CreateObject(dr)
			            
			end while        					
			
			return sFConfig
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim sFConfigList as ArrayList = new ArrayList
			
			while dr.Read
					dim sFConfig as SFConfig = me.CreateObject(dr)
					sFConfigList.Add(sFConfig)
			end while
			     
			return sFConfigList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim sFConfig as SFConfig = ctype(obj, SFConfig)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFConfig.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim sFConfig as SFConfig = ctype(obj, SFConfig)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,sFConfig.Name)
			DbCommandWrapper.AddInParameter("@Url",DbType.AnsiString,sFConfig.Url)
			DbCommandWrapper.AddInParameter("@Username",DbType.AnsiString,sFConfig.Username)
			DbCommandWrapper.AddInParameter("@Password",DbType.AnsiString,sFConfig.Password)
			DbCommandWrapper.AddInParameter("@ConsumerSecret",DbType.AnsiString,sFConfig.ConsumerSecret)
            DbCommandWrapper.AddInParameter("@ConsumerKey", DbType.String, sFConfig.ConsumerKey)
            DbCommandWrapper.AddInParameter("@WebProxy", DbType.String, sFConfig.WebProxy)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFConfig.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,sFConfig.LastUpdateBy)
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
		
			dim sFConfig as SFConfig = ctype(obj, SFConfig)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,sFConfig.ID)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,sFConfig.Name)
			DbCommandWrapper.AddInParameter("@Url",DbType.AnsiString,sFConfig.Url)
			DbCommandWrapper.AddInParameter("@Username",DbType.AnsiString,sFConfig.Username)
			DbCommandWrapper.AddInParameter("@Password",DbType.AnsiString,sFConfig.Password)
			DbCommandWrapper.AddInParameter("@ConsumerSecret",DbType.AnsiString,sFConfig.ConsumerSecret)
            DbCommandWrapper.AddInParameter("@ConsumerKey", DbType.String, sFConfig.ConsumerKey)
            DbCommandWrapper.AddInParameter("@WebProxy", DbType.String, sFConfig.WebProxy)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,sFConfig.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,sFConfig.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SFConfig
		
			dim sFConfig as SFConfig = new SFConfig
			
			sFConfig.ID = ctype(dr("ID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Name")) then sFConfig.Name = dr("Name").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Url")) then sFConfig.Url = dr("Url").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Username")) then sFConfig.Username = dr("Username").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Password")) then sFConfig.Password = dr("Password").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ConsumerSecret")) then sFConfig.ConsumerSecret = dr("ConsumerSecret").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumerKey")) Then sFConfig.ConsumerKey = dr("ConsumerKey").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WebProxy")) Then sFConfig.WebProxy = dr("WebProxy").ToString
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then sFConfig.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then sFConfig.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then sFConfig.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then sFConfig.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then sFConfig.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			
			return sFConfig
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SFConfig) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SFConfig),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SFConfig).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

