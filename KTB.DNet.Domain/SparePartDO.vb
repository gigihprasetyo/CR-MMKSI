
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDO Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2016 - 11:35:17 AM
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
    <Serializable(), TableInfo("SparePartDO")> _
    Public Class SparePartDO
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
        Private _dONumber As String = String.Empty
        Private _doDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _estmationDeliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pickingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _packingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _goodIssueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _paymentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _readyForDeliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

        'Private _sparePartBillings As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _sparePartDODetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property


        <ColumnInfo("DoDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DoDate As DateTime
            Get
                Return _doDate
            End Get
            Set(ByVal value As DateTime)
                _doDate = New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second)
            End Set
        End Property


        <ColumnInfo("EstmationDeliveryDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property EstmationDeliveryDate As DateTime
            Get
                Return _estmationDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                _estmationDeliveryDate = New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second)
            End Set
        End Property


        <ColumnInfo("PickingDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PickingDate As DateTime
            Get
                Return _pickingDate
            End Get
            Set(ByVal value As DateTime)
                _pickingDate = New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second)
            End Set
        End Property


        <ColumnInfo("PackingDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PackingDate As DateTime
            Get
                Return _packingDate
            End Get
            Set(ByVal value As DateTime)
                _packingDate = New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second)
            End Set
        End Property


        <ColumnInfo("GoodIssueDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property GoodIssueDate As DateTime
            Get
                Return _goodIssueDate
            End Get
            Set(ByVal value As DateTime)
                _goodIssueDate = New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second)
            End Set
        End Property


        <ColumnInfo("PaymentDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PaymentDate As DateTime
            Get
                Return _paymentDate
            End Get
            Set(ByVal value As DateTime)
                _paymentDate = New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second)
            End Set
        End Property


        <ColumnInfo("ReadyForDeliveryDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ReadyForDeliveryDate As DateTime
            Get
                Return _readyForDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                _readyForDeliveryDate = New DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second)
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
        RelationInfo("Dealer", "ID", "SparePartDO", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        '<RelationInfo("SparePartDO", "ID", "SparePartBilling", "SparePartDOID")> _
        'Public ReadOnly Property SparePartBillings As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._sparePartBillings.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(SparePartBilling), "SparePartDO", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(SparePartBilling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._sparePartBillings = DoLoadArray(GetType(SparePartBilling).ToString, criterias)
        '            End If

        '            Return Me._sparePartBillings

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
        'End Property

        <RelationInfo("SparePartDO", "ID", "SparePartDODetail", "SparePartDOID")> _
        Public ReadOnly Property SparePartDODetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartDODetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartDODetail), "SparePartDO", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartDODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartDODetails = DoLoadArray(GetType(SparePartDODetail).ToString, criterias)
                    End If

                    Return Me._sparePartDODetails

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

