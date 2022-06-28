#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKNationalEvent Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 4/27/2021 - 11:21:30 AM
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
    Public Class SPKNationalEventReport
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
        Private _eventName As String = String.Empty
        Private _sPKNumber As String = String.Empty
        Private _dealerSPKDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerName As String = String.Empty
        Private _salesName As String = String.Empty
        Private _salesCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _vehicleTypeCategory As String = String.Empty
        Private _vehicleTypeName As String = String.Empty
        Private _vechileColorName As String = String.Empty
        Private _assyYear As String = String.Empty
        Private _fakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturNumber As String = String.Empty
        Private _downPayment As Long = 0
        Private _quantity As Integer
        Private _remarks As String = String.Empty
        Private _shift As String = String.Empty
        Private _paymentMethod As String = String.Empty
        Private _leasingName As String = String.Empty
        Private _typeInputSPK As String = String.Empty
        Private _paymentMethodID As Integer
        Private _spkNationalEventID As Integer
        Private _leasingID As Integer

        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

#End Region

#Region "Public Properties"

        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        Public Property EventName As String
            Get
                Return _eventName
            End Get
            Set(ByVal value As String)
                _eventName = value
            End Set
        End Property


        <ColumnInfo("SPKNumber", "'{0}'")> _
        Public Property SPKNumber As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
            End Set
        End Property


        <ColumnInfo("DealerSPKDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DealerSPKDate As DateTime
            Get
                Return _dealerSPKDate
            End Get
            Set(ByVal value As DateTime)
                _dealerSPKDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        Public Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        Public Property SalesName As String
            Get
                Return _salesName
            End Get
            Set(ByVal value As String)
                _salesName = value
            End Set
        End Property

        Public Property SalesCode As String
            Get
                Return _salesCode
            End Get
            Set(ByVal value As String)
                _salesCode = value
            End Set
        End Property

        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property

        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        Public Property VehicleTypeCategory As String
            Get
                Return _vehicleTypeCategory
            End Get
            Set(ByVal value As String)
                _vehicleTypeCategory = value
            End Set
        End Property

        Public Property VehicleTypeName As String
            Get
                Return _vehicleTypeName
            End Get
            Set(ByVal value As String)
                _vehicleTypeName = value
            End Set
        End Property

        Public Property VechileColorName As String
            Get
                Return _vechileColorName
            End Get
            Set(ByVal value As String)
                _vechileColorName = value
            End Set
        End Property


        <ColumnInfo("AssyYear", "{0}")> _
        Public Property AssyYear As Integer
            Get
                Return _assyYear
            End Get
            Set(ByVal value As Integer)
                _assyYear = value
            End Set
        End Property


        <ColumnInfo("FakturDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FakturDate As DateTime
            Get
                Return _fakturDate
            End Get
            Set(ByVal value As DateTime)
                _fakturDate = value
            End Set
        End Property


        <ColumnInfo("FakturNumber", "'{0}'")> _
        Public Property FakturNumber As String
            Get
                Return _fakturNumber
            End Get
            Set(ByVal value As String)
                _fakturNumber = value
            End Set
        End Property


        <ColumnInfo("DownPayment", "{0}")> _
        Public Property DownPayment As Long
            Get
                Return _downPayment
            End Get
            Set(ByVal value As Long)
                _downPayment = value
            End Set
        End Property


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
            End Set
        End Property


        <ColumnInfo("Remarks", "'{0}'")> _
        Public Property Remarks As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
            End Set
        End Property


        <ColumnInfo("Shift", "{0}")> _
        Public Property Shift As Long
            Get
                Return _shift
            End Get
            Set(ByVal value As Long)
                _shift = value
            End Set
        End Property


        <ColumnInfo("PaymentMethod", "{0}")> _
        Public Property PaymentMethod As String
            Get
                Return _paymentMethod
            End Get
            Set(ByVal value As String)
                _paymentMethod = value
            End Set
        End Property


        <ColumnInfo("PaymentMethodID", "{0}")> _
        Public Property PaymentMethodID As String
            Get
                Return _paymentMethodID
            End Get
            Set(ByVal value As String)
                _paymentMethodID = value
            End Set
        End Property

        <ColumnInfo("SPKNationalEventID", "{0}")> _
        Public Property SPKNationalEventID As String
            Get
                Return _spkNationalEventID
            End Get
            Set(ByVal value As String)
                _spkNationalEventID = value
            End Set
        End Property

        <ColumnInfo("LeasingName", "{0}")> _
        Public Property LeasingName As String
            Get
                Return _leasingName
            End Get
            Set(ByVal value As String)
                _leasingName = value
            End Set
        End Property


        <ColumnInfo("LeasingID", "{0}")> _
        Public Property LeasingID As Integer
            Get
                Return _leasingID
            End Get
            Set(ByVal value As Integer)
                _leasingID = value
            End Set
        End Property

        <ColumnInfo("TypeInputSPK", "{0}")> _
        Public Property TypeInputSPK As String
            Get
                Return _typeInputSPK
            End Get
            Set(ByVal value As String)
                _typeInputSPK = value
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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
