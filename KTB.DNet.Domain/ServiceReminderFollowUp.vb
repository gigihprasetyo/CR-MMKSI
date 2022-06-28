
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceReminderFollowUp Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 06/07/2020 - 14:51:25
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("ServiceReminderFollowUp")> _
    Public Class ServiceReminderFollowUp
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _serviceReminderID As Integer
        Private _followUpStatus As Integer
        Private _followUpAction As String = String.Empty
        Private _followUpDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _bookingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _serviceReminder As ServiceReminder
        Private _serviceBooking As ServiceBooking


#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        '<ColumnInfo("ServiceReminderID", "{0}")> _
        'Public Property ServiceReminderID As Integer
        '    Get
        '        Return _serviceReminderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _serviceReminderID = value
        '    End Set
        'End Property


        <ColumnInfo("ServiceReminderID", "{0}"), _
        RelationInfo("ServiceReminder", "ID", "ServiceReminderFollowUp", "ServiceReminderID")> _
        Public Property ServiceReminder() As ServiceReminder
            Get
                Try
                    If Not IsNothing(Me._serviceReminder) AndAlso (Not Me._serviceReminder.IsLoaded) Then

                        Me._serviceReminder = CType(DoLoad(GetType(ServiceReminder).ToString(), _serviceReminder.ID), ServiceReminder)
                        Me._serviceReminder.MarkLoaded()

                    End If

                    Return Me._serviceReminder

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ServiceReminder)

                Me._serviceReminder = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._serviceReminder.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ServiceBookingID", "{0}"), _
        RelationInfo("ServiceBooking", "ID", "ServiceReminderFollowUp", "ServiceBookingID")> _
        Public Property ServiceBooking() As ServiceBooking
            Get
                Try
                    If Not IsNothing(Me._serviceBooking) AndAlso (Not Me._serviceBooking.IsLoaded) Then

                        Me._serviceBooking = CType(DoLoad(GetType(ServiceBooking).ToString(), _serviceBooking.ID), ServiceBooking)
                        If Not IsNothing(Me._serviceBooking) Then
                            Me._serviceBooking.MarkLoaded()
                        End If
                    End If

                    Return Me._serviceBooking

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ServiceBooking)

                Me._serviceBooking = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._serviceBooking.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("FollowUpStatus", "{0}")> _
        Public Property FollowUpStatus As Integer
            Get
                Return _followUpStatus
            End Get
            Set(ByVal value As Integer)
                _followUpStatus = value
            End Set
        End Property


        <ColumnInfo("FollowUpAction", "'{0}'")> _
        Public Property FollowUpAction As String
            Get
                Return _followUpAction
            End Get
            Set(ByVal value As String)
                _followUpAction = value
            End Set
        End Property


        <ColumnInfo("FollowUpDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FollowUpDate As DateTime
            Get
                Return _followUpDate
            End Get
            Set(ByVal value As DateTime)
                _followUpDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("BookingDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property BookingDate As DateTime
            Get
                Return _bookingDate
            End Get
            Set(ByVal value As DateTime)
                _bookingDate = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property




#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


