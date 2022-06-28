
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartBillingDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2016 - 2:51:34 PM
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
    <Serializable(), TableInfo("SparePartBillingDetail")> _
    Public Class SparePartBillingDetail
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
        Private _billingItemNo As Integer
        Private _quantity As Integer
        Private _itemPrice As Decimal
        Private _totalPrice As Decimal
        Private _tax As Decimal
        Private _retailPrice As Decimal
        Private _discount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartDODetail As SparePartDODetail
        Private _sparePartBilling As SparePartBilling



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


        <ColumnInfo("BillingItemNo", "{0}")> _
        Public Property BillingItemNo As Integer
            Get
                Return _billingItemNo
            End Get
            Set(ByVal value As Integer)
                _billingItemNo = value
            End Set
        End Property


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
            End Set
        End Property


        <ColumnInfo("ItemPrice", "{0}")> _
        Public Property ItemPrice As Decimal
            Get
                Return _itemPrice
            End Get
            Set(ByVal value As Decimal)
                _itemPrice = value
            End Set
        End Property


        <ColumnInfo("TotalPrice", "{0}")> _
        Public Property TotalPrice As Decimal
            Get
                Return _totalPrice
            End Get
            Set(ByVal value As Decimal)
                _totalPrice = value
            End Set
        End Property


        <ColumnInfo("Tax", "{0}")> _
        Public Property Tax As Decimal
            Get
                Return _tax
            End Get
            Set(ByVal value As Decimal)
                _tax = value
            End Set
        End Property

        <ColumnInfo("RetailPrice", "{0}")> _
              Public Property RetailPrice As Decimal
            Get
                Return _retailPrice
            End Get
            Set(ByVal value As Decimal)
                _retailPrice = value
            End Set
        End Property


        <ColumnInfo("Discount", "{0}")> _
        Public Property Discount As Decimal
            Get
                Return _discount
            End Get
            Set(ByVal value As Decimal)
                _discount = value
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


        <ColumnInfo("SparePartDODetailID", "{0}"), _
        RelationInfo("SparePartDODetail", "ID", "SparePartBillingDetail", "SparePartDODetailID")> _
        Public Property SparePartDODetail As SparePartDODetail
            Get
                Try
                    If Not IsNothing(Me._sparePartDODetail) AndAlso (Not Me._sparePartDODetail.IsLoaded) Then

                        Me._sparePartDODetail = CType(DoLoad(GetType(SparePartDODetail).ToString(), _sparePartDODetail.ID), SparePartDODetail)
                        Me._sparePartDODetail.MarkLoaded()

                    End If

                    Return Me._sparePartDODetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartDODetail)

                Me._sparePartDODetail = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartDODetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartBillingID", "{0}"), _
        RelationInfo("SparePartBilling", "ID", "SparePartBillingDetail", "SparePartBillingID")> _
        Public Property SparePartBilling As SparePartBilling
            Get
                Try
                    If Not isnothing(Me._sparePartBilling) AndAlso (Not Me._sparePartBilling.IsLoaded) Then

                        Me._sparePartBilling = CType(DoLoad(GetType(SparePartBilling).ToString(), _sparePartBilling.ID), SparePartBilling)
                        Me._sparePartBilling.MarkLoaded()

                    End If

                    Return Me._sparePartBilling

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartBilling)

                Me._sparePartBilling = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartBilling.MarkLoaded()
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

