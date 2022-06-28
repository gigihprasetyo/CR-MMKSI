
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SalesmanPart Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/9/2011 - 11:00:26 AM
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

	public class V_SalesmanPartMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertV_SalesmanPart"
		private m_UpdateStatement as string = "up_UpdateV_SalesmanPart"
		private m_RetrieveStatement as string = "up_RetrieveV_SalesmanPart"
		private m_RetrieveListStatement as string = "up_RetrieveV_SalesmanPartList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteV_SalesmanPart"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim v_SalesmanPart as V_SalesmanPart = nothing
			while dr.Read
			
				v_SalesmanPart = me.CreateObject(dr)
			            
			end while        					
			
			return v_SalesmanPart
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim v_SalesmanPartList as ArrayList = new ArrayList
			
			while dr.Read
					dim v_SalesmanPart as V_SalesmanPart = me.CreateObject(dr)
					v_SalesmanPartList.Add(v_SalesmanPart)
			end while
			     
			return v_SalesmanPartList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim v_SalesmanPart as V_SalesmanPart = ctype(obj, V_SalesmanPart)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int16,v_SalesmanPart.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim v_SalesmanPart as V_SalesmanPart = ctype(obj, V_SalesmanPart)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int16,2)
			DbCommandWrapper.AddInParameter("@DealerId",DbType.Int16,v_SalesmanPart.DealerId)
			DbCommandWrapper.AddInParameter("@SalesmanCode",DbType.AnsiString,v_SalesmanPart.SalesmanCode)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,v_SalesmanPart.Name)
			DbCommandWrapper.AddInParameter("@Image",DbType.Binary,v_SalesmanPart.Image)
			DbCommandWrapper.AddInParameter("@PlaceOfBirth",DbType.AnsiString,v_SalesmanPart.PlaceOfBirth)
			DbCommandWrapper.AddInParameter("@DateOfBirth",DbType.DateTime,v_SalesmanPart.DateOfBirth)
			DbCommandWrapper.AddInParameter("@Gender",DbType.Byte,v_SalesmanPart.Gender)
			DbCommandWrapper.AddInParameter("@Address",DbType.AnsiString,v_SalesmanPart.Address)
			DbCommandWrapper.AddInParameter("@City",DbType.AnsiString,v_SalesmanPart.City)
			DbCommandWrapper.AddInParameter("@ShopSiteNumber",DbType.Int32,v_SalesmanPart.ShopSiteNumber)
			DbCommandWrapper.AddInParameter("@HireDate",DbType.DateTime,v_SalesmanPart.HireDate)
			DbCommandWrapper.AddInParameter("@SalesmanAreaId",DbType.Int32,v_SalesmanPart.SalesmanAreaId)
			DbCommandWrapper.AddInParameter("@JobPositionId_Main",DbType.Int32,v_SalesmanPart.JobPositionId_Main)
			DbCommandWrapper.AddInParameter("@SalesmanLevelID",DbType.Int32,v_SalesmanPart.SalesmanLevelID)
			DbCommandWrapper.AddInParameter("@JobPositionId_Second",DbType.Int32,v_SalesmanPart.JobPositionId_Second)
			DbCommandWrapper.AddInParameter("@JobPositionId_Third",DbType.Int32,v_SalesmanPart.JobPositionId_Third)
			DbCommandWrapper.AddInParameter("@LeaderId",DbType.Int32,v_SalesmanPart.LeaderId)
			DbCommandWrapper.AddInParameter("@JobPositionId_Leader",DbType.Int32,v_SalesmanPart.JobPositionId_Leader)
			DbCommandWrapper.AddInParameter("@RegisterStatus",DbType.AnsiStringFixedLength,v_SalesmanPart.RegisterStatus)
			DbCommandWrapper.AddInParameter("@MarriedStatus",DbType.AnsiStringFixedLength,v_SalesmanPart.MarriedStatus)
			DbCommandWrapper.AddInParameter("@ResignDate",DbType.DateTime,v_SalesmanPart.ResignDate)
			DbCommandWrapper.AddInParameter("@ResignReason",DbType.AnsiString,v_SalesmanPart.ResignReason)
			DbCommandWrapper.AddInParameter("@SalesIndicator",DbType.Byte,v_SalesmanPart.SalesIndicator)
			DbCommandWrapper.AddInParameter("@SalesUnitIndicator",DbType.Byte,v_SalesmanPart.SalesUnitIndicator)
			DbCommandWrapper.AddInParameter("@MechanicIndicator",DbType.Byte,v_SalesmanPart.MechanicIndicator)
			DbCommandWrapper.AddInParameter("@SparePartIndicator",DbType.Byte,v_SalesmanPart.SparePartIndicator)
			DbCommandWrapper.AddInParameter("@SPAdminIndicator",DbType.Byte,v_SalesmanPart.SPAdminIndicator)
			DbCommandWrapper.AddInParameter("@SPWareHouseIndicator",DbType.Byte,v_SalesmanPart.SPWareHouseIndicator)
			DbCommandWrapper.AddInParameter("@SPCounterIndicator",DbType.Byte,v_SalesmanPart.SPCounterIndicator)
			DbCommandWrapper.AddInParameter("@SPSalesIndicator",DbType.Byte,v_SalesmanPart.SPSalesIndicator)
			DbCommandWrapper.AddInParameter("@IsRequestID",DbType.Byte,v_SalesmanPart.IsRequestID)
			DbCommandWrapper.AddInParameter("@Status",DbType.AnsiStringFixedLength,v_SalesmanPart.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,v_SalesmanPart.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,v_SalesmanPart.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SalesmanPart.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, v_SalesmanPart.DealerBranchCode)
			DbCommandWrapper.AddInParameter("@ProvinceName",DbType.AnsiString,v_SalesmanPart.ProvinceName)
			DbCommandWrapper.AddInParameter("@DivisiID",DbType.Int32,v_SalesmanPart.DivisiID)
			DbCommandWrapper.AddInParameter("@DivisiName",DbType.AnsiString,v_SalesmanPart.DivisiName)
			DbCommandWrapper.AddInParameter("@PosisiID",DbType.Int32,v_SalesmanPart.PosisiID)
			DbCommandWrapper.AddInParameter("@PosisiName",DbType.AnsiString,v_SalesmanPart.PosisiName)
			DbCommandWrapper.AddInParameter("@LevelID",DbType.Int32,v_SalesmanPart.LevelID)
			DbCommandWrapper.AddInParameter("@LevelName",DbType.AnsiString,v_SalesmanPart.LevelName)
			DbCommandWrapper.AddInParameter("@Salary",DbType.Currency,v_SalesmanPart.Salary)
			DbCommandWrapper.AddInParameter("@LeaderCode",DbType.AnsiString,v_SalesmanPart.LeaderCode)
			DbCommandWrapper.AddInParameter("@LeaderName",DbType.AnsiString,v_SalesmanPart.LeaderName)
			DbCommandWrapper.AddInParameter("@AreaDesc",DbType.AnsiString,v_SalesmanPart.AreaDesc)
			DbCommandWrapper.AddInParameter("@PENDIDIKAN",DbType.AnsiString,v_SalesmanPart.PENDIDIKAN)
			DbCommandWrapper.AddInParameter("@EMAIL",DbType.AnsiString,v_SalesmanPart.EMAIL)
			DbCommandWrapper.AddInParameter("@NO_HP",DbType.AnsiString,v_SalesmanPart.NO_HP)
			DbCommandWrapper.AddInParameter("@NOKTP",DbType.AnsiString,v_SalesmanPart.NOKTP)

						
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
		
			dim v_SalesmanPart as V_SalesmanPart = ctype(obj, V_SalesmanPart)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int16,v_SalesmanPart.ID)
			DbCommandWrapper.AddInParameter("@DealerId",DbType.Int16,v_SalesmanPart.DealerId)
			DbCommandWrapper.AddInParameter("@SalesmanCode",DbType.AnsiString,v_SalesmanPart.SalesmanCode)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,v_SalesmanPart.Name)
			DbCommandWrapper.AddInParameter("@Image",DbType.Binary,v_SalesmanPart.Image)
			DbCommandWrapper.AddInParameter("@PlaceOfBirth",DbType.AnsiString,v_SalesmanPart.PlaceOfBirth)
			DbCommandWrapper.AddInParameter("@DateOfBirth",DbType.DateTime,v_SalesmanPart.DateOfBirth)
			DbCommandWrapper.AddInParameter("@Gender",DbType.Byte,v_SalesmanPart.Gender)
			DbCommandWrapper.AddInParameter("@Address",DbType.AnsiString,v_SalesmanPart.Address)
			DbCommandWrapper.AddInParameter("@City",DbType.AnsiString,v_SalesmanPart.City)
			DbCommandWrapper.AddInParameter("@ShopSiteNumber",DbType.Int32,v_SalesmanPart.ShopSiteNumber)
			DbCommandWrapper.AddInParameter("@HireDate",DbType.DateTime,v_SalesmanPart.HireDate)
			DbCommandWrapper.AddInParameter("@SalesmanAreaId",DbType.Int32,v_SalesmanPart.SalesmanAreaId)
			DbCommandWrapper.AddInParameter("@JobPositionId_Main",DbType.Int32,v_SalesmanPart.JobPositionId_Main)
			DbCommandWrapper.AddInParameter("@SalesmanLevelID",DbType.Int32,v_SalesmanPart.SalesmanLevelID)
			DbCommandWrapper.AddInParameter("@JobPositionId_Second",DbType.Int32,v_SalesmanPart.JobPositionId_Second)
			DbCommandWrapper.AddInParameter("@JobPositionId_Third",DbType.Int32,v_SalesmanPart.JobPositionId_Third)
			DbCommandWrapper.AddInParameter("@LeaderId",DbType.Int32,v_SalesmanPart.LeaderId)
			DbCommandWrapper.AddInParameter("@JobPositionId_Leader",DbType.Int32,v_SalesmanPart.JobPositionId_Leader)
			DbCommandWrapper.AddInParameter("@RegisterStatus",DbType.AnsiStringFixedLength,v_SalesmanPart.RegisterStatus)
			DbCommandWrapper.AddInParameter("@MarriedStatus",DbType.AnsiStringFixedLength,v_SalesmanPart.MarriedStatus)
			DbCommandWrapper.AddInParameter("@ResignDate",DbType.DateTime,v_SalesmanPart.ResignDate)
			DbCommandWrapper.AddInParameter("@ResignReason",DbType.AnsiString,v_SalesmanPart.ResignReason)
			DbCommandWrapper.AddInParameter("@SalesIndicator",DbType.Byte,v_SalesmanPart.SalesIndicator)
			DbCommandWrapper.AddInParameter("@SalesUnitIndicator",DbType.Byte,v_SalesmanPart.SalesUnitIndicator)
			DbCommandWrapper.AddInParameter("@MechanicIndicator",DbType.Byte,v_SalesmanPart.MechanicIndicator)
			DbCommandWrapper.AddInParameter("@SparePartIndicator",DbType.Byte,v_SalesmanPart.SparePartIndicator)
			DbCommandWrapper.AddInParameter("@SPAdminIndicator",DbType.Byte,v_SalesmanPart.SPAdminIndicator)
			DbCommandWrapper.AddInParameter("@SPWareHouseIndicator",DbType.Byte,v_SalesmanPart.SPWareHouseIndicator)
			DbCommandWrapper.AddInParameter("@SPCounterIndicator",DbType.Byte,v_SalesmanPart.SPCounterIndicator)
			DbCommandWrapper.AddInParameter("@SPSalesIndicator",DbType.Byte,v_SalesmanPart.SPSalesIndicator)
			DbCommandWrapper.AddInParameter("@IsRequestID",DbType.Byte,v_SalesmanPart.IsRequestID)
			DbCommandWrapper.AddInParameter("@Status",DbType.AnsiStringFixedLength,v_SalesmanPart.Status)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,v_SalesmanPart.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,v_SalesmanPart.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SalesmanPart.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, v_SalesmanPart.DealerBranchCode)
			DbCommandWrapper.AddInParameter("@ProvinceName",DbType.AnsiString,v_SalesmanPart.ProvinceName)
			DbCommandWrapper.AddInParameter("@DivisiID",DbType.Int32,v_SalesmanPart.DivisiID)
			DbCommandWrapper.AddInParameter("@DivisiName",DbType.AnsiString,v_SalesmanPart.DivisiName)
			DbCommandWrapper.AddInParameter("@PosisiID",DbType.Int32,v_SalesmanPart.PosisiID)
			DbCommandWrapper.AddInParameter("@PosisiName",DbType.AnsiString,v_SalesmanPart.PosisiName)
			DbCommandWrapper.AddInParameter("@LevelID",DbType.Int32,v_SalesmanPart.LevelID)
			DbCommandWrapper.AddInParameter("@LevelName",DbType.AnsiString,v_SalesmanPart.LevelName)
			DbCommandWrapper.AddInParameter("@Salary",DbType.Currency,v_SalesmanPart.Salary)
			DbCommandWrapper.AddInParameter("@LeaderCode",DbType.AnsiString,v_SalesmanPart.LeaderCode)
			DbCommandWrapper.AddInParameter("@LeaderName",DbType.AnsiString,v_SalesmanPart.LeaderName)
			DbCommandWrapper.AddInParameter("@AreaDesc",DbType.AnsiString,v_SalesmanPart.AreaDesc)
			DbCommandWrapper.AddInParameter("@PENDIDIKAN",DbType.AnsiString,v_SalesmanPart.PENDIDIKAN)
			DbCommandWrapper.AddInParameter("@EMAIL",DbType.AnsiString,v_SalesmanPart.EMAIL)
			DbCommandWrapper.AddInParameter("@NO_HP",DbType.AnsiString,v_SalesmanPart.NO_HP)
			DbCommandWrapper.AddInParameter("@NOKTP",DbType.AnsiString,v_SalesmanPart.NOKTP)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as V_SalesmanPart
		
			dim v_SalesmanPart as V_SalesmanPart = new V_SalesmanPart
			
			v_SalesmanPart.ID = ctype(dr("ID"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("DealerId")) then v_SalesmanPart.DealerId = ctype(dr("DealerId"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) then v_SalesmanPart.SalesmanCode = dr("SalesmanCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Name")) then v_SalesmanPart.Name = dr("Name").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Image")) then v_SalesmanPart.Image = ctype(dr("Image"), byte()) 
			if not dr.IsDBNull(dr.GetOrdinal("PlaceOfBirth")) then v_SalesmanPart.PlaceOfBirth = dr("PlaceOfBirth").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DateOfBirth")) then v_SalesmanPart.DateOfBirth = ctype(dr("DateOfBirth"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("Gender")) then v_SalesmanPart.Gender = ctype(dr("Gender"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("Address")) then v_SalesmanPart.Address = dr("Address").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("City")) then v_SalesmanPart.City = dr("City").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ShopSiteNumber")) then v_SalesmanPart.ShopSiteNumber = ctype(dr("ShopSiteNumber"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("HireDate")) then v_SalesmanPart.HireDate = ctype(dr("HireDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("SalesmanAreaId")) then v_SalesmanPart.SalesmanAreaId = ctype(dr("SalesmanAreaId"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Main")) then v_SalesmanPart.JobPositionId_Main = ctype(dr("JobPositionId_Main"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("SalesmanLevelID")) then v_SalesmanPart.SalesmanLevelID = ctype(dr("SalesmanLevelID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Second")) then v_SalesmanPart.JobPositionId_Second = ctype(dr("JobPositionId_Second"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Third")) then v_SalesmanPart.JobPositionId_Third = ctype(dr("JobPositionId_Third"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("LeaderId")) then v_SalesmanPart.LeaderId = ctype(dr("LeaderId"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("JobPositionId_Leader")) then v_SalesmanPart.JobPositionId_Leader = ctype(dr("JobPositionId_Leader"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("RegisterStatus")) then v_SalesmanPart.RegisterStatus = dr("RegisterStatus").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("MarriedStatus")) then v_SalesmanPart.MarriedStatus = dr("MarriedStatus").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("ResignDate")) then v_SalesmanPart.ResignDate = ctype(dr("ResignDate"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("ResignReason")) then v_SalesmanPart.ResignReason = dr("ResignReason").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("SalesIndicator")) then v_SalesmanPart.SalesIndicator = ctype(dr("SalesIndicator"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("SalesUnitIndicator")) then v_SalesmanPart.SalesUnitIndicator = ctype(dr("SalesUnitIndicator"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("MechanicIndicator")) then v_SalesmanPart.MechanicIndicator = ctype(dr("MechanicIndicator"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("SparePartIndicator")) then v_SalesmanPart.SparePartIndicator = ctype(dr("SparePartIndicator"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("SPAdminIndicator")) then v_SalesmanPart.SPAdminIndicator = ctype(dr("SPAdminIndicator"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("SPWareHouseIndicator")) then v_SalesmanPart.SPWareHouseIndicator = ctype(dr("SPWareHouseIndicator"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("SPCounterIndicator")) then v_SalesmanPart.SPCounterIndicator = ctype(dr("SPCounterIndicator"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("SPSalesIndicator")) then v_SalesmanPart.SPSalesIndicator = ctype(dr("SPSalesIndicator"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("IsRequestID")) then v_SalesmanPart.IsRequestID = ctype(dr("IsRequestID"), byte) 
			if not dr.IsDBNull(dr.GetOrdinal("Status")) then v_SalesmanPart.Status = dr("Status").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then v_SalesmanPart.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then v_SalesmanPart.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then v_SalesmanPart.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then v_SalesmanPart.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then v_SalesmanPart.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SalesmanPart.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then v_SalesmanPart.DealerBranchCode = dr("DealerBranchCode").ToString
			if not dr.IsDBNull(dr.GetOrdinal("ProvinceName")) then v_SalesmanPart.ProvinceName = dr("ProvinceName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DivisiID")) then v_SalesmanPart.DivisiID = ctype(dr("DivisiID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("DivisiName")) then v_SalesmanPart.DivisiName = dr("DivisiName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("PosisiID")) then v_SalesmanPart.PosisiID = ctype(dr("PosisiID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("PosisiName")) then v_SalesmanPart.PosisiName = dr("PosisiName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LevelID")) then v_SalesmanPart.LevelID = ctype(dr("LevelID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("LevelName")) then v_SalesmanPart.LevelName = dr("LevelName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Salary")) then v_SalesmanPart.Salary = ctype(dr("Salary"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("LeaderCode")) then v_SalesmanPart.LeaderCode = dr("LeaderCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LeaderName")) then v_SalesmanPart.LeaderName = dr("LeaderName").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("AreaDesc")) then v_SalesmanPart.AreaDesc = dr("AreaDesc").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("PENDIDIKAN")) then v_SalesmanPart.PENDIDIKAN = dr("PENDIDIKAN").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("EMAIL")) then v_SalesmanPart.EMAIL = dr("EMAIL").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("NO_HP")) then v_SalesmanPart.NO_HP = dr("NO_HP").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("NOKTP")) then v_SalesmanPart.NOKTP = dr("NOKTP").ToString 
			
			return v_SalesmanPart
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (V_SalesmanPart) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(V_SalesmanPart),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(V_SalesmanPart).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

