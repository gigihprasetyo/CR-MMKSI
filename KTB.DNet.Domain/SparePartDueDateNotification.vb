
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDueDateNotification Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 25/06/2020 - 10:28:57
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
Imports System.Collections.Generic

#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("SparePartDueDateNotification")> _
    Public Class SparePartDueDateNotification
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
        'Private _dealerID As Short
        Private _nameRecipient As String = String.Empty
        Private _emailDealer As String = String.Empty
        Private _positionRecipient As String = String.Empty
        Private _emailNotificationKind As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _errorMessage As String = String.Empty
        Private _dealer As Dealer

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


        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID As Short
        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Short)
        '        _dealerID = value
        '    End Set
        'End Property


        <ColumnInfo("NameRecipient", "'{0}'")> _
        Public Property NameRecipient As String
            Get
                Return _nameRecipient
            End Get
            Set(ByVal value As String)
                _nameRecipient = value
            End Set
        End Property


        <ColumnInfo("EmailDealer", "'{0}'")> _
        Public Property EmailDealer As String
            Get
                Return _emailDealer
            End Get
            Set(ByVal value As String)
                _emailDealer = value
            End Set
        End Property


        <ColumnInfo("PositionRecipient", "'{0}'")> _
        Public Property PositionRecipient As String
            Get
                Return _positionRecipient
            End Get
            Set(ByVal value As String)
                _positionRecipient = value
            End Set
        End Property


        <ColumnInfo("EmailNotificationKind", "{0}")> _
        Public Property EmailNotificationKind As Short
            Get
                Return _emailNotificationKind
            End Get
            Set(ByVal value As Short)
                _emailNotificationKind = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SparePartDueDateNotification", "DealerID")> _
        Public Property Dealer As Dealer
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
        Public Function RetrieveIndenPartExpired(Optional ByVal oDealer As String = Nothing) As List(Of v_EquipPO)
            Dim lists As New List(Of v_EquipPO)
            Dim _vEquipPOData As ArrayList = Nothing
            Dim typePayment As String = "Deposit B"
            Dim newDate1 As Date = Date.Now().AddDays(-14)
            Dim newDate2 As Date = Date.Now().AddDays(-1)
            Dim sts As String = "'0','1'"
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(v_EquipPO), "CreatedTime", MatchType.GreaterOrEqual, Format(newDate1, "yyyy-MM-dd")))
            criterias.opAnd(New Criteria(GetType(v_EquipPO), "CreatedTime", MatchType.LesserOrEqual, Format(newDate2, "yyyy-MM-dd")))
            criterias.opAnd(New Criteria(GetType(v_EquipPO), "PaymentTypeDesc", MatchType.Exact, typePayment))
            criterias.opAnd(New Criteria(GetType(v_EquipPO), "StatusKTB", MatchType.InSet, "(" & sts & ")"))


            If Not IsNothing(oDealer) Then
                criterias.opAnd(New Criteria(GetType(v_EquipPO), "DealerCode", MatchType.Exact, oDealer))
                Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
                sortColl.Add(New Sort(GetType(v_EquipPO), "ID", Sort.SortDirection.DESC))

                _vEquipPOData = DoLoadArray(GetType(v_EquipPO).ToString, criterias, sortColl)
            Else
                _vEquipPOData = DoLoadArray(GetType(v_EquipPO).ToString, criterias)
            End If

            If Not IsNothing(_vEquipPOData) Then
                For Each item As v_EquipPO In _vEquipPOData
                    lists.Add(item)
                Next
            End If
            Return lists
        End Function

        Public Function RetrieveIndenPartDetail(ByVal EquipHID As Integer) As List(Of IndentPartDetail)
            Dim lists As New List(Of IndentPartDetail)
            Dim _dDetail As ArrayList = Nothing

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.ID", MatchType.Exact, EquipHID))

            _dDetail = DoLoadArray(GetType(IndentPartDetail).ToString, criterias)
            If Not IsNothing(_dDetail) Then
                For Each item As IndentPartDetail In _dDetail
                    lists.Add(item)
                Next
            End If

            Return lists
        End Function

        Public Function RetrieveDueDateNotification() As List(Of IndentPartHeader)
            Dim lists As New List(Of IndentPartHeader)
            Dim _dDetail As ArrayList = Nothing
            Dim sts As String = "'0','2'"

            Dim sqlQuery As String = " SELECT ID FROM v_EquipPO v WHERE 1 = 1 And DateAdd(Day, 14, v.CreatedTime) > GETDATE() and v.PaymentType =1 and v.Status IN (0,2) order by v.CreatedTime desc"

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "ID", MatchType.InSet, "(" & sqlQuery & ")"))

            _dDetail = DoLoadArray(GetType(IndentPartHeader).ToString, criterias)
            If Not IsNothing(_dDetail) Then
                For Each item As IndentPartHeader In _dDetail
                    lists.Add(item)
                Next
            End If
            Return lists
        End Function

        Public Function RetrieveDueDateNotificationByDealer(ByVal oDealer As Dealer) As List(Of IndentPartHeader)
            Dim lists As New List(Of IndentPartHeader)
            Dim _dDetail As ArrayList = Nothing
            Dim sts As String = "'0','2'"

            Dim sqlQuery As String = " SELECT ID FROM v_EquipPO v WHERE 1 = 1 And DateAdd(Day, 14, v.CreatedTime) > GETDATE() and v.PaymentType =1 and v.Status IN (0,2) order by v.CreatedTime desc"

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "ID", MatchType.InSet, "(" & sqlQuery & ")"))
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "DealerCode", MatchType.Exact, oDealer.DealerCode))

            _dDetail = DoLoadArray(GetType(IndentPartHeader).ToString, criterias)
            If Not IsNothing(_dDetail) Then
                For Each item As IndentPartHeader In _dDetail
                    lists.Add(item)
                Next
            End If

            Return lists
        End Function

#End Region

    End Class
End Namespace

