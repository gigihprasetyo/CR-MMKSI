
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_ProfileDetailFromHeaderCode Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 16/04/2018 - 15:40:05
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
    <Serializable(), TableInfo("VWI_ProfileDetailFromHeaderCode")> _
    Public Class VWI_ProfileDetailFromHeaderCode
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
        Private _profileHeaderID As Integer
        Private _profileHeaderCode As String = String.Empty
        Private _profileHeaderDesc As String = String.Empty
        Private _profileDetailCode As String = String.Empty
        Private _profileDetailDesc As String = String.Empty
        Private _status As Integer
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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


        <ColumnInfo("ProfileHeaderID", "'{0}'")> _
        Public Property ProfileHeaderID As Integer
            Get
                Return _profileHeaderID
            End Get
            Set(ByVal value As Integer)
                _profileHeaderID = value
            End Set
        End Property


        <ColumnInfo("ProfileHeaderCode", "'{0}'")> _
        Public Property ProfileHeaderCode As String
            Get
                Return _profileHeaderCode
            End Get
            Set(ByVal value As String)
                _profileHeaderCode = value
            End Set
        End Property


        <ColumnInfo("ProfileHeaderDesc", "'{0}'")> _
        Public Property ProfileHeaderDesc As String
            Get
                Return _profileHeaderDesc
            End Get
            Set(ByVal value As String)
                _profileHeaderDesc = value
            End Set
        End Property


        <ColumnInfo("ProfileDetailCode", "'{0}'")> _
        Public Property ProfileDetailCode As String
            Get
                Return _profileDetailCode
            End Get
            Set(ByVal value As String)
                _profileDetailCode = value
            End Set
        End Property


        <ColumnInfo("ProfileDetailDesc", "'{0}'")> _
        Public Property ProfileDetailDesc As String
            Get
                Return _profileDetailDesc
            End Get
            Set(ByVal value As String)
                _profileDetailDesc = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
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