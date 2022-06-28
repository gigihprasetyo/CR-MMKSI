#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceBooking Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:05:06 PM
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

    Public Class ServiceBookingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceBooking"
        Private m_UpdateStatement As String = "up_UpdateServiceBooking"
        Private m_RetrieveStatement As String = "up_RetrieveServiceBooking"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceBookingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceBooking"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ServiceBooking As ServiceBooking = Nothing
            While dr.Read

                ServiceBooking = Me.CreateObject(dr)

            End While

            Return ServiceBooking

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ServiceBookingList As ArrayList = New ArrayList

            While dr.Read
                Dim ServiceBooking As ServiceBooking = Me.CreateObject(dr)
                ServiceBookingList.Add(ServiceBooking)
            End While

            Return ServiceBookingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceBooking As ServiceBooking = CType(obj, ServiceBooking)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceBooking.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceBooking As ServiceBooking = CType(obj, ServiceBooking)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ServiceBookingCode", DbType.AnsiString, ServiceBooking.ServiceBookingCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, ServiceBooking.ChassisNumber)
            DbCommandWrapper.AddInParameter("@PlateNumber", DbType.AnsiString, ServiceBooking.PlateNumber)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, ServiceBooking.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerPhoneNumber", DbType.AnsiString, ServiceBooking.CustomerPhoneNumber)
            DbCommandWrapper.AddInParameter("@Odometer", DbType.Int32, ServiceBooking.OdoMeter)
            DbCommandWrapper.AddInParameter("@PickupType", DbType.Int16, ServiceBooking.PickupType)
            DbCommandWrapper.AddInParameter("@StallServiceType", DbType.Int16, ServiceBooking.StallServiceType)
            DbCommandWrapper.AddInParameter("@StandardTime", DbType.Decimal, ServiceBooking.StandardTime)
            DbCommandWrapper.AddInParameter("@IncomingDateStart", DbType.DateTime, ServiceBooking.IncomingDateStart)
            DbCommandWrapper.AddInParameter("@IncomingDateEnd", DbType.DateTime, ServiceBooking.IncomingDateEnd)
            DbCommandWrapper.AddInParameter("@WorkingTimeStart", DbType.DateTime, ServiceBooking.WorkingTimeStart)
            DbCommandWrapper.AddInParameter("@WorkingTimeEnd", DbType.DateTime, ServiceBooking.WorkingTimeEnd)
            DbCommandWrapper.AddInParameter("@IsMitsubishi", DbType.Int16, ServiceBooking.IsMitsubishi)
            DbCommandWrapper.AddInParameter("@VehicleTypeDescription", DbType.AnsiString, ServiceBooking.VehicleTypeDescription)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, ServiceBooking.Notes)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, ServiceBooking.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceBooking.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, ServiceBooking.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceBooking.Dealer))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(ServiceBooking.ChassisMaster))
            DbCommandWrapper.AddInParameter("@StallMasterID", DbType.Int32, Me.GetRefObject(ServiceBooking.StallMaster))
            DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int16, Me.GetRefObject(ServiceBooking.VechileModel))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceBooking.VechileType))
            DbCommandWrapper.AddInParameter("@ServiceAdvisorID", DbType.Int32, Me.GetRefObject(ServiceBooking.TrTrainee))

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

            Dim ServiceBooking As ServiceBooking = CType(obj, ServiceBooking)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)



            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceBooking.ID)
            DbCommandWrapper.AddInParameter("@ServiceBookingCode", DbType.AnsiString, ServiceBooking.ServiceBookingCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, ServiceBooking.ChassisNumber)
            DbCommandWrapper.AddInParameter("@PlateNumber", DbType.AnsiString, ServiceBooking.PlateNumber)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, ServiceBooking.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerPhoneNumber", DbType.AnsiString, ServiceBooking.CustomerPhoneNumber)
            DbCommandWrapper.AddInParameter("@Odometer", DbType.Int32, ServiceBooking.OdoMeter)
            DbCommandWrapper.AddInParameter("@PickupType", DbType.Int16, ServiceBooking.PickupType)
            DbCommandWrapper.AddInParameter("@StallServiceType", DbType.Int16, ServiceBooking.StallServiceType)
            DbCommandWrapper.AddInParameter("@StandardTime", DbType.Decimal, ServiceBooking.StandardTime)
            DbCommandWrapper.AddInParameter("@IncomingDateStart", DbType.DateTime, ServiceBooking.IncomingDateStart)
            DbCommandWrapper.AddInParameter("@IncomingDateEnd", DbType.DateTime, ServiceBooking.IncomingDateEnd)
            DbCommandWrapper.AddInParameter("@WorkingTimeStart", DbType.DateTime, ServiceBooking.WorkingTimeStart)
            DbCommandWrapper.AddInParameter("@WorkingTimeEnd", DbType.DateTime, ServiceBooking.WorkingTimeEnd)
            DbCommandWrapper.AddInParameter("@IsMitsubishi", DbType.Int16, ServiceBooking.IsMitsubishi)
            DbCommandWrapper.AddInParameter("@VehicleTypeDescription", DbType.AnsiString, ServiceBooking.VehicleTypeDescription)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, ServiceBooking.Notes)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, ServiceBooking.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceBooking.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ServiceBooking.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceBooking.Dealer))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(ServiceBooking.ChassisMaster))
            DbCommandWrapper.AddInParameter("@StallMasterID", DbType.Int32, Me.GetRefObject(ServiceBooking.StallMaster))
            DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int16, Me.GetRefObject(ServiceBooking.VechileModel))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceBooking.VechileType))
            DbCommandWrapper.AddInParameter("@ServiceAdvisorID", DbType.Int32, Me.GetRefObject(ServiceBooking.TrTrainee))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceBooking

            Dim ServiceBooking As ServiceBooking = New ServiceBooking

            ServiceBooking.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceBookingCode")) Then ServiceBooking.ServiceBookingCode = dr("ServiceBookingCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then ServiceBooking.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PlateNumber")) Then ServiceBooking.PlateNumber = dr("PlateNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then ServiceBooking.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerPhoneNumber")) Then ServiceBooking.CustomerPhoneNumber = dr("CustomerPhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Odometer")) Then ServiceBooking.OdoMeter = CType(dr("Odometer"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PickupType")) Then ServiceBooking.PickupType = CType(dr("PickupType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StallServiceType")) Then ServiceBooking.StallServiceType = CType(dr("StallServiceType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StandardTime")) Then ServiceBooking.StandardTime = CType(dr("StandardTime"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IncomingDateStart")) Then ServiceBooking.IncomingDateStart = CType(dr("IncomingDateStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IncomingDateEnd")) Then ServiceBooking.IncomingDateEnd = CType(dr("IncomingDateEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkingTimeStart")) Then ServiceBooking.WorkingTimeStart = CType(dr("WorkingTimeStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkingTimeEnd")) Then ServiceBooking.WorkingTimeEnd = CType(dr("WorkingTimeEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsMitsubishi")) Then ServiceBooking.IsMitsubishi = CType(dr("IsMitsubishi"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeDescription")) Then ServiceBooking.VehicleTypeDescription = dr("VehicleTypeDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then ServiceBooking.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then ServiceBooking.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceBooking.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ServiceBooking.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ServiceBooking.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then ServiceBooking.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then ServiceBooking.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                ServiceBooking.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                ServiceBooking.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("StallMasterID")) Then
                ServiceBooking.StallMaster = New StallMaster(CType(dr("StallMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileModelID")) Then
                ServiceBooking.VechileModel = New VechileModel(CType(dr("VechileModelID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                ServiceBooking.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceAdvisorID")) Then
                ServiceBooking.TrTrainee = New TrTrainee(CType(dr("ServiceAdvisorID"), Integer))
            End If

            Return ServiceBooking

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceBooking) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceBooking), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceBooking).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

