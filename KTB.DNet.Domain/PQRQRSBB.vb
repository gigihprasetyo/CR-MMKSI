#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRQRSBB Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/10/2007 - 8:18:37 AM
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
    <Serializable(), TableInfo("PQRQRSBB")> _
    Public Class PQRQRSBB
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
        Private _tglKerusakan As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _note As String = String.Empty
        Private _odometer As Integer
        Private _rowStatus As Integer
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _PQRHeaderBB As PQRHeaderBB
        Private _ChassisMasterBB As ChassisMasterBB



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


        <ColumnInfo("TglKerusakan", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property TglKerusakan() As DateTime
            Get
                Return _tglKerusakan
            End Get
            Set(ByVal value As DateTime)
                _tglKerusakan = value
            End Set
        End Property


        <ColumnInfo("Note", "'{0}'")> _
        Public Property Note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
            End Set
        End Property


        <ColumnInfo("Odometer", "{0}")> _
        Public Property Odometer() As Integer
            Get
                Return _odometer
            End Get
            Set(ByVal value As Integer)
                _odometer = value
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


        <ColumnInfo("PQRHeaderBBID", "{0}"), _
        RelationInfo("PQRHeaderBB", "ID", "PQRQRSBB", "PQRHeaderBBID")> _
        Public Property PQRHeaderBB() As PQRHeaderBB
            Get
                Try
                    If Not isnothing(Me._PQRHeaderBB) AndAlso (Not Me._PQRHeaderBB.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._PQRHeaderBB.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterBBID", "{0}"), _
        RelationInfo("ChassisMasterBB", "ID", "PQRQRSBB", "ChassisMasterBBID")> _
        Public Property ChassisMasterBB() As ChassisMasterBB
            Get
                Try
                    If Not isnothing(Me._ChassisMasterBB) AndAlso (Not Me._ChassisMasterBB.IsLoaded) Then

                        Me._ChassisMasterBB = CType(DoLoad(GetType(ChassisMasterBB).ToString(), _ChassisMasterBB.ID), ChassisMasterBB)
                        Me._ChassisMasterBB.MarkLoaded()

                    End If

                    Return Me._ChassisMasterBB

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMasterBB)

                Me._ChassisMasterBB = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ChassisMasterBB.MarkLoaded()
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

