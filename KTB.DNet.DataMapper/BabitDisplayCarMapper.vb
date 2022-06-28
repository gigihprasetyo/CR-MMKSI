
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitDisplayCar Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 05/13/2019 - 1:57:12 PM
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

	public class BabitDisplayCarMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertBabitDisplayCar"
		private m_UpdateStatement as string = "up_UpdateBabitDisplayCar"
		private m_RetrieveStatement as string = "up_RetrieveBabitDisplayCar"
		private m_RetrieveListStatement as string = "up_RetrieveBabitDisplayCarList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteBabitDisplayCar"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim babitDisplayCar as BabitDisplayCar = nothing
			while dr.Read
			
				babitDisplayCar = me.CreateObject(dr)
			            
			end while        					
			
			return babitDisplayCar
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim babitDisplayCarList as ArrayList = new ArrayList
			
			while dr.Read
					dim babitDisplayCar as BabitDisplayCar = me.CreateObject(dr)
					babitDisplayCarList.Add(babitDisplayCar)
			end while
			     
			return babitDisplayCarList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim babitDisplayCar as BabitDisplayCar = ctype(obj, BabitDisplayCar)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,babitDisplayCar.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim babitDisplayCar as BabitDisplayCar = ctype(obj, BabitDisplayCar)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            'DbCommandWrapper.AddInParameter("@BabitHeaderID",DbType.Int32,babitDisplayCar.BabitHeaderID)
            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID",DbType.Int32,babitDisplayCar.SubCategoryVehicleID)
			DbCommandWrapper.AddInParameter("@Qty",DbType.Int32,babitDisplayCar.Qty)
			DbCommandWrapper.AddInParameter("@SalesTarget",DbType.Int32,babitDisplayCar.SalesTarget)
			DbCommandWrapper.AddInParameter("@IsTestDrive",DbType.Boolean,babitDisplayCar.IsTestDrive)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,babitDisplayCar.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,babitDisplayCar.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitHeaderID", DbType.Int32, Me.GetRefObject(babitDisplayCar.BabitHeader))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(babitDisplayCar.SubCategoryVehicle))

						
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
		
			dim babitDisplayCar as BabitDisplayCar = ctype(obj, BabitDisplayCar)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,babitDisplayCar.ID)
            'DbCommandWrapper.AddInParameter("@BabitHeaderID",DbType.Int32,babitDisplayCar.BabitHeaderID)
            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID",DbType.Int32,babitDisplayCar.SubCategoryVehicleID)
			DbCommandWrapper.AddInParameter("@Qty",DbType.Int32,babitDisplayCar.Qty)
			DbCommandWrapper.AddInParameter("@SalesTarget",DbType.Int32,babitDisplayCar.SalesTarget)
			DbCommandWrapper.AddInParameter("@IsTestDrive",DbType.Boolean,babitDisplayCar.IsTestDrive)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,babitDisplayCar.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,babitDisplayCar.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
            DbCommandWrapper.AddInParameter("@BabitHeaderID", DbType.Int32, Me.GetRefObject(babitDisplayCar.BabitHeader))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(babitDisplayCar.SubCategoryVehicle))
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as BabitDisplayCar
		
			dim babitDisplayCar as BabitDisplayCar = new BabitDisplayCar
			
			babitDisplayCar.ID = ctype(dr("ID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("BabitHeaderID")) then babitDisplayCar.BabitHeaderID = ctype(dr("BabitHeaderID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) then babitDisplayCar.SubCategoryVehicleID = ctype(dr("SubCategoryVehicleID"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("Qty")) then babitDisplayCar.Qty = ctype(dr("Qty"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("SalesTarget")) then babitDisplayCar.SalesTarget = ctype(dr("SalesTarget"), integer) 
			if not dr.IsDBNull(dr.GetOrdinal("IsTestDrive")) then babitDisplayCar.IsTestDrive = ctype(dr("IsTestDrive"), boolean) 
			if not dr.IsDBNull(dr.GetOrdinal("RowStatus")) then babitDisplayCar.RowStatus = ctype(dr("RowStatus"), short) 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) then babitDisplayCar.CreatedBy = dr("CreatedBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) then babitDisplayCar.CreatedTime = ctype(dr("CreatedTime"), DateTime) 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) then babitDisplayCar.LastUpdateBy = dr("LastUpdateBy").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) then babitDisplayCar.LastUpdateTime = ctype(dr("LastUpdateTime"), DateTime) 

            If Not dr.IsDBNull(dr.GetOrdinal("BabitHeaderID")) Then
                babitDisplayCar.BabitHeader = New BabitHeader(CType(dr("BabitHeaderID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then
                babitDisplayCar.SubCategoryVehicle = New SubCategoryVehicle(CType(dr("SubCategoryVehicleID"), Short))
            End If

			return babitDisplayCar
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (BabitDisplayCar) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(BabitDisplayCar),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(BabitDisplayCar).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

