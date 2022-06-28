
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RevisionPaymentHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/4/2018 - 9:06:40 AM
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
    <Serializable(), TableInfo("RevisionPaymentHeader")> _
    Public Class RevisionPaymentHeader
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
        Private _dealer As Dealer
        Private _paymentType As String = String.Empty
        Private _regNumber As String = String.Empty
        Private _revisionPaymentDocID As Integer
        Private _slipNumber As String = String.Empty
        Private _totalAmount As Decimal
        Private _status As Short
        Private _evidencePath As String = String.Empty
        Private _actualPaymentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _actualPaymentAmount As Decimal
        Private _accDocNumber As String = String.Empty
        Private _gyroDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _revisionPaymentDetails As System.Collections.ArrayList = New System.Collections.ArrayList

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("PaymentType", "'{0}'")> _
        Public Property PaymentType As String
            Get
                Return _paymentType
            End Get
            Set(ByVal value As String)
                _paymentType = value
            End Set
        End Property


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("RevisionPaymentDocID", "{0}")> _
        Public Property RevisionPaymentDocID As Integer
            Get
                Return _revisionPaymentDocID
            End Get
            Set(ByVal value As Integer)
                _revisionPaymentDocID = value
            End Set
        End Property


        <ColumnInfo("SlipNumber", "'{0}'")> _
        Public Property SlipNumber As String
            Get
                Return _slipNumber
            End Get
            Set(ByVal value As String)
                _slipNumber = value
            End Set
        End Property


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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


        <ColumnInfo("ActualPaymentDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ActualPaymentDate As DateTime
            Get
                Return _actualPaymentDate
            End Get
            Set(ByVal value As DateTime)
                _actualPaymentDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ActualPaymentAmount", "{0}")> _
        Public Property ActualPaymentAmount As Decimal
            Get
                Return _actualPaymentAmount
            End Get
            Set(ByVal value As Decimal)
                _actualPaymentAmount = value
            End Set
        End Property


        <ColumnInfo("AccDocNumber", "'{0}'")> _
        Public Property AccDocNumber As String
            Get
                Return _accDocNumber
            End Get
            Set(ByVal value As String)
                _accDocNumber = value
            End Set
        End Property


        <ColumnInfo("GyroDate", "'{0:yyyy/MM/dd}'")> _
        Public Property GyroDate As DateTime
            Get
                Return _gyroDate
            End Get
            Set(ByVal value As DateTime)
                _gyroDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property

        <RelationInfo("RevisionPaymentHeader", "ID", "RevisionPaymentDetail", "RevisionPaymentHeaderID")> _
        Public ReadOnly Property RevisionPaymentDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._revisionPaymentDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(RevisionPaymentDetail), "RevisionPaymentHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(RevisionPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._revisionPaymentDetails = DoLoadArray(GetType(RevisionPaymentDetail).ToString, criterias)
                    End If

                    Return Me._revisionPaymentDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "RevisionPaymentHeader", "DealerID")> _
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

