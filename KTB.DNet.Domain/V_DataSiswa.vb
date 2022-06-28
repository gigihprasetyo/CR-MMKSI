
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_DataSiswa Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/14/2017 - 1:21:04 PM
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
    <Serializable(), TableInfo("V_DataSiswa")> _
    Public Class V_DataSiswa
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
        Private _tanggalRegister As String = String.Empty
        Private _tanggalRegister2 As String = String.Empty
        Private _name As String = String.Empty
        Private _jobPosition As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _cityName As String = String.Empty
        Private _status As String = String.Empty
        Private _description As String = String.Empty
        Private _salesmanCode As String = String.Empty


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


        <ColumnInfo("TanggalRegister", "'{0}'")> _
        Public Property TanggalRegister As String
            Get
                Return _tanggalRegister
            End Get
            Set(ByVal value As String)
                _tanggalRegister = value
            End Set
        End Property


        <ColumnInfo("TanggalRegister2", "'{0}'")> _
        Public Property TanggalRegister2 As String
            Get
                Return _tanggalRegister2
            End Get
            Set(ByVal value As String)
                _tanggalRegister2 = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
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


        <ColumnInfo("CityName", "'{0}'")> _
        Public Property CityName As String
            Get
                Return _cityName
            End Get
            Set(ByVal value As String)
                _cityName = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
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
       
        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
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

