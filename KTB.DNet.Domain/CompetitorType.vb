#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CompetitorType Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/14/2007 - 2:24:54 PM
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
    <Serializable(), TableInfo("CompetitorType")> _
    Public Class CompetitorType
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
        Private _code As String = String.Empty
        Private _description As String = String.Empty
        Private _status As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _vehicleClass As VehicleClass
        Private _competitorBrand As CompetitorBrand

        Private _marketPrices As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
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


        <ColumnInfo("VehicleClassID", "{0}"), _
        RelationInfo("VehicleClass", "ID", "CompetitorType", "VehicleClassID")> _
        Public Property VehicleClass() As VehicleClass
            Get
                Try
                    If Not isnothing(Me._vehicleClass) AndAlso (Not Me._vehicleClass.IsLoaded) Then

                        Me._vehicleClass = CType(DoLoad(GetType(VehicleClass).ToString(), _vehicleClass.ID), VehicleClass)
                        Me._vehicleClass.MarkLoaded()

                    End If

                    Return Me._vehicleClass

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VehicleClass)

                Me._vehicleClass = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vehicleClass.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CompetitorBrandID", "{0}"), _
        RelationInfo("CompetitorBrand", "ID", "CompetitorType", "CompetitorBrandID")> _
        Public Property CompetitorBrand() As CompetitorBrand
            Get
                Try
                    If Not isnothing(Me._competitorBrand) AndAlso (Not Me._competitorBrand.IsLoaded) Then

                        Me._competitorBrand = CType(DoLoad(GetType(CompetitorBrand).ToString(), _competitorBrand.ID), CompetitorBrand)
                        Me._competitorBrand.MarkLoaded()

                    End If

                    Return Me._competitorBrand

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CompetitorBrand)

                Me._competitorBrand = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._competitorBrand.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("CompetitorType", "ID", "MarketPrice", "CompetitorTypeID")> _
        Public ReadOnly Property MarketPrices() As System.Collections.ArrayList
            Get
                Try
                    If (Me._marketPrices.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MarketPrice), "CompetitorType", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MarketPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._marketPrices = DoLoadArray(GetType(MarketPrice).ToString, criterias)
                    End If

                    Return Me._marketPrices

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
