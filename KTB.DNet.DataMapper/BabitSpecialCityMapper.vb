
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitSpecialCity Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 05/23/2019 - 2:04:24 PM
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

	public class BabitSpecialCityMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertBabitSpecialCity"
		private m_UpdateStatement as string = "up_UpdateBabitSpecialCity"
		private m_RetrieveStatement as string = "up_RetrieveBabitSpecialCity"
		private m_RetrieveListStatement as string = "up_RetrieveBabitSpecialCityList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteBabitSpecialCity"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim babitSpecialCity as BabitSpecialCity = nothing
			while dr.Read
			
				babitSpecialCity = me.CreateObject(dr)
			            
			end while        					
			
			return babitSpecialCity
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim babitSpecialCityList as ArrayList = new ArrayList
			
			while dr.Read
					dim babitSpecialCity as BabitSpecialCity = me.CreateObject(dr)
					babitSpecialCityList.Add(babitSpecialCity)
			end while
			     
			return babitSpecialCityList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim babitSpecialCity as BabitSpecialCity = ctype(obj, BabitSpecialCity)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,babitSpecialCity.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim babitSpecialCity as BabitSpecialCity = ctype(obj, BabitSpecialCity)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            'DbCommandWrapper.AddInParameter("@CityID",DbType.Int32,babitSpecialCity.CityID)
            'DbCommandWrapper.AddInParameter("@BabitSpecialProvinceid",DbType.Int32,babitSpecialCity.BabitSpecialProvinceid)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int32,babitSpecialCity.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,babitSpecialCity.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,babitSpecialCity.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(babitSpecialCity.City))
            DbCommandWrapper.AddInParameter("@BabitSpecialProvinceID", DbType.Int32, Me.GetRefObject(babitSpecialCity.BabitSpecialProvince))

						
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
		
			dim babitSpecialCity as BabitSpecialCity = ctype(obj, BabitSpecialCity)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,babitSpecialCity.ID)
            'DbCommandWrapper.AddInParameter("@CityID",DbType.Int32,babitSpecialCity.CityID)
            'DbCommandWrapper.AddInParameter("@BabitSpecialProvinceid",DbType.Int32,babitSpecialCity.BabitSpecialProvinceid)
			DbCommandWrapper.AddInParameter("@Status",DbType.Int32,babitSpecialCity.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,babitSpecialCity.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,babitSpecialCity.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(babitSpecialCity.City))
            DbCommandWrapper.AddInParameter("@BabitSpecialProvinceID", DbType.Int32, Me.GetRefObject(babitSpecialCity.BabitSpecialProvince))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as BabitSpecialCity
		
			dim babitSpecialCity as BabitSpecialCity = new BabitSpecialCity
			
			babitSpecialCity.ID = ctype(dr("ID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("CityID")) then babitSpecialCity.CityID = ctype(dr("CityID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("BabitSpecialProvinceid")) then babitSpecialCity.BabitSpecialProvinceid = ctype(dr("BabitSpecialProvinceid"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then babitSpecialCity.Status = ctype(dr("Status"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then babitSpecialCity.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then babitSpecialCity.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then babitSpecialCity.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then babitSpecialCity.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then babitSpecialCity.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                babitSpecialCity.City = New City(CType(dr("CityID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitSpecialProvinceID")) Then
                babitSpecialCity.BabitSpecialProvince = New BabitSpecialProvince(CType(dr("BabitSpecialProvinceID"), Short))
            End If

			return babitSpecialCity
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (BabitSpecialCity) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(BabitSpecialCity),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(BabitSpecialCity).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

