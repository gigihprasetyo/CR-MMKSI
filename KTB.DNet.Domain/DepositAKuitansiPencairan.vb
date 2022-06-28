
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositAKuitansiPencairan Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 1/7/2009 - 2:35:54 PM
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
    <Serializable(), TableInfo("DepositAKuitansiPencairan")> _
    Public Class DepositAKuitansiPencairan
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
        Private _type As Byte
        Private _noSurat As String = String.Empty
        Private _dNNumber As String = String.Empty
        Private _assignmentNumber As String = String.Empty
        Private _requestedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _description As String = String.Empty
        Private _status As Byte
        'Private _fakturPajakNo As String = String.Empty
        'Private _fakturPajakDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _receiptNumber As String = String.Empty
        Private _receiptDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _totalAmount As Decimal
        Private _signedBy As String = String.Empty
        Private _jabatan As String = String.Empty
        Private _tglPencairan As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isTransfer As Byte
        Private _noJV As String
        Private _noReg As String
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _productCategory As ProductCategory


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


        <ColumnInfo("Type", "{0}")> _
        Public Property Type() As Byte
            Get
                Return _type
            End Get
            Set(ByVal value As Byte)
                _type = value
            End Set
        End Property


        <ColumnInfo("NoSurat", "'{0}'")> _
        Public Property NoSurat() As String
            Get
                Return _noSurat
            End Get
            Set(ByVal value As String)
                _noSurat = value
            End Set
        End Property


        <ColumnInfo("DNNumber", "'{0}'")> _
        Public Property DNNumber() As String
            Get
                Return _dNNumber
            End Get
            Set(ByVal value As String)
                _dNNumber = value
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


        <ColumnInfo("RequestedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property RequestedTime() As DateTime
            Get
                Return _requestedTime
            End Get
            Set(ByVal value As DateTime)
                _requestedTime = value
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

        '<ColumnInfo("FakturPajakNo", "'{0}'")> _
        'Public Property FakturPajakNo() As String
        '    Get
        '        Return _fakturPajakNo
        '    End Get
        '    Set(ByVal value As String)
        '        _fakturPajakNo = value
        '    End Set
        'End Property


        '<ColumnInfo("FakturPajakDate", "'{0:yyyy/MM/dd}'")> _
        'Public Property FakturPajakDate() As DateTime
        '    Get
        '        Return _fakturPajakDate
        '    End Get
        '    Set(ByVal value As DateTime)
        '        _fakturPajakDate = New DateTime(value.Year, value.Month, value.Day)
        '    End Set
        'End Property

        <ColumnInfo("ReceiptNumber", "'{0}'")> _
        Public Property ReceiptNumber() As String
            Get
                Return _receiptNumber
            End Get
            Set(ByVal value As String)
                _receiptNumber = value
            End Set
        End Property


        <ColumnInfo("ReceiptDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReceiptDate() As DateTime
            Get
                Return _receiptDate
            End Get
            Set(ByVal value As DateTime)
                _receiptDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("SignedBy", "'{0}'")> _
        Public Property SignedBy() As String
            Get
                Return _signedBy
            End Get
            Set(ByVal value As String)
                _signedBy = value
            End Set
        End Property

        <ColumnInfo("Jabatan", "'{0}'")> _
        Public Property Jabatan() As String
            Get
                Return _jabatan
            End Get
            Set(ByVal value As String)
                _jabatan = value
            End Set
        End Property

        <ColumnInfo("TglPencairan", "'{0:yyyy/MM/dd}'")> _
        Public Property TglPencairan() As DateTime
            Get
                Return _tglPencairan
            End Get
            Set(ByVal value As DateTime)
                _tglPencairan = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("IsTransfer", "{0}")> _
      Public Property IsTransfer() As Byte
            Get
                Return _isTransfer
            End Get
            Set(ByVal value As Byte)
                _isTransfer = value
            End Set
        End Property

        <ColumnInfo("NoJV", "'{0}'")> _
       Public Property NoJV() As String
            Get
                Return _noJV
            End Get
            Set(ByVal value As String)
                _noJV = value
            End Set
        End Property

        <ColumnInfo("NoReg", "'{0}'")> _
       Public Property NoReg() As String
            Get
                Return _noReg
            End Get
            Set(ByVal value As String)
                _noReg = value
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
        RelationInfo("Dealer", "ID", "DepositAKuitansiPencairan", "DealerID")> _
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

        <ColumnInfo("ProductCategoryID", "{0}"), _
     RelationInfo("ProductCategory", "ID", "DepositAKuitansiPencairan", "ProductCategoryID")> _
        Public Property ProductCategory() As ProductCategory
            Get
                Try
                    If Not IsNothing(Me._productCategory) AndAlso (Not Me._productCategory.IsLoaded) Then

                        Me._productCategory = CType(DoLoad(GetType(ProductCategory).ToString(), _productCategory.ID), ProductCategory)
                        Me._productCategory.MarkLoaded()

                    End If

                    Return Me._productCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProductCategory)

                Me._productCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._productCategory.MarkLoaded()
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

