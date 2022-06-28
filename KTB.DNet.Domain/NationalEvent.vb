#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : NationalEvent Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 4/21/2021 - 11:23:15 AM
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
    <Serializable(), TableInfo("NationalEvent")> _
    Public Class NationalEvent
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
        Private _regNumber As String = String.Empty
        Private _periodStart As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodEnd As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealerCityID As String = String.Empty
        Private _targetProspect As Integer
        Private _targetSPK As Integer

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _nationalEventDetails As ArrayList = New ArrayList
        Private _spkNationalEvents As ArrayList = New ArrayList

        Private _nationalEventType As NationalEventType
        Private _nationalEventCity As NationalEventCity
        Private _nationalEventVenue As NationalEventVenue
        Private _dealerArea1 As Area1

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


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("PeriodStart", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodStart As DateTime
            Get
                Return _periodStart
            End Get
            Set(ByVal value As DateTime)
                _periodStart = value
            End Set
        End Property


        <ColumnInfo("PeriodEnd", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodEnd As DateTime
            Get
                Return _periodEnd
            End Get
            Set(ByVal value As DateTime)
                _periodEnd = value
            End Set
        End Property


        <ColumnInfo("DealerCityID", "'{0}'")> _
        Public Property DealerCityID As String
            Get
                Return _dealerCityID
            End Get
            Set(ByVal value As String)
                _dealerCityID = value
            End Set
        End Property


        <ColumnInfo("TargetProspect", "{0}")> _
        Public Property TargetProspect As Integer
            Get
                Return _targetProspect
            End Get
            Set(ByVal value As Integer)
                _targetProspect = value
            End Set
        End Property


        <ColumnInfo("TargetSPK", "{0}")> _
        Public Property TargetSPK As Integer
            Get
                Return _targetSPK
            End Get
            Set(ByVal value As Integer)
                _targetSPK = value
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


        <ColumnInfo("NationalEventTypeID", "{0}"), _
        RelationInfo("NationalEventType", "ID", "NationalEvent", "NationalEventTypeID")> _
        Public Property NationalEventType As NationalEventType
            Get
                Try
                    If Not IsNothing(Me._nationalEventType) AndAlso (Not Me._nationalEventType.IsLoaded) Then

                        Me._nationalEventType = CType(DoLoad(GetType(NationalEventType).ToString(), _nationalEventType.ID), NationalEventType)
                        Me._nationalEventType.MarkLoaded()

                    End If

                    Return Me._nationalEventType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As NationalEventType)

                Me._nationalEventType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._nationalEventType.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("NationalEventCityID", "{0}"), _
        RelationInfo("NationalEventCity", "ID", "NationalEvent", "NationalEventCityID")> _
        Public Property NationalEventCity As NationalEventCity
            Get
                Try
                    If Not IsNothing(Me._nationalEventCity) AndAlso (Not Me._nationalEventCity.IsLoaded) Then

                        Me._nationalEventCity = CType(DoLoad(GetType(NationalEventCity).ToString(), _nationalEventCity.ID), NationalEventCity)
                        Me._nationalEventCity.MarkLoaded()

                    End If

                    Return Me._nationalEventCity

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As NationalEventCity)

                Me._nationalEventCity = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._nationalEventCity.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("NationalEventVenueID", "{0}"), _
        RelationInfo("NationalEventVenue", "ID", "NationalEvent", "NationalEventVenueID")> _
        Public Property NationalEventVenue As NationalEventVenue
            Get
                Try
                    If Not IsNothing(Me._nationalEventVenue) AndAlso (Not Me._nationalEventVenue.IsLoaded) Then

                        Me._nationalEventVenue = CType(DoLoad(GetType(NationalEventVenue).ToString(), _nationalEventVenue.ID), NationalEventVenue)
                        Me._nationalEventVenue.MarkLoaded()

                    End If

                    Return Me._nationalEventVenue

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As NationalEventVenue)

                Me._nationalEventVenue = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._nationalEventVenue.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("DealerArea1ID", "{0}"), _
        RelationInfo("Area1", "ID", "NationalEvent", "DealerArea1ID")> _
        Public Property DealerArea1 As Area1
            Get
                Try
                    If Not IsNothing(Me._dealerArea1) AndAlso (Not Me._dealerArea1.IsLoaded) Then

                        Me._dealerArea1 = CType(DoLoad(GetType(Area1).ToString(), _dealerArea1.ID), Area1)
                        Me._dealerArea1.MarkLoaded()

                    End If

                    Return Me._dealerArea1

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Area1)

                Me._dealerArea1 = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerArea1.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("NationalEvent", "ID", "NationalEventDetail", "NationalEventID")> _
        Public ReadOnly Property NationalEventDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._nationalEventDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(NationalEventDetail), "NationalEvent", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(NationalEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._nationalEventDetails = DoLoadArray(GetType(NationalEventDetail).ToString, criterias)
                    End If

                    Return Me._nationalEventDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <RelationInfo("NationalEvent", "ID", "SPKNationalEvent", "NationalEventID")> _
        Public ReadOnly Property SPKNationalEvents As System.Collections.ArrayList
            Get
                Try
                    If (Me._spkNationalEvents.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKNationalEvent), "NationalEvent", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKNationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._spkNationalEvents = DoLoadArray(GetType(SPKNationalEvent).ToString, criterias)
                    End If

                    Return Me._spkNationalEvents

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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
