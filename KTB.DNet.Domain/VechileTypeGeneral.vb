﻿#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VechileTypeGeneral Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 9/10/2020 - 10:04:01 AM
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
    <Serializable(), TableInfo("VechileTypeGeneral")> _
    Public Class VechileTypeGeneral
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Short)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Short
        'Private _subCategoryVehicleID As Short
        Private _name As String = String.Empty
        Private _status As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _subCategoryVehicle As SubCategoryVehicle





#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Short
            Get
                Return _iD
            End Get
            Set(ByVal value As Short)
                _iD = value
            End Set
        End Property


        '<ColumnInfo("SubCategoryVehicleID", "{0}")> _
        'Public Property SubCategoryVehicleID As Short
        '    Get
        '        Return _subCategoryVehicleID
        '    End Get
        '    Set(ByVal value As Short)
        '        _subCategoryVehicleID = value
        '    End Set
        'End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
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


        <ColumnInfo("SubCategoryVehicleID", "{0}"), _
        RelationInfo("SubCategoryVehicle", "ID", "VechileTypeGeneral", "SubCategoryVehicleID")> _
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
