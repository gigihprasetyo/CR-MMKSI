
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VW_ServiceTemplateDetailPart Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 11/15/2016 - 9:11:51 AM
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
    <Serializable(), TableInfo("VW_ServiceTemplateDetailPart")> _
    Public Class VW_ServiceTemplateDetailPart
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
        Private _templateType As String = String.Empty
        Private _partNumber As String = String.Empty
        Private _partName As String = String.Empty
        Private _serviceTemplateHeaderID As Integer
        Private _sparepartMasterID As Integer
        Private _partAmount As Decimal
        Private _partQuantity As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"

        <ColumnInfo("TemplateType", "{0}")> _
        Public Property TemplateType As String
            Get
                Return _templateType
            End Get
            Set(ByVal value As String)
                _templateType = value
            End Set
        End Property

        <ColumnInfo("PartNumber", "{0}")> _
        Public Property PartNumber As String
            Get
                Return _partNumber
            End Get
            Set(ByVal value As String)
                _partNumber = value
            End Set
        End Property

        <ColumnInfo("PartAmount", "{0}")> _
        Public Property PartAmount As Decimal
            Get
                Return _partAmount
            End Get
            Set(ByVal value As Decimal)
                _partAmount = value
            End Set
        End Property

        <ColumnInfo("PartName", "{0}")> _
        Public Property PartName As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
            End Set
        End Property

        <ColumnInfo("PartQuantity", "{0}")> _
        Public Property PartQuantity As Decimal
            Get
                Return _partQuantity
            End Get
            Set(ByVal value As Decimal)
                _partQuantity = value
            End Set
        End Property

        <ColumnInfo("ServiceTemplateHeaderID", "{0}")> _
        Public Property ServiceTemplateHeaderID As Integer
            Get
                Return _serviceTemplateHeaderID
            End Get
            Set(ByVal value As Integer)
                _serviceTemplateHeaderID = value
            End Set
        End Property

        <ColumnInfo("SparepartMasterID", "{0}")> _
        Public Property SparepartMasterID As Decimal
            Get
                Return _sparepartMasterID
            End Get
            Set(ByVal value As Decimal)
                _sparepartMasterID = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
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

