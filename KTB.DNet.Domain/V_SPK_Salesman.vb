
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SPK_Salesman Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 4/19/2012 - 9:24:49 AM
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
    <Serializable(), TableInfo("V_SPK_Salesman")> _
    Public Class V_SPK_Salesman
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal SPKFakturID As Integer)
            _sPKFakturID = SPKFakturID
        End Sub

#End Region

#Region "Private Variables"

        Private _sPKFakturID As Integer
        Private _fakturStatus As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _address As String = String.Empty
        Private _cityName As String = String.Empty
        Private _salesmanHeaderID As Short
        Private _salesmanCode As String = String.Empty
        Private _salesmanName As String = String.Empty
        Private _leaderId As Integer
        Private _leaderName As String = String.Empty
        Private _fakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturNumber As String = String.Empty
        Private _sPKDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sPKNumber As String = String.Empty
        Private _chassisMasterID As Integer
        Private _chassisNumber As String = String.Empty
        Private _categoryID As Byte
        Private _categoryCode As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _customerName As String = String.Empty
        Private _phoneNo As String = String.Empty
        Private _categoryTeam As String = String.Empty
        Private _alamat As String = String.Empty
        Private _fakturValidateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _invoiceOpen As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"

        <ColumnInfo("SPKFakturID", "{0}")> _
        Public Property SPKFakturID() As Integer
            Get
                Return _sPKFakturID
            End Get
            Set(ByVal value As Integer)
                _sPKFakturID = value
            End Set
        End Property


        <ColumnInfo("FakturStatus", "'{0}'")> _
        Public Property FakturStatus() As String
            Get
                Return _fakturStatus
            End Get
            Set(ByVal value As String)
                _fakturStatus = value
            End Set
        End Property


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


        <ColumnInfo("Address", "'{0}'")> _
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
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


        <ColumnInfo("SalesmanHeaderID", "{0}")> _
        Public Property SalesmanHeaderID() As Short
            Get
                Return _salesmanHeaderID
            End Get
            Set(ByVal value As Short)
                _salesmanHeaderID = value
            End Set
        End Property

        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode() As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property

        <ColumnInfo("SalesmanName", "'{0}'")> _
        Public Property SalesmanName() As String
            Get
                Return _salesmanName
            End Get
            Set(ByVal value As String)
                _salesmanName = value
            End Set
        End Property


        <ColumnInfo("LeaderId", "{0}")> _
        Public Property LeaderId() As Integer
            Get
                Return _leaderId
            End Get
            Set(ByVal value As Integer)
                _leaderId = value
            End Set
        End Property


        <ColumnInfo("LeaderName", "'{0}'")> _
        Public Property LeaderName() As String
            Get
                Return _leaderName
            End Get
            Set(ByVal value As String)
                _leaderName = value
            End Set
        End Property


        <ColumnInfo("FakturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FakturDate() As DateTime
            Get
                Return _fakturDate
            End Get
            Set(ByVal value As DateTime)
                _fakturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FakturNumber", "'{0}'")> _
        Public Property FakturNumber() As String
            Get
                Return _fakturNumber
            End Get
            Set(ByVal value As String)
                _fakturNumber = value
            End Set
        End Property


        <ColumnInfo("SPKDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SPKDate() As DateTime
            Get
                Return _sPKDate
            End Get
            Set(ByVal value As DateTime)
                _sPKDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SPKNumber", "'{0}'")> _
        Public Property SPKNumber() As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
            End Set
        End Property


        <ColumnInfo("ChassisMasterID", "{0}")> _
        Public Property ChassisMasterID() As Integer
            Get
                Return _chassisMasterID
            End Get
            Set(ByVal value As Integer)
                _chassisMasterID = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber() As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property


        <ColumnInfo("CategoryID", "{0}")> _
        Public Property CategoryID() As Byte
            Get
                Return _categoryID
            End Get
            Set(ByVal value As Byte)
                _categoryID = value
            End Set
        End Property

        <ColumnInfo("CategoryCode", "'{0}'")> _
        Public Property CategoryCode() As String
            Get
                Return _categoryCode
            End Get
            Set(ByVal value As String)
                _categoryCode = value
            End Set
        End Property

        <ColumnInfo("MaterialDescription", "'{0}'")> _
        Public Property MaterialDescription() As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property


        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName() As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        <ColumnInfo("PhoneNo", "'{0}'")> _
        Public Property PhoneNo() As String
            Get
                Return _phoneNo
            End Get
            Set(ByVal value As String)
                _phoneNo = value
            End Set
        End Property

        <ColumnInfo("CategoryTeam", "'{0}'")> _
        Public Property CategoryTeam() As String
            Get
                Return _categoryTeam
            End Get
            Set(ByVal value As String)
                _categoryTeam = value
            End Set
        End Property

        <ColumnInfo("Alamat", "'{0}'")> _
                Public Property Alamat() As String
            Get
                Return _alamat
            End Get
            Set(ByVal value As String)
                _alamat = value
            End Set
        End Property

        <ColumnInfo("FakturValidateTime", "'{0:yyyy/MM/dd}'")> _
        Public Property FakturValidateTime() As DateTime
            Get
                Return _fakturValidateTime
            End Get
            Set(ByVal value As DateTime)
                _fakturValidateTime = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("InvoiceOpen", "'{0:yyyy/MM/dd}'")> _
        Public Property InvoiceOpen() As DateTime
            Get
                Return _invoiceOpen
            End Get
            Set(ByVal value As DateTime)
                _invoiceOpen = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ConfirmTime", "'{0:yyyy/MM/dd}'")> _
        Public Property ConfirmTime() As DateTime
            Get
                Return _confirmTime
            End Get
            Set(ByVal value As DateTime)
                _confirmTime = New DateTime(value.Year, value.Month, value.Day)
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

