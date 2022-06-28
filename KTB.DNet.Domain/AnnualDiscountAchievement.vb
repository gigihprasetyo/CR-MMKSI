#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AnnualDiscountAchievement Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:18:29 PM
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
    <Serializable(), TableInfo("AnnualDiscountAchievement")> _
    Public Class AnnualDiscountAchievement
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
        Private _materialCode As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _point As Integer
        Private _minimumQty As Integer
        Private _billQtyThisMonth As Integer
        Private _billQtyThisPeriod As Integer
        Private _rebateQtyThisPeriod As Integer
        Private _semester As Short
        Private _rebateAmountThisPeriod As Integer
        Private _remainQty As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _annualDiscountAchievementHeader As AnnualDiscountAchievementHeader



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


        <ColumnInfo("MaterialCode", "'{0}'")> _
        Public Property MaterialCode() As String
            Get
                Return _materialCode
            End Get
            Set(ByVal value As String)
                _materialCode = value
            End Set
        End Property


        <ColumnInfo("MaterialDescription", "'{0}'")> _
        Public Property MaterialDescription() As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property


        <ColumnInfo("Point", "{0}")> _
        Public Property Point() As Integer
            Get
                Return _point
            End Get
            Set(ByVal value As Integer)
                _point = value
            End Set
        End Property


        <ColumnInfo("MinimumQty", "{0}")> _
        Public Property MinimumQty() As Integer
            Get
                Return _minimumQty
            End Get
            Set(ByVal value As Integer)
                _minimumQty = value
            End Set
        End Property


        <ColumnInfo("BillQtyThisMonth", "{0}")> _
        Public Property BillQtyThisMonth() As Integer
            Get
                Return _billQtyThisMonth
            End Get
            Set(ByVal value As Integer)
                _billQtyThisMonth = value
            End Set
        End Property


        <ColumnInfo("BillQtyThisPeriod", "{0}")> _
        Public Property BillQtyThisPeriod() As Integer
            Get
                Return _billQtyThisPeriod
            End Get
            Set(ByVal value As Integer)
                _billQtyThisPeriod = value
            End Set
        End Property


        <ColumnInfo("RebateQtyThisPeriod", "{0}")> _
        Public Property RebateQtyThisPeriod() As Integer
            Get
                Return _rebateQtyThisPeriod
            End Get
            Set(ByVal value As Integer)
                _rebateQtyThisPeriod = value
            End Set
        End Property


        <ColumnInfo("Semester", "{0}")> _
        Public Property Semester() As Short
            Get
                Return _semester
            End Get
            Set(ByVal value As Short)
                _semester = value
            End Set
        End Property


        <ColumnInfo("RebateAmountThisPeriod", "{0}")> _
        Public Property RebateAmountThisPeriod() As Integer
            Get
                Return _rebateAmountThisPeriod
            End Get
            Set(ByVal value As Integer)
                _rebateAmountThisPeriod = value
            End Set
        End Property


        <ColumnInfo("RemainQty", "{0}")> _
        Public Property RemainQty() As Integer
            Get
                Return _remainQty
            End Get
            Set(ByVal value As Integer)
                _remainQty = value
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


        <ColumnInfo("AnnualDiscountAchivementHeaderID", "{0}"), _
        RelationInfo("AnnualDiscountAchievementHeader", "ID", "AnnualDiscountAchievement", "AnnualDiscountAchivementHeaderID")> _
        Public Property AnnualDiscountAchievementHeader() As AnnualDiscountAchievementHeader
            Get
                Try
                    If Not isnothing(Me._annualDiscountAchievementHeader) AndAlso (Not Me._annualDiscountAchievementHeader.IsLoaded) Then

                        Me._annualDiscountAchievementHeader = CType(DoLoad(GetType(AnnualDiscountAchievementHeader).ToString(), _annualDiscountAchievementHeader.ID), AnnualDiscountAchievementHeader)
                        Me._annualDiscountAchievementHeader.MarkLoaded()

                    End If

                    Return Me._annualDiscountAchievementHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AnnualDiscountAchievementHeader)

                Me._annualDiscountAchievementHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._annualDiscountAchievementHeader.MarkLoaded()
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

