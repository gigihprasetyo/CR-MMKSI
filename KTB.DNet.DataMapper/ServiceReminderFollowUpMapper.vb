
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceReminderFollowUp Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 06/07/2020 - 14:52:26
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

    Public Class ServiceReminderFollowUpMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceReminderFollowUp"
        Private m_UpdateStatement As String = "up_UpdateServiceReminderFollowUp"
        Private m_RetrieveStatement As String = "up_RetrieveServiceReminderFollowUp"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceReminderFollowUpList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceReminderFollowUp"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim serviceReminderFollowUp As ServiceReminderFollowUp = Nothing
            While dr.Read

                serviceReminderFollowUp = Me.CreateObject(dr)

            End While

            Return serviceReminderFollowUp

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim serviceReminderFollowUpList As ArrayList = New ArrayList

            While dr.Read
                Dim serviceReminderFollowUp As ServiceReminderFollowUp = Me.CreateObject(dr)
                serviceReminderFollowUpList.Add(serviceReminderFollowUp)
            End While

            Return serviceReminderFollowUpList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceReminderFollowUp As ServiceReminderFollowUp = CType(obj, ServiceReminderFollowUp)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceReminderFollowUp.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceReminderFollowUp As ServiceReminderFollowUp = CType(obj, ServiceReminderFollowUp)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@ServiceReminderID", DbType.Int32, serviceReminderFollowUp.ServiceReminderID)
            DbCommandWrapper.AddInParameter("@ServiceReminderID", DbType.Int32, Me.GetRefObject(serviceReminderFollowUp.ServiceReminder))
            DbCommandWrapper.AddInParameter("@ServiceBookingID", DbType.Int32, Me.GetRefObject(serviceReminderFollowUp.ServiceBooking))
            DbCommandWrapper.AddInParameter("@FollowUpStatus", DbType.Int32, serviceReminderFollowUp.FollowUpStatus)
            DbCommandWrapper.AddInParameter("@FollowUpAction", DbType.AnsiString, serviceReminderFollowUp.FollowUpAction)
            DbCommandWrapper.AddInParameter("@FollowUpDate", DbType.DateTime, serviceReminderFollowUp.FollowUpDate)
            DbCommandWrapper.AddInParameter("@BookingDate", DbType.DateTime, serviceReminderFollowUp.BookingDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceReminderFollowUp.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, serviceReminderFollowUp.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim serviceReminderFollowUp As ServiceReminderFollowUp = CType(obj, ServiceReminderFollowUp)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceReminderFollowUp.ID)
            'DbCommandWrapper.AddInParameter("@ServiceReminderID", DbType.Int32, serviceReminderFollowUp.ServiceReminderID)
            DbCommandWrapper.AddInParameter("@ServiceReminderID", DbType.Int32, Me.GetRefObject(serviceReminderFollowUp.ServiceReminder))
            DbCommandWrapper.AddInParameter("@ServiceBookingID", DbType.Int32, Me.GetRefObject(serviceReminderFollowUp.ServiceBooking))
            DbCommandWrapper.AddInParameter("@FollowUpStatus", DbType.Int32, serviceReminderFollowUp.FollowUpStatus)
            DbCommandWrapper.AddInParameter("@FollowUpAction", DbType.AnsiString, serviceReminderFollowUp.FollowUpAction)
            DbCommandWrapper.AddInParameter("@FollowUpDate", DbType.DateTime, serviceReminderFollowUp.FollowUpDate)
            DbCommandWrapper.AddInParameter("@BookingDate", DbType.DateTime, serviceReminderFollowUp.BookingDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceReminderFollowUp.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, serviceReminderFollowUp.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceReminderFollowUp

            Dim serviceReminderFollowUp As ServiceReminderFollowUp = New ServiceReminderFollowUp

            serviceReminderFollowUp.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("ServiceReminderID")) Then serviceReminderFollowUp.ServiceReminderID = CType(dr("ServiceReminderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FollowUpStatus")) Then serviceReminderFollowUp.FollowUpStatus = CType(dr("FollowUpStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FollowUpAction")) Then serviceReminderFollowUp.FollowUpAction = dr("FollowUpAction").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FollowUpDate")) Then serviceReminderFollowUp.FollowUpDate = CType(dr("FollowUpDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BookingDate")) Then serviceReminderFollowUp.BookingDate = CType(dr("BookingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then serviceReminderFollowUp.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then serviceReminderFollowUp.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then serviceReminderFollowUp.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then serviceReminderFollowUp.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then serviceReminderFollowUp.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ServiceReminderID")) Then
                serviceReminderFollowUp.ServiceReminder = New ServiceReminder(CType(dr("ServiceReminderID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ServiceBookingID")) Then
                serviceReminderFollowUp.ServiceBooking = New ServiceBooking(CType(dr("ServiceBookingID"), Integer))
            End If

            Return serviceReminderFollowUp

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceReminderFollowUp) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceReminderFollowUp), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceReminderFollowUp).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


