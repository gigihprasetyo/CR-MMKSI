
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_CeilingPO Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 1/2/2012 - 4:36:17 PM
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
    <Serializable(), TableInfo("sp_CeilingPO")> _
    Public Class sp_CeilingPO
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
        Private _reqAllocationDateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _effectiveDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gyroNumber As String = String.Empty
        Private _gyroStatus As Short
        Private _totalDetail As Decimal
        Private _remark As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _poHeader As POHeader
        Private _dailyPayments As ArrayList
        Private _slipNumbers As String = String.Empty
        Private _baselineDates As String = String.Empty
        Private _gyroStatuss As String = String.Empty

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


        <ColumnInfo("ReqAllocationDateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ReqAllocationDateTime() As DateTime
            Get
                Return _reqAllocationDateTime
            End Get
            Set(ByVal value As DateTime)
                _reqAllocationDateTime = value
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

        <ColumnInfo("GyroNumber", "'{0}'")> _
        Public Property GyroNumber() As String
            Get
                Return _gyroNumber
            End Get
            Set(ByVal value As String)
                _gyroNumber = value
            End Set
        End Property

        <ColumnInfo("GyroStatus", "{0}")> _
        Public Property GyroStatus() As Short
            Get
                Return _GyroStatus
            End Get
            Set(ByVal value As Short)
                _GyroStatus = value
            End Set
        End Property

        <ColumnInfo("TotalDetail", "{0}")> _
        Public Property TotalDetail() As Decimal
            Get
                Return _totalDetail
            End Get
            Set(ByVal value As Decimal)
                _totalDetail = value
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
        ReadOnly Property POHeader() As POHeader
            Get
                If IsNothing(Me._poHeader) OrElse Me._poHeader.ID < 1 Then
                    Me._poHeader = DoLoad(GetType(POHeader).ToString, Me.ID)
                End If
                Return Me._poHeader
            End Get
        End Property

        ReadOnly Property DailyPayments() As ArrayList
            Get
                If IsNothing(Me._dailyPayments) OrElse Me._dailyPayments.Count < 1 Then
                    Dim cDP As New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, Me.ID))
                    cDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, 1))
                    cDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, 1))
                    cDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
                    'Storing CeilingType->If Me.LastUpdateBy = "" Then --storing CeilingType
                    If Me.LastUpdateBy = "1" Then
                        cDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(0,1)"))
                    ElseIf Me.LastUpdateBy = "2" Then
                        cDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(0,1,2)"))
                    ElseIf Me.LastUpdateBy = "3" Then
                        cDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.InSet, "(2)"))
                    End If
                    Me._dailyPayments = DoLoadArray(GetType(DailyPayment).ToString, cDP)
                End If
                Return Me._dailyPayments
            End Get
        End Property

        ReadOnly Property SlipNumbers() As String
            Get
                Me._slipNumbers = Me.GyroNumber

                'Me._slipNumbers = ""
                'For Each oDP As DailyPayment In Me.DailyPayments

                '    Me._slipNumbers &= IIf(Me._slipNumbers.Trim = "", "", "; ") & oDP.SlipNumber
                'Next
                Return Me._slipNumbers
            End Get
        End Property

        ReadOnly Property GyroStatuss() As String
            Get
                Me._gyroStatuss = EnumPaymentStatus.GetStringValue(Me.GyroStatus)
                'Me._gyroStatuss = ""
                'For Each oDP As DailyPayment In Me.DailyPayments
                '    Me._gyroStatuss &= IIf(Me._gyroStatuss.Trim = "", "", "; ") & EnumPaymentStatus.GetStringValue(oDP.Status)
                'Next
                Return Me._gyroStatuss
            End Get
        End Property

        ReadOnly Property BaselineDates() As String
            Get
                Me._baselineDates = ""
                For Each oDP As DailyPayment In Me.DailyPayments
                    Me._baselineDates &= IIf(Me._baselineDates.Trim = "", "", "; ") & oDP.BaselineDate.ToString("dd/MMM/yyyy")
                Next
                Return Me._baselineDates
            End Get
        End Property

#End Region

    End Class
End Namespace

