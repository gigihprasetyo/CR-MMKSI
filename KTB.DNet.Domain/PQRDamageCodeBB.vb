#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRDamageCodeBB Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/26/2007 - 1:15:44 PM
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
    <Serializable(), TableInfo("PQRDamageCodeBB")> _
    Public Class PQRDamageCodeBB
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
        Private _rowStatus As Integer
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastModifiedBy As String = String.Empty
        Private _lastModifiedTime As String = String.Empty

        Private _PQRHeaderBB As PQRHeaderBB
        Private _deskripsiKodePosisi As DeskripsiKodePosisi



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


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Integer
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Integer)
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


        <ColumnInfo("LastModifiedBy", "'{0}'")> _
        Public Property LastModifiedBy() As String
            Get
                Return _lastModifiedBy
            End Get
            Set(ByVal value As String)
                _lastModifiedBy = value
            End Set
        End Property


        <ColumnInfo("LastModifiedTime", "'{0}'")> _
        Public Property LastModifiedTime() As String
            Get
                Return _lastModifiedTime
            End Get
            Set(ByVal value As String)
                _lastModifiedTime = value
            End Set
        End Property


        <ColumnInfo("PQRHeaderBBID", "{0}"), _
        RelationInfo("PQRHeaderBB", "ID", "PQRDamageCodeBB", "PQRHeaderBBID")> _
        Public Property PQRHeaderBB() As PQRHeaderBB
            Get
                Try
                    If Not IsNothing(Me._PQRHeaderBB) AndAlso (Not Me._PQRHeaderBB.IsLoaded) Then

                        Me._PQRHeaderBB = CType(DoLoad(GetType(PQRHeaderBB).ToString(), _PQRHeaderBB.ID), PQRHeaderBB)
                        Me._PQRHeaderBB.MarkLoaded()

                    End If

                    Return Me._PQRHeaderBB

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PQRHeaderBB)

                Me._PQRHeaderBB = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._PQRHeaderBB.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DeskripsiKodePosisiID", "{0}"), _
        RelationInfo("DeskripsiKodePosisi", "ID", "PQRDamageCodeBB", "DeskripsiKodePosisiID")> _
        Public Property DeskripsiKodePosisi() As DeskripsiKodePosisi
            Get
                Try
                    If Not IsNothing(Me._deskripsiKodePosisi) AndAlso (Not Me._deskripsiKodePosisi.IsLoaded) Then

                        Me._deskripsiKodePosisi = CType(DoLoad(GetType(DeskripsiKodePosisi).ToString(), _deskripsiKodePosisi.ID), DeskripsiKodePosisi)
                        Me._deskripsiKodePosisi.MarkLoaded()

                    End If

                    Return Me._deskripsiKodePosisi

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DeskripsiKodePosisi)

                Me._deskripsiKodePosisi = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._deskripsiKodePosisi.MarkLoaded()
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

