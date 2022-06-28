
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SparePartPOSummary Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 1/14/2009 - 12:59:44 PM
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
    <Serializable(), TableInfo("V_SparePartPOSummary")> _
    Public Class V_SparePartPOSummary
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
        Private _pONumber As String = String.Empty
        Private _orderType As String = String.Empty
        Private _dealerID As Integer
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _pODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _processCode As String = String.Empty
        Private _cancelRequestBy As String = String.Empty
        Private _indentTransfer As Short
        Private _itemCount As Integer
        Private _itemAmount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sentPoDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


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


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property


        <ColumnInfo("OrderType", "'{0}'")> _
        Public Property OrderType() As String
            Get
                Return _orderType
            End Get
            Set(ByVal value As String)
                _orderType = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Integer
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Integer)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("PODate", "'{0:yyyy/MM/dd}'")> _
        Public Property PODate() As DateTime
            Get
                Return _pODate
            End Get
            Set(ByVal value As DateTime)
                _pODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ProcessCode", "'{0}'")> _
        Public Property ProcessCode() As String
            Get
                Return _processCode
            End Get
            Set(ByVal value As String)
                _processCode = value
            End Set
        End Property


        <ColumnInfo("CancelRequestBy", "'{0}'")> _
        Public Property CancelRequestBy() As String
            Get
                Return _cancelRequestBy
            End Get
            Set(ByVal value As String)
                _cancelRequestBy = value
            End Set
        End Property


        <ColumnInfo("IndentTransfer", "{0}")> _
        Public Property IndentTransfer() As Short
            Get
                Return _indentTransfer
            End Get
            Set(ByVal value As Short)
                _indentTransfer = value
            End Set
        End Property


        <ColumnInfo("ItemCount", "{0}")> _
        Public Property ItemCount() As Integer
            Get
                Return _itemCount
            End Get
            Set(ByVal value As Integer)
                _itemCount = value
            End Set
        End Property


        <ColumnInfo("ItemAmount", "{0}")> _
        Public Property ItemAmount() As Decimal
            Get
                Return _itemAmount
            End Get
            Set(ByVal value As Decimal)
                _itemAmount = value
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

        <ColumnInfo("SentPODate", "'{0:yyyy/MM/dd}'")> _
        Public Property SentPODate() As DateTime
            Get
                Return _sentPoDate
            End Get
            Set(ByVal value As DateTime)
                _sentPoDate = New DateTime(value.Year, value.Month, value.Day)
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

#Region "Custom Property"
        Public ReadOnly Property OrderTypeDesc() As String
            Get
                If _orderType = "E" Then
                    Return "Emergency"
                ElseIf _orderType = "R" Then
                    Return "Regular"

                ElseIf _orderType.ToUpper = "K" Then
                    Return "P.Khusus"
                ElseIf _orderType.ToUpper = "I" Then
                    Return "Indent"
                Else

                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property ProcessCodeDesc() As String
            Get
                If _processCode = "" Then
                    Return "Baru"
                ElseIf _processCode = "S" Then
                    Return "Telah dikirim"
                ElseIf _processCode = "P" Then
                    Return "Telah diproses"
                ElseIf _processCode = "C" Then
                    Return "Batal"
                ElseIf _processCode = "X" Then
                    Return "Batal MMKSI"
                ElseIf _processCode = "T" Then
                    Return "Tidak Dipenuhi"
                Else
                    Return String.Empty
                End If
            End Get
        End Property
#End Region

    End Class
End Namespace

