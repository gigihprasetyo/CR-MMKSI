
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PartIncidentalDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 3:52:05 PM
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
    <Serializable(), TableInfo("PartIncidentalDetail")> _
    Public Class PartIncidentalDetail
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
        Private _quantity As Integer
        Private _status As Integer
        Private _statusDetail As Integer
        Private _chassisNumber As String = String.Empty
        Private _assemblyYear As String = String.Empty
        Private _remark As String = String.Empty
        Private _sparePartMasterSubstitutionID As Integer
        Private _planDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _reject As Integer
        Private _alokasi As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _partIncidentalHeader As PartIncidentalHeader
        Private _sparePartMaster As SparePartMaster

        Private _partIncidentalPOs As System.Collections.ArrayList = New System.Collections.ArrayList


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

        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity() As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
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

        <ColumnInfo("StatusDetail", "{0}")> _
        Public Property StatusDetail() As Integer
            Get
                Return _statusDetail
            End Get
            Set(ByVal value As Integer)
                _statusDetail = value
            End Set
        End Property

        <ColumnInfo("ChassisNumber", "{0}")> _
        Public Property ChassisNumber() As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("AssemblyYear", "{0}")> _
        Public Property AssemblyYear() As String
            Get
                Return _assemblyYear
            End Get
            Set(ByVal value As String)
                _assemblyYear = value
            End Set
        End Property

        <ColumnInfo("Remark", "'{0}'")> _
        Public Property Remark() As String
            Get
                Return _remark
            End Get
            Set(ByVal value As String)
                _remark = value
            End Set
        End Property

        <ColumnInfo("SparePartMasterSubstitutionID", "{0}")> _
        Public Property SparePartMasterSubstitutionID() As Integer
            Get
                Return _sparePartMasterSubstitutionID
            End Get
            Set(ByVal value As Integer)
                _sparePartMasterSubstitutionID = value
            End Set
        End Property

        <ColumnInfo("PlanDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanDate() As DateTime
            Get
                Return _planDate
            End Get
            Set(ByVal value As DateTime)
                _planDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("Reject", "{0}")> _
        Public Property Reject() As Integer
            Get
                Return _reject
            End Get
            Set(ByVal value As Integer)
                _reject = value
            End Set
        End Property

        <ColumnInfo("Alokasi", "{0}")> _
        Public Property Alokasi() As Integer
            Get
                Return _alokasi
            End Get
            Set(ByVal value As Integer)
                _alokasi = value
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

        <ColumnInfo("PartIncidentalHeaderID", "{0}"), _
        RelationInfo("PartIncidentalHeader", "ID", "PartIncidentalDetail", "PartIncidentalHeaderID")> _
        Public Property PartIncidentalHeader() As PartIncidentalHeader
            Get
                Try
                    If Not IsNothing(Me._partIncidentalHeader) AndAlso (Not Me._partIncidentalHeader.IsLoaded) Then

                        Me._partIncidentalHeader = CType(DoLoad(GetType(PartIncidentalHeader).ToString(), _partIncidentalHeader.ID), PartIncidentalHeader)
                        Me._partIncidentalHeader.MarkLoaded()

                    End If

                    Return Me._partIncidentalHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PartIncidentalHeader)

                Me._partIncidentalHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._partIncidentalHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "PartIncidentalDetail", "SparePartMasterID")> _
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

        <RelationInfo("PartIncidentalDetail", "ID", "PartIncidentalPO", "PartIncidentalDetailID")> _
        Public ReadOnly Property PartIncidentalPOs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._partIncidentalPOs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PartIncidentalPO), "PartIncidentalDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PartIncidentalPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._partIncidentalPOs = DoLoadArray(GetType(PartIncidentalPO).ToString, criterias)
                    End If

                    Return Me._partIncidentalPOs

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
        Public ReadOnly Property RemainQuantity() As Integer
            Get
                'Dim _remainQuantity As Integer
                'Better use aggregate
                'For Each item As PartIncidentalPO In PartIncidentalPOs
                '    If Not item.RowStatus < 0 Then
                '        _remainQuantity += item.Alocation
                '    End If
                'Next
                'Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPO), "RowStatus", MatchType.GreaterOrEqual, 0))
                'criterias.opAnd(New Criteria(GetType(PartIncidentalPO), "PartIncidentalDetail", Me.ID))
                'Dim agg As Aggregate = New Aggregate(GetType(PartIncidentalPO), "Alocation", AggregateType.Sum)

                '_remainQuantity = DoLoadScalar(GetType(PartIncidentalPO).ToString(), agg, criterias)

                Return (Quantity - AlocatedQuantity) - _alokasi
            End Get
        End Property

        Private _alocatedQuantity As Integer = 0
        Public ReadOnly Property AlocatedQuantity() As Integer
            Get
                If _alocatedQuantity = 0 Then
                    Dim _Quantity As Integer = 0
                    'Better use aggregate
                    'For Each item As PartIncidentalPO In PartIncidentalPOs
                    '    If Not item.RowStatus < 0 Then
                    '        _Quantity += item.Alocation
                    '    End If
                    'Next
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPO), "RowStatus", MatchType.GreaterOrEqual, 0))
                    criterias.opAnd(New Criteria(GetType(PartIncidentalPO), "PartIncidentalDetail", Me.ID))
                    Dim agg As Aggregate = New Aggregate(GetType(PartIncidentalPO), "Alocation", AggregateType.Sum)

                    _alocatedQuantity = DoLoadScalar(GetType(PartIncidentalPO).ToString(), agg, criterias)

                    'Else
                End If
                Return _alocatedQuantity
            End Get
        End Property
#End Region

    End Class
End Namespace

