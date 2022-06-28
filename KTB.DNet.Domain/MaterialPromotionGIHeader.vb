#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionGIHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/7/2007 - 10:21:17 AM
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
    <Serializable(), TableInfo("MaterialPromotionGIHeader")> _
    Public Class MaterialPromotionGIHeader
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
        Private _gINo As String = String.Empty
        Private _gIDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _materialPromotionPeriod As MaterialPromotionPeriod

        Private _materialPromotionGIDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("GINo", "'{0}'")> _
        Public Property GINo() As String
            Get
                Return _gINo
            End Get
            Set(ByVal value As String)
                _gINo = value
            End Set
        End Property


        <ColumnInfo("GIDate", "'{0:yyyy/MM/dd}'")> _
        Public Property GIDate() As DateTime
            Get
                Return _gIDate
            End Get
            Set(ByVal value As DateTime)
                _gIDate = New DateTime(value.Year, value.Month, value.Day)
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
        RelationInfo("Dealer", "ID", "MaterialPromotionGIHeader", "DealerID")> _
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

        <ColumnInfo("MaterialPromotionPeriodID", "{0}"), _
        RelationInfo("MaterialPromotionPeriod", "ID", "MaterialPromotionGIHeader", "MaterialPromotionPeriodID")> _
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


        <RelationInfo("MaterialPromotionGIHeader", "ID", "MaterialPromotionGIDetail", "MaterialPromotionGIHeaderID")> _
        Public ReadOnly Property MaterialPromotionGIDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._materialPromotionGIDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MaterialPromotionGIDetail), "MaterialPromotionGIHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MaterialPromotionGIDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._materialPromotionGIDetails = DoLoadArray(GetType(MaterialPromotionGIDetail).ToString, criterias)
                    End If

                    Return Me._materialPromotionGIDetails

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

#End Region

    End Class
End Namespace

