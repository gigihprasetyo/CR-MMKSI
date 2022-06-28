
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOStatusDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 24/11/2005 - 10:31:24
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("SparePartPOStatusDetail")> _
    Public Class SparePartPOStatusDetail
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
        Private _sOQuantity As Integer
        Private _billingQuantity As Integer
        Private _netPrice As Decimal
        Private _sOPrice As Decimal
        Private _billingPrice As Decimal
        Private _dONumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartPOStatus As SparePartPOStatus
        Private _sparePartMaster As SparePartMaster



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("SOQuantity", "{0}")> _
        Public Property SOQuantity() As Integer
            Get
                Return _sOQuantity
            End Get
            Set(ByVal value As Integer)
                _sOQuantity = value
            End Set
        End Property


        <ColumnInfo("BillingQuantity", "{0}")> _
        Public Property BillingQuantity() As Integer
            Get
                Return _billingQuantity
            End Get
            Set(ByVal value As Integer)
                _billingQuantity = value
            End Set
        End Property


        <ColumnInfo("NetPrice", "{0}")> _
        Public Property NetPrice() As Decimal
            Get
                Return _netPrice
            End Get
            Set(ByVal value As Decimal)
                _netPrice = value
            End Set
        End Property


        <ColumnInfo("SOPrice", "{0}")> _
        Public Property SOPrice() As Decimal
            Get
                Return _sOPrice
            End Get
            Set(ByVal value As Decimal)
                _sOPrice = value
            End Set
        End Property


        <ColumnInfo("BillingPrice", "{0}")> _
        Public Property BillingPrice() As Decimal
            Get
                Return _billingPrice
            End Get
            Set(ByVal value As Decimal)
                _billingPrice = value
            End Set
        End Property


        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber() As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
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




        <ColumnInfo("SparePartPOStatusID", "{0}"), _
        RelationInfo("SparePartPOStatus", "ID", "SparePartPOStatusDetail", "SparePartPOStatusID")> _
        Public Property SparePartPOStatus() As SparePartPOStatus
            Get
                Try
                    If Not IsNothing(Me._sparePartPOStatus) AndAlso (Not Me._sparePartPOStatus.IsLoaded) Then

                        Me._sparePartPOStatus = CType(DoLoad(GetType(SparePartPOStatus).ToString(), _sparePartPOStatus.ID), SparePartPOStatus)
                        Me._sparePartPOStatus.MarkLoaded()

                    End If

                    Return Me._sparePartPOStatus

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartPOStatus)

                Me._sparePartPOStatus = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartPOStatus.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "SparePartPOStatusDetail", "SparePartMasterID")> _
        Public Property SparePartMaster() As SparePartMaster
            Get
                Try
                    If Not IsNothing(Me._sparePartMaster) AndAlso (Not Me._sparePartMaster.IsLoaded) Then

                        Me._sparePartMaster = CType(DoLoad(GetType(SparePartMaster).ToString(), _sparePartMaster.ID), SparePartMaster)
                        Me._sparePartMaster.MarkLoaded()

                    End If

                    Return Me._sparePartMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartMaster)

                Me._sparePartMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartMaster.MarkLoaded()
                End If
            End Set
        End Property




#End Region

#Region "Custom Properties"

        Public ReadOnly Property ClaimedQty() As Integer
            Get

                If Me.ID = 0 Then
                    Return 0
                End If

                Dim _ClaimedQty As Integer = 0

                Dim criterias As New CriteriaComposite(New Criteria(GetType(ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ClaimDetail), "SparePartPOStatusDetail.ID", MatchType.Exact, Me.ID))
                criterias.opAnd(New Criteria(GetType(ClaimDetail), "StatusDetail", MatchType.No, CInt(EnumClaimStatusDetail.ClaimStatusDetail.Ditolak)))

                Dim agg As Aggregate = New Aggregate(GetType(ClaimDetail), "ApprovedQty", AggregateType.Sum)

                _ClaimedQty = DoLoadScalar(GetType(ClaimDetail).ToString(), agg, criterias)

                Return _ClaimedQty

            End Get
        End Property


        Public ReadOnly Property ClaimPriceUnit() As Decimal
            Get

                If Me.BillingQuantity = 0 Then
                    Return 0
                Else
                    Return (Me.BillingPrice / Me.BillingQuantity)
                End If


            End Get
        End Property

        Private _IsChangedWSM As Boolean

        Public Property IsChangedWSM() As Boolean
            Get
                Return _IsChangedWSM
            End Get
            Set(ByVal value As Boolean)
                _IsChangedWSM = value
            End Set
        End Property


#End Region


        Public Function GetApprovedQty(ByVal ClaimDetailId As Integer) As Integer

            Dim _ClaimedQty As Integer = 0

            Dim criterias As New CriteriaComposite(New Criteria(GetType(ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ClaimDetail), "SparePartPOStatusDetail.ID", MatchType.Exact, Me.ID))
            criterias.opAnd(New Criteria(GetType(ClaimDetail), "StatusDetail", MatchType.No, CInt(EnumClaimStatusDetail.ClaimStatusDetail.Ditolak)))
            criterias.opAnd(New Criteria(GetType(ClaimDetail), "ID", MatchType.No, ClaimDetailId))
            criterias.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim agg As Aggregate = New Aggregate(GetType(ClaimDetail), "ApprovedQty", AggregateType.Sum)

            _ClaimedQty = DoLoadScalar(GetType(ClaimDetail).ToString(), agg, criterias)

            Return _ClaimedQty


        End Function

        Public Function GetClaimedQty(ByVal ClaimDetailId As Integer) As Integer

            Dim _ClaimedQty As Integer = 0

            Dim criterias As New CriteriaComposite(New Criteria(GetType(V_ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_ClaimDetail), "SparePartPOStatusDetail.ID", MatchType.Exact, Me.ID))
            criterias.opAnd(New Criteria(GetType(V_ClaimDetail), "StatusDetail", MatchType.No, CInt(EnumClaimStatusDetail.ClaimStatusDetail.Ditolak)))
            criterias.opAnd(New Criteria(GetType(V_ClaimDetail), "ClaimHeader.ID", MatchType.No, ClaimDetailId))
            criterias.opAnd(New Criteria(GetType(V_ClaimDetail), "ClaimHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim agg As Aggregate = New Aggregate(GetType(V_ClaimDetail), "QtyClaim", AggregateType.Sum)

            _ClaimedQty = DoLoadScalar(GetType(V_ClaimDetail).ToString(), agg, criterias)

            Return _ClaimedQty


        End Function


        Public Function GetClaimedDetail(ByVal ClaimDetailId As Integer) As ArrayList

            Dim cld As New ArrayList

            Dim _ClaimedQty As Integer = 0

            Dim criterias As New CriteriaComposite(New Criteria(GetType(ClaimDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ClaimDetail), "SparePartPOStatusDetail.ID", MatchType.Exact, Me.ID))
            criterias.opAnd(New Criteria(GetType(ClaimDetail), "StatusDetail", MatchType.No, CInt(EnumClaimStatusDetail.ClaimStatusDetail.Ditolak)))
            criterias.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.ID", MatchType.No, ClaimDetailId))
            criterias.opAnd(New Criteria(GetType(ClaimDetail), "ClaimHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            cld = DoLoadArray(GetType(ClaimDetail).ToString(), criterias)

            Return cld


        End Function

    End Class
End Namespace

