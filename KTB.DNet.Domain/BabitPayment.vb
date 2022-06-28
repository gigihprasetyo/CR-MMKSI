#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitPayment Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 4/6/2006 - 10:08:59 AM
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
    <Serializable(), TableInfo("BabitPayment")> _
    Public Class BabitPayment
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
        Private _paymentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerInvoice As String = String.Empty
        Private _paymentStatus As Integer
        Private _nomorPembayaran As String = String.Empty
        Private _keterangan As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _babitProposal As BabitProposal
        Private _gLAccount As GLAccount
        Private _costCenter As CostCenter



#End Region

#Region "Public Properties"

        Public Const REJECTED_ALL_DESC As String = "Pameran/Iklan/Event Tidak Sesuai Dengan Standarisasi KTB"
        Public Const REJECTED_ITEM_DESC As String = "Ada Iklan Yang Tidak Sesuai Dengan Standarisasi KTB"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("PaymentDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PaymentDate() As DateTime
            Get
                Return _paymentDate
            End Get
            Set(ByVal value As DateTime)
                _paymentDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DealerInvoice", "'{0}'")> _
        Public Property DealerInvoice() As String
            Get
                Return _dealerInvoice
            End Get
            Set(ByVal value As String)
                _dealerInvoice = value
            End Set
        End Property


        <ColumnInfo("PaymentStatus", "{0}")> _
        Public Property PaymentStatus() As Integer
            Get
                Return _paymentStatus
            End Get
            Set(ByVal value As Integer)
                _paymentStatus = value
            End Set
        End Property


        <ColumnInfo("NomorPembayaran", "'{0}'")> _
        Public Property NomorPembayaran() As String
            Get
                Return _nomorPembayaran
            End Get
            Set(ByVal value As String)
                _nomorPembayaran = value
            End Set
        End Property


        <ColumnInfo("Keterangan", "'{0}'")> _
        Public Property Keterangan() As String
            Get
                Return _keterangan
            End Get
            Set(ByVal value As String)
                _keterangan = value
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
        RelationInfo("Dealer", "ID", "BabitPayment", "DealerID")> _
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

        <ColumnInfo("BabitProposalID", "{0}"), _
        RelationInfo("BabitProposal", "ID", "BabitPayment", "BabitProposalID")> _
        Public Property BabitProposal() As BabitProposal
            Get
                Try
                    If Not IsNothing(Me._babitProposal) AndAlso (Not Me._babitProposal.IsLoaded) Then

                        Me._babitProposal = CType(DoLoad(GetType(BabitProposal).ToString(), _babitProposal.ID), BabitProposal)
                        Me._babitProposal.MarkLoaded()

                    End If

                    Return Me._babitProposal

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitProposal)

                Me._babitProposal = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitProposal.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("GLAccountID", "{0}"), _
        RelationInfo("GLAccount", "ID", "BabitPayment", "GLAccountID")> _
        Public Property GLAccount() As GLAccount
            Get
                Try
                    If Not IsNothing(Me._gLAccount) AndAlso (Not Me._gLAccount.IsLoaded) Then

                        Me._gLAccount = CType(DoLoad(GetType(GLAccount).ToString(), _gLAccount.ID), GLAccount)
                        Me._gLAccount.MarkLoaded()

                    End If

                    Return Me._gLAccount

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As GLAccount)

                Me._gLAccount = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._gLAccount.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CostCenterID", "{0}"), _
        RelationInfo("CostCenter", "ID", "BabitPayment", "CostCenterID")> _
        Public Property CostCenter() As CostCenter
            Get
                Try
                    If Not IsNothing(Me._costCenter) AndAlso (Not Me._costCenter.IsLoaded) Then

                        Me._costCenter = CType(DoLoad(GetType(CostCenter).ToString(), _costCenter.ID), CostCenter)
                        Me._costCenter.MarkLoaded()

                    End If

                    Return Me._costCenter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CostCenter)

                Me._costCenter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._costCenter.MarkLoaded()
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

