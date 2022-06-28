
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_JobPositionParts Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 09/01/2019 - 9:10:41
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
    <Serializable(), TableInfo("VWI_JobPositionParts")> _
    Public Class VWI_JobPositionParts
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
        Private _code As String = String.Empty
        Private _positionName As String = String.Empty
        Private _parentID As Integer
        Private _parentCode As String = String.Empty
        Private _parentPositionName As String = String.Empty




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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("PositionName", "'{0}'")> _
        Public Property PositionName As String
            Get
                Return _positionName
            End Get
            Set(ByVal value As String)
                _positionName = value
            End Set
        End Property


        <ColumnInfo("ParentID", "{0}")> _
        Public Property ParentID As Integer
            Get
                Return _parentID
            End Get
            Set(ByVal value As Integer)
                _parentID = value
            End Set
        End Property


        <ColumnInfo("ParentCode", "'{0}'")> _
        Public Property ParentCode As String
            Get
                Return _parentCode
            End Get
            Set(ByVal value As String)
                _parentCode = value
            End Set
        End Property


        <ColumnInfo("ParentPositionName", "'{0}'")> _
        Public Property ParentPositionName As String
            Get
                Return _parentPositionName
            End Get
            Set(ByVal value As String)
                _parentPositionName = value
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

