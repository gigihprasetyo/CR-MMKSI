#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EquipmentSalesDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 09/11/2005 - 1:10:38 PM
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
    <Serializable(), TableInfo("EquipmentSalesDetail")> _
    Public Class EquipmentSalesDetail
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
        Private _quantity As Integer
        Private _discount As Short
        Private _price As Decimal
        Private _estimatePrice As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _equipmentMaster As EquipmentMaster
        Private _equipmentSalesHeader As EquipmentSalesHeader



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


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity() As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
            End Set
        End Property


        <ColumnInfo("Discount", "{0}")> _
        Public Property Discount() As Short
            Get
                Return _discount
            End Get
            Set(ByVal value As Short)
                _discount = value
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


        <ColumnInfo("EstimatePrice", "{0}")> _
        Public Property EstimatePrice() As Decimal
            Get
                Return _estimatePrice
            End Get
            Set(ByVal value As Decimal)
                _estimatePrice = value
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


        <ColumnInfo("EquipmentmasterID", "{0}"), _
        RelationInfo("EquipmentMaster", "ID", "EquipmentSalesDetail", "EquipmentmasterID")> _
        Public Property EquipmentMaster() As EquipmentMaster
            Get
                Try
                    If Not isnothing(Me._equipmentMaster) AndAlso (Not Me._equipmentMaster.IsLoaded) Then

                        Me._equipmentMaster = CType(DoLoad(GetType(EquipmentMaster).ToString(), _equipmentMaster.ID), EquipmentMaster)
                        Me._equipmentMaster.MarkLoaded()

                    End If

                    Return Me._equipmentMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EquipmentMaster)

                Me._equipmentMaster = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._equipmentMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("EquipmentSalesHeaderID", "{0}"), _
        RelationInfo("EquipmentSalesHeader", "ID", "EquipmentSalesDetail", "EquipmentSalesHeaderID")> _
        Public Property EquipmentSalesHeader() As EquipmentSalesHeader
            Get
                Try
                    If Not isnothing(Me._equipmentSalesHeader) AndAlso (Not Me._equipmentSalesHeader.IsLoaded) Then

                        Me._equipmentSalesHeader = CType(DoLoad(GetType(EquipmentSalesHeader).ToString(), _equipmentSalesHeader.ID), EquipmentSalesHeader)
                        Me._equipmentSalesHeader.MarkLoaded()

                    End If

                    Return Me._equipmentSalesHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EquipmentSalesHeader)

                Me._equipmentSalesHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._equipmentSalesHeader.MarkLoaded()
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
        Public ReadOnly Property PriceFromEquipmentMaster() As Decimal
            Get
                Dim _total As Decimal = 0
                If Not Me.EquipmentMaster Is Nothing Then
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(HeaderBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(HeaderBOM), "EquipmentMaster.ID", MatchType.Exact, Me.EquipmentMaster.ID))
                    Dim m_HeaderBOMMapper As IMapper
                    m_HeaderBOMMapper = MapperFactory.GetInstance.GetMapper(GetType(HeaderBOM).ToString)
                    Dim HeaderBOMColl As ArrayList = m_HeaderBOMMapper.RetrieveByCriteria(criterias)
                    If HeaderBOMColl.Count = 0 Then
                        _total = Me.EquipmentMaster.Price
                    Else
                        _total = CType(HeaderBOMColl(0), HeaderBOM).TotalHarga
                    End If
                End If
                Return _total
            End Get
        End Property
#End Region

    End Class
End Namespace

