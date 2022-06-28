#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : view_DealerWSCProccessed Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/28/2005 - 10:46:58 AM
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

    <Serializable(), TableInfo("view_DealerWSCProccessedBB")> _
    Public Class view_DealerWSCProccessedBB
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Private Variables"

        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _cityName As String = String.Empty
        Private _createdDate As DateTime = System.DateTime.MinValue
        Private _releaseDate As DateTime = System.DateTime.MinValue
        Private _claimType As String = String.Empty
        Private _wSCCount As Integer

#End Region

#Region "Public Properties"

        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("CityName", "'{0}'")> _
        Public Property CityName() As String
            Get
                Return _cityName
            End Get
            Set(ByVal value As String)
                _cityName = value
            End Set
        End Property


        <ColumnInfo("CreatedDate", "'{0:dd/MM/yyyy}'")> _
        Public Property CreatedDate() As DateTime
            Get
                Return _createdDate
            End Get
            Set(ByVal value As DateTime)
                _createdDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ReleaseDate", "'{0:dd/MM/yyyy}'")> _
        Public Property ReleaseDate() As DateTime
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As DateTime)
                _releaseDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ClaimType", "'{0}'")> _
                        Public Property ClaimType() As String
            Get
                Return _claimType
            End Get
            Set(ByVal value As String)
                _claimType = value
            End Set
        End Property

        <ColumnInfo("WSCCount", "{0}")> _
        Public Property WSCCount() As Integer
            Get
                Return _wSCCount
            End Get
            Set(ByVal value As Integer)
                _wSCCount = value
            End Set
        End Property

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

