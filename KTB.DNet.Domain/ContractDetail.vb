#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ContractDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 8:58:20 AM
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
    <Serializable(), TableInfo("ContractDetail")> _
    Public Class ContractDetail
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
        Private _lineItem As Integer
        Private _targetQty As Integer
        Private _confirmQty As Integer
        Private _inProcessQty As Integer
        Private _amount As Decimal
        Private _pPh22 As Decimal
        Private _discount As Decimal
        Private _salesSurcharge As Decimal
        Private _guaranteeAmount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pKDetail As PKDetail
        Private _contractHeader As ContractHeader
        Private _vechileColor As VechileColor



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


        <ColumnInfo("LineItem", "{0}")> _
        Public Property LineItem() As Integer
            Get
                Return _lineItem
            End Get
            Set(ByVal value As Integer)
                _lineItem = value
            End Set
        End Property


        <ColumnInfo("TargetQty", "{0}")> _
        Public Property TargetQty() As Integer
            Get
                If Me.ContractHeader.IsCarriedOver = 1 Then
                    Return _targetQty - Me.SisaUnit(True)
                End If
                Return _targetQty
            End Get
            Set(ByVal value As Integer)
                _targetQty = value
            End Set
        End Property


        <ColumnInfo("ConfirmQty", "{0}")> _
        Public Property ConfirmQty() As Integer
            Get
                Return _confirmQty
            End Get
            Set(ByVal value As Integer)
                _confirmQty = value
            End Set
        End Property


        <ColumnInfo("InProcessQty", "{0}")> _
        Public Property InProcessQty() As Integer
            Get
                Return _inProcessQty
            End Get
            Set(ByVal value As Integer)
                _inProcessQty = value
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("PPh22", "{0}")> _
        Public Property PPh22() As Decimal
            Get
                Return _pPh22
            End Get
            Set(ByVal value As Decimal)
                _pPh22 = value
            End Set
        End Property


        <ColumnInfo("Discount", "{0}")> _
        Public Property Discount() As Decimal
            Get
                Return _discount
            End Get
            Set(ByVal value As Decimal)
                _discount = value
            End Set
        End Property


        <ColumnInfo("SalesSurcharge", "{0}")> _
        Public Property SalesSurcharge() As Decimal
            Get
                Return _salesSurcharge
            End Get
            Set(ByVal value As Decimal)
                _salesSurcharge = value
            End Set
        End Property

        <ColumnInfo("GuaranteeAmount", "{0}")> _
        Public Property GuaranteeAmount() As Decimal
            Get
                Return _guaranteeAmount
            End Get
            Set(ByVal value As Decimal)
                _guaranteeAmount = value
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


        <ColumnInfo("PKDetailID", "{0}"), _
        RelationInfo("PKDetail", "ID", "ContractDetail", "PKDetailID")> _
        Public Property PKDetail() As PKDetail
            Get
                Try
                    If Not IsNothing(Me._pKDetail) AndAlso (Not Me._pKDetail.IsLoaded) Then

                        Me._pKDetail = CType(DoLoad(GetType(PKDetail).ToString(), _pKDetail.ID), PKDetail)
                        Me._pKDetail.MarkLoaded()

                    End If

                    Return Me._pKDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PKDetail)

                Me._pKDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pKDetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ContractHeaderID", "{0}"), _
        RelationInfo("ContractHeader", "ID", "ContractDetail", "ContractHeaderID")> _
        Public Property ContractHeader() As ContractHeader
            Get
                Try
                    If Not IsNothing(Me._contractHeader) AndAlso (Not Me._contractHeader.IsLoaded) Then

                        Me._contractHeader = CType(DoLoad(GetType(ContractHeader).ToString(), _contractHeader.ID), ContractHeader)
                        Me._contractHeader.MarkLoaded()

                    End If

                    Return Me._contractHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ContractHeader)

                Me._contractHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._contractHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VehicleColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "ContractDetail", "VehicleColorID")> _
        Public Property VechileColor() As VechileColor
            Get
                Try
                    If Not IsNothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
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

        Private _ContractHeaderID As Integer

        Public ReadOnly Property ContractHeaderID() As Integer
            Get
                Return _contractHeader.ID
            End Get
        End Property

        Public ReadOnly Property SisaUnit(Optional ByVal IsReal As Boolean = False) As Integer
            Get
                If IsReal = False AndAlso Me.ContractHeader.IsCarriedOver = 1 Then
                    Return 0
                End If
                Dim _total As Integer = Me._targetQty
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.ID", MatchType.Exact, Me.ID))
                criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Integer) & "," & CType(enumStatusPO.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Konfirmasi, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                Dim m_PODetailMapper As IMapper
                m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
                Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias)
                If (PODetailColl.Count > 0) Then
                    For Each item As PODetail In PODetailColl
                        If item.POHeader.Status = CInt(enumStatusPO.Status.Baru) Or item.POHeader.Status = CInt(enumStatusPO.Status.Konfirmasi) Then
                            _total = _total - CType(item.ReqQty, Integer)
                        ElseIf item.POHeader.Status = CInt(enumStatusPO.Status.Rilis) Or item.POHeader.Status = CInt(enumStatusPO.Status.Setuju) Or item.POHeader.Status = CInt(enumStatusPO.Status.Selesai) Then
                            _total = _total - CType(item.AllocQty, Integer)
                        End If
                    Next
                End If
                Return _total
            End Get
        End Property



#End Region

    End Class
End Namespace