
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_SparePartConversion Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/03/2018 - 9:03:38
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
    <Serializable(), TableInfo("VWI_SparePartConversion")> _
    Public Class VWI_SparePartConversion
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
        Private _partNumber As String = String.Empty
        Private _partName As String = String.Empty
        Private _typeCode As String = String.Empty
        Private _modelCode As String = String.Empty
        Private _uOMFrom As String = String.Empty
        Private _uOMTo As String = String.Empty
        Private _qty As Integer
        Private _partNumberReff As String = String.Empty
        Private _status As Short
        Private _productType As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"
        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("PartNumber", "'{0}'")> _
        Public Property PartNumber As String
            Get
                Return _partNumber
            End Get
            Set(ByVal value As String)
                _partNumber = value
            End Set
        End Property


        <ColumnInfo("PartName", "'{0}'")> _
        Public Property PartName As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
            End Set
        End Property

        <ColumnInfo("TypeCode", "'{0}'")> _
        Public Property TypeCode As String
            Get
                Return _typeCode
            End Get
            Set(ByVal value As String)
                _typeCode = value
            End Set
        End Property

        <ColumnInfo("ModelCode", "'{0}'")> _
        Public Property ModelCode As String
            Get
                Return _modelCode
            End Get
            Set(ByVal value As String)
                _modelCode = value
            End Set
        End Property

        <ColumnInfo("UOMFrom", "'{0}'")> _
        Public Property UOMFrom As String
            Get
                Return _uOMFrom
            End Get
            Set(ByVal value As String)
                _uOMFrom = value
            End Set
        End Property


        <ColumnInfo("UOMTo", "'{0}'")> _
        Public Property UOMTo As String
            Get
                Return _uOMTo
            End Get
            Set(ByVal value As String)
                _uOMTo = value
            End Set
        End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property


        <ColumnInfo("PartNumberReff", "'{0}'")> _
        Public Property PartNumberReff As String
            Get
                Return _partNumberReff
            End Get
            Set(ByVal value As String)
                _partNumberReff = value
            End Set
        End Property

        <ColumnInfo("ProductTYpe", "'{0}'")> _
        Public Property ProductType As String
            Get
                Return _productType
            End Get
            Set(ByVal value As String)
                _productType = value
            End Set
        End Property

        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
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

