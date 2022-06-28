#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DailyPayment Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 12/23/2005 - 10:33:37 AM
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
    <Serializable(), TableInfo("DailyPayment")> _
    Public Class DailyPayment
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
        Private _dailyPaymentHeaderID As Integer
        Private _docNumber As String = String.Empty
        Private _fiscalYear As Short
        Private _docDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _baselineDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _slipNumber As String = String.Empty
        Private _receiptNumber As String = String.Empty
        Private _sAPCreator As String = String.Empty
        Private _amount As Decimal
        Private _rejectStatus As Integer
        Private _effectiveDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _acceleratedGyro As Short
        Private _acceleratedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _remark As String = String.Empty
        Private _isReversed As Short
        Private _isCleared As Short
        Private _reason As String = String.Empty
        Private _entryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pIC As String = String.Empty
        Private _gyroType As Short
        Private _entryType As Short
        Private _ref1 As String = String.Empty
        Private _ref2 As String = String.Empty
        Private _ref3 As String = String.Empty
        Private _acceleratorID As Integer
        Private _cessieID As Integer
        Private _cessieTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Short
        Private _bankID As Integer
        Private _numOfTransfered As Integer
        Private _remarkStatus As Short
        Private _reUpload As Short
        Private _lastUploadedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUploadedBy As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _paymentPurpose As PaymentPurpose
        Private _dailyPaymentHeader As DailyPaymentHeader = Nothing
        Private _pOHeader As POHeader
        Private _reversedDocNumber As String = String.Empty
        Private _salesOrder As SalesOrder

        Private _accelerator As DailyPayment = Nothing
        Private _oldDailyPayment As DailyPayment = Nothing
        Private _otherAssignments As ArrayList = New ArrayList
        Private _bank As Bank = Nothing

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

        <ColumnInfo("DocNumber", "'{0}'")> _
        Public Property DocNumber() As String
            Get
                Return _docNumber
            End Get
            Set(ByVal value As String)
                _docNumber = value
            End Set
        End Property

        <ColumnInfo("FiscalYear", "{0}")> _
        Public Property FiscalYear() As Short
            Get
                Return _fiscalYear
            End Get
            Set(ByVal value As Short)
                _fiscalYear = value
            End Set
        End Property


        <ColumnInfo("DocDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DocDate() As DateTime
            Get
                Return _docDate
            End Get
            Set(ByVal value As DateTime)
                _docDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("BaselineDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BaselineDate() As DateTime
            Get
                Return _baselineDate
            End Get
            Set(ByVal value As DateTime)
                _baselineDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SlipNumber", "'{0}'")> _
        Public Property SlipNumber() As String
            Get
                If _slipNumber.Trim = "" Then
                    If Not IsNothing(DailyPaymentHeader) Then
                        If DailyPaymentHeader.RegNumber.Trim <> "" Then
                            Return DailyPaymentHeader.RegNumber
                        Else
                            Return String.Empty
                        End If
                    Else
                        Return String.Empty
                    End If
                Else
                    Return _slipNumber
                End If

                'Return _slipNumber
            End Get
            Set(ByVal value As String)
                _slipNumber = value
            End Set
        End Property


        <ColumnInfo("ReceiptNumber", "'{0}'")> _
        Public Property ReceiptNumber() As String
            Get
                Return _receiptNumber
            End Get
            Set(ByVal value As String)
                _receiptNumber = value
            End Set
        End Property


        <ColumnInfo("SAPCreator", "'{0}'")> _
        Public Property SAPCreator() As String
            Get
                Return _sAPCreator
            End Get
            Set(ByVal value As String)
                _sAPCreator = value
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


        <ColumnInfo("RejectStatus", "{0}")> _
        Public Property RejectStatus() As Integer
            Get
                Return _rejectStatus
            End Get
            Set(ByVal value As Integer)
                _rejectStatus = value
            End Set
        End Property

        <ColumnInfo("EffectiveDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EffectiveDate() As DateTime
            Get
                Return _effectiveDate
            End Get
            Set(ByVal value As DateTime)
                _effectiveDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("AcceleratedGyro", "{0}")> _
        Public Property AcceleratedGyro() As Short
            Get
                Return _acceleratedGyro
            End Get
            Set(ByVal value As Short)
                _acceleratedGyro = value
            End Set
        End Property


        <ColumnInfo("AcceleratedDate", "'{0:yyyy/MM/dd}'")> _
        Public Property AcceleratedDate() As DateTime
            Get
                Return _acceleratedDate
            End Get
            Set(ByVal value As DateTime)
                _acceleratedDate = New DateTime(value.Year, value.Month, value.Day)
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



        <ColumnInfo("IsReversed", "{0}")> _
        Public Property IsReversed() As Short
            Get
                Return _isReversed
            End Get
            Set(ByVal value As Short)
                _isReversed = value
            End Set
        End Property


        <ColumnInfo("IsCleared", "{0}")> _
        Public Property IsCleared() As Short
            Get
                Return _isCleared
            End Get
            Set(ByVal value As Short)
                _isCleared = value
            End Set
        End Property


        <ColumnInfo("Reason", "'{0}'")> _
        Public Property Reason() As String
            Get
                Return _reason
            End Get
            Set(ByVal value As String)
                _reason = value
            End Set
        End Property


        <ColumnInfo("EntryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EntryDate() As DateTime
            Get
                Return _entryDate
            End Get
            Set(ByVal value As DateTime)
                _entryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PIC", "'{0}'")> _
        Public Property PIC() As String
            Get
                Return _pIC
            End Get
            Set(ByVal value As String)
                _pIC = value
            End Set
        End Property

        <ColumnInfo("GyroType", "{0}")> _
        Public Property GyroType() As Short
            Get
                Return _gyroType
            End Get
            Set(ByVal value As Short)
                _gyroType = value
            End Set
        End Property

        <ColumnInfo("EntryType", "{0}")> _
        Public Property EntryType() As Short
            Get
                Return _entryType
            End Get
            Set(ByVal value As Short)
                _entryType = value
            End Set
        End Property



        <ColumnInfo("Ref1", "'{0}'")> _
        Public Property Ref1() As String
            Get
                Return _ref1
            End Get
            Set(ByVal value As String)
                _ref1 = value
            End Set
        End Property


        <ColumnInfo("Ref2", "'{0}'")> _
        Public Property Ref2() As String
            Get
                Return _ref2
            End Get
            Set(ByVal value As String)
                _ref2 = value
            End Set
        End Property


        <ColumnInfo("Ref3", "'{0}'")> _
        Public Property Ref3() As String
            Get
                Return _ref3
            End Get
            Set(ByVal value As String)
                _ref3 = value
            End Set
        End Property


        <ColumnInfo("AcceleratorID", "{0}")> _
        Public Property AcceleratorID() As Integer
            Get
                Return _acceleratorID
            End Get
            Set(ByVal value As Integer)
                _acceleratorID = value
            End Set
        End Property

        <ColumnInfo("CessieID", "{0}")> _
        Public Property CessieID() As Integer
            Get
                Return _cessieID
            End Get
            Set(ByVal value As Integer)
                _cessieID = value
            End Set
        End Property


        <ColumnInfo("CessieTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CessieTime() As DateTime
            Get
                Return _cessieTime
            End Get
            Set(ByVal value As DateTime)
                _cessieTime = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property

        <ColumnInfo("BankID", "{0}")> _
        Public Property BankID() As Integer
            Get
                Return _bankID
            End Get
            Set(ByVal value As Integer)
                _bankID = value
            End Set
        End Property

        <ColumnInfo("NumOfTransfered", "{0}")> _
        Public Property NumOfTransfered() As Integer
            Get
                Return _numOfTransfered
            End Get
            Set(ByVal value As Integer)
                _numOfTransfered = value
            End Set
        End Property

        <ColumnInfo("RemarkStatus", "{0}")> _
        Public Property RemarkStatus() As Short
            Get
                Return _remarkStatus
            End Get
            Set(ByVal value As Short)
                _remarkStatus = value
            End Set
        End Property

        <ColumnInfo("ReUpload", "{0}")> _
        Public Property ReUpload() As Short
            Get
                Return _reUpload
            End Get
            Set(ByVal value As Short)
                _reUpload = value
            End Set
        End Property

        <ColumnInfo("LastUploadedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUploadedTime() As DateTime
            Get
                Return _lastUploadedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUploadedTime = value
            End Set
        End Property


        <ColumnInfo("LastUploadedBy", "'{0}'")> _
        Public Property LastUploadedBy() As String
            Get
                Return _lastUploadedBy
            End Get
            Set(ByVal value As String)
                _lastUploadedBy = value
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


        <ColumnInfo("PaymentPurposeID", "{0}"), _
        RelationInfo("PaymentPurpose", "ID", "DailyPayment", "PaymentPurposeID")> _
        Public Property PaymentPurpose() As PaymentPurpose
            Get
                Try
                    If Not IsNothing(Me._paymentPurpose) AndAlso (Not Me._paymentPurpose.IsLoaded) Then

                        Me._paymentPurpose = CType(DoLoad(GetType(PaymentPurpose).ToString(), _paymentPurpose.ID), PaymentPurpose)
                        Me._paymentPurpose.MarkLoaded()

                    End If

                    Return Me._paymentPurpose

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PaymentPurpose)

                Me._paymentPurpose = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._paymentPurpose.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DailyPaymentHeaderID", "{0}"), _
        RelationInfo("DailyPaymentHeader", "ID", "DailyPayment", "DailyPaymentHeaderID")> _
        Public Property DailyPaymentHeader() As DailyPaymentHeader
            Get
                Try
                    'If IsNothing(Me._dailyPaymentHeader) OrElse Me._dailyPaymentHeader.ID < 1 OrElse Me._dailyPaymentHeader.IsLoaded = False Then
                    If IsNothing(Me._dailyPaymentHeader) = False AndAlso (Me._dailyPaymentHeader.IsLoaded = False) Then

                        Me._dailyPaymentHeader = CType(DoLoad(GetType(DailyPaymentHeader).ToString(), _dailyPaymentHeader.ID), DailyPaymentHeader)
                        Me._dailyPaymentHeader.MarkLoaded()
                    End If

                    Return Me._dailyPaymentHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DailyPaymentHeader)

                Me._dailyPaymentHeader = value
                If (Not IsNothing(value)) Then ' AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dailyPaymentHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("POID", "{0}"), _
        RelationInfo("POHeader", "ID", "DailyPayment", "POID")> _
        Public Property POHeader() As POHeader
            Get
                Try
                    If Not IsNothing(Me._pOHeader) AndAlso (Not Me._pOHeader.IsLoaded) Then

                        Me._pOHeader = CType(DoLoad(GetType(POHeader).ToString(), _pOHeader.ID), POHeader)
                        Me._pOHeader.MarkLoaded()

                    End If

                    Return Me._pOHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As POHeader)

                Me._pOHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pOHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SalesOrderID", "{0}"), _
        RelationInfo("SalesOrder", "ID", "DailyPayment", "SalesOrderID")> _
        Public Property SalesOrder() As SalesOrder
            Get
                Try
                    If Not IsNothing(Me._salesOrder) AndAlso (Not Me._salesOrder.IsLoaded) Then

                        Me._salesOrder = CType(DoLoad(GetType(SalesOrder).ToString(), _salesOrder.ID), SalesOrder)
                        Me._salesOrder.MarkLoaded()

                    End If

                    Return Me._salesOrder

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesOrder)

                Me._salesOrder = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesOrder.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ReversedDocNumber", "'{0}'")> _
        Public Property ReversedDocNumber() As String
            Get
                Return _reversedDocNumber
            End Get
            Set(ByVal value As String)
                _reversedDocNumber = value
            End Set
        End Property


        Public ReadOnly Property Accelerator() As DailyPayment
            Get
                If IsNothing(Me._accelerator) Then
                    Me._accelerator = DoLoad(GetType(DailyPayment).ToString, Me._acceleratorID)
                End If
                Return Me._accelerator
            End Get
        End Property

        Public ReadOnly Property OldDailyPayment() As DailyPayment
            Get
                If IsNothing(Me._oldDailyPayment) OrElse Me._oldDailyPayment.ID = 0 Then
                    Dim cDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aDP As New ArrayList

                    cDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
                    cDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratorID", MatchType.Exact, Me.ID))
                    aDP = DoLoadArray(GetType(DailyPayment).ToString, cDP)
                    If aDP.Count > 0 Then
                        Me._oldDailyPayment = CType(aDP(0), DailyPayment)
                    Else
                        Me._oldDailyPayment = New DailyPayment
                    End If
                End If
                Return Me._oldDailyPayment
            End Get
        End Property

        Public ReadOnly Property OtherAssignments(Optional ByVal IsIncludingMe As Boolean = False) As ArrayList
            Get
                If _otherAssignments.Count = 0 Then
                    Dim cDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aDP As New ArrayList

                    cDP.opAnd(New Criteria(GetType(DailyPayment), "SlipNumber", MatchType.Exact, Me.SlipNumber))
                    cDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.Exact, Me.PaymentPurpose.ID))
                    cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.ID", MatchType.Exact, Me.POHeader.Dealer.ID))
                    cDP.opAnd(New Criteria(GetType(DailyPayment), "BaselineDate", MatchType.Exact, Me.BaselineDate))
                    If Not IsIncludingMe Then
                        cDP.opAnd(New Criteria(GetType(DailyPayment), "ID", MatchType.No, Me.ID))
                    End If
                    _otherAssignments = DoLoadArray(GetType(DailyPayment).ToString, cDP)

                End If
                Return _otherAssignments
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
        Private _statusTolakan As String
        Public Property StatusTolakan() As String
            Get
                Return _statusTolakan
            End Get
            Set(ByVal Value As String)
                _statusTolakan = Value
            End Set
        End Property

        Private _IsChangedWSM As Boolean

        Public Property IsChangedWSM() As Boolean
            Get
                Return _IsChangedWSM
            End Get
            Set(ByVal value As Boolean)
                _IsChangedWSM = value
            End Set
        End Property

        Public Function GetBankCode() As String

            'Dim intSpacePos As Integer = Me._slipNumber.IndexOf(" ")
            'Dim strFirst As String = ""
            'Dim strSecond As String = ""
            'strFirst = Left(Me._slipNumber, intSpacePos).Trim
            'strSecond = Right(Me._slipNumber, Me._slipNumber.Length - intSpacePos).Trim

            'Try
            '    If Me._entryType = CShort(EnumGyroEntryType.EntryType.Gyro) Then
            '        Return strFirst
            '    Else
            '        Return strSecond
            '    End If

            'Catch ex As Exception
            Return CType(DoLoad(GetType(Bank).ToString(), _bankID), Bank).BankCode
            'End Try

        End Function

        Public Function GetGyroNumber() As String
            Dim sSlip() As String = Me._slipNumber.Split(" ")
            'Dim i As Integer
            Dim sGyroNumber As String = ""

            If sSlip.Length > 0 Then sGyroNumber = sSlip(sSlip.Length - 1)
            Return sGyroNumber
        End Function

        Public Property Bank() As Bank
            Get
                If IsNothing(Me._bank) OrElse Me._bank.ID < 1 Then
                    Me._bank = DoLoad(GetType(Bank).ToString, Me._bankID)
                End If
                'If IsNothing(Me._accelerator) Then
                '    Me._bank = DoLoad(GetType(Bank).ToString, Me._bankID)
                'End If
                Return Me._bank
            End Get
            Set(ByVal value As Bank)
                Me._bank = value
            End Set
        End Property

        Private _StatusChangeHistoryValidation As StatusChangeHistory

        Private Sub SetStatusChangeHistoryValidation()
            Dim cSCH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aSCH As ArrayList
            If Me.Status = EnumPaymentStatus.PaymentStatus.Validasi OrElse Me.Status = EnumPaymentStatus.PaymentStatus.Selesai Then
                If IsNothing(_StatusChangeHistoryValidation) OrElse _StatusChangeHistoryValidation.id < 1 Then
                    cSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, CType(LookUp.DocumentType.Gyro, Short)))
                    cSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, Me.ID.ToString))
                    cSCH.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Validasi, Short)))
                    aSCH = DoLoadArray(GetType(StatusChangeHistory).ToString, cSCH)
                    If aSCH.Count > 0 Then
                        Me._StatusChangeHistoryValidation = CType(aSCH(0), StatusChangeHistory)
                    Else
                        Me._StatusChangeHistoryValidation = New StatusChangeHistory
                        Me.CreatedTime = New Date(1900, 1, 1)
                    End If
                End If
            Else
                Me._StatusChangeHistoryValidation = New StatusChangeHistory
                Me.CreatedTime = New Date(1900, 1, 1)
            End If
        End Sub

        ReadOnly Property UserValidator() As String
            Get
                Me.SetStatusChangeHistoryValidation()
                If Me._StatusChangeHistoryValidation.CreatedBy.Trim.Length > 6 Then
                    Return Me._StatusChangeHistoryValidation.CreatedBy.Trim.Substring(6)
                Else
                    Return Me._StatusChangeHistoryValidation.CreatedBy
                End If

            End Get
        End Property
        ReadOnly Property TimeValidation() As DateTime
            Get
                Me.SetStatusChangeHistoryValidation()
                Return Me._StatusChangeHistoryValidation.CreatedTime
            End Get
        End Property

        ReadOnly Property BaselineDateOri() As DateTime
            Get
                Dim dt As DateTime

                If Me.PaymentPurpose.PaymentPurposeCode = "PP" Then
                    dt = New Date(Me.POHeader.ReqAllocationDateTime.Year, Me.POHeader.ReqAllocationDateTime.Month, 1)
                    dt = dt.AddMonths(1).AddDays(-1)
                ElseIf Me.PaymentPurpose.PaymentPurposeCode = "IT" Then
                    dt = Me.POHeader.ReqAllocationDateTime
                Else
                    dt = DateAdd(DateInterval.Day, Me.POHeader.TermOfPayment.TermOfPaymentValue, Me.POHeader.ReqAllocationDateTime)
                End If

                Return dt
            End Get
        End Property
#End Region

    End Class
End Namespace

