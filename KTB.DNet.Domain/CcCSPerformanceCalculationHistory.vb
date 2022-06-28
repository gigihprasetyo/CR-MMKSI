
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceCalculationHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 29/03/2020 - 21:10:20
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
    <Serializable(), TableInfo("CcCSPerformanceCalculationHistory")> _
    Public Class CcCSPerformanceCalculationHistory
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
        Private _ccCSPerformanceMaster As CcCSPerformanceMaster
        Private _ccPeriod As CcPeriod
        Private _ccCSPerformanceCluster As CcCSPerformanceCluster
        Private _requestedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _processedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("CcCSPerformanceMasterID", "{0}"), _
        RelationInfo("CcCSPerformanceMaster", "ID", "CcCSPerformanceCalculationHistory", "CcCSPerformanceMasterID")> _
        Public Property CcCSPerformanceMaster As CcCSPerformanceMaster
            Get
                Try
                    If Not IsNothing(Me._ccCSPerformanceMaster) AndAlso (Not Me._ccCSPerformanceMaster.IsLoaded) Then

                        Me._ccCSPerformanceMaster = CType(DoLoad(GetType(CcCSPerformanceMaster).ToString(), _ccCSPerformanceMaster.ID), CcCSPerformanceMaster)
                        Me._ccCSPerformanceMaster.MarkLoaded()

                    End If

                    Return Me._ccCSPerformanceMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceMaster)

                Me._ccCSPerformanceMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCSPerformanceMaster.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("CcPeriodID", "{0}"), _
        RelationInfo("CcPeriod", "ID", "CcCSPerformanceCalculationHistory", "CcPeriodID")> _
        Public Property CcPeriod As CcPeriod
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

        <ColumnInfo("ClusterID", "{0}"), _
        RelationInfo("CcCSPerformanceCluster", "ID", "CcCSPerformanceCalculationHistory", "ClusterID")> _
        Public Property CcCSPerformanceCluster As CcCSPerformanceCluster
            Get
                Try
                    If Not IsNothing(Me._ccCSPerformanceCluster) AndAlso (Not Me._ccCSPerformanceCluster.IsLoaded) Then

                        Me._ccCSPerformanceCluster = CType(DoLoad(GetType(CcCSPerformanceCluster).ToString(), _ccCSPerformanceCluster.ID), CcCSPerformanceCluster)
                        Me._ccCSPerformanceCluster.MarkLoaded()

                    End If

                    Return Me._ccCSPerformanceCluster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCSPerformanceCluster)

                Me._ccCSPerformanceCluster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCSPerformanceCluster.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("RequestedDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property RequestedDate As DateTime
            Get
                Return _requestedDate
            End Get
            Set(ByVal value As DateTime)
                _requestedDate = value
            End Set
        End Property


        <ColumnInfo("ProcessedDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ProcessedDate As DateTime
            Get
                Return _processedDate
            End Get
            Set(ByVal value As DateTime)
                _processedDate = value
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

