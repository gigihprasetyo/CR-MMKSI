
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 6/5/2009 - 3:25:40 PM
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
    <Serializable(), TableInfo("PKHeader")> _
    Public Class PKHeader
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
        Private _pKNumber As String = String.Empty
        Private _headPKNumber As Integer
        Private _pKDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pKType As String = String.Empty
        Private _pKStatus As String = String.Empty
        Private _orderType As Byte
        Private _purpose As Byte
        Private _projectName As String = String.Empty
        Private _projectDetail As String = String.Empty
        Private _dealerPKNumber As String = String.Empty
        Private _requestPeriodeDay As Byte
        Private _requestPeriodeMonth As Byte
        Private _requestPeriodeYear As Short
        Private _pricingPeriodeDay As Byte
        Private _pricingPeriodeMonth As Byte
        Private _pricingPeriodeYear As Short
        Private _productionYear As Short
        Private _kTBResponse As String = String.Empty
        Private _responseBy As String = String.Empty
        Private _responseTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _agreeBy As String = String.Empty
        Private _agreeTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _description As String = String.Empty
        Private _statusDownload As Byte
        Private _rowStatus As Short
        Private _freePPh22Indicator As Byte
        Private _sPLNumber As String = String.Empty
        Private _maxTOPDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _maxTopDay As Integer
        Private _freeIntIndicator As Byte
        Private _maxTopIndicator As Integer
        Private _isAproveRilis As Byte
        Private _isAutoApprovedDealer As Byte
        Private _isFormAConfirmation As Byte
        Private _isUnlockFreeze As Byte
        Private _jaminanID As Integer
        Private _evidencePath As String = String.Empty
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rejectedReason As String = String.Empty
        Private _fleetDiscountCode As String = String.Empty

        Private _category As Category
        Private _dealer As Dealer

        Private _pKDetails As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _dealerBranch As DealerBranch

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


        <ColumnInfo("PKNumber", "'{0}'")> _
        Public Property PKNumber() As String
            Get
                Return _pKNumber
            End Get
            Set(ByVal value As String)
                _pKNumber = value
            End Set
        End Property


        <ColumnInfo("HeadPKNumber", "{0}")> _
        Public Property HeadPKNumber() As Integer
            Get
                Return _headPKNumber
            End Get
            Set(ByVal value As Integer)
                _headPKNumber = value
            End Set
        End Property


        <ColumnInfo("PKDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PKDate() As DateTime
            Get
                Return _pKDate
            End Get
            Set(ByVal value As DateTime)
                _pKDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PKType", "'{0}'")> _
        Public Property PKType() As String
            Get
                Return _pKType
            End Get
            Set(ByVal value As String)
                _pKType = value
            End Set
        End Property


        <ColumnInfo("PKStatus", "'{0}'")> _
        Public Property PKStatus() As String
            Get
                Return _pKStatus
            End Get
            Set(ByVal value As String)
                _pKStatus = value
            End Set
        End Property


        <ColumnInfo("OrderType", "{0}")> _
        Public Property OrderType() As Byte
            Get
                Return _orderType
            End Get
            Set(ByVal value As Byte)
                _orderType = value
            End Set
        End Property


        <ColumnInfo("Purpose", "{0}")> _
        Public Property Purpose() As Byte
            Get
                Return _purpose
            End Get
            Set(ByVal value As Byte)
                _purpose = value
            End Set
        End Property


        <ColumnInfo("ProjectName", "'{0}'")> _
        Public Property ProjectName() As String
            Get
                Return _projectName
            End Get
            Set(ByVal value As String)
                _projectName = value
            End Set
        End Property


        <ColumnInfo("ProjectDetail", "'{0}'")> _
        Public Property ProjectDetail() As String
            Get
                Return _projectDetail
            End Get
            Set(ByVal value As String)
                _projectDetail = value
            End Set
        End Property


        <ColumnInfo("DealerPKNumber", "'{0}'")> _
        Public Property DealerPKNumber() As String
            Get
                Return _dealerPKNumber
            End Get
            Set(ByVal value As String)
                _dealerPKNumber = value
            End Set
        End Property


        <ColumnInfo("RequestPeriodeDay", "{0}")> _
        Public Property RequestPeriodeDay() As Byte
            Get
                Return _requestPeriodeDay
            End Get
            Set(ByVal value As Byte)
                _requestPeriodeDay = value
            End Set
        End Property

        <ColumnInfo("RequestPeriodeMonth", "{0}")> _
        Public Property RequestPeriodeMonth() As Byte
            Get
                Return _requestPeriodeMonth
            End Get
            Set(ByVal value As Byte)
                _requestPeriodeMonth = value
            End Set
        End Property


        <ColumnInfo("RequestPeriodeYear", "{0}")> _
        Public Property RequestPeriodeYear() As Short
            Get
                Return _requestPeriodeYear
            End Get
            Set(ByVal value As Short)
                _requestPeriodeYear = value
            End Set
        End Property


        <ColumnInfo("PricingPeriodeDay", "{0}")> _
        Public Property PricingPeriodeDay() As Byte
            Get
                Return _pricingPeriodeDay
            End Get
            Set(ByVal value As Byte)
                _pricingPeriodeDay = value
            End Set
        End Property


        <ColumnInfo("PricingPeriodeMonth", "{0}")> _
        Public Property PricingPeriodeMonth() As Byte
            Get
                Return _pricingPeriodeMonth
            End Get
            Set(ByVal value As Byte)
                _pricingPeriodeMonth = value
            End Set
        End Property


        <ColumnInfo("PricingPeriodeYear", "{0}")> _
        Public Property PricingPeriodeYear() As Short
            Get
                Return _pricingPeriodeYear
            End Get
            Set(ByVal value As Short)
                _pricingPeriodeYear = value
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


        <ColumnInfo("KTBResponse", "'{0}'")> _
        Public Property KTBResponse() As String
            Get
                Return _kTBResponse
            End Get
            Set(ByVal value As String)
                _kTBResponse = value
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


        <ColumnInfo("ResponseTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ResponseTime() As DateTime
            Get
                Return _responseTime
            End Get
            Set(ByVal value As DateTime)
                _responseTime = value
            End Set
        End Property


        <ColumnInfo("AgreeBy", "'{0}'")> _
        Public Property AgreeBy() As String
            Get
                Return _agreeBy
            End Get
            Set(ByVal value As String)
                _agreeBy = value
            End Set
        End Property


        <ColumnInfo("AgreeTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property AgreeTime() As DateTime
            Get
                Return _agreeTime
            End Get
            Set(ByVal value As DateTime)
                _agreeTime = value
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


        <ColumnInfo("StatusDownload", "{0}")> _
        Public Property StatusDownload() As Byte
            Get
                Return _statusDownload
            End Get
            Set(ByVal value As Byte)
                _statusDownload = value
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


        <ColumnInfo("FreePPh22Indicator", "{0}")> _
        Public Property FreePPh22Indicator() As Byte
            Get
                Return _freePPh22Indicator
            End Get
            Set(ByVal value As Byte)
                _freePPh22Indicator = value
            End Set
        End Property


        <ColumnInfo("SPLNumber", "'{0}'")> _
        Public Property SPLNumber() As String
            Get
                Return _sPLNumber
            End Get
            Set(ByVal value As String)
                _sPLNumber = value
            End Set
        End Property


        <ColumnInfo("MaxTOPDate", "'{0:yyyy/MM/dd}'")> _
        Public Property MaxTOPDate() As DateTime
            Get
                Return _maxTOPDate
            End Get
            Set(ByVal value As DateTime)
                _maxTOPDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("MaxTopDay", "{0}")> _
        Public Property MaxTopDay() As Integer
            Get
                Return _maxTopDay
            End Get
            Set(ByVal value As Integer)
                _maxTopDay = value
            End Set
        End Property


        <ColumnInfo("FreeIntIndicator", "{0}")> _
        Public Property FreeIntIndicator() As Byte
            Get
                Return _freeIntIndicator
            End Get
            Set(ByVal value As Byte)
                _freeIntIndicator = value
            End Set
        End Property


        <ColumnInfo("MaxTopIndicator", "{0}")> _
        Public Property MaxTopIndicator() As Integer
            Get
                Return _maxTopIndicator
            End Get
            Set(ByVal value As Integer)
                _maxTopIndicator = value
            End Set
        End Property


        <ColumnInfo("IsAproveRilis", "{0}")> _
        Public Property IsAproveRilis() As Byte
            Get
                Return _isAproveRilis
            End Get
            Set(ByVal value As Byte)
                _isAproveRilis = value
            End Set
        End Property

        <ColumnInfo("IsAutoApprovedDealer", "{0}")> _
        Public Property IsAutoApprovedDealer() As Byte
            Get
                Return _isAutoApprovedDealer
            End Get
            Set(ByVal value As Byte)
                _isAutoApprovedDealer = value
            End Set
        End Property

        <ColumnInfo("IsFormAConfirmation", "{0}")> _
        Public Property IsFormAConfirmation() As Byte
            Get
                Return _isFormAConfirmation
            End Get
            Set(ByVal value As Byte)
                _isFormAConfirmation = value
            End Set
        End Property


        <ColumnInfo("IsUnlockFreeze", "{0}")> _
        Public Property IsUnlockFreeze() As Byte
            Get
                Return _isUnlockFreeze
            End Get
            Set(ByVal value As Byte)
                _isUnlockFreeze = value
            End Set
        End Property

        <ColumnInfo("JaminanID", "{0}")> _
        Public Property JaminanID() As Integer
            Get
                Return _jaminanID
            End Get
            Set(ByVal value As Integer)
                _jaminanID = value
            End Set
        End Property

        <ColumnInfo("EvidencePath", "'{0}'")> _
        Public Property EvidencePath As String
            Get
                Return _evidencePath
            End Get
            Set(ByVal value As String)
                _evidencePath = value
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


        <ColumnInfo("RejectedReason", "'{0}'")> _
        Public Property RejectedReason() As String
            Get
                Return _rejectedReason
            End Get
            Set(ByVal value As String)
                _rejectedReason = value
            End Set
        End Property


        <ColumnInfo("FleetDiscountCode", "'{0}'")> _
        Public Property FleetDiscountCode() As String
            Get
                Return _fleetDiscountCode
            End Get
            Set(ByVal value As String)
                _fleetDiscountCode = value
            End Set
        End Property


        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "PKHeader", "CategoryID")> _
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

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "PKHeader", "DealerID")> _
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


        <RelationInfo("PKHeader", "ID", "PKDetail", "PKHeaderID")> _
        Public ReadOnly Property PKDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pKDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PKDetail), "PKHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pKDetails = DoLoadArray(GetType(PKDetail).ToString, criterias)
                    End If

                    Return Me._pKDetails

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
     RelationInfo("DealerBranch", "ID", "PKHeader", "DealerBranchID")> _
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
        Public ReadOnly Property TotalHargaTebus() As Double
            Get
                Dim Total As Double = Domain.Calculation.CountPKHargaTotal(Me.PKDetails) 'donin.20120111


                'For Each item As PKDetail In Me.PKDetails
                '    If (Me.PKStatus = enumStatusPK.Status.Tidak_Setuju OrElse Me.PKStatus = enumStatusPK.Status.DiBlok OrElse Me.PKStatus = enumStatusPK.Status.Rilis OrElse Me.PKStatus = enumStatusPK.Status.Setuju OrElse Me.PKStatus = enumStatusPK.Status.Selesai) Then
                '        'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                '        Total = Total + (CType(item.ResponseQty, Double) * (CType(item.ResponseAmount, Double) + (CType(item.ResponsePPh22, Double) * CInt(item.PKHeader.FreePPh22Indicator))))
                '        'Total = Total + (CType(item.ResponseQty, Double) * (CType(item.ResponseAmount, Double) + (CType(item.ResponsePPh22, Double) * IIf(CInt(item.PKHeader.FreePPh22Indicator) = 1, 0, 1))))
                '        'End    :RemainModule-DailyPO:FreePPh By:Doni N
                '    Else
                '        'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                '        Total = Total + (CType(item.TargetQty, Double) * (CType(item.TargetAmount, Double) + (CType(item.TargetPPh22, Double) * CInt(item.PKHeader.FreePPh22Indicator))))
                '        'Total = Total + (CType(item.TargetQty, Double) * (CType(item.TargetAmount, Double) + (CType(item.TargetPPh22, Double) * IIf(CInt(item.PKHeader.FreePPh22Indicator) = 1, 0, 1))))
                '        'End    :RemainModule-DailyPO:FreePPh By:Doni N
                '    End If
                'Next
                Return Total
            End Get
        End Property

        Public ReadOnly Property TotalQuantity() As Double
            Get
                Dim Total As Double
                For Each item As PKDetail In Me.PKDetails
                    'Perubahan Phase 4
                    If (Me.PKStatus = enumStatusPK.Status.Tidak_Setuju OrElse Me.PKStatus = enumStatusPK.Status.DiBlok OrElse Me.PKStatus = enumStatusPK.Status.Rilis OrElse Me.PKStatus = enumStatusPK.Status.Setuju OrElse Me.PKStatus = enumStatusPK.Status.Selesai) Then
                        Total = Total + (CType(item.ResponseQty, Long))
                    Else
                        Total = Total + (CType(item.TargetQty, Long))
                    End If
                Next
                Return Total
            End Get
        End Property

        Public ReadOnly Property TotalTargetQuantity() As Double
            Get
                Dim Total As Double
                For Each item As PKDetail In Me.PKDetails
                    'Request Peggy
                    Total = Total + (CType(item.TargetQty, Long))
                Next
                Return Total
            End Get
        End Property

        Public ReadOnly Property FreezeStatus(ByVal IsInPeriodForFreeze As Boolean) As Byte
            Get
                If Me.OrderType = 1 Then 'Tambahan
                    If IsInPeriodForFreeze And Me.PKStatus = enumStatusPK.Status.Baru Then
                        If Me.IsUnlockFreeze = 0 Then
                            Return enumStatusPK.EnumFreezeStatus.Freeze
                        Else
                            Return enumStatusPK.EnumFreezeStatus.FreezeButUnlock
                        End If
                    Else
                        Return enumStatusPK.EnumFreezeStatus.NotFreeze
                    End If
                Else
                    Return enumStatusPK.EnumFreezeStatus.NeverFreeze
                End If
            End Get
        End Property

#End Region

    End Class
End Namespace

