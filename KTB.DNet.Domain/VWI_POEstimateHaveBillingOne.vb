#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_POEstimateHaveBilling Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 12/04/2018 - 14:41:54
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
    <Serializable(), TableInfo("VWI_POEstimateHaveBilling")> _
    Public Class VWI_POEstimateHaveBillingOne
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
        Private _sparePartPOEstimateID As Integer
        Private _sONumber As String = String.Empty
        Private _sODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pONumber As String = String.Empty
        Private _dMSPRNo As String = String.Empty
        Private _dealerID As Integer
        Private _dealerCode As String = String.Empty
        Private _documentType As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _termOfPaymentValue As Integer
        Private _termOfPaymentCode As String = String.Empty
        Private _termOfPaymentDesc As String = String.Empty
        Private _amountC2 As Decimal

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


        <ColumnInfo("SparePartPOEstimateID", "{0}")> _
        Public Property SparePartPOEstimateID As Integer
            Get
                Return _sparePartPOEstimateID
            End Get
            Set(ByVal value As Integer)
                _sparePartPOEstimateID = value
            End Set
        End Property


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property


        <ColumnInfo("SODate", "'{0:yyyy/MM/dd}'")> _
        Public Property SODate As DateTime
            Get
                Return _sODate
            End Get
            Set(ByVal value As DateTime)
                _sODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property


        <ColumnInfo("DMSPRNo", "'{0}'")> _
        Public Property DMSPRNo As String
            Get
                Return _dMSPRNo
            End Get
            Set(ByVal value As String)
                _dMSPRNo = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID As Integer
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Integer)
                _dealerID = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("DocumentType", "'{0}'")> _
        Public Property DocumentType As String
            Get
                Return _documentType
            End Get
            Set(ByVal value As String)
                _documentType = value
            End Set
        End Property

        <ColumnInfo("TermOfPaymentValue", "{0}")> _
        Public Property TermOfPaymentValue As Integer
            Get
                Return _termOfPaymentValue
            End Get
            Set(ByVal value As Integer)
                _termOfPaymentValue = value
            End Set
        End Property

        <ColumnInfo("TermOfPaymentCode", "'{0}'")> _
        Public Property TermOfPaymentCode As String
            Get
                Return _termOfPaymentCode
            End Get
            Set(ByVal value As String)
                _termOfPaymentCode = value
            End Set
        End Property

        <ColumnInfo("TermOfPaymentDesc", "'{0}'")> _
        Public Property TermOfPaymentDesc As String
            Get
                Return _termOfPaymentDesc
            End Get
            Set(ByVal value As String)
                _termOfPaymentDesc = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property

        <ColumnInfo("AmountC2", "{0}")>
        Public Property AmountC2() As Decimal
            Get
                Return _amountC2
            End Get
            Set(ByVal value As Decimal)
                _amountC2 = value
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

