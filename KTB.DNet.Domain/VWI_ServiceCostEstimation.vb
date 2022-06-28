#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_ServiceCostEstimation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2020 - 12:23:06 PM
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
    <Serializable(), TableInfo("VWI_ServiceCostEstimation")> _
    Public Class VWI_ServiceCostEstimation
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
        Private _serviceType As Integer
        Private _kindCode As String = String.Empty
        Private _vechileTypeCode As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _varian As String = String.Empty
        Private _jenisService As String = String.Empty
        Private _jenisKegiatan As String = String.Empty
        Private _jasaService As Decimal
        Private _details As String = String.Empty
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

        <ColumnInfo("ServiceType", "{0}")> _
        Public Property ServiceType As Integer
            Get
                Return _serviceType
            End Get
            Set(ByVal value As Integer)
                _serviceType = value
            End Set
        End Property

        <ColumnInfo("KindCode", "'{0}'")> _
        Public Property KindCode As String
            Get
                Return _kindCode
            End Get
            Set(ByVal value As String)
                _kindCode = value
            End Set
        End Property

        <ColumnInfo("VechileTypeCode", "'{0}'")> _
        Public Property VechileTypeCode As String
            Get
                Return _vechileTypeCode
            End Get
            Set(ByVal value As String)
                _vechileTypeCode = value
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

        <ColumnInfo("Varian", "'{0}'")> _
        Public Property Varian As String
            Get
                Return _varian
            End Get
            Set(ByVal value As String)
                _varian = value
            End Set
        End Property

        <ColumnInfo("JenisKegiatan", "'{0}'")> _
        Public Property JenisKegiatan As String
            Get
                Return _jenisKegiatan
            End Get
            Set(ByVal value As String)
                _jenisKegiatan = value
            End Set
        End Property

        <ColumnInfo("JenisService", "'{0}'")> _
        Public Property JenisService As String
            Get
                Return _jenisService
            End Get
            Set(ByVal value As String)
                _jenisService = value
            End Set
        End Property

        <ColumnInfo("JasaService", "{0}")> _
        Public Property JasaService As Decimal
            Get
                Return _jasaService
            End Get
            Set(ByVal value As Decimal)
                _jasaService = value
            End Set
        End Property

        <ColumnInfo("Details", "'{0}'")> _
        Public Property Details As String
            Get
                Return _details
            End Get
            Set(ByVal value As String)
                _details = value
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
