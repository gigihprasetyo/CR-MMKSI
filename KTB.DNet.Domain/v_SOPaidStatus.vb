
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_SOPaidStatus Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 09/08/2016 - 16:38:44
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
    <Serializable(), TableInfo("v_SOPaidStatus")> _
    Public Class v_SOPaidStatus
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
        Private _totalVH As Decimal
        Private _totalPP As Decimal
        Private _totalIT As Decimal
        Private _paymentVH As Decimal
        Private _paymentPP As Decimal
        Private _paymentIT As Decimal
        Private _isFullyPaidVH As Integer
        Private _isFullyPaidPP As Integer
        Private _isFullyPaidIT As Integer




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


        <ColumnInfo("TotalVH", "{0}")> _
        Public Property TotalVH As Decimal
            Get
                Return _totalVH
            End Get
            Set(ByVal value As Decimal)
                _totalVH = value
            End Set
        End Property


        <ColumnInfo("TotalPP", "{0}")> _
        Public Property TotalPP As Decimal
            Get
                Return _totalPP
            End Get
            Set(ByVal value As Decimal)
                _totalPP = value
            End Set
        End Property


        <ColumnInfo("TotalIT", "{0}")> _
        Public Property TotalIT As Decimal
            Get
                Return _totalIT
            End Get
            Set(ByVal value As Decimal)
                _totalIT = value
            End Set
        End Property


        <ColumnInfo("PaymentVH", "{0}")> _
        Public Property PaymentVH As Decimal
            Get
                Return _paymentVH
            End Get
            Set(ByVal value As Decimal)
                _paymentVH = value
            End Set
        End Property


        <ColumnInfo("PaymentPP", "{0}")> _
        Public Property PaymentPP As Decimal
            Get
                Return _paymentPP
            End Get
            Set(ByVal value As Decimal)
                _paymentPP = value
            End Set
        End Property


        <ColumnInfo("PaymentIT", "{0}")> _
        Public Property PaymentIT As Decimal
            Get
                Return _paymentIT
            End Get
            Set(ByVal value As Decimal)
                _paymentIT = value
            End Set
        End Property


        <ColumnInfo("IsFullyPaidVH", "{0}")> _
        Public Property IsFullyPaidVH As Integer
            Get
                Return _isFullyPaidVH
            End Get
            Set(ByVal value As Integer)
                _isFullyPaidVH = value
            End Set
        End Property


        <ColumnInfo("IsFullyPaidPP", "{0}")> _
        Public Property IsFullyPaidPP As Integer
            Get
                Return _isFullyPaidPP
            End Get
            Set(ByVal value As Integer)
                _isFullyPaidPP = value
            End Set
        End Property


        <ColumnInfo("IsFullyPaidIT", "{0}")> _
        Public Property IsFullyPaidIT As Integer
            Get
                Return _isFullyPaidIT
            End Get
            Set(ByVal value As Integer)
                _isFullyPaidIT = value
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

