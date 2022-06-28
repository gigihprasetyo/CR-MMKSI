
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_ServiceTemplatex Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 26/07/2018 - 11:58:28
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
    <Serializable(), TableInfo("VWI_ServiceTemplate")> _
    Public Class VWI_ServiceTemplate
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
        Private _sVCTMPTParentGroup As String = String.Empty
        Private _sVCTMPTParent As String = String.Empty
        Private _sVCTMPTSubGroup As String = String.Empty
        Private _svcTemplateCode As String = String.Empty
        Private _description As String = String.Empty
        Private _dNETKind As String = String.Empty
        Private _intervalKM As Integer
        Private _serviceTemplateActivityDesc As String = String.Empty
        Private _duration As Double
        Private _item As String = String.Empty
        Private _itemDesc As String = String.Empty
        Private _qty As Double
        Private _price As Decimal




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


        <ColumnInfo("SVCTMPTParentGroup", "'{0}'")> _
        Public Property SVCTMPTParentGroup As String
            Get
                Return _sVCTMPTParentGroup
            End Get
            Set(ByVal value As String)
                _sVCTMPTParentGroup = value
            End Set
        End Property


        <ColumnInfo("SVCTMPTParent", "'{0}'")> _
        Public Property SVCTMPTParent As String
            Get
                Return _sVCTMPTParent
            End Get
            Set(ByVal value As String)
                _sVCTMPTParent = value
            End Set
        End Property


        <ColumnInfo("SVCTMPTSubGroup", "'{0}'")> _
        Public Property SVCTMPTSubGroup As String
            Get
                Return _sVCTMPTSubGroup
            End Get
            Set(ByVal value As String)
                _sVCTMPTSubGroup = value
            End Set
        End Property


        <ColumnInfo("SvcTemplateCode", "'{0}'")> _
        Public Property SvcTemplateCode As String
            Get
                Return _svcTemplateCode
            End Get
            Set(ByVal value As String)
                _svcTemplateCode = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("DNETKind", "'{0}'")> _
        Public Property DNETKind As String
            Get
                Return _dNETKind
            End Get
            Set(ByVal value As String)
                _dNETKind = value
            End Set
        End Property


        <ColumnInfo("IntervalKM", "{0}")> _
        Public Property IntervalKM As Integer
            Get
                Return _intervalKM
            End Get
            Set(ByVal value As Integer)
                _intervalKM = value
            End Set
        End Property


        <ColumnInfo("ServiceTemplateActivityDesc", "'{0}'")> _
        Public Property ServiceTemplateActivityDesc As String
            Get
                Return _serviceTemplateActivityDesc
            End Get
            Set(ByVal value As String)
                _serviceTemplateActivityDesc = value
            End Set
        End Property


        <ColumnInfo("Duration", "#,##0")> _
        Public Property Duration As Double
            Get
                Return _duration
            End Get
            Set(ByVal value As Double)
                _duration = value
            End Set
        End Property


        <ColumnInfo("Item", "'{0}'")> _
        Public Property Item As String
            Get
                Return _item
            End Get
            Set(ByVal value As String)
                _item = value
            End Set
        End Property


        <ColumnInfo("ItemDesc", "'{0}'")> _
        Public Property ItemDesc As String
            Get
                Return _itemDesc
            End Get
            Set(ByVal value As String)
                _itemDesc = value
            End Set
        End Property


        <ColumnInfo("Qty", "#,##0")> _
        Public Property Qty As Double
            Get
                Return _qty
            End Get
            Set(ByVal value As Double)
                _qty = value
            End Set
        End Property


        <ColumnInfo("Price", "{0}")> _
        Public Property Price As Decimal
            Get
                Return _price
            End Get
            Set(ByVal value As Decimal)
                _price = value
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

