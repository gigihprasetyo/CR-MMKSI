
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_BabitMasterRetailTarget Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 08/16/2017 - 3:26:26 PM
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
    <Serializable(), TableInfo("V_BabitMasterRetailTarget")> _
    Public Class V_BabitMasterRetailTarget
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
        Private _quarterPeriod As Short
        Private _quarterPeriodText As String = String.Empty
        Private _yearPeriod As Integer
        Private _subCategoryVehicleID As Short
        Private _retailTarget As Integer
        Private _allocationBabit As Decimal
        Private _additionalPrice As Decimal
        Private _totalAllocationBabit As Decimal
        Private _sumSubsidyAmount As Decimal
        Private _onGoing As Decimal

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _category As Category

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

        <ColumnInfo("QuarterPeriod", "{0}")> _
        Public Property QuarterPeriod As Short
            Get
                Return _quarterPeriod
            End Get
            Set(ByVal value As Short)
                _quarterPeriod = value
            End Set
        End Property

        <ColumnInfo("QuarterPeriodText", "'{0}'")> _
        Public Property QuarterPeriodText As String
            Get
                Return _quarterPeriodText
            End Get
            Set(ByVal value As String)
                _quarterPeriodText = value
            End Set
        End Property

        <ColumnInfo("YearPeriod", "'{0}'")> _
        Public Property YearPeriod As Integer
            Get
                Return _yearPeriod
            End Get
            Set(ByVal value As Integer)
                _yearPeriod = value
            End Set
        End Property

        <ColumnInfo("RetailTarget", "'{0}'")> _
        Public Property RetailTarget As Integer
            Get
                Return _retailTarget
            End Get
            Set(ByVal value As Integer)
                _retailTarget = value
            End Set
        End Property

        <ColumnInfo("SubCategoryVehicleID", "{0}")> _
        Public Property SubCategoryVehicleID As Short
            Get
                Return _subCategoryVehicleID
            End Get
            Set(ByVal value As Short)
                _subCategoryVehicleID = value
            End Set
        End Property


        <ColumnInfo("AllocationBabit", "'{0}'")> _
        Public Property AllocationBabit As Decimal
            Get
                Return _allocationBabit
            End Get
            Set(ByVal value As Decimal)
                _allocationBabit = value
            End Set
        End Property


        <ColumnInfo("AdditionalPrice", "'{0}'")> _
        Public Property AdditionalPrice As Decimal
            Get
                Return _additionalPrice
            End Get
            Set(ByVal value As Decimal)
                _additionalPrice = value
            End Set
        End Property

        <ColumnInfo("TotalAllocationBabit", "'{0}'")> _
        Public Property TotalAllocationBabit As Decimal
            Get
                Return _totalAllocationBabit
            End Get
            Set(ByVal value As Decimal)
                _totalAllocationBabit = value
            End Set
        End Property

        <ColumnInfo("SumSubsidyAmount", "'{0}'")> _
        Public Property SumSubsidyAmount As Decimal
            Get
                Return _sumSubsidyAmount
            End Get
            Set(ByVal value As Decimal)
                _sumSubsidyAmount = value
            End Set
        End Property

        <ColumnInfo("OnGoing", "'{0}'")> _
        Public Property OnGoing As Decimal
            Get
                Return _onGoing
            End Get
            Set(ByVal value As Decimal)
                _onGoing = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("CategoryID", "{0}"), _
      RelationInfo("Category", "ID", "V_BabitMasterRetailTarget", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not IsNothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}"), _
      RelationInfo("Dealer", "ID", "V_BabitMasterRetailTarget", "DealerID")> _
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


        <ColumnInfo("DealerBranchID", "{0}"), _
      RelationInfo("DealerBranch", "ID", "V_BabitMasterRetailTarget", "DealerBranchID")> _
        Public Property DealerBranch() As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealerBranch.ID), DealerBranch)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
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


