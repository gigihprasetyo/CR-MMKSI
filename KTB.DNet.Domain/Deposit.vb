#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Deposit Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 15/11/2005 - 14:17:41
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
    <Serializable(), TableInfo("Deposit")> _
    Public Class Deposit
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
        Private _period As String = String.Empty
        Private _begBalance As Decimal
        Private _endBalance As Decimal
        Private _totalDebit As Decimal
        Private _totalCredit As Decimal
        Private _availableDeposit As Decimal
        Private _giroReceive As Decimal
        Private _rO As Decimal
        Private _service As Decimal
        Private _inClearing As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

        Private _depositLines As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Period", "'{0}'")> _
        Public Property Period() As String
            Get
                Return _period
            End Get
            Set(ByVal value As String)
                _period = value
            End Set
        End Property


        <ColumnInfo("BegBalance", "{0}")> _
        Public Property BegBalance() As Decimal
            Get
                Return _begBalance
            End Get
            Set(ByVal value As Decimal)
                _begBalance = value
            End Set
        End Property


        <ColumnInfo("EndBalance", "{0}")> _
        Public Property EndBalance() As Decimal
            Get
                Return _endBalance
            End Get
            Set(ByVal value As Decimal)
                _endBalance = value
            End Set
        End Property


        <ColumnInfo("TotalDebit", "{0}")> _
        Public Property TotalDebit() As Decimal
            Get
                Return _totalDebit
            End Get
            Set(ByVal value As Decimal)
                _totalDebit = value
            End Set
        End Property


        <ColumnInfo("TotalCredit", "{0}")> _
        Public Property TotalCredit() As Decimal
            Get
                Return _totalCredit
            End Get
            Set(ByVal value As Decimal)
                _totalCredit = value
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


        <ColumnInfo("InClearing", "{0}")> _
        Public Property InClearing() As Decimal
            Get
                Return _inClearing
            End Get
            Set(ByVal value As Decimal)
                _inClearing = value
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




        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "Deposit", "DealerID")> _
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


        <RelationInfo("Deposit", "ID", "DepositLine", "DepositID")> _
        Public ReadOnly Property DepositLines() As System.Collections.ArrayList
            Get
                Try
                    If (Me._depositLines.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DepositLine), "Deposit", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DepositLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._depositLines = DoLoadArray(GetType(DepositLine).ToString, criterias)
                    End If

                    Return Me._depositLines

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

#Region "Custom Method"

#End Region

    End Class
End Namespace
