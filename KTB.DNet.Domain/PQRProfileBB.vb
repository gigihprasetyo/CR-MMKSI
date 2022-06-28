#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRProfileBB Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/23/2007 - 10:23:54 AM
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
    <Serializable(), TableInfo("PQRProfileBB")> _
    Public Class PQRProfileBB
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
        Private _profileValue As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _PQRHeaderBB As PQRHeaderBB
        Private _profileHeader As ProfileHeader
        Private _profileGroup As ProfileGroup

        Private _PQRProfileHistoryBBs As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("ProfileValue", "'{0}'")> _
        Public Property ProfileValue() As String
            Get
                Return _profileValue
            End Get
            Set(ByVal value As String)
                _profileValue = value
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
        RelationInfo("PQRHeaderBB", "ID", "PQRProfileBB", "PQRHeaderBBID")> _
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

        <ColumnInfo("ProfileHeaderID", "{0}"), _
        RelationInfo("ProfileHeader", "ID", "PQRProfileBB", "ProfileHeaderID")> _
        Public Property ProfileHeader() As ProfileHeader
            Get
                Try
                    If Not isnothing(Me._profileHeader) AndAlso (Not Me._profileHeader.IsLoaded) Then

                        Me._profileHeader = CType(DoLoad(GetType(ProfileHeader).ToString(), _profileHeader.ID), ProfileHeader)
                        Me._profileHeader.MarkLoaded()

                    End If

                    Return Me._profileHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProfileHeader)

                Me._profileHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._profileHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("GroupID", "{0}"), _
        RelationInfo("ProfileGroup", "ID", "PQRProfileBB", "GroupID")> _
        Public Property ProfileGroup() As ProfileGroup
            Get
                Try
                    If Not isnothing(Me._profileGroup) AndAlso (Not Me._profileGroup.IsLoaded) Then

                        Me._profileGroup = CType(DoLoad(GetType(ProfileGroup).ToString(), _profileGroup.ID), ProfileGroup)
                        Me._profileGroup.MarkLoaded()

                    End If

                    Return Me._profileGroup

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProfileGroup)

                Me._profileGroup = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._profileGroup.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("PQRProfileBB", "ID", "PQRProfileHistoryBB", "PQRProfileBBID")> _
        Public ReadOnly Property PQRProfileHistoryBBs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._PQRProfileHistoryBBs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PQRProfileHistoryBB), "PQRProfileBB", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PQRProfileHistoryBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._PQRProfileHistoryBBs = DoLoadArray(GetType(PQRProfileHistoryBB).ToString, criterias)
                    End If

                    Return Me._PQRProfileHistoryBBs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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

