#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PendingOrder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 12/19/2007 - 9:23:58 AM
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
    <Serializable(), TableInfo("PendingOrder")> _
    Public Class PendingOrder
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
        Private _availableDeposit As Decimal
        Private _giroReceive As Decimal
        Private _rO As Decimal
        Private _service As Decimal
        Private _retail As Decimal
        Private _pPN As Decimal
        Private _depositC2 As Decimal
        Private _totalAmount As Decimal
        Private _issueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sONumber As String = String.Empty
        Private _billingNumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartPO As SparePartPO
        Private _dealer As Dealer



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("AvailableDeposit", "{0}")> _
        Public Property AvailableDeposit() As Decimal
            Get
                Return _availableDeposit
            End Get
            Set(ByVal value As Decimal)
                _availableDeposit = value
            End Set
        End Property

        <ColumnInfo("GiroReceive", "{0}")> _
        Public Property GiroReceive() As Decimal
            Get
                Return _giroReceive
            End Get
            Set(ByVal value As Decimal)
                _giroReceive = value
            End Set
        End Property

        <ColumnInfo("RO", "{0}")> _
  Public Property RO() As Decimal
            Get
                Return _rO
            End Get
            Set(ByVal value As Decimal)
                _rO = value
            End Set
        End Property


        <ColumnInfo("Service", "{0}")> _
        Public Property Service() As Decimal
            Get
                Return _service
            End Get
            Set(ByVal value As Decimal)
                _service = value
            End Set
        End Property

        <ColumnInfo("Retail", "{0}")> _
        Public Property Retail() As Decimal
            Get
                Return _retail
            End Get
            Set(ByVal value As Decimal)
                _retail = value
            End Set
        End Property


        <ColumnInfo("PPN", "{0}")> _
        Public Property PPN() As Decimal
            Get
                Return _pPN
            End Get
            Set(ByVal value As Decimal)
                _pPN = value
            End Set
        End Property


        <ColumnInfo("DepositC2", "{0}")> _
        Public Property DepositC2() As Decimal
            Get
                Return _depositC2
            End Get
            Set(ByVal value As Decimal)
                _depositC2 = value
            End Set
        End Property


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount() As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
            End Set
        End Property


        <ColumnInfo("IssueDate", "'{0:yyyy/MM/dd}'")> _
        Public Property IssueDate() As DateTime
            Get
                Return _issueDate
            End Get
            Set(ByVal value As DateTime)
                _issueDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property


        <ColumnInfo("BillingNumber", "'{0}'")> _
        Public Property BillingNumber() As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("SparepartPOID", "{0}"), _
        RelationInfo("SparePartPO", "ID", "PendingOrder", "SparepartPOID")> _
        Public Property SparePartPO() As SparePartPO
            Get
                Try
                    If Not IsNothing(Me._sparePartPO) AndAlso (Not Me._sparePartPO.IsLoaded) Then

                        Me._sparePartPO = CType(DoLoad(GetType(SparePartPO).ToString(), _sparePartPO.ID), SparePartPO)
                        Me._sparePartPO.MarkLoaded()

                    End If

                    Return Me._sparePartPO

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPO)

                Me._sparePartPO = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPO.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "PendingOrder", "DealerID")> _
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

