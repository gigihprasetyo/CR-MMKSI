
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKProfile Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 3/7/2011 - 8:40:32 AM
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
    <Serializable(), TableInfo("SPKProfile")> _
    Public Class SPKProfile
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
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _profileHeader As ProfileHeader
        Private _profileGroup As ProfileGroup
        Private _sPKDetail As SPKDetail
        Private _sPKDetailCustomer As SPKDetailCustomer
        Private _sPKDetailCustomerID As Integer




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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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


        <ColumnInfo("ProfileHeaderID", "{0}"), _
        RelationInfo("ProfileHeader", "ID", "SPKProfile", "ProfileHeaderID")> _
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
        RelationInfo("ProfileGroup", "ID", "SPKProfile", "GroupID")> _
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

        <ColumnInfo("SPKDetailID", "{0}"), _
        RelationInfo("SPKDetail", "ID", "SPKProfile", "SPKDetailID")> _
        Public Property SPKDetail() As SPKDetail
            Get
                Try
                    If Not isnothing(Me._sPKDetail) AndAlso (Not Me._sPKDetail.IsLoaded) Then

                        Me._sPKDetail = CType(DoLoad(GetType(SPKDetail).ToString(), _sPKDetail.ID), SPKDetail)
                        Me._sPKDetail.MarkLoaded()

                    End If

                    Return Me._sPKDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKDetail)

                Me._sPKDetail = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKDetail.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SPKDetailCustomerID", "{0}"), _
      RelationInfo("SPKDetailCustomer", "ID", "SPKProfile", "SPKDetailCustomerID")> _
        Public Property SPKDetailCustomer() As SPKDetailCustomer
            Get
                Try
                    If Not isnothing(Me._SPKDetailCustomer) AndAlso (Not Me._SPKDetailCustomer.IsLoaded) Then

                        Me._SPKDetailCustomer = CType(DoLoad(GetType(SPKDetailCustomer).ToString(), _SPKDetailCustomer.ID), SPKDetailCustomer)
                        Me._SPKDetailCustomer.MarkLoaded()

                    End If

                    Return Me._SPKDetailCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKDetailCustomer)

                Me._sPKDetailCustomer = value
                If Not IsNothing(value) Then
                    Me._sPKDetailCustomerID = value.ID
                End If
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._SPKDetailCustomer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPKDetailCustomerID", "{0}")> _
        Public Property SPKDetailCustomerID As Integer
            Get
                Try
                   

                    Return Me._sPKDetailCustomerID

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Integer)

                Me._sPKDetailCustomerID = value
 
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

