﻿#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ESRUTItem Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/29/2019 - 10:46:49 AM
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
    <Serializable(), TableInfo("ESRUTItem")> _
    Public Class ESRUTItem
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
        Private _ESRUTHeader As ESRUTHeader
        Private _pageNumber As Integer
        Private _engineNumber As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _nomorSRUT As String = String.Empty
        Private _uRLQRCode As String = String.Empty
        Private _chassisMaster As ChassisMaster
        Private _isRevision As Boolean = False
        Private _status As Short
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty




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

        <ColumnInfo("ESRUTHeaderID", "{0}"), _
         RelationInfo("ESRUTHeader", "ID", "ESRUTItem", "ESRUTHeaderID")> _
        Public Property ESRUTHeader As ESRUTHeader
            Get
                Try
                    If Not IsNothing(Me._ESRUTHeader) AndAlso (Not Me._ESRUTHeader.IsLoaded) Then

                        Me._ESRUTHeader = CType(DoLoad(GetType(ESRUTHeader).ToString(), _ESRUTHeader.ID), ESRUTHeader)
                        Me._ESRUTHeader.MarkLoaded()

                    End If

                    Return Me._ESRUTHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ESRUTHeader)

                Me._ESRUTHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ESRUTHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("PageNumber", "{0}")> _
        Public Property PageNumber As Integer
            Get
                Return _pageNumber
            End Get
            Set(ByVal value As Integer)
                _pageNumber = value
            End Set
        End Property


        <ColumnInfo("EngineNumber", "'{0}'")> _
        Public Property EngineNumber As String
            Get
                Return _engineNumber
            End Get
            Set(ByVal value As String)
                _engineNumber = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}"), _
       RelationInfo("ChassisMaster", "ID", "ESRUTItem", "ChassisMasterID")> _
        Public Property ChassisMaster As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        Me._chassisMaster.MarkLoaded()

                    End If

                    Return Me._chassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("NomorSRUT", "'{0}'")> _
        Public Property NomorSRUT As String
            Get
                Return _nomorSRUT
            End Get
            Set(ByVal value As String)
                _nomorSRUT = value
            End Set
        End Property


        <ColumnInfo("URLQRCode", "'{0}'")> _
        Public Property URLQRCode As String
            Get
                Return _uRLQRCode
            End Get
            Set(ByVal value As String)
                _uRLQRCode = value
            End Set
        End Property

        <ColumnInfo("IsRevision", "{0}")> _
        Public Property IsRevision As Boolean
            Get
                Return _isRevision
            End Get
            Set(ByVal value As Boolean)
                _isRevision = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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