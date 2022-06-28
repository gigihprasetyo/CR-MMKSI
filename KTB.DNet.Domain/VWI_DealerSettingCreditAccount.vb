
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_DealerSettingCreditAccount Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 20/08/2018 - 11:38:45
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
    <Serializable(), TableInfo("VWI_DealerSettingCreditAccount")> _
    Public Class VWI_DealerSettingCreditAccount
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Integer)
            _id = id
        End Sub

#End Region

#Region "Private Variables"

        Private _id As Integer
        Private _dealerId As Short
        Private _dealerGroupId As Short
        Private _provinceId As Short
        Private _rowStatus As Short
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _cityName As String = String.Empty
        Private _provinceName As String = String.Empty
        Private _groupName As String = String.Empty
        Private _searchTerm1 As String = String.Empty
        Private _status As Integer
        Private _salesUnitFlag As String = String.Empty
        Private _serviceFlag As String = String.Empty
        Private _sparepartFlag As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _termOfPaymentID As Integer
        Private _prevTermOfPaymentID As Integer
        Private _creditAccount As String = String.Empty
        Private _kelipatanPembayaran As Integer




#End Region

#Region "Public Properties"

        <ColumnInfo("id", "{0}")> _
        Public Property id As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property


        <ColumnInfo("DealerId", "{0}")> _
        Public Property DealerId As Short
            Get
                Return _dealerId
            End Get
            Set(ByVal value As Short)
                _dealerId = value
            End Set
        End Property

        <ColumnInfo("DealerGroupId", "{0}")> _
        Public Property DealerGroupId As Short
            Get
                Return _dealerGroupId
            End Get
            Set(ByVal value As Short)
                _dealerGroupId = value
            End Set
        End Property

        <ColumnInfo("ProvinceId", "{0}")> _
        Public Property ProvinceId As Short
            Get
                Return _provinceId
            End Get
            Set(ByVal value As Short)
                _provinceId = value
            End Set
        End Property

        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
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


        <ColumnInfo("ProvinceName", "'{0}'")> _
        Public Property ProvinceName As String
            Get
                Return _provinceName
            End Get
            Set(ByVal value As String)
                _provinceName = value
            End Set
        End Property


        <ColumnInfo("GroupName", "'{0}'")> _
        Public Property GroupName As String
            Get
                Return _groupName
            End Get
            Set(ByVal value As String)
                _groupName = value
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


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("TermOfPaymentID", "{0}")> _
        Public Property TermOfPaymentID As Integer
            Get
                Return _termOfPaymentID
            End Get
            Set(ByVal value As Integer)
                _termOfPaymentID = value
            End Set
        End Property

        <ColumnInfo("PrevTermOfPaymentID", "{0}")> _
        Public Property PrevTermOfPaymentID As Integer
            Get
                Return _prevtermOfPaymentID
            End Get
            Set(ByVal value As Integer)
                _prevtermOfPaymentID = value
            End Set
        End Property


        <ColumnInfo("CreditAccount", "'{0}'")> _
        Public Property CreditAccount As String
            Get
                Return _creditAccount
            End Get
            Set(ByVal value As String)
                _creditAccount = value
            End Set
        End Property


        <ColumnInfo("KelipatanPembayaran", "{0}")> _
        Public Property KelipatanPembayaran As Integer
            Get
                Return _kelipatanPembayaran
            End Get
            Set(ByVal value As Integer)
                _kelipatanPembayaran = value
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

