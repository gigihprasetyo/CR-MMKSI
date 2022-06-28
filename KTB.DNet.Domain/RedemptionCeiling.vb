
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RedemptionCeiling Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 4/29/2010 - 8:36:18 AM
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
    <Serializable(), TableInfo("RedemptionCeiling")> _
    Public Class RedemptionCeiling
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
        Private _creditAccount As String = String.Empty
        Private _paymentType As Byte
        Private _periodMonth As Integer
        Private _periodYear As Integer
        Private _ceiling As Decimal
        Private _maxContractDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("CreditAccount", "'{0}'")> _
        Public Property CreditAccount() As String
            Get
                Return _creditAccount
            End Get
            Set(ByVal value As String)
                _creditAccount = value
            End Set
        End Property


        <ColumnInfo("PaymentType", "{0}")> _
        Public Property PaymentType() As Byte
            Get
                Return _paymentType
            End Get
            Set(ByVal value As Byte)
                _paymentType = value
            End Set
        End Property


        <ColumnInfo("PeriodMonth", "{0}")> _
        Public Property PeriodMonth() As Integer
            Get
                Return _periodMonth
            End Get
            Set(ByVal value As Integer)
                _periodMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodYear", "{0}")> _
        Public Property PeriodYear() As Integer
            Get
                Return _periodYear
            End Get
            Set(ByVal value As Integer)
                _periodYear = value
            End Set
        End Property


        <ColumnInfo("Ceiling", "{0}")> _
        Public Property Ceiling() As Decimal
            Get
                Return _ceiling
            End Get
            Set(ByVal value As Decimal)
                _ceiling = value
            End Set
        End Property


        <ColumnInfo("MaxContractDate", "'{0:yyyy/MM/dd}'")> _
        Public Property MaxContractDate() As DateTime
            Get
                Return _maxContractDate
            End Get
            Set(ByVal value As DateTime)
                _maxContractDate = New DateTime(value.Year, value.Month, value.Day)
                _maxContractDate = _maxContractDate.AddHours(value.Hour).AddMinutes(value.Minute).AddSeconds(value.Second)
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

#End Region

    End Class
End Namespace

