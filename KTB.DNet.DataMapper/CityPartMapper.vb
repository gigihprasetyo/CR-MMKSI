
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CityPart Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/24/2011 - 2:29:38 PM
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

	public class CityPartMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertCityPart"
		private m_UpdateStatement as string = "up_UpdateCityPart"
		private m_RetrieveStatement as string = "up_RetrieveCityPart"
		private m_RetrieveListStatement as string = "up_RetrieveCityPartList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteCityPart"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim cityPart as CityPart = nothing
			while dr.Read
			
				cityPart = me.CreateObject(dr)
			            
			end while        					
			
			return cityPart
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim cityPartList as ArrayList = new ArrayList
			
			while dr.Read
					dim cityPart as CityPart = me.CreateObject(dr)
					cityPartList.Add(cityPart)
			end while
			     
			return cityPartList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim cityPart as CityPart = ctype(obj, CityPart)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,cityPart.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
	            Dim cityPart As CityPart = CType(obj, CityPart)
	            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


	            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
	            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(cityPart.City))
	            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, cityPart.CityName)
	            DbCommandWrapper.AddInParameter("@CityCode", DbType.AnsiString, cityPart.CityCode)
	            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cityPart.RowStatus)
	            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
	            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
	            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, cityPart.LastUpdateBy)
	            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

	            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, Me.GetRefObject(cityPart.Province))

	            Return DbCommandWrapper
			
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
		
	            Dim cityPart As CityPart = CType(obj, CityPart)

	            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

	            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cityPart.ID)
	            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(cityPart.City))
	            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, cityPart.CityName)
	            DbCommandWrapper.AddInParameter("@CityCode", DbType.AnsiString, cityPart.CityCode)
	            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cityPart.RowStatus)
	            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, cityPart.CreatedBy)
	            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
	            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
	            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


	            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, Me.GetRefObject(cityPart.Province))

	            Return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as CityPart
		
	            Dim cityPart As CityPart = New CityPart

	            cityPart.ID = CType(dr("ID"), Integer)
	            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then cityPart.CityName = dr("CityName").ToString
	            If Not dr.IsDBNull(dr.GetOrdinal("CityCode")) Then cityPart.CityCode = dr("CityCode").ToString
	            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then cityPart.RowStatus = CType(dr("RowStatus"), Short)
	            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then cityPart.CreatedBy = dr("CreatedBy").ToString
	            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then cityPart.CreatedTime = CType(dr("CreatedTime"), DateTime)
	            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then cityPart.LastUpdateBy = dr("LastUpdateBy").ToString
	            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then cityPart.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
	            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceID")) Then
	                cityPart.Province = New Province(CType(dr("ProvinceID"), Integer))
	            End If
	            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
	                cityPart.City = New City(CType(dr("CityID"), Integer))
	            End If
	            Return cityPart
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (CityPart) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(CityPart),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(CityPart).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

