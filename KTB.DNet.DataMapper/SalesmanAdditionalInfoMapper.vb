
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanAdditionalInfo Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/6/2011 - 9:43:24 AM
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

	public class SalesmanAdditionalInfoMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertSalesmanAdditionalInfo"
		private m_UpdateStatement as string = "up_UpdateSalesmanAdditionalInfo"
		private m_RetrieveStatement as string = "up_RetrieveSalesmanAdditionalInfo"
		private m_RetrieveListStatement as string = "up_RetrieveSalesmanAdditionalInfoList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteSalesmanAdditionalInfo"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim salesmanAdditionalInfo as SalesmanAdditionalInfo = nothing
			while dr.Read
			
				salesmanAdditionalInfo = me.CreateObject(dr)
			            
			end while        					
			
			return salesmanAdditionalInfo
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim salesmanAdditionalInfoList as ArrayList = new ArrayList
			
			while dr.Read
					dim salesmanAdditionalInfo as SalesmanAdditionalInfo = me.CreateObject(dr)
					salesmanAdditionalInfoList.Add(salesmanAdditionalInfo)
			end while
			     
			return salesmanAdditionalInfoList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim salesmanAdditionalInfo as SalesmanAdditionalInfo = ctype(obj, SalesmanAdditionalInfo)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,salesmanAdditionalInfo.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim salesmanAdditionalInfo as SalesmanAdditionalInfo = ctype(obj, SalesmanAdditionalInfo)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.SalesmanHeader))
            'DbCommandWrapper.AddInParameter("@ContractDetailID", DbType.Int32, Me.GetRefObject(pODetail.ContractDetail))
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID_Ref", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.SalesmanHeader_Ref))
			DbCommandWrapper.AddInParameter("@ReligionID",DbType.AnsiString,salesmanAdditionalInfo.ReligionID)
            DbCommandWrapper.AddInParameter("@SalesmanCategoryLevelID", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.SalesmanCategoryLevel))
            DBCommandWrapper.AddInParameter("@SalesmanLevel", DbType.Int32, salesmanAdditionalInfo.SalesmanLevel)
            DbCommandWrapper.AddInParameter("@Salary", DbType.Currency, salesmanAdditionalInfo.Salary)
            DbCommandWrapper.AddInParameter("@KtpImagePath", DbType.AnsiString, salesmanAdditionalInfo.KtpImagePath)
            DbCommandWrapper.AddInParameter("@CSOHireDate", DbType.DateTime, salesmanAdditionalInfo.CSOHireDate)
            DbCommandWrapper.AddInParameter("@AppointmentLetterPath", DbType.AnsiString, salesmanAdditionalInfo.AppointmentLetterPath)
            DbCommandWrapper.AddInParameter("@BirthCityID", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.BirthCity))
            DbCommandWrapper.AddInParameter("@AddressCityID", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.AddressCity))
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,salesmanAdditionalInfo.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,salesmanAdditionalInfo.LastUpdateBy)
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
		
			dim salesmanAdditionalInfo as SalesmanAdditionalInfo = ctype(obj, SalesmanAdditionalInfo)
			
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_UpdateStatement)
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,salesmanAdditionalInfo.ID)
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.SalesmanHeader))
            DBCommandWrapper.AddInParameter("@SalesmanHeaderID_Ref", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.SalesmanHeader_Ref))
            DbCommandWrapper.AddInParameter("@ReligionID", DbType.AnsiString, salesmanAdditionalInfo.ReligionID)
            DbCommandWrapper.AddInParameter("@SalesmanCategoryLevelID", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.SalesmanCategoryLevel))
            DBCommandWrapper.AddInParameter("@SalesmanLevel", DbType.Int32, salesmanAdditionalInfo.SalesmanLevel)
            DbCommandWrapper.AddInParameter("@Salary", DbType.Currency, salesmanAdditionalInfo.Salary)
            DbCommandWrapper.AddInParameter("@KtpImagePath", DbType.AnsiString, salesmanAdditionalInfo.KtpImagePath)
            DbCommandWrapper.AddInParameter("@CSOHireDate", DbType.DateTime, salesmanAdditionalInfo.CSOHireDate)
            DbCommandWrapper.AddInParameter("@AppointmentLetterPath", DbType.AnsiString, salesmanAdditionalInfo.AppointmentLetterPath)
            DbCommandWrapper.AddInParameter("@BirthCityID", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.BirthCity))
            DbCommandWrapper.AddInParameter("@AddressCityID", DbType.Int32, Me.GetRefObject(salesmanAdditionalInfo.AddressCity))
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,salesmanAdditionalInfo.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,salesmanAdditionalInfo.CreatedBy)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString, User)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
			
						
						
			return DbCommandWrapper
			
		end function

	#end region
	
	
	#region "Private Methods"
	
		private function CreateObject(byval dr as IDataReader) as SalesmanAdditionalInfo
		
			dim salesmanAdditionalInfo as SalesmanAdditionalInfo = new SalesmanAdditionalInfo
			
            salesmanAdditionalInfo.ID = CType(dr("ID"), Integer)
            
            If Not dr.IsDBNull(dr.GetOrdinal("ReligionID")) Then salesmanAdditionalInfo.ReligionID = dr("ReligionID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanLevel")) Then salesmanAdditionalInfo.SalesmanLevel = CType(dr("SalesmanLevel"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Salary")) Then salesmanAdditionalInfo.Salary = CType(dr("Salary"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("KtpImagePath")) Then salesmanAdditionalInfo.KtpImagePath = dr("KtpImagePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CSOHireDate")) Then salesmanAdditionalInfo.CSOHireDate = CType(dr("CSOHireDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AppointmentLetterPath")) Then salesmanAdditionalInfo.AppointmentLetterPath = dr("AppointmentLetterPath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanAdditionalInfo.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanAdditionalInfo.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanAdditionalInfo.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanAdditionalInfo.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanAdditionalInfo.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanAdditionalInfo.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID_Ref")) Then
                salesmanAdditionalInfo.SalesmanHeader_Ref = New SalesmanHeader(CType(dr("SalesmanHeaderID_Ref"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCategoryLevelID")) Then
                salesmanAdditionalInfo.SalesmanCategoryLevel = New SalesmanCategoryLevel(CType(dr("SalesmanCategoryLevelID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BirthCityID")) Then
                salesmanAdditionalInfo.BirthCity = New City(CType(dr("BirthCityID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("AddressCityID")) Then
                salesmanAdditionalInfo.AddressCity = New City(CType(dr("AddressCityID"), Integer))
            End If

            Return salesmanAdditionalInfo
		
		end function
		
		private sub SetTableName()
		
			if not (gettype (SalesmanAdditionalInfo) is nothing) then
			
				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(SalesmanAdditionalInfo),gettype(TableInfoAttribute)), TableInfoAttribute)
				
				if not isnothing(attr) then
					m_TableName = attr.TableName
				else
					throw new SearchException(gettype(SalesmanAdditionalInfo).ToString + " does not have TableInfoAttribute.")
				end if
			end if
		end sub
		
		#end region
		
#Region "Custom Method"

#End Region

end class
end namespace

