
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 3/30/2011 - 10:00:34 AM
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
    <Serializable(), TableInfo("SPKHeader")> _
    Public Class SPKHeader
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
        Private _status As String = String.Empty
        Private _sPKNumber As String = String.Empty
        Private _campaignName As String = String.Empty
        Private _sPKReferenceNumber As String = String.Empty
        Private _indentNumber As String = String.Empty
        Private _dealerSPKNumber As String = String.Empty
        Private _planDeliveryMonth As Byte
        Private _planDeliveryYear As Short
        Private _planDeliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _planInvoiceMonth As Byte
        Private _planInvoiceYear As Short
        Private _planInvoiceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerRequestID As Integer
        Private _validateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateBy As String = String.Empty
        Private _rejectedReason As String = String.Empty
        Private _evidenceFile As String = String.Empty
        Private _validationKey As String = String.Empty
        Private _flagUpdate As Short
        Private _isSend As Short
        Private _dealerSPKDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eventType As Integer
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _dealer As Dealer
        Private _category As Category
        Private _salesmanHeader As SalesmanHeader
        Private _sPKCustomer As SPKCustomer
        Private _customerRequest As CustomerRequest
        Private _dealerBranch As DealerBranch
        Private _benefitMasterHeader As BenefitMasterHeader

        Private _sPKFakturs As System.Collections.ArrayList = New System.Collections.ArrayList
        'Private _sPKStatusHistorys As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sPKDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("SPKNumber", "'{0}'")> _
        Public Property SPKNumber() As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
            End Set
        End Property

        <ColumnInfo("SPKReferenceNumber", "'{0}'")> _
        Public Property SPKReferenceNumber() As String
            Get
                Return _sPKReferenceNumber
            End Get
            Set(ByVal value As String)
                _sPKReferenceNumber = value
            End Set
        End Property

        <ColumnInfo("DealerSPKNumber", "'{0}'")> _
        Public Property DealerSPKNumber() As String
            Get
                Return _dealerSPKNumber
            End Get
            Set(ByVal value As String)
                _dealerSPKNumber = value
            End Set
        End Property

        <ColumnInfo("IndentNumber", "'{0}'")> _
        Public Property IndentNumber() As String
            Get
                Return _indentNumber
            End Get
            Set(ByVal value As String)
                _indentNumber = value
            End Set
        End Property

        <ColumnInfo("PlanDeliveryMonth", "{0}")> _
        Public Property PlanDeliveryMonth() As Byte
            Get
                Return _planDeliveryMonth
            End Get
            Set(ByVal value As Byte)
                _planDeliveryMonth = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryYear", "{0}")> _
        Public Property PlanDeliveryYear() As Short
            Get
                Return _planDeliveryYear
            End Get
            Set(ByVal value As Short)
                _planDeliveryYear = value
            End Set
        End Property


        <ColumnInfo("PlanDeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanDeliveryDate() As DateTime
            Get
                Return _planDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                _planDeliveryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PlanInvoiceMonth", "{0}")> _
        Public Property PlanInvoiceMonth() As Byte
            Get
                Return _planInvoiceMonth
            End Get
            Set(ByVal value As Byte)
                _planInvoiceMonth = value
            End Set
        End Property


        <ColumnInfo("PlanInvoiceYear", "{0}")> _
        Public Property PlanInvoiceYear() As Short
            Get
                Return _planInvoiceYear
            End Get
            Set(ByVal value As Short)
                _planInvoiceYear = value
            End Set
        End Property


        <ColumnInfo("PlanInvoiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanInvoiceDate() As DateTime
            Get
                Return _planInvoiceDate
            End Get
            Set(ByVal value As DateTime)
                _planInvoiceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerRequestID", "{0}")> _
        Public Property CustomerRequestID() As Integer
            Get
                Return _customerRequestID
            End Get
            Set(ByVal value As Integer)
                _customerRequestID = value
            End Set
        End Property


        <ColumnInfo("ValidateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidateTime() As DateTime
            Get
                Return _validateTime
            End Get
            Set(ByVal value As DateTime)
                _validateTime = value
            End Set
        End Property


        <ColumnInfo("ValidateBy", "'{0}'")> _
        Public Property ValidateBy() As String
            Get
                Return _validateBy
            End Get
            Set(ByVal value As String)
                _validateBy = value
            End Set
        End Property


        <ColumnInfo("RejectedReason", "'{0}'")> _
        Public Property RejectedReason() As String
            Get
                Return _rejectedReason
            End Get
            Set(ByVal value As String)
                _rejectedReason = value
            End Set
        End Property

        <ColumnInfo("EvidenceFile", "'{0}'")> _
        Public Property EvidenceFile() As String
            Get
                Return _evidenceFile
            End Get
            Set(ByVal value As String)
                _evidenceFile = value
            End Set
        End Property

        <ColumnInfo("ValidationKey", "'{0}'")> _
        Public Property ValidationKey() As String
            Get
                Return _validationKey
            End Get
            Set(ByVal value As String)
                _validationKey = value
            End Set
        End Property

        <ColumnInfo("FlagUpdate", "{0}")> _
        Public Property FlagUpdate() As Short
            Get
                Return _flagUpdate
            End Get
            Set(ByVal value As Short)
                _flagUpdate = value
            End Set
        End Property


        <ColumnInfo("EventType", "{0}")> _
        Public Property EventType() As Integer
            Get
                Return _eventType
            End Get
            Set(ByVal value As Integer)
                _eventType = value
            End Set
        End Property


        <ColumnInfo("CampaignName", "'{0}'")> _
        Public Property CampaignName() As String
            Get
                Return _campaignName
            End Get
            Set(ByVal value As String)
                _campaignName = value
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

        <ColumnInfo("IsSend", "{0}")> _
        Public Property IsSend() As Short
            Get
                Return _isSend
            End Get
            Set(ByVal value As Short)
                _isSend = value
            End Set
        End Property

        <ColumnInfo("DealerSPKDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DealerSPKDate() As DateTime
            Get
                Return _dealerSPKDate
            End Get
            Set(ByVal value As DateTime)
                _dealerSPKDate = value
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


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property

        <ColumnInfo("BenefitMasterHeaderID", "{0}"), _
        RelationInfo("BenefitMasterHeader", "ID", "SPKHeader", "BenefitMasterHeaderID")> _
        Public Property BenefitMasterHeader() As BenefitMasterHeader
            Get
                Try
                    If Not IsNothing(Me._benefitMasterHeader) AndAlso (Not Me._benefitMasterHeader.IsLoaded) Then

                        Me._benefitMasterHeader = CType(DoLoad(GetType(BenefitMasterHeader).ToString(), _benefitMasterHeader.ID), BenefitMasterHeader)
                        Me._benefitMasterHeader.MarkLoaded()

                    End If

                    Return Me._benefitMasterHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitMasterHeader)

                Me._benefitMasterHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitMasterHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SPKHeader", "DealerID")> _
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

        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "SPKHeader", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not IsNothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SalesmanHeaderID", "{0}"), _
        RelationInfo("SalesmanHeader", "ID", "SPKHeader", "SalesmanHeaderID")> _
        Public Property SalesmanHeader() As SalesmanHeader
            Get
                Try
                    If Not IsNothing(Me._salesmanHeader) AndAlso (Not Me._salesmanHeader.IsLoaded) Then

                        Me._salesmanHeader = CType(DoLoad(GetType(SalesmanHeader).ToString(), _salesmanHeader.ID), SalesmanHeader)
                        Me._salesmanHeader.MarkLoaded()

                    End If

                    Return Me._salesmanHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesmanHeader)

                Me._salesmanHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesmanHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPKCustomerID", "{0}"), _
        RelationInfo("SPKCustomer", "ID", "SPKHeader", "SPKCustomerID")> _
        Public Property SPKCustomer() As SPKCustomer
            Get
                Try
                    If Not IsNothing(Me._sPKCustomer) AndAlso (Not Me._sPKCustomer.IsLoaded) Then

                        Me._sPKCustomer = CType(DoLoad(GetType(SPKCustomer).ToString(), _sPKCustomer.ID), SPKCustomer)
                        Me._sPKCustomer.MarkLoaded()

                    End If

                    Return Me._sPKCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKCustomer)

                Me._sPKCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKCustomer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CustomerRequestID", "{0}"), _
        RelationInfo("CustomerRequest", "ID", "SPKHeader", "CustomerRequestID")> _
        Public ReadOnly Property CustomerRequest() As CustomerRequest
            Get
                Try
                    If IsNothing(_customerRequest) Then
                        Me._customerRequest = CType(DoLoad(GetType(CustomerRequest).ToString(), Me.CustomerRequestID), CustomerRequest)
                        Me._customerRequest.MarkLoaded()

                    End If

                    Return Me._customerRequest

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property

        <ColumnInfo("DealerBranchID", "{0}"), _
        RelationInfo("DealerBranch", "ID", "SPKHeader", "DealerBranchID")> _
        Public Property DealerBranch() As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealerBranch.ID), DealerBranch)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("SPKHeader", "ID", "SPKFaktur", "SPKHeaderID")> _
        Public ReadOnly Property SPKFakturs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPKFakturs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKFaktur), "SPKHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPKFakturs = DoLoadArray(GetType(SPKFaktur).ToString, criterias)
                    End If

                    Return Me._sPKFakturs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        '<RelationInfo("SPKHeader", "ID", "SPKStatusHistory", "SPKHeaderID")> _
        'Public ReadOnly Property SPKStatusHistorys() As System.Collections.ArrayList
        '    Get
        '        Try
        '            If (Me._sPKStatusHistorys.Count < 1) Then
        '                Dim _criteria As Criteria = New Criteria(GetType(SPKStatusHistory), "SPKHeader", Me.ID)
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
        '                criterias.opAnd(New Criteria(GetType(SPKStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '                Me._sPKStatusHistorys = DoLoadArray(GetType(SPKStatusHistory).ToString, criterias)
        '            End If

        '            Return Me._sPKStatusHistorys

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing

        '    End Get
        'End Property

        <RelationInfo("SPKHeader", "ID", "SPKDetail", "SPKHeaderID")> _
        Public ReadOnly Property SPKDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPKDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKDetail), "SPKHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPKDetails = DoLoadArray(GetType(SPKDetail).ToString, criterias)
                    End If

                    Return Me._sPKDetails

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

    End Class
End Namespace

