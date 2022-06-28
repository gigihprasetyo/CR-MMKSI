#Region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : DealerVehiclePriceDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2021 - 3:28:26 PM
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
    <Serializable(), TableInfo("DealerVehiclePriceDetail")> _
    Public Class DealerVehiclePriceDetail
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
        Private _dealerVehiclePriceGUID As String = String.Empty
        Private _gUID As String = String.Empty
        Private _name As String = String.Empty
        Private _vechileTypeCode As String = String.Empty
        Private _vechileColorCode As String = String.Empty
        Private _oTR As Decimal
        Private _registrationFee As Decimal
        Private _consumptionTax1 As String = String.Empty
        Private _consumptionTax2 As String = String.Empty
        Private _offTR As Decimal
        Private _basePrice As Decimal
        Private _consumptionTaxAmount1 As Decimal
        Private _consumptionTaxAmount2 As Decimal
        Private _specialColorPrice As Decimal
        Private _bookingFee As Decimal
        Private _lastUpdateTimeinDMS As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
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


        <ColumnInfo("DealerVehiclePriceGUID", "'{0}'")> _
        Public Property DealerVehiclePriceGUID As String
            Get
                Return _dealerVehiclePriceGUID
            End Get
            Set(ByVal value As String)
                _dealerVehiclePriceGUID = value
            End Set
        End Property


        <ColumnInfo("GUID", "'{0}'")> _
        Public Property GUID As String
            Get
                Return _gUID
            End Get
            Set(ByVal value As String)
                _gUID = value
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


        <ColumnInfo("VechileTypeCode", "'{0}'")> _
        Public Property VechileTypeCode As String
            Get
                Return _vechileTypeCode
            End Get
            Set(ByVal value As String)
                _vechileTypeCode = value
            End Set
        End Property


        <ColumnInfo("VechileColorCode", "'{0}'")> _
        Public Property VechileColorCode As String
            Get
                Return _vechileColorCode
            End Get
            Set(ByVal value As String)
                _vechileColorCode = value
            End Set
        End Property


        <ColumnInfo("OTR", "{0}")> _
        Public Property OTR As Decimal
            Get
                Return _oTR
            End Get
            Set(ByVal value As Decimal)
                _oTR = value
            End Set
        End Property


        <ColumnInfo("RegistrationFee", "{0}")> _
        Public Property RegistrationFee As Decimal
            Get
                Return _registrationFee
            End Get
            Set(ByVal value As Decimal)
                _registrationFee = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTax1", "{0}")> _
        Public Property ConsumptionTax1 As String
            Get
                Return _consumptionTax1
            End Get
            Set(ByVal value As String)
                _consumptionTax1 = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTax2", "{0}")> _
        Public Property ConsumptionTax2 As String
            Get
                Return _consumptionTax2
            End Get
            Set(ByVal value As String)
                _consumptionTax2 = value
            End Set
        End Property


        <ColumnInfo("OffTR", "{0}")> _
        Public Property OffTR As Decimal
            Get
                Return _offTR
            End Get
            Set(ByVal value As Decimal)
                _offTR = value
            End Set
        End Property


        <ColumnInfo("BasePrice", "{0}")> _
        Public Property BasePrice As Decimal
            Get
                Return _basePrice
            End Get
            Set(ByVal value As Decimal)
                _basePrice = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTaxAmount1", "{0}")> _
        Public Property ConsumptionTaxAmount1 As Decimal
            Get
                Return _consumptionTaxAmount1
            End Get
            Set(ByVal value As Decimal)
                _consumptionTaxAmount1 = value
            End Set
        End Property


        <ColumnInfo("ConsumptionTaxAmount2", "{0}")> _
        Public Property ConsumptionTaxAmount2 As Decimal
            Get
                Return _consumptionTaxAmount2
            End Get
            Set(ByVal value As Decimal)
                _consumptionTaxAmount2 = value
            End Set
        End Property


        <ColumnInfo("SpecialColorPrice", "{0}")> _
        Public Property SpecialColorPrice As Decimal
            Get
                Return _specialColorPrice
            End Get
            Set(ByVal value As Decimal)
                _specialColorPrice = value
            End Set
        End Property


        <ColumnInfo("BookingFee", "{0}")> _
        Public Property BookingFee As Decimal
            Get
                Return _bookingFee
            End Get
            Set(ByVal value As Decimal)
                _bookingFee = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTimeinDMS", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTimeinDMS As DateTime
            Get
                Return _lastUpdateTimeinDMS
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTimeinDMS = value
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


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
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
