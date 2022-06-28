#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FSKind Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/13/2005 - 11:17:37 AM
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
    <Serializable(), TableInfo("FSKind")> _
    Public Class FSKind
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
        Private _kindCode As String = String.Empty
        Private _kM As Integer
        Private _kindDescription As String = String.Empty
        Private _fsType As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pmKind As PMKind



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


        <ColumnInfo("KindCode", "'{0}'")> _
        Public Property KindCode() As String
            Get
                Return _kindCode
            End Get
            Set(ByVal value As String)
                _kindCode = value
            End Set
        End Property


        <ColumnInfo("KM", "{0}")> _
        Public Property KM() As Integer
            Get
                Return _kM
            End Get
            Set(ByVal value As Integer)
                _kM = value
            End Set
        End Property


        <ColumnInfo("KindDescription", "'{0}'")> _
        Public Property KindDescription() As String
            Get
                Return _kindDescription
            End Get
            Set(ByVal value As String)
                _kindDescription = value
            End Set
        End Property


        <ColumnInfo("FSType", "'{0}'")> _
        Public Property FSType() As String
            Get
                Return _fsType
            End Get
            Set(ByVal value As String)
                _fsType = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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

        <ColumnInfo("PMKindID", "{0}"), _
        RelationInfo("PMKind", "ID", "FSKind", "PMKindID")> _
        Public Property PMKind() As PMKind
            Get
                Try
                    If Not IsNothing(Me._pmKind) AndAlso (Not Me._pmKind.IsLoaded) Then

                        Me._pmKind = CType(DoLoad(GetType(PMKind).ToString(), _pmKind.ID), PMKind)
                        If Not IsNothing(Me._pmKind) Then
                            Me._pmKind.MarkLoaded()
                        End If

                    End If

                    Return Me._pmKind

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PMKind)

                Me._pmKind = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pmKind.MarkLoaded()
                End If
            End Set
        End Property
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace



