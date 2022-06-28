#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_Dealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 9/18/2020 - 4:10:45 PM
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
    <Serializable(), TableInfo("VWI_Dealer")> _
    Public Class VWI_Dealer
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Short)
            _id = id
        End Sub

#End Region

#Region "Private Variables"

        Private _id As Short
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _term As String = String.Empty
        Private _kategori As String = String.Empty
        Private _status As String = String.Empty
        Private _address As String = String.Empty
        Private _cityName As String = String.Empty
        Private _provinceName As String = String.Empty
        Private _phone As String = String.Empty
        Private _salesUnitFlag As String = String.Empty
        Private _serviceFlag As String = String.Empty
        Private _sparepartFlag As String = String.Empty
        Private _systemID As Short

        Private _nicknameDigital As String = String.Empty
        Private _nicknameEcommerce As String = String.Empty
        Private _longitude As String = String.Empty
        Private _latitude As String = String.Empty
        Private _isPublish As Integer

        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


#End Region

#Region "Public Properties"

        <ColumnInfo("id", "{0}")> _
        Public Property id As Short
            Get
                Return _id
            End Get
            Set(ByVal value As Short)
                _id = value
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


        <ColumnInfo("Term", "'{0}'")> _
        Public Property Term As String
            Get
                Return _term
            End Get
            Set(ByVal value As String)
                _term = value
            End Set
        End Property


        <ColumnInfo("kategori", "'{0}'")> _
        Public Property kategori As String
            Get
                Return _kategori
            End Get
            Set(ByVal value As String)
                _kategori = value
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


        <ColumnInfo("Address", "'{0}'")> _
        Public Property Address As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
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


        <ColumnInfo("ProvinceName", "'{0}'")> _
        Public Property ProvinceName As String
            Get
                Return _provinceName
            End Get
            Set(ByVal value As String)
                _provinceName = value
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


        <ColumnInfo("SalesUnitFlag", "'{0}'")> _
        Public Property SalesUnitFlag As String
            Get
                Return _salesUnitFlag
            End Get
            Set(ByVal value As String)
                _salesUnitFlag = value
            End Set
        End Property


        <ColumnInfo("ServiceFlag", "'{0}'")> _
        Public Property ServiceFlag As String
            Get
                Return _serviceFlag
            End Get
            Set(ByVal value As String)
                _serviceFlag = value
            End Set
        End Property


        <ColumnInfo("SparepartFlag", "'{0}'")> _
        Public Property SparepartFlag As String
            Get
                Return _sparepartFlag
            End Get
            Set(ByVal value As String)
                _sparepartFlag = value
            End Set
        End Property


        <ColumnInfo("SystemID", "{0}")> _
        Public Property SystemID As Short
            Get
                Return _systemID
            End Get
            Set(ByVal value As Short)
                _systemID = value
            End Set
        End Property

        '=============================================== CR DigitalizeProduct =========

        <ColumnInfo("NicknameDigital", "'{0}'")> _
        Public Property NicknameDigital As String
            Get
                Return _nicknameDigital
            End Get
            Set(ByVal value As String)
                _nicknameDigital = value
            End Set
        End Property

        <ColumnInfo("NicknameEcommerce", "'{0}'")> _
        Public Property NicknameEcommerce As String
            Get
                Return _nicknameEcommerce
            End Get
            Set(ByVal value As String)
                _nicknameEcommerce = value
            End Set
        End Property

        <ColumnInfo("Longitude", "'{0}'")> _
        Public Property Longitude As String
            Get
                Return _longitude
            End Get
            Set(ByVal value As String)
                _longitude = value
            End Set
        End Property

        <ColumnInfo("Latitude", "'{0}'")> _
        Public Property Latitude As String
            Get
                Return _latitude
            End Get
            Set(ByVal value As String)
                _latitude = value
            End Set
        End Property

        <ColumnInfo("IsPublish", "'{0}'")> _
        Public Property IsPublish As Integer
            Get
                Return _isPublish
            End Get
            Set(ByVal value As Integer)
                _isPublish = value
            End Set
        End Property

        '=============================================== CR DigitalizeProduct =========

        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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
