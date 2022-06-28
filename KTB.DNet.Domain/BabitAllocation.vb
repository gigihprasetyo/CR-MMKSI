#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitAllocation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/9/2007 - 2:23:50 PM
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
    <Serializable(), TableInfo("BabitAllocation")> _
    Public Class BabitAllocation
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
        Private _pC As Decimal
        Private _lCV As Decimal
        Private _cV As Decimal
        Private _noPerjanjian As String = String.Empty
        Private _reffNoPerjanjian As String = String.Empty
        Private _status As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _babit As Babit
        Private _dealer As Dealer

        Private _babitProposals As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("PC", "{0}")> _
        Public Property PC() As Decimal
            Get
                Return _pC
            End Get
            Set(ByVal value As Decimal)
                _pC = value
            End Set
        End Property


        <ColumnInfo("LCV", "{0}")> _
        Public Property LCV() As Decimal
            Get
                Return _lCV
            End Get
            Set(ByVal value As Decimal)
                _lCV = value
            End Set
        End Property


        <ColumnInfo("CV", "{0}")> _
        Public Property CV() As Decimal
            Get
                Return _cV
            End Get
            Set(ByVal value As Decimal)
                _cV = value
            End Set
        End Property


        <ColumnInfo("NoPerjanjian", "'{0}'")> _
        Public Property NoPerjanjian() As String
            Get
                Return _noPerjanjian
            End Get
            Set(ByVal value As String)
                _noPerjanjian = value
            End Set
        End Property


        <ColumnInfo("ReffNoPerjanjian", "'{0}'")> _
        Public Property ReffNoPerjanjian() As String
            Get
                Return _reffNoPerjanjian
            End Get
            Set(ByVal value As String)
                _reffNoPerjanjian = value
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


        <ColumnInfo("BabitID", "{0}"), _
        RelationInfo("Babit", "ID", "BabitAllocation", "BabitID")> _
        Public Property Babit() As Babit
            Get
                Try
                    If Not isnothing(Me._babit) AndAlso (Not Me._babit.IsLoaded) Then

                        Me._babit = CType(DoLoad(GetType(Babit).ToString(), _babit.ID), Babit)
                        Me._babit.MarkLoaded()

                    End If

                    Return Me._babit

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Babit)

                Me._babit = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babit.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "BabitAllocation", "DealerID")> _
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


        <RelationInfo("BabitAllocation", "ID", "BabitProposal", "BabitAllocationID")> _
        Public ReadOnly Property BabitProposals() As System.Collections.ArrayList
            Get
                Try
                    If (Me._babitProposals.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BabitProposal), "BabitAllocation", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._babitProposals = DoLoadArray(GetType(BabitProposal).ToString, criterias)
                    End If

                    Return Me._babitProposals

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
        Public ReadOnly Property DanaBabit() As Decimal
            Get
                Return _cV + _lCV + _pC
            End Get
        End Property

        Public ReadOnly Property TotalDanaBabit() As Decimal
            Get
                Return TotalDanaBabit(EnumBabit.BabitAllocationType.Alokasi_Reguler)
                'Dim _babitProposalAmountAwal As Decimal = 0
                'Dim babitAlocId As Integer = 0
                'For Each obj As BabitProposal In BabitProposals
                '    If obj.Status = EnumBabit.StatusBabitProposal.Disetujui Then
                '        _babitProposalAmountAwal += obj.BabitAllocation.DanaBabit
                '    End If
                'Next
                'Return _babitProposalAmountAwal

                'Dim _DanaTambahan As Decimal = 0
                'Dim m_BabitAllocation As IMapper

                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(BabitAllocation), "Dealer.ID", MatchType.Exact, _dealer.ID))
                'criterias.opAnd(New Criteria(GetType(BabitAllocation), "ReffNoPerjanjian", MatchType.Exact, _reffNoPerjanjian))
                'criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.AllocationType", MatchType.No, CType(EnumBabit.BabitAllocationType.Babit_Khusus, Short)))
                'criterias.opAnd(New Criteria(GetType(BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Rilis, Short)))

                'm_BabitAllocation = MapperFactory.GetInstance.GetMapper(GetType(BabitAllocation).ToString)
                'Dim _ListDanaTambahan As ArrayList = m_BabitAllocation.RetrieveByCriteria(criterias)

                'If _ListDanaTambahan.Count > 0 Then
                '    For Each item As BabitAllocation In _ListDanaTambahan
                '        _DanaTambahan += item.DanaBabit
                '    Next

                'End If

                'Return DanaBabit + _DanaTambahan
            End Get
        End Property

        Public ReadOnly Property TotalDanaBabit(ByVal alocationType As Integer) As Decimal
            Get
                Dim _DanaTambahan As Decimal = 0
                Dim m_BabitAllocation As IMapper

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitAllocation), "Dealer.ID", MatchType.Exact, _dealer.ID))
                'criterias.opAnd(New Criteria(GetType(BabitAllocation), "ReffNoPerjanjian", MatchType.Exact, _reffNoPerjanjian))
                criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.AllocationType", MatchType.Exact, alocationType))
                criterias.opAnd(New Criteria(GetType(BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Rilis, Short)))

                m_BabitAllocation = MapperFactory.GetInstance.GetMapper(GetType(BabitAllocation).ToString)
                Dim _ListDanaTambahan As ArrayList = m_BabitAllocation.RetrieveByCriteria(criterias)

                If _ListDanaTambahan.Count > 0 Then
                    For Each item As BabitAllocation In _ListDanaTambahan
                        _DanaTambahan += item.DanaBabit
                    Next

                End If

                Return _DanaTambahan
            End Get
        End Property

        Public ReadOnly Property SisaBabit() As Decimal
            Get
                Return Me.DanaBabit - PenggunaanBabit
            End Get
        End Property

        Public ReadOnly Property SisaBabit(ByVal alocationType As Integer) As Decimal
            Get
                Return TotalDanaBabit(alocationType) - PenggunaanBabit
            End Get
        End Property


        Public ReadOnly Property PenggunaanBabit() As Decimal
            Get
                Dim _babitProposalAmountAwal As Decimal = 0
                'Dim _babitProposalAmountTambahan As Decimal = 0
                For Each obj As BabitProposal In BabitProposals
                    If obj.Status = EnumBabit.StatusBabitProposal.Disetujui Then
                        _babitProposalAmountAwal += obj.KTBApprovalAmount
                    End If
                Next

                'Dim m_BabitAllocation As IMapper

                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(BabitAllocation), "Dealer.ID", MatchType.Exact, _dealer.ID))
                'criterias.opAnd(New Criteria(GetType(BabitAllocation), "ReffNoPerjanjian", MatchType.Exact, _noPerjanjian))
                'criterias.opAnd(New Criteria(GetType(BabitAllocation), "Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Alokasi_Tambahan, Short)))
                'criterias.opAnd(New Criteria(GetType(BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Rilis, Short)))

                'm_BabitAllocation = MapperFactory.GetInstance.GetMapper(GetType(BabitAllocation).ToString)
                'Dim _ListDanaTambahan As ArrayList = m_BabitAllocation.RetrieveByCriteria(criterias)

                'If _ListDanaTambahan.Count > 0 Then
                '    For Each item As BabitAllocation In _ListDanaTambahan
                '        If item.BabitProposals.Count > 0 Then
                '            For Each sub_item As BabitProposal In item.BabitProposals
                '                If sub_item.Status = EnumBabit.StatusBabitProposal.Disetujui Then
                '                    _babitProposalAmountTambahan += sub_item.KTBApprovalAmount
                '                End If
                '            Next
                '        End If
                '    Next

                'End If

                'Return (_babitProposalAmountAwal + _babitProposalAmountTambahan)
                Return _babitProposalAmountAwal
            End Get
        End Property

        Public Function FilterApprovedProposals(ByVal param As EnumBabit.BabitProposalType) As ArrayList
            Dim newArr As New ArrayList
            newArr.Clear()
            For Each item As BabitProposal In ApprovedProposals()
                If item.ActivityType = param Then
                    newArr.Add(item)
                End If
            Next
            Return newArr
        End Function

        Public Function ApprovedProposals() As ArrayList
            Dim newArr As New ArrayList
            newArr.Clear()
            For Each item As BabitProposal In _babitProposals
                If item.Status = EnumBabit.StatusBabitProposal.Disetujui Then
                    newArr.Add(item)
                End If
            Next
            Return newArr
        End Function

        Public ReadOnly Property TipeAlokasi() As Integer
            Get
                Return Babit.AllocationType
            End Get
        End Property
#End Region

    End Class
End Namespace

