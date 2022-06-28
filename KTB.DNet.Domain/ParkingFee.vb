
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ParkingFee Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 2/27/2012 - 9:16:24 AM
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
    <Serializable(), TableInfo("ParkingFee")> _
    Public Class ParkingFee
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
        Private _periode As Short
        Private _year As Short
        Private _letterNumber As String = String.Empty
        Private _debitChargeNumber As String = String.Empty
        Private _debitMemoNumber As String = String.Empty
        Private _fileNameDebitMemo As String = String.Empty
        Private _fileNameParkingFee As String = String.Empty
        Private _assignmentNumber As String = String.Empty
        Private _fakturPajakNo As String = String.Empty
        Private _amount As Decimal
        Private _description As String = String.Empty
        Private _status As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _category As Category
        Private _dealerDepositA As Dealer


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


        <ColumnInfo("Periode", "{0}")> _
        Public Property Periode() As Short
            Get
                Return _periode
            End Get
            Set(ByVal value As Short)
                _periode = value
            End Set
        End Property


        <ColumnInfo("Year", "{0}")> _
        Public Property Year() As Short
            Get
                Return _year
            End Get
            Set(ByVal value As Short)
                _year = value
            End Set
        End Property

        <ColumnInfo("LetterNumber", "'{0}'")> _
        Public Property LetterNumber() As String
            Get
                Return _letterNumber
            End Get
            Set(ByVal value As String)
                _letterNumber = value
            End Set
        End Property

        <ColumnInfo("DebitChargeNumber", "'{0}'")> _
        Public Property DebitChargeNumber() As String
            Get
                Return _debitChargeNumber
            End Get
            Set(ByVal value As String)
                _debitChargeNumber = value
            End Set
        End Property


        <ColumnInfo("DebitMemoNumber", "'{0}'")> _
        Public Property DebitMemoNumber() As String
            Get
                Return _debitMemoNumber
            End Get
            Set(ByVal value As String)
                _debitMemoNumber = value
            End Set
        End Property


        <ColumnInfo("FileNameDebitMemo", "'{0}'")> _
        Public Property FileNameDebitMemo() As String
            Get
                Return _fileNameDebitMemo
            End Get
            Set(ByVal value As String)
                _fileNameDebitMemo = value
            End Set
        End Property


        <ColumnInfo("FileNameParkingFee", "'{0}'")> _
        Public Property FileNameParkingFee() As String
            Get
                Return _fileNameParkingFee
            End Get
            Set(ByVal value As String)
                _fileNameParkingFee = value
            End Set
        End Property


        <ColumnInfo("AssignmentNumber", "'{0}'")> _
        Public Property AssignmentNumber() As String
            Get
                Return _assignmentNumber
            End Get
            Set(ByVal value As String)
                _assignmentNumber = value
            End Set
        End Property

        <ColumnInfo("FakturPajakNo", "'{0}'")> _
        Public Property FakturPajakNo() As String
            Get
                Return _fakturPajakNo
            End Get
            Set(ByVal value As String)
                _fakturPajakNo = value
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
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
        RelationInfo("Dealer", "ID", "ParkingFee", "DealerID")> _
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

        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "ParkingFee", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not IsNothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerDepositA", "{0}"), _
        RelationInfo("Dealer", "ID", "ParkingFee", "DealerDepositA")> _
        Public Property DealerDepositA() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealerDepositA) AndAlso (Not Me._dealerDepositA.IsLoaded) Then

                        Me._dealerDepositA = CType(DoLoad(GetType(Dealer).ToString(), _dealerDepositA.ID), Dealer)
                        Me._dealerDepositA.MarkLoaded()

                    End If

                    Return Me._dealerDepositA

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealerDepositA = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerDepositA.MarkLoaded()
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

