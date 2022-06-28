#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformValidation Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:28:59 AM
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
    <Serializable(), TableInfo("UniformValidation")> _
    Public Class UniformValidation
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
        Private _dealerCode As String = String.Empty
        Private _salesmanId As Integer
        Private _uniformCode As String = String.Empty
        Private _uniformSize As String = String.Empty
        Private _qty As Integer
        Private _retailPrice As Decimal
        Private _totalAmount As Decimal
        Private _requestStatus As String = String.Empty
        Private _validationStatus As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _uniformDistributionId As Integer



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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("SalesmanId", "{0}")> _
        Public Property SalesmanId() As Integer
            Get
                Return _salesmanId
            End Get
            Set(ByVal value As Integer)
                _salesmanId = value
            End Set
        End Property


        <ColumnInfo("UniformCode", "'{0}'")> _
        Public Property UniformCode() As String
            Get
                Return _uniformCode
            End Get
            Set(ByVal value As String)
                _uniformCode = value
            End Set
        End Property


        <ColumnInfo("UniformSize", "'{0}'")> _
        Public Property UniformSize() As String
            Get
                Return _uniformSize
            End Get
            Set(ByVal value As String)
                _uniformSize = value
            End Set
        End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty() As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property


        <ColumnInfo("RetailPrice", "#,##0")> _
        Public Property RetailPrice() As Decimal
            Get
                Return _retailPrice
            End Get
            Set(ByVal value As Decimal)
                _retailPrice = value
            End Set
        End Property


        <ColumnInfo("TotalAmount", "#,##0")> _
        Public Property TotalAmount() As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
            End Set
        End Property


        <ColumnInfo("RequestStatus", "'{0}'")> _
        Public Property RequestStatus() As String
            Get
                Return _requestStatus
            End Get
            Set(ByVal value As String)
                _requestStatus = value
            End Set
        End Property


        <ColumnInfo("ValidationStatus", "'{0}'")> _
        Public Property ValidationStatus() As String
            Get
                Return _validationStatus
            End Get
            Set(ByVal value As String)
                _validationStatus = value
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


        <ColumnInfo("UniformDistributionId", "{0}")> _
        Public Property UniformDistributionId() As Integer

            Get
                Return _uniformDistributionId
            End Get
            Set(ByVal value As Integer)
                _uniformDistributionId = value
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

