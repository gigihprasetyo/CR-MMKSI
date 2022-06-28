#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EstimationEquipDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/18/2009 - 10:33:05
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
    <Serializable(), TableInfo("EstimationEquipDetail")> _
    Public Class EstimationEquipDetail
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
        Private _harga As Decimal
        Private _discount As Decimal
        Private _totalForecast As Integer
        Private _estimationUnit As Integer
        Private _status As Byte
        Private _confirmedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _remark As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _estimationEquipHeaderID As Integer
        Private _sparePartMasterID As Integer

        Private _estimationEquipHeader As EstimationEquipHeader
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


        <ColumnInfo("Harga", "{0}")> _
        Public Property Harga() As Decimal
            Get
                Return _harga
            End Get
            Set(ByVal value As Decimal)
                _harga = value
            End Set
        End Property

        <ColumnInfo("Discount", "#,##0")> _
        Public Property Discount() As Decimal
            Get
                Return _discount
            End Get
            Set(ByVal value As Decimal)
                _discount = value
            End Set
        End Property

        <ColumnInfo("EstimationUnit", "{0}")> _
        Public Property EstimationUnit() As Integer
            Get
                Return _estimationUnit
            End Get
            Set(ByVal value As Integer)
                _estimationUnit = value
            End Set
        End Property

        <ColumnInfo("TotalForecast", "{0}")> _
        Public Property TotalForecast() As Integer
            Get
                Return _totalForecast
            End Get
            Set(ByVal value As Integer)
                _totalForecast = value
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


        <ColumnInfo("ConfirmedDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ConfirmedDate() As DateTime
            Get
                Return _confirmedDate
            End Get
            Set(ByVal value As DateTime)
                _confirmedDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy() As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime() As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property

        '<ColumnInfo("EstimationEquipHeaderID", "{0}")> _
        'Public Property EstimationEquipHeaderID() As Integer
        '    Get
        '        Return _estimationEquipHeaderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _estimationEquipHeaderID = value
        '    End Set
        'End Property


        <ColumnInfo("EstimationEquipHeaderID", "{0}"), _
        RelationInfo("EstimationEquipHeader", "ID", "EstimationEquipDetail", "EstimationEquipHeaderID")> _
        Public Property EstimationEquipHeader() As EstimationEquipHeader
            Get
                Try
                    If Not IsNothing(Me._estimationEquipHeader) AndAlso (Not Me._estimationEquipHeader.IsLoaded) Then

                        Me._estimationEquipHeader = CType(DoLoad(GetType(EstimationEquipHeader).ToString(), _estimationEquipHeader.ID), EstimationEquipHeader)
                        Me._estimationEquipHeader.MarkLoaded()

                    End If

                    'If IsNothing(Me._estimationEquipHeader) Then

                    '    Me._estimationEquipHeader = CType(DoLoad(GetType(EstimationEquipHeader).ToString(), _estimationEquipHeaderID), EstimationEquipHeader)
                    '    Me._estimationEquipHeader.MarkLoaded()
                    'End If

                    Return Me._estimationEquipHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
            Set(ByVal Value As EstimationEquipHeader)
                _estimationEquipHeader = Value
                If (Not IsNothing(Value)) AndAlso (CType(Value, DomainObject)).IsLoaded Then
                    Me._estimationEquipHeader.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("SparePartMasterID", "{0}")> _
        'Public Property SparePartMasterID() As Integer
        '    Get
        '        Return _sparePartMasterID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sparePartMasterID = value
        '    End Set
        'End Property

        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "EstimationEquipDetail", "SparePartMasterID")> _
        Public Property SparePartMaster() As SparePartMaster
            Get
                Try
                    'If IsNothing(Me._sparePartMaster) Then

                    '    Me._sparePartMaster = CType(DoLoad(GetType(SparePartMaster).ToString(), _sparePartMasterID), SparePartMaster)
                    '    Me._sparePartMaster.MarkLoaded()

                    'End If

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
            Set(ByVal Value As SparePartMaster)
                _sparePartMaster = Value
            End Set
        End Property

        Dim _estimationEquipPO As ArrayList = New ArrayList
        <RelationInfo("EstimationEquipDetail", "ID", "EstimationEquipPO", "EstimationEquipDetailID")> _
        Public Property EstimationEquipPO() As System.Collections.ArrayList
            Get
                Try
                    If (Me._estimationEquipPO.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EstimationEquipPO), "EstimationEquipDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EstimationEquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._estimationEquipPO = DoLoadArray(GetType(EstimationEquipPO).ToString, criterias)
                    End If

                    Return Me._estimationEquipPO

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get

            Set(ByVal Value As System.Collections.ArrayList)
                Me._estimationEquipPO = Value
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

