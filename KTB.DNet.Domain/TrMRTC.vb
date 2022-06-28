
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrMRTC Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 10/07/2019 - 10:12:01
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
    <Serializable(), TableInfo("TrMRTC")> _
    Public Class TrMRTC
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
        Private _code As String = String.Empty
        Private _name As String = String.Empty
        Private _grade As Short
        Private _isMainDealer As Boolean = False
        Private _dealer As Dealer 'pemilik/owner MRTC
        Private _address As String = String.Empty
        Private _mainArea As MainArea
        Private _area1 As Area1
        Private _city As City
        Private _pricePerDay As Decimal
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _listOfPIC As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _listOfDealer As System.Collections.ArrayList = New System.Collections.ArrayList 'list dealer yg boleh pakai MRTC ini


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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        <ColumnInfo("Grade", "{0}")> _
        Public Property Grade As Short
            Get
                Return _grade
            End Get
            Set(ByVal value As Short)
                _grade = value
            End Set
        End Property


        <ColumnInfo("IsMainDealer", "'{0}'")> _
        Public Property IsMainDealer As Boolean
            Get
                Return _isMainDealer
            End Get
            Set(ByVal value As Boolean)
                _isMainDealer = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
         RelationInfo("Dealer", "ID", "TrMRTC", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("Address", "'{0}'")> _
        Public Property Address As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property


        <ColumnInfo("MainAreaID", "{0}"), _
           RelationInfo("MainArea", "ID", "TrMRTC", "MainAreaID")> _
        Public Property MainArea() As MainArea
            Get
                Try
                    If Not IsNothing(Me._mainArea) AndAlso (Not Me._mainArea.IsLoaded) Then

                        Me._mainArea = CType(DoLoad(GetType(MainArea).ToString(), _mainArea.ID), MainArea)
                        Me._mainArea.MarkLoaded()

                    End If

                    Return Me._mainArea

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MainArea)

                Me._mainArea = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mainArea.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("Area1ID", "{0}"), _
           RelationInfo("Area1", "ID", "TrMRTC", "Area1ID")> _
        Public Property Area1() As Area1
            Get
                Try
                    If Not IsNothing(Me._area1) AndAlso (Not Me._area1.IsLoaded) Then

                        Me._area1 = CType(DoLoad(GetType(Area1).ToString(), _area1.ID), Area1)
                        Me._area1.MarkLoaded()

                    End If

                    Return Me._area1

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Area1)

                Me._area1 = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._area1.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("CityID", "{0}"), _
           RelationInfo("City", "ID", "TrMRTC", "CityID")> _
        Public Property City() As City
            Get
                Try
                    If Not IsNothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

                        Me._city = CType(DoLoad(GetType(City).ToString(), _city.ID), City)
                        Me._city.MarkLoaded()

                    End If

                    Return Me._city

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._city = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PricePerDay", "{0}")> _
        Public Property PricePerDay As Decimal
            Get
                Return _pricePerDay
            End Get
            Set(ByVal value As Decimal)
                _pricePerDay = value
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

        <RelationInfo("TrMRTC", "ID", "TrMRTCPIC", "TrMRTCID")> _
        Public ReadOnly Property ListOfDetail() As System.Collections.ArrayList
            Get
                Try
                    If (Me._listOfPIC.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrMRTCPIC), "TrMRTC.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrMRTCPIC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._listOfPIC = DoLoadArray(GetType(TrMRTCPIC).ToString, criterias)
                    End If

                    Return Me._listOfPIC

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("TrMRTC", "ID", "TrMRTCDealer", "TrMRTCID")> _
        Public ReadOnly Property ListOfDealer() As System.Collections.ArrayList
            Get
                Try
                    If (Me._listOfDealer.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrMRTCDealer), "TrMRTC.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrMRTCDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._listOfDealer = DoLoadArray(GetType(TrMRTCDealer).ToString, criterias)
                    End If

                    Return Me._listOfDealer

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

