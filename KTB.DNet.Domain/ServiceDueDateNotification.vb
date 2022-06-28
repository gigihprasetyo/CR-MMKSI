#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceDueDateNotification Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/13/2021 - 10:24:35 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("ServiceDueDateNotification")> _
    Public Class ServiceDueDateNotification
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
        Private _nameRecipient As String = String.Empty
        Private _emailDealer As String = String.Empty
        Private _positionRecipient As String = String.Empty
        Private _emailNotificationKind As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _dealerID As Short
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


        '    <ColumnInfo("DealerID", "{0}")> _
        '    Public Property DealerID As Short

        '        Get
        'return _dealerID}
        '        End Get
        '        Set(ByVal value As Short)
        '            _dealerID = value
        '        End Set
        '    End Property



        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ServiceDueDateNotification", "DealerID")> _
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
        Public ReadOnly Property RetrieveUnCompletePayment() As List(Of MSPExPayment)
            Get
                Dim lists As New List(Of MSPExPayment)
                Dim _aMSPExPayment As ArrayList = Nothing
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(MSPExPayment), "Status", MatchType.No, CType(EnumMSPEx.MSPExStatusPayment.Selesai, Integer)))
                'criterias.opAnd(New Criteria(GetType(MSPExPayment), "PlanTransferDate", MatchType.GreaterOrEqual, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 0, 0, 0)))
                'criterias.opAnd(New Criteria(GetType(MSPExPayment), "PlanTransferDate", MatchType.LesserOrEqual, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day + 2, 0, 0, 0)))
                Dim oDate As Date = DateAdd(DateInterval.Day, 2, Date.Now)
                criterias.opAnd(New Criteria(GetType(MSPExPayment), "PlanTransferDate", MatchType.LesserOrEqual, New Date(oDate.Year, oDate.Month, oDate.Day, 0, 0, 0)))

                _aMSPExPayment = DoLoadArray(GetType(MSPExPayment).ToString, criterias)
                If Not IsNothing(_aMSPExPayment) Then
                    For Each item As MSPExPayment In _aMSPExPayment
                        lists.Add(item)
                    Next
                End If
                Return lists
            End Get
        End Property

        Public Function RetrieveUnCompletePaymentByDealer(ByVal oDealer As Dealer) As List(Of MSPExPayment)
            Dim lists As New List(Of MSPExPayment)
            Dim _aMSPExPaymentDetail As ArrayList = Nothing
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPExPayment), "Status", MatchType.No, CType(EnumMSPEx.MSPExStatusPayment.Selesai, Integer)))
            Dim oDate As Date = DateAdd(DateInterval.Day, 2, Date.Now)
            criterias.opAnd(New Criteria(GetType(MSPExPayment), "PlanTransferDate", MatchType.LesserOrEqual, New Date(oDate.Year, oDate.Month, oDate.Day, 0, 0, 0)))
            criterias.opAnd(New Criteria(GetType(MSPExPayment), "Dealer.ID", MatchType.Exact, oDealer.ID))

            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            sortColl.Add(New Sort(GetType(MSPExPayment), "ID", Sort.SortDirection.DESC))

            _aMSPExPaymentDetail = DoLoadArray(GetType(MSPExPayment).ToString, criterias, sortColl)
            If Not IsNothing(_aMSPExPaymentDetail) Then
                For Each item As MSPExPayment In _aMSPExPaymentDetail
                    lists.Add(item)
                Next
            End If
            Return lists
        End Function

        Public Function RetrieveOutStandingServiceDocument(Optional ByVal oDealer As Dealer = Nothing) As List(Of MonthlyDocument)
            Dim lists As New List(Of MonthlyDocument)
            Dim _aMonthlyDocument As ArrayList = Nothing
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "ProductCategory.ID", MatchType.Exact, "1"))
            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "Kind", MatchType.InSet, "(1,6,7,2,4,5,23,24)"))
            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "(", True)
            criterias.opOr(New Criteria(GetType(MonthlyDocument), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), ")", False)
            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "AccountingNo", MatchType.No, "''"))
            'Dim oDateStart As Date = DateAdd(DateInterval.Day, -15, Date.Now)
            'criterias.opAnd(New Criteria(GetType(MonthlyDocument), "CreatedTime", MatchType.GreaterOrEqual, New Date(oDateStart.Year, oDateStart.Month, oDateStart.Day, 0, 0, 0)))
            'criterias.opAnd(New Criteria(GetType(MonthlyDocument), "CreatedTime", MatchType.LesserOrEqual, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 0, 0, 0)))

            If Not IsNothing(oDealer) Then
                criterias.opAnd(New Criteria(GetType(MonthlyDocument), "Dealer.ID", MatchType.Exact, oDealer.ID))
                Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
                sortColl.Add(New Sort(GetType(MonthlyDocument), "id", Sort.SortDirection.DESC))

                _aMonthlyDocument = DoLoadArray(GetType(MonthlyDocument).ToString, criterias, sortColl)
            Else
                _aMonthlyDocument = DoLoadArray(GetType(MonthlyDocument).ToString, criterias)
            End If

            If Not IsNothing(_aMonthlyDocument) Then
                For Each item As MonthlyDocument In _aMonthlyDocument
                    lists.Add(item)
                Next
            End If
            Return lists
        End Function


        Public ReadOnly Property RetrieveUnCompleteRegularPayment() As List(Of MSPTransferPayment)
            Get
                Dim lists As New List(Of MSPTransferPayment)
                Dim _aMSPTransferPayment As ArrayList = Nothing
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(MSPTransferPayment), "Status", MatchType.No, CType(EnumStatusMSP.Status.Selesai, Short)))
                Dim oDate As Date = DateAdd(DateInterval.Day, 2, Date.Now)
                criterias.opAnd(New Criteria(GetType(MSPTransferPayment), "PlanTransferDate", MatchType.LesserOrEqual, New Date(oDate.Year, oDate.Month, oDate.Day, 0, 0, 0)))

                _aMSPTransferPayment = DoLoadArray(GetType(MSPTransferPayment).ToString, criterias)
                If Not IsNothing(_aMSPTransferPayment) Then
                    For Each item As MSPTransferPayment In _aMSPTransferPayment
                        lists.Add(item)
                    Next
                End If
                Return lists
            End Get
        End Property

        Public Function RetrieveUnCompleteRegularPaymentByDealer(ByVal oDealer As Dealer) As List(Of MSPTransferPayment)
            Dim lists As New List(Of MSPTransferPayment)
            Dim _aMSPTransferPaymentDetail As ArrayList = Nothing
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MSPTransferPayment), "Status", MatchType.No, CType(EnumStatusMSP.Status.Selesai, Integer)))
            Dim oDate As Date = DateAdd(DateInterval.Day, 2, Date.Now)
            criterias.opAnd(New Criteria(GetType(MSPTransferPayment), "PlanTransferDate", MatchType.LesserOrEqual, New Date(oDate.Year, oDate.Month, oDate.Day, 0, 0, 0)))
            criterias.opAnd(New Criteria(GetType(MSPTransferPayment), "Dealer.ID", MatchType.Exact, oDealer.ID))

            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            sortColl.Add(New Sort(GetType(MSPTransferPayment), "ID", Sort.SortDirection.DESC))

            _aMSPTransferPaymentDetail = DoLoadArray(GetType(MSPTransferPayment).ToString, criterias, sortColl)
            If Not IsNothing(_aMSPTransferPaymentDetail) Then
                For Each item As MSPTransferPayment In _aMSPTransferPaymentDetail
                    lists.Add(item)
                Next
            End If
            Return lists
        End Function
#End Region

    End Class
End Namespace
