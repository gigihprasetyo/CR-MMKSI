
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailPrice Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:43:14
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
    <Serializable(), TableInfo("DiscountProposalDetailPrice")> _
    Public Class DiscountProposalDetailPrice
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
        Private _redemptionPrice As Decimal
        Private _bBN As Decimal
        Private _otherCost As Decimal
        Private _discountRequest As Decimal
        Private _logisticCost As Decimal
        Private _retailPriceOnRoad As Decimal
        Private _deliveryCost As Decimal
        Private _accessories As Decimal
        Private _dealPriceEstimation As Decimal

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _subCategoryVehicleName As String = String.Empty
        Private _typeDescription As String = String.Empty
        Private _colorIndName As String = String.Empty
        Private _vechileTypeCode As String = String.Empty
        Private _vechileTypeDesc As String = String.Empty

        Private _subCategoryVehicleID As Integer
        Private _vechileColor As Integer
        Private _vechileTypeID As Integer
        Private _discountProposalHeader As DiscountProposalHeader
        Private _discountProposalDetail As DiscountProposalDetail

        Private _proposeQty As Integer
        Private _assyYear As Integer
        Private _modelYear As Integer
        Private _numberRow As Short

        Private _discountProposalPricetoParameters As System.Collections.ArrayList = New System.Collections.ArrayList()

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


        <ColumnInfo("RedemptionPrice", "{0}")> _
        Public Property RedemptionPrice As Decimal
            Get
                Return _redemptionPrice
            End Get
            Set(ByVal value As Decimal)
                _redemptionPrice = value
            End Set
        End Property


        <ColumnInfo("BBN", "{0}")> _
        Public Property BBN As Decimal
            Get
                Return _bBN
            End Get
            Set(ByVal value As Decimal)
                _bBN = value
            End Set
        End Property


        <ColumnInfo("OtherCost", "{0}")> _
        Public Property OtherCost As Decimal
            Get
                Return _otherCost
            End Get
            Set(ByVal value As Decimal)
                _otherCost = value
            End Set
        End Property


        <ColumnInfo("DiscountRequest", "{0}")> _
        Public Property DiscountRequest As Decimal
            Get
                Return _discountRequest
            End Get
            Set(ByVal value As Decimal)
                _discountRequest = value
            End Set
        End Property


        <ColumnInfo("LogisticCost", "{0}")> _
        Public Property LogisticCost As Decimal
            Get
                Return _logisticCost
            End Get
            Set(ByVal value As Decimal)
                _logisticCost = value
            End Set
        End Property


        <ColumnInfo("RetailPriceOnRoad", "{0}")> _
        Public Property RetailPriceOnRoad As Decimal
            Get
                Return _retailPriceOnRoad
            End Get
            Set(ByVal value As Decimal)
                _retailPriceOnRoad = value
            End Set
        End Property


        <ColumnInfo("DeliveryCost", "{0}")> _
        Public Property DeliveryCost As Decimal
            Get
                Return _deliveryCost
            End Get
            Set(ByVal value As Decimal)
                _deliveryCost = value
            End Set
        End Property


        <ColumnInfo("Accessories", "{0}")> _
        Public Property Accessories As Decimal
            Get
                Return _accessories
            End Get
            Set(ByVal value As Decimal)
                _accessories = value
            End Set
        End Property


        <ColumnInfo("DealPriceEstimation", "{0}")> _
        Public Property DealPriceEstimation As Decimal
            Get
                Return _dealPriceEstimation
            End Get
            Set(ByVal value As Decimal)
                _dealPriceEstimation = value
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


        <ColumnInfo("DiscountProposalHeaderID", "{0}"), _
        RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetailPrice", "DiscountProposalHeaderID")> _
        Public Property DiscountProposalHeader As DiscountProposalHeader
            Get
                Try
                    If Not isnothing(Me._discountProposalHeader) AndAlso (Not Me._discountProposalHeader.IsLoaded) Then

                        Me._discountProposalHeader = CType(DoLoad(GetType(DiscountProposalHeader).ToString(), _discountProposalHeader.ID), DiscountProposalHeader)
                        Me._discountProposalHeader.MarkLoaded()

                    End If

                    Return Me._discountProposalHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DiscountProposalHeader)

                Me._discountProposalHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountProposalHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DiscountProposalDetailID", "{0}"), _
        RelationInfo("DiscountProposalDetail", "ID", "DiscountProposalDetailPrice", "DiscountProposalDetailID")> _
        Public Property DiscountProposalDetail As DiscountProposalDetail
            Get
                Try
                    If Not isnothing(Me._discountProposalDetail) AndAlso (Not Me._discountProposalDetail.IsLoaded) Then
                        If _discountProposalDetail.ID > 0 Then
                            Me._discountProposalDetail = CType(DoLoad(GetType(DiscountProposalDetail).ToString(), _discountProposalDetail.ID), DiscountProposalDetail)
                            Me._discountProposalDetail.MarkLoaded()
                        End If
                    End If

                    Return Me._discountProposalDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DiscountProposalDetail)

                Me._discountProposalDetail = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountProposalDetail.MarkLoaded()
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
        Public Property NumberRow As Short
            Get
                Return _numberRow
            End Get
            Set(ByVal value As Short)
                _numberRow = value
            End Set
        End Property

        Public Property SubCategoryVehicleID As Integer
            Get
                Return _subCategoryVehicleID
            End Get
            Set(ByVal value As Integer)
                _subCategoryVehicleID = value
            End Set
        End Property

        Public Property SubCategoryVehicleName As String
            Get
                Return _subCategoryVehicleName
            End Get
            Set(ByVal value As String)
                _subCategoryVehicleName = value
            End Set
        End Property

        Public Property VechileTypeID As Integer
            Get
                Return _vechileTypeID
            End Get
            Set(ByVal value As Integer)
                _vechileTypeID = value
            End Set
        End Property

        Public Property VechileTypeCode As String
            Get
                Return _vechileTypeCode
            End Get
            Set(ByVal value As String)
                _vechileTypeCode = value
            End Set
        End Property

        Public Property VechileTypeDesc As String
            Get
                Return _vechileTypeDesc
            End Get
            Set(ByVal value As String)
                _vechileTypeDesc = value
            End Set
        End Property

        Public Property VechileColorID As Integer
            Get
                Return _vechileColor
            End Get
            Set(ByVal value As Integer)
                _vechileColor = value
            End Set
        End Property

        Public Property ColorIndName As String
            Get
                Return _colorIndName
            End Get
            Set(ByVal value As String)
                _colorIndName = value
            End Set
        End Property

        Public Property TypeDescription As String
            Get
                Return _typeDescription
            End Get
            Set(ByVal value As String)
                _typeDescription = value
            End Set
        End Property

        Public Property ProposeQty As Integer
            Get
                Return _proposeQty
            End Get
            Set(ByVal value As Integer)
                _proposeQty = value
            End Set
        End Property

        Public Property AssyYear As Integer
            Get
                Return _assyYear
            End Get
            Set(ByVal value As Integer)
                _assyYear = value
            End Set
        End Property

        Public Property ModelYear As Integer
            Get
                Return _modelYear
            End Get
            Set(ByVal value As Integer)
                _modelYear = value
            End Set
        End Property

        <RelationInfo("DiscountProposalDetailPrice", "ID", "DiscountProposalPricetoParameter", "DiscountProposalDetailPriceID")> _
        Public ReadOnly Property DiscountProposalPricetoParameters As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalPricetoParameters.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalPricetoParameter), "DiscountProposalDetailPrice", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalPricetoParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalPricetoParameters = DoLoadArray(GetType(DiscountProposalPricetoParameter).ToString, criterias)
                    End If

                    Return Me._discountProposalPricetoParameters

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


#End Region

    End Class
End Namespace

