
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VwBCPColumns Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 12/13/2012 - 3:08:28 PM
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
    <Serializable(), TableInfo("VwBCPColumns")> _
    Public Class VwBCPColumns
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal object_id As Integer, ByVal ColumnID As Integer)
            _object_id = object_id
            _columnID = ColumnID
        End Sub

#End Region

#Region "Private Variables"

        Private _object_id As Integer
        Private _viewName As String = String.Empty
        Private _columnID As Integer
        Private _coloumnName As String = String.Empty
        Private _columnType As Integer
        Private _maxlength As Short




#End Region

#Region "Public Properties"

        <ColumnInfo("object_id", "{0}")> _
        Public Property object_id() As Integer
            Get
                Return _object_id
            End Get
            Set(ByVal value As Integer)
                _object_id = value
            End Set
        End Property


        <ColumnInfo("ViewName", "'{0}'")> _
        Public Property ViewName() As String
            Get
                Return _viewName
            End Get
            Set(ByVal value As String)
                _viewName = value
            End Set
        End Property


        <ColumnInfo("ColumnID", "{0}")> _
        Public Property ColumnID() As Integer
            Get
                Return _columnID
            End Get
            Set(ByVal value As Integer)
                _columnID = value
            End Set
        End Property


        <ColumnInfo("ColoumnName", "'{0}'")> _
        Public Property ColoumnName() As String
            Get
                Return _coloumnName
            End Get
            Set(ByVal value As String)
                _coloumnName = value
            End Set
        End Property


        <ColumnInfo("ColumnType", "{0}")> _
        Public Property ColumnType() As Integer
            Get
                Return _columnType
            End Get
            Set(ByVal value As Integer)
                _columnType = value
            End Set
        End Property


        <ColumnInfo("Maxlength", "{0}")> _
        Public Property Maxlength() As Short
            Get
                Return _maxlength
            End Get
            Set(ByVal value As Short)
                _maxlength = value
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

