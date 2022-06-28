
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerCaseResponse Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 7/10/2017 - 10:10:16 AM
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
    <Serializable(), TableInfo("CustomerCaseResponse")> _
    Public Class CustomerCaseResponse
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
        Private _workOrderNumber As String = String.Empty
        Private _subject As String = String.Empty
        Private _description As String = String.Empty
        Private _status As Short
        Private _isSend As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTIme As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _customerCase As CustomerCase
        Private _customerCaseResponseEvidences As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _serviceBooking As ServiceBooking

        Private _response As Short
        Private _bookingDatetime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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

        <ColumnInfo("WorkOrderNumber", "'{0}'")> _
        Public Property WorkOrderNumber As String
            Get
                Return _workOrderNumber
            End Get
            Set(ByVal value As String)
                _workOrderNumber = value
            End Set
        End Property

        <ColumnInfo("Subject", "'{0}'")> _
        Public Property Subject As String
            Get
                Return _subject
            End Get
            Set(ByVal value As String)
                _subject = value
            End Set
        End Property

        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("IsSend", "{0}")> _
        Public Property IsSend As Short
            Get
                Return _isSend
            End Get
            Set(ByVal value As Short)
                _isSend = value
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


        <ColumnInfo("LastUpdateTIme", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTIme As DateTime
            Get
                Return _lastUpdateTIme
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTIme = value
            End Set
        End Property


        <ColumnInfo("CustomerCaseID", "{0}"), _
        RelationInfo("CustomerCase", "ID", "CustomerCaseResponse", "CustomerCaseID")> _
        Public Property CustomerCase As CustomerCase
            Get
                Try
                    If Not isnothing(Me._customerCase) AndAlso (Not Me._customerCase.IsLoaded) Then

                        Me._customerCase = CType(DoLoad(GetType(CustomerCase).ToString(), _customerCase.ID), CustomerCase)
                        Me._customerCase.MarkLoaded()

                    End If

                    Return Me._customerCase

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CustomerCase)

                Me._customerCase = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._customerCase.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("CustomerCaseResponse", "ID", "CustomerCaseResponseEvidence", "CustomerCaseResponseID")> _
        Public ReadOnly Property CustomerCaseResponseEvidences() As System.Collections.ArrayList
            Get
                Try
                    If (Me._customerCaseResponseEvidences.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CustomerCaseResponseEvidence), "CustomerCaseResponse", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CustomerCaseResponseEvidence), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._customerCaseResponseEvidences = DoLoadArray(GetType(CustomerCaseResponseEvidence).ToString, criterias)
                    End If

                    Return Me._customerCaseResponseEvidences

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("ServiceBookingID", "{0}"), _
        RelationInfo("ServiceBooking", "ID", "CustomerCaseResponse", "ServiceBookingID")> _
        Public Property ServiceBooking As ServiceBooking
            Get
                Try
                    If Not IsNothing(Me._serviceBooking) AndAlso (Not Me._serviceBooking.IsLoaded) Then

                        Me._serviceBooking = CType(DoLoad(GetType(ServiceBooking).ToString(), _serviceBooking.ID), ServiceBooking)
                        If Not IsNothing(_serviceBooking) Then
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

        <ColumnInfo("Response", "{0}")> _
        Public Property Response As Short
            Get
                Return _response
            End Get
            Set(ByVal value As Short)
                _response = value
            End Set
        End Property


        <ColumnInfo("BookingDatetime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property BookingDatetime As DateTime
            Get
                Return _bookingDatetime
            End Get
            Set(ByVal value As DateTime)
                _bookingDatetime = value
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

#Region "Custom Properties"
#End Region

#Region "Custom Method"
#End Region

    End Class
End Namespace

