#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EquipmentSalesHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 09/11/2005 - 11:06:50 AM
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
    <Serializable(), TableInfo("EquipmentSalesHeader")> _
    Public Class EquipmentSalesHeader
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
        Private _regPONumber As String = String.Empty
        Private _status As Integer
        Private _isSOProcess As Integer
        Private _kind As Integer
        Private _isKTBView As Integer
        Private _pONumber As String = String.Empty
        Private _reqDeliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _deteilRequirement As String = String.Empty
        Private _approveBy As String = String.Empty
        Private _approveDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _estimateDeliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _responseBy As String = String.Empty
        Private _responseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _responseDetail As String = String.Empty
        Private _validate1By As String = String.Empty
        Private _validate1Date As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validate2By As String = String.Empty
        Private _validate2Date As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

        Private _equipmentSalesPayments As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _equipmentSalesDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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

        <ColumnInfo("RegPONumber", "'{0}'")> _
        Public Property RegPONumber() As String
            Get
                Return _regPONumber
            End Get
            Set(ByVal value As String)
                _regPONumber = value
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

        <ColumnInfo("IsSOProcess", "{0}")> _
        Public Property IsSOProcess() As Integer
            Get
                Return _isSOProcess
            End Get
            Set(ByVal value As Integer)
                _isSOProcess = value
            End Set
        End Property

        <ColumnInfo("Kind", "{0}")> _
        Public Property Kind() As Integer
            Get
                Return _kind
            End Get
            Set(ByVal value As Integer)
                _kind = value
            End Set
        End Property

        <ColumnInfo("IsKTBView", "{0}")> _
        Public Property IsKTBView() As Integer
            Get
                Return _isKTBView
            End Get
            Set(ByVal value As Integer)
                _isKTBView = value
            End Set
        End Property

        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property

        <ColumnInfo("ReqDeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReqDeliveryDate() As DateTime
            Get
                Return _reqDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                _reqDeliveryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("DeteilRequirement", "'{0}'")> _
        Public Property DeteilRequirement() As String
            Get
                Return _deteilRequirement
            End Get
            Set(ByVal value As String)
                _deteilRequirement = value
            End Set
        End Property

        <ColumnInfo("ApproveBy", "'{0}'")> _
        Public Property ApproveBy() As String
            Get
                Return _approveBy
            End Get
            Set(ByVal value As String)
                _approveBy = value
            End Set
        End Property

        <ColumnInfo("ApproveDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ApproveDate() As DateTime
            Get
                Return _approveDate
            End Get
            Set(ByVal value As DateTime)
                _approveDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("EstimateDeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EstimateDeliveryDate() As DateTime
            Get
                Return _estimateDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                _estimateDeliveryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ResponseBy", "'{0}'")> _
        Public Property ResponseBy() As String
            Get
                Return _responseBy
            End Get
            Set(ByVal value As String)
                _responseBy = value
            End Set
        End Property

        <ColumnInfo("ResponseDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ResponseDate() As DateTime
            Get
                Return _responseDate
            End Get
            Set(ByVal value As DateTime)
                _responseDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ResponseDetail", "'{0}'")> _
        Public Property ResponseDetail() As String
            Get
                Return _responseDetail
            End Get
            Set(ByVal value As String)
                _responseDetail = value
            End Set
        End Property

        <ColumnInfo("Validate1By", "'{0}'")> _
        Public Property Validate1By() As String
            Get
                Return _validate1By
            End Get
            Set(ByVal value As String)
                _validate1By = value
            End Set
        End Property

        <ColumnInfo("Validate1Date", "'{0:yyyy/MM/dd}'")> _
        Public Property Validate1Date() As DateTime
            Get
                Return _validate1Date
            End Get
            Set(ByVal value As DateTime)
                _validate1Date = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("Validate2By", "'{0}'")> _
        Public Property Validate2By() As String
            Get
                Return _validate2By
            End Get
            Set(ByVal value As String)
                _validate2By = value
            End Set
        End Property

        <ColumnInfo("Validate2Date", "'{0:yyyy/MM/dd}'")> _
        Public Property Validate2Date() As DateTime
            Get
                Return _validate2Date
            End Get
            Set(ByVal value As DateTime)
                _validate2Date = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ValidateDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ValidateDate() As DateTime
            Get
                Return _validateDate
            End Get
            Set(ByVal value As DateTime)
                _validateDate = New DateTime(value.Year, value.Month, value.Day)
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

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "EquipmentSalesHeader", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("EquipmentSalesHeader", "ID", "EquipmentSalesPayment", "EquipmentSalesHederID")> _
        Public ReadOnly Property EquipmentSalesPayments() As System.Collections.ArrayList
            Get
                Try
                    If (Me._equipmentSalesPayments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EquipmentSalesPayment), "EquipmentSalesHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EquipmentSalesPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._equipmentSalesPayments = DoLoadArray(GetType(EquipmentSalesPayment).ToString, criterias)
                    End If

                    Return Me._equipmentSalesPayments

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("EquipmentSalesHeader", "ID", "EquipmentSalesDetail", "EquipmentSalesHeaderID")> _
        Public ReadOnly Property EquipmentSalesDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._equipmentSalesDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EquipmentSalesDetail), "EquipmentSalesHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EquipmentSalesDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._equipmentSalesDetails = DoLoadArray(GetType(EquipmentSalesDetail).ToString, criterias)
                    End If

                    Return Me._equipmentSalesDetails

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
        Public ReadOnly Property Total() As Double
            Get
                Dim _total As Double = 0
                For Each item As EquipmentSalesDetail In Me.EquipmentSalesDetails
                    If item.Price <> 0 Then
                        _total += (item.Price - ((item.Discount * item.Price) / 100)) * item.Quantity
                    Else
                        _total += (item.PriceFromEquipmentMaster - ((item.Discount * item.PriceFromEquipmentMaster) / 100)) * item.Quantity
                    End If
                Next
                Return _total
            End Get
        End Property

        Public ReadOnly Property Subsidi() As Double
            Get
                Dim _total As Double = 0
                For Each item As EquipmentSalesDetail In Me.EquipmentSalesDetails
                    If item.Price <> 0 Then
                        _total += ((item.Discount * item.Price) / 100) * item.Quantity
                    Else
                        _total += ((item.Discount * item.PriceFromEquipmentMaster) / 100) * item.Quantity
                    End If
                Next
                Return _total
            End Get

        End Property

        Public ReadOnly Property TotalPembayaran() As Double
            Get
                'Todo Aggregate
                Dim _total As Double = 0
                For Each item As EquipmentSalesPayment In Me.EquipmentSalesPayments
                    If item.Status <> -1 Then
                        _total += item.Amount
                    End If
                Next
                Return _total
            End Get

        End Property
#End Region

    End Class
End Namespace