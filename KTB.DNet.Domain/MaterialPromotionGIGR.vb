#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionGIGR Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 11/08/2007 - 22:26
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
    <Serializable(), TableInfo("MaterialPromotionGIGR")> _
    Public Class MaterialPromotionGIGR
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
        Private _requestNo As String = String.Empty
        Private _qty As Integer
        Private _noGI As String = String.Empty
        Private _noGR As String = String.Empty
        Private _status As Byte
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _statusGI As Integer

        Private _dealer As Dealer
        Private _materialPromotion As MaterialPromotion
        Private _materialPromotionPeriod As MaterialPromotionPeriod



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


        <ColumnInfo("RequestNo", "'{0}'")> _
        Public Property RequestNo() As String
            Get
                Return _requestNo
            End Get
            Set(ByVal value As String)
                _requestNo = value
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


        <ColumnInfo("NoGI", "'{0}'")> _
        Public Property NoGI() As String
            Get
                Return _noGI
            End Get
            Set(ByVal value As String)
                _noGI = value
            End Set
        End Property


        <ColumnInfo("NoGR", "'{0}'")> _
        Public Property NoGR() As String
            Get
                Return _noGR
            End Get
            Set(ByVal value As String)
                _noGR = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "MaterialPromotionGIGR", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MaterialPromotionID", "{0}"), _
        RelationInfo("MaterialPromotion", "ID", "MaterialPromotionGIGR", "MaterialPromotionID")> _
        Public Property MaterialPromotion() As MaterialPromotion
            Get
                Try
                    If Not isnothing(Me._materialPromotion) AndAlso (Not Me._materialPromotion.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._materialPromotion.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("MaterialPromotionPeriodID", "{0}"), _
        RelationInfo("MaterialPromotionPeriod", "ID", "MaterialPromotionGIGR", "MaterialPromotionPeriodID")> _
        Public Property MaterialPromotionPeriod() As MaterialPromotionPeriod
            Get
                Try
                    If Not isnothing(Me._materialPromotionPeriod) AndAlso (Not Me._materialPromotionPeriod.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._materialPromotionPeriod.MarkLoaded()
                End If
            End Set
        End Property

        Public Property StatusGI() As Integer
            Get
                Return _statusGI

            End Get
            Set(ByVal Value As Integer)
                _statusGI = Value
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

