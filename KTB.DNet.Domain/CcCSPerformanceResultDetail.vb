
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcAttribute Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/03/2020 - 10:58:34
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
    <Serializable(), TableInfo("CcCSPerformanceResultDetail")> _
    Public Class CcCSPerformanceResultDetail
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Short)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Short
        Private _ccCsPerformanceMaster As CcCSPerformanceMaster
        Private _ccPeriod As CcPeriod
        Private _ccCustomerCategory As CcCustomerCategory
        Private _dealer As Dealer
        Private _CcCSPerformanceCluster As CcCSPerformanceCluster
        Private _CcCSPerformanceParameter As CcCSPerformanceParameter
        Private _CcCSPerformanceSubParameter As CcCSPerformanceSubParameter
        Private _totalScore As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Short
            Get
                Return _iD
            End Get
            Set(ByVal value As Short)
                _iD = value
            End Set
        End Property

        <ColumnInfo("CcPeriodID", "{0}"), _
    RelationInfo("CcPeriod", "ID", "CcCSPerformanceResultDetail", "CcPeriodID")> _
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

        <ColumnInfo("CcCsPerformanceMasterID", "{0}"), _
   RelationInfo("CcCsPerformanceMaster", "ID", "CcCSPerformanceResultDetail", "CcCsPerformanceMasterID")> _
        Public Property CcCsPerformanceMaster() As CcCSPerformanceMaster
            Get
                Try
                    If Not IsNothing(Me._ccCsPerformanceMaster) AndAlso (Not Me._ccCsPerformanceMaster.IsLoaded) Then

                        Me._ccCsPerformanceMaster = CType(DoLoad(GetType(CcCSPerformanceMaster).ToString(), _ccCsPerformanceMaster.ID), CcCSPerformanceMaster)
                        Me._ccCsPerformanceMaster.MarkLoaded()

                    End If

                    Return Me._ccCsPerformanceMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceMaster)

                Me._ccCsPerformanceMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCsPerformanceMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcCustomerCategoryID", "{0}"), _
  RelationInfo("CcCustomerCategory", "ID", "CcCSPerformanceResultDetail", "CcCustomerCategoryID")> _
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

        <ColumnInfo("DealerID", "{0}"), _
  RelationInfo("Dealer", "ID", "CcCSPerformanceResultDetail", "DealerID")> _
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

        <ColumnInfo("ClusterID", "{0}"), _
 RelationInfo("CcCSPerformanceCluster", "ID", "CcCSPerformanceResultDetail", "ClusterID")> _
        Public Property CcCSPerformanceCluster() As CcCSPerformanceCluster
            Get
                Try
                    If Not IsNothing(Me._CcCSPerformanceCluster) AndAlso (Not Me._CcCSPerformanceCluster.IsLoaded) Then

                        Me._CcCSPerformanceCluster = CType(DoLoad(GetType(CcCSPerformanceCluster).ToString(), _CcCSPerformanceCluster.ID), CcCSPerformanceCluster)
                        Me._CcCSPerformanceCluster.MarkLoaded()

                    End If

                    Return Me._CcCSPerformanceCluster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceCluster)

                Me._CcCSPerformanceCluster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._CcCSPerformanceCluster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcCSPerformanceParameterID", "{0}"), _
RelationInfo("CcCSPerformanceParameter", "ID", "CcCSPerformanceResultDetail", "CcCSPerformanceParameterID")> _
        Public Property CcCSPerformanceParameter() As CcCSPerformanceParameter
            Get
                Try
                    If Not IsNothing(Me._CcCSPerformanceParameter) AndAlso (Not Me._CcCSPerformanceParameter.IsLoaded) Then

                        Me._CcCSPerformanceParameter = CType(DoLoad(GetType(CcCSPerformanceParameter).ToString(), _CcCSPerformanceParameter.ID), CcCSPerformanceParameter)
                        Me._CcCSPerformanceParameter.MarkLoaded()

                    End If

                    Return Me._CcCSPerformanceParameter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceParameter)

                Me._CcCSPerformanceParameter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._CcCSPerformanceParameter.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcCSPerformanceSubParameterID", "{0}"), _
RelationInfo("CcCSPerformanceSubParameter", "ID", "CcCSPerformanceResultDetail", "CcCSPerformanceSubParameterID")> _
        Public Property CcCSPerformanceSubParameter() As CcCSPerformanceSubParameter
            Get
                Try
                    If Not IsNothing(Me._CcCSPerformanceSubParameter) AndAlso (Not Me._CcCSPerformanceSubParameter.IsLoaded) Then

                        Me._CcCSPerformanceSubParameter = CType(DoLoad(GetType(CcCSPerformanceSubParameter).ToString(), _CcCSPerformanceSubParameter.ID), CcCSPerformanceSubParameter)
                        Me._CcCSPerformanceSubParameter.MarkLoaded()

                    End If

                    Return Me._CcCSPerformanceSubParameter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceSubParameter)

                Me._CcCSPerformanceSubParameter = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._CcCSPerformanceSubParameter.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TotalScore", "{0,0}")> _
        Public Property TotalScore As Decimal
            Get
                Return _totalScore
            End Get
            Set(ByVal value As Decimal)
                _totalScore = value
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


