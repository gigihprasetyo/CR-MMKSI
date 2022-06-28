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
    <Serializable(), TableInfo("SPKNationalEvent")> _
    Public Class SPKNationalEvent
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
        Private _sPKNumber As String = String.Empty
        Private _dealerSPKDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerName As String = String.Empty
        Private _assyYear As Integer
        Private _endCustomerPrintedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _downPayment As Long
        Private _quantity As Integer
        Private _remarks As String = String.Empty
        Private _shift As Long
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _groupDealer As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerCity As String = String.Empty
        Private _salesmanName As String = String.Empty
        Private _salesmanCode As String = String.Empty
        Private _vehicleType As String = String.Empty
        Private _vehicleName As String = String.Empty
        Private _vehicleColor As String = String.Empty
        Private _paymentTypeTemp As String = String.Empty
        Private _leasingTemp As String = String.Empty

        Private _dealer As Dealer
        Private _salesmanHeader As SalesmanHeader
        Private _vechileColor As VechileColor
        Private _nationalEvent As NationalEvent
        Private _paymentType As PaymentType
        Private _leasing As Leasing
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


        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
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


        <ColumnInfo("EndCustomerPrintedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property EndCustomerPrintedTime As DateTime
            Get
                Return _endCustomerPrintedTime
            End Get
            Set(ByVal value As DateTime)
                _endCustomerPrintedTime = value
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


        <ColumnInfo("NationalEventID", "{0}"), _
        RelationInfo("NationalEvent", "ID", "SPKNationalEvent", "NationalEventID")> _
        Public Property NationalEvent As NationalEvent
            Get
                Try
                    If Not IsNothing(Me._nationalEvent) AndAlso (Not Me._nationalEvent.IsLoaded) Then

                        Me._nationalEvent = CType(DoLoad(GetType(NationalEvent).ToString(), _nationalEvent.ID), NationalEvent)
                        Me._nationalEvent.MarkLoaded()

                    End If

                    Return Me._nationalEvent

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As NationalEvent)

                Me._nationalEvent = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._nationalEvent.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("VechileColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "SPKNationalEvent", "VechileColorID")> _
        Public Property VechileColor As VechileColor
            Get
                Try
                    If Not IsNothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then

                        Me._vechileColor = CType(DoLoad(GetType(VechileColor).ToString(), _vechileColor.ID), VechileColor)
                        Me._vechileColor.MarkLoaded()

                    End If

                    Return Me._vechileColor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColor)

                Me._vechileColor = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SPKNationalEvent", "DealerID")> _
        Public Property Dealer As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanHeaderID", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "SPKNationalEvent", "SalesmanHeaderID")> _
        Public Property SalesmanHeader As SalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._salesmanHeader) AndAlso (Not Me._salesmanHeader.IsLoaded) Then

                        Me._salesmanHeader = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeader.ID), SalesmanHeader)
                        Me._salesmanHeader.MarkLoaded()

                    End If

                    Return Me._salesmanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PaymentTypeID", "{0}"), _
        RelationInfo("PaymentType", "ID", "SPKNationalEvent", "PaymentTypeID")> _
        Public Property PaymentType As PaymentType
            Get
                Try
                    If Not IsNothing(Me._paymentType) AndAlso (Not Me._paymentType.IsLoaded) Then

                        Me._paymentType = CType(DoLoad(GetType(PaymentType).ToString(), _paymentType.ID), PaymentType)
                        Me._paymentType.MarkLoaded()

                    End If

                    Return Me._paymentType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PaymentType)

                Me._paymentType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._paymentType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("LeasingID", "{0}"), _
        RelationInfo("Leasing", "ID", "SPKNationalEvent", "LeasingID")> _
        Public Property Leasing As Leasing
            Get
                Try
                    If Not IsNothing(Me._leasing) AndAlso (Not Me._leasing.IsLoaded) Then

                        Me._leasing = CType(DoLoad(GetType(Leasing).ToString(), _leasing.ID), Leasing)
                        Me._leasing.MarkLoaded()

                    End If

                    Return Me._leasing

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Leasing)

                Me._leasing = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._leasing.MarkLoaded()
                End If
            End Set
        End Property

        Public Property GroupDealer As String
            Get
                Return _groupDealer
            End Get
            Set(ByVal value As String)
                _groupDealer = value
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

        Public Property DealerCity As String
            Get
                Return _dealerCity
            End Get
            Set(ByVal value As String)
                _dealerCity = value
            End Set
        End Property

        Public Property SalesmanName As String
            Get
                Return _salesmanName
            End Get
            Set(ByVal value As String)
                _salesmanName = value
            End Set
        End Property

        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property

        Public Property VehicleType As String
            Get
                Return _vehicleType
            End Get
            Set(ByVal value As String)
                _vehicleType = value
            End Set
        End Property

        Public Property VehicleName As String
            Get
                Return _vehicleName
            End Get
            Set(ByVal value As String)
                _vehicleName = value
            End Set
        End Property

        Public Property VehicleColor As String
            Get
                Return _vehicleColor
            End Get
            Set(ByVal value As String)
                _vehicleColor = value
            End Set
        End Property

        Public Property PaymentTypeTemp As String
            Get
                Return _paymentTypeTemp
            End Get
            Set(ByVal value As String)
                _paymentTypeTemp = value
            End Set
        End Property

        Public Property LeasingTemp As String
            Get
                Return _leasingTemp
            End Get
            Set(ByVal value As String)
                _leasingTemp = value
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
