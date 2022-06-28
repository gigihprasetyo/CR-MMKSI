
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceCluster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/03/2020 - 8:42:36
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
    <Serializable(), TableInfo("CcCSPerformanceCluster")> _
    Public Class CcCSPerformanceCluster
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
        Private _ccCSPerformanceMasterID As Integer
        Private _ccCSPerformanceMaster As CcCSPerformanceMaster
        Private _clusterName As String = String.Empty
        Private _DealerType As String = String.Empty
        Private _VehicleType As String = String.Empty
        Private _startPeriodCal As Integer
        Private _endPeriodCal As Integer
        Private _minPoint As Integer
        Private _maxPoint As Integer
        Private _typeCal As Short
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


        <ColumnInfo("CcCSPerformanceMasterID", "{0}")> _
        Public Property CcCSPerformanceMasterID As Integer
            Get
                Return _ccCSPerformanceMasterID
            End Get
            Set(ByVal value As Integer)
                _ccCSPerformanceMasterID = value
            End Set
        End Property

        <ColumnInfo("CcCSPerformanceMasterID", "{0}"), _
        RelationInfo("CcCSPerformanceMaster", "ID", "CcCSPerformanceCluster", "CcCSPerformanceMasterID")> _
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


        <ColumnInfo("ClusterName", "'{0}'")> _
        Public Property ClusterName As String
            Get
                Return _clusterName
            End Get
            Set(ByVal value As String)
                _clusterName = value
            End Set
        End Property


        <ColumnInfo("StartPeriodCal", "{0}")> _
        Public Property StartPeriodCal As Integer
            Get
                Return _startPeriodCal
            End Get
            Set(ByVal value As Integer)
                _startPeriodCal = value
            End Set
        End Property


        <ColumnInfo("EndPeriodCal", "{0}")> _
        Public Property EndPeriodCal As Integer
            Get
                Return _endPeriodCal
            End Get
            Set(ByVal value As Integer)
                _endPeriodCal = value
            End Set
        End Property


        <ColumnInfo("MinPoint", "{0}")> _
        Public Property MinPoint As Integer
            Get
                Return _minPoint
            End Get
            Set(ByVal value As Integer)
                _minPoint = value
            End Set
        End Property


        <ColumnInfo("MaxPoint", "{0}")> _
        Public Property MaxPoint As Integer
            Get
                Return _maxPoint
            End Get
            Set(ByVal value As Integer)
                _maxPoint = value
            End Set
        End Property


        <ColumnInfo("TypeCal", "{0}")> _
        Public Property TypeCal As Short
            Get
                Return _typeCal
            End Get
            Set(ByVal value As Short)
                _typeCal = value
            End Set
        End Property

        <ColumnInfo("VehicleType", "'{0}'")> _
        Public Property VehicleType As String
            Get
                Return _VehicleType
            End Get
            Set(ByVal value As String)
                _VehicleType = value
            End Set
        End Property

        <ColumnInfo("DealerType", "'{0}'")> _
        Public Property DealerType As String
            Get
                Return _DealerType
            End Get
            Set(ByVal value As String)
                _DealerType = value
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

