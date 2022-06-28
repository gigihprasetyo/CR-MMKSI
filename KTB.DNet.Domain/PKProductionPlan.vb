#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKProductionPlan Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 12/1/2005 - 1:00:22 PM
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
Imports KTB.DNet.DataMapper.Framework
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("PKProductionPlan")> _
    Public Class PKProductionPlan
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Integer)
            _id = id
        End Sub

#End Region

#Region "Private Variables"

        Private _id As Integer
        Private _periodMonth As Short
        Private _periodYear As Short
        Private _productionYear As Short
        Private _carryOverPreviousQty As Integer
        Private _planQty As Integer
        Private _unselledStock As Integer
        Private _allocationQty As Integer
        Private _reserveQty As Integer
        Private _materialNumber As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lasUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _vechileColor As VechileColor
        Private _stokSetelahAlokasi As Integer
        Private _totalDO As Integer
        Private _total As Integer
        Private _sisaStokDNet As Integer
        Private _lineNo As Integer
        Private _totalBaruAndValidasi As Integer
        Private _totalKonfirmasiAndTungguDiskon As Integer
        Private _totalReleaseAndAgree As Integer
        Private _totalTolak As Integer
        Private _totalSelesai As Integer


#End Region

#Region "Public Properties"

        <ColumnInfo("id", "{0}")> _
        Public Property id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property


        <ColumnInfo("PeriodMonth", "{0}")> _
        Public Property PeriodMonth() As Short
            Get
                Return _periodMonth
            End Get
            Set(ByVal value As Short)
                _periodMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodYear", "{0}")> _
        Public Property PeriodYear() As Short
            Get
                Return _periodYear
            End Get
            Set(ByVal value As Short)
                _periodYear = value
            End Set
        End Property


        <ColumnInfo("ProductionYear", "{0}")> _
        Public Property ProductionYear() As Short
            Get
                Return _productionYear
            End Get
            Set(ByVal value As Short)
                _productionYear = value
            End Set
        End Property


        <ColumnInfo("CarryOverPreviousQty", "{0}")> _
        Public Property CarryOverPreviousQty() As Integer
            Get
                Return _carryOverPreviousQty
            End Get
            Set(ByVal value As Integer)
                _carryOverPreviousQty = value
            End Set
        End Property


        <ColumnInfo("PlanQty", "{0}")> _
        Public Property PlanQty() As Integer
            Get
                Return _planQty
            End Get
            Set(ByVal value As Integer)
                _planQty = value
            End Set
        End Property


        <ColumnInfo("UnselledStock", "{0}")> _
        Public Property UnselledStock() As Integer
            Get
                Return _unselledStock
            End Get
            Set(ByVal value As Integer)
                _unselledStock = value
            End Set
        End Property


        <ColumnInfo("AllocationQty", "{0}")> _
        Public Property AllocationQty() As Integer
            Get
                Return _allocationQty
            End Get
            Set(ByVal value As Integer)
                _allocationQty = value
            End Set
        End Property


        <ColumnInfo("ReserveQty", "{0}")> _
        Public Property ReserveQty() As Integer
            Get
                Return _reserveQty
            End Get
            Set(ByVal value As Integer)
                _reserveQty = value
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


        <ColumnInfo("LasUpdateBy", "'{0}'")> _
        Public Property LasUpdateBy() As String
            Get
                Return _lasUpdateBy
            End Get
            Set(ByVal value As String)
                _lasUpdateBy = value
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


        <ColumnInfo("VehicleColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "PKProductionPlan", "VehicleColorID")> _
        Public Property VechileColor() As VechileColor
            Get
                Try
                    If Not isnothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then

                        Me._vechileColor = CType(DoLoad(GetType(VechileColor).ToString(), _vechileColor.ID), VechileColor)
                        Me._vechileColor.MarkLoaded()

                    End If

                    Return Me._vechileColor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColor)

                Me._vechileColor = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
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
        Private _CriteriaStatus As Criteria
        Public Property CriteriaStatus() As Criteria
            Get
                Return _CriteriaStatus
            End Get
            Set(ByVal Value As Criteria)
                _CriteriaStatus = Value
            End Set
        End Property

        Public ReadOnly Property TotalPermintaan() As Integer
            Get
                'Todo Aggregate
                Dim _total As Integer
                If Not Me.VechileColor Is Nothing Then
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Baru, Integer) & "," & CType(enumStatusPK.Status.Validasi, Integer) & "," & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Selesai, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & ")"))
                    If Not Me.CriteriaStatus Is Nothing Then
                        criterias.opAnd(Me.CriteriaStatus)
                    End If
                    criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.ID", MatchType.Exact, Me.VechileColor.ID))
                    criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, Me.PeriodMonth))
                    criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, Me.PeriodYear))
                    criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.ProductionYear", MatchType.Exact, Me.ProductionYear))
                    Dim m_PKDetailMapper As IMapper
                    m_PKDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PKDetail).ToString)
                    Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias)
                    If (PKDetailColl.Count > 0) Then
                        For Each item As PKDetail In PKDetailColl
                            _total = _total + CType(item.TargetQty, Integer)
                        Next
                    End If
                Else
                    _total = 0
                End If
                Return _total
            End Get

        End Property

        'Public ReadOnly Property TotalBaruAndValidasi() As Integer
        '    Get
        '        'Todo Aggregate
        '        Dim _total As Integer = 0
        '        If Not Me.VechileColor Is Nothing Then
        '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Baru, Integer) & "," & CType(enumStatusPK.Status.Validasi, Integer) & ")"))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.ID", MatchType.Exact, Me.VechileColor.ID))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, Me.PeriodMonth))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, Me.PeriodYear))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.ProductionYear", MatchType.Exact, Me.ProductionYear))
        '            Dim m_PKDetailMapper As IMapper
        '            m_PKDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PKDetail).ToString)
        '            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias)
        '            If (PKDetailColl.Count > 0) Then
        '                For Each item As PKDetail In PKDetailColl
        '                    _total = _total + CType(item.TargetQty, Integer)
        '                Next
        '            End If
        '        Else
        '            _total = 0
        '        End If
        '        Return _total
        '    End Get
        'End Property

        'Public ReadOnly Property TotalKonfirmasiAndTungguDiskon() As Integer
        '    Get
        '        'Todo Aggregate
        '        Dim _total As Integer
        '        If Not Me.VechileColor Is Nothing Then
        '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Tunggu_Diskon, Integer) & ")"))

        '            criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.ID", MatchType.Exact, Me.VechileColor.ID))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, Me.PeriodMonth))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, Me.PeriodYear))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.ProductionYear", MatchType.Exact, Me.ProductionYear))
        '            Dim m_PKDetailMapper As IMapper
        '            m_PKDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PKDetail).ToString)
        '            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias)
        '            If (PKDetailColl.Count > 0) Then
        '                For Each item As PKDetail In PKDetailColl
        '                    _total = _total + IIf(CType(item.ResponseQty, Integer) = 0, CType(item.TargetQty, Integer), CType(item.ResponseQty, Integer))
        '                Next
        '            End If
        '        Else
        '            _total = 0
        '        End If
        '        Return _total
        '    End Get
        'End Property

        Public ReadOnly Property TotalAlokasi() As Integer
            Get
                'Todo Aggregate
                Dim _total As Integer
                If Not Me.VechileColor Is Nothing Then
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Selesai, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & ")"))
                    criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.ID", MatchType.Exact, Me.VechileColor.ID))
                    criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, Me.PeriodMonth))
                    criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, Me.PeriodYear))
                    criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.ProductionYear", MatchType.Exact, Me.ProductionYear))
                    Dim m_PKDetailMapper As IMapper
                    m_PKDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PKDetail).ToString)
                    Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias)
                    If (PKDetailColl.Count > 0) Then
                        For Each item As PKDetail In PKDetailColl
                            _total = _total + CType(item.ResponseQty, Integer)
                        Next
                    End If
                Else
                    _total = 0
                End If
                Return _total
            End Get
        End Property

        'Public ReadOnly Property TotalReleaseAndAgree() As Integer
        '    Get
        '        'Todo Aggregate
        '        Dim _total As Integer
        '        If Not Me.VechileColor Is Nothing Then
        '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            'criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Selesai, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & ")"))

        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & ")"))

        '            criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.ID", MatchType.Exact, Me.VechileColor.ID))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, Me.PeriodMonth))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, Me.PeriodYear))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.ProductionYear", MatchType.Exact, Me.ProductionYear))
        '            Dim m_PKDetailMapper As IMapper
        '            m_PKDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PKDetail).ToString)
        '            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias)
        '            If (PKDetailColl.Count > 0) Then
        '                For Each item As PKDetail In PKDetailColl
        '                    _total = _total + CType(item.ResponseQty, Integer)
        '                Next
        '            End If
        '        Else
        '            _total = 0
        '        End If

        '        Return _total
        '    End Get
        'End Property

        'Public ReadOnly Property TotalTolak() As Integer
        '    Get
        '        'Todo Aggregate
        '        Dim _total As Integer
        '        If Not Me.VechileColor Is Nothing Then
        '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.Exact, CType(enumStatusPK.Status.Ditolak, Integer)))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.ID", MatchType.Exact, Me.VechileColor.ID))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, Me.PeriodMonth))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, Me.PeriodYear))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.ProductionYear", MatchType.Exact, Me.ProductionYear))
        '            Dim m_PKDetailMapper As IMapper
        '            m_PKDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PKDetail).ToString)
        '            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias)
        '            If (PKDetailColl.Count > 0) Then
        '                For Each item As PKDetail In PKDetailColl
        '                    _total = _total + IIf(CType(item.ResponseQty, Integer) = 0, CType(item.TargetQty, Integer), CType(item.ResponseQty, Integer))
        '                Next
        '            End If
        '        Else
        '            _total = 0
        '        End If

        '        Return _total
        '    End Get
        'End Property

        'Public ReadOnly Property TotalSelesai() As Integer
        '    Get
        '        'Todo Aggregate
        '        Dim _total As Integer
        '        If Not Me.VechileColor Is Nothing Then
        '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.Exact, CType(enumStatusPK.Status.Selesai, Integer)))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "VechileColor.ID", MatchType.Exact, Me.VechileColor.ID))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, Me.PeriodMonth))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, Me.PeriodYear))
        '            criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.ProductionYear", MatchType.Exact, Me.ProductionYear))
        '            Dim m_PKDetailMapper As IMapper
        '            m_PKDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PKDetail).ToString)
        '            Dim PKDetailColl As ArrayList = m_PKDetailMapper.RetrieveByCriteria(criterias)
        '            If (PKDetailColl.Count > 0) Then
        '                For Each item As PKDetail In PKDetailColl
        '                    _total = _total + CType(item.ResponseQty, Integer)
        '                Next
        '            End If
        '        Else
        '            _total = 0
        '        End If

        '        Return _total
        '    End Get
        'End Property

        Public Property TotalBaruAndValidasi() As Integer
            Get
                Return _totalBaruAndValidasi
            End Get
            Set(ByVal value As Integer)
                _totalBaruAndValidasi = value
            End Set
        End Property

        Public Property TotalKonfirmasiAndTungguDiskon() As Integer
            Get
                Return _totalKonfirmasiAndTungguDiskon
            End Get
            Set(ByVal value As Integer)
                _totalKonfirmasiAndTungguDiskon = value
            End Set
        End Property

        Public Property TotalReleaseAndAgree() As Integer
            Get
                Return _totalReleaseAndAgree
            End Get
            Set(ByVal value As Integer)
                _totalReleaseAndAgree = value
            End Set
        End Property

        Public Property TotalTolak() As Integer
            Get
                Return _totalTolak
            End Get
            Set(ByVal value As Integer)
                _totalTolak = value
            End Set
        End Property

        Public Property TotalSelesai() As Integer
            Get
                Return _totalSelesai
            End Get
            Set(ByVal value As Integer)
                _totalSelesai = value
            End Set
        End Property

        Public Property MaterialNumber() As String
            Get
                Return _materialNumber
            End Get
            Set(ByVal value As String)
                _materialNumber = value
            End Set
        End Property

        Public Property MaterialDescription() As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property
#End Region

#Region "Custom Public Properties"
        Public Property StokSetelahAlokasi() As Integer
            Get
                Return _stokSetelahAlokasi
            End Get
            Set(ByVal value As Integer)
                _stokSetelahAlokasi = value
            End Set
        End Property

        Public Property TotalDO() As Integer
            Get
                Return _totalDO
            End Get
            Set(ByVal value As Integer)
                _totalDO = value
            End Set
        End Property

        Public Property Total() As Integer
            Get
                Return _total
            End Get
            Set(ByVal value As Integer)
                _total = value
            End Set
        End Property
        Public Property SisaStokDNet() As Integer
            Get
                Return _sisaStokDNet
            End Get
            Set(ByVal value As Integer)
                _sisaStokDNet = value
            End Set
        End Property
        Public Property LineNo() As Integer
            Get
                Return _lineNo
            End Get
            Set(ByVal value As Integer)
                _lineNo = value
            End Set
        End Property
#End Region

    End Class

End Namespace

