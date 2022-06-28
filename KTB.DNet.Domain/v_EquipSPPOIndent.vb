
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SpPO_Indent Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/22/2008 - 11:03:51 AM
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
    <Serializable(), TableInfo("v_EquipSPPOIndent")> _
    Public Class v_EquipSPPOIndent
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
        Private _requestNo As String = String.Empty
        Private _pONumber As String = String.Empty
        Private _sONumber As String = String.Empty
        Private _orderType As String = String.Empty
        Private _dealerID As Integer
        Private _pODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _indentTransfer As Short
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _indentPartHeaderID As Integer
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

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


        <ColumnInfo("RequestNo", "'{0}'")> _
        Public Property RequestNo() As String
            Get
                Return _requestNo
            End Get
            Set(ByVal value As String)
                _requestNo = value
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

        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
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


        <ColumnInfo("PODate", "'{0:yyyy/MM/dd}'")> _
        Public Property PODate() As DateTime
            Get
                Return _pODate
            End Get
            Set(ByVal value As DateTime)
                _pODate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("IndentPartHeaderID", "{0}")> _
        Public Property IndentPartHeaderID() As Integer
            Get
                Return _indentPartHeaderID
            End Get
            Set(ByVal value As Integer)
                _indentPartHeaderID = value
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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
        Public ReadOnly Property IndentTransferDesc() As String
            Get
                If Me.IndentTransfer = 1 Then
                    Return "Sudah Transfer"
                Else
                    Return "Belum Transfer"
                End If
            End Get
        End Property

#End Region

    End Class
End Namespace

