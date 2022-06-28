#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 8:55:44 AM
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
    <Serializable(), TableInfo("PKDetail")> _
    Public Class PKDetail
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
        Private _lineItem As Integer
        Private _vehicleTypeCode As String = String.Empty
        Private _vehicleColorCode As String = String.Empty
        Private _vehicleColorName As String = String.Empty
        Private _materialNumber As String = String.Empty
        Private _targetQty As Integer
        Private _targetAmount As Decimal
        Private _targetPPh22 As Decimal
        Private _responseQty As Integer
        Private _responseDiscount As Decimal
        Private _responseAmount As Decimal
        Private _responsePPh22 As Decimal
        Private _agreeQty As Integer
        Private _agreeDiscount As Decimal
        Private _agreeAmount As Decimal
        Private _agreePPh22 As Decimal
        Private _responseSalesSurcharge As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _freeDays As Integer
        Private _maxTOPDay As Integer
        Private _sequence As Integer

        Private _vechileColor As VechileColor
        Private _pKHeader As PKHeader
        Private _VechileType As VechileType


#End Region

#Region "Public Properties"

        <ColumnInfo("FreeDays", "{0}")> _
        Public Property FreeDays() As Integer
            Get
                Return _freeDays
            End Get
            Set(ByVal value As Integer)
                _freeDays = value
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("LineItem", "{0}")> _
        Public Property LineItem() As Integer
            Get
                Return _lineItem
            End Get
            Set(ByVal value As Integer)
                _lineItem = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeCode", "'{0}'")> _
        Public Property VehicleTypeCode() As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property


        <ColumnInfo("VehicleColorCode", "'{0}'")> _
        Public Property VehicleColorCode() As String
            Get
                Return _vehicleColorCode
            End Get
            Set(ByVal value As String)
                _vehicleColorCode = value
            End Set
        End Property


        <ColumnInfo("VehicleColorName", "'{0}'")> _
        Public Property VehicleColorName() As String
            Get
                Return _vehicleColorName
            End Get
            Set(ByVal value As String)
                _vehicleColorName = value
            End Set
        End Property


        <ColumnInfo("MaterialNumber", "'{0}'")> _
        Public Property MaterialNumber() As String
            Get
                Return _materialNumber
            End Get
            Set(ByVal value As String)
                _materialNumber = value
            End Set
        End Property


        <ColumnInfo("TargetQty", "{0}")> _
        Public Property TargetQty() As Integer
            Get
                Return _targetQty
            End Get
            Set(ByVal value As Integer)
                _targetQty = value
            End Set
        End Property


        <ColumnInfo("TargetAmount", "{0}")> _
        Public Property TargetAmount() As Decimal
            Get
                Return _targetAmount
            End Get
            Set(ByVal value As Decimal)
                _targetAmount = value
            End Set
        End Property


        <ColumnInfo("TargetPPh22", "{0}")> _
        Public Property TargetPPh22() As Decimal
            Get
                Return _targetPPh22
            End Get
            Set(ByVal value As Decimal)
                _targetPPh22 = value
            End Set
        End Property


        <ColumnInfo("ResponseQty", "{0}")> _
        Public Property ResponseQty() As Integer
            Get
                Return _responseQty
            End Get
            Set(ByVal value As Integer)
                _responseQty = value
            End Set
        End Property


        <ColumnInfo("ResponseDiscount", "{0}")> _
        Public Property ResponseDiscount() As Decimal
            Get
                Return _responseDiscount
            End Get
            Set(ByVal value As Decimal)
                _responseDiscount = value
            End Set
        End Property


        <ColumnInfo("ResponseAmount", "{0}")> _
        Public Property ResponseAmount() As Decimal
            Get
                Return _responseAmount
            End Get
            Set(ByVal value As Decimal)
                _responseAmount = value
            End Set
        End Property


        <ColumnInfo("ResponsePPh22", "{0}")> _
        Public Property ResponsePPh22() As Decimal
            Get
                Return _responsePPh22
            End Get
            Set(ByVal value As Decimal)
                _responsePPh22 = value
            End Set
        End Property


        <ColumnInfo("AgreeQty", "{0}")> _
        Public Property AgreeQty() As Integer
            Get
                Return _agreeQty
            End Get
            Set(ByVal value As Integer)
                _agreeQty = value
            End Set
        End Property


        <ColumnInfo("AgreeDiscount", "{0}")> _
        Public Property AgreeDiscount() As Decimal
            Get
                Return _agreeDiscount
            End Get
            Set(ByVal value As Decimal)
                _agreeDiscount = value
            End Set
        End Property


        <ColumnInfo("AgreeAmount", "{0}")> _
        Public Property AgreeAmount() As Decimal
            Get
                Return _agreeAmount
            End Get
            Set(ByVal value As Decimal)
                _agreeAmount = value
            End Set
        End Property


        <ColumnInfo("AgreePPh22", "{0}")> _
        Public Property AgreePPh22() As Decimal
            Get
                Return _agreePPh22
            End Get
            Set(ByVal value As Decimal)
                _agreePPh22 = value
            End Set
        End Property


        <ColumnInfo("ResponseSalesSurcharge", "{0}")> _
        Public Property ResponseSalesSurcharge() As Decimal
            Get
                Return _responseSalesSurcharge
            End Get
            Set(ByVal value As Decimal)
                _responseSalesSurcharge = value
            End Set
        End Property


        <ColumnInfo("MaxTOPDay", "{0}")> _
        Public Property MaxTOPDay() As Integer
            Get
                Return _maxTOPDay
            End Get
            Set(ByVal value As Integer)
                _maxTOPDay = value
            End Set
        End Property


        <ColumnInfo("Sequence", "{0}")> _
        Public Property Sequence() As Integer
            Get
                Return _sequence
            End Get
            Set(ByVal value As Integer)
                _sequence = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("VehicleColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "PKDetail", "VehicleColorID")> _
        Public Property VechileColor() As VechileColor
            Get
                Try
                    If Not isnothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PKHeaderID", "{0}"), _
        RelationInfo("PKHeader", "ID", "PKDetail", "PKHeaderID")> _
        Public Property PKHeader() As PKHeader
            Get
                Try
                    If Not isnothing(Me._pKHeader) AndAlso (Not Me._pKHeader.IsLoaded) Then

                        Me._pKHeader = CType(DoLoad(GetType(PKHeader).ToString(), _pKHeader.ID), PKHeader)
                        Me._pKHeader.MarkLoaded()

                    End If

                    Return Me._pKHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PKHeader)

                Me._pKHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pKHeader.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("VechileTypeCode", "{0}"), _
      RelationInfo("VechileType", "VechileTypeCode", "PKDetail", "VehicleTypeCode")> _
        Public Property VechileType() As VechileType
            Get
                Try
                    If Not IsNothing(Me._vechileColor) Then

                        Dim _criteria As Criteria = New Criteria(GetType(VechileType), "VechileTypeCode", Me._vehicleTypeCode)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim ObjVechileType As ArrayList
                        ObjVechileType = New ArrayList
                        ObjVechileType = DoLoadArray(GetType(VechileType).ToString, criterias)
                        If Not IsNothing(ObjVechileType) Then
                            Me._VechileType = IIf(ObjVechileType.Count > 0, CType(ObjVechileType(0), VechileType), Nothing)
                            Me._VechileType.MarkLoaded()
                        End If


                    End If

                    Return Me._VechileType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileType)

                Me._VechileType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._VechileType.MarkLoaded()
                End If
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

