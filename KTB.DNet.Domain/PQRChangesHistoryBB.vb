#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRChangesHistoryBB Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/4/2007 - 10:49:27 AM
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
    <Serializable(), TableInfo("PQRChangesHistoryBB")> _
    Public Class PQRChangesHistoryBB
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
        Private _documentType As String = String.Empty
        Private _oldStatus As String = String.Empty
        Private _newStatus As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _PQRHeaderBB As PQRHeaderBB



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


        <ColumnInfo("DocumentType", "'{0}'")> _
        Public Property DocumentType() As String
            Get
                Return _documentType
            End Get
            Set(ByVal value As String)
                _documentType = value
            End Set
        End Property


        <ColumnInfo("OldStatus", "'{0}'")> _
        Public Property OldStatus() As String
            Get
                Return _oldStatus
            End Get
            Set(ByVal value As String)
                _oldStatus = value
            End Set
        End Property


        <ColumnInfo("NewStatus", "'{0}'")> _
        Public Property NewStatus() As String
            Get
                Return _newStatus
            End Get
            Set(ByVal value As String)
                _newStatus = value
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


        <ColumnInfo("PQRHeaderBBID", "{0}"), _
        RelationInfo("PQRHeaderBB", "ID", "PQRChangesHistoryBB", "PQRHeaderBBID")> _
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

