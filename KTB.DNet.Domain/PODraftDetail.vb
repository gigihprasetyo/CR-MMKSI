
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODraftDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 12/17/2018 - 5:49:58 PM
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
    <Serializable(), TableInfo("PODraftDetail")> _
    Public Class PODraftDetail
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
        'private _pODraftHeaderID as integer 		
        Private _lineItem As Short
        'private _contractDetailID as integer 		
        Private _reqQty As Integer
        Private _price As Decimal
        Private _discount As Decimal
        Private _interest As Decimal
        Private _discountReward As Decimal
        Private _amountReward As Decimal
        Private _amountRewardDepA As Decimal
        Private _pPh22 As Decimal
        Private _logisticCost As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _freeDays As Integer
        Private _maxTOPDay As Integer

        Private _contractDetail As ContractDetail
        Private _pODraftHeader As PODraftHeader


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


        <ColumnInfo("LineItem", "{0}")> _
        Public Property LineItem As Short
            Get
                Return _lineItem
            End Get
            Set(ByVal value As Short)
                _lineItem = value
            End Set
        End Property


        <ColumnInfo("ReqQty", "{0}")> _
        Public Property ReqQty As Integer
            Get
                Return _reqQty
            End Get
            Set(ByVal value As Integer)
                _reqQty = value
            End Set
        End Property


        <ColumnInfo("Price", "{0}")> _
        Public Property Price As Decimal
            Get
                Return _price
            End Get
            Set(ByVal value As Decimal)
                _price = value
            End Set
        End Property


        <ColumnInfo("Discount", "{0}")> _
        Public Property Discount As Decimal
            Get
                Return _discount
            End Get
            Set(ByVal value As Decimal)
                _discount = value
            End Set
        End Property


        <ColumnInfo("Interest", "{0}")> _
        Public Property Interest As Decimal
            Get
                Return _interest
            End Get
            Set(ByVal value As Decimal)
                _interest = value
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


        <ColumnInfo("AmountReward", "{0}")> _
        Public Property AmountReward As Decimal
            Get
                Return _amountReward
            End Get
            Set(ByVal value As Decimal)
                _amountReward = value
            End Set
        End Property


        <ColumnInfo("AmountRewardDepA", "{0}")> _
        Public Property AmountRewardDepA As Decimal
            Get
                Return _amountRewardDepA
            End Get
            Set(ByVal value As Decimal)
                _amountRewardDepA = value
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


        <ColumnInfo("LogisticCost", "{0}")> _
        Public Property LogisticCost As Decimal
            Get
                Return _logisticCost
            End Get
            Set(ByVal value As Decimal)
                _logisticCost = value
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


        <ColumnInfo("FreeDays", "{0}")> _
        Public Property FreeDays As Integer
            Get
                Return _freeDays
            End Get
            Set(ByVal value As Integer)
                _freeDays = value
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


        <ColumnInfo("ContractDetailID", "{0}"), _
        RelationInfo("ContractDetail", "ID", "PODraftDetail", "ContractDetailID")> _
        Public Property ContractDetail() As ContractDetail
            Get
                Try
                    If Not IsNothing(Me._contractDetail) AndAlso (Not Me._contractDetail.IsLoaded) Then

                        Me._contractDetail = CType(DoLoad(GetType(ContractDetail).ToString(), _contractDetail.ID), ContractDetail)
                        Me._contractDetail.MarkLoaded()

                    End If

                    Return Me._contractDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ContractDetail)

                Me._contractDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._contractDetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PODraftHeaderID", "{0}"), _
        RelationInfo("PODraftHeader", "ID", "PODraftDetail", "PODraftHeaderID")> _
        Public Property PODraftHeader() As PODraftHeader
            Get
                Try
                    If Not IsNothing(Me._pODraftHeader) AndAlso (Not Me._pODraftHeader.IsLoaded) Then

                        Me._pODraftHeader = CType(DoLoad(GetType(PODraftHeader).ToString(), _pODraftHeader.ID), PODraftHeader)
                        Me._pODraftHeader.MarkLoaded()

                    End If

                    Return Me._pODraftHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PODraftHeader)

                Me._pODraftHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pODraftHeader.MarkLoaded()
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

