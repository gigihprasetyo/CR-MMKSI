
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:47:39
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
    <Serializable(), TableInfo("DiscountProposalDetail")> _
    Public Class DiscountProposalDetail
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
        Private _assyYear As Short
        Private _modelYear As Short
        Private _proposeQty As Integer
        Private _responseQty As Integer
        Private _priceReff As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _maxTOPDay As Integer
        Private _freeIntIndicator As Short
        Private _deliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _discountProposalHeader As DiscountProposalHeader
        Private _sPLDetail As SPLDetail
        Private _subCategoryVehicle As SubCategoryVehicle
        Private _vechileColorIsActiveOnPK As VechileColorIsActiveOnPK

        Private _numberRow As Short

        Private _discountProposalDetailPrices As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("AssyYear", "{0}")> _
        Public Property AssyYear As Short
            Get
                Return _assyYear
            End Get
            Set(ByVal value As Short)
                _assyYear = value
            End Set
        End Property


        <ColumnInfo("ModelYear", "{0}")> _
        Public Property ModelYear As Short
            Get
                Return _modelYear
            End Get
            Set(ByVal value As Short)
                _modelYear = value
            End Set
        End Property


        <ColumnInfo("ProposeQty", "{0}")> _
        Public Property ProposeQty As Integer
            Get
                Return _proposeQty
            End Get
            Set(ByVal value As Integer)
                _proposeQty = value
            End Set
        End Property


        <ColumnInfo("ResponseQty", "{0}")> _
        Public Property ResponseQty As Integer
            Get
                Return _responseQty
            End Get
            Set(ByVal value As Integer)
                _responseQty = value
            End Set
        End Property


        <ColumnInfo("PriceReff", "'{0:yyyy/MM/dd}'")> _
        Public Property PriceReff As DateTime
            Get
                Return _priceReff
            End Get
            Set(ByVal value As DateTime)
                _priceReff = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("MaxTOPDay", "{0}")> _
        Public Property MaxTOPDay As Integer
            Get
                Return _maxTOPDay
            End Get
            Set(ByVal value As Integer)
                _maxTOPDay = value
            End Set
        End Property


        <ColumnInfo("FreeIntIndicator", "{0}")> _
        Public Property FreeIntIndicator As Short
            Get
                Return _freeIntIndicator
            End Get
            Set(ByVal value As Short)
                _freeIntIndicator = value
            End Set
        End Property


        <ColumnInfo("DeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DeliveryDate As DateTime
            Get
                Return _deliveryDate
            End Get
            Set(ByVal value As DateTime)
                _deliveryDate = New DateTime(value.Year, value.Month, value.Day)
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
        RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetail", "DiscountProposalHeaderID")> _
        Public Property DiscountProposalHeader As DiscountProposalHeader
            Get
                Try
                    If Not IsNothing(Me._discountProposalHeader) AndAlso (Not Me._discountProposalHeader.IsLoaded) Then
                        If _discountProposalHeader.ID > 0 Then
                            Me._discountProposalHeader = CType(DoLoad(GetType(DiscountProposalHeader).ToString(), _discountProposalHeader.ID), DiscountProposalHeader)
                            Me._discountProposalHeader.MarkLoaded()
                        End If
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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountProposalHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SubCategoryVehicleID", "{0}"), _
        RelationInfo("SubCategoryVehicle", "ID", "DiscountProposalDetail", "SubCategoryVehicleID")> _
        Public Property SubCategoryVehicle As SubCategoryVehicle
            Get
                Try
                    If Not IsNothing(Me._subCategoryVehicle) AndAlso (Not Me._subCategoryVehicle.IsLoaded) Then
                        If _subCategoryVehicle.ID > 0 Then '
                            Me._subCategoryVehicle = CType(DoLoad(GetType(SubCategoryVehicle).ToString(), _subCategoryVehicle.ID), SubCategoryVehicle)
                            Me._subCategoryVehicle.MarkLoaded()
                        End If
                    End If

                    Return Me._subCategoryVehicle

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SubCategoryVehicle)

                Me._subCategoryVehicle = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._subCategoryVehicle.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VechileColorIsActiveOnPKID", "{0}"), _
        RelationInfo("VechileColorIsActiveOnPK", "ID", "DiscountProposalDetail", "VechileColorIsActiveOnPKID")> _
        Public Property VechileColorIsActiveOnPK As VechileColorIsActiveOnPK
            Get
                Try
                    If Not IsNothing(Me._vechileColorIsActiveOnPK) AndAlso (Not Me._vechileColorIsActiveOnPK.IsLoaded) Then
                        If _vechileColorIsActiveOnPK.ID > 0 Then
                            Me._vechileColorIsActiveOnPK = CType(DoLoad(GetType(VechileColorIsActiveOnPK).ToString(), _vechileColorIsActiveOnPK.ID), VechileColorIsActiveOnPK)
                            Me._vechileColorIsActiveOnPK.MarkLoaded()
                        End If
                    End If

                    Return Me._vechileColorIsActiveOnPK

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColorIsActiveOnPK)

                Me._vechileColorIsActiveOnPK = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColorIsActiveOnPK.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("DiscountProposalDetail", "ID", "DiscountProposalDetailPrice", "DiscountProposalDetailID")> _
        Public ReadOnly Property DiscountProposalDetailPrices As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalDetailPrices.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalDetailPrice), "DiscountProposalDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalDetailPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalDetailPrices = DoLoadArray(GetType(DiscountProposalDetailPrice).ToString, criterias)
                    End If

                    Return Me._discountProposalDetailPrices

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
#End Region

    End Class
End Namespace

