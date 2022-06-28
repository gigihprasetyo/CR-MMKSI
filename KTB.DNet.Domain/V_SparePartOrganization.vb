
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SparePartOrganization Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2011 - 11:12:23 AM
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
    <Serializable(), TableInfo("V_SparePartOrganization")> _
    Public Class V_SparePartOrganization
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Short)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Short
        Private _grade As String = String.Empty
        Private _salesmanCategoryLevelID As Integer
        Private _parentID As Integer
        Private _positionName As String = String.Empty
        Private _levelNumber As Short
        Private _coloumnNumber As Byte
        Private _orderNUmber As Short




#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Short
            Get
                Return _iD
            End Get
            Set(ByVal value As Short)
                _iD = value
            End Set
        End Property


        <ColumnInfo("Grade", "'{0}'")> _
        Public Property Grade() As String
            Get
                Return _grade
            End Get
            Set(ByVal value As String)
                _grade = value
            End Set
        End Property


        <ColumnInfo("SalesmanCategoryLevelID", "{0}")> _
        Public Property SalesmanCategoryLevelID() As Integer
            Get
                Return _salesmanCategoryLevelID
            End Get
            Set(ByVal value As Integer)
                _salesmanCategoryLevelID = value
            End Set
        End Property


        <ColumnInfo("ParentID", "{0}")> _
        Public Property ParentID() As Integer
            Get
                Return _parentID
            End Get
            Set(ByVal value As Integer)
                _parentID = value
            End Set
        End Property


        <ColumnInfo("PositionName", "'{0}'")> _
        Public Property PositionName() As String
            Get
                Return _positionName
            End Get
            Set(ByVal value As String)
                _positionName = value
            End Set
        End Property


        <ColumnInfo("LevelNumber", "{0}")> _
        Public Property LevelNumber() As Short
            Get
                Return _levelNumber
            End Get
            Set(ByVal value As Short)
                _levelNumber = value
            End Set
        End Property


        <ColumnInfo("ColoumnNumber", "{0}")> _
        Public Property ColoumnNumber() As Byte
            Get
                Return _coloumnNumber
            End Get
            Set(ByVal value As Byte)
                _coloumnNumber = value
            End Set
        End Property


        <ColumnInfo("OrderNUmber", "{0}")> _
        Public Property OrderNUmber() As Short
            Get
                Return _orderNUmber
            End Get
            Set(ByVal value As Short)
                _orderNUmber = value
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

