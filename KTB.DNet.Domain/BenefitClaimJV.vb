
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimReceipt Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 10:56:00 AM
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
    <Serializable(), TableInfo("BenefitClaimJV")> _
    Public Class BenefitClaimJV
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
        Private _tipeAccount As String = String.Empty
        Private _vendorId As String = String.Empty
        Private _amount As Decimal
        Private _accuredAmount As Decimal
        Private _businessArea As String = String.Empty
        Private _costCenter As String = String.Empty
        Private _paymentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _actualPaymentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _jvNumber As String = String.Empty
        Private _internalOrder As String = String.Empty
        Private _remarks As String = String.Empty
        Private _noBaris As Short
        Private _debitCreditIndicatorValue As Short

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _month As Short
        Private _benefitClaimHeader As BenefitClaimHeader
        Private _masterAccrued As MasterAccrued



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


        <ColumnInfo("TipeAccount", "'{0}'")> _
        Public Property TipeAccount As String
            Get
                Return _tipeAccount
            End Get
            Set(ByVal value As String)
                _tipeAccount = value
            End Set
        End Property

        <ColumnInfo("VendorID", "'{0}'")> _
        Public Property VendorID As String
            Get
                Return _vendorId
            End Get
            Set(ByVal value As String)
                _vendorId = value
            End Set
        End Property

        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property

        <ColumnInfo("AccuredMount", "{0}")> _
        Public Property AccuredMount As Decimal
            Get
                Return _accuredAmount
            End Get
            Set(ByVal value As Decimal)
                _accuredAmount = value
            End Set
        End Property

        <ColumnInfo("BusinessArea", "'{0}'")> _
        Public Property BusinessArea As String
            Get
                Return _businessArea
            End Get
            Set(ByVal value As String)
                _businessArea = value
            End Set
        End Property

        <ColumnInfo("CostCenter", "{0}")> _
        Public Property CostCenter As String
            Get
                Return _costCenter
            End Get
            Set(ByVal value As String)
                _costCenter = value
            End Set
        End Property


        <ColumnInfo("PaymentDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PaymentDate As DateTime
            Get
                Return _paymentDate
            End Get
            Set(ByVal value As DateTime)
                _paymentDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("JVNumber", "{0}")> _
        Public Property JVNumber As String
            Get
                Return _jvNumber
            End Get
            Set(ByVal value As String)
                _jvNumber = value
            End Set
        End Property


        <ColumnInfo("InternalOrder", "{0}")> _
        Public Property InternalOrder As String
            Get
                Return _internalOrder
            End Get
            Set(ByVal value As String)
                _internalOrder = value
            End Set
        End Property


        <ColumnInfo("Remarks", "{0}")> _
        Public Property Remarks As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
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


        <ColumnInfo("Month", "{0}")> _
        Public Property Month As Short
            Get
                Return _month
            End Get
            Set(ByVal value As Short)
                _month = value
            End Set
        End Property


        <ColumnInfo("BenefitClaimHeaderID", "{0}"), _
        RelationInfo("BenefitClaimHeader", "ID", "BenefitClaimJV", "BenefitClaimHeaderID")> _
        Public Property BenefitClaimHeader As BenefitClaimHeader
            Get
                Try
                    If Not IsNothing(Me._benefitClaimHeader) AndAlso (Not Me._benefitClaimHeader.IsLoaded) Then

                        Me._benefitClaimHeader = CType(DoLoad(GetType(BenefitClaimHeader).ToString(), _benefitClaimHeader.ID), BenefitClaimHeader)
                        Me._benefitClaimHeader.MarkLoaded()

                    End If

                    Return Me._benefitClaimHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitClaimHeader)

                Me._benefitClaimHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitClaimHeader.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("MasterAccruedID", "{0}"), _
        RelationInfo("MasterAccrued", "ID", "BenefitClaimJV", "MasterAccruedID")> _
        Public Property MasterAccrued As MasterAccrued
            Get
                Try
                    If Not IsNothing(Me._masterAccrued) AndAlso (Not Me._masterAccrued.IsLoaded) Then

                        Me._masterAccrued = CType(DoLoad(GetType(MasterAccrued).ToString(), _masterAccrued.ID), MasterAccrued)
                        Me._masterAccrued.MarkLoaded()

                    End If

                    Return Me._masterAccrued

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MasterAccrued)

                Me._masterAccrued = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._masterAccrued.MarkLoaded()
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
        Public Property NoBaris As Short
            Get
                Return _noBaris
            End Get
            Set(ByVal value As Short)
                _noBaris = value
            End Set
        End Property

        Public Property DebitCreditIndicatorValue As Short
            Get
                If Amount < 0 Then
                    _debitCreditIndicatorValue = 1  '---Credit
                Else
                    _debitCreditIndicatorValue = 0  '---Debit
                End If

                Return _debitCreditIndicatorValue
            End Get
            Set(ByVal value As Short)
                _debitCreditIndicatorValue = value
            End Set
        End Property

#End Region

    End Class
End Namespace

