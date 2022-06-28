
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitBudgetDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/10/2019 - 17:02:32
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
    <Serializable(), TableInfo("BabitBudgetDetail")> _
    Public Class BabitBudgetDetail
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
        Private _totalAmount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _babitBudgetHeader As BabitBudgetHeader
        Private _babitMasterPrice As BabitMasterPrice
        Private _babitMasterRetailTarget As BabitMasterRetailTarget



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


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
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


        <ColumnInfo("BabitBudgetHeaderID", "{0}"), _
        RelationInfo("BabitBudgetHeader", "ID", "BabitBudgetDetail", "BabitBudgetHeaderID")> _
        Public Property BabitBudgetHeader As BabitBudgetHeader
            Get
                Try
                    If Not IsNothing(Me._babitBudgetHeader) AndAlso (Not Me._babitBudgetHeader.IsLoaded) Then

                        Me._babitBudgetHeader = CType(DoLoad(GetType(BabitBudgetHeader).ToString(), _babitBudgetHeader.ID), BabitBudgetHeader)
                        Me._babitBudgetHeader.MarkLoaded()

                    End If

                    Return Me._babitBudgetHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitBudgetHeader)

                Me._babitBudgetHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitBudgetHeader.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("BabitMasterPriceID", "{0}"), _
        RelationInfo("BabitMasterPrice", "ID", "BabitBudgetDetail", "BabitMasterPriceID")> _
        Public Property BabitMasterPrice As BabitMasterPrice
            Get
                Try
                    If Not IsNothing(Me._babitMasterPrice) AndAlso (Not Me._babitMasterPrice.IsLoaded) Then

                        Me._babitMasterPrice = CType(DoLoad(GetType(BabitMasterPrice).ToString(), _babitMasterPrice.ID), BabitMasterPrice)
                        Me._babitMasterPrice.MarkLoaded()

                    End If

                    Return Me._babitMasterPrice

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitMasterPrice)

                Me._babitMasterPrice = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitMasterPrice.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("BabitMasterRetailTargetID", "{0}"), _
        RelationInfo("BabitMasterRetailTarget", "ID", "BabitBudgetDetail", "BabitMasterRetailTargetID")> _
        Public Property BabitMasterRetailTarget As BabitMasterRetailTarget
            Get
                Try
                    If Not IsNothing(Me._babitMasterRetailTarget) AndAlso (Not Me._babitMasterRetailTarget.IsLoaded) Then

                        Me._babitMasterRetailTarget = CType(DoLoad(GetType(BabitMasterRetailTarget).ToString(), _babitMasterRetailTarget.ID), BabitMasterRetailTarget)
                        Me._babitMasterRetailTarget.MarkLoaded()

                    End If

                    Return Me._babitMasterRetailTarget

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BabitMasterRetailTarget)

                Me._babitMasterRetailTarget = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitMasterRetailTarget.MarkLoaded()
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

