#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PPQty Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/11/2005 - 11:48:43
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("PPQty")> _
    Public Class PPQty
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
        Private _periodeDate As Short
        Private _periodeMonth As Short
        Private _periodeYear As Short
        Private _productionYear As Short
        Private _allocationQty As Integer
        Private _dealerCode As String = String.Empty
        Private _unAllocatedQty As Integer
        Private _materialNumber As String = String.Empty
        Private _validatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("PeriodeDate", "{0}")> _
        Public Property PeriodeDate() As Short
            Get
                Return _periodeDate
            End Get
            Set(ByVal value As Short)
                _periodeDate = value
            End Set
        End Property


        <ColumnInfo("PeriodeMonth", "{0}")> _
        Public Property PeriodeMonth() As Short
            Get
                Return _periodeMonth
            End Get
            Set(ByVal value As Short)
                _periodeMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodeYear", "{0}")> _
        Public Property PeriodeYear() As Short
            Get
                Return _periodeYear
            End Get
            Set(ByVal value As Short)
                _periodeYear = value
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


        <ColumnInfo("AllocationQty", "{0}")> _
        Public Property AllocationQty() As Integer
            Get
                Return _allocationQty
            End Get
            Set(ByVal value As Integer)
                _allocationQty = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("UnAllocatedQty", "{0}")> _
        Public Property UnAllocatedQty() As Integer
            Get
                Return _unAllocatedQty
            End Get
            Set(ByVal value As Integer)
                _unAllocatedQty = value
            End Set
        End Property


        <ColumnInfo("MaterialNumber", "'{0}'")> _
        Public Property MaterialNumber() As String
            Get
                Return _materialNumber
            End Get
            Set(ByVal value As String)
                _materialNumber = value
            End Set
        End Property


        <ColumnInfo("ValidatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidatedTime() As DateTime
            Get
                Return _validatedTime
            End Get
            Set(ByVal value As DateTime)
                _validatedTime = value
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

#End Region

#Region "Custom Method"

        Public ReadOnly Property TotalAllocate() As Integer
            Get
                'Todo Aggregate
                Dim Total As Integer
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, Me.MaterialNumber))
                Dim m_PPQtyMapper As IMapper
                m_PPQtyMapper = MapperFactory.GetInstance.GetMapper(GetType(PPQty).ToString)
                Dim PPQtyColl As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias)
                If (PPQtyColl.Count > 0) Then
                    For Each item As PPQty In PPQtyColl
                        Total = Total - CType(item.AllocationQty, Integer)
                    Next
                End If
                Return _periodeDate
            End Get
        End Property

        Public ReadOnly Property TotalATP() As Integer
            Get
                Dim _total As Integer
                'Dim m_PPQtyMapper As IMapper

                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                'If Me.MaterialNumber.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, Me.MaterialNumber))

                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", Me.PeriodeDate))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeMonth", Me.PeriodeMonth))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeYear", Me.PeriodeYear))

                'm_PPQtyMapper = MapperFactory.GetInstance.GetMapper(GetType(PPQty).ToString)
                'Dim PPQtyColl As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias)


                'If (PPQtyColl.Count > 0) Then
                '    If Me.MaterialNumber.Trim <> String.Empty Then
                '        For Each item As PPQty In PPQtyColl
                '            _total += item.AllocationQty
                '        Next
                '    Else
                '        For Each item As PPQty In PPQtyColl
                '            If item.MaterialNumber = Me.MaterialNumber Then
                '                _total += item.AllocationQty
                '            End If
                '        Next
                '    End If

                'End If

                'Rumus:Stok ATP = Sisa+AlokasiSehari(alokasi dalam satu hari)
                '_total = Me.TotalSisa() + (Me.TotalRilis() + Me.TotalSetuju() + Me.TotalSelesai())

                _total = Me.TotalRilis() + Me.TotalSetuju() + Me.TotalSelesai()
                Dim oAH As ATPHistory = Me.GetLastATPHistory()
                If Not IsNothing(oAH) AndAlso oAH.ID > 0 Then
                    _total = _total + oAH.StokSesudah
                End If

                Return _total
            End Get
        End Property
        Private _ATPHistory As ATPHistory
        Private Function GetLastATPHistory() As ATPHistory
            If IsNothing(_ATPHistory) OrElse _ATPHistory.ID < 1 Then

                Dim cAH As New CriteriaComposite(New Criteria(GetType(ATPHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim dtStart As Date = DateSerial(Me.PeriodeYear, Me.PeriodeMonth, Me.PeriodeDate)
                Dim sAH As New SortCollection
                Dim m_AHMapper As IMapper
                Dim aAHs As ArrayList

                m_AHMapper = MapperFactory.GetInstance.GetMapper(GetType(ATPHistory).ToString)
                cAH.opAnd(New Criteria(GetType(ATPHistory), "MaterialNumber", MatchType.Exact, Me.MaterialNumber))
                cAH.opAnd(New Criteria(GetType(ATPHistory), "ProductionYear", MatchType.Exact, Me.ProductionYear))
                cAH.opAnd(New Criteria(GetType(ATPHistory), "AllocationDate", MatchType.GreaterOrEqual, dtStart))
                cAH.opAnd(New Criteria(GetType(ATPHistory), "AllocationDate", MatchType.Lesser, dtStart.AddDays(1)))
                'sAH.Add(New Sort(GetType(ATPHistory), "CreatedTime", Sort.SortDirection.DESC))
                'sAH.Add(New Sort(GetType(ATPHistory), "ID", Sort.SortDirection.DESC))

                sAH.Add(New Sort(GetType(ATPHistory), "DownloadedTime", Sort.SortDirection.DESC)) 'add by dna on 201308131646
                sAH.Add(New Sort(GetType(ATPHistory), "StokSesudah", Sort.SortDirection.ASC))

                aAHs = m_AHMapper.RetrieveByCriteria(cAH, sAH)
                If aAHs.Count > 0 Then
                    Me._ATPHistory = aAHs(0)
                Else
                    Me._ATPHistory = New ATPHistory
                End If
            End If
            Return Me._ATPHistory
        End Function

        Public ReadOnly Property TotalPermintaan() As Integer
            Get
                Dim _total As Integer

                Try
                    Dim m_PODetailMapper As IMapper
                    Dim currentDate As Date = New Date(Me.PeriodeYear, Me.PeriodeMonth, Me.PeriodeDate)
                    'currentDate = currentDate.AddDays(1)'replaced for dealerorder CR
                    Dim nextdate As Date = RetrieveNextDay(currentDate)

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    If Me.MaterialNumber.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, Me.MaterialNumber))
                    'If Me.DealerCode.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ContractHeader.Dealer.DealerCode", MatchType.Exact, Me.DealerCode))

                    'criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ContractHeader.Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
                    'start:replaced by DealerOrder
                    'criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDate", MatchType.Exact, nextdate.Day))
                    'criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationMonth", MatchType.Exact, nextdate.Month))
                    'criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationYear", MatchType.Exact, nextdate.Year))
                    'end:replaced by DealerOrder
                    'start:dealer order
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.GreaterOrEqual, currentDate.ToString("yyyy/MM/dd 00:00:00")))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.Lesser, currentDate.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))
                    'end:dealer order

                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" _
                        & CType(enumStatusPO.Status.Ditolak, Integer) & "," _
                        & CType(enumStatusPO.Status.Rilis, Integer) & "," _
                        & CType(enumStatusPO.Status.DiBlok, Integer) & "," _
                        & CType(enumStatusPO.Status.Setuju, Integer) & "," _
                        & CType(enumStatusPO.Status.Tidak_Setuju, Integer) & "," _
                        & CType(enumStatusPO.Status.Selesai, Integer) & ")"))

                    '& CType(enumStatusPO.Status.Baru, Integer) & "," _
                    '& CType(enumStatusPO.Status.Konfirmasi, Integer) & "," _

                    m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)

                    Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias)

                    If (PODetailColl.Count > 0) Then
                        If Me.MaterialNumber.Trim <> String.Empty Then
                            For Each item As PODetail In PODetailColl
                                _total += item.ReqQty
                            Next
                        Else
                            For Each item As PODetail In PODetailColl
                                If item.ContractDetail.VechileColor.MaterialNumber = Me.MaterialNumber Then
                                    _total += item.ReqQty
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    _total = Nothing
                End Try

                Return _total
            End Get
        End Property

        Private Function RetrieveNextDay(ByVal currentDate As Date) As Date
            Dim m_NationalHolidayMapper As IMapper = MapperFactory.GetInstance().GetMapper(GetType(NationalHoliday).ToString)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDate", MatchType.Exact, currentDate.Day))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayMonth", MatchType.Exact, currentDate.Month))
            criterias.opAnd(New Criteria(GetType(NationalHoliday), "HolidayYear", MatchType.Exact, currentDate.Year))

            Dim NationalColl As ArrayList = m_NationalHolidayMapper.RetrieveByCriteria(criterias)

            Dim count = NationalColl.Count
            Dim nextDay As Date = currentDate
            While count > 0
                Dim _natDay As NationalHoliday = CType(NationalColl(0), NationalHoliday)
                nextDay = New Date(_natDay.HolidayYear, _natDay.HolidayMonth, _natDay.HolidayDate)
                nextDay = nextDay.AddDays(1)
                nextDay = RetrieveNextDay(nextDay)
                count = 0
            End While
            Return nextDay

        End Function

        Private _TotalRilis As Integer = 0
        Public ReadOnly Property TotalRilis() As Integer
            Get
                If Me._TotalRilis < 1 Then
                    Dim m_PODetailMapper As IMapper
                    Dim currentDate As Date = New Date(Me.PeriodeYear, Me.PeriodeMonth, Me.PeriodeDate)
                    'currentDate = currentDate.AddDays(1) 'replaced from DealerOrderCR
                    Dim nextdate As Date = RetrieveNextDay(currentDate)
                    Dim agg As New Aggregate(GetType(PODetail), "AllocQty", AggregateType.Sum)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    If Me.MaterialNumber.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, Me.MaterialNumber))

                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.GreaterOrEqual, currentDate.ToString("yyyy/MM/dd 00:00:00")))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.Lesser, currentDate.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))


                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Rilis, Integer) & ")"))

                    m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
                    Try
                        Me._TotalRilis = CType(m_PODetailMapper.RetrieveScalar(agg, criterias), Integer)
                    Catch ex As Exception
                        Me._TotalRilis = 0
                    End Try

                End If

                Return _TotalRilis
            End Get
        End Property

        Private _TotalSetuju As Integer = 0
        Public ReadOnly Property TotalSetuju() As Integer
            Get
                If Me._TotalSetuju < 1 Then
                    Dim m_PODetailMapper As IMapper
                    Dim currentDate As Date = New Date(Me.PeriodeYear, Me.PeriodeMonth, Me.PeriodeDate)
                    'currentDate = currentDate.AddDays(1)'replaced for DealerOrder CR
                    Dim nextdate As Date = RetrieveNextDay(currentDate)
                    Dim agg As New Aggregate(GetType(PODetail), "AllocQty", AggregateType.Sum)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    If Me.MaterialNumber.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, Me.MaterialNumber))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.GreaterOrEqual, currentDate.ToString("yyyy/MM/dd 00:00:00")))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.Lesser, currentDate.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))
                    
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Setuju, Integer) & ")"))

                    m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
                    Try
                        Me._TotalSetuju = CType(m_PODetailMapper.RetrieveScalar(agg, criterias), Integer)
                    Catch ex As Exception
                        Me._TotalSetuju = 0
                    End Try

                End If

                Return _TotalSetuju
            End Get
        End Property

        Private _TotalTolak As Integer = 0
        Public ReadOnly Property TotalTolak() As Integer
            Get
                If Me._TotalTolak < 1 Then
                    Dim m_PODetailMapper As IMapper
                    Dim currentDate As Date = New Date(Me.PeriodeYear, Me.PeriodeMonth, Me.PeriodeDate)
                    'currentDate = currentDate.AddDays(1)'replaced for Dealer Order CR
                    Dim nextdate As Date = RetrieveNextDay(currentDate)
                    Dim agg As New Aggregate(GetType(PODetail), "ReqQty", AggregateType.Sum)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    If Me.MaterialNumber.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, Me.MaterialNumber))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.GreaterOrEqual, currentDate.ToString("yyyy/MM/dd 00:00:00")))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.Lesser, currentDate.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))
                    

                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" _
                        & CType(enumStatusPO.Status.Ditolak, Integer) & "," _
                        & CType(enumStatusPO.Status.DiBlok, Integer) & "," _
                        & CType(enumStatusPO.Status.Tidak_Setuju, Integer) & ")"))

                    m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
                    Try
                        Me._TotalTolak = CType(m_PODetailMapper.RetrieveScalar(agg, criterias), Integer)
                    Catch ex As Exception
                        Me._TotalTolak = 0
                    End Try
                    'UnAllocatedRequest
                    Me._TotalTolak += Me.TotalSelisihAlokasi()
                End If

                Return _TotalTolak
            End Get
        End Property

        Private _TotalSelesai As Integer = 0
        Public ReadOnly Property TotalSelesai() As Integer
            Get
                If Me._TotalSelesai < 1 Then
                    Dim m_PODetailMapper As IMapper
                    Dim currentDate As Date = New Date(Me.PeriodeYear, Me.PeriodeMonth, Me.PeriodeDate)
                    'currentDate = currentDate.AddDays(1)'replaced for DealerOrder CR
                    Dim nextdate As Date = RetrieveNextDay(currentDate)
                    Dim agg As New Aggregate(GetType(PODetail), "AllocQty", AggregateType.Sum)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    If Me.MaterialNumber.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, Me.MaterialNumber))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.GreaterOrEqual, currentDate.ToString("yyyy/MM/dd 00:00:00")))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.Lesser, currentDate.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))

                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Selesai, Integer) & ")"))

                    m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
                    Try
                        Me._TotalSelesai = CType(m_PODetailMapper.RetrieveScalar(agg, criterias), Integer)
                    Catch ex As Exception
                        Me._TotalSelesai = 0
                    End Try

                End If

                Return _TotalSelesai
            End Get
        End Property
        Private _TotalSelisihAlokasi As Integer
        Public ReadOnly Property TotalSelisihAlokasi() As Integer
            Get
                If Me._TotalSelisihAlokasi < 1 Then
                    Dim m_PODetailMapper As IMapper
                    Dim currentDate As Date = New Date(Me.PeriodeYear, Me.PeriodeMonth, Me.PeriodeDate)
                    Dim nextdate As Date = RetrieveNextDay(currentDate)
                    Dim agg As New Aggregate(GetType(PODetail), "AllocQty", AggregateType.Sum)
                    Dim agg2 As New Aggregate(GetType(PODetail), "ReqQty", AggregateType.Sum)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim nReq As Integer, nAlloc As Integer
                    Dim sStatus As String

                    If Me.MaterialNumber.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, Me.MaterialNumber))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.GreaterOrEqual, currentDate.ToString("yyyy/MM/dd 00:00:00")))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.Lesser, currentDate.AddDays(1).ToString("yyyy/MM/dd 00:00:00")))

                    sStatus = CType(enumStatusPO.Status.Selesai, Integer).ToString
                    sStatus &= "," & CType(enumStatusPO.Status.Setuju, Integer).ToString
                    sStatus &= "," & CType(enumStatusPO.Status.Rilis, Integer).ToString()
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & sStatus & ")"))

                    m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
                    Try
                        nReq = CType(m_PODetailMapper.RetrieveScalar(agg2, criterias), Integer)
                    Catch ex As Exception
                        nReq = 0
                    End Try
                    Try
                        nAlloc = CType(m_PODetailMapper.RetrieveScalar(agg, criterias), Integer)
                    Catch ex As Exception
                        nAlloc = 0
                    End Try
                    Me._TotalSelisihAlokasi = nReq - nAlloc
                End If
                Return Me._TotalSelisihAlokasi
            End Get
        End Property


        Private _TotalSisa As Integer = 0
        Public ReadOnly Property TotalSisa() As Integer
            Get
                If _TotalSisa < 1 Then
                    'Me._TotalSisa = Me.TotalPermintaan - (Me.TotalRilis + Me.TotalSetuju + Me.TotalTolak + Me.TotalSelesai)
                    'Rumus:Sisa = ATPSAPFile-Alokasi>ValidatedTime
                    'Me._TotalSisa = Me.TotalATPInDB() - TotalAlokasiBaru()
                    Dim oAH As ATPHistory = Me.GetLastATPHistory()
                    Me._TotalSisa = 0
                    If Not IsNothing(oAH) AndAlso oAH.ID > 0 Then
                        Me._TotalSisa += oAH.StokSesudah
                    Else
                        Me._TotalSisa = Me.TotalATPInDB()
                    End If
                End If
                Return _TotalSisa
            End Get
        End Property
        Private _totalAlokasi As Integer = 0
        Public ReadOnly Property TotalAlokasiBaru() As Integer
            Get
                If Me._totalAlokasi < 1 Then
                    Dim m_PODetailMapper As IMapper
                    Dim currentDate As Date = New Date(Me.PeriodeYear, Me.PeriodeMonth, Me.PeriodeDate)
                    'currentDate = currentDate.AddDays(1)'replaced for DealerOrder CR
                    Dim nextdate As Date = RetrieveNextDay(currentDate)
                    Dim agg As New Aggregate(GetType(PODetail), "AllocQty", AggregateType.Sum)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    If Me.MaterialNumber.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, Me.MaterialNumber))
                    criterias.opAnd(New Criteria(GetType(PODetail), "AllocationDateTime", MatchType.Greater, Me.ValidatedTime.ToString("yyyy/MM/dd HH:mm:ss")))
                    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Selesai, Integer) & ")"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" _
                                            & CType(enumStatusPO.Status.Rilis, Integer) & "," _
                                            & CType(enumStatusPO.Status.Setuju, Integer) & "," _
                                            & CType(enumStatusPO.Status.Selesai, Integer) & ")"))
                    m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
                    Try
                        Me._totalAlokasi = CType(m_PODetailMapper.RetrieveScalar(agg, criterias), Integer)
                    Catch ex As Exception
                        Me._totalAlokasi = 0
                    End Try

                End If

                Return _totalAlokasi
            End Get
        End Property

        Public ReadOnly Property TotalATPInDB() As Integer
            Get
                Dim _total As Integer
                Dim m_PPQtyMapper As IMapper

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                If Me.MaterialNumber.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, Me.MaterialNumber))

                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", Me.PeriodeDate))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeMonth", Me.PeriodeMonth))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeYear", Me.PeriodeYear))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "ProductionYear", Me.ProductionYear))

                m_PPQtyMapper = MapperFactory.GetInstance.GetMapper(GetType(PPQty).ToString)
                Dim PPQtyColl As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias)


                If (PPQtyColl.Count > 0) Then
                    If Me.MaterialNumber.Trim <> String.Empty Then
                        For Each item As PPQty In PPQtyColl
                            _total += item.AllocationQty
                        Next
                    Else
                        For Each item As PPQty In PPQtyColl
                            If item.MaterialNumber = Me.MaterialNumber Then
                                _total += item.AllocationQty
                            End If
                        Next
                    End If

                End If

                Return _total
            End Get
        End Property

        ReadOnly Property VechileColor() As VechileColor
            Get
                Dim cVC As New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim aVCs As ArrayList
                Dim m_VCMapper As IMapper
                Dim oVC As New VechileColor

                m_VCMapper = MapperFactory.GetInstance.GetMapper(GetType(VechileColor).ToString)
                cVC.opAnd(New Criteria(GetType(VechileColor), "MaterialNumber", MatchType.Exact, Me.MaterialNumber))
                aVCs = m_VCMapper.RetrieveByCriteria(cVC)
                If aVCs.Count > 0 Then
                    oVC = aVCs(0)
                End If
                Return oVC
            End Get
        End Property


#End Region

    End Class
End Namespace