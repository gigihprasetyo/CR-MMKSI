
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_Ceiling Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 1/2/2012 - 4:21:23 PM
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
    <Serializable(), TableInfo("sp_Ceiling")> _
    Public Class sp_Ceiling
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
        Private _ceiling As Decimal
        Private _proposedPO As Decimal
        Private _liquifiedPO As Decimal
        Private _outStanding As Decimal
        Private _maxTOPDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
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


        <ColumnInfo("Ceiling", "{0}")> _
        Public Property Ceiling() As Decimal
            Get
                Return _ceiling
            End Get
            Set(ByVal value As Decimal)
                _ceiling = value
            End Set
        End Property


        <ColumnInfo("ProposedPO", "{0}")> _
        Public Property ProposedPO() As Decimal
            Get
                Return _proposedPO
            End Get
            Set(ByVal value As Decimal)
                _proposedPO = value
            End Set
        End Property


        <ColumnInfo("LiquifiedPO", "{0}")> _
        Public Property LiquifiedPO() As Decimal
            Get
                Return _liquifiedPO
            End Get
            Set(ByVal value As Decimal)
                _liquifiedPO = value
            End Set
        End Property


        <ColumnInfo("OutStanding", "{0}")> _
        Public Property OutStanding() As Decimal
            Get
                Return _outStanding
            End Get
            Set(ByVal value As Decimal)
                _outStanding = value
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

        Private _outstandingSAP As Double

        Public Property OutstandingSAP()
            Get
                Return _outstandingSAP
            End Get
            Set(ByVal Value)
                _outstandingSAP = Value
            End Set
        End Property
#End Region

    End Class
End Namespace

