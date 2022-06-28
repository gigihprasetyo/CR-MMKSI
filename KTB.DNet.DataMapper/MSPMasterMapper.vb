
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/6/2017 - 10:39:57 AM
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

	public class MSPMasterMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertMSPMaster"
		private m_UpdateStatement as string = "up_UpdateMSPMaster"
		private m_RetrieveStatement as string = "up_RetrieveMSPMaster"
		private m_RetrieveListStatement as string = "up_RetrieveMSPMasterList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteMSPMaster"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim mSPMaster as MSPMaster = nothing
			while dr.Read
			
				mSPMaster = me.CreateObject(dr)
			            
			end while        					
			
			return mSPMaster
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim mSPMasterList as ArrayList = new ArrayList
			
			while dr.Read
					dim mSPMaster as MSPMaster = me.CreateObject(dr)
					mSPMasterList.Add(mSPMaster)
			end while
			     
			return mSPMasterList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim mSPMaster as MSPMaster = ctype(obj, MSPMaster)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,mSPMaster.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim mSPMaster as MSPMaster = ctype(obj, MSPMaster)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)

			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
            DbCommandWrapper.AddInParameter("@MSPTypeID", DbType.Int32, mSPMaster.MSPType.ID)
            DbCommandWrapper.AddInParameter("@VehicleTypeID", DbType.Int32, mSPMaster.VehicleType.ID)
            DbCommandWrapper.AddInParameter("@MSPKm", DbType.Int32, mSPMaster.MSPKm)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPMaster.Duration)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, mSPMaster.Amount)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, mSPMaster.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, mSPMaster.EndDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPMaster.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mSPMaster.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, mSPMaster.LastUpdateTime)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPMaster As MSPMaster = CType(obj, MSPMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPMaster.ID)
            DbCommandWrapper.AddInParameter("@MSPTypeID", DbType.Int32, mSPMaster.MSPType.ID)
            DbCommandWrapper.AddInParameter("@VehicleTypeID", DbType.Int32, mSPMaster.VehicleType.ID)
            DbCommandWrapper.AddInParameter("@MSPKm", DbType.Int32, mSPMaster.MSPKm)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPMaster.Duration)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, mSPMaster.Amount)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, mSPMaster.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, mSPMaster.EndDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPMaster.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mSPMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, mSPMaster.LastUpdateTime)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MSPMaster

            Dim mSPMaster As MSPMaster = New MSPMaster

            mSPMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPKm")) Then mSPMaster.MSPKm = CType(dr("MSPKm"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Duration")) Then mSPMaster.Duration = CType(dr("Duration"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then mSPMaster.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then mSPMaster.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndDate")) Then mSPMaster.EndDate = CType(dr("EndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then mSPMaster.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mSPMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mSPMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mSPMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mSPMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mSPMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeID")) Then
                mSPMaster.VehicleType = New VechileType(CType(dr("VehicleTypeID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("MSPTypeID")) Then
                mSPMaster.MSPType = New MSPType(CType(dr("MSPTypeID"), Integer))
            End If


            Return mSPMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(MSPMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MSPMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MSPMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

