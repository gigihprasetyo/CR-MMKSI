#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_ServiceBooking Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2020 - 12:26:31 PM
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class VWI_ServiceBookingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_RetrieveStatement As String = "up_RetrieveVWI_ServiceBooking"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_ServiceBookingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_ServiceBooking As VWI_ServiceBooking = Nothing
            While dr.Read

                VWI_ServiceBooking = Me.CreateObject(dr)

            End While

            Return VWI_ServiceBooking

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_ServiceBookingList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_ServiceBooking As VWI_ServiceBooking = Me.CreateObject(dr)
                VWI_ServiceBookingList.Add(VWI_ServiceBooking)
            End While

            Return VWI_ServiceBookingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
            Throw New NotImplementedException()
        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper
            Throw New NotImplementedException()
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
            Throw New NotImplementedException()
        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_ServiceBooking

            Dim VWI_ServiceBooking As VWI_ServiceBooking = New VWI_ServiceBooking

            VWI_ServiceBooking.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceBookingCode")) Then VWI_ServiceBooking.ServiceBookingCode = dr("ServiceBookingCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_ServiceBooking.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then VWI_ServiceBooking.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then VWI_ServiceBooking.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StallMasterID")) Then VWI_ServiceBooking.StallMasterID = CInt(dr("StallMasterID").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("StallCode")) Then VWI_ServiceBooking.StallCode = dr("StallCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then VWI_ServiceBooking.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileModelDescription")) Then VWI_ServiceBooking.VechileModelDescription = dr("VechileModelDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeDescription")) Then VWI_ServiceBooking.VechileTypeDescription = dr("VechileTypeDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PlateNumber")) Then VWI_ServiceBooking.PlateNumber = dr("PlateNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then VWI_ServiceBooking.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerPhoneNumber")) Then VWI_ServiceBooking.CustomerPhoneNumber = dr("CustomerPhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Odometer")) Then VWI_ServiceBooking.Odometer = CInt(dr("Odometer").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceAdvisorID")) Then VWI_ServiceBooking.ServiceAdvisorID = CInt(dr("ServiceAdvisorID").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceAdvisorName")) Then VWI_ServiceBooking.ServiceAdvisorName = dr("ServiceAdvisorName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IncomingPlan")) Then VWI_ServiceBooking.IncomingPlan = dr("IncomingPlan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StallServiceType")) Then VWI_ServiceBooking.StallServiceType = dr("StallServiceType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StandardTime")) Then VWI_ServiceBooking.StandardTime = CDec(dr("StandardTime").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("IncomingDateStart")) Then VWI_ServiceBooking.IncomingDateStart = CType(dr("IncomingDateStart").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IncomingDateEnd")) Then VWI_ServiceBooking.IncomingDateEnd = CType(dr("IncomingDateEnd").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkingTimeStart")) Then VWI_ServiceBooking.WorkingTimeStart = CType(dr("WorkingTimeStart").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkingTimeEnd")) Then VWI_ServiceBooking.WorkingTimeEnd = CType(dr("WorkingTimeEnd").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsMitsubishi")) Then VWI_ServiceBooking.IsMitsubishi = dr("IsMitsubishi").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then VWI_ServiceBooking.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then VWI_ServiceBooking.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceBookingActivities")) Then VWI_ServiceBooking.ServiceBookingActivities = dr("ServiceBookingActivities").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then VWI_ServiceBooking.RowStatus = CShort(dr("RowStatus").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then VWI_ServiceBooking.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then VWI_ServiceBooking.CreatedTime = CType(dr("CreatedTime").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then VWI_ServiceBooking.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then VWI_ServiceBooking.LastUpdatedTime = CType(dr("LastUpdatedTime").ToString, DateTime)
            Return VWI_ServiceBooking

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_ServiceBooking) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_ServiceBooking), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_ServiceBooking).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
