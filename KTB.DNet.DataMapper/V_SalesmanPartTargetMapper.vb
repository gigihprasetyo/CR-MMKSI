
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SalesmanPartTarget Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 7/14/2011 - 10:17:47 AM
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

	public class V_SalesmanPartTargetMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertV_SalesmanPartTarget"
		private m_UpdateStatement as string = "up_UpdateV_SalesmanPartTarget"
		private m_RetrieveStatement as string = "up_RetrieveV_SalesmanPartTarget"
		private m_RetrieveListStatement as string = "up_RetrieveV_SalesmanPartTargetList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteV_SalesmanPartTarget"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim v_SalesmanPartTarget as V_SalesmanPartTarget = nothing
			while dr.Read
			
				v_SalesmanPartTarget = me.CreateObject(dr)
			            
			end while        					
			
			return v_SalesmanPartTarget
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim v_SalesmanPartTargetList as ArrayList = new ArrayList
			
			while dr.Read
					dim v_SalesmanPartTarget as V_SalesmanPartTarget = me.CreateObject(dr)
					v_SalesmanPartTargetList.Add(v_SalesmanPartTarget)
			end while
			     
			return v_SalesmanPartTargetList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim v_SalesmanPartTarget as V_SalesmanPartTarget = ctype(obj, V_SalesmanPartTarget)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,v_SalesmanPartTarget.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim v_SalesmanPartTarget as V_SalesmanPartTarget = ctype(obj, V_SalesmanPartTarget)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@SalesmanHeaderID",DbType.Int16,v_SalesmanPartTarget.SalesmanHeaderID)
			DbCommandWrapper.AddInParameter("@Year",DbType.Int16,v_SalesmanPartTarget.Year)
			DbCommandWrapper.AddInParameter("@Month",DbType.Int16,v_SalesmanPartTarget.Month)
            DBCommandWrapper.AddInParameter("@Period", DbType.DateTime, v_SalesmanPartTarget.Period)
			DbCommandWrapper.AddInParameter("@Target",DbType.Currency,v_SalesmanPartTarget.Target)
			DbCommandWrapper.AddInParameter("@Realization",DbType.Currency,v_SalesmanPartTarget.Realization)
			DbCommandWrapper.AddInParameter("@Persentage",DbType.Decimal,v_SalesmanPartTarget.Persentage)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,v_SalesmanPartTarget.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,v_SalesmanPartTarget.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@DealerID",DbType.Int16,v_SalesmanPartTarget.DealerID)
			DbCommandWrapper.AddInParameter("@SearchTerm2",DbType.AnsiString,v_SalesmanPartTarget.SearchTerm2)
			DbCommandWrapper.AddInParameter("@DealerCode",DbType.AnsiString,v_SalesmanPartTarget.DealerCode)
			DbCommandWrapper.AddInParameter("@SalesmanCode",DbType.AnsiString,v_SalesmanPartTarget.SalesmanCode)
			DbCommandWrapper.AddInParameter("@AreaDesc",DbType.AnsiString,v_SalesmanPartTarget.AreaDesc)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,v_SalesmanPartTarget.Name)
            DBCommandWrapper.AddInParameter("@Kategori", DbType.AnsiString, v_SalesmanPartTarget.Kategori)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, v_SalesmanPartTarget.Status)
			DbCommandWrapper.AddInParameter("@Posisi",DbType.AnsiString,v_SalesmanPartTarget.Posisi)
			DbCommandWrapper.AddInParameter("@Level",DbType.AnsiString,v_SalesmanPartTarget.Level)
			DbCommandWrapper.AddInParameter("@TargetDealer",DbType.Currency,v_SalesmanPartTarget.TargetDealer)
			DbCommandWrapper.AddInParameter("@RealizationDealer",DbType.Currency,v_SalesmanPartTarget.RealizationDealer)

						
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
		
			dim v_SalesmanPartTarget as V_SalesmanPartTarget = ctype(obj, V_SalesmanPartTarget)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,v_SalesmanPartTarget.ID)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, v_SalesmanPartTarget.SalesmanHeaderID)
			DbCommandWrapper.AddInParameter("@Year",DbType.Int16,v_SalesmanPartTarget.Year)
			DbCommandWrapper.AddInParameter("@Month",DbType.Int16,v_SalesmanPartTarget.Month)
            DBCommandWrapper.AddInParameter("@Period", DbType.DateTime, v_SalesmanPartTarget.Period)
			DbCommandWrapper.AddInParameter("@Target",DbType.Currency,v_SalesmanPartTarget.Target)
			DbCommandWrapper.AddInParameter("@Realization",DbType.Currency,v_SalesmanPartTarget.Realization)
			DbCommandWrapper.AddInParameter("@Persentage",DbType.Decimal,v_SalesmanPartTarget.Persentage)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,v_SalesmanPartTarget.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,v_SalesmanPartTarget.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@DealerID",DbType.Int16,v_SalesmanPartTarget.DealerID)
			DbCommandWrapper.AddInParameter("@SearchTerm2",DbType.AnsiString,v_SalesmanPartTarget.SearchTerm2)
			DbCommandWrapper.AddInParameter("@DealerCode",DbType.AnsiString,v_SalesmanPartTarget.DealerCode)
			DbCommandWrapper.AddInParameter("@SalesmanCode",DbType.AnsiString,v_SalesmanPartTarget.SalesmanCode)
			DbCommandWrapper.AddInParameter("@AreaDesc",DbType.AnsiString,v_SalesmanPartTarget.AreaDesc)
			DbCommandWrapper.AddInParameter("@Name",DbType.AnsiString,v_SalesmanPartTarget.Name)
            DBCommandWrapper.AddInParameter("@Kategori", DbType.AnsiString, v_SalesmanPartTarget.Kategori)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, v_SalesmanPartTarget.Status)
			DbCommandWrapper.AddInParameter("@Posisi",DbType.AnsiString,v_SalesmanPartTarget.Posisi)
			DbCommandWrapper.AddInParameter("@Level",DbType.AnsiString,v_SalesmanPartTarget.Level)
			DbCommandWrapper.AddInParameter("@TargetDealer",DbType.Currency,v_SalesmanPartTarget.TargetDealer)
			DbCommandWrapper.AddInParameter("@RealizationDealer",DbType.Currency,v_SalesmanPartTarget.RealizationDealer)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as V_SalesmanPartTarget
		
			dim v_SalesmanPartTarget as V_SalesmanPartTarget = new V_SalesmanPartTarget
			
			v_SalesmanPartTarget.ID = ctype(dr("ID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then v_SalesmanPartTarget.SalesmanHeaderID = CType(dr("SalesmanHeaderID"), Int32)
			if not dr.IsDBNull(dr.GetOrdinal("Year")) then v_SalesmanPartTarget.Year = ctype(dr("Year"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("Month")) then v_SalesmanPartTarget.Month = ctype(dr("Month"), short) 
            If Not dr.IsDBNull(dr.GetOrdinal("Period")) Then v_SalesmanPartTarget.Period = CType(dr("Period"), Date)
			if not dr.IsDBNull(dr.GetOrdinal("Target")) then v_SalesmanPartTarget.Target = ctype(dr("Target"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("Realization")) then v_SalesmanPartTarget.Realization = ctype(dr("Realization"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("Persentage")) then v_SalesmanPartTarget.Persentage = ctype(dr("Persentage"), decimal) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then v_SalesmanPartTarget.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then v_SalesmanPartTarget.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then v_SalesmanPartTarget.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then v_SalesmanPartTarget.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then v_SalesmanPartTarget.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("DealerID")) then v_SalesmanPartTarget.DealerID = ctype(dr("DealerID"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("SearchTerm2")) then v_SalesmanPartTarget.SearchTerm2 = dr("SearchTerm2").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("DealerCode")) then v_SalesmanPartTarget.DealerCode = dr("DealerCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) then v_SalesmanPartTarget.SalesmanCode = dr("SalesmanCode").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("AreaDesc")) then v_SalesmanPartTarget.AreaDesc = dr("AreaDesc").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("Name")) then v_SalesmanPartTarget.Name = dr("Name").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("Kategori")) Then v_SalesmanPartTarget.Kategori = dr("Kategori").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_SalesmanPartTarget.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Posisi")) Then v_SalesmanPartTarget.Posisi = dr("Posisi").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Level")) Then v_SalesmanPartTarget.Level = dr("Level").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TargetDealer")) Then v_SalesmanPartTarget.TargetDealer = CType(dr("TargetDealer"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RealizationDealer")) Then v_SalesmanPartTarget.RealizationDealer = CType(dr("RealizationDealer"), Decimal)

            Return v_SalesmanPartTarget
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (V_SalesmanPartTarget) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(V_SalesmanPartTarget),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(V_SalesmanPartTarget).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

