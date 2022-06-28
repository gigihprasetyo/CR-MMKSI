#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : IndentPartDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/2/2007 - 12:46:08 PM
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
    <Serializable(), TableInfo("IndentPartDetail")> _
    Public Class IndentPartDetail
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
        Private _totalForecast As Integer
        Private _qty As Integer
        Private _description As String = String.Empty
        Private _allocationQty As Integer
        Private _isCompletedAllocation As Byte
        Private _price As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _indentPartHeader As IndentPartHeader
        Private _sparePartMaster As SparePartMaster
        Private _indentPartPODetails As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _indentPartAllocationDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _POQty As Integer
        Private _indentPartPOs As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("TotalForecast", "{0}")> _
        Public Property TotalForecast() As Integer
            Get
                Return _totalForecast
            End Get
            Set(ByVal value As Integer)
                _totalForecast = value
            End Set
        End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty() As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
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

        <ColumnInfo("AllocationQty", "{0}")> _
        Public Property AllocationQty() As Integer
            Get
                Return _allocationQty
            End Get
            Set(ByVal value As Integer)
                _allocationQty = value
            End Set
        End Property

        <ColumnInfo("IsCompletedAllocation", "{0}")> _
        Public Property IsCompletedAllocation() As Byte
            Get
                Return _isCompletedAllocation
            End Get
            Set(ByVal value As Byte)
                _isCompletedAllocation = value
            End Set
        End Property

        <ColumnInfo("Price", "{0}")> _
        Public Property Price() As Decimal
            Get
                Return _price
            End Get
            Set(ByVal value As Decimal)
                _price = value
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


        <ColumnInfo("IndentPartHeaderID", "{0}"), _
        RelationInfo("IndentPartHeader", "ID", "IndentPartDetail", "IndentPartHeaderID")> _
        Public Property IndentPartHeader() As IndentPartHeader
            Get
                Try
                    If Not IsNothing(Me._indentPartHeader) AndAlso (Not Me._indentPartHeader.IsLoaded) Then

                        Me._indentPartHeader = CType(DoLoad(GetType(IndentPartHeader).ToString(), _indentPartHeader.ID), IndentPartHeader)
                        Me._indentPartHeader.MarkLoaded()

                    End If

                    Return Me._indentPartHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As IndentPartHeader)

                Me._indentPartHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._indentPartHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "IndentPartDetail", "SparePartMasterID")> _
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

        <RelationInfo("IndentPartDetail", "ID", "IndentPartPODetail", "IndentPartDetailID")> _
        Public ReadOnly Property IndentPartPODetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._indentPartPODetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(IndentPartPODetail), "IndentPartDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(IndentPartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._indentPartPODetails = DoLoadArray(GetType(IndentPartPODetail).ToString, criterias)
                    End If

                    Return Me._indentPartPODetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("IndentPartDetail", "ID", "IndentPartPO", "IndentPartDetailID")> _
        Public ReadOnly Property IndentPartPOs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._indentPartPOs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(IndentPartPO), "IndentPartDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(IndentPartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._indentPartPOs = DoLoadArray(GetType(IndentPartPO).ToString, criterias)
                    End If

                    Return Me._indentPartPOs

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

#Region "Custom Properties"
        Public ReadOnly Property POQty() As Integer
            Get


                If Me.ID = 0 Then
                    Return 0
                End If

                Dim IDPOToInclude As String = ""

                For Each item As IndentPartPO In Me.IndentPartPOs
                    IDPOToInclude = IDPOToInclude & item.SparePartPODetail.ID.ToString & ","
                Next

                If IDPOToInclude = "" Then
                    Return 0
                End If

                IDPOToInclude = Left(IDPOToInclude, IDPOToInclude.Length - 1)

                Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SparePartPODetail), "ID", MatchType.InSet, "(" & IDPOToInclude & ")"))
                'criterias.opAnd(New Criteria(GetType(SparePartPODetail), "IndentPartDetail.ID", MatchType.Exact, Me.ID))
                'criterias.opAnd(New Criteria(GetType(SparePartPODetail), "IndentPartPOHeader.IsCancelled", MatchType.Exact, 0))

                Dim agg As Aggregate = New Aggregate(GetType(SparePartPODetail), "Quantity", AggregateType.Sum)

                _POQty = DoLoadScalar(GetType(SparePartPODetail).ToString(), agg, criterias)

                Return _POQty
            End Get
        End Property

        Public ReadOnly Property SisaQty() As Integer
            Get

                Return Me.Qty - Me.POQty
            End Get
        End Property

        ReadOnly Property EstimationEquipDetail() As EstimationEquipDetail
            Get
                'Dim oEEPOFac As EstimationEquipPOFacade = New EstimationEquipPOFacade(User)
                Dim cEEPO As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim aEEPO As New ArrayList
                'Dim oEEPO As EstimationEquipPO
                Dim oEED As EstimationEquipDetail

                cEEPO.opAnd(New Criteria(GetType(EstimationEquipPO), "IndentPartDetail.ID", MatchType.Exact, Me.ID))
                'aEEPO = oEEPOFac.Retrieve(cEEPO)
                aEEPO = DoLoadArray(GetType(EstimationEquipPO).ToString, cEEPO)
                If aEEPO.Count > 0 Then
                    oEED = CType(aEEPO(0), EstimationEquipPO).EstimationEquipDetail
                    If Not IsNothing(oEED) AndAlso oEED.ID < 1 Then
                        oEED = New EstimationEquipDetail
                    End If
                    Return oEED
                Else
                    Return New EstimationEquipDetail
                End If
            End Get
        End Property

#End Region

    End Class
End Namespace

