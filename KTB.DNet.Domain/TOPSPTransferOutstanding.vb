#Region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : TOPSPTransferOutstanding Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2021 - 10:41:22 AM
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
    <Serializable(), TableInfo("TOPSPTransferOutstanding")> _
    Public Class TOPSPTransferOutstanding
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
        Private _refBank As String = String.Empty
        Private _transferAmount As Decimal
        Private _transferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _tRNo As String = String.Empty
        Private _iDTransaction As Short
        Private _narrative As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _dealerID As Short
        Private _dealer As Dealer

        'Private _bankID As Integer
        Private _bank As Bank


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


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("RefBank", "'{0}'")> _
        Public Property RefBank As String
            Get
                Return _refBank
            End Get
            Set(ByVal value As String)
                _refBank = value
            End Set
        End Property


        '<ColumnInfo("BankID", "{0}")> _
        'Public Property BankID As Integer
        '    Get
        '        Return _bankID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _bankID = value
        '    End Set
        'End Property


        <ColumnInfo("TransferAmount", "{0}")> _
        Public Property TransferAmount As Decimal
            Get
                Return _transferAmount
            End Get
            Set(ByVal value As Decimal)
                _transferAmount = value
            End Set
        End Property


        <ColumnInfo("TransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferDate As DateTime
            Get
                Return _transferDate
            End Get
            Set(ByVal value As DateTime)
                _transferDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TRNo", "'{0}'")> _
        Public Property TRNo As String
            Get
                Return _tRNo
            End Get
            Set(ByVal value As String)
                _tRNo = value
            End Set
        End Property


        <ColumnInfo("IDTransaction", "{0}")> _
        Public Property IDTransaction As Short
            Get
                Return _iDTransaction
            End Get
            Set(ByVal value As Short)
                _iDTransaction = value
            End Set
        End Property


        <ColumnInfo("Narrative", "'{0}'")> _
        Public Property Narrative As String
            Get
                Return _narrative
            End Get
            Set(ByVal value As String)
                _narrative = value
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "TOPSPTransferOutstanding", "DealerID")> _
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

        <ColumnInfo("BankID", "{0}"), _
        RelationInfo("Bank", "ID", "TOPSPTransferOutstanding", "BankID")> _
        Public Property Bank As Bank
            Get
                Try
                    If Not IsNothing(Me._bank) AndAlso (Not Me._bank.IsLoaded) Then

                        Me._bank = CType(DoLoad(GetType(Bank).ToString(), _bank.ID), Bank)
                        Me._bank.MarkLoaded()

                    End If

                    Return Me._bank

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Bank)

                Me._bank = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._bank.MarkLoaded()
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
