
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : vwholesalesprice Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 06/06/2018 - 13:45:51
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
    <Serializable(), TableInfo("VWI_WholeSalesPrice")> _
    Public Class VWI_WholeSalesPrice
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
        Private _vehicleColorID As Short
        Private _materialNumber As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _vehicleColorCode As String = String.Empty
        Private _vehicleColorName As String = String.Empty
        Private _vehicleTypeCode As String = String.Empty
        Private _vehicleTypeDesc As String = String.Empty
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _basePrice As Decimal
        Private _optionPrice As Decimal
        Private _pPN_BM As Decimal
        Private _pPN As Decimal
        Private _pPh22 As Decimal
        Private _pPh23 As Decimal
        Private _factoringInt As Decimal
        Private _discountReward As Decimal
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


        <ColumnInfo("VehicleColorID", "{0}")> _
        Public Property VehicleColorID As Short
            Get
                Return _vehicleColorID
            End Get
            Set(ByVal value As Short)
                _vehicleColorID = value
            End Set
        End Property


        <ColumnInfo("MaterialNumber", "'{0}'")> _
        Public Property MaterialNumber As String
            Get
                Return _materialNumber
            End Get
            Set(ByVal value As String)
                _materialNumber = value
            End Set
        End Property


        <ColumnInfo("MaterialDescription", "'{0}'")> _
        Public Property MaterialDescription As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property


        <ColumnInfo("VehicleColorCode", "'{0}'")> _
        Public Property VehicleColorCode As String
            Get
                Return _vehicleColorCode
            End Get
            Set(ByVal value As String)
                _vehicleColorCode = value
            End Set
        End Property


        <ColumnInfo("VehicleColorName", "'{0}'")> _
        Public Property VehicleColorName As String
            Get
                Return _vehicleColorName
            End Get
            Set(ByVal value As String)
                _vehicleColorName = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeCode", "'{0}'")> _
        Public Property VehicleTypeCode As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeDesc", "'{0}'")> _
        Public Property VehicleTypeDesc As String
            Get
                Return _vehicleTypeDesc
            End Get
            Set(ByVal value As String)
                _vehicleTypeDesc = value
            End Set
        End Property


        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidFrom As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
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


        <ColumnInfo("OptionPrice", "{0}")> _
        Public Property OptionPrice As Decimal
            Get
                Return _optionPrice
            End Get
            Set(ByVal value As Decimal)
                _optionPrice = value
            End Set
        End Property


        <ColumnInfo("PPN_BM", "{0}")> _
        Public Property PPN_BM As Decimal
            Get
                Return _pPN_BM
            End Get
            Set(ByVal value As Decimal)
                _pPN_BM = value
            End Set
        End Property


        <ColumnInfo("PPN", "{0}")> _
        Public Property PPN As Decimal
            Get
                Return _pPN
            End Get
            Set(ByVal value As Decimal)
                _pPN = value
            End Set
        End Property


        <ColumnInfo("PPh22", "{0}")> _
        Public Property PPh22 As Decimal
            Get
                Return _pPh22
            End Get
            Set(ByVal value As Decimal)
                _pPh22 = value
            End Set
        End Property


        <ColumnInfo("PPh23", "{0}")> _
        Public Property PPh23 As Decimal
            Get
                Return _pPh23
            End Get
            Set(ByVal value As Decimal)
                _pPh23 = value
            End Set
        End Property


        <ColumnInfo("FactoringInt", "{0}")> _
        Public Property FactoringInt As Decimal
            Get
                Return _factoringInt
            End Get
            Set(ByVal value As Decimal)
                _factoringInt = value
            End Set
        End Property


        <ColumnInfo("DiscountReward", "{0}")> _
        Public Property DiscountReward As Decimal
            Get
                Return _discountReward
            End Get
            Set(ByVal value As Decimal)
                _discountReward = value
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

