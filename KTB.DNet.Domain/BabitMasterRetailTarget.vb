
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitMasterRetailTarget Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 20/06/2019 - 8:31:39
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
    <Serializable(), TableInfo("BabitMasterRetailTarget")> _
    Public Class BabitMasterRetailTarget
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
        Private _monthPeriod As Byte
        Private _yearPeriod As Short
        Private _retailTarget As Integer
        Private _status As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _categoryID As Integer
        Private _babitMasterPriceID As Integer
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _unitPrice As Decimal
        Private _specialCategoryFlag As Short
        Private _specialFlag As Short

        'Private _dealerID As Short
        'Private _dealerBranchID As Integer
        'Private _subCategoryVehicleID As Short

        Private _dealer As Dealer
        Private _dealerBranch As DealerBranch
        Private _subCategoryVehicle As SubCategoryVehicle


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


        <ColumnInfo("MonthPeriod", "{0}")> _
        Public Property MonthPeriod As String
            Get
                Return _monthPeriod
            End Get
            Set(ByVal value As String)
                _monthPeriod = value
            End Set
        End Property


        <ColumnInfo("YearPeriod", "{0}")> _
        Public Property YearPeriod As Short
            Get
                Return _yearPeriod
            End Get
            Set(ByVal value As Short)
                _yearPeriod = value
            End Set
        End Property


        <ColumnInfo("RetailTarget", "{0}")> _
        Public Property RetailTarget As Integer
            Get
                Return _retailTarget
            End Get
            Set(ByVal value As Integer)
                _retailTarget = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
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

        <ColumnInfo("DealerID", "{0}"), _
       RelationInfo("Dealer", "ID", "BabitMasterRetailTarget", "DealerID")> _
        Public Property Dealer As Dealer
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
        RelationInfo("DealerBranch", "ID", "BabitMasterRetailTarget", "DealerBranchID")> _
        Public Property DealerBranch As DealerBranch
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

        <ColumnInfo("SubCategoryVehicleID", "{0}"), _
        RelationInfo("SubCategoryVehicle", "ID", "BabitMasterRetailTarget", "SubCategoryVehicleID")> _
        Public Property SubCategoryVehicle As SubCategoryVehicle
            Get
                Try
                    If Not IsNothing(Me._subCategoryVehicle) AndAlso (Not Me._subCategoryVehicle.IsLoaded) Then

                        Me._subCategoryVehicle = CType(DoLoad(GetType(SubCategoryVehicle).ToString(), _subCategoryVehicle.ID), SubCategoryVehicle)
                        Me._subCategoryVehicle.MarkLoaded()

                    End If

                    Return Me._subCategoryVehicle

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SubCategoryVehicle)

                Me._subCategoryVehicle = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._subCategoryVehicle.MarkLoaded()
                End If
            End Set
        End Property
        '<ColumnInfo("DealerID", "{0}")> _
        'Public Property DealerID As Short

        '    Get
        '        Return _dealerID
        '    End Get
        '    Set(ByVal value As Short)
        '        _dealerID = value
        '    End Set
        'End Property
        '<ColumnInfo("DealerBranchID", "{0}")> _
        'Public Property DealerBranchID As Integer

        '    Get
        '        Return _dealerBranchID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _dealerBranchID = value
        '    End Set
        'End Property
        '<ColumnInfo("SubCategoryVehicleID", "{0}")> _
        'Public Property SubCategoryVehicleID As Short

        '    Get
        '        Return _subCategoryVehicleID
        '    End Get
        '    Set(ByVal value As Short)
        '        _subCategoryVehicleID = value
        '    End Set
        'End Property


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
        Public Property BabitMasterPriceID As Integer
            Get
                Return _babitMasterPriceID
            End Get
            Set(ByVal value As Integer)
                _babitMasterPriceID = value
            End Set
        End Property

        Public Property CategoryID As Integer
            Get
                Return _categoryID
            End Get
            Set(ByVal value As Integer)
                _categoryID = value
            End Set
        End Property

        Public Property ValidFrom As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property

        Public Property ValidTo As DateTime
            Get
                Return _validTo
            End Get
            Set(ByVal value As DateTime)
                _validTo = value
            End Set
        End Property

        Public Property UnitPrice As Decimal
            Get
                Return _unitPrice
            End Get
            Set(ByVal value As Decimal)
                _unitPrice = value
            End Set
        End Property

        Public Property SpecialCategoryFlag As Short
            Get
                Return _specialCategoryFlag
            End Get
            Set(ByVal value As Short)
                _specialCategoryFlag = value
            End Set
        End Property

        Public Property SpecialFlag As Short
            Get
                Return _specialFlag
            End Get
            Set(ByVal value As Short)
                _specialFlag = value
            End Set
        End Property

#End Region

    End Class
End Namespace

