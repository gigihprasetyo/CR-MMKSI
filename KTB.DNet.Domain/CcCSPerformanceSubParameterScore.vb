
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceSubParameterScore Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 5/18/2018 - 2:02:08 PM
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
    <Serializable(), TableInfo("CcCSPerformanceSubParameterScore")> _
    Public Class CcCSPerformanceSubParameterScore
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
        Private _ccPeriodID As Integer
        Private _dealerID As Integer
        Private _ccCustomerCategoryID As Integer
        Private _ccVehicleCategoryID As Integer
        Private _ccCSPerformanceSubParameterCode As String = String.Empty
        Private _subFunction As String = String.Empty
        Private _parameterScore As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)



        Private _statusUpload As String

        Private _dealer As Dealer
        Private _ccPeriod As CcPeriod
        Private _ccCustomerCategory As CcCustomerCategory
        Private _ccVehicleCategory As CcVehicleCategory
        Private _ccCSPerformanceSubParameter As CcCSPerformanceSubParameter

#End Region

#Region "Public Properties"

        Public Property StatusUpload As String
            Get
                Return _statusUpload
            End Get
            Set(ByVal value As String)
                _statusUpload = value
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("CcPeriodID", "{0}")> _
        Public Property CcPeriodID As Integer
            Get
                Return _ccPeriodID
            End Get
            Set(ByVal value As Integer)
                _ccPeriodID = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID As Integer
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Integer)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("CcCustomerCategoryID", "{0}")> _
        Public Property CcCustomerCategoryID As Integer
            Get
                Return _ccCustomerCategoryID
            End Get
            Set(ByVal value As Integer)
                _ccCustomerCategoryID = value
            End Set
        End Property


        <ColumnInfo("CcVehicleCategoryID", "{0}")> _
        Public Property CcVehicleCategoryID As Integer
            Get
                Return _ccVehicleCategoryID
            End Get
            Set(ByVal value As Integer)
                _ccVehicleCategoryID = value
            End Set
        End Property


        <ColumnInfo("CcCSPerformanceSubParameterCode", "'{0}'")> _
        Public Property CcCSPerformanceSubParameterCode As String
            Get
                Return _ccCSPerformanceSubParameterCode
            End Get
            Set(ByVal value As String)
                _ccCSPerformanceSubParameterCode = value
            End Set
        End Property

        <ColumnInfo("SubFunction", "'{0}'")> _
        Public Property SubFunction As String
            Get
                Return _subFunction
            End Get
            Set(ByVal value As String)
                _subFunction = value
            End Set
        End Property


        <ColumnInfo("ParameterScore", "#,##0")> _
        Public Property ParameterScore As Decimal
            Get
                Return _parameterScore
            End Get
            Set(ByVal value As Decimal)
                _parameterScore = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "CcCSPerformanceSubParameterScore", "DealerID")> _
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



        <ColumnInfo("CcPeriodID", "{0}"), _
        RelationInfo("CcPeriod", "ID", "CcCSPerformanceSubParameterScore", "CcPeriodID")> _
        Public Property CcPeriod() As CcPeriod
            Get
                Try
                    If Not IsNothing(Me._ccPeriod) AndAlso (Not Me._ccPeriod.IsLoaded) Then

                        Me._ccPeriod = CType(DoLoad(GetType(CcPeriod).ToString(), _ccPeriod.ID), CcPeriod)
                        Me._ccPeriod.MarkLoaded()

                    End If

                    Return Me._ccPeriod

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcPeriod)

                Me._ccPeriod = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccPeriod.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("CcCustomerCategoryID", "{0}"), _
        RelationInfo("CcCustomerCategory", "ID", "CcCSPerformanceSubParameterScore", "CcCustomerCategoryID")> _
        Public Property CcCustomerCategory() As CcCustomerCategory
            Get
                Try
                    If Not IsNothing(Me._ccCustomerCategory) AndAlso (Not Me._ccCustomerCategory.IsLoaded) Then

                        Me._ccCustomerCategory = CType(DoLoad(GetType(CcCustomerCategory).ToString(), _ccCustomerCategory.ID), CcCustomerCategory)
                        Me._ccCustomerCategory.MarkLoaded()

                    End If

                    Return Me._ccCustomerCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCustomerCategory)

                Me._ccCustomerCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCustomerCategory.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("CcVehicleCategoryID", "{0}"), _
        RelationInfo("CcVehicleCategory", "ID", "CcCSPerformanceSubParameterScore", "CcVehicleCategoryID")> _
        Public Property CcVehicleCategory() As CcVehicleCategory
            Get
                Try
                    If Not IsNothing(Me._ccVehicleCategory) AndAlso (Not Me._ccVehicleCategory.IsLoaded) Then

                        Me._ccVehicleCategory = CType(DoLoad(GetType(CcVehicleCategory).ToString(), _ccVehicleCategory.ID), CcVehicleCategory)
                        Me._ccVehicleCategory.MarkLoaded()

                    End If

                    Return Me._ccVehicleCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcVehicleCategory)

                Me._ccVehicleCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccVehicleCategory.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("CcCSPerformanceSubParameterCode", "{0}"), _
        RelationInfo("CcCSPerformanceSubParameter", "ID", "CcCSPerformanceSubParameterScore", "CcCSPerformanceSubParameterCode")> _
        Public Property CcCSPerformanceSubParameter() As CcCSPerformanceSubParameter
            Get
                Try
                    If IsNothing(Me._ccCSPerformanceSubParameter) Then

                        Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "ID", MatchType.Exact, Me.CcCSPerformanceSubParameterCode))
                        Dim arrayListSubParameter As ArrayList = DoLoadArray(GetType(CcCSPerformanceSubParameter).ToString, crit)

                        If (arrayListSubParameter.Count > 0) Then
                            Me._ccCSPerformanceSubParameter = CType(arrayListSubParameter(0), CcCSPerformanceSubParameter)
                            Me._ccCSPerformanceSubParameter.MarkLoaded()
                        Else
                            Me._ccCSPerformanceSubParameter = Nothing
                        End If

                    End If

                    Return Me._ccCSPerformanceSubParameter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceSubParameter)

                Me._ccCSPerformanceSubParameter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCSPerformanceSubParameter.MarkLoaded()
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

