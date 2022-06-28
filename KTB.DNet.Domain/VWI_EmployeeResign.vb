
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_EmployeeResign Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/04/2018 - 10:03:42
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
    <Serializable(), TableInfo("VWI_EmployeeResign")>
    Public Class VWI_EmployeeResign
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
        Private _salesmanCode As String = String.Empty
        Private _name As String = String.Empty
        Private _placeOfBirth As String = String.Empty
        Private _dateOfBirth As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _noKTP As String = String.Empty
        'Private _noHP As String = String.Empty
        'Private _email As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")>
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("SalesmanCode", "'{0}'")>
        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property

        <ColumnInfo("Name", "'{0}'")>
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        <ColumnInfo("PlaceOfBirth", "'{0}'")>
        Public Property PlaceOfBirth As String
            Get
                Return _placeOfBirth
            End Get
            Set(ByVal value As String)
                _placeOfBirth = value
            End Set
        End Property

        <ColumnInfo("DateOfBirth", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property DateOfBirth As DateTime
            Get
                Return _dateOfBirth
            End Get
            Set(ByVal value As DateTime)
                _dateOfBirth = value
            End Set
        End Property

        <ColumnInfo("NoKTP", "'{0}'")>
        Public Property NoKTP As String
            Get
                Return _noKTP
            End Get
            Set(ByVal value As String)
                _noKTP = value
            End Set
        End Property

        '<ColumnInfo("NoHP", "{0}")>
        'Public Property NoHP As String
        '    Get
        '        Return _noHP
        '    End Get
        '    Set(ByVal value As String)
        '        _noHP = value
        '    End Set
        'End Property

        '<ColumnInfo("Email", "{0}")>
        'Public Property Email As String
        '    Get
        '        Return _email
        '    End Get
        '    Set(ByVal value As String)
        '        _email = value
        '    End Set
        'End Property

        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")>
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
