
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VSumDetailInvoice Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/4/2008 - 5:08:12 PM
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
    <Serializable(), TableInfo("VSumDetailInvoice")> _
    Public Class VSumDetailInvoice
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
        Private _materialNumber As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _billedQty As Long
        Private _itemAmount As Decimal
        Private _pPH22 As Decimal
        Private _interest As Decimal




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


        <ColumnInfo("MaterialNumber", "'{0}'")> _
        Public Property MaterialNumber() As String
            Get
                Return _materialNumber
            End Get
            Set(ByVal value As String)
                _materialNumber = value
            End Set
        End Property


        <ColumnInfo("MaterialDescription", "'{0}'")> _
        Public Property MaterialDescription() As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property


        <ColumnInfo("BilledQty", "{0}")> _
        Public Property BilledQty() As Long
            Get
                Return _billedQty
            End Get
            Set(ByVal value As Long)
                _billedQty = value
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


        <ColumnInfo("PPH22", "{0}")> _
        Public Property PPH22() As Decimal
            Get
                Return _pPH22
            End Get
            Set(ByVal value As Decimal)
                _pPH22 = value
            End Set
        End Property


        <ColumnInfo("Interest", "{0}")> _
        Public Property Interest() As Decimal
            Get
                Return _interest
            End Get
            Set(ByVal value As Decimal)
                _interest = value
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

