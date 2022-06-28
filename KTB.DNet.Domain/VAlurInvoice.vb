
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VAlurInvoice Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/3/2008 - 11:56:56 AM
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
    <Serializable(), TableInfo("VAlurInvoice")> _
    Public Class VAlurInvoice
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
        Private _pKNumber As String = String.Empty
        Private _contractNumber As String = String.Empty
        Private _pONumber As String = String.Empty
        Private _sONumber As String = String.Empty
        Private _invoiceNumber As String = String.Empty
        Private _pKQty As Integer
        Private _contractQty As Integer
        Private _pOQty As Integer
        Private _invQty As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("PKNumber", "'{0}'")> _
        Public Property PKNumber() As String
            Get
                Return _pKNumber
            End Get
            Set(ByVal value As String)
                _pKNumber = value
            End Set
        End Property


        <ColumnInfo("ContractNumber", "'{0}'")> _
        Public Property ContractNumber() As String
            Get
                Return _contractNumber
            End Get
            Set(ByVal value As String)
                _contractNumber = value
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


        <ColumnInfo("InvoiceNumber", "'{0}'")> _
        Public Property InvoiceNumber() As String
            Get
                Return _invoiceNumber
            End Get
            Set(ByVal value As String)
                _invoiceNumber = value
            End Set
        End Property


        <ColumnInfo("PKQty", "{0}")> _
        Public Property PKQty() As Integer
            Get
                Return _pKQty
            End Get
            Set(ByVal value As Integer)
                _pKQty = value
            End Set
        End Property


        <ColumnInfo("ContractQty", "{0}")> _
        Public Property ContractQty() As Integer
            Get
                Return _contractQty
            End Get
            Set(ByVal value As Integer)
                _contractQty = value
            End Set
        End Property


        <ColumnInfo("POQty", "{0}")> _
        Public Property POQty() As Integer
            Get
                Return _pOQty
            End Get
            Set(ByVal value As Integer)
                _pOQty = value
            End Set
        End Property


        <ColumnInfo("InvQty", "{0}")> _
        Public Property InvQty() As Integer
            Get
                Return _invQty
            End Get
            Set(ByVal value As Integer)
                _invQty = value
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

