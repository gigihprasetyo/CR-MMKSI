#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerContactNotificationCase Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 3/12/2021 - 10:47:56 AM
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
    <Serializable(), TableInfo("DealerContactNotificationCase")> _
    Public Class DealerContactNotificationCase
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
        Private _dealerID As Short
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _searchTerm1 As String = String.Empty
        Private _jobPosition As String = String.Empty
        Private _jobpositionid As Integer
        Private _jobPosisi As String = String.Empty
        Private _phone As String = String.Empty
        Private _salesmanHeaderID As Integer
        Private _tipe As Integer
        Private _lokasiUbah As String = String.Empty




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


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("SearchTerm1", "'{0}'")> _
        Public Property SearchTerm1 As String
            Get
                Return _searchTerm1
            End Get
            Set(ByVal value As String)
                _searchTerm1 = value
            End Set
        End Property


        <ColumnInfo("JobPosition", "'{0}'")> _
        Public Property JobPosition As String
            Get
                Return _jobPosition
            End Get
            Set(ByVal value As String)
                _jobPosition = value
            End Set
        End Property


        <ColumnInfo("jobpositionid", "{0}")> _
        Public Property jobpositionid As Integer
            Get
                Return _jobpositionid
            End Get
            Set(ByVal value As Integer)
                _jobpositionid = value
            End Set
        End Property


        <ColumnInfo("JobPosisi", "'{0}'")> _
        Public Property JobPosisi As String
            Get
                Return _jobPosisi
            End Get
            Set(ByVal value As String)
                _jobPosisi = value
            End Set
        End Property


        <ColumnInfo("Phone", "'{0}'")> _
        Public Property Phone As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
            End Set
        End Property


        <ColumnInfo("SalesmanHeaderID", "{0}")> _
        Public Property SalesmanHeaderID As Integer
            Get
                Return _salesmanHeaderID
            End Get
            Set(ByVal value As Integer)
                _salesmanHeaderID = value
            End Set
        End Property


        <ColumnInfo("Tipe", "{0}")> _
        Public Property Tipe As Integer
            Get
                Return _tipe
            End Get
            Set(ByVal value As Integer)
                _tipe = value
            End Set
        End Property


        <ColumnInfo("LokasiUbah", "'{0}'")> _
        Public Property LokasiUbah As String
            Get
                Return _lokasiUbah
            End Get
            Set(ByVal value As String)
                _lokasiUbah = value
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
