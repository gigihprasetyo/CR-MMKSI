﻿
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WarrantyPartAccessories Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 11/11/2019 - 14:17:10
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
    <Serializable(), TableInfo("WarrantyPartAccessories")> _
    Public Class WarrantyPartAccessories
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
        Private _partName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartMaster As SparePartMaster
        'Private _sparePartMasterID As Integer



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


        <ColumnInfo("PartName", "'{0}'")> _
        Public Property PartName As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
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

        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "SparePartMasterATA", "SparePartMasterID")> _
        Public Property SparePartMaster As SparePartMaster
            Get
                Try
                    If Not IsNothing(Me._sparePartMaster) AndAlso (Not Me._sparePartMaster.IsLoaded) Then

                        Me._sparePartMaster = CType(DoLoad(GetType(SparePartMaster).ToString(), _sparePartMaster.ID), SparePartMaster)
                        Me._sparePartMaster.MarkLoaded()

                    End If

                    Return Me._sparePartMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartMaster)

                Me._sparePartMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartMaster.MarkLoaded()
                End If
            End Set
        End Property


        '    <ColumnInfo("SparePartMasterID", "{0}")> _
        '    Public Property SparePartMasterID As Integer

        '        Get
        'return _sparePartMasterID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _sparePartMasterID = value
        '        End Set
        '    End Property


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
