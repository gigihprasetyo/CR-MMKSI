#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionAllocation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 04/08/2007 - 11:38
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
    <Serializable(), TableInfo("MaterialPromotionAllocation")> _
    Public Class MaterialPromotionAllocation
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
        Private _qty As Integer
        Private _validateQty As Integer

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _materialPromotionPeriod As MaterialPromotionPeriod
        Private _materialPromotion As MaterialPromotion
        Private _dealer As Dealer
        Private _TempRequestNo As String
        Private _TempRequestQty As Integer



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


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty() As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property


        <ColumnInfo("ValidateQty", "{0}")> _
        Public Property ValidateQty() As Integer
            Get
                Return _validateQty
            End Get
            Set(ByVal value As Integer)
                _validateQty = value
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


        <ColumnInfo("MaterialPromotionPeriodID", "{0}"), _
        RelationInfo("MaterialPromotionPeriod", "ID", "MaterialPromotionAllocation", "MaterialPromotionPeriodID")> _
        Public Property MaterialPromotionPeriod() As MaterialPromotionPeriod
            Get
                Try
                    If Not IsNothing(Me._materialPromotionPeriod) AndAlso (Not Me._materialPromotionPeriod.IsLoaded) Then

                        Me._materialPromotionPeriod = CType(DoLoad(GetType(MaterialPromotionPeriod).ToString(), _materialPromotionPeriod.ID), MaterialPromotionPeriod)
                        Me._materialPromotionPeriod.MarkLoaded()

                    End If

                    Return Me._materialPromotionPeriod

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MaterialPromotionPeriod)

                Me._materialPromotionPeriod = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._materialPromotionPeriod.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MaterialPromotionID", "{0}"), _
        RelationInfo("MaterialPromotion", "ID", "MaterialPromotionAllocation", "MaterialPromotionID")> _
        Public Property MaterialPromotion() As MaterialPromotion
            Get
                Try
                    If Not IsNothing(Me._materialPromotion) AndAlso (Not Me._materialPromotion.IsLoaded) Then

                        Me._materialPromotion = CType(DoLoad(GetType(MaterialPromotion).ToString(), _materialPromotion.ID), MaterialPromotion)
                        Me._materialPromotion.MarkLoaded()

                    End If

                    Return Me._materialPromotion

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MaterialPromotion)

                Me._materialPromotion = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._materialPromotion.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "MaterialPromotionAllocation", "DealerID")> _
        Public Property Dealer() As Dealer
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

#Region "Custom Properties"

        Public Property TempRequestQty() As Integer
            Get
                Return _TempRequestQty
            End Get
            Set(ByVal value As Integer)
                _TempRequestQty = value
            End Set
        End Property

        Public Property TempRequestNo() As String
            Get
                Return _TempRequestNo
            End Get
            Set(ByVal value As String)
                _TempRequestNo = value
            End Set
        End Property

        Public ReadOnly Property QtyGI() As Integer
            Get
                Dim _QtyGI As Integer

                If Me.ID = 0 Then
                    Return 0
                End If

                Dim agg As Aggregate = New Aggregate(GetType(MaterialPromotionGIGR), "Qty", AggregateType.Sum)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "MaterialPromotion.ID", MatchType.Exact, Me.MaterialPromotion.ID))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "MaterialPromotionPeriod.ID", MatchType.Exact, Me.MaterialPromotionPeriod.ID))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Status", MatchType.No, CInt(EnumStatusMatPromotion.StatusMatPromotion.Batal)))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Status", MatchType.No, CInt(EnumStatusMatPromotion.StatusMatPromotion.Ditolak)))


                'agg = New Aggregate(GetType(MaterialPromotionGIGR), "Qty", AggregateType.Sum)
                _QtyGI = DoLoadScalar(GetType(MaterialPromotionGIGR).ToString(), agg, criterias)


                Return _QtyGI
            End Get
        End Property

        Public ReadOnly Property QtySisa() As Integer
            Get
                Dim _QtySisa As Integer

                If Me.ID = 0 Then
                    Return 0
                End If
                _QtySisa = Me.ValidateQty - Me.QtyGI
                Return _QtySisa
            End Get

        End Property

        Public ReadOnly Property StatusGI() As String
            Get
                If Me.Qty = 0 Then
                    Return "Belum Good Issue"
                Else
                    If Me.QtySisa = 0 Then
                        Return "Good Issue"
                    ElseIf Me.QtySisa = Me.Qty Then
                        Return "Belum Good Issue"
                    Else
                        Return "Partial GI"
                    End If
                End If
            End Get
        End Property

#End Region

    End Class
End Namespace

