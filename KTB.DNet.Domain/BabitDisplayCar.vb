
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitDisplayCar Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 05/13/2019 - 1:43:11 PM
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
    <Serializable(), TableInfo("BabitDisplayCar")> _
    Public Class BabitDisplayCar
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
        'Private _babitHeaderID As Integer
        'Private _subCategoryVehicleID As Integer
        Private _qty As Integer
        Private _salesTarget As Integer
        Private _isTestDrive As Boolean
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _babitHeader As BabitHeader
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


        '<ColumnInfo("BabitHeaderID", "{0}")> _
        'Public Property BabitHeaderID As Integer
        '    Get
        '        Return _babitHeaderID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _babitHeaderID = value
        '    End Set
        'End Property


        '<ColumnInfo("SubCategoryVehicleID", "{0}")> _
        'Public Property SubCategoryVehicleID As Integer
        '    Get
        '        Return _subCategoryVehicleID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _subCategoryVehicleID = value
        '    End Set
        'End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property


        <ColumnInfo("SalesTarget", "{0}")> _
        Public Property SalesTarget As Integer
            Get
                Return _salesTarget
            End Get
            Set(ByVal value As Integer)
                _salesTarget = value
            End Set
        End Property


        <ColumnInfo("IsTestDrive", "{0}")> _
        Public Property IsTestDrive As Boolean
            Get
                Return _isTestDrive
            End Get
            Set(ByVal value As Boolean)
                _isTestDrive = value
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


        <ColumnInfo("BabitHeaderID", "{0}"), _
        RelationInfo("BabitHeader", "ID", "BabitDisplayCar", "BabitHeaderID")> _
        Public Property BabitHeader As BabitHeader
            Get
                Try
                    If Not IsNothing(Me._babitHeader) AndAlso (Not Me._babitHeader.IsLoaded) Then

                        Me._babitHeader = CType(DoLoad(GetType(BabitHeader).ToString(), _babitHeader.ID), BabitHeader)
                        Me._babitHeader.MarkLoaded()
                    End If
                    Return Me._babitHeader
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As BabitHeader)
                Me._babitHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SubCategoryVehicleID", "{0}"), _
        RelationInfo("SubCategoryVehicle", "ID", "BabitDisplayCar", "SubCategoryVehicleID")> _
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

