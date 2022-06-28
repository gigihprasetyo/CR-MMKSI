
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetHasilSurveyDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 8/31/2017 - 1:40:52 PM
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

	public class FleetHasilSurveyDetailMapper
		inherits AbstractMapper

	#region "Constructors/Destructors/Finalizers"
	
		public sub new()
			Db = DatabaseFactory.CreateDatabase
			SetTableName
		end sub
		
	#end region
	
	#region "Private Variables"
	
		private m_InsertStatement as string = "up_InsertFleetHasilSurveyDetail"
		private m_UpdateStatement as string = "up_UpdateFleetHasilSurveyDetail"
		private m_RetrieveStatement as string = "up_RetrieveFleetHasilSurveyDetail"
		private m_RetrieveListStatement as string = "up_RetrieveFleetHasilSurveyDetailList"
		private m_DinamicQuery as string = "up_DinamicQuery"
		private m_PagingQuery as string = "up_PagingQuery"
		private m_DeleteStatement as string = "up_DeleteFleetHasilSurveyDetail"
		
	#end region
	
	#region "Protected Methods"
	
		protected overrides function DoRetrieve(byval dr as System.Data.IDataReader) as object
		
			dim fleetHasilSurveyDetail as FleetHasilSurveyDetail = nothing
			while dr.Read
			
				fleetHasilSurveyDetail = me.CreateObject(dr)
			            
			end while        					
			
			return fleetHasilSurveyDetail
			
		end function
		
		protected overrides function DoRetrieveList(byval dr as System.Data.IDataReader) as System.Collections.ArrayList
		
			dim fleetHasilSurveyDetailList as ArrayList = new ArrayList
			
			while dr.Read
					dim fleetHasilSurveyDetail as FleetHasilSurveyDetail = me.CreateObject(dr)
					fleetHasilSurveyDetailList.Add(fleetHasilSurveyDetail)
			end while
			     
			return fleetHasilSurveyDetailList
			
		end function
		
		protected overrides function GetDeleteParameter(byval obj as object) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
					
			dim fleetHasilSurveyDetail as FleetHasilSurveyDetail = ctype(obj, FleetHasilSurveyDetail)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_DeleteStatement)
			
			
			DbCommandWrapper.AddInParameter("@ID",DbType.Int32,fleetHasilSurveyDetail.ID)
			return DbCommandWrapper
			
		end function
		
		protected overrides function GetInsertParameter(byval obj as object, byval User as string) as Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
		
			dim fleetHasilSurveyDetail as FleetHasilSurveyDetail = ctype(obj, FleetHasilSurveyDetail)
			DbCommandWrapper = Db.GetStoredProcCommandWrapper(me.m_InsertStatement)
			
			
			DbCommandWrapper.AddOutParameter("@ID",DbType.Int32,4)
			DbCommandWrapper.AddInParameter("@Amount",DbType.Int32,fleetHasilSurveyDetail.Amount)
			DbCommandWrapper.AddInParameter("@SelisihHasilSurvey",DbType.Int32,fleetHasilSurveyDetail.SelisihHasilSurvey)
			DbCommandWrapper.AddInParameter("@RowStatus",DbType.Int16,fleetHasilSurveyDetail.RowStatus)
			DbCommandWrapper.AddInParameter("@CreatedBy",DbType.AnsiString,User)
			'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
			DbCommandWrapper.AddInParameter("@LastUpdateBy",DbType.AnsiString,fleetHasilSurveyDetail.LastUpdateBy)
			'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FleetHasilSurveyHeaderID", DbType.Int32, fleetHasilSurveyDetail.FleetHasilSurveyHeaderID)
            DbCommandWrapper.AddInParameter("@CompetitorID", DbType.Int32, fleetHasilSurveyDetail.CompetitorID)
            DbCommandWrapper.AddInParameter("@VehicleClassID", DbType.Int32, fleetHasilSurveyDetail.VehicleClassID)

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

            Dim fleetHasilSurveyDetail As FleetHasilSurveyDetail = CType(obj, FleetHasilSurveyDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetHasilSurveyDetail.ID)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Int32, fleetHasilSurveyDetail.Amount)
            DbCommandWrapper.AddInParameter("@SelisihHasilSurvey", DbType.Int32, fleetHasilSurveyDetail.SelisihHasilSurvey)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetHasilSurveyDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fleetHasilSurveyDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)


            DbCommandWrapper.AddInParameter("@FleetHasilSurveyHeaderID", DbType.Int32, fleetHasilSurveyDetail.FleetHasilSurveyHeaderID)
            DbCommandWrapper.AddInParameter("@CompetitorID", DbType.Int32, fleetHasilSurveyDetail.CompetitorID)
            DbCommandWrapper.AddInParameter("@VehicleClassID", DbType.Int32, fleetHasilSurveyDetail.VehicleClassID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FleetHasilSurveyDetail

            Dim fleetHasilSurveyDetail As FleetHasilSurveyDetail = New FleetHasilSurveyDetail

            fleetHasilSurveyDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then fleetHasilSurveyDetail.Amount = CType(dr("Amount"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SelisihHasilSurvey")) Then fleetHasilSurveyDetail.SelisihHasilSurvey = CType(dr("SelisihHasilSurvey"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fleetHasilSurveyDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fleetHasilSurveyDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fleetHasilSurveyDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then fleetHasilSurveyDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then fleetHasilSurveyDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FleetHasilSurveyHeaderID")) Then
                fleetHasilSurveyDetail.FleetHasilSurveyHeaderID = CType(dr("FleetHasilSurveyHeaderID"), Integer)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CompetitorID")) Then
                fleetHasilSurveyDetail.CompetitorID = CType(dr("CompetitorID"), Integer)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleClassID")) Then
                fleetHasilSurveyDetail.VehicleClassID = CType(dr("VehicleClassID"), Integer)
            End If

            Return fleetHasilSurveyDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(FleetHasilSurveyDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FleetHasilSurveyDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FleetHasilSurveyDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region
		
		#Region "Custom Method"

		#End Region
		
end class
end namespace

