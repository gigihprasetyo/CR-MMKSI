#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformOrder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 10:46:43 AM
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
    <Serializable(), TableInfo("UniformOrder")> _
    Public Class UniformOrder
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
        Private _orderNum As String = String.Empty
        Private _orderDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _salesmanId As Integer
        Private _jobPositionId As Integer
        Private _uniformCode As String = String.Empty
        Private _uniformSize As String = String.Empty
        Private _qty As Integer
        Private _dealerPrice As Decimal
        Private _amountPrice As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _uniformDistribution As UniformDistribution



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


        <ColumnInfo("OrderNum", "'{0}'")> _
        Public Property OrderNum() As String
            Get
                Return _orderNum
            End Get
            Set(ByVal value As String)
                _orderNum = value
            End Set
        End Property


        <ColumnInfo("OrderDate", "'{0:yyyy/MM/dd}'")> _
        Public Property OrderDate() As DateTime
            Get
                Return _orderDate
            End Get
            Set(ByVal value As DateTime)
                _orderDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("JobPositionId", "{0}")> _
        Public Property JobPositionId() As Integer
            Get
                Return _jobPositionId
            End Get
            Set(ByVal value As Integer)
                _jobPositionId = value
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


        <ColumnInfo("DealerPrice", "#,##0")> _
        Public Property DealerPrice() As Decimal
            Get
                Return _dealerPrice
            End Get
            Set(ByVal value As Decimal)
                _dealerPrice = value
            End Set
        End Property


        <ColumnInfo("AmountPrice", "#,##0")> _
        Public Property AmountPrice() As Decimal
            Get
                Return _amountPrice
            End Get
            Set(ByVal value As Decimal)
                _amountPrice = value
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


        <ColumnInfo("UniformDistributionId", "{0}"), _
        RelationInfo("UniformDistribution", "ID", "UniformOrder", "UniformDistributionId")> _
        Public Property UniformDistribution() As UniformDistribution
            Get
                Try
                    If Not isnothing(Me._uniformDistribution) AndAlso (Not Me._uniformDistribution.IsLoaded) Then

                        Me._uniformDistribution = CType(DoLoad(GetType(UniformDistribution).ToString(), _uniformDistribution.ID), UniformDistribution)
                        Me._uniformDistribution.MarkLoaded()

                    End If

                    Return Me._uniformDistribution

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As UniformDistribution)

                Me._uniformDistribution = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._uniformDistribution.MarkLoaded()
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

